using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Infrastructure.WebToken 
{
    public interface IJwtFactory
    {
        Task<string> GenerateJwtToken(JwtDto jwtTokenDto);

        #region Common
        string GeneratePublicJwtToken(string modelJson);

        #endregion

        #region Integration Token
        string GenerateIntegrationPublicJwtToken(string modelJson);
        Task<string> GenerateIntegrationPrivateJwtToken(JwtDto jwtTokenDto);


        #endregion
    }
}
