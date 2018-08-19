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
/// Summary description for Mails
/// </summary>
public class Mails
{
	public Mails()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#32895 04/17/2008 Build Number 0.0.0.9
    public static DataTable getMails(string userID, string displayOption, int noofRecords, int index)
    {
        DataTable returnData = new DataTable();
        returnData = DL_Mails.getMails(userID, displayOption, noofRecords, index);
        returnData.DefaultView.Sort = "DATERECEIVED DESC, TIMERECEIVED DESC";
        returnData = returnData.DefaultView.ToTable();
        return returnData;
    }

    public static string insertMails(string toUsers,string toGropus, string subject, string message, string ack, string ackMessageID,string fromSystem,string oldMessage,string isPriority)
    {
        return DL_Mails.insertMails(toUsers, toGropus, subject, message, ack, ackMessageID, SessionHelper.UserContext.ID, fromSystem, oldMessage, isPriority);
    }

    public static void SendAcknowledgement(string mailRowId, string ackMessage, string mailToUser, string mailFromUser)
    {
        string strMailSystem = ConfigurationSettings.AppSettings["MAILSYSTEM"];
        string strMailTo = mailToUser;

        Mails.insertMails(strMailTo, "", "Acknowledgement for message ID: " + ackMessage, "", ackMessage, mailFromUser, strMailSystem,"","N");
        DL_Mails.UpdateAcknowledgmentFlag(mailRowId);
    }

    //AM Issue#32899 04/26/2008 Build Number 0.0.0.9
    public static void deleteMail(string UserID,string MailID)
    {
        DL_Mails.deleteMail(UserID,MailID);
    }

    public static void SetMailRead(string userRowId, string messageId,bool read)
    {
        if (read)
        {
            DL_Mails.SetMailRead(userRowId, messageId);
        }
        else
        {
            DL_Mails.ResetMailRead(userRowId, messageId);
        }
    }
    //AM Issue#32899 04/18/2008 Build Number 0.0.0.9
    public Mails(string MailID)
    {
        this._ID = MailID;
        DL_Mails mailDetails = new DL_Mails();
        DataTable mailData = mailDetails.getMailDetails(MailID);
        if (mailData == null)
        {
            this.IsValid = false;
        }
        else if (mailData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else if (mailData.Rows.Count > 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;
            DataRow dr = mailData.Rows[0];
            this._from = dr["FROMUSERID"].ToString();
            this._to = dr["USERID"].ToString();
            this._subject = dr["SUBJECT"].ToString();
            this._date = dr["DATERECEIVED"].ToString();
            this._time = dr["TIMERECEIVED"].ToString();
            this._message = dr["MESSAGETEXT"].ToString();
            this._isACK = (dr["ISACK"].ToString().ToUpper().Equals("1") ? true : false);
            this._ackMessage = dr["ACKMESSAGE"].ToString();
            this._mailRowID = dr["MAILROWID"].ToString();
            this._fromUserName = dr["FromUserName"].ToString();
            this._toUserString = dr["ToUserString"].ToString();
            this._toGroupString = dr["ToGroupString"].ToString();
        }
    }
    private Boolean _isValid;
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
    #region Mail Details Properties
    private string _ID;
    public string ID
    {
        get { return _ID; }
        //set { _ID = value; }
    }
    private string _from;
    public string From
    {
        get { return _from; }
        //set { _from = value; }
    }
    private string _to;
    public string To
    {
        get { return _to; }
        //set { _to = value; }
    }
    private string _subject;
    public string Subject
    {
        get { return _subject; }
        //set { _subject = value; }
    }
    private string _message;
    public string Message
    {
        get
        {
            if (_message.IndexOf("\r\n\r\n") > -1)
            {
                string[] stringseparator = new string[] {"\r\n\r\n"};
                String[] arrMessage = _message.Split(stringseparator,StringSplitOptions.None);
                for (int i = 0; i < arrMessage.Length; i++)
                {
                    arrMessage[i] = arrMessage[i].Replace("\n\r", "\r\n");
                }
                _message = string.Join("\r\n\r\n", arrMessage);
                return _message.Replace("^", ",");
            }
            else
            {
                return _message.Replace("\n\r", "\r\n").Replace("^", ",");
            }
        }
        //set { _message = value; }
    }
    private string _date;
    public string Date
    {
        get { return _date; }
        //set { _date = value; }
    }
    private string _time;
    public string Time
    {
        get { return _time; }
        //set { _time = value; }
    }
    private bool _isACK;
    public bool IsACK
    {
        get { return _isACK; }
        //set { _isACK = value; }
    }
    private string _ackMessage;
    public string AckMessage
    {
        get { return _ackMessage; }
        //set { _ackMessage = value; }
    }
    private string _mailRowID;
    public string MailRowId
    {
        get { return _mailRowID; }
        //set { _mailRowID = value; }
    }
    private string _fromUserName;
    public string FromUserName
    {
        get { return _fromUserName; }
        //set { _fromUserName = value; }
    }
    private string _toUserString;
    public string ToUserString
    {
        get
        {
            if (_toUserString.Length > 0)
            {
                if (_toUserString.IndexOf(":") > -1)
                {
                    string[] arrToUsers = _toUserString.Split(new char[] { ':' });
                    if (arrToUsers.Length > 1)
                    {
                        _toUserString = arrToUsers[1];
                    }
                }
                _toUserString = _toUserString.ToString().Replace("^", ",");
            }
            else
            {
                _toUserString = SessionHelper.UserContext.ID;// +" \"" + SessionHelper.UserContext.Name + "\"";
            }
            return _toUserString;
        }
    }
    private string _toGroupString;
    public string ToGroupString
    {
        get
        {
            if (_toGroupString.Length > 0)
            {
                if (_toGroupString.IndexOf(":") > -1)
                {
                    string[] arrToGroups = _toGroupString.Split(new char[] { ':' });
                    if (arrToGroups.Length > 1)
                    {
                        _toGroupString = arrToGroups[1];
                    }
                }
                _toGroupString = _toGroupString.ToString().Replace("^", ",");
            }
            return _toGroupString;
        }
    }
    #endregion

    public void getNextMail(string rowID)
    {
        if (rowID.Length == 0)
            return;

        string [] sep = {"||"};
        string[] arrID = rowID.Split(sep,StringSplitOptions.RemoveEmptyEntries);

        DataTable mailData = DL_Mails.getNextMail(arrID[0], arrID[1]);

        if (mailData == null || mailData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;
            DataRow dr = mailData.Rows[0];
            this._from = mailData.Rows[0]["FROMUSERID"].ToString();
            this._to = mailData.Rows[0]["USERID"].ToString();
            this._subject = mailData.Rows[0]["SUBJECT"].ToString();
            this._date = mailData.Rows[0]["DATERECEIVED"].ToString();
            this._message = mailData.Rows[0]["MESSAGETEXT"].ToString();
            this._ackMessage = mailData.Rows[0]["ACKMESSAGE"].ToString();
            this._isACK = (dr["ISACK"].ToString().ToUpper().Equals("1") ? true : false);
            this._mailRowID = mailData.Rows[0]["MAILROWID"].ToString();
            this._fromUserName = dr["FromUserName"].ToString();
            this._toUserString = dr["ToUserString"].ToString();
            this._toGroupString = dr["ToGroupString"].ToString();
        }
    }

    public void getPreviousMail(string rowID)
    {
        if (rowID.Length == 0)
            return;

        string[] sep = { "||" };
        string[] arrID = rowID.Split(sep, StringSplitOptions.RemoveEmptyEntries);

        DataTable mailData = DL_Mails.getPreviousMail(arrID[0], arrID[1]);

        if (mailData == null || mailData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;
            DataRow dr = mailData.Rows[0];
            this._from = mailData.Rows[0]["FROMUSERID"].ToString();
            this._to = mailData.Rows[0]["USERID"].ToString();
            this._subject = mailData.Rows[0]["SUBJECT"].ToString();
            this._date = mailData.Rows[0]["DATERECEIVED"].ToString();
            this._message = mailData.Rows[0]["MESSAGETEXT"].ToString();
            this._isACK = (mailData.Rows[0]["ISACK"].ToString().ToUpper().Equals("1") ? true : false);
            this._ackMessage = mailData.Rows[0]["ACKMESSAGE"].ToString();
            this._mailRowID = mailData.Rows[0]["MAILROWID"].ToString();
            this._fromUserName = dr["FromUserName"].ToString();
            this._toUserString = dr["ToUserString"].ToString();
            this._toGroupString = dr["ToGroupString"].ToString();
        }
    }

    public DataTable MailDetails(string[] MailIDs)
    {
        DL_Mails mailDetails = new DL_Mails();
        DataTable mailData = mailDetails.getAllMailDetails(MailIDs);
        return mailData;
    }

    public static string getUserGrpNameUsingIDs(string calledFor, string toUserGrpString)
    {
        return DL_Mails.GetUserGrpNameUsingIDs(calledFor, toUserGrpString); 
    }
}
