using System;
using System.Data;
using System.Configuration;
using System.Collections;
using System.Collections.Generic;

public class DL_Mails
{
	public DL_Mails()
	{
		//
	}
    
    //AM Issue#32895 04/17/2008 Build Number 0.0.0.9
    public static DataTable getMails(String userID,string displayOption,int noofRecords,int index)
    {
        String strSQLSelect = "SELECT USER_LastFirstName FROM_USER, MAIL_FromUser FROMUSERID,MAIL_DateReceived DATERECEIVED,MAIL_TimeReceived TIMERECEIVED,MAIL_Subject SUBJECT,MAIL_USER_ParRef USERID,MAIL_MessageID MESSAGEID, MAIL_MessageTextToDisplay MESSAGETEXT, MAIL_IsNewMessage MAILSTATUS, MAIL_RowID MAILROWID, MAIL_ToUserString ToUserString,MAIL_ToGroupString ToGroupString FROM DIC_UserMail left outer join DIC_User on MAIL_FromUser = DIC_User.USER_UserID WHERE DIC_UserMail.MAIL_USER_ParRef ='" + userID + "'";
        String strDisplayFilter = "";
        if (displayOption == "N")
        {
            strDisplayFilter = " AND DIC_UserMail.MAIL_IsNewMessage = '1'";
        }
        else if (displayOption == "O")
        {
            strDisplayFilter = " AND DIC_UserMail.MAIL_IsNewMessage = '0'";
        }
        else if (displayOption == "P")
        {
            strDisplayFilter = " AND DIC_UserMail.MAIL_IsPriority = 'Y'";
        }
        strSQLSelect = strSQLSelect + strDisplayFilter + " ORDER BY MAIL_MessageID DESC";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(strSQLSelect, index * noofRecords, noofRecords + 1);        
    }
    /// <summary>
    ///  Get the next mail.
    /// </summary>
    /// <param name="userID">User ID</param>
    /// <param name="mailMessageId">This is the bookmark field</param>
    /// <returns></returns>
    public static DataTable getNextMail(String userID, string mailMessageId)
    {
        String strSQLSelect = "SELECT TOP 1 MAIL_FromUser FROMUSERID,MAIL_FromUser->USER_LastFirstName FromUserName,%EXTERNAL(MAIL_DateReceived) DATERECEIVED,MAIL_Subject SUBJECT,MAIL_USER_ParRef USERID,MAIL_MessageID MESSAGEID, MAIL_MessageTextToDisplay MESSAGETEXT, MAIL_AcknowledgementRequested ISACK, MAIL_AcknowledgementID ACKMESSAGE, MAIL_RowID MAILROWID, MAIL_IsNewMessage IsNewMessage, MAIL_ToUserString ToUserString,MAIL_ToGroupString ToGroupString FROM DIC_UserMail left outer join DIC_User on MAIL_FromUser = DIC_User.USER_UserID WHERE DIC_UserMail.MAIL_USER_ParRef ='" + userID + "' AND MAIL_MessageID < " + mailMessageId + " ORDER BY MAIL_MessageID DESC";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataTable dtblEmail = cache.FillCacheDataTable(strSQLSelect);

        if (dtblEmail.Rows.Count > 0)
        {
            if (dtblEmail.Rows[0]["IsNewMessage"].ToString().ToUpper().Equals("1"))
            {
                SetMailRead(dtblEmail.Rows[0]["USERID"].ToString(), dtblEmail.Rows[0]["MESSAGEID"].ToString());
            }
        }

        return dtblEmail;
    }

    /// <summary>
    ///  Get the previous mail.
    /// </summary>
    /// <param name="userID">User ID</param>
    /// <param name="mailMessageId">This is the bookmark field</param>
    /// <returns></returns>
    public static DataTable getPreviousMail(String userID, string mailMessageId)
    {
        String strSQLSelect = "SELECT TOP 1 MAIL_FromUser FROMUSERID,MAIL_FromUser->USER_LastFirstName FromUserName,%EXTERNAL(MAIL_DateReceived) DATERECEIVED,MAIL_Subject SUBJECT,MAIL_USER_ParRef USERID,MAIL_MessageID MESSAGEID, MAIL_MessageTextToDisplay MESSAGETEXT, MAIL_AcknowledgementRequested ISACK, MAIL_AcknowledgementID ACKMESSAGE, MAIL_RowID MAILROWID, MAIL_IsNewMessage IsNewMessage, MAIL_ToUserString ToUserString,MAIL_ToGroupString ToGroupString FROM DIC_UserMail left outer join DIC_User on MAIL_FromUser = DIC_User.USER_UserID WHERE DIC_UserMail.MAIL_USER_ParRef ='" + userID + "' AND MAIL_MessageID > " + mailMessageId + " ORDER BY MAIL_MessageID";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataTable dtblEmail = cache.FillCacheDataTable(strSQLSelect);

        if (dtblEmail.Rows.Count > 0)
        {
            if (dtblEmail.Rows[0]["IsNewMessage"].ToString().ToUpper().Equals("1"))
            {
                SetMailRead(dtblEmail.Rows[0]["USERID"].ToString(), dtblEmail.Rows[0]["MESSAGEID"].ToString());
            }
        }

        return dtblEmail;
    }
    
    //AM Issue#32895 04/19/2008 Build Number 0.0.0.9
    public static string insertMails(String toUser,string toGroups, String subject, String message, String ack, String ackMessageID, String fromUser, String fromSystem,String oldMessage,String isPriority)
    {
        Dictionary<String, String> mailData = new Dictionary<String, String>();
        mailData.Add("ToUsersString", toUser);
        mailData.Add("ToGroupsString", toGroups);
        mailData.Add("Subject", subject);
        mailData.Add("Message", message);
        mailData.Add("Ack", ack);
        mailData.Add("AckMessageID", ackMessageID);
        mailData.Add("FromUser", fromUser);
        mailData.Add("FromSystem", fromSystem);
        mailData.Add("OldMessage", oldMessage);
        mailData.Add("IsPriority", isPriority);
        
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP_SaveMails(?,?,?,?,?,?,?,?,?,?)", mailData).Value.ToString();
    }
    
    //AM Issue#32895 04/18/2008 Build Number 0.0.0.9
    public DataTable getMailDetails(String MailID) // this is user for getting message details as well as updating new mail status.
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();

        String strSQLSelect = "SELECT MAIL_FromUser FROMUSERID,MAIL_FromUser->USER_LastFirstName FromUserName,%EXTERNAL(MAIL_DateReceived) DATERECEIVED,%EXTERNAL(MAIL_TimeReceived) TIMERECEIVED,MAIL_Subject SUBJECT,MAIL_USER_ParRef USERID, MAIL_IsNewMessage IsNewMessage, MAIL_MessageID MESSAGEID, MAIL_MessageTextToDisplay MESSAGETEXT, MAIL_AcknowledgementRequested ISACK, MAIL_AcknowledgementID ACKMESSAGE, MAIL_RowID MAILROWID, MAIL_ToUserString ToUserString,MAIL_ToGroupString ToGroupString  FROM DIC_UserMail WHERE DIC_UserMail.MAIL_RowID ='" + MailID + "'";
        DataTable returnData = cache.FillCacheDataTable(strSQLSelect);

        if (returnData.Rows[0]["IsNewMessage"].ToString().Equals("1"))
        {
            SetMailRead(returnData.Rows[0]["USERID"].ToString(), returnData.Rows[0]["MESSAGEID"].ToString());
        }

        return returnData;
    }
    
    //AM Issue#32899 04/26/2008 Build Number 0.0.0.9

    public DataTable getAllMailDetails(String[] MailIDs) // this is user for getting message details as well as updating new mail status.
    {
        string MailId = string.Empty;
        for (int iMailCount = 0; iMailCount < MailIDs.Length; iMailCount++)
        {
            if (iMailCount > 0)
            {
                MailId = MailId + "','" + MailIDs[iMailCount];
            }
            else
            {
                MailId = MailIDs[iMailCount];
            }
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string strSQLSelect = "SELECT MAIL_FromUser FROMUSERID,MAIL_FromUser->USER_LastFirstName FromUserName,%EXTERNAL(MAIL_DateReceived) DATERECEIVED,%EXTERNAL(MAIL_TimeReceived) TIMERECEIVED,MAIL_Subject SUBJECT,MAIL_USER_ParRef USERID, MAIL_IsNewMessage IsNewMessage, MAIL_MessageID MESSAGEID, MAIL_MessageTextToDisplay MESSAGETEXT, MAIL_AcknowledgementRequested ISACK, MAIL_AcknowledgementID ACKMESSAGE, MAIL_RowID MAILROWID, $$CONCATUSERNAMEWITHID^XT60(MAIL_ToUserString) ToUserString,MAIL_ToGroupString ToGroupString FROM DIC_UserMail WHERE DIC_UserMail.MAIL_RowID IN ('" + MailId + "')";
        DataTable returnData = cache.FillCacheDataTable(strSQLSelect);
        return returnData;
    }

    public static void deleteMail(String UserID, String MailID)
    {
        Dictionary<string, string> _mailData = new Dictionary<String, String>();
        _mailData.Add("USERID", UserID);
        _mailData.Add("MAILID", MailID);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        String dummyReturn = cache.StoredProcedure("?=call SP_DeleteMails(?,?)", _mailData).Value.ToString();
    }

    public static void SetMailRead(string userRowId, string messageId)
    {
        Dictionary<String, String> _mailData = new Dictionary<String, String>();
        _mailData.Add("USER", userRowId);
        _mailData.Add("MESSAGENUM", messageId);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        cache.StoredProcedure("?=call SP2_SETREADMAIL(?,?)", _mailData);   
    }
    
    public static void ResetMailRead(string userRowId, string messageId)
    {
        Dictionary<String, String> _mailData = new Dictionary<String, String>();
        _mailData.Add("USER", userRowId);
        _mailData.Add("MESSAGENUM", messageId);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        cache.StoredProcedure("?=call SP2_RESETREADMAIL(?,?)", _mailData);
    }
    public static void UpdateAcknowledgmentFlag(string mailRowId)
    {
        string strSQL = "UPDATE DIC_UserMail SET MAIL_AcknowledgementRequested ='0' WHERE MAIL_RowID ='" + mailRowId + "'";

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        cache.Transaction(strSQL);
    }

    public static string GetUserGrpNameUsingIDs(string calledFor, string toUserGrpString)
    {
        Dictionary<String, String> _mailData = new Dictionary<String, String>();
        _mailData.Add("CalledFor", calledFor);
        _mailData.Add("ToUserGrpString", toUserGrpString);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetUserGrpNameUsingIDs(?,?)", _mailData).Value.ToString();
    }
}
