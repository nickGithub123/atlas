using System;
using System.Data;
using System.Configuration;

public class Message
{
    public Message()
    {
        //
    }
    //AM Issue#32801 04/09/2008 0.0.0.9 -- AM Issue#32801 09/17/2008 AntechCSM 1.0.13.0 (Search with Message Keyword list)
    public static DataTable getMessageDetails(string searchText, string searchOption)
    {
        DataTable dtMessage = new DataTable();
        dtMessage = DL_Message.getMessageDetails(searchText, searchOption);
        return dtMessage;
    }

    public Message(String messageCode)
    {
        this._messageCode = messageCode;
        DataTable dtMessage = DL_Message.getMessageDetailsByCode(this.MessageCode);
        if (dtMessage != null && dtMessage.Rows.Count == 1)
        {
            this._isValid = true;

            DataRow dr = dtMessage.Rows[0];
            this.MessageDetails = dr["MSG_MessageText"].ToString();
            this.AutoComment = dr["MSG_AutoProblemComment"].ToString();
            this.AutoResolution = dr["MSG_AutoProblemResolution"].ToString();
            this.Category = dr["MSG_DefaultProblemCategoryDR"].ToString();
            this.AutoInquiryNote = dr["MSG_AutoInquiryNoteText"].ToString();
        }
        else
        {
            this._isValid = false;
        }
    }

    #region Message Properties

    #region Message Code

    public String _messageCode;
    public String MessageCode
    {
        get { return _messageCode; }
        //set { _messageCode = value; }
    }

    #endregion Message Code

    #region Message Details

    public String _messageDetails;
    public String MessageDetails
    {
        get { return _messageDetails; }
        set { _messageDetails = value; }
    }

    #endregion Message Details

    #region Auto Comment

    public String _autoComment;
    public String AutoComment
    {
        get { return _autoComment; }
        set { _autoComment = value; }
    }

    #endregion Auto Comment

    #region Auto Resolution

    public String _autoResolution;
    public String AutoResolution
    {
        get { return _autoResolution; }
        set { _autoResolution = value; }
    }

    #endregion Auto Resolution

    #region Category

    public String _category;
    public String Category
    {
        get { return _category; }
        set { _category = value; }
    }
    #endregion Category

    #region AutoInquaryNote

    public String _autoInquiryNote;
    public String AutoInquiryNote
    {
        get { return this._autoInquiryNote; }
        set { this._autoInquiryNote = value; }
    }
    #endregion AutoInquiryNote

    #endregion Message Properties

    #region Supporting Properties

    #region IsValid

    public Boolean _isValid;
    public Boolean IsValid
    {
        get { return _isValid; }
        //set { _isValid = value; }
    }

    #endregion IsValid

    #endregion Supporting Properties


}
