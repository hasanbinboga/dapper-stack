using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using NetFrame.Infrastructure.WebToken;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Infrastructure
{
    public class ElasticSearchLogActionFilter : IActionFilter
    {
        
        private readonly IConfiguration _configuration;
        private readonly JwtDto currentUser;

        private ActionExecutingContext _actionContext;
        private ActionExecutedContext _actionExecutedContext;
        private ElasticsearchLog elasticLogDto;
        private string actionName = string.Empty;
        private string controllerName = string.Empty;
        private string ipAddress = string.Empty;

        public ElasticSearchLogActionFilter(IConfiguration configuration, IJwtHelper jwtHelper)
        {   
            _configuration = configuration;
            this.currentUser = jwtHelper.GetJwtDto();
            _actionContext = null!;
            _actionExecutedContext = null!;
            elasticLogDto = null!;
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
            _actionContext = context;
            ipAddress = context.HttpContext.Request.Headers["ClientIP"].ToString();
            actionName = _actionContext.ActionDescriptor.RouteValues["action"]!;
            controllerName = _actionContext.ActionDescriptor.RouteValues["controller"]!;

            EleasticSearchExecuting();
        }
        public void OnActionExecuted(ActionExecutedContext actionExecutedContext)
        {
            _actionExecutedContext = actionExecutedContext;
            EleasticSearchExecuted();
        }

        #region Elastic Search
        private void EleasticSearchExecuting()
        {
            try
            {
                var parameters = JsonConvert.SerializeObject(_actionContext.ActionArguments);

                elasticLogDto = new ElasticsearchLog
                {
                    ControllerName = controllerName,
                    Name = actionName,
                    Parameters = parameters,
                    UserId = currentUser?.Id
                };
                elasticLogDto = StartElasticLog(elasticLogDto);
            }
            catch
            {
                try
                {
                    elasticLogDto = new ElasticsearchLog
                    {
                        ControllerName = controllerName,
                        Name = actionName,
                        UserId = currentUser?.Id
                    };
                    elasticLogDto = StartElasticLog(elasticLogDto);
                }
                catch
                {
                }
            }
        }

        private void EleasticSearchExecuted()
        {
            try
            {
                if (_actionExecutedContext.Exception == null)
                {
                    var pattern = _configuration.GetSection("ElasticPattern").Value?.Split(',').Select(x => x.ToString()).ToList();

                    string parameters = string.Empty;
                    string response = string.Empty;
                    string token = string.Empty;
                    try
                    {
                        foreach (object item in _actionContext.ActionArguments)
                        {
                            foreach (var v in ((KeyValuePair<string, object>)item).Value.GetType().GetProperties())
                            {
                                if (pattern!.Contains(v.Name.ToLower()) || pattern.Contains(v.Name))
                                {
                                    PropertyInfo propertyInfo = ((KeyValuePair<string, object>)item).Value.GetType().GetProperty(v.Name)!;
                                    try
                                    {
                                        propertyInfo.SetValue(((KeyValuePair<string, object>)item).Value, Convert.ChangeType("******", v.PropertyType), null);
                                    }
                                    catch (Exception)
                                    {
                                        propertyInfo.SetValue(((KeyValuePair<string, object>)item).Value, Convert.ChangeType(0, v.PropertyType), null);
                                    }
                                }
                            }

                        }
                        parameters = JsonConvert.SerializeObject(_actionContext.ActionArguments.ToList());
                    }
                    catch (Exception ex)
                    {
                        parameters = string.Format("{0} | {1}", ex.Message, ex.StackTrace);
                    }

                    try
                    {
                        var resModelJson = (ServiceResult) ((ObjectResult)_actionExecutedContext.Result!).Value!;
                        var jsonResponse = JsonConvert.SerializeObject(resModelJson);
                        elasticLogDto.Parameters = parameters;
                        elasticLogDto.Response = jsonResponse;
                        EndElasticLog(elasticLogDto);
                    }
                    catch
                    {
                    }
                }
            }
            catch
            {
            }
        }

        private ElasticsearchLog StartElasticLog(ElasticsearchLog model)
        {
            try
            {


                string serverInfo = string.Empty;
                try
                {

                }
                catch
                {
                }

                model.HostName = System.Net.Dns.GetHostName();
                model.ServerInfo = serverInfo;
                model.IpAddress = ipAddress;
                model.StartTime = DateTime.UtcNow;
                return model;
            }
            catch
            {
                return null!;
            }
        }

        private void EndElasticLog(ElasticsearchLog model)
        {
            try
            {
                if (model != null)
                {
                    model.EndTime = DateTime.UtcNow;
                    TimeSpan duration = model.EndTime.Value - model.StartTime!.Value;
                    model.ExecutionMs = duration.TotalMilliseconds;
                    model.LogTime = DateTime.UtcNow;
                    using (ElasticsearchService ser = new())
                    {
                        ser.Index(model, "elk_log-" + DateTime.UtcNow.ToString("yyyy.MM.dd"));
                    }
                }
            }
            catch
            {
            }
        }
        #endregion
    }
}
