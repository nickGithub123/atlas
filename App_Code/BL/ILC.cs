using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// InterLab Communication - specific for edit screen, need to be generalized.
/// </summary>
public class ILC
{

    #region Constructors

    public ILC()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public ILC(String id, String accessionNumber, String user)
    {
        DataTable ILCMessages = DL_ILC.getILCMessages(id, user);
        if (ILCMessages == null)
        {
            this.IsValid = false;
        }
        else if (ILCMessages.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else
        {
            _isValid = true;
            _rowId = id;
            _accession = new Accession(accessionNumber);
            _tubeLocation = AccessionExtended.getSpecimenLocation(accessionNumber);
            _previousCommunication = ILCMessages;
            _testsString = DL_ILC.getILCTestsString(id);
            _defaultToLab = DL_ILC.getDefaultILCToLab(id);
        }
    }

    #endregion Constructors

    public static String addILCMessage(String rowId, String fromLab, String fromUser, String toLab, String message, String status, String tdTests, String tdLab, String tdDept, String tdReason, String mrMessageCode)
    {
        return DL_ILC.addILCMessage(rowId, fromLab, fromUser, toLab, message, status, tdTests, tdLab, tdDept, tdReason, mrMessageCode);
    }

    public static String deleteILC(String rowId, String user, String accessionNumber)
    {
        return DL_ILC.deleteILC(rowId, user, accessionNumber);
    }

    public static String getILCMessageDefaultStatus(string MessageCode)
    {
        return DL_ILC.getILCMessageDefaultStatus(MessageCode);
    }

    #region InterLab Communication Properties

    #region ILC Row ID
    protected String _rowId;
    public String RowId
    {
        get { return _rowId; }
        set { _rowId = value; }
    }
    #endregion ILC Row ID

    #region Accession
    protected Accession _accession;
    public Accession Accession
    {
        get { return _accession; }
        set { _accession = value; }
    }
    #endregion Accession

    #region Tube Location
    protected String _tubeLocation;
    public String TubeLocation
    {
        get { return _tubeLocation; }
        set { _tubeLocation = value; }
    }
    #endregion Tube Location

    #region Previous Communication
    protected DataTable _previousCommunication;
    public DataTable PreviousCommunication
    {
        get { return _previousCommunication; }
        set { _previousCommunication = value; }
    }
    #endregion Previous Communication

    #region Tests String
    protected String _testsString;
    public String TestsString
    {
        get { return _testsString; }
        set { _testsString = value; }
    }
    #endregion Tests String

    #region Default To Lab
    protected String _defaultToLab;
    public String DefaultToLab
    {
        get { return _defaultToLab; }
        set { _defaultToLab = value; }
    }
    #endregion Tests String

    #endregion InterLab Communication Properties

    #region Not Used
    //#region Initiating User
    //protected String _initiatingUser;
    //public String InitiatingUser
    //{
    //    get { return _initiatingUser; }
    //    set { _initiatingUser = value; }
    //}
    //#endregion Initiating User

    //#region Initiating Lab
    //protected String _initiatingLab;
    //public String InitiatingLab
    //{
    //    get { return _initiatingLab; }
    //    set { _initiatingLab = value; }
    //}
    //#endregion Initiating Lab

    //#region Sent DateTime
    //protected String _sentDateTime;
    //public String SentDateTime
    //{
    //    get { return _sentDateTime; }
    //    set { _sentDateTime = value; }
    //}
    //#endregion Sent DateTime

    //#region Sent To Lab
    //protected String _sentToLab;
    //public String SentToLab
    //{
    //    get { return _sentToLab; }
    //    set { _sentToLab = value; }
    //}
    //#endregion Sent To Lab

    //#region Message
    //protected String _message;
    //public String Message
    //{
    //    get { return _message; }
    //    set { _message = value; }
    //}
    //#endregion Message 
    #endregion Not Used

    #region Supporting Properties

    #region Is ILC Valid
    private Boolean _isValid = false;
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
    #endregion Is ILC Valid

    #endregion Supporting Properties
}
