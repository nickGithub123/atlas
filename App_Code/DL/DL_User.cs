using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Collections.Generic;

namespace AtlasIndia.AntechCSM
{
    /// <summary>
    /// User Specific Data Access
    /// </summary>
    public class DL_User
    {
        public DL_User()
        {
            //
        }

        public static string validateUserLogin(String userName, String password, String passwordExpiryPeriod)
        {
            Dictionary<string, string> userInfo = new Dictionary<string, string>();
            userInfo.Add("USERID", userName);
            userInfo.Add("PASSWORD", password);
            userInfo.Add("PASSEXPPERIOD", passwordExpiryPeriod);
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.StoredProcedure("?=call SP2_ValidateUserLogin(?,?,?)", userInfo, 32000).Value.ToString();
        }

        public static DataTable getUserDetails(String userName, String password)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("SELECT ");
            sb.Append("USER_UserID As UserID,");
            sb.Append("USER_LastFirstName As Name,");
            sb.Append("USER_LabLocationDR->LABLO_RowID1 As LabID,");
            sb.Append("USER_IsClientServicesUser As IsCSUser,");
            sb.Append("USER_HasSupervisoryPrivilege As IsSupervisor,");
            sb.Append("USER_ConsultantDR As ConsultantID, ");
            sb.Append("USER_CanChangeClientPassword As CanChangeClientPassword, ");
            sb.Append("USER_IsLabC As IsLabC,");
            sb.Append("USER_EmailAddress As UserEmail,");
            sb.Append("USER_IsActive As IsActive,");
            sb.Append("USER_DisplayUserID As UserDispName,");
            sb.Append("USER_IsSuperUser As IsSuperUser ");
            sb.Append("FROM DIC_User");
            sb.Append(" WHERE %SQLUPPER USER_UserID=%SQLUPPER '" + userName + "'");
            sb.Append(" AND USER_Password='" + password + "'");
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(sb.ToString());
        }

        public static DataTable getUserDetails(String userName)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            sb.Append("SELECT ");
            sb.Append("USER_UserID As UserID,");
            sb.Append("USER_LastFirstName As Name,");
            sb.Append("USER_LabLocationDR->LABLO_RowID1 As LabID,");
            sb.Append("USER_NumberOfNewMessages As NewMessageCount,");
            sb.Append("USER_IsClientServicesUser As IsCSUser,");
            sb.Append("USER_HasSupervisoryPrivilege As IsSupervisor,");
            sb.Append("USER_EmailAddress As UserEmail,");
            sb.Append("USER_DisplayUserID As UserDispName,");
            sb.Append("USER_IsSuperUser As IsSuperUser ");
            sb.Append("FROM DIC_User");
            sb.Append(" WHERE %SQLUPPER USER_UserID=%SQLUPPER '" + userName + "'");
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(sb.ToString());
        }

        public static string getUserAlerts(String userId)
        {
            Dictionary<String, String> UserData = new Dictionary<String, String>();
            UserData.Add("User", userId);

            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.StoredProcedure("?=call SP2_GetUserAlerts(?)", UserData).Value.ToString();
        }

        public static string changeUserPassword(string userId, string oldPassword, string newPassword)
        {
            Dictionary<String, String> UserData = new Dictionary<String, String>();
            UserData.Add("USER", userId);
            UserData.Add("OLDPASSWORD", oldPassword);
            UserData.Add("NEWPASSWORD", newPassword);
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.StoredProcedure("?=call SP2_UpdateUserPassword(?,?,?)", UserData).Value.ToString();
        }
    }
}