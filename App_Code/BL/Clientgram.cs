using System;
using System.Data;
using System.Configuration;

public class Clientgram
{
    #region Clientgram Properties
    private Boolean _isValid;
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
    private String _clientgramID;
    public String ClientgramID
    {
        get { return _clientgramID; }
        //set { _clientgramID = value; }
    }
    private String _accountNumber;
    public String AccountNumber
    {
        get { return _accountNumber; }
        //set { _accountNumber = value; }
    }
    private String _accessionNumber;
    public String AccessionNumber
    {
        get { return _accessionNumber; }
        //set { _accessionNumber = value; }
    }
    private String _filedBy;
    public String FiledBy
    {
        get { return _filedBy; }
        //set { _filedBy = value; }
    }
    private String _filedByDispName;
    public String FiledByDispName
    {
        get { return _filedByDispName; }
        //set { _filedBy = value; }
    }


    private DateTime? _filedDateTime;
    public DateTime? FiledDateTime
    {
        get { return _filedDateTime; }
        //set { _filedDateTime = value; }
    }

    private String _batchInfo;
    public String BatchInfo
    {
        get { return _batchInfo; }
        //set { _batchInfo = value; }
    }
    private String _messageText;
    public String MessageText
    {
        get { return _messageText; }
        //set { _messageText = value; }
    }
    #endregion

    public Clientgram()
    {
        //
    }

    public Clientgram(String clientGramId)
    {
        this._clientgramID = clientGramId;
        DataTable clientGram = DL_Clientgram.getCientGramDetails(clientGramId);
        if (clientGram == null)
        {
            this._isValid = false;
        }
        else if (clientGram.Rows.Count < 1)
        {
            this._isValid = false;
        }
        else if (clientGram.Rows.Count > 1)
        {
            this._isValid = false;
        }
        else
        {
            this._isValid = true;

            DataRow dr = clientGram.Rows[0];
            this._clientgramID = dr["CLIENTGRAMID"].ToString();
            this._accountNumber = dr["CLIENTNUM"].ToString();
            this._accessionNumber = dr["ACCESSION"].ToString();
            this._filedBy = dr["AGENTID"].ToString();
            this._filedByDispName = dr["AGENTIDDISPNAME"].ToString();

            this._filedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dr["COLLECTIONDATE"].ToString(), dr["COLLECTIONTIME"].ToString());

            if (!String.IsNullOrEmpty(dr["AGENTID"].ToString()))
            {
                this._batchInfo = "Sent by " + dr["AGENTIDDISPNAME"].ToString();
            }

            // +AA Issue #59586 AntechCSM 1.0.82.0
            String tmpBatchDT;
            tmpBatchDT = AtlasIndia.AntechCSM.functions.AddTimeToDateString(dr["BatchDate"].ToString(), dr["BatchTime"].ToString());
            if (tmpBatchDT.Length > 0)
            {
                this._batchInfo += " on " + tmpBatchDT;
            }
            // -AA Issue #59586 AntechCSM 1.0.82.0       

            #region Commented Issue #59586 - Moved in AtlasIndia.AntechCSM.functions.AddTimeToDateString()
            // +Commented Issue #59586 AntechCSM 1.0.82.0
            //String tmpBatchDT;
            //DateTime tmpDateTime;
            //if (DateTime.TryParse(dr["BatchDate"].ToString(), out tmpDateTime) == false)
            //{
            //    tmpBatchDT = dr["BatchDate"].ToString();
            //}
            //else
            //{
            //    tmpBatchDT = tmpDateTime.ToString(AtlasIndia.AntechCSM.functions.getDateFormat());
            //}

            //if (DateTime.TryParse(dr["BatchTime"].ToString(), out tmpDateTime) == false)
            //{
            //    tmpBatchDT = tmpBatchDT + " " + dr["BatchTime"].ToString();
            //}
            //else
            //{
            //    tmpBatchDT = tmpBatchDT + " " + tmpDateTime.ToString(AtlasIndia.AntechCSM.functions.getTimeFormat());
            //}
            //if (tmpBatchDT.Trim().Length > 0) this._batchInfo += " on " + tmpBatchDT;
            // -Commented Issue #59586 AntechCSM 1.0.82.0
            #endregion Commented Issue #59586 - AtlasIndia.AntechCSM.functions.AddTimeToDateString()

            this._messageText = dr["MESSAGEDETAILS"].ToString();
        }
    }
    public static String insertClientGram(string strACCOUNT, string strPID, string strACCESSIONSTRING, string strMESSAGE, string strTrakVal, string strProbCategory, string strLocation, string strComments, string strResolution, string strFiledBy, string strFiledByDateTime, string strBatchInfo, string strMsgType, string strAutodial, string strInquiryNote )
    {
        return DL_Clientgram.insertClientGram(strACCOUNT, strPID, strACCESSIONSTRING, strMESSAGE, strTrakVal, strProbCategory, strLocation, strComments, strResolution, strFiledBy, strFiledByDateTime, strBatchInfo, strMsgType, strAutodial, strInquiryNote);
    }
    // AM IT#59635 AntechCSM 1.0.82.0 
    public static void ResendClientgram(String strAutodial, String strAccount, String strCGID, String strUser, String strLab)
    {
        DL_Clientgram.ResendClientgram(strAutodial, strAccount, strCGID, strUser, strLab);
    }
}