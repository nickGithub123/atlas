using System;
using System.Data;
using System.Configuration;

public class ProblemManagement
{
    public ProblemManagement()
    {
        //	
    }

    public ProblemManagement(String problemID)
    {
        this._ID = problemID;
        DataTable problemData = DL_ProblemManagement.ProblemDetails(problemID);
        if (problemData == null)
        {
            this.IsValid = false;
        }
        else if (problemData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else if (problemData.Rows.Count > 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;
            DataRow dr = problemData.Rows[0];
            this._accessionNumber = dr["ACCESSION"].ToString();
            this._comments = dr["COMMENTS"].ToString();
            this._resolution = dr["RESOLUTION"].ToString();
            this._enteredBy = dr["ENTEREDBY"].ToString();
            this._enteredDate = dr["ENTEREDDATE"].ToString();
            this._enteredTime = dr["ENTEREDTIME"].ToString();
            this._resolvedBy = dr["RESOLVEDBY"].ToString();
            this._resolvedDate = dr["RESOLUTIONDATE"].ToString();
            this._resolvedTime = dr["RESOLUTIONTIME"].ToString();
            this._problemType = dr["PROBLEMTYPE"].ToString();
            this._labLocation = dr["LABLOCATION"].ToString();
            this._enteredByDispName =dr["ENTEREDBYDISPNAME"].ToString();
            this._resolvedByDispName =dr["RESOLVEDBYDISPNAME"].ToString();
        }
    }

    #region Problem Properties

    #region Is Valid

    private Boolean _isValid;
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }

    #endregion Is Valid

    #region ID

    private string _ID;
    public string ID
    {
        get { return _ID; }
        //set { _ID = value; }
    }

    #endregion ID

    #region Accession Number

    private string _accessionNumber;
    public string AccessionNumber
    {
        get { return _accessionNumber; }
        //set { _accessionNumber = value; }
    }

    #endregion Accession Number

    #region Comments

    private string _comments;
    public string Comments
    {
        get { return _comments; }
        //set { Comments = value; }
    }

    #endregion Comments

    #region Lab Location

    private string _labLocation;
    public string LabLocation
    {
        get { return _labLocation; }
        //set { _labLocation = value; }
    }

    #endregion  Lab Location

    #region Problem Type

    private string _problemType;
    public string ProblemType
    {
        get { return _problemType; }
        //set { _problemType = value; }
    }

    #endregion Problem Type

    #region Resolution

    private string _resolution;
    public string Resolution
    {
        get { return _resolution; }
        //set { _resolution = value; }
    }

    #endregion Resolution

    #region Entered By

    private string _enteredBy;
    public string EnteredBy
    {
        get { return _enteredBy; }
        //set { _enteredBy = value; }
    }

    #endregion Entered By

    #region Entered By Display Name

    private string _enteredByDispName;
    public string EnteredByDispName
    {
        get { return _enteredByDispName; }
        //set { _enteredBy = value; }
    }

    #endregion Entered By Display Name

    #region Resolved By

    private string _resolvedBy;
    public string ResolvedBy
    {
        get { return _resolvedBy; }
        //set { _resolvedBy = value; }
    }

    #endregion Resolved By

    #region Resolved By Display Name

    private string _resolvedByDispName;
    public string ResolvedByDispName
    {
        get { return _resolvedByDispName; }
        //set { _resolvedBy = value; }
    }

    #endregion Resolved By

    #region Entered Date

    private string _enteredDate;
    public string EnteredDate
    {
        get { return _enteredDate; }
        //set { _enteredDate = value; }
    }

    #endregion Entered Date

    #region Entered Time

    private string _enteredTime;
    public string EnteredTime
    {
        get { return _enteredTime; }
        //set { _enteredTime = value; }
    }

    #endregion Entered Time

    #region Resolved Date

    private string _resolvedDate;
    public string ResolvedDate
    {
        get { return _resolvedDate; }
        //set { _resolvedDate = value; }
    }

    #endregion Resolved Date

    #region Resolved Time

    private string _resolvedTime;
    public string ResolvedTime
    {
        get { return _resolvedTime; }
        //set { _resolvedTime = value; }
    }

    #endregion Resolved Time

    #endregion

    public static DataTable getProblemDetailsBySearchOptions(string AccountNumber, string AccessionNumber, string ProblemCategory, string ProblemStatus, string Location, string EnteredBy, string ResolvedBy, string ProblemNumber, string DateFrom, string DateTo, string SalesTerritory)
    {
        return DL_ProblemManagement.getProblemDetailsBySearchOptions(AccountNumber, AccessionNumber, ProblemCategory, ProblemStatus, Location, EnteredBy, ResolvedBy, ProblemNumber, DateFrom, DateTo, SalesTerritory);
    }

    public static string insertProblemDetails(string ClientID, string AccessionNumber, string ProblemCategory, string location, string commentText, string resolutionText, string enteredBy, string resolvedBy)
    {
        return DL_ProblemManagement.insertProblemDetails(ClientID, AccessionNumber, ProblemCategory, location, commentText, resolutionText, enteredBy, resolvedBy);
    }

    public static string updateProblemDetails(string ClientID, string ProblemID, string ResolvedBy, string CommentText, string ResolvedText, String Location, String problemCategory)
    {
        return DL_ProblemManagement.updateProblemDetails(ClientID, ProblemID, ResolvedBy, CommentText, ResolvedText, Location, problemCategory);
    }

    //+SSM 15/11/2011 AntechCSM 2a2 #Issue116544  Added BL method to call DL method
    public static DataTable getProblemSearchReport(String strQS)
    {
        return DL_ProblemManagement.getProblemSearchReport(strQS);
    }
    //-SSM
}
