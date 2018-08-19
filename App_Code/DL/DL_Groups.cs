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
/// Summary description for DL_Groups
/// </summary>
public class DL_Groups
{
	public DL_Groups()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#37633 04/29/2008 0.0.0.9
    public static DataTable getGroupByGroupName(string GroupName)
    {
        string selectStatement = "SELECT MGRP_GroupID GROUP_ID,MGRP_GroupName GROUP_NAME, MGRP_UserList USERS FROM DIC_MailGroup WHERE UPPER(MGRP_GroupName) %STARTSWITH '" + GroupName + "'";
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
    //AM Issue#37633 04/29/2008 0.0.0.9
    public static DataTable getGroupByGroupID(string GroupID)
    {
        string selectStatement = "SELECT MGRP_GroupID GROUP_ID,MGRP_GroupName GROUP_NAME,MGRP_UserList USERS FROM DIC_MailGroup WHERE UPPER(MGRP_GroupID) = '" + GroupID + "'";
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
    //AM Issue#37633 04/29/2008 0.0.0.9
    public static DataTable getUsersByGroupID(string GroupID)
    {
        string selectStatement = "SELECT MGUL_UserDR->USER_UserID USER_ID,MGUL_DestinationDR->MDEST_ID SYSTEM_ID FROM DIC_MailGroupUserList WHERE UPPER(MGUL_MGRP_ParRef->MGRP_GroupID) = '" + GroupID + "' AND (MGUL_UserDR->USER_UserID <> '' AND MGUL_DestinationDR->MDEST_ID <> '')";
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
