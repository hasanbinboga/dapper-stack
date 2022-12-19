using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Net;
using System.Reflection;
using System.Text;

namespace NetFrame.Infrastructure
{
    public class AntiXssActionFilter : IActionFilter
    {

        private ServiceResult _error = new ServiceResult();
        
        public void OnActionExecuting(ActionExecutingContext context)
        {

            // Check XSS in URL
            if (!string.IsNullOrWhiteSpace(context.HttpContext.Request.Path.Value))
            {
                var url = context.HttpContext.Request.Path.Value;

                if (CrossSiteScriptingValidation.IsDangerousString(url, out _))
                {
                    RespondWithAnError(context);
                    return;
                }
            }

            // Check XSS in query string
            if (!string.IsNullOrWhiteSpace(context.HttpContext.Request.QueryString.Value))
            {
                var queryString = WebUtility.UrlDecode(context.HttpContext.Request.QueryString.Value);

                if (CrossSiteScriptingValidation.IsDangerousString(queryString, out _))
                {
                    RespondWithAnError(context);
                    return;
                }
            }

            context.HttpContext.Request.EnableBuffering();
            // Check XSS in request content
            context.HttpContext.Request.Body.Seek(0, SeekOrigin.Begin);
            var originalBody = context.HttpContext.Request.Body;
            try
            {
                var content = CrossSiteScriptingValidation.ReadRequestBody(context.HttpContext).Result;

                if (content.Contains("<script") || content.Contains("/script"))
                {
                    RespondWithAnError(context);
                    return;
                }

                if (!content.Contains("Content-Disposition"))
                {

                    List<string> preventFilterFields = new List<string>();
                    PropertyInfo[] props = context.ActionArguments.Values.FirstOrDefault()?.GetType().GetProperties()!;
                    if (props != null)
                    {
                        foreach (PropertyInfo prop in props)
                        {
                            if (prop.GetCustomAttributes(true)
                                .Any(q => q.ToString() == typeof(NotXssKontrolAttribute).ToString()))
                            {
                                preventFilterFields.Add(LowerFirstCharacter(prop.Name));
                            }
                        }
                    }

                    foreach (var fieldName in preventFilterFields)
                    {
                        JObject jo = JObject.Parse(content);
                        jo.Property(fieldName)?.Remove();
                        content = jo.ToString();
                    }


                    if (CrossSiteScriptingValidation.IsDangerousString(content, out _))
                    {
                        RespondWithAnError(context);
                        return;
                    }
                }
            }
            finally
            {
                context.HttpContext.Request.Body = originalBody;
            }

        }
        public void OnActionExecuted(ActionExecutedContext context)
        {
            // Method intentionally left empty.
        }

        private string LowerFirstCharacter(string value)
        {
            if (string.IsNullOrEmpty(value)) return value;
            var strList = value.TrimEnd().TrimStart().Split(" ");
            string returnValue = "";

            foreach (string input in strList)
            {
                returnValue += string.Concat(input[0].ToString().ToLower(), input.AsSpan(1)) + " ";
            }
            returnValue = returnValue.Remove(returnValue.Length - 1);
            return returnValue;
        }

        private void RespondWithAnError(ActionExecutingContext context)
        {
             

            if (_error == null)
            {
                _error = new ServiceResult
                {
                    Message = "An attempt to run a cross-site command has been detected. Check the content you entered.",
                    ResultType = ResultType.Warning,
                };
            }

            context.Result = new OkObjectResult(_error);
        }
    }


    /// <summary>
    /// Imported from System.Web.CrossSiteScriptingValidation Class
    /// </summary>
    public static class CrossSiteScriptingValidation
    {
        private static readonly char[] StartingChars = { '<', '&' };

        #region Public methods

        public static bool IsDangerousString(string s, out int matchIndex)
        {
            //bool inComment = false;
            matchIndex = 0;
            for (var i = 0; ;)
            {
                // Look for the start of one of our patterns
                var n = s.IndexOfAny(StartingChars, i);

                // If not found, the string is safe
                if (n < 0) return false;

                // If it's the last char, it's safe
                if (n == s.Length - 1) return false;

                matchIndex = n;

                switch (s[n])
                {
                    case '<':
                        // If the < is followed by a letter or '!', it's unsafe (looks like a tag or HTML comment)
                        if (IsAtoZ(s[n + 1]) || s[n + 1] == '!' || s[n + 1] == '/' || s[n + 1] == '?') return true;
                        break;
                    case '&':
                        // If the & is followed by a #, it's unsafe (e.g. S)
                        if (s[n + 1] == '#') return true;
                        break;

                }

                // Continue searching
                i = n + 1;
            }

        }

        public static async Task<string> ReadRequestBody(HttpContext context)
        {
            var buffer = new MemoryStream();
            await context.Request.Body.CopyToAsync(buffer);
            context.Request.Body = buffer;
            buffer.Position = 0;

            var encoding = Encoding.UTF8;

            var requestContent = await new StreamReader(buffer, encoding).ReadToEndAsync();
            context.Request.Body.Position = 0;

            return requestContent;
        }




        #endregion

        #region Private methods

        private static bool IsAtoZ(char c)
        {
            return (c >= 'a' && c <= 'z') || (c >= 'A' && c <= 'Z');
        }

        #endregion

        public static void AddHeaders(this IHeaderDictionary headers)
        {
            if (headers["P3P"].IsNullOrEmpty())
            {
                headers.Add("P3P", "CP=\"IDC DSP COR ADM DEVi TAIi PSA PSD IVAi IVDi CONi HIS OUR IND CNT\"");
            }
        }

        public static bool IsNullOrEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || !source.Any();
        }
        public static string ToJSON(this object value)
        {
            return JsonConvert.SerializeObject(value);
        }
    }

    public class ErrorResponse
    {
        public int ErrorCode { get; set; }
        public string Description { get; set; } = string.Empty;
    }
}
