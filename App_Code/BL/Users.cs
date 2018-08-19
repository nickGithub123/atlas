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
/// Summary description for Users
/// </summary>
public class Users
{
    public enum SearchOption
    {
        USER_ID = 0,
        USER_NAME = 1,
        USER_FIRSTNAME =2
    }
    public Users()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#37267 04/17/2008 0.0.0.9
    public static DataTable getUsers(string searchValue, SearchOption searchKey)
    {
        DataTable returnDataTable = new DataTable();
        switch (searchKey)
        {
            case SearchOption.USER_ID:
                returnDataTable = DL_Users.getUsersByUserID(searchValue);
                break;
            case SearchOption.USER_NAME:
                returnDataTable = DL_Users.getUsersByUserName(searchValue);
                break;
            case SearchOption.USER_FIRSTNAME:
                returnDataTable = DL_Users.getUsersByUserFirstName(searchValue);
                break;
        }
        return returnDataTable;
    }
}
