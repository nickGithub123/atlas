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

public class InterLabCommunication
{
    #region InterLab Communication Constructors

    public InterLabCommunication()
    {
        //
    }

    #region Unused Code
    //public InterLabCommunication(String ID)
    //{
    //    this._ID = ID;
    //    DataTable interLabDetails = DL_InterLabCommunication.getInterLabCommDetails(ID);
    //    if (interLabDetails == null)
    //    {
    //        this.IsValid = false;
    //    }
    //    else if (interLabDetails.Rows.Count < 1)
    //    {
    //        this.IsValid = false;
    //    }
    //    else if (interLabDetails.Rows.Count > 1)
    //    {
    //        this.IsValid = false;
    //    }
    //    else
    //    {
    //        this.IsValid = true;
    //        DataRow dr = interLabDetails.Rows[0];

    //        this.Accession = dr["Accession"].ToString();
    //        this.Client = dr["Client"].ToString();
    //        this.InitiatingUser = dr["InitiatingUser"].ToString();
    //        this.InitiatingLab = dr["InitiatingLab"].ToString();
    //        this.DateTimeEntered = AtlasIndia.AntechCSM.functions.AddTimeToDate(dr["DateEntered"].ToString(), dr["TimeEntered"].ToString());
    //        this.ResolutionStatus = dr["Status"].ToString();

    //        DataTable dtHistory = DL_InterLabCommunication.getInterLabHistory(ID);
    //        System.Text.StringBuilder sb = new System.Text.StringBuilder();
    //        for (Int32 i = 0; i < dtHistory.Rows.Count; i++)
    //        {
    //            sb.AppendLine("Message" + (i+1).ToString());
    //            sb.Append('-', 13);
    //            sb.AppendLine();
    //            sb.Append("Sent By: ");
    //            sb.Append(dtHistory.Rows[i]["SenderLab"].ToString());
    //            sb.Append("/");
    //            sb.Append(dtHistory.Rows[i]["SenderUser"].ToString());
    //            sb.Append("\t\t");
    //            sb.Append("Sent On: ");
    //            sb.AppendLine(AtlasIndia.AntechCSM.functions.AddTimeToDate(dtHistory.Rows[i]["SendDate"].ToString(), dtHistory.Rows[i]["SentTime"].ToString()).ToString(AtlasIndia.AntechCSM.functions.getDateTimeFormat()));
    //            sb.Append("Sent To: ");
    //            sb.Append(dtHistory.Rows[i]["RecipientLab"].ToString());
    //            sb.Append("/");
    //            sb.AppendLine(dtHistory.Rows[i]["RecipientUser"].ToString());
    //            sb.AppendLine(dtHistory.Rows[i]["Message"].ToString().Replace("\n\r",Environment.NewLine));
    //            sb.Append('-', 90);
    //            sb.AppendLine();
    //        }
    //        this.PreviousCommunication = sb.ToString();
    //        this.LastLab = dtHistory.Rows[dtHistory.Rows.Count - 1]["SenderLab"].ToString();
    //        this.LastDepartment = dtHistory.Rows[dtHistory.Rows.Count - 1]["SenderDepartment"].ToString();
    //    }
    //}
    #endregion Unused Code
    #endregion InterLab Communication Constructors

    #region InterLab Communication Supporting Methods

    public static String insertNewInterLabCommunication(String strAccession,String strInitUser,String strInitLab,String strMessage,String strToLab,String strTestString, String strMessageCode)
    {
        return DL_InterLabCommunication.insertNewInterLabCommunication(strAccession, strInitUser, strInitLab, strMessage, strToLab, strTestString, strMessageCode);
    }

    public static Boolean addResponseToInterLabCommunication()
    {
        //
        return false;
    }

    #endregion InterLab Communication Supporting Methods

    public DataTable getInterLabCommDetails(string AccessionNumber, string InitiatingUser, string MessageToLab, string CurrentStatus, string DateFrom, string DateTo, string MessageFromLab,string AccountNumber, string InnitiatingMessageCode)
    {
        return DL_InterLabCommunication.getInterLabCommDetails(AccessionNumber, InitiatingUser, MessageToLab, CurrentStatus, DateFrom, DateTo, MessageFromLab, AccountNumber, InnitiatingMessageCode);
    }

    #region InterLab Communication Properties

    #region ILC ID
    private String _ID;
    public String ID
    {
        get { return _ID; }
        // set { _ID = value; }
    }
    #endregion ILC ID

    #region Related Accession
    private String _accession;
    public String Accession
    {
        get { return _accession; }
        set { _accession = value; }
    }
    #endregion Related Accession

    #region Related Client
    private String _client;
    public String Client
    {
        get { return _client; }
        set { _client = value; }
    }
    #endregion Related Client

    #region Initiating User
    private String _initiatingUser;
    public String InitiatingUser
    {
        get { return _initiatingUser; }
        set { _initiatingUser = value; }
    }
    #endregion Initiating User

    #region Initiating Lab
    private String _initiatingLab;
    public String InitiatingLab
    {
        get { return _initiatingLab; }
        set { _initiatingLab = value; }
    }
    #endregion Initiating Lab

    #region Date & Time Entered
    private DateTime _dateTimeEntered;
    public DateTime DateTimeEntered
    {
        get { return _dateTimeEntered; }
        set { _dateTimeEntered = value; }
    }
    #endregion Date & Time Entered

    #region Resolution Status
    private String _resolutionStatus;
    public String ResolutionStatus
    {
        get { return _resolutionStatus; }
        set { _resolutionStatus = value; }
    }
    #endregion Resolution Status

    #region Previous Communication
    private String _previousCommunication;
    public String PreviousCommunication
    {
        get { return _previousCommunication; }
        set { _previousCommunication = value; }
    }
    #endregion Previous Communication

    #region Is Valid
    private Boolean _isValid;
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
    #endregion Is Valid

    #region Last Department
    private String _lastDepartment;
    public String LastDepartment
    {
        get { return _lastDepartment; }
        set { _lastDepartment = value; }
    }
    #endregion Last Department

    #region Last Lab
    private String _lastLab;
    public String LastLab
    {
        get { return _lastLab; }
        set { _lastLab = value; }
    }
    #endregion Last Lab

    #endregion InterLab Communication Properties
}
