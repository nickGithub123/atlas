using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for MissingBeforeAccn
/// </summary>
public class MissingBeforeAccession
{
    #region Properties
    public string RowId
    {
        get;
        set;
    }
    public string ClientMnemonic
    {
        get;
        set;
    }
    public string ContactPerson
    {
        get;
        set;
    }
    public string AlternateContactNo
    {
        get;
        set;
    }

    public string DateSubmitted
    {
        get;
        set;
    }
    public string EnteredByUser
    {
        get;
        set;
    }
    public string EnteredByUserDispName
    {
        get;
        set;
    }
    public string DateTimeEntered
    {
        get;
        set;
    }

    public string MissingFromLab
    {
        get;
        set;
    }
    public string MissingFromLabID
    {
        get;
        set;
    }
    public string SamplesOnRouteSheet
    {
        get;
        set;
    }
    public string SamplesOnSystem
    {
        get;
        set;
    }
    public string ReqLabCAttn
    {
        get;
        set;
    }

    public string RouteStop
    {
        get;
        set;
    }

    public string OutcomeStatus
    {
        get;
        set;
    }
    public string TRFStatusId
    {
        get;
        set;
    }
    public string ProgressingStatus
    {
        get;
        set;
    }
    public string CheckOutBy
    {
        get;
        set;
    }

    /// <summary>
    /// If status is "In Progress", that means some user has checked it out and CheckedOutBy = "currently logged in user"
    /// Then the record will be editable, otherwise it will be read-only.
    /// </summary>
    public bool IsEditable
    {
        get
        {
            if (this.ProgressingStatus.Equals("PRN") && this.CheckOutBy == SessionHelper.UserContext.ID)
            {
                return true;
            }
            return false;
        }
    }
    #endregion Properties
    public MissingBeforeAccession()
    {
    }

    public static string InsertPurple(string accountNo, string contactPerson, string altContactNo, string dateSubmitted, string comments, string testDetails, string lab, string trfStatus, string noOfSamplesOnRoute, string noOfSamplesOnSystem)
    {
        return DL_MissingSpecimen.InsertPurple(accountNo, contactPerson, altContactNo, dateSubmitted, comments, testDetails, SessionHelper.UserContext.ID, lab, trfStatus, noOfSamplesOnRoute, noOfSamplesOnSystem);
    }

    public static DataTable getMissingBeforeAccnBySearchOptions(string clientID, string user, string dateFrom, string dateTo, string lab, string progressStatus, string reqLabCAttn, string checkedBy, string finalizedBy,string labCResolution)
    {
        return DL_MissingSpecimen.getMissingBeforeAccnBySearchOptions("B", clientID, user, dateFrom, dateTo, lab, progressStatus, reqLabCAttn, "", checkedBy, labCResolution, "", "", finalizedBy);
    }

    public static DataTable getMissingSpecimenForReport(string quarryString)
    {
        String[] QS = quarryString.Split('^');
        return MissingBeforeAccession.getMissingSpecimenForReport(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8]);
    }

    public static DataTable getMissingSpecimenForReport(string clientID, string user, string dateFrom, string dateTo, string progressStatus, string lab, string reqLabCAttn,string checkedBy,string labCResolution)
    {
        return DL_MissingSpecimen.getMissingBeforeAccnForReport(clientID, user, dateFrom, dateTo, progressStatus, lab, reqLabCAttn, checkedBy, labCResolution);
    }

    public void getMissingBeforeAccnDetails(string rowId)
    {
        DataTable dtblMBADetails = DL_MissingSpecimen.getMissingBeforeAccnDetails(rowId);

        this.RowId = dtblMBADetails.Rows[0]["RowId"].ToString();
        this.ContactPerson = dtblMBADetails.Rows[0]["ContactPerson"].ToString();
        this.AlternateContactNo = dtblMBADetails.Rows[0]["AltContactNo"].ToString();
        this.DateSubmitted = dtblMBADetails.Rows[0]["DateSubmitted"].ToString();
        this.DateTimeEntered = AtlasIndia.AntechCSM.UI.UIfunctions.combineDateTime(dtblMBADetails.Rows[0]["DateEntered"].ToString(), dtblMBADetails.Rows[0]["TimeEntered"].ToString());
        this.EnteredByUser = dtblMBADetails.Rows[0]["EnteredByUser"].ToString();
        this.OutcomeStatus = dtblMBADetails.Rows[0]["OutcomeStatus"].ToString();
        this.ProgressingStatus = dtblMBADetails.Rows[0]["ProcessingStatus"].ToString();
        this.RouteStop = dtblMBADetails.Rows[0]["RouteStop"].ToString();
        this.TRFStatusId = dtblMBADetails.Rows[0]["TRFStatusId"].ToString();
        this.SamplesOnRouteSheet = dtblMBADetails.Rows[0]["SamplesOnRouteSheet"].ToString();
        this.SamplesOnSystem = dtblMBADetails.Rows[0]["SamplesOnSystem"].ToString();
        this.ReqLabCAttn = dtblMBADetails.Rows[0]["ReqLabCAttn"].ToString();
        this.ClientMnemonic = dtblMBADetails.Rows[0]["AccountMnemonic"].ToString();
        this.CheckOutBy = dtblMBADetails.Rows[0]["CheckOutBy"].ToString();
        this.MissingFromLab = dtblMBADetails.Rows[0]["MissingFromLab"].ToString();
        this.MissingFromLabID = dtblMBADetails.Rows[0]["MissingFromLabId"].ToString();
        this.EnteredByUserDispName = dtblMBADetails.Rows[0]["EnteredByUserDispName"].ToString();
    }

    public static DataTable GetProgressNotes(string rowId)
    {
        return DL_MissingSpecimen.GetProgressNotes(rowId);
    }

    public static DataTable GetTestDetails(string rowId)
    {
        return DL_MissingSpecimen.GetTestDetails(rowId);
    }

    public static bool CheckOut(string rowId, string accessionNo)
    {
        DL_MissingSpecimen.CheckOut(rowId, accessionNo, SessionHelper.UserContext.ID);
        return true;
    }

    public static bool UnCheckOut(string rowId, string accessionNo)
    {
        DL_MissingSpecimen.UnCheckOut(rowId, accessionNo, SessionHelper.UserContext.ID);
        return true;
    }

    public static string UpdatePurple(string rowID, string contactPerson, string altContactNo, string user, string noOfSamplesOnRoute, string noOfSamplesOnSystem, bool isAttnRequire, string routeStop, string dateSubmitted,string lab)
    {
        string attnRequireFlag = (isAttnRequire == true ? "Y" : "N");
        return DL_MissingSpecimen.UpdatePurple(rowID, contactPerson, altContactNo, user, noOfSamplesOnRoute, noOfSamplesOnSystem, attnRequireFlag, routeStop, dateSubmitted, lab);
    }

    public static string ResolveIssue(string rowID, string issueStatus, string user, string resolutionNote, string locatedAt, string personContected, string onDate, string problemCategory, string accessionNumber,string typeOfTest)
    {
        return DL_MissingSpecimen.ResolveIssue(rowID, issueStatus, user, resolutionNote, locatedAt, personContected, onDate, problemCategory, accessionNumber, typeOfTest);
    }
    public static string UpdateProgressNote(string rowID, string note, string user, string copyInqNote)
    {
        return DL_MissingSpecimen.UpdateProgressNote(rowID, note, user, copyInqNote);
    }
}
