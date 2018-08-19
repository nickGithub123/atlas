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
/// Summary description for UserSettings
/// </summary>
public class UserSettings
{
	public UserSettings()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#38713 06/17/2008
    public static DataTable getSystemMessages()
    {
        DataTable returnData = new DataTable();
        returnData = DL_UserSettings.getSystemMessages();
        return returnData;
    }
    //AM Issue#38713 06/17/2008 
    public String getSystemMessage(string MessageID)
    {
        this._ID = MessageID;
        DL_UserSettings currentMessageID = new DL_UserSettings();
        String strSystemMessageData = currentMessageID.getSystemMessage(MessageID);
        if (strSystemMessageData.Split('^').Length  < 2)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;
            this._messageText = strSystemMessageData.Split('^')[1];
            this._isDisplayOnLogin = strSystemMessageData.Split('^')[0];            
        }
        return strSystemMessageData;
    }
    //AM Issue#38713 06/17/2008 
    public static void insertSystemMessage(string MessageText, string IsDiaplayLogIn)
    {
        DL_UserSettings.insertSystemMessage(MessageText, IsDiaplayLogIn);
    }
    //AM Issue#38713 06/17/2008 
    public static void updateSystemMessage(string MessageID, string DisplayOnLogin, string MessageText)
    {
        DL_UserSettings.updateSystemMessage(MessageID, DisplayOnLogin, MessageText);
    }
    //AM Issue#38713 06/18/2008 
    public static void deleteSystemMessage(string MessageID)
    {
        DL_UserSettings.deleteSystemMessage(MessageID);
    }
    private Boolean _isValid;
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
    #region user settings Properties
    private string _ID;
    public string ID
    {
        get { return _ID; }
        //set { _ID = value; }
    }
    private string _messageText;
    public string MessageText
    {
        get { return _messageText; }
        //set { _messageText = value; }
    }
    private string _isDisplayOnLogin;
    public string IsDisplayOnLogin
    {
        get { return _isDisplayOnLogin; }
        //set { _isDisplayOnLogin = value; }
    }
    #endregion
}
