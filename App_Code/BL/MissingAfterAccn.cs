using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for MissingBeforeAccn
/// </summary>
public class MissingAfterAccession
{
    #region Properties
    public string RowId
    {
        get;
        set;
    }
    public string AccessionNumber
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
    public string MissingFromDept
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
    public DataTable TestDetails
    {
        get;
        set;
    }
    public string OutcomeStatusInterim
    {
        get;
        set;
    }
    public string OutcomeStatus
    {
        get;
        set;
    }

    public bool IsResolved
    {
        get
        {
            if (this.ProgressingStatus.Equals("PROC"))
            {
                return true;
            }
            return false;
        }
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
    public MissingAfterAccession()
    {
    }

    public static string InsertPink(string accessionNo, string comment, string testDetails, string lab, string department)
    {
        return DL_MissingSpecimen.InsertPink(accessionNo, comment, testDetails, SessionHelper.UserContext.ID, lab, department);
    }

    public static DataTable getMissingAfterAccnBySearchOptions(string clientID, string user, string dateFrom, string dateTo, string lab, string progressStatus, string reqLabCAttn, string accession, string checkedBy,string labCResolution,string department,string processStatus)
    {
        return DL_MissingSpecimen.getMissingBeforeAccnBySearchOptions("A", clientID, user, dateFrom, dateTo, lab, progressStatus, reqLabCAttn, accession, checkedBy,labCResolution,department,processStatus,"");
    }

    public static DataTable getMissingSpecimenForReport(string quarryString)
    {
        String[] QS = quarryString.Split('^');
        return MissingAfterAccession.getMissingSpecimenForReport(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7],QS[8],QS[9],QS[10]);
    }
    public static DataTable getMissingSpecimenForReport(string clientID, string user, string dateFrom, string dateTo, string progressStatus, string lab, string accessionNo, string checkedBy, string labCResolution, string department, string processStatus)
    {
        return DL_MissingSpecimen.getMissingAfterAccnForReport(clientID, user, dateFrom, dateTo, progressStatus, lab, accessionNo, checkedBy, labCResolution, department,processStatus);
    }
    public void getMissingAfterAccnDetails(string rowId)
    {
        DataTable dtblMBADetails = DL_MissingSpecimen.getMissingAfterAccnDetails(rowId);

        this.RowId = dtblMBADetails.Rows[0]["RowId"].ToString();
        this.DateTimeEntered = AtlasIndia.AntechCSM.UI.UIfunctions.combineDateTime(dtblMBADetails.Rows[0]["DateEntered"].ToString(), dtblMBADetails.Rows[0]["TimeEntered"].ToString());
        this.EnteredByUser = dtblMBADetails.Rows[0]["EnteredByUser"].ToString();
        this.MissingFromLab = dtblMBADetails.Rows[0]["MissingFromLab"].ToString();
        this.MissingFromDept = dtblMBADetails.Rows[0]["MissingFromDept"].ToString();

        this.OutcomeStatus = dtblMBADetails.Rows[0]["OutcomeStatus"].ToString();
        this.OutcomeStatusInterim = dtblMBADetails.Rows[0]["OutcomeStatusInterim"].ToString();

        this.AccessionNumber = dtblMBADetails.Rows[0]["AccessionNumber"].ToString();
        this.ProgressingStatus = dtblMBADetails.Rows[0]["ProcessingStatus"].ToString();
        this.CheckOutBy = dtblMBADetails.Rows[0]["CheckOutBy"].ToString();
        this.EnteredByUserDispName = dtblMBADetails.Rows[0]["EnteredByUserDispName"].ToString();

        string strTestDetails = dtblMBADetails.Rows[0]["TestDetails"].ToString();
        this.TestDetails = new DataTable();
        this.TestDetails.Columns.Add("TestDetails");

        string [] arrTests = strTestDetails.Split(new char[]{';'});

        foreach (string strEachTest in arrTests)
        {
            DataRow drowTest = this.TestDetails.NewRow();
            drowTest["TestDetails"] = strEachTest;
            this.TestDetails.Rows.Add(drowTest);
        }
    }

    public static DataTable GetProgressNotes(string rowId)
    {
        return DL_MissingSpecimen.GetProgressNotes(rowId);
    }

    public static DataTable GetTestDetails(string rowId)
    {
        return DL_MissingSpecimen.GetTestDetails(rowId);
    }

    public static DataTable GetProcessingResolutionDetails(string rowId)
    {
        return DL_MissingSpecimen.GetProcessingResolutionDetails(rowId);
    }

    public static string UpdateProcessingResolutionDetails(string rowId, string outcomeStatusInterim, string whereLocatedInterim, string resolutionNotesInterim)
    {
        return DL_MissingSpecimen.UpdateProcessingResolutionDetails(rowId, outcomeStatusInterim, whereLocatedInterim, resolutionNotesInterim, SessionHelper.UserContext.ID);
    }

    public static string UpdateMissingAfterAccession(string rowId, string lab, string reason, string user)
    {
        return DL_MissingSpecimen.UpdateMissingAfterAccession(rowId, lab, reason, user);
    }
}
