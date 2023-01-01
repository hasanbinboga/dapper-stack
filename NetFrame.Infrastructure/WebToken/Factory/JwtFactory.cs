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
    public class JwtFactory : IJwtFactory
    {
        public async Task<string> GenerateJwtToken(JwtDto jwtDto)
        {
            var claims = new List<Claim>
            {
                new Claim("id",jwtDto.Id.ToString()),
                new Claim("pipeUserId",(jwtDto.PipeUserId.HasValue ? jwtDto.PipeUserId.ToString() : string.Empty)!),
                new Claim("pipeUserType",(jwtDto.PipeUserType.HasValue ? jwtDto.PipeUserType.ToString() : string.Empty) !),
                new Claim("name",jwtDto.Name==null?String.Empty:jwtDto.Name),
                new Claim("surname",jwtDto.Surname==null?String.Empty:jwtDto.Surname),
                new Claim("userType",jwtDto.UserType.ToString()),
                new Claim("identityNo",jwtDto.IdentityNo == null ? String.Empty : jwtDto.IdentityNo),
                new Claim("gender",jwtDto?.Gender.ToString()!), 
                new Claim("isPublic","false"), 
                new Claim("rowGuid",jwtDto?.RowGuid.ToString()!),
            };

            foreach (var role in jwtDto?.Roles!)
            {
                claims.Add(new Claim("roles", role));
            }

            foreach (var group in jwtDto.RoleGroups)
            {
                claims.Add(new Claim("roleGroups", group));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfos.JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenStr = tokenHandler.WriteToken(token);
            return await Task.FromResult(tokenStr).ConfigureAwait(false);
        }

        public string GeneratePublicJwtToken(string modelJson)
        {
            var claims = new List<Claim> {
                new Claim("Model",modelJson ?? string.Empty),
                new Claim("isPublic","true"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfos.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }

        #region Integration Token
        public string GenerateIntegrationPublicJwtToken(string modelJson)
        {
            var claims = new List<Claim> {
                new Claim("Model",modelJson ?? string.Empty),
                new Claim("isIntegrationPublic","true"),
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfos.JwtKey));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddDays(1),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
        public async Task<string> GenerateIntegrationPrivateJwtToken(JwtDto jwtDto)
        {
            var claims = new List<Claim>
            {
                new Claim("id",jwtDto.Id.ToString()),
                new Claim("name",jwtDto.Name),
                new Claim("surname",jwtDto.Surname),
                new Claim("userType",jwtDto.UserType.ToString()),
                new Claim("identityNo",jwtDto.IdentityNo == null ? String.Empty : jwtDto.IdentityNo),
                new Claim("gender",jwtDto ?.Gender.ToString()!), 
                new Claim("isIntegrationPublic","false"),
            };

            foreach (var role in jwtDto?.Roles!)
            {
                claims.Add(new Claim("roles", role));
            }

            foreach (var group in jwtDto.RoleGroups)
            {
                claims.Add(new Claim("roleGroups", group));
            }

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtInfos.JwtKey));

            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddHours(9),
                SigningCredentials = creds
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            string tokenStr = tokenHandler.WriteToken(token);
            return await Task.FromResult(tokenStr).ConfigureAwait(false);
        }

        #endregion

    }
}
