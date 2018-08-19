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

/// <summary>
/// Summary description for DL_Users
/// </summary>
public class DL_Users
{
	public DL_Users()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#37267 04/17/2008 0.0.0.9
    public static DataTable getUsersByUserName(string userName)
    {
        string selectStatement = "SELECT MDUL_UserDR->USER_UserID USER_ID,MDUL_UserDR->USER_LastFirstName USER_NAME,MDUL_UserDR->USER_LabLocationDR->LABLO_LabName USER_LABLOCATION, MDUL_MDEST_ParRef->MDEST_ID USER_SYSTEM_ID, MDUL_MDEST_ParRef->MDEST_Name USER_SYSTEM_NAME  FROM DIC_MailDestinationUserList WHERE UPPER(MDUL_UserDR->USER_LastFirstName) %STARTSWITH '" + userName + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataSet returnDS = cache.FillCacheDataSet(selectStatement);
        if (returnDS.Tables.Count > 0)
        {
            return returnDS.Tables[0];
        }
        else
        {
            return null;
        }
    }
    //AM Issue#37267 05/15/2008 0.0.0.9
    public static DataTable getUsersByUserFirstName(string userFirstName)
    {
        string selectStatement = "SELECT MDUL_UserDR->USER_UserID USER_ID,MDUL_UserDR->USER_LastFirstName USER_NAME,MDUL_UserDR->USER_LabLocationDR->LABLO_LabName USER_LABLOCATION, MDUL_MDEST_ParRef->MDEST_ID USER_SYSTEM_ID, MDUL_MDEST_ParRef->MDEST_Name USER_SYSTEM_NAME  FROM DIC_MailDestinationUserList WHERE UPPER(MDUL_UserDR->USER_LastFirstName) LIKE '%," + userFirstName + "%'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataSet returnDS = cache.FillCacheDataSet(selectStatement);
        if (returnDS.Tables.Count > 0)
        {
            return returnDS.Tables[0];
        }
        else
        {
            return null;
        }
    }
    //AM Issue#37267 04/17/2008 0.0.0.9
    public static DataTable getUsersByUserID(string userID)
    {
        string selectStatement = "SELECT MDUL_UserDR->USER_UserID USER_ID,MDUL_UserDR->USER_LastFirstName USER_NAME,MDUL_UserDR->USER_LabLocationDR->LABLO_LabName USER_LABLOCATION, MDUL_MDEST_ParRef->MDEST_ID USER_SYSTEM_ID, MDUL_MDEST_ParRef->MDEST_Name USER_SYSTEM_NAME  FROM DIC_MailDestinationUserList WHERE UPPER(MDUL_UserDR->USER_UserID) = '" + userID + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataSet returnDS = cache.FillCacheDataSet(selectStatement);
        if (returnDS.Tables.Count > 0)
        {
            return returnDS.Tables[0];
        }
        else
        {
            return null;
        }
    }
}
