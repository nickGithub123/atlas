using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

/// <summary>
/// Summary description for Groups
/// </summary>
public class Groups
{
    public enum SearchOption
    {
        GROUP_ID = 0,
        GROUP_NAME = 1
    }
    public Groups()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#37633 04/29/2008 0.0.0.9
    public static DataTable getGroups(string searchValue, SearchOption searchKey)
    {
        DataTable returnDataTable = new DataTable();
        switch (searchKey)
        {
            case SearchOption.GROUP_ID:
                returnDataTable = DL_Groups.getGroupByGroupID(searchValue);
                break;
            case SearchOption.GROUP_NAME:
                returnDataTable = DL_Groups.getGroupByGroupName(searchValue);
                break;
        }
        return returnDataTable;
    }
    //AM Issue#37633 04/29/2008 0.0.0.9
    public static string getUsersByGroupID(string GroupID)
    {
        string strSYSUser = "";
        DataTable dtUsers =  DL_Groups.getUsersByGroupID(GroupID);
        if (dtUsers.Rows.Count > 0)
        {
            foreach (DataRow dr in dtUsers.Rows)
            {
                strSYSUser = (strSYSUser != "" ? strSYSUser + ";" + dr.Field<string>(1) + ":" + dr.Field<string>(0) : dr.Field<string>(1) + ":" + dr.Field<string>(0));
            }
        }
        return strSYSUser;
    }
}
