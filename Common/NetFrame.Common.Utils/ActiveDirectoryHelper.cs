using NetFrame.Common.Exception;
using System.Collections;
using System.DirectoryServices.AccountManagement;
using System.Runtime.Versioning;

namespace NetFrame.Common.Utils
{
    /// <summary>
    /// Active Directory fonksiyonlarını içeren sınıf 
    /// </summary>
    [SupportedOSPlatform("windows")]
    public class ActiveDirectoryHelper
    {
        #region Variables

        private readonly string _sDomain;
        private readonly string _sDefaultOu;
        private readonly string _sServiceUser;
        private readonly string _sServicePassword;

        #endregion

        /// <summary>
        /// Constructor
        /// </summary>
        public ActiveDirectoryHelper(string domain, string searchBase, string user, string pass)
        {

            if (string.IsNullOrEmpty(domain) || string.IsNullOrEmpty(searchBase)
                || string.IsNullOrEmpty(user) || string.IsNullOrEmpty(pass))
                throw new UtilsException("Active Directory related config settings doesn't not exist");

            _sDomain = domain;
            _sDefaultOu = searchBase;
            _sServiceUser = user;
            _sServicePassword = pass;
        }

        #region Validate Methods

        ///<summary>
        ///Verilen kullanıcın username and password bilgilerinin uygunluğunu kontrol eder
        ///</summary>
        ///<param name="sUserName">username bilgisi</param>
        ///<param name="sPassword">password bilgisi</param>
        ///<returns>Kullanıcı uygun mu sonucu</returns>
        public bool ValidateCredentials(string sUserName, string sPassword)
        {
            var oPrincipalContext = GetPrincipalContext();
            return oPrincipalContext.ValidateCredentials(sUserName, sPassword);
        }

        ///<summary>
        /// Kullanıcı hesabının süresi dolmuş mu kontrolü
        ///</summary>
        ///<param name="sUserName">username bilgisi</param>
        ///<returns>sürenin dolup dolmadığı bilgisi</returns>
        public bool IsUserExpired(string sUserName)
        {
            var oUserPrincipal = GetUser(sUserName);

            return oUserPrincipal.AccountExpirationDate == null;
        }

        ///<summary>
        /// Kullanıcı adı mevcut mu kontrolü
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<returns>Kullanıcı adı varsa tru döner</returns>
        public bool IsUserExisiting(string sUserName)
        {
            return GetUser(sUserName) != null;
        }

        ///<summary>
        ///Kullanıcı hesabının kilitlenip kilitlenmediğini kontrol eder
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<returns>Kullanıcı hesabının kilitlenmişse true döner</returns>
        public bool IsAccountLocked(string sUserName)
        {
            var oUserPrincipal = GetUser(sUserName);
            return oUserPrincipal.IsAccountLockedOut();
        }

        #endregion

        #region Search Methods

        ///<summary>
        ///Active Directory kesin olan kullanıcı bilgisini döndürür
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<returns>UserPrincipal Objesini döndürür</returns>
        public UserPrincipal GetUser(string sUserName)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext();

            UserPrincipal oUserPrincipal =
               UserPrincipal.FindByIdentity(oPrincipalContext, sUserName);
            return oUserPrincipal;
        }

        ///<summary>
        /// Active Directory kesin olan grup bilgisini döndürür
        ///</summary>
        ///<param name="sGroupName">Grup ismi bilgisi</param>
        ///<returns>GroupPrincipal Objesini döndürür</returns>
        public GroupPrincipal GetGroup(string sGroupName)
        {
            var oPrincipalContext = GetPrincipalContext();

            var oGroupPrincipal =
               GroupPrincipal.FindByIdentity(oPrincipalContext, sGroupName);
            return oGroupPrincipal;
        }

        #endregion

        #region User Account Methods

        ///<summary>
        /// Kullanıcı şifresini SET eder
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<param name="sNewPassword">yeni belirlenen şifre</param>
        ///<param name="sMessage">Verilmek istenen herahngibir mesaj</param>
        public void SetUserPassword(string sUserName, string sNewPassword, out string sMessage)
        {
            try
            {
                var oUserPrincipal = GetUser(sUserName);
                oUserPrincipal.SetPassword(sNewPassword);
                sMessage = "";
            }
            catch (System.Exception ex)
            {
                sMessage = ex.Message;
            }
        }

        ///<summary>
        /// Aktif olmayan kullanıcı hesabını aktif yapar
        ///</summary>
        ///<param name="sUserName">aktif yapılacak kullanıcı bilgisi</param>
        public void EnableUserAccount(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            oUserPrincipal.Enabled = true;
            oUserPrincipal.Save();
        }

        ///<summary>
        /// Kullanıcı hesabının aktifliğini bitirir
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        public void DisableUserAccount(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            oUserPrincipal.Enabled = false;
            oUserPrincipal.Save();
        }

        ///<summary>
        /// Kullanıcının şifresinin kullanım süresini dolmuş yapar
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        public void ExpireUserPassword(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            oUserPrincipal.ExpirePasswordNow();
            oUserPrincipal.Save();
        }

        ///<summary>
        /// Kilitlenen hesabı açar
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        public void UnlockUserAccount(string sUserName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            oUserPrincipal.UnlockAccount();
            oUserPrincipal.Save();
        }

        ///<summary>
        /// Active Directory'de yeni bir kullanıcı oluşturur
        ///</summary>
        ///<param name="sou">Kullanıcıyı kaydetmek istediğimiz OU location'u</param>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<param name="sPassword">Kullanıcı şifre bilgisi</param>
        ///<param name="sGivenName">Kullanıcıya verilen ad</param>
        ///<param name="sSurname">Kullanıcı soyadı bilgisi</param>
        ///<returns>UserPrincipal objesini döndürür</returns>
        public UserPrincipal CreateNewUser(string sou, string sUserName, string sPassword, string sGivenName, string sSurname)
        {
            if (!IsUserExisiting(sUserName))
            {
                PrincipalContext oPrincipalContext = GetPrincipalContext(sou);

                var oUserPrincipal = new UserPrincipal(oPrincipalContext, sUserName, sPassword, true /*Enabled or not*/)
                {
                    UserPrincipalName = sUserName,
                    GivenName = sGivenName,
                    Surname = sSurname
                };

                //User Log on Name
                oUserPrincipal.Save();

                return oUserPrincipal;
            }

            return GetUser(sUserName);
        }

        ///<summary>
        /// Active Directory'den kullanıcı siler
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<returns>Kullanıcı başarıyla silindiyse true döndürür.</returns>
        public bool DeleteUser(string sUserName)
        {
            try
            {
                var oUserPrincipal = GetUser(sUserName);

                oUserPrincipal.Delete();
                return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Group Methods

        ///<summary>
        /// Active Directory'de grup oluşturur.
        ///</summary>
        ///<param name="sou">Kullanıcıyı kaydetmek istediğimiz OU location'u</param>
        ///<param name="sGroupName">Yeni grup adı</param>
        ///<param name="sDescription">Grup açıklaması</param>
        ///<param name="oGroupScope">Grup scobu</param>
        ///<param name="bSecurityGroup">Güvenlikli grupsa true,değilse false  </param>
        ///<returns>GroupPrincipal objesi döner.</returns>
        public GroupPrincipal CreateNewGroup(string sou, string sGroupName,
           string sDescription, GroupScope oGroupScope, bool bSecurityGroup)
        {
            PrincipalContext oPrincipalContext = GetPrincipalContext(sou);

            var oGroupPrincipal = new GroupPrincipal(oPrincipalContext, sGroupName)
            {
                Description = sDescription,
                GroupScope = oGroupScope,
                IsSecurityGroup = bSecurityGroup
            };

            oGroupPrincipal.Save();

            return oGroupPrincipal;
        }

        ///<summary>
        /// Bir gruba kullanıcı ekler
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<param name="sGroupName">Grup adı bilgisi</param>
        ///<returns>Başarılıysa true döner.</returns>
        public bool AddUserToGroup(string sUserName, string sGroupName)
        {
            try
            {
                var oUserPrincipal = GetUser(sUserName);
                var oGroupPrincipal = GetGroup(sGroupName);
                if (oUserPrincipal != null && oGroupPrincipal != null)
                {
                    if (!IsUserGroupMember(sUserName, sGroupName))
                    {
                        oGroupPrincipal?.Members.Add(oUserPrincipal);
                        oGroupPrincipal!.Save();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        ///<summary>
        /// Grupdan kullanıcı siler
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<param name="sGroupName">Grup adı bilgisi</param>
        ///<returns>Başarılıysa true döner.</returns>
        public bool RemoveUserFromGroup(string sUserName, string sGroupName)
        {
            try
            {
                var oUserPrincipal = GetUser(sUserName);
                var oGroupPrincipal = GetGroup(sGroupName);

                if (oUserPrincipal != null && oGroupPrincipal != null)
                {
                    if (IsUserGroupMember(sUserName, sGroupName))
                    {
                        // ReSharper disable once PossibleNullReferenceException
                        // ReSharper disable once AssignNullToNotNullAttribute
                        oGroupPrincipal.Members.Remove(oUserPrincipal);
                        oGroupPrincipal.Save();
                    }
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        ///<summary>
        /// Kullanıcı grubun zaten bir üyesimi kontrolünü yapar
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<param name="sGroupName">Grup adı bilgisi</param>
        ///<returns>Kullanıcı grubun zaten üyesiyse true döner</returns>
        public bool IsUserGroupMember(string sUserName, string sGroupName)
        {
            UserPrincipal oUserPrincipal = GetUser(sUserName);
            GroupPrincipal oGroupPrincipal = GetGroup(sGroupName);

            if (oUserPrincipal != null && oGroupPrincipal != null)
            {
                // ReSharper disable once PossibleNullReferenceException
                // ReSharper disable once AssignNullToNotNullAttribute
                return oGroupPrincipal.Members.Contains(oUserPrincipal);
            }

            return false;
        }

        ///<summary>
        /// Kullanıcının kayıtlı olduğu grupların listesini döner
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<returns>Kullanıcın üyesi olduğu grupların listesini döner.</returns>
        public ArrayList GetUserGroups(string sUserName)
        {
            var myItems = new ArrayList();

            var oUserPrincipal = GetUser(sUserName);

            var oPrincipalSearchResult = oUserPrincipal.GetGroups();

            foreach (var oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }
            return myItems;
        }

        ///<summary>
        /// Kullanıcının authorization olduğu grupların listesini döner
        ///</summary>
        ///<param name="sUserName">Kullanıcı adı bilgisi</param>
        ///<returns>Kullanıcının authorization olduğu grupların listesi</returns>
        public ArrayList GetUserAuthorizationGroups(string sUserName)
        {
            var myItems = new ArrayList();

            var oUserPrincipal = GetUser(sUserName);

            var oPrincipalSearchResult =
                       oUserPrincipal.GetAuthorizationGroups();

            foreach (Principal oResult in oPrincipalSearchResult)
            {
                myItems.Add(oResult.Name);
            }
            return myItems;
        }

        #endregion

        #region Helper Methods

        ///<summary>
        /// Gets the main principal context.
        ///</summary>
        ///<returns>Return PrincipalContext object</returns>
        public PrincipalContext GetPrincipalContext()
        {
            var oPrincipalContext = new PrincipalContext(ContextType.Domain, _sDomain, _sDefaultOu,
                ContextOptions.SimpleBind, _sServiceUser, _sServicePassword);

            return oPrincipalContext;
        }

        ///<summary>
        /// Belirli OU için principal context alır.
        ///</summary>
        ///<param name="sou"> OU bilgisi </param>
        ///<returns>PrincipalContext object döner</returns>
        public PrincipalContext GetPrincipalContext(string sou)
        {
            var oPrincipalContext = new PrincipalContext(ContextType.Domain, _sDomain, sou,
               ContextOptions.SimpleBind, _sServiceUser, _sServicePassword);

            return oPrincipalContext;
        }

        #endregion
    }
}
