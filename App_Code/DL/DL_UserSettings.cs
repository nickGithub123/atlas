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

/// <summary>
/// Summary description for DL_UserSettings
/// </summary>
public class DL_UserSettings
{
	public DL_UserSettings()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#38713 06/17/2008 
    public static DataTable getSystemMessages()
    {
        DataTable returnData = new DataTable();
        string selectStatement = "SELECT SM_RowID MessageID, SM_MessageText MessageText, SM_DisplayOnLogin IsDisplayOnLogin FROM DIC_SystemMessage ORDER BY MessageID";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnData = cache.FillCacheDataTable(selectStatement);
        return returnData;
    }
    //AM Issue#38713 06/17/2008 
    public String getSystemMessage(string MessageID)
    {
        Dictionary<string, string> _getSystemMessage = new Dictionary<string, string>();
        _getSystemMessage.Add("MSGROW", MessageID);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetSystemMessage(?)", _getSystemMessage, 32000).Value.ToString();
    }
    //AM Issue#38713 06/17/2008 
    public static String updateSystemMessage(string MessageID, string DisplayOnLogin, string MessageText)
    {
        Dictionary<string, string> _updateSystemMessage = new Dictionary<string, string>();
        _updateSystemMessage.Add("MSGROW", MessageID);
        _updateSystemMessage.Add("MESSAGE", MessageText);
        _updateSystemMessage.Add("DISPLAYLOGIN", DisplayOnLogin);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_InsertUpdateSystemMessage(?,?,?)", _updateSystemMessage).Value.ToString();
    }
    //AM Issue#38713 06/17/2008 
    public static String insertSystemMessage(string MessageText, string IsDiaplayLogIn)
    {
        Dictionary<string, string> _insertSystemMessage = new Dictionary<string, string>();
        _insertSystemMessage.Add("MSGROW", "");
        _insertSystemMessage.Add("MESSAGE", MessageText);
        _insertSystemMessage.Add("DISPLAYLOGIN", IsDiaplayLogIn);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_InsertUpdateSystemMessage(?,?,?)", _insertSystemMessage).Value.ToString();
    }
    //AM Issue#38713 06/18/2008 
    public static void deleteSystemMessage(string MessageID)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("Delete From DIC_SystemMessage Where SM_RowID=");
        sb.Append("'" + MessageID + "'");
        string deleteStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        cache.Insert(deleteStatement);
    }
}
