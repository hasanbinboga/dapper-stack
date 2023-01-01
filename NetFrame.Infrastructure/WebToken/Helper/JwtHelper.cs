using Microsoft.AspNetCore.Http;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Runtime;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Infrastructure.WebToken
{
    public static class JwtInfos
    {
        public static string JwtKey = "B$7'-4cyG]JtKby/;d5g_#b)Vq4.zé~T5x@3SkD,ra^R6zm#=!wX*2U%Y4`gv3<s";
    }

    public class JwtHelper : IJwtHelper
    {
        private readonly IHttpContextAccessor httpContextAccessor;

        public JwtHelper(IHttpContextAccessor httpContextAccessor)
        {
            this.httpContextAccessor = httpContextAccessor;
        }

        public string GetValueFromToken(string propertyName)
        {
            try
            {
                if (httpContextAccessor.HttpContext == null)
                    return "0";

                var jwt = httpContextAccessor.HttpContext.Request.Headers["Authorization"];
                var handler = new JwtSecurityTokenHandler();
                var tokens = handler.ReadToken(jwt[0]!.Replace("Bearer ", "")) as JwtSecurityToken;
                var deger = tokens!.Claims.FirstOrDefault(claim => claim.Type == propertyName);
                if (deger != null)
                {
                    return deger.Value;
                }
                else
                {
                    return "0";
                }
            }
            catch (Exception)
            {
                return "0";
            }
        }

        public List<string> GetValueListFromToken(string propertyName)
        {
            try
            {
                if (httpContextAccessor.HttpContext == null)
                    return new List<string>();

                var jwt = httpContextAccessor.HttpContext.Request.Headers["Authorization"];

                var handler = new JwtSecurityTokenHandler();
                var tokens = handler.ReadToken(jwt[0]!.Replace("Bearer ", "")) as JwtSecurityToken;

                var claims = tokens!.Claims.Where(claim => claim.Type == propertyName).ToList();

                var valueList = new List<string>();
                foreach (var claim in claims)
                {
                    valueList.Add(claim.Value);
                }
                return valueList;
            }
            catch
            {
                return new List<string>();
            }
        }

        public bool UserHasRole(string roleGuid)
        {
            return GetValueListFromToken("roles").Any(p => p == roleGuid);
        }

        public bool UserHasRoleGroup(string roleGroupGuid)
        {
            return GetValueListFromToken("roleGroups").Any(p => p == roleGroupGuid);
        }


        public JwtDto GetJwtDto()
        {
            JwtDto jwtDto = new();
            try
            {
                jwtDto.Id = !String.IsNullOrEmpty(GetValueFromToken("id")) ? long.Parse(GetValueFromToken("id")) : -1;
                jwtDto.PipeUserId = !String.IsNullOrEmpty(GetValueFromToken("pipeUserId")) ? long.Parse(GetValueFromToken("pipeUserId")) : null;
                jwtDto.PipeUserType = !String.IsNullOrEmpty(GetValueFromToken("pipeUserType")) ? int.Parse(GetValueFromToken("pipeUserType")) : null;
                jwtDto.Name = GetValueFromToken("name");
                jwtDto.Surname = GetValueFromToken("surname");
                jwtDto.UserType = !String.IsNullOrEmpty(GetValueFromToken("userType")) ? int.Parse(GetValueFromToken("userType")) : -1;
                jwtDto.IdentityNo = GetValueFromToken("identityNo");
                jwtDto.Roles = GetValueListFromToken("roles");
                jwtDto.RoleGroups = GetValueListFromToken("roleGroups"); 

                string gender = GetValueFromToken("gender");
                if (!string.IsNullOrEmpty(gender))
                {
                    jwtDto.Gender = int.Parse(gender);
                }

                
                return jwtDto;
            }
            catch (Exception)
            {
                return jwtDto;
            }
        }

        public string ValidatePublicToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                token = token.Replace("Bearer ", "");
                token = token.Replace("Bearer", "");
                var tokens = handler.ReadToken(token) as JwtSecurityToken;
                return tokens?.Claims.FirstOrDefault(claim => claim.Type == "Model")?.Value!;
            }
            catch
            {
                return null!;
            }
        }
        public bool ValidateIntegrationToken(string token, bool isPublic)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                token = token.Replace("Bearer ", "");
                token = token.Replace("Bearer", "");
                var tokens = handler.ReadToken(token) as JwtSecurityToken;
                var sonuc = tokens?.Claims.FirstOrDefault(claim => claim.Type == "isIntegrationPublic");
                if (isPublic)
                    return (sonuc != null && sonuc.Value == "true");
                else
                    return (sonuc != null && sonuc.Value == "false");
            }
            catch
            {
                return false;
            }
        }
        public string ValidateToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                token = token.Replace("Bearer ", "");
                token = token.Replace("Bearer", "");

                ClaimsPrincipal principal = GetPrincipal(token);
                if (principal == null)
                    return null!;

                var tokens = handler.ReadToken(token) as JwtSecurityToken;
                var deger = tokens?.Claims.FirstOrDefault(claim => claim.Type == "id");
                return (deger != null) ? deger.Value : null!;
            }
            catch
            {
                return null!;
            }
        }

        public static ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();
                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);
                if (jwtToken == null)
                    return null!;
                var plainTextBytes = Encoding.UTF8.GetBytes(JwtInfos.JwtKey);
                byte[] key = Convert.FromBase64String(Convert.ToBase64String(plainTextBytes));
                TokenValidationParameters parameters = new TokenValidationParameters()
                {
                    RequireExpirationTime = true,
                    ValidateLifetime = true,
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ClockSkew = new TimeSpan(0, 0, 0),
                };
                SecurityToken securityToken;
                ClaimsPrincipal principal = tokenHandler.ValidateToken(token,
                        parameters, out securityToken);
                return principal;
            }
            catch (Exception)
            {
                return null!;
            }

        }

        public bool IsPublicToken(string token)
        {
            try
            {
                var handler = new JwtSecurityTokenHandler();
                token = token.Replace("Bearer ", "");
                token = token.Replace("Bearer", "");
                var tokens = handler.ReadToken(token) as JwtSecurityToken;
                var sonuc = tokens?.Claims.FirstOrDefault(claim => claim.Type == "isPublic");
                return (sonuc != null && sonuc.Value == "true");
            }
            catch
            {
                return false;
            }
        }
    }
}
