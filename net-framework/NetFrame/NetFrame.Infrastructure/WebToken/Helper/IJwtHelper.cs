using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetFrame.Infrastructure.WebToken
{
    public interface IJwtHelper
    {
        string GetValueFromToken(string propertyName);
        List<string> GetValueListFromToken(string propertyName);
        bool UserHasRole(string roleGuid);
        bool UserHasRoleGroup(string roleGroupGuid);
        JwtDto GetJwtDto();

        #region Common
        string ValidatePublicToken(string token);
        string ValidateToken(string token);
        bool IsPublicToken(string token);
        bool ValidateIntegrationToken(string token, bool isPublic);

        #endregion
    }
}
