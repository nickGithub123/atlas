using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Web;
using System.Data;
using Antech.Atlas.Objects;


/// <summary>
/// Summary description for ClientIssues
/// </summary>
public class ClientIssues
{
    /// <summary>
    /// Row Id of the Issue
    /// </summary>
    public string RowId
    {
        get;
        set;
    }

    /// <summary>
    /// Account number of the user
    /// </summary>
    public string AccountNumber
    {
        get;
        set;
    }

    /// <summary>
    /// Accession number of the issue generated
    /// </summary>
    public string AccessionNumber
    {
        get;
        set;
    }

    /// <summary>
    /// Priority of the issue
    /// </summary>
    public string PriorityIssue
    {
        get;
        set;
    }

    /// <summary>
    /// Notes when resolving the issue
    /// </summary>
    public string ResolutionNote
    {
        get;
        set;
    }

    /// <summary>
    /// Person contacted for the issue
    /// </summary>
    public string ContactPerson
    {
        get;
        set;
    }

    /// <summary>
    /// Alternate contact number
    /// </summary>
    public string AltContactNo
    {
        get;
        set;
    }

    /// <summary>
    /// Status of the issue
    /// </summary>
    public string Status
    {
        get;
        set;
    }

    /// <summary>
    /// Date time when the issue is entered
    /// </summary>
    public string DateTimeEntered
    {
        get;
        set;
    }

    /// <summary>
    /// User created the issue
    /// </summary>
    public string User
    {
        get;
        set;
    }

    /// <summary>
    /// Reason for the issue
    /// </summary>
    public string Reason
    {
        get;
        set;
    }

    /// <summary>
    /// ReasonID for the issue
    /// </summary>
    public string ReasonID
    {
        get;
        set;
    }

    /// <summary>
    /// Location of the lab
    /// </summary>
    public string LabLocation
    {
        get;
        set;
    }

    /// <summary>
    /// Id of the lab location property
    /// </summary>
    public string LabLocationId
    {
        get;
        set;
    }

    /// <summary>
    /// Group the issue is assigned to
    /// </summary>
    public string AssignToGroup
    {
        get;
        set;
    }

    /// <summary>
    /// Problem tracking category
    /// </summary>
    public string ProbTrckCat
    {
        get;
        set;
    }

    /// <summary>
    /// Comment for the issue
    /// </summary>
    public string Comments
    {
        get;
        set;
    }

    /// <summary>
    /// Progressing status of the issue
    /// </summary>
    public string ProgressingStatus
    {
        get;
        set;
    }

    /// <summary>
    /// User id of user checking out the issue
    /// </summary>
    public string CheckOutBy
    {
        get;
        set;
    }

    /// <summary>
    /// Date when the issue is submitted
    /// </summary>
    public string DateSubmitted
    {
        get;
        set;
    }

    /// <summary>
    /// Mnemonic of the client
    /// </summary>
    public string ClientMnemonic
    {
        get;
        set;
    }

    /// <summary>
    /// Entered by user
    /// </summary>
    public string EnteredByUser
    {
        get;
        set;
    }

    /// <summary>
    /// Person contacted
    /// </summary>
    public string PersonContact
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
            if (this.ProgressingStatus.Equals("PROG") && this.CheckOutBy == SessionHelper.UserContext.ID)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// If the status is "In Progress" the "Add Progress Note" button will be enabled
    /// </summary>
    public bool IsProgress
    {
        get
        {
            if (this.ProgressingStatus.Equals("PROG"))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// If status is "Final", that means the user checkedout has resolved the issue. And the issue is noneditable after resolved
    /// </summary>
    public bool IsResolved
    {
        get
        {
            if (this.ProgressingStatus.Equals("F"))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Used to check if re-issue functionality enabled.
    /// </summary>
    public bool IsReIssueEnabled
    {
        get
        {
            bool retValue = false;
            AtlasIndia.AntechCSM.CSUser userContext = SessionHelper.UserContext;
            if ((userContext.IsLabC || userContext.IsCSUser) && userContext.IsSupervisor && this.IsResolved)
            {
                retValue = true;
            }
            return retValue;
        }
    }

    /// <summary>
    /// Problem tracking lab
    /// </summary>
    public string ProbTrackLab
    {
        get;
        set;
    }

    /// <summary>
    /// The Row ID associated with the Problem Category
    /// </summary>
    public string ProblemCategoryNo { get; set; }

    /// <summary>
    /// Problem tracking lab id
    /// </summary>
    public string ProbTrackLabID
    {
        get;
        set;
    }

    /// <summary>
    /// Re-issued flag
    /// </summary>
    public string ReIssuedFlag
    {
        get;
        set;
    }

    /// <summary>
    /// Boolean flag for re-issue
    /// </summary>
    public bool IsReIssued
    {
        get
        {
            if (!string.IsNullOrEmpty(this.ReIssuedFlag) && this.ReIssuedFlag.StartsWith("Y", StringComparison.OrdinalIgnoreCase))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Previous row ID, to denote from where the issue is regenerated
    /// </summary>
    public string PreviousRowID
    {
        get;
        set;
    }

    /// <summary>
    /// Checks if the issue is generated from other issue
    /// </summary>
    public bool IsReIssuedFrom
    {
        get
        {
            if (!string.IsNullOrEmpty(this.PreviousRowID))
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// Problem tracking id
    /// </summary>
    public string ProblemId
    {
        get;
        set;
    }

    /// <summary>
    /// Row id of problem tracking
    /// </summary>
    public string ProblemDR
    {
        get;
        set;
    }

    /// <summary>
    /// Parent problem id
    /// </summary>
    public string ParentProblemId
    {
        get;
        set;
    }

    /// <summary>
    /// Parent problem DR
    /// </summary>
    public string ParentProblemDR
    {
        get;
        set;
    }

    public bool RequiredCallback { get; set; }
    public string LastCallbackNoteType { get; set; }
    public bool SuccessCallback { get; set; }
    public string LastCallbackPersonContacted { get; set; }


    public ClientIssues()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    /// <summary>
    /// Gets the detail of the issue for the given row id. 
    /// </summary>
    /// <param name="rowId">Issue RowId</param>
    public void GetClientIssueDetail(string rowId)
    {
        DataTable dtblCLIDetails = DL_ClientIssues.GetClientIssueDetail(rowId);

        this.RowId = dtblCLIDetails.Rows[0]["RowId"].ToString();
        this.ContactPerson = dtblCLIDetails.Rows[0]["ContactPerson"].ToString();
        this.AltContactNo = dtblCLIDetails.Rows[0]["AltContactNo"].ToString();
        this.DateSubmitted = dtblCLIDetails.Rows[0]["DateSubmitted"].ToString();
        this.DateTimeEntered = AtlasIndia.AntechCSM.UI.UIfunctions.combineDateTime(dtblCLIDetails.Rows[0]["DateEntered"].ToString(), dtblCLIDetails.Rows[0]["TimeEntered"].ToString());
        this.EnteredByUser = dtblCLIDetails.Rows[0]["EnteredByUser"].ToString();
        this.ProgressingStatus = dtblCLIDetails.Rows[0]["ProcessingStatus"].ToString();
        this.Reason = dtblCLIDetails.Rows[0]["Reason"].ToString();
        this.ReasonID = dtblCLIDetails.Rows[0]["ReasonID"].ToString();
        this.PriorityIssue = dtblCLIDetails.Rows[0]["PriorityIssue"].ToString();
        this.ClientMnemonic = dtblCLIDetails.Rows[0]["ClientMnemonic"].ToString();
        this.AssignToGroup = dtblCLIDetails.Rows[0]["AssignToGroup"].ToString();
        this.ProbTrckCat = dtblCLIDetails.Rows[0]["ProbTrckCat"].ToString();
        this.CheckOutBy = dtblCLIDetails.Rows[0]["CheckOutBy"].ToString();
        this.LabLocation = dtblCLIDetails.Rows[0]["LabLocation"].ToString();
        this.LabLocationId = dtblCLIDetails.Rows[0]["LabLocationId"].ToString();
        this.AccessionNumber = dtblCLIDetails.Rows[0]["AccessionNumber"].ToString();
        this.ProbTrackLab = dtblCLIDetails.Rows[0]["ProbTrackLab"].ToString();
        this.ProblemCategoryNo = dtblCLIDetails.Rows[0]["ProbTrckNo"].ToString();
        this.ProbTrackLabID = dtblCLIDetails.Rows[0]["ProbTrackLabId"].ToString();
        this.ReIssuedFlag = dtblCLIDetails.Rows[0]["IsReIssued"].ToString();
        this.PreviousRowID = dtblCLIDetails.Rows[0]["PrevCLIRow"].ToString();
        this.ProblemId = dtblCLIDetails.Rows[0]["ProblemId"].ToString();
        this.ProblemDR = dtblCLIDetails.Rows[0]["ProblemDR"].ToString();
        this.ParentProblemId = dtblCLIDetails.Rows[0]["ParentProblemId"].ToString();
        this.ParentProblemDR = dtblCLIDetails.Rows[0]["ParentProblemDR"].ToString();
    }

    public static DataTable GetAssignedToGroupList(string labId)
    {
        return DL_ClientIssues.GetAssignedToGroupList(labId);
    }

    /// <summary>
    /// Gets the issue progress notes for the given rowid
    /// </summary>
    /// <param name="rowId">Issue RowId</param>
    /// <returns>Datatable containing the progress notes</returns>
    public static DataTable GetProgressNotes(string rowId)
    {
        return DL_ClientIssues.GetProgressNotes(rowId);
    }

    /// <summary>
    /// Get persons contacted in clinic when taking the callback notes
    /// </summary>
    /// <param name="rowId"></param>
    /// <returns></returns>
    public static string GetCallbackContactedPerson (string rowId)
    {
        return DL_ClientIssues.GetCallbackContactedPerson(rowId);
    }

    public void GetClientIssueCallbackNotes(string rowId)
    {
        this.LastCallbackNoteType = "0";
        this.RequiredCallback = true;
        this.LastCallbackPersonContacted = string.Empty;
        DataTable table = DL_ClientIssues.GetClientIssueCallbackNotes(rowId);
        
        if (table != null && table.Rows.Count > 0)
        {
            var lastNote = (from row in table.AsEnumerable()
                            select row).FirstOrDefault();

            this.RequiredCallback = string.IsNullOrEmpty(lastNote.Field<string>("RequiredCallback")) ? true : lastNote.Field<string>("RequiredCallback").Equals("N") ? false : true;
            this.LastCallbackNoteType = lastNote.Field<string>("NoteType");
            this.SuccessCallback = string.IsNullOrEmpty(lastNote.Field<string>("SuccessCall")) ? false : lastNote.Field<string>("SuccessCall").Equals("Y") ? true : false;
            this.LastCallbackPersonContacted = lastNote.Field<string>("ContactedPerson");

            if (!RequiredCallback)
            {
                this.LastCallbackPersonContacted = "N/A";
            }
        }
    }

    /// <summary>
    /// Populate client issue callback notes
    /// </summary>
    /// <param name="rowId"></param>
    /// <param name="obj"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string UpdateCallbackNote (string rowId, ClientIssueCallback obj, string user)
    {
        if (obj == null) throw new ArgumentNullException("obj");

        return DL_ClientIssues.UpdateCallbackNote(rowId, obj.Required, obj.NoteType, obj.Details, obj.PersonContacted, user);
    }

    /// <summary>
    /// Retriev all client callback notes for the issue
    /// </summary>
    /// <param name="rowId"></param>
    /// <returns></returns>
    public static DataTable GetClientIssueCallbackNoteDetails (string rowId)
    {
        return DL_ClientIssues.GetClientIssueCallbackNoteDetails(rowId);
    }

    /// <summary>
    /// Gets the first progress note from the given issue rowid
    /// </summary>
    /// <param name="rowId">Issue RowId</param>
    /// <returns>Datatable having the first progress note</returns>
    public static DataTable GetFirstProgressNotes(string rowId)
    {
        return DL_ClientIssues.GetFirstProgressNotes(rowId);
    }

    /// <summary>
    /// Inserts the progress notes and copies to inquiry note when copyInqNote is flagged.
    /// </summary>
    /// <param name="rowID">Client Issue RowId</param>
    /// <param name="note">Notes to be inserted</param>
    /// <param name="user">User inserting the notes</param>
    /// <param name="copyInqNote">Copy to inquiry note</param>
    /// <returns>Returns a string telling the status of the notes insertion</returns>
    public static string UpdateProgressNote(string rowID, string note, string user, string copyInqNote)
    {
        return DL_ClientIssues.UpdateProgressNote(rowID, note, user, copyInqNote);
    }

    /// <summary>
    /// Inserts a new Issue
    /// </summary>
    /// <param name="accountNo">Account number for the issue</param>
    /// <param name="accessionNo">Accession number for the issue</param>
    /// <param name="priorityIssue">Priority of the issue</param>
    /// <param name="contactPerson">Contact person for the issue</param>
    /// <param name="altContactNo">Alternate contact number for the issue</param>
    /// <param name="status">Current status of the issue</param>
    /// <param name="user">User</param>
    /// <param name="reason">Reasoon for the issue</param>
    /// <param name="labLocation">Location of the lab</param>
    /// <param name="assignToGroup">Group assigned to</param>
    /// <param name="probTrackCat">Problem tracking category</param>
    /// <param name="probTrackCatLab">Problem tracking category lab</param>
    /// <param name="comments">Commments regarding the issue</param>
    /// <returns>Returns a string which tells the status of the insertion</returns>
    public static string InsertClientIssue(string accountNo, string accessionNo, string priorityIssue, string contactPerson, string altContactNo, string status, string user, string reason, string labLocation, string assignToGroup, string probTrackCat, string probTrackCatLab, string comments)
    {
        return DL_ClientIssues.InsertClientIssue(accountNo, accessionNo, priorityIssue, contactPerson, altContactNo, status, user, reason, labLocation, assignToGroup, probTrackCat, probTrackCatLab, comments);
    }

    /// <summary>
    /// Searches the client issues based on the given parameters and returns the datatable containing searched issues
    /// </summary>
    /// <param name="accountNo">Account number for the issue</param>
    /// <param name="accessionNo">Accession number for the issue</param>
    /// <param name="fromDate">From date range for the search</param>
    /// <param name="toDate">To date range for the search</param>
    /// <param name="priority">Priority of the issue</param>
    /// <param name="checkedBy">User who checked the issue out</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="progressStatus">Status of the progress of the issue</param>
    /// <param name="enteredBy">User id of the user entered</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containg the issues searched</returns>
    public static DataTable GetClientIssues(string accountNo, string accessionNo, string fromDate, string toDate, string priority, string checkedBy, string labLocation, string progressStatus, string enteredBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        return DL_ClientIssues.GetClientIssues(accountNo, accessionNo, fromDate, toDate, priority, checkedBy, labLocation, progressStatus, enteredBy, assignedToGroup, issueReason, finalizedBy, salesTerritory);
    }

    /// <summary>
    /// Checkouts an issue for the requested user 
    /// </summary>
    /// <param name="rowId">RowId of the issue</param>
    /// <param name="accessionNo">Accession number</param>
    /// <returns>returns boolean value which tells about the status of the checkout</returns>
    public static bool CheckOut(string rowId, string accessionNo)
    {
        DL_ClientIssues.CheckOut(rowId, accessionNo, SessionHelper.UserContext.ID);
        return true;
    }

    /// <summary>
    /// UnCheckOut an issue for the requested user
    /// </summary>
    /// <param name="rowId">RowId of the issue</param>
    /// <param name="accessionNo">Accession Number</param>
    /// <returns>returns boolean value which tells about the status of the UnCheckOut</returns>
    public static bool UnCheckOut(string rowId, string accessionNo)
    {
        DL_ClientIssues.UnCheckOut(rowId, accessionNo, SessionHelper.UserContext.ID);
        return true;
    }

    /// <summary>
    /// Checks for pending issues with a particular client
    /// </summary>
    /// <param name="clientId">Client account number</param>
    /// <returns>Datatable containing duplicate client issues if any</returns>
    public static DataTable CheckDuplicateClientIssueEntry(string clientId)
    {
        return DL_ClientIssues.VerifyExistingRecords(clientId);
    }

    /// <summary>
    /// Resolves a client issue
    /// </summary>
    /// <param name="RowID">RowId of the issue</param>
    /// <param name="Accession">Accession Number</param>
    /// <param name="user">User</param>
    /// <param name="ProblemCat">Problen Tracking category</param>
    /// <param name="ResolutionNote">Resolution note</param>
    /// <param name="PersonContact">Person contacted</param>
    /// <param name="AccountNo">Account numberof the user</param>
    /// <param name="lab">lab location</param>
    /// <param name="ProbTrackLab">Problem tracking lab</param>
    /// <param name="ProbTrackId">Problem tracking Id</param>
    /// <returns>Returns a string which tells the status of the resolution</returns>
    public static string ResolveClientIssue(string RowID, string Accession, string user, string ProblemCat, string ResolutionNote, string PersonContact, string AccountNo, string lab, string ProbTrackLab, string ProbTrackId, string CallbackNoteType, bool RequiredCallback)
    {
        return DL_ClientIssues.ResolveClientIssue(RowID, Accession, user, ProblemCat, ResolutionNote, PersonContact, AccountNo, lab, ProbTrackLab, ProbTrackId, CallbackNoteType, RequiredCallback);
    }

    /// <summary>
    /// Gets the report of the issue
    /// </summary>
    /// <param name="strQueryString">Query string to required to generate the report</param>
    /// <returns>Datatable containing the report generated</returns>
    public static DataTable GetClientIssuesReport(string strQueryString)
    {
        String[] qsParts = strQueryString.Split('^');
        return DL_ClientIssues.GetClientIssuesReport(qsParts[0], qsParts[1], qsParts[2], qsParts[3], qsParts[4], qsParts[5], qsParts[6], qsParts[7], qsParts[8], qsParts[9], qsParts[10], qsParts[11], qsParts[12]);
    }

    /// <summary>
    /// Updates the client issue
    /// </summary>
    /// <param name="rowID">RowId of the issue</param>
    /// <param name="contactPerson">Contact person</param>
    /// <param name="alternateContNo">Alternate contact number</param>
    /// <param name="priorityValue">Priority of the issue</param>
    /// <param name="reason">Reason for the issue</param>
    /// <param name="groupValue">Assigned to group id</param>
    /// <param name="labLocation">llocation of the lab</param>
    /// <param name="user">User</param>
    /// <param name="probTrckCat">Problem tracking category</param>
    /// <param name="probTrckLab">Problem tracking laboratory</param>
    /// <returns>Returns a string telling the success or failure status of the update</returns>
    public static string UpdateClientIssue(string rowID, string contactPerson, string alternateContNo, string priorityValue, string reason, string groupValue, string labLocation, string user, string probTrckCat, string probTrckLab)
    {
        return DL_ClientIssues.UpdateClientIssue(rowID, contactPerson, alternateContNo, priorityValue, reason, groupValue, labLocation, user, probTrckCat, probTrckLab);
    }

    /// <summary>
    /// Updates the client issue problem category
    /// </summary>
    /// <param name="rowID">RowId of the issue</param>
    /// <param name="probCat">Problem tracking category</param>
    /// <returns>Returns a string telling the success or failure status of the update</returns>
    public static string UpdateClientIssueProbCat(string rowID, string probCat)
    {
        return DL_ClientIssues.UpdateClientIssueProbCat(rowID, probCat, SessionHelper.UserContext.ID);
    }

    #region Completed Client Issue Reports

    /// <summary>
    /// Gets the completed client issue summary report
    /// </summary>
    /// <param name="strQueryString">Query string containing the search criteria of the issue for the report</param>
    /// <param name="totalCount">reference variable for total count of the issues</param>
    /// <param name="dateRequested">reference variable for date requested</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    public static DataTable getCompletedClientIssueSummaryReport(string strQueryString, out int totalCount, out string dateRequested, out int[] arrGrandTotal)
    {
        String[] QS = strQueryString.Split('^');
        DataTable returnDataTable = DL_ClientIssues.getCompletedClientIssueSummaryReport(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], false);
        DataTable dtIssueCompleteReport = new DataTable();
        dtIssueCompleteReport = GetIssuesByClientSummaryTable(false);
        totalCount = 0;
        dateRequested = QS[3] + " to " + QS[4];
        arrGrandTotal = new int[13];
        dtIssueCompleteReport = SummaryReportLogic(returnDataTable, dtIssueCompleteReport, "ClientMne", "ClientCount", "ReasonIssue", dateRequested, "completeIssue", out arrGrandTotal, true);
        totalCount = arrGrandTotal[0];
        dtIssueCompleteReport.DefaultView.Sort = "CompletedClientIssues DESC";
        return dtIssueCompleteReport;
    }

    /// <summary>
    /// Gets the completed client issue detail report
    /// </summary>
    /// <param name="strQueryString">Query string containing the search detail of the issues</param>
    /// <param name="totalCount">reference variable for total count of the issue</param>
    /// <param name="dateRequested">reference variable for date requested</param>
    /// <returns>Datatable containing the issue details</returns>
    public static DataTable getCompletedClientIssueDetailReport(string strQueryString, out int totalCount, out string dateRequested)
    {
        String[] QS = strQueryString.Split('^');

        DataTable dtblReport = new DataTable();
        dtblReport.Columns.Add("ClientMnemonic", typeof(string));
        dtblReport.Columns.Add("ClientAccountNumber", typeof(string));
        dtblReport.Columns.Add("ClientFirstName", typeof(string));
        dtblReport.Columns.Add("RepName", typeof(string));
        dtblReport.Columns.Add("RepPhone", typeof(string));
        dtblReport.Columns.Add("RepExtension", typeof(string));
        dtblReport.Columns.Add("RepMail", typeof(string));
        dtblReport.Columns.Add("ManagerName", typeof(string));
        dtblReport.Columns.Add("ManagerPhone", typeof(string));
        dtblReport.Columns.Add("ManagerExtension", typeof(string));
        dtblReport.Columns.Add("ManagerMail", typeof(string));
        dtblReport.Columns.Add("LastUpdated", typeof(string));
        dtblReport.Columns.Add("CompletedClientIssues", typeof(int));
        dtblReport.Columns.Add("TerritoryCode", typeof(string));
        dtblReport.Columns.Add("DetailsTable", typeof(DataTable));

        DataTable dtblReportDetail = new DataTable();
        dtblReportDetail.Columns.Add("ClientMnee", typeof(string));
        dtblReportDetail.Columns.Add("Accession", typeof(string));
        dtblReportDetail.Columns.Add("Reason", typeof(string));
        dtblReportDetail.Columns.Add("Status", typeof(string));
        dtblReportDetail.Columns.Add("EnteredDate", typeof(string));
        dtblReportDetail.Columns.Add("Priority", typeof(string));
        dtblReportDetail.Columns.Add("ResolvedDate", typeof(string));
        dtblReportDetail.Columns.Add("ResolvedTime", typeof(string));
        dtblReportDetail.Columns.Add("Lab", typeof(string));
        dtblReportDetail.Columns.Add("ProblemCat", typeof(string));
        dtblReportDetail.Columns.Add("ClientName", typeof(string));

        DataTable dtblRawData = DL_ClientIssues.getCompletedClientIssueSummaryReport(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], true);

        totalCount = 0;
        dateRequested = QS[3] + " to " + QS[4];
        string strClientMne = string.Empty;
        string strAcctNo = string.Empty;
        string clientName = string.Empty;

        string repName = string.Empty;
        string repPhone = string.Empty;
        string repExten = string.Empty;
        string repMail = string.Empty;
        string manName = string.Empty;
        string manPhone = string.Empty;
        string manExten = string.Empty;
        string manMail = string.Empty;
        string lastUpdate = string.Empty;
        string territoryCode = string.Empty;

        int intCount = 0;

        DataTable dtDetailsTable = DL_ClientIssues.GetClientSummaryDetails(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8]);

        for (int intCnt = 0; intCnt < dtblRawData.Rows.Count; intCnt++)
        {
            strClientMne = dtblRawData.Rows[intCnt]["ClientMne"].ToString();
            strAcctNo = dtblRawData.Rows[intCnt]["ClientAcctNo"].ToString();
            clientName = dtblRawData.Rows[intCnt]["ClientFirstName"].ToString();
            repName = dtblRawData.Rows[intCnt]["RepName"].ToString();
            repPhone = dtblRawData.Rows[intCnt]["RepPhone"].ToString();
            repExten = dtblRawData.Rows[intCnt]["RepExtension"].ToString();
            repMail = dtblRawData.Rows[intCnt]["RepMail"].ToString();
            manName = dtblRawData.Rows[intCnt]["ManagerName"].ToString();
            manPhone = dtblRawData.Rows[intCnt]["ManagerPhone"].ToString();
            manExten = dtblRawData.Rows[intCnt]["ManagerExten"].ToString();
            manMail = dtblRawData.Rows[intCnt]["ManagerMail"].ToString();
            lastUpdate = dtblRawData.Rows[intCnt]["LastUpdate"].ToString();
            territoryCode = dtblRawData.Rows[intCnt]["TerritoryCode"].ToString();

            DataTable dtIssueDetails = GetClientSummaryDetails(strClientMne, dtDetailsTable, dtblReportDetail.Clone());

            DataRow drowNewRow = dtblReport.NewRow();
            drowNewRow["ClientMnemonic"] = strClientMne;
            drowNewRow["ClientAccountNumber"] = strAcctNo;
            drowNewRow["ClientFirstName"] = clientName;
            drowNewRow["RepName"] = repName;
            drowNewRow["RepPhone"] = repPhone;
            drowNewRow["RepExtension"] = repExten;
            drowNewRow["RepMail"] = repMail;
            drowNewRow["ManagerName"] = manName;
            drowNewRow["ManagerPhone"] = manPhone;
            drowNewRow["ManagerExtension"] = manExten;
            drowNewRow["ManagerMail"] = manMail;
            drowNewRow["LastUpdated"] = lastUpdate;
            intCount += Convert.ToInt32(dtblRawData.Rows[intCnt]["ClientCount"]);
            drowNewRow["CompletedClientIssues"] = intCount;
            drowNewRow["TerritoryCode"] = territoryCode;
            drowNewRow["DetailsTable"] = dtIssueDetails;
            dtblReport.Rows.Add(drowNewRow);
            totalCount += intCount;
            intCount = 0;
        }
        return SortTableByColumn(dtblReport, "ClientAccountNumber", "System.Int32", "ASC");
    }

    public static DataTable SortTableByColumn(DataTable dtOriginalTable, string columnName, string convertingDataType, string order)
    {
        if (dtOriginalTable == null || dtOriginalTable.Rows.Count == 0)
        {
            return dtOriginalTable;
        }
        DataTable dtClone = dtOriginalTable.Clone();
        dtClone.Columns[columnName].DataType = Type.GetType(convertingDataType);

        foreach (DataRow dr in dtOriginalTable.Rows)
        {
            if (string.IsNullOrEmpty(Convert.ToString(dr[columnName])))
                dr[columnName] = null;
            dtClone.ImportRow(dr);
        }
        dtClone.AcceptChanges();
        DataView dv = dtClone.DefaultView;
        dv.Sort = columnName + " " + order;
        return dv.ToTable();
    }

    /// <summary>
    /// Gets the client issue summary details
    /// </summary>
    /// <param name="enteredBy">Entered by usre id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From Date</param>
    /// <param name="dateTo">To Date</param>
    /// <param name="clientMne">Client Mnemonic</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress notes</param>
    /// <param name="checkedBy">Checked by user id</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable GetClientSummaryDetails(string clientMne, DataTable dtDatailsTable, DataTable dtblReportDetail)
    {
        DataTable dtReturn = new DataTable();
        dtReturn.Columns.Add("DateResolved", typeof(string));
        dtReturn.Columns.Add("TotalCount", typeof(int));
        dtReturn.Columns.Add("DetailsTable1", typeof(DataTable));

        string clientMnee = "";
        string clientName = "";
        string accessionNum = "";
        string reason = "";
        string status = "";
        string enteredDate = "";
        string priority = "";
        string resolvedDate = "";
        string resolvedTime = "";
        string lab = "";
        string probCat = "";

        DataRow[] drReport = dtDatailsTable.Select("ClientMnee='" + clientMne + "'", "ClientMnee ASC");
        if (drReport != null && drReport.Length > 0)
        {
            string strOldDate = Convert.ToDateTime(drReport[0]["ResolvedDate"]).ToString("MM/dd/yyyy");
            string strNewDate = "";
            string accession = "";
            string strReasonAbr = "";
            int iCount = 0;

            DataTable dtChild = dtDatailsTable.Clone();
            for (int iRowCount = 0; iRowCount < drReport.Length; iRowCount++)
            {
                clientMnee = drReport[iRowCount]["ClientMnee"].ToString().Trim();
                clientName = drReport[iRowCount]["ClientName"].ToString().Trim();
                accessionNum = drReport[iRowCount]["Accession"].ToString().Trim();
                reason = drReport[iRowCount]["Reason"].ToString().Trim();

                switch (reason)
                {
                    case "PENDOVERDUEREP":
                        strReasonAbr = "TAT";
                        break;
                    case "CANCELTEST":
                        strReasonAbr = "CXA";
                        break;
                    case "CNSLTCOMM":
                        strReasonAbr = "CON";
                        break;
                    case "COGGINSEIAISS":
                        strReasonAbr = "COG";
                        break;
                    case "CYTOISS":
                        strReasonAbr = "CYT";
                        break;
                    case "GENINQ":
                        strReasonAbr = "GEN";
                        break;
                    case "HISTOISS":
                        strReasonAbr = "HIS";
                        break;
                    case "LABISS":
                        strReasonAbr = "TRA";
                        break;
                    case "ORDERENTRY":
                        strReasonAbr = "OEP";
                        break;
                    case "SALESISS":
                        strReasonAbr = "SAL";
                        break;
                    case "SPECTRANSISSUE":
                        strReasonAbr = "LAB";
                        break;
                    case "UNREQSUPISS":
                        strReasonAbr = "SUP";
                        break;
                }

                status = drReport[iRowCount]["Status"].ToString().Trim();
                enteredDate = drReport[iRowCount]["EnteredDate"].ToString().Trim();
                priority = drReport[iRowCount]["Priority"].ToString().Trim();
                resolvedDate = drReport[iRowCount]["ResolvedDate"].ToString().Trim();
                resolvedTime = drReport[iRowCount]["ResolvedTime"].ToString().Trim();
                lab = drReport[iRowCount]["Lab"].ToString().Trim();
                probCat = drReport[iRowCount]["problemCat"].ToString().Trim();

                strNewDate = Convert.ToDateTime(resolvedDate).ToString("MM/dd/yyyy");
                accession = accessionNum;

                if (strNewDate.Equals(strOldDate))
                {
                    dtChild.Rows.Add(clientMnee, accessionNum, strReasonAbr, status, enteredDate, priority, resolvedDate, resolvedTime, lab, probCat, clientName);
                    iCount++;
                }
                else
                {
                    dtReturn.Rows.Add(strOldDate, iCount, dtChild);
                    iCount = 0;
                    iRowCount--;
                    dtChild = null;
                    dtChild = dtDatailsTable.Clone();
                }

                strOldDate = strNewDate;
            }
            dtReturn.Rows.Add(strOldDate, iCount, dtChild);
        }
        return dtReturn;
    }
    #endregion
    #region Canned Reports

    /// <summary>
    /// Gets the summary issue report, reported by account
    /// </summary>
    /// <param name="strQueryString">Query string containing the search detail of the issues</param>
    /// <param name="strFromDate">reference variable for from date</param>
    /// <param name="strToDate">reference variable to Date</param>
    /// <param name="arrGrandTotal">reference variable total count of the issues</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable ReportedIssueByAccountSummary(string strQueryString, out string strFromDate, out string strToDate, out int[] arrGrandTotal)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtIssueByAccountSummary = new DataTable();
        dtIssueByAccountSummary = GetIssuesByAccountSummaryTable(false);
        strFromDate = QS[3];
        strToDate = QS[4];
        arrGrandTotal = new int[13];
        DataTable returnDataTable = DL_ClientIssues.ReportedIssueByAccountSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], false);
        dtIssueByAccountSummary = SummaryReportLogic(returnDataTable, dtIssueByAccountSummary, "CLNUM", "CountIssue", "ReasonIssue", "CLNAM", "account", out arrGrandTotal, true);
        dtIssueByAccountSummary.DefaultView.Sort = "TotalIssue DESC";
        return dtIssueByAccountSummary;
    }

    /// <summary>
    /// Table having fields for the report
    /// </summary>
    /// <param name="isDetailTable">Flag to determine detail or summry table</param>
    /// <returns>Datatable with the fields for summary and detail report</returns>
    public static DataTable GetIssuesByAccountSummaryTable(bool isDetailTable)
    {
        DataTable table = new DataTable();
        table.Columns.Add("AccountNo", typeof(string));
        table.Columns.Add("AccountName", typeof(string));
        table.Columns.Add("TotalIssue", typeof(int));
        table.Columns.Add("Revenue1", typeof(string));
        table.Columns.Add("Revenue2", typeof(string));
        table.Columns.Add("Revenue3", typeof(string));
        if (!isDetailTable)
        {
            table.Columns.Add("CXA", typeof(int));
            table.Columns.Add("COG", typeof(int));
            table.Columns.Add("CON", typeof(int));
            table.Columns.Add("CYT", typeof(int));
            table.Columns.Add("GEN", typeof(int));
            table.Columns.Add("HIS", typeof(int));
            table.Columns.Add("OEP", typeof(int));
            table.Columns.Add("SAL", typeof(int));
            table.Columns.Add("LAB", typeof(int));
            table.Columns.Add("TAT", typeof(int));
            table.Columns.Add("TRA", typeof(int));
            table.Columns.Add("SUP", typeof(int));
        }
        if (isDetailTable)
        {
            table.Columns.Add("AccountPhoneNo", typeof(string));
            table.Columns.Add("RouteStop", typeof(string));
            table.Columns.Add("SalesTerritory", typeof(string));
            table.Columns.Add("DetailTable", typeof(DataTable));
        }
        return table;
    }

    /// <summary>
    /// Gets the detail issue report, reported by account
    /// </summary>
    /// <param name="strQueryString">Query string containing the search detail of the issues</param>
    /// <param name="strFromDate">reference variable for from date</param>
    /// <param name="strToDate">reference variable for to date</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable GetReportedIssueByAccountDetail(string strQueryString, out string strFromDate, out string strToDate)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtConsultByAccountDetail = new DataTable();
        dtConsultByAccountDetail = GetIssuesByAccountSummaryTable(true);
        strFromDate = QS[3];
        strToDate = QS[4];

        DataTable dtDetailSchema = new DataTable();
        dtDetailSchema.Columns.Add("Accession", typeof(string));
        dtDetailSchema.Columns.Add("Priority", typeof(string));
        dtDetailSchema.Columns.Add("EnteredDate", typeof(string));
        dtDetailSchema.Columns.Add("ProbCat", typeof(string));
        dtDetailSchema.Columns.Add("Lab", typeof(string));
        dtDetailSchema.Columns.Add("Reason", typeof(string));
        dtDetailSchema.Columns.Add("Status", typeof(string));

        DataTable returnDataTable = DL_ClientIssues.ReportedIssueByAccountSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], true);
        DataTable dtCompDetailsTable = DL_ClientIssues.GetReportedIssuesByAccountChild(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            double dRevenue1 = 0, dRevenue2 = 0, dRevenue3 = 0;
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strCount = returnDataTable.Rows[iRowCount]["CountIssue"].ToString().Trim();
                string strClientRevenue = returnDataTable.Rows[iRowCount]["CLIENTREVENUE"].ToString().Trim();
                int iCount = 0;
                if (strCount.Length > 0)
                {
                    iCount = Convert.ToInt32(strCount);
                }

                string[] arrRevenue = strClientRevenue.Split(new Char[] { '^' });
                if (arrRevenue.Length > 2)
                {
                    dRevenue1 = (arrRevenue[0].Length > 0) ? Convert.ToDouble(arrRevenue[0].ToString()) : 0;
                    dRevenue2 = (arrRevenue[1].Length > 0) ? Convert.ToDouble(arrRevenue[1].ToString()) : 0;
                    dRevenue3 = (arrRevenue[2].Length > 0) ? Convert.ToDouble(arrRevenue[2].ToString()) : 0;
                }
                string strAccountNo = returnDataTable.Rows[iRowCount]["CLNUM"].ToString().Trim();
                string strAccountName = returnDataTable.Rows[iRowCount]["CLNAM"].ToString().Trim();
                string strAccountPhoneNumber = returnDataTable.Rows[iRowCount]["CLF_CLPHN"].ToString().Trim();
                string strRouteStop = returnDataTable.Rows[iRowCount]["CLF_RSTOP"].ToString().Trim();
                string strSalesTerritory = returnDataTable.Rows[iRowCount]["ST_TerritoryCode"].ToString().Trim();
                if (iCount > 0 && strAccountNo.Length > 0)
                {
                    DataTable dtDatailsTable = GetReportedIssueChildTable(strAccountNo, dtCompDetailsTable, dtDetailSchema.Clone());
                    dtConsultByAccountDetail.Rows.Add(strAccountNo, strAccountName, iCount, dRevenue1.ToString("#,###,##0.00"), dRevenue2.ToString("#,###,##0.00"), dRevenue3.ToString("#,###,##0.00"), strAccountPhoneNumber, strRouteStop, strSalesTerritory, dtDatailsTable);
                }
            }
        }
        return dtConsultByAccountDetail;
    }

    /// <summary>
    /// Gets the child table for detailed report, reported by account
    /// </summary>
    /// <param name="accountNo">Account number</param>
    /// <param name="dtCompDetailsTable">Datatable having the results of "GetReportedIssuesByAccountChild" DL method</param>
    /// <param name="dtDetailTable">Datatable clone of dtDetailSchema</param>
    /// <returns>Datatable containing the details of the child table</returns>
    public static DataTable GetReportedIssueChildTable(string accountNo, DataTable dtCompDetailsTable, DataTable dtDetailTable)
    {
        string accession = "";
        string priority = "";
        string enterDate = "";
        string probCat = "";
        string lab = "";
        string reason = "";
        string retReason = "";
        string status = "";
        List<KeyValuePair> allReasonMappings = FetchAllClientIssueReasonUIMapping();
        DataRow[] drReport = dtCompDetailsTable.Select("AccountNo='" + accountNo + "'", "AccountNo ASC");

        if (drReport != null && drReport.Length > 0)
        {
            for (int iCnt = 0; iCnt < drReport.Length; iCnt++)
            {
                accession = drReport[iCnt]["Accession"].ToString().Trim();
                priority = drReport[iCnt]["Priority"].ToString().Trim();
                enterDate = drReport[iCnt]["EnteredDate"].ToString().Trim();
                probCat = drReport[iCnt]["ProbCat"].ToString().Trim();
                lab = drReport[iCnt]["Lab"].ToString().Trim();
                retReason = drReport[iCnt]["Reason"].ToString().Trim();
                var targetStatusObj = allReasonMappings.SingleOrDefault(rea => rea.Key == retReason);
                reason = (targetStatusObj != null) ? targetStatusObj.Value : retReason;
                status = drReport[iCnt]["Status"].ToString().Trim();
                dtDetailTable.Rows.Add(accession, priority, enterDate, probCat, lab, reason, status);
            }
        }
        return dtDetailTable;
    }

    /// <summary>
    /// Contains logic to generate summary report
    /// </summary>
    /// <param name="sourceDatatable">Source data table for the logic</param>
    /// <param name="resultDatatable">Return datatable structure</param>
    /// <param name="groupString">String used to specify coloumn for grouping</param>
    /// <param name="countString">String used to specify coloumn for count of the issues</param>
    /// <param name="groupString">String used to specify coloumn for grouping</param>
    /// <param name="reasonString">String used to specify coloumn for reason</param>
    /// <param name="secondVariable">String specifing the second parameter coloumn name</param>
    /// <param name="reportName">Report name for which the datatable is generated</param>
    /// <param name="arrGrandTotal">Reference array containning the total counts</param>
    /// <param name="isGrandReasonTotReq">Boolean variable specifing whether grand total for the reasons are required</param>
    /// <returns>Datatable containing the details of the child table</returns>
    public static DataTable SummaryReportLogic(DataTable sourceDatatable, DataTable resultDatatable, string groupString, string countString, string reasonString,string secondVariable, string reportName, out int[] arrGrandTotal,bool isGrandReasonTotReq)
    {
        int iTotalCount = 0;
        arrGrandTotal = new int[1];
        if (isGrandReasonTotReq)
        {
            arrGrandTotal = new int[13];
        }
        if (sourceDatatable != null && sourceDatatable.Rows.Count > 0)
        {
            double dRevenue1 = 0, dRevenue2 = 0, dRevenue3 = 0;
            int totalIssueCount = 0;
            int iCxa = 0, iCog = 0, iCon = 0, iCyt = 0, iGen = 0, iHis = 0, iOep = 0, iSal = 0, iLab = 0, iTat = 0, iTra = 0, iSup = 0;
            int iGrandCxa = 0, iGrandCog = 0, iGrandCon = 0, iGrandCyt = 0, iGrandGen = 0, iGrandHis = 0, iGrandOep = 0, iGrandSal = 0, iGrandLab = 0, iGrandTat = 0, iGrandTra = 0, iGrandSup = 0;
            string strOldGroupValue = sourceDatatable.Rows[0][groupString].ToString().Trim();

            for (int iRowCount = 0; iRowCount < sourceDatatable.Rows.Count; iRowCount++)
            {
                string strNewGroupValue = sourceDatatable.Rows[iRowCount][groupString].ToString().Trim();
                if (strNewGroupValue == strOldGroupValue)
                {
                    string strCount = sourceDatatable.Rows[iRowCount][countString].ToString().Trim();
                    int iCount = 0;
                    iCount = Convert.ToInt32(strCount);

                    if (reportName == "account")
                    {
                        string strClientRevenue = sourceDatatable.Rows[iRowCount]["CLIENTREVENUE"].ToString().Trim();
                        string[] arrRevenue = strClientRevenue.Split(new Char[] { '^' });
                        if (arrRevenue.Length > 2)
                        {
                            dRevenue1 = (arrRevenue[0].Length > 0) ? Convert.ToDouble(arrRevenue[0]) : 0;
                            dRevenue2 = (arrRevenue[1].Length > 0) ? Convert.ToDouble(arrRevenue[1]) : 0;
                            dRevenue3 = (arrRevenue[2].Length > 0) ? Convert.ToDouble(arrRevenue[2]) : 0;
                        }
                    }

                    string strReason = sourceDatatable.Rows[iRowCount][reasonString].ToString().Trim();
                    iTotalCount += iCount;
                    totalIssueCount += iCount;
                    switch (strReason)
                    {
                        case "PENDOVERDUEREP":
                            iTat += iCount;
                            break;
                        case "CANCELTEST":
                            iCxa += iCount;
                            break;
                        case "CNSLTCOMM":
                            iCon += iCount;
                            break;
                        case "COGGINSEIAISS":
                            iCog += iCount;
                            break;
                        case "CYTOISS":
                            iCyt += iCount;
                            break;
                        case "GENINQ":
                            iGen += iCount;
                            break;
                        case "HISTOISS":
                            iHis += iCount;
                            break;
                        case "LABISS":
                            iTra += iCount;
                            break;
                        case "ORDERENTRY":
                            iOep += iCount;
                            break;
                        case "SALESISS":
                            iSal += iCount;
                            break;
                        case "SPECTRANSISSUE":
                            iLab += iCount;
                            break;
                        case "UNREQSUPISS":
                            iSup += iCount;
                            break;
                    }
                }
                else
                {
                    switch (reportName)
                    {
                        case "lab":
                            {
                                string strSecondParam = sourceDatatable.Rows[iRowCount - 1][secondVariable].ToString().Trim();
                                if (totalIssueCount > 0)
                                {
                                    resultDatatable.Rows.Add(strOldGroupValue, strSecondParam, totalIssueCount, iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                                }
                                break;
                            }
                        case "account":
                            {
                                string strSecondParam = sourceDatatable.Rows[iRowCount - 1][secondVariable].ToString().Trim();
                                if (totalIssueCount > 0)
                                {
                                    resultDatatable.Rows.Add(strOldGroupValue, strSecondParam, totalIssueCount, dRevenue1.ToString("#,###,##0.00"), dRevenue2.ToString("#,###,##0.00"), dRevenue3.ToString("#,###,##0.00"), iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                                }
                                break;
                            }
                        case "userAgent":
                            {
                                if (totalIssueCount > 0)
                                {
                                    resultDatatable.Rows.Add(strOldGroupValue, secondVariable, totalIssueCount, iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                                }
                                break;
                            }
                        case "territory":
                            {
                                string strSecondParam = sourceDatatable.Rows[iRowCount - 1][secondVariable].ToString().Trim();
                                string strManagerName = sourceDatatable.Rows[iRowCount - 1]["ManagerName"].ToString().Trim();
                                if (totalIssueCount > 0)
                                {
                                    resultDatatable.Rows.Add(strOldGroupValue, strSecondParam, strManagerName, totalIssueCount, iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                                }
                                break;
                            }
                        case "completeIssue":
                            {
                                string clientAcNo = sourceDatatable.Rows[iRowCount - 1]["ClientAcctNo"].ToString().Trim();
                                string clientName = sourceDatatable.Rows[iRowCount - 1]["ClientName"].ToString().Trim();
                                if (totalIssueCount > 0)
                                {
                                    resultDatatable.Rows.Add(strOldGroupValue, secondVariable, totalIssueCount, clientAcNo, clientName, iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                                }
                                break;
                            }
                    }
                    if (isGrandReasonTotReq)
                    {
                        iGrandCog += iCog;
                        iGrandCon += iCon;
                        iGrandCxa += iCxa;
                        iGrandCyt += iCyt;
                        iGrandGen += iGen;
                        iGrandHis += iHis;
                        iGrandLab += iLab;
                        iGrandOep += iOep;
                        iGrandSal += iSal;
                        iGrandSup += iSup;
                        iGrandTat += iTat;
                        iGrandTra += iTra;
                    }
                    totalIssueCount = 0; iCog = 0; iCon = 0; iCxa = 0; iCyt = 0; iGen = 0; iHis = 0; iLab = 0; iOep = 0; iSal = 0; iSup = 0; iTat = 0; iTra = 0;
                    iRowCount--;
                }
                strOldGroupValue = strNewGroupValue;
            }
            int iLastCount = sourceDatatable.Rows.Count;

            switch (reportName)
            {
                case "lab":
                    {
                        string strSecondParam = sourceDatatable.Rows[iLastCount - 1][secondVariable].ToString().Trim();
                        if (totalIssueCount > 0)
                        {
                            resultDatatable.Rows.Add(strOldGroupValue, strSecondParam, totalIssueCount, iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                        }
                        break;
                    }
                case "account":
                    {
                        string strSecondParam = sourceDatatable.Rows[iLastCount - 1][secondVariable].ToString().Trim();
                        if (totalIssueCount > 0)
                        {
                            resultDatatable.Rows.Add(strOldGroupValue, strSecondParam, totalIssueCount, dRevenue1.ToString("#,###,##0.00"), dRevenue2.ToString("#,###,##0.00"), dRevenue3.ToString("#,###,##0.00"), iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                        }
                        break;
                    }
                case "userAgent":
                    {
                        if (totalIssueCount > 0)
                        {
                            resultDatatable.Rows.Add(strOldGroupValue, secondVariable, totalIssueCount, iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                        }
                        break;
                    }
                case "territory":
                    {
                        string strSecondParam = sourceDatatable.Rows[iLastCount - 1][secondVariable].ToString().Trim();
                        string strManagerName = sourceDatatable.Rows[iLastCount - 1]["ManagerName"].ToString().Trim();
                        if (totalIssueCount > 0)
                        {
                            resultDatatable.Rows.Add(strOldGroupValue, strSecondParam, strManagerName, totalIssueCount, iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                        }
                        break;
                    }
                case "completeIssue":
                    {
                        string clientAcNo = sourceDatatable.Rows[iLastCount - 1]["ClientAcctNo"].ToString().Trim();
                        string clientName = sourceDatatable.Rows[iLastCount - 1]["ClientName"].ToString().Trim();
                        if (totalIssueCount > 0)
                        {
                            resultDatatable.Rows.Add(strOldGroupValue, secondVariable, totalIssueCount, clientAcNo, clientName, iCxa, iCog, iCon, iCyt, iGen, iHis, iOep, iSal, iLab, iTat, iTra, iSup);
                        }
                        break;
                    }
            }
            if (isGrandReasonTotReq)
            {
                iGrandCog += iCog;
                iGrandCon += iCon;
                iGrandCxa += iCxa;
                iGrandCyt += iCyt;
                iGrandGen += iGen;
                iGrandHis += iHis;
                iGrandLab += iLab;
                iGrandOep += iOep;
                iGrandSal += iSal;
                iGrandSup += iSup;
                iGrandTat += iTat;
                iGrandTra += iTra;

                int[] intarrGrandTotal = new int[13];
                intarrGrandTotal[0] = iTotalCount;
                intarrGrandTotal[1] = iGrandCxa;
                intarrGrandTotal[2] = iGrandCog;
                intarrGrandTotal[3] = iGrandCon;
                intarrGrandTotal[4] = iGrandCyt;
                intarrGrandTotal[5] = iGrandGen;
                intarrGrandTotal[6] = iGrandHis;
                intarrGrandTotal[7] = iGrandOep;
                intarrGrandTotal[8] = iGrandSal;
                intarrGrandTotal[9] = iGrandLab;
                intarrGrandTotal[10] = iGrandTat;
                intarrGrandTotal[11] = iGrandTra;
                intarrGrandTotal[12] = iGrandSup;
                Array.Copy(intarrGrandTotal, arrGrandTotal, 13);
            }
            else
            {
                int[] intarrGrandTotal = new int[1];
                intarrGrandTotal[0] = iTotalCount;
                Array.Copy(intarrGrandTotal, arrGrandTotal, 1);
            }
        }
        return resultDatatable;
    }

    /// <summary>
    /// Gets the summary issue report, reported by lab
    /// </summary>
    /// <param name="strQueryString">>Query string containing the search detail of the issues</param>
    /// <param name="strFromDate">reference variable for from date</param>
    /// <param name="strToDate">reference variable to Date</param>
    /// <param name="arrGrandTotal">reference variable total count of the issues</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable ReportedIssueByLabSummary(string strQueryString, out string strFromDate, out string strToDate, out int[] arrGrandTotal)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtIssueByLabSummary = new DataTable();
        dtIssueByLabSummary = GetIsuuesByLabSummaryTable(false);
        strFromDate = QS[3];
        strToDate = QS[4];
        arrGrandTotal = new int[1];
        DataTable returnDataTable = DL_ClientIssues.ReportedIssueByLabSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], false);
        dtIssueByLabSummary = SummaryReportLogic(returnDataTable, dtIssueByLabSummary, "LabCode", "TotalIssue", "ReasonIssue", "LabName", "lab", out arrGrandTotal, false);
        dtIssueByLabSummary.DefaultView.Sort = "TotalIssue DESC";
        return dtIssueByLabSummary;
    }

    /// <summary>
    /// Table having fields for the "Reported Issue by Laboratory" report
    /// </summary>
    /// <param name="isDetailTable">Flag to determine detail or summry table</param>
    /// <returns>Datatable with the fields for summary and detail report</returns>
    public static DataTable GetIsuuesByLabSummaryTable(bool isDetailTable)
    {
        DataTable table = new DataTable();
        table.Columns.Add("LabCode", typeof(string));
        table.Columns.Add("LabName", typeof(string));
        table.Columns.Add("TotalIssue", typeof(int));
        if (!isDetailTable)
        {
            table.Columns.Add("CXA", typeof(int));
            table.Columns.Add("COG", typeof(int));
            table.Columns.Add("CON", typeof(int));
            table.Columns.Add("CYT", typeof(int));
            table.Columns.Add("GEN", typeof(int));
            table.Columns.Add("HIS", typeof(int));
            table.Columns.Add("OEP", typeof(int));
            table.Columns.Add("SAL", typeof(int));
            table.Columns.Add("LAB", typeof(int));
            table.Columns.Add("TAT", typeof(int));
            table.Columns.Add("TRA", typeof(int));
            table.Columns.Add("SUP", typeof(int));
        }
        if (isDetailTable)
        {
            table.Columns.Add("DetailTable", typeof(DataTable));
        }
        return table;
    }

    /// <summary>
    /// Table having fields for the "Reported Issue by Laboratory" report
    /// </summary>
    /// <param name="isDetailTable">Flag to determine detail or summry table</param>
    /// <returns>Datatable with the fields for summary and detail report</returns>
    public static DataTable GetIssuesByClientSummaryTable(bool isDetailTable)
    {
        DataTable table = new DataTable();
        table.Columns.Add("ClientMne", typeof(string));
        table.Columns.Add("RequestedDate", typeof(string));
        table.Columns.Add("CompletedClientIssues", typeof(int));
        if (!isDetailTable)
        {
            table.Columns.Add("ClientAcNo", typeof(string));
            table.Columns.Add("ClientName", typeof(string));
            table.Columns.Add("CXA", typeof(int));
            table.Columns.Add("COG", typeof(int));
            table.Columns.Add("CON", typeof(int));
            table.Columns.Add("CYT", typeof(int));
            table.Columns.Add("GEN", typeof(int));
            table.Columns.Add("HIS", typeof(int));
            table.Columns.Add("OEP", typeof(int));
            table.Columns.Add("SAL", typeof(int));
            table.Columns.Add("LAB", typeof(int));
            table.Columns.Add("TAT", typeof(int));
            table.Columns.Add("TRA", typeof(int));
            table.Columns.Add("SUP", typeof(int));
        }
        if (isDetailTable)
        {
            table.Columns.Add("DetailTable", typeof(DataTable));
        }
        return table;
    }

    /// <summary>
    /// Gets detailed issue report, reported by laboratory
    /// </summary>
    /// <param name="strQueryString">Query string containing the search detail of the issues</param>
    /// <param name="strFromDate">reference variable for from date</param>
    /// <param name="strToDate">reference variable for to date</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable ReportedIssueByLabDetail(string strQueryString, out string strFromDate, out string strToDate)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtIssueByLabSummary = new DataTable();
        DataTable dtIssueByLabDetails = GetIsuuesByLabSummaryTable(true);
        strFromDate = QS[3];
        strToDate = QS[4];

        DataTable dtDetailSchema = new DataTable();
        dtDetailSchema.Columns.Add("Client", typeof(string));
        dtDetailSchema.Columns.Add("Accession", typeof(string));
        dtDetailSchema.Columns.Add("Priority", typeof(string));
        dtDetailSchema.Columns.Add("EnteredDate", typeof(string));
        dtDetailSchema.Columns.Add("ProbCat", typeof(string));
        dtDetailSchema.Columns.Add("Lab", typeof(string));
        dtDetailSchema.Columns.Add("Reason", typeof(string));
        dtDetailSchema.Columns.Add("Status", typeof(string));

        DataTable returnDataTable = DL_ClientIssues.ReportedIssueByLabSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], true);
        DataTable dtCompDetailsTable = DL_ClientIssues.GetReportedIssuesByLabChild(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strCount = returnDataTable.Rows[iRowCount]["TotalIssue"].ToString().Trim();
                int iCount = 0;
                if (strCount.Length > 0)
                {
                    iCount = Convert.ToInt32(strCount);
                }
                string strLab = returnDataTable.Rows[iRowCount]["LabCode"].ToString().Trim();
                string strLabName = returnDataTable.Rows[iRowCount]["LabName"].ToString().Trim();
                if (iCount > 0 && strLab.Length > 0)
                {
                    DataTable dtDetailsTable = GetReportedIssueByLabChildTable(strLab, dtCompDetailsTable, dtDetailSchema.Clone());
                    dtIssueByLabDetails.Rows.Add(strLab, strLabName, strCount, dtDetailsTable);
                }
            }
        }
        dtIssueByLabDetails.DefaultView.Sort = "TotalIssue DESC";
        return dtIssueByLabDetails;
    }

    /// <summary>
    /// Gets the child table for generating "Reported Issue by Laboratory" detailed report
    /// </summary>
    /// <param name="labLocation">Lab Location</param>
    /// <param name="dtCompDetailsTable">Datatable having the results of "GetReportedIssuesByLabChild" method in DL layer</param>
    /// <param name="dtDetailsTable">Datatable clone of dtDetailSchema</param>
    /// <returns>Datatable containing the details of the child table</returns>
    public static DataTable GetReportedIssueByLabChildTable(string labLocation, DataTable dtCompDetailsTable, DataTable dtDetailsTable)
    {
        string client = "";
        string accession = "";
        string priority = "";
        string enterDate = "";
        string probCat = "";
        string lab = "";
        string reason = "";
        string status = "";
        string retReason = string.Empty;
        List<KeyValuePair> allReasonMappings = FetchAllClientIssueReasonUIMapping();
        DataRow[] drReport = dtCompDetailsTable.Select("Lab='" + labLocation + "'", "Lab ASC");

        if (drReport != null && drReport.Length > 0)
        {
            for (int iCnt = 0; iCnt < drReport.Length; iCnt++)
            {
                client = drReport[iCnt]["Client"].ToString().Trim();
                accession = drReport[iCnt]["Accession"].ToString().Trim();
                priority = drReport[iCnt]["Priority"].ToString().Trim();
                enterDate = drReport[iCnt]["EnteredDate"].ToString().Trim();
                probCat = drReport[iCnt]["ProbCat"].ToString().Trim();
                lab = drReport[iCnt]["Lab"].ToString().Trim();
                status = drReport[iCnt]["Status"].ToString().Trim();
                retReason = drReport[iCnt]["Reason"].ToString().Trim();
                var targetStatusObj = allReasonMappings.SingleOrDefault(rea => rea.Key == retReason);
                reason = (targetStatusObj != null) ? targetStatusObj.Value : retReason;
                dtDetailsTable.Rows.Add(client, accession, priority, enterDate, probCat, lab, reason, status);
            }
        }
        return dtDetailsTable;
    }

    public static List<KeyValuePair> FetchAllClientIssueReasonUIMapping()
    {
        List<KeyValuePair> allReasonMappings = new List<KeyValuePair>();
        allReasonMappings.Add(new KeyValuePair() { Key = "CANCELTEST", Value = "CXA" });
        allReasonMappings.Add(new KeyValuePair() { Key = "COGGINSEIAISS", Value = "COG" });
        allReasonMappings.Add(new KeyValuePair() { Key = "CNSLTCOMM", Value = "CON" });
        allReasonMappings.Add(new KeyValuePair() { Key = "GENINQ", Value = "GEN" });
        allReasonMappings.Add(new KeyValuePair() { Key = "CYTOISS", Value = "CYT" });
        allReasonMappings.Add(new KeyValuePair() { Key = "ORDERENTRY", Value = "OEP" });
        allReasonMappings.Add(new KeyValuePair() { Key = "UNREQSUPISS", Value = "SUP" });
        allReasonMappings.Add(new KeyValuePair() { Key = "SALESISS", Value = "SAL" });
        allReasonMappings.Add(new KeyValuePair() { Key = "SPECTRANSISSUE", Value = "LAB" });
        allReasonMappings.Add(new KeyValuePair() { Key = "LABISS", Value = "TRA" });
        allReasonMappings.Add(new KeyValuePair() { Key = "PENDOVERDUEREP", Value = "TAT" });
        allReasonMappings.Add(new KeyValuePair() { Key = "HISTOISS", Value = "HIS" });
        return allReasonMappings;
    }

    #endregion

    /// <summary>
    /// Gets the summary issue report, reported by user agent
    /// </summary>
    /// <param name="strQueryString">Query string containing the search detail of the issues</param>
    /// <param name="arrGrandTotal">reference variable total count of the issues</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable ClientIssueByUserAgentSummary(string strQueryString, out int[] arrGrandTotal)
    {
        DataTable dtIssueByUserAgent = new DataTable();
        String[] QS = strQueryString.Split('^');
        dtIssueByUserAgent = GetClientIssueByUserAgentSummaryTable(false);
        arrGrandTotal = new int[1];
        string strRequested = QS[3] + " to " + QS[4];
        DataTable returnDataTable = DL_ClientIssues.ClientIssueByUserAgentSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], false);
        dtIssueByUserAgent = SummaryReportLogic(returnDataTable, dtIssueByUserAgent, "UserEntered", "TotalCount", "ReasonIssue", strRequested, "userAgent", out arrGrandTotal,false);
        dtIssueByUserAgent.DefaultView.Sort = "TotalIssue DESC";
        return dtIssueByUserAgent;
    }

    /// <summary>
    /// Gets the detail issue report, reported by user agent
    /// </summary>
    /// <param name="strQueryString">Query string containing the search detail of the issues</param>
    /// <param name="strDateRequested">reference variable for the date requested</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable ClientIssueByUserAgentDetails(string strQueryString, out string strDateRequested)
    {
        DataTable dtClientIssueByUserAgent = new DataTable();
        String[] QS = strQueryString.Split('^');
        dtClientIssueByUserAgent = GetClientIssueByUserAgentSummaryTable(true);
        strDateRequested = QS[3] + " to " + QS[4];
        DataTable dtReportSchema = new DataTable();
        dtReportSchema.Columns.Add("Name", typeof(string));
        dtReportSchema.Columns.Add("EnteredDate", typeof(string));
        dtReportSchema.Columns.Add("EnteredTime", typeof(string));
        dtReportSchema.Columns.Add("Reason", typeof(string));
        dtReportSchema.Columns.Add("Accession", typeof(string));
        int intCount = 0;
        DataTable returnDataTable = DL_ClientIssues.ClientIssueByUserAgentSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], true);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            DataTable dtReportChild = new DataTable();
            dtReportChild = DL_ClientIssues.ClientIssueByUserAgentDetails(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12]);
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strUserDR = returnDataTable.Rows[iRowCount]["EnteredBy"].ToString().Trim();
                intCount = Convert.ToInt32(returnDataTable.Rows[iRowCount]["TotalCount"]);
                string strUserName = returnDataTable.Rows[iRowCount]["UserEntered"].ToString().Trim();
                string strRequested = QS[3] + " to " + QS[4];
                if (intCount > 0)
                {
                    DataTable dtDatailsTable = GetClientIssueByUserAgentDetailsTable(strUserDR, dtReportChild, dtReportSchema.Clone());
                    dtClientIssueByUserAgent.Rows.Add(strUserName, strRequested, intCount, dtDatailsTable);
                }
            }
        }
        dtClientIssueByUserAgent.DefaultView.Sort = "TotalIssue DESC";
        return dtClientIssueByUserAgent;
    }

    /// <summary>
    /// Gets the issues from user agent detail table
    /// </summary>
    /// <param name="userDR">User id of the user entered</param>
    /// <param name="dtReportChild">Datatable having the result of "ClientIssueByUserAgentDetails" method in DL layer</param>
    /// <param name="detailTable">Datatable having the clone of dtReportSchema </param>
    /// <returns>Datatable containing the useragent field details</returns>
    public static DataTable GetClientIssueByUserAgentDetailsTable(string userDR, DataTable dtReportChild, DataTable detailTable)
    {
        string name = "";
        string enteredDate = "";
        string enteredTime = "";
        string accession = string.Empty;
        string reason = string.Empty;
        DataRow[] drReport = dtReportChild.Select("UserId='" + userDR + "'", "UserId ASC");
        if (drReport != null && drReport.Length > 0)
        {
            for (int iCnt = 0; iCnt < drReport.Length; iCnt++)
            {
                name = drReport[iCnt]["UserEntered"].ToString().Trim();
                enteredDate = drReport[iCnt]["EnteredDate"].ToString().Trim();
                enteredTime = drReport[iCnt]["EnteredTime"].ToString().Trim();
                accession = drReport[iCnt]["Accession"].ToString().Trim();
                reason = drReport[iCnt]["Reason"].ToString().Trim();
                detailTable.Rows.Add(name, enteredDate, enteredTime, reason, accession);
            }
        }
        return detailTable;
    }

    /// <summary>
    /// Table having fields for "Reported Issue by User/Agent" report
    /// </summary>
    /// <param name="isDetailTable">Flag to determine detail or summry table</param>
    /// <returns>Datatable with the fields for summary and detail report</returns>
    public static DataTable GetClientIssueByUserAgentSummaryTable(Boolean isDetailTable)
    {
        DataTable table = new DataTable();
        table.Columns.Add("UserEntered", typeof(string));
        table.Columns.Add("Requested", typeof(string));
        table.Columns.Add("TotalIssue", typeof(int));
        if (!isDetailTable)
        {
            table.Columns.Add("CXA", typeof(int));
            table.Columns.Add("COG", typeof(int));
            table.Columns.Add("CON", typeof(int));
            table.Columns.Add("CYT", typeof(int));
            table.Columns.Add("GEN", typeof(int));
            table.Columns.Add("HIS", typeof(int));
            table.Columns.Add("OEP", typeof(int));
            table.Columns.Add("SAL", typeof(int));
            table.Columns.Add("LAB", typeof(int));
            table.Columns.Add("TAT", typeof(int));
            table.Columns.Add("TRA", typeof(int));
            table.Columns.Add("SUP", typeof(int));
        }
        if (isDetailTable)
        {
            table.Columns.Add("DetailTable", typeof(DataTable));
        }
        return table;
    }

    /// <summary>
    /// Gets summary issue report, reported by territory
    /// </summary>
    /// <param name="strQueryString">Query string containing the search detail of the issues</param>
    /// <param name="strFromDate">reference variable for from date</param>
    /// <param name="strToDate">reference variable to Date</param>
    /// <param name="iTotalCount">reference variable total count of the issues</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable ReportedIssueByTerritorySummary(string strQueryString, out string strFromDate, out string strToDate, out int[] arrGrandTotal)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtIssueByTerritorySummary = new DataTable();
        dtIssueByTerritorySummary = GetIssuesByTerritorySummaryTable(false);
        strFromDate = QS[3];
        strToDate = QS[4];
        arrGrandTotal = new int[1];
        DataTable returnDataTable = DL_ClientIssues.ReportedIssueByTerritorySummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], false);
        dtIssueByTerritorySummary = SummaryReportLogic(returnDataTable, dtIssueByTerritorySummary, "TerritoryCode", "TotalIssue", "ReasonIssue", "RepName", "territory", out arrGrandTotal,false);
        dtIssueByTerritorySummary.DefaultView.Sort = "TotalIssue DESC";
        return dtIssueByTerritorySummary;
    }

    /// <summary>
    /// Table having fields for the "Reported Issue by Terrirory" report
    /// </summary>
    /// <param name="isDetailTable">Flag to determine detail or summry table</param>
    /// <returns>Datatable with the fields for summary and detail report</returns>
    public static DataTable GetIssuesByTerritorySummaryTable(bool isDetailTable)
    {
        DataTable table = new DataTable();
        table.Columns.Add("TerritoryCode", typeof(string));
        table.Columns.Add("RepName", typeof(string));
        table.Columns.Add("ManagerName", typeof(string));
        table.Columns.Add("TotalIssue", typeof(string));
        if (!isDetailTable)
        {
            table.Columns.Add("CXA", typeof(int));
            table.Columns.Add("COG", typeof(int));
            table.Columns.Add("CON", typeof(int));
            table.Columns.Add("CYT", typeof(int));
            table.Columns.Add("GEN", typeof(int));
            table.Columns.Add("HIS", typeof(int));
            table.Columns.Add("OEP", typeof(int));
            table.Columns.Add("SAL", typeof(int));
            table.Columns.Add("LAB", typeof(int));
            table.Columns.Add("TAT", typeof(int));
            table.Columns.Add("TRA", typeof(int));
            table.Columns.Add("SUP", typeof(int));
        }
        if (isDetailTable)
        {
            table.Columns.Add("RepPhone", typeof(string));
            table.Columns.Add("RepExten", typeof(string));
            table.Columns.Add("RepMail", typeof(string));
            table.Columns.Add("ManagerPhone", typeof(string));
            table.Columns.Add("ManagerExten", typeof(string));
            table.Columns.Add("ManagerMail", typeof(string));
            table.Columns.Add("LastUpdatedDate", typeof(string));
            table.Columns.Add("DetailTable", typeof(DataTable));
        }
        return table;
    }

    /// <summary>
    /// Gets detail issue report, reported by territory
    /// </summary>
    /// <param name="strQueryString">Query string containing the search detail of the issues</param>
    /// <param name="strFromDate">reference variable for from date</param>
    /// <param name="strToDate">reference variable for to date</param>
    /// <returns>Datatable containing the details of the issue for the report</returns>
    public static DataTable GetReportedIssueByTerritoryDetail(string strQueryString, out string strFromDate, out string strToDate)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtIssueByTerritorySummary = new DataTable();
        dtIssueByTerritorySummary = GetIssuesByTerritorySummaryTable(true);

        DataTable dtReportSchema = new DataTable();
        dtReportSchema.Columns.Add("RowId", typeof(string));
        dtReportSchema.Columns.Add("Accession", typeof(string));
        dtReportSchema.Columns.Add("Priority", typeof(string));
        dtReportSchema.Columns.Add("EnteredDate", typeof(string));
        dtReportSchema.Columns.Add("ProbCat", typeof(string));
        dtReportSchema.Columns.Add("Lab", typeof(string));
        dtReportSchema.Columns.Add("Reason", typeof(string));
        dtReportSchema.Columns.Add("Status", typeof(string));

        strFromDate = QS[3];
        strToDate = QS[4];
        int iTotalCount = 0;
        DataTable returnDataTable = DL_ClientIssues.ReportedIssueByTerritorySummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], true);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            DataTable dtReportChild = new DataTable();
            dtReportChild = DL_ClientIssues.GetReportedIssuesByTerritoryChild(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12]);
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strCount = returnDataTable.Rows[iRowCount]["TotalIssue"].ToString().Trim();
                int iCount = 0;
                if (strCount.Length > 0)
                {
                    iCount = Convert.ToInt32(strCount);
                }
                string territoryCode = returnDataTable.Rows[iRowCount]["TerritoryCode"].ToString().Trim();
                string repName = returnDataTable.Rows[iRowCount]["RepName"].ToString().Trim();
                string managName = returnDataTable.Rows[iRowCount]["ManagerName"].ToString().Trim();
                string repPhone = returnDataTable.Rows[iRowCount]["RepPhone"].ToString().Trim();
                string repExten = returnDataTable.Rows[iRowCount]["RepExten"].ToString().Trim();
                string repMail = returnDataTable.Rows[iRowCount]["RepMail"].ToString().Trim();
                string managPhone = returnDataTable.Rows[iRowCount]["ManagerPhone"].ToString().Trim();
                string managExten = returnDataTable.Rows[iRowCount]["ManagerExten"].ToString().Trim();
                string managMail = returnDataTable.Rows[iRowCount]["ManagerMail"].ToString().Trim();
                string lastUpdateDate = returnDataTable.Rows[iRowCount]["LastUpdatedDate"].ToString().Trim();
                iTotalCount += iCount;
                if (iCount > 0 && territoryCode.Length > 0)
                {
                    DataTable dtDatailsTable = GetReportedTerritoryIssueChildTable(territoryCode, dtReportChild, dtReportSchema.Clone());
                    dtIssueByTerritorySummary.Rows.Add(territoryCode, repName, managName, iCount, repPhone, repExten, repMail, managPhone, managExten, managMail, lastUpdateDate, dtDatailsTable);
                }
            }
        }
        dtIssueByTerritorySummary.DefaultView.Sort = "TotalIssue DESC";
        return dtIssueByTerritorySummary;
    }

    /// <summary>
    /// Gets the child table for issue "Reported Issue by Terrirory" detailed report
    /// </summary>
    /// <param name="territoryCode">Territory code</param>
    /// <param name="dtReportChild">Datatable having the results of the "GetReportedIssuesByTerritoryChild" DL method</param>
    ///<param name="reportTable">Datatable clone of dtReportSchema having the detail properties</param>
    /// <returns>Datatable containing the details of the child table</returns>
    public static DataTable GetReportedTerritoryIssueChildTable(string territoryCode, DataTable dtReportChild, DataTable reportTable)
    {
        string rowid = "";
        string accession = "";
        string priority = "";
        string enterDate = "";
        string probCat = "";
        string lab = "";
        string reason = "";
        string status = "";
        string retReason = string.Empty;
        List<KeyValuePair> allReasonMappings = FetchAllClientIssueReasonUIMapping();

        DataRow[] drReport = dtReportChild.Select("TerrCode='" + territoryCode + "'", "TerrCode ASC");
        if (drReport != null && drReport.Length > 0)
        {
            for (int iCnt = 0; iCnt < drReport.Length; iCnt++)
            {
                rowid = drReport[iCnt]["RowId"].ToString().Trim();
                accession = drReport[iCnt]["Accession"].ToString().Trim();
                priority = drReport[iCnt]["Priority"].ToString().Trim();
                enterDate = drReport[iCnt]["EnteredDate"].ToString().Trim();
                probCat = drReport[iCnt]["ProbCat"].ToString().Trim();
                lab = drReport[iCnt]["Lab"].ToString().Trim();
                retReason = drReport[iCnt]["Reason"].ToString().Trim();
                var targetStatusObj = allReasonMappings.SingleOrDefault(rea => rea.Key == retReason);
                reason = (targetStatusObj != null) ? targetStatusObj.Value : retReason;
                status = drReport[iCnt]["Status"].ToString().Trim();
                reportTable.Rows.Add(rowid, accession, priority, enterDate, probCat, lab, reason, status);
            }
        }
        return reportTable;
    }

    /// <summary>
    /// Re-Issues a final client issue
    /// </summary>
    /// <param name="clientIssueRow">RowID of the current client issue</param>
    /// <param name="userName">Current user from session</param>
    /// <returns>Status code</returns>
    public static string ReIssueClientIssue(string clientIssueRow, string userName)
    {
        return DL_ClientIssues.ReIssueClientIssue(clientIssueRow, userName);
    }

    #region Workflow Analysis Reports

    /// <summary>
    /// Method for populating the details required for the workflow analysis report.
    /// </summary>
    /// <param name="lab">Lab name</param>
    /// <param name="dtReportChild">Datatable containing the result of "getLabCWorkflowAnalysisDetail" query</param>
    /// <param name="detailTable">Datatable containing the detail properties</param>
    /// <returns>Datatable with populated detail values</returns>
    public static DataTable GetWorkflowReportDetailTable(string lab, DataTable dtReportChild, DataTable detailTable)
    {
        string clientNum = string.Empty;
        string clientName = string.Empty;
        string accession = string.Empty;
        string enteredDate = string.Empty;
        string enteredTime = string.Empty;
        string enteredBy = string.Empty;
        string routeDate = string.Empty;
        string routeTime = string.Empty;
        string routeBy = string.Empty;
        string claimedDate = string.Empty;
        string claimedTime = string.Empty;
        string claimedBy = string.Empty;
        string finalDate = string.Empty;
        string finalTime = string.Empty;
        string finalizedBy = string.Empty;
        string routedAndClaimedData = string.Empty;
        string[] routedAndClaimedArr;

        DataRow[] drReport = dtReportChild.Select("Lab='" + lab + "'", "lab ASC");
        if (drReport.Length > 0)
        {
            for (int iCnt = 0; iCnt < drReport.Length; iCnt++)
            {
                clientNum = drReport[iCnt]["ClientNum"].ToString().Trim();
                clientName = drReport[iCnt]["ClientName"].ToString().Trim();
                accession = drReport[iCnt]["Accession"].ToString().Trim();

                enteredDate = drReport[iCnt]["EnteredDate"].ToString().Trim();
                enteredTime = drReport[iCnt]["EnteredTime"].ToString().Trim();
                enteredBy = drReport[iCnt]["EnteredBy"].ToString().Trim();

                routedAndClaimedData = drReport[iCnt]["RoutedAndClaimedData"].ToString().Trim();
                routedAndClaimedArr = routedAndClaimedData.Split('^');

                routeDate = routedAndClaimedArr[0].Trim();
                routeTime = routedAndClaimedArr[1].Trim();
                routeBy = routedAndClaimedArr[2].Trim();

                claimedDate = routedAndClaimedArr[3].Trim();
                claimedTime = routedAndClaimedArr[4].Trim();
                claimedBy = routedAndClaimedArr[5].Trim();

                finalDate = drReport[iCnt]["FinalizedDate"].ToString().Trim();
                finalTime = drReport[iCnt]["FinalizedTime"].ToString().Trim();
                finalizedBy = drReport[iCnt]["FinalizedBy"].ToString().Trim();
                detailTable.Rows.Add(clientNum, clientName, accession, lab, enteredDate, enteredTime, enteredBy, finalDate, finalTime, finalizedBy, routeDate, routeTime, routeBy, claimedDate, claimedTime, claimedBy);
            }
        }
        return detailTable;
    }

    /// <summary>
    /// Method for generating the workflow analysis report for a specific assigned to group.
    /// </summary>
    /// <param name="reportGeneratedForGroup">The assigned to group for which the report needs to be generated</param>
    /// <param name="strQueryString">Search parameter values</param>
    /// <param name="strDateRequested">Reference variable for the search date range</param>
    /// <returns>Datatable containing the workflow details to generate the report</returns>
    public static DataTable RetrieveWorkflowAnalysisData(string reportGeneratedForGroup, string strQueryString, out string strDateRequested)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtblReport = new DataTable();
        dtblReport.Columns.Add("Lab", typeof(string));
        dtblReport.Columns.Add("CountIssue", typeof(string));
        dtblReport.Columns.Add("RequestedDate", typeof(string));
        dtblReport.Columns.Add("DetailsTable", typeof(DataTable));

        DataTable dtDetailSchema = new DataTable();
        dtDetailSchema.Columns.Add("ClientNum", typeof(string));
        dtDetailSchema.Columns.Add("ClientName", typeof(string));
        dtDetailSchema.Columns.Add("Accession", typeof(string));
        dtDetailSchema.Columns.Add("Lab", typeof(string));
        dtDetailSchema.Columns.Add("EnteredDate", typeof(string));
        dtDetailSchema.Columns.Add("EnteredTime", typeof(string));
        dtDetailSchema.Columns.Add("EnteredBy", typeof(string));
        dtDetailSchema.Columns.Add("FinalizedDate", typeof(string));
        dtDetailSchema.Columns.Add("FinalizedTime", typeof(string));
        dtDetailSchema.Columns.Add("FinalizedBy", typeof(string));

        dtDetailSchema.Columns.Add("RoutedDate", typeof(string));
        dtDetailSchema.Columns.Add("RoutedTime", typeof(string));
        dtDetailSchema.Columns.Add("RoutedBy", typeof(string));

        dtDetailSchema.Columns.Add("ClaimedDate", typeof(string));
        dtDetailSchema.Columns.Add("ClaimedTime", typeof(string));
        dtDetailSchema.Columns.Add("ClaimedBy", typeof(string));

        strDateRequested = QS[3] + " to " + QS[4];
        int count = 0;

        DataTable returnDataTable = DL_ClientIssues.GetWorkflowAnalysisSummaryData(reportGeneratedForGroup, QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            DataTable dtReportChild = new DataTable();
            dtReportChild = DL_ClientIssues.GetWorkflowAnalysisDetail(reportGeneratedForGroup, QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12]);
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strLab = returnDataTable.Rows[iRowCount]["Lab"].ToString().Trim();
                count = Convert.ToInt32(returnDataTable.Rows[iRowCount]["CountIssue"]);
                string strRequested = QS[3] + " to " + QS[4];
                if (count > 0)
                {
                    DataTable dtDatailsTable = GetWorkflowReportDetailTable(strLab, dtReportChild, dtDetailSchema.Clone());
                    dtblReport.Rows.Add(strLab, count, strRequested, dtDatailsTable);
                }
            }
        }
        return dtblReport;
    }
    #endregion


    }
