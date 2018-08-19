using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for DL_ClientIssues
/// </summary>
public class DL_ClientIssues
{
	public DL_ClientIssues()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    /// <summary>
    /// Query for getting the issue detail for the row id passed
    /// </summary>
    /// <param name="rowId">Issue rowid</param>
    /// <returns>Datatable with client issue details</returns>
    public static DataTable GetClientIssueDetail(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("CLI_RowID AS RowId,");
        sbSQL.Append("CLI_EnteredByUserDR->USER_UserID AS EnteredByUser,");
        sbSQL.Append("CLI_ClientDR AS ClientMnemonic,");
        sbSQL.Append("CLI_AccessionDR As AccessionNumber,");
        sbSQL.Append("%EXTERNAL(CLI_IsPriority) As PriorityIssue,");
        sbSQL.Append("%EXTERNAL(CLI_EnteredDate) As DateEntered,");
        sbSQL.Append("%EXTERNAL(CLI_EnteredTime) As TimeEntered,");
        sbSQL.Append("CLI_ContactPerson AS ContactPerson,");
        sbSQL.Append("CLI_AlternateContact AS AltContactNo,");
        sbSQL.Append("CLI_IssueReasonDR->CIR_ReasonText As Reason,");
        sbSQL.Append("CLI_IssueReasonDR As ReasonID,");
        sbSQL.Append("%EXTERNAL(CLI_AssignedTo) As AssignToGroup,");
        sbSQL.Append("%INTERNAL(CLI_ProcessingStatus) AS ProcessingStatus,");
        sbSQL.Append("CLI_InitialProblemTypeDR As ProbTrckCat,");
        sbSQL.Append("CLI_CheckedOutByUserDR AS CheckOutBy,");
        sbSQL.Append("CLI_LabLocationDR->LABLO_LabName AS LabLocation,");
        sbSQL.Append("CLI_LabLocationDR->LABLO_RowID AS LabLocationId,");
        sbSQL.Append("%EXTERNAL(CLI_EnteredDate) As DateSubmitted,");
        sbSQL.Append("CLI_ProbTrackingLabDR->LABLO_LabName As ProbTrackLab,");
        sbSQL.Append("CLI_ProblemDR->PBT_PSQ As ProbTrckNo,");
        sbSQL.Append("CLI_ProbTrackingLabDR->LABLO_RowID As ProbTrackLabId,");
        sbSQL.Append("CLI_IsReIssued As IsReIssued,");
        sbSQL.Append("CLI_PrevCLIRow As PrevCLIRow,");
        sbSQL.Append("CLI_ProblemDR AS ProblemDR,");
        sbSQL.Append("CLI_ProblemDR->PBT_PSQ AS ProblemId,");
        sbSQL.Append("CLI_PrevCLIRow->CLI_ProblemDR->PBT_PSQ AS ParentProblemId,");
        sbSQL.Append("CLI_PrevCLIRow->CLI_ProblemDR AS ParentProblemDR,");
        sbSQL.Append("CLI_RequiredCallback AS RequiredCallback");
        sbSQL.Append(" FROM CLF_ClientIssue ");
        sbSQL.Append("WHERE CLI_RowID ='");
        sbSQL.Append(rowId);
        sbSQL.Append("'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static DataTable GetAssignedToGroupList(string labID)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("LABLO_AssignedToGroupList As GroupList ");
        sbSQL.Append("from ");
        sbSQL.Append("DIC_LabLocation ");
        sbSQL.Append("where LABLO_RowID ='");
        sbSQL.Append(labID);
        sbSQL.Append("' ");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    /// <summary>
    /// Query for getting the progress issue notes for the rowid passed
    /// </summary>
    /// <param name="rowId">Issue row id</param>
    /// <returns>Datatable with the progress notes of the issue</returns>
    public static DataTable GetProgressNotes(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("%EXTERNAL(CINOT_EnteredDate) AS EnteredDate,");
        sbSQL.Append("%EXTERNAL(CINOT_EnteredTime) AS EnteredTime,");
        sbSQL.Append("$$CO17^XT58(CINOT_EnteredByUserDR->USER_UserID) AS EnteredByUserID,");
        sbSQL.Append("CINOT_NotesText AS ProgressNote ");
        sbSQL.Append("FROM CLF_ClientIssueNotes ");
        sbSQL.Append("WHERE CINOT_CLI_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");
        sbSQL.Append("AND CINOT_NotesText <> '' ");
        sbSQL.Append("ORDER BY CINOT_EnteredDate DESC,CINOT_EnteredTime DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    /// <summary>
    /// Retrieve all client issue callback notes per rowId
    /// </summary>
    /// <param name="rowId"></param>
    /// <returns></returns>
    public static DataTable GetClientIssueCallbackNoteDetails (string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("CICBN_ChildSub AS ROWID, ");
        sbSQL.Append("%EXTERNAL(CICBN_EnteredDate) AS EnteredDate,");
        sbSQL.Append("%EXTERNAL(CICBN_EnteredTime) AS EnteredTime,");
        sbSQL.Append("$$CO17^XT58(CICBN_EnteredByUserDR) AS USERID,");
        sbSQL.Append("CICBN_ContactedPerson AS ContactedPerson,");
        sbSQL.Append("CICBN_NoteTypeDR->CBNT_Description AS NoteType, ");
        sbSQL.Append("CICBN_NotesText AS NoteDetail ");
        sbSQL.Append("FROM CLF_ClientIssueCallbackNotes ");
        sbSQL.Append("WHERE CICBN_CLI_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");
        sbSQL.Append("and CICBN_ChildSub <> 0 ");
        sbSQL.Append("ORDER BY CICBN_EnteredDate DESC,CICBN_EnteredTime DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    /// <summary>
    /// Get contacted persons from clinic when taking callback notes
    /// </summary>
    /// <param name="rowId"></param>
    /// <returns></returns>
    public static string GetCallbackContactedPerson (string rowId)
    {
        string rtnVal = string.Empty;
        Dictionary<String, String> callback = new Dictionary<String, String>();
        callback.Add("RowId", rowId);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        rtnVal = cache.StoredProcedure("?=call SP2_GetClientIssueCallbacks(?)", callback).Value.ToString();
        return !string.IsNullOrEmpty(rtnVal) ? rtnVal.TrimEnd(new char[] { ',' }) : string.Empty;
    }

    /// <summary>
    /// Query for getting the first progress notes for the rowid passed
    /// </summary>
    /// <param name="rowId">Issue row id</param>
    /// <returns>Datatable with the first progress note</returns>
    public static DataTable GetFirstProgressNotes(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("TOP 1 ");
        sbSQL.Append("%EXTERNAL(CINOT_EnteredDate) AS EnteredDate,");
        sbSQL.Append("%EXTERNAL(CINOT_EnteredTime) AS EnteredTime,");
        sbSQL.Append("$$CO17^XT58(CINOT_EnteredByUserDR->USER_UserID) AS EnteredByUserID,");
        sbSQL.Append("CINOT_NotesText AS ProgressNote ");
        sbSQL.Append("FROM CLF_ClientIssueNotes ");
        sbSQL.Append("WHERE CINOT_CLI_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");
        sbSQL.Append("AND CINOT_NotesText <> '' ");
        sbSQL.Append("ORDER BY CINOT_EnteredDate ASC,CINOT_EnteredTime ASC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    /// <summary>
    /// This method calls a cache method for adding a new progress notes
    /// </summary>
    /// <param name="rowID">Issue row id</param>
    /// <param name="progressNote">Progress notes to be added</param>
    /// <param name="user">User id</param>
    /// <param name="addInqNote">Flag to copy the progress notes to the inquiry note</param>
    /// <returns>String shows status of the notes insertion</returns>
    public static string UpdateProgressNote(string rowID, string progressNote, string user, string addInqNote)
    {
        Dictionary<String, String> ClientIssueData = new Dictionary<String, String>();
        ClientIssueData.Add("RowId", rowID);
        ClientIssueData.Add("ProgressNote", progressNote);
        ClientIssueData.Add("User", user);
        ClientIssueData.Add("AddInqNote", addInqNote);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdateClientIssueProgressNote(?,?,?,?)", ClientIssueData).Value.ToString();
    }

    /// <summary>
    /// Populate client issue callback notes
    /// </summary>
    /// <param name="rowId"></param>
    /// <param name="required"></param>
    /// <param name="noteType"></param>
    /// <param name="noteDetails"></param>
    /// <param name="personContacted"></param>
    /// <param name="user"></param>
    /// <returns></returns>
    public static string UpdateCallbackNote (string rowId, bool required, string noteType, string noteDetails, string personContacted, string user)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();

        Dictionary<String, String> callbackNote = new Dictionary<String, String>();
        callbackNote.Add("RowId", rowId);
        callbackNote.Add("NoteType", noteType);
        callbackNote.Add("NoteDetail", noteDetails);
        callbackNote.Add("ContactedPerson", personContacted);
        callbackNote.Add("User", user);
        callbackNote.Add("RequiredCallback", required ? "Y" : "N");
        try
        {
            return cache.StoredProcedure("?=call SP2_UpdateClientIssueCallbackNote(?,?,?,?,?,?)", callbackNote).Value.ToString();
        }
        catch (Exception e)
        {
            throw new Exception(e.Message, e);
        }
    }

    /// <summary>
    /// Retrieve all callback notes per client issue Id
    /// </summary>
    /// <param name="rowId"></param>
    /// <returns></returns>
    public static DataTable GetCallbackNotes (string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT CICBN_CLI_PR AS CID, CICBN_ChildSub As CallbackID, ");
        sbSQL.Append("CICBN_NoteTypeDR->CBNT_Description AS NoteType, CICBN_NotesText AS NoteText, ");
        sbSQL.Append("CICBN_ContactedPerson AS ContactedPerson, %EXTERNAL(CICBN_EnteredDate) AS EnteredDate, ");
        sbSQL.Append("%EXTERNAL(CICBN_EnteredTime) AS EnteredTime, CICBN_EnteredByUserDR AS EnteredUser ");
        sbSQL.Append("FROM CLF_ClientIssueCallbackNotes ");
        sbSQL.Append(string.Format("WHERE CICBN_CLI_PR={0} ORDER BY CICBN_EnteredDate DESC, CICBN_EnteredTime DESC", rowId));

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static DataTable GetClientIssueCallbackNotes (string rowId)
    {
        DataTable returnDataTable = new DataTable();
        Dictionary<String, String> callback = new Dictionary<String, String>();
        callback.Add("RowId", rowId);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataSet("?=call SP2_GetClientIssueCallbacks(?)", callback);
    }

    /// <summary>
    /// This method calls a cache method for inserting a new client issue
    /// </summary>
    /// <param name="accountNo">Account number</param>
    /// <param name="accessionNo">Accession number</param>
    /// <param name="priorityIssue">Priority of the client issue</param>
    /// <param name="contactPerson">Contact person</param>
    /// <param name="altContactNo">Alternate contact number</param>
    /// <param name="status">Status of the issue which is passed as "" initially</param>
    /// <param name="user">User id</param>
    /// <param name="reason">Reasoon for the issue</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="assignToGroup">Assigned to group</param>
    /// <param name="probTrackCat">Problem tracking category</param>
    /// <param name="probTrackCatLab">Problem tracking category lab</param>
    /// <param name="comments">Commment for the issue</param>
    /// <returns>String showing the status of issue insertion</returns>
    public static string InsertClientIssue(string accountNo, string accessionNo, string priorityIssue, string contactPerson, string altContactNo, string status, string user, string reason, string labLocation, string assignToGroup, string probTrackCat, string probTrackCatLab, string comments)
    {
        Dictionary<String, String> ClientIssueData = new Dictionary<String, String>();
        ClientIssueData.Add("AccountNumber", accountNo);
        ClientIssueData.Add("AccessionNumber", accessionNo);
        ClientIssueData.Add("PriorityIssue", priorityIssue);
        ClientIssueData.Add("ContactPerson", contactPerson);
        ClientIssueData.Add("AltContactNo", altContactNo);
        ClientIssueData.Add("Status", status);
        ClientIssueData.Add("User", user);
        ClientIssueData.Add("Reason", reason);
        ClientIssueData.Add("LabLocation", labLocation);
        ClientIssueData.Add("AssignToGroup", assignToGroup);
        ClientIssueData.Add("ProbTrckCat", probTrackCat);
        ClientIssueData.Add("ProbTrckLab", probTrackCatLab);
        ClientIssueData.Add("Comments", comments);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_SaveClientIssue(?,?,?,?,?,?,?,?,?,?,?,?,?)", ClientIssueData).Value.ToString();
    }

    /// <summary>
    /// Query for getting the client issues searched based on the parameters passed
    /// </summary>
    /// <param name="accountNo">Account number</param>
    /// <param name="accessionNo">Accession number</param>
    /// <param name="fromDate">From date</param>
    /// <param name="toDate">To date</param>
    /// <param name="priority">Priority of the issue</param>
    /// <param name="checkedBy">Id of the user checkedout by</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="enteredBy">Issue entered by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the issue search results</returns>
    public static DataTable GetClientIssues(string accountNo, string accessionNo, string fromDate, string toDate, string priority, string checkedBy, string labLocation, string progressStatus, string enteredBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLI_RowID,");
        sb.Append("$$CO17^XT58(CLI_EnteredByUserDR->USER_UserID) AS EnteredBy,");
        sb.Append("%EXTERNAL(CLI_EnteredDate) AS EnteredOnDate,");
        sb.Append("%EXTERNAL(CLI_EnteredTime) AS EnteredOnTime,");
        sb.Append("CLI_ClientDR->CLF_CLNUM AS AccountNo,");
        sb.Append("%EXTERNAL(CLI_AssignedTo) AS AssignedTo,");
        sb.Append("%EXTERNAL(CLI_ProcessingStatus) AS ProcessingStatus,");
        sb.Append("CLI_ProcessingStatus AS StatusCode,");
        sb.Append("CLI_AccessionDR->ACC_Accession AS AccessionNumber,");
        sb.Append("CLI_IssueReasonDR->CIR_ReasonText AS IssueReason, ");
        sb.Append("$$CO17^XT58(CLI_CheckedOutByUserDR) AS CheckOutBy,");
        sb.Append("CLI_LabLocationDR As Location,");
        sb.Append("%EXTERNAL(CLI_IsPriority) AS PriorityFlag,");
        sb.Append("$$CO17^XT58(CLI_ResolvedByUserDR) AS ResolvedByUser,");
        sb.Append("CLI_ClientDR->CLF_CLNAM AS AccountName,");
        sb.Append("CLI_ClientDR->CLF_CLMNE AS AccountMnemonic,");
        sb.Append("CLI_ClientDR->CLF_CLPHN As ClientPhone,");
        sb.Append("CLI_ClientDR->CLF_CLAD1 AS ClientAddress1,");
        sb.Append("CLI_ClientDR->CLF_CLAD2 AS ClientAddress2,");
        sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sb.Append("CLI_ClientDR->CLF_IsHot As ClientIsHot,");
        sb.Append("CLI_ClientDR->CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLI_ClientDR->CLF_IsNew As ClientIsNew,");
        sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sb.Append("CLI_ClientDR->CLF_CLADG AS AutodialGroup,");
        sb.Append("CLI_ClientDR->CLF_CLRTS AS RouteStop,");
        sb.Append("CLI_ProblemDR->PBT_ProbTP AS ProblemType, ");
        sb.Append("CLI_ProblemDR->PBT_PSQ AS ProblemId, ");
        sb.Append("Zoasis_num As Zoasis,");
        sb.Append("$$GETCLISNAPSHOT^XT145(CLI_RowID) AS ClientIssueSnapshot,");
        sb.Append("$$GETREVHIST^XT1(CLI_ClientDR) AS ClientRevenue");
        sb.Append(" FROM ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("CLF_ClientFile Client on CLI_ClientDR = Client.CLF_CLMNE ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sb.Append("WHERE 1=1 ");

        if (accountNo.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNo + "'");
        }

        if (accessionNo.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNo + "'");
        }
        if (fromDate.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + fromDate + "','MM/DD/YYYY')");
        }
        if (toDate.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + toDate + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priority.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priority + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }

        sb.Append(" ORDER BY CLI_EnteredDate DESC,CLI_EnteredTime DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    /// <summary>
    /// This method calls a cache method for checking out an issue for the user passed
    /// </summary>
    /// <param name="rowID">Issue rowid</param>
    /// <param name="accessionNo">Accession number</param>
    /// <param name="user">User id</param>
    /// <returns>String showing the status of issue checkout</returns>
    public static string CheckOut(string rowID, string accessionNo, string user)
    {
        Dictionary<String, String> ClientIssueData = new Dictionary<String, String>();
        ClientIssueData.Add("AccessionNumber", accessionNo);
        ClientIssueData.Add("RowId", rowID);
        ClientIssueData.Add("User", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_CheckOutClientIssue(?,?,?)", ClientIssueData).Value.ToString();
    }

    /// <summary>
    /// This method calls a cache method for unchecking out an issue for the user id passed
    /// </summary>
    /// <param name="rowID">Issue row id</param>
    /// <param name="accessionNo">Accession number</param>
    /// <param name="user">User id</param>
    /// <returns>String showing the status of issue uncheckout</returns>
    public static string UnCheckOut(string rowID, string accessionNo, string user)
    {
        Dictionary<String, String> ClientIssueData = new Dictionary<String, String>();
        ClientIssueData.Add("AccessionNumber", accessionNo);
        ClientIssueData.Add("RowId", rowID);
        ClientIssueData.Add("User", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UnCheckOutClientIssue(?,?,?)", ClientIssueData).Value.ToString();
    }

    /// <summary>
    /// Query for checking wheather any pre-existing issues exists for the client account number passed
    /// </summary>
    /// <param name="clientAccountNo">Account number</param>
    /// <returns>Datatable containing the pre existing issues for the account number</returns>
    public static DataTable VerifyExistingRecords(string clientAccountNo)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT CLI_AccessionDR AS AccessionNumber,CLI_AccessionDR->ACC_PatientName As Owner,");
        sbSQL.Append("CLI_AccessionDR->ACC_PetFirstName As PetFirstName,CLI_IssueReasonDR As Reason,CLI_IssueReasonDR->CIR_ReasonText As ReasonText,");
        sbSQL.Append("%EXTERNAL(CLI_EnteredDate) As EnteredDate");
        sbSQL.Append(" FROM CLF_ClientIssue");
        sbSQL.Append(" WHERE CLI_ClientDR->CLF_CLNUM  ='");
        sbSQL.Append(clientAccountNo);
        sbSQL.Append("' AND CLI_ProcessingStatus <> 'F'");
        sbSQL.Append(" AND CLI_ProcessingStatus <> 'DEL'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    /// <summary>
    /// Calls a cache method which resolves the client issue
    /// </summary>
    /// <param name="RowID">Issue rowid</param>
    /// <param name="Accession">Accession number</param>
    /// <param name="user">User id</param>
    /// <param name="ProblemCat">Problem tracking category</param>
    /// <param name="ResolutionNote">Resolution note to be added</param>
    /// <param name="PersonContact">Person contacted</param>
    /// <param name="AccountNo">Account number</param>
    /// <param name="lab">Lab location</param>
    /// <param name="ProbTrackLab">Problem tracking lab</param>
    /// <param name="ProbTrackId">Problem tracking Id</param>
    /// <returns>String showing the status of issue resolution</returns>
    public static string ResolveClientIssue(string RowID, string Accession, string user, string ProblemCat, string ResolutionNote, string PersonContact, string AccountNo, string lab, string ProbTrackLab, string ProbTrackId, string CallbackNoteType, bool RequiredCallback)
    {
        Dictionary<String, String> ResolveClientIssue = new Dictionary<string, string>();
        ResolveClientIssue.Add("RowId", RowID);
        ResolveClientIssue.Add("AccessionNumber", Accession);
        ResolveClientIssue.Add("User", user);
        ResolveClientIssue.Add("ProbTrckCat", ProblemCat);
        ResolveClientIssue.Add("ResolutionNote", ResolutionNote);
        ResolveClientIssue.Add("PersonContact", PersonContact);
        ResolveClientIssue.Add("AccountNumber", AccountNo);
        ResolveClientIssue.Add("LabLocation", lab);
        ResolveClientIssue.Add("ProbTrackLab", ProbTrackLab);
        ResolveClientIssue.Add("ProbTrackId", ProbTrackId);
        ResolveClientIssue.Add("CallbackNoteType", CallbackNoteType);
        ResolveClientIssue.Add("RequiredCallback", RequiredCallback ? "Y" : "N");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ResolveClientIssue(?,?,?,?,?,?,?,?,?,?,?,?)", ResolveClientIssue).Value.ToString();
    }

    /// <summary>
    /// Query for getting the issue details for report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNo">Account number</param>
    /// <param name="accessionNo">Accession number</param>
    /// <param name="fromDate">From date</param>
    /// <param name="toDate">To datte</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priority">Priority of the issue</param>
    /// <param name="progStatus">Progress status of the issue</param>
    /// <param name="checkedOutBy">Checked out by user id</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    public static DataTable GetClientIssuesReport(string enteredBy, string accountNo, string accessionNo, string fromDate, string toDate, string labLocation, string priority, string progStatus, string checkedOutBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLI_ClientDR->CLF_CLNUM AS ACCOUNT, ");
        sb.Append("CLI_ClientDR->CLF_CLNAM AS AccountName, ");
        sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sb.Append("%EXTERNAL(CLI_IsPriority) AS PriorityFlag,");
        sb.Append("%EXTERNAL(CLI_AssignedTo) AS AssignedTo,");
        sb.Append("%EXTERNAL(CLI_ProcessingStatus) AS ProcessingStatus,");
        sb.Append("CLI_ProcessingStatus AS StatusCode,");
        sb.Append("CLI_AccessionDR->ACC_Accession AS AccessionNumber,");
        sb.Append("CLI_AccessionDR->ACC_OwnerLastName||', '||CLI_AccessionDR->ACC_PetFirstName AS PATIENTFULLNAME,");
        sb.Append("CLI_AccessionDR->ACC_PatientName AS PATIENT,");
        sb.Append("CLI_AccessionDR->ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("%EXTERNAL(CLI_AccessionDR->ACC_MiniLogDate) As ORDERDATE,");
        sb.Append("%EXTERNAL(CLI_AccessionDR->ACC_MiniLogTime) As ORDERTIME,");
        sb.Append("CLI_ClientDR->CLF_CLPHN As ClientPhone,");
        sb.Append("CLI_ContactPerson AS ContactPerson, ");
        sb.Append("CLI_AlternateContact AS AlternateContact, ");
        sb.Append("CLI_LabLocationDR->LABLO_LabName AS LabLocation, ");
        sb.Append("CLI_ProblemDR->PBT_ProbTP AS ProblemType, ");
        sb.Append("CLI_ProblemDR->PBT_PSQ AS ProblemId, ");
        sb.Append("CLI_PersonContactedAtClinic AS PersonContactedAtClinic, ");
        sb.Append("CLI_IssueReasonDR->CIR_ReasonText AS IssueReason, ");
        sb.Append("$$GETPROGRESSNOTE^XT145(CLI_RowID) AS ProgressNote,");
        sb.Append("$$CO17^XT58(CLI_EnteredByUserDR->USER_UserID) AS EnteredBy,");
        sb.Append("%EXTERNAL(CLI_EnteredDate) AS EnteredOnDate,");
        sb.Append("%EXTERNAL(CLI_EnteredTime) AS EnteredOnTime,");
        sb.Append("$$CO17^XT58(CLI_CheckedOutByUserDR) AS CheckOutBy,");
        sb.Append("%EXTERNAL(CLI_ResolvedDate) AS ResolvedOnDate,");
        sb.Append("%EXTERNAL(CLI_ResolvedTime) AS ResolvedOnTime,");
        sb.Append("$$CO17^XT58(CLI_ResolvedByUserDR) AS ResolvedBy");
        sb.Append(" FROM ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("CLF_ClientFile Client on CLI_ClientDR = Client.CLF_CLMNE ");
        sb.Append("LEFT OUTER JOIN zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sb.Append("WHERE 1=1");

        if (accountNo.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNo + "'");
        }

        if (accessionNo.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNo + "'");
        }
        if (fromDate.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + fromDate + "','MM/DD/YYYY')");
        }
        if (toDate.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + toDate + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priority.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priority + "'");
        }
        if (checkedOutBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedOutBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }

        sb.Append(" ORDER BY CLI_EnteredDate DESC,CLI_EnteredTime DESC");
        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(selectStatement);
        return returnDataTable;
    }

    /// <summary>
    /// This method calls a cache method which updates the issue according to the parameter passed.
    /// </summary>
    /// <param name="rowID">Issue rowid</param>
    /// <param name="contactPerson">Contact person</param>
    /// <param name="alternateContNo">Alternate contact number</param>
    /// <param name="priorityValue">Issue priority</param>
    /// <param name="reason">Reason for the issue</param>
    /// <param name="groupValue">Assigned to group</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="user">User</param>
    /// <param name="probTrckCat">Problem tracking category</param>
    /// <param name="probTrckLab">Problem tracking laboratory</param>
    /// <returns>String showing the status of issue updation</returns>
    public static string UpdateClientIssue(string rowID, string contactPerson, string alternateContNo, string priorityValue, string reason, string groupValue, string labLocation, string user, string probTrckCat, string probTrckLab)
    {
        Dictionary<String, String> ClientIssueData = new Dictionary<String, String>();
        ClientIssueData.Add("RowId", rowID);
        ClientIssueData.Add("ContactPerson", contactPerson);
        ClientIssueData.Add("AltContactNo", alternateContNo);
        ClientIssueData.Add("PriorityIssue", priorityValue);
        ClientIssueData.Add("Reason", reason);
        ClientIssueData.Add("AssignToGroup", groupValue);
        ClientIssueData.Add("LabLocation", labLocation);
        ClientIssueData.Add("User", user);
        ClientIssueData.Add("ProbTrckCat", probTrckCat);
        ClientIssueData.Add("ProbTrackLab", probTrckLab);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdateClientIssue(?,?,?,?,?,?,?,?,?,?)", ClientIssueData).Value.ToString();
    }

    /// <summary>
    /// This method calls a cache method which updates the problem tracking category property of an issue
    /// </summary>
    /// <param name="rowID">Issue roow id</param>
    /// <param name="probCat">Problem tracking category to be updated</param>
    /// <param name="user">User</param>
    /// <returns>String showing the status of problem tracking category updation</returns>
    public static string UpdateClientIssueProbCat(string rowID, string probCat, string user)
    {
        Dictionary<String, String> ClientIssueData = new Dictionary<String, String>();
        ClientIssueData.Add("RowId", rowID);
        ClientIssueData.Add("ProbTrckCat", probCat);
        ClientIssueData.Add("User", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdateClientIssueProblemCat(?,?,?)", ClientIssueData).Value.ToString();
    }

    #region Completed Client Issue Reports

    /// <summary>
    /// Query for getting the completed client issue detail information used for "Completed Issues" report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To datte</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <param name="isForDetailReport">Bool value for detailed report</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable getCompletedClientIssueSummaryReport(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory, bool isForDetailReport)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT COUNT(*) AS ClientCount, ");
        sbSQL.Append("CLI_ClientDR AS ClientMne, ");
        sbSQL.Append("CLI_ClientDR->CLF_CLNAM AS ClientName, ");
        sbSQL.Append("CLI_ClientDR->CLF_CLNUM AS ClientAcctNo, ");
        sbSQL.Append("CLI_ResolvedByUserDR->USER_LastFirstName AS ClientFirstName, ");
        if (isForDetailReport)
        {
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode As TerritoryCode, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepName As RepName, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepCellPhone As RepPhone, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepExtension As RepExtension, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepEmail As RepMail, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesManagerName As ManagerName, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesManagerCellPhone As ManagerPhone, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesManagerExtension As ManagerExten, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesManagerEmail As ManagerMail, ");
            sbSQL.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_LastUpdatedDate As LastUpdate, ");
        }
        sbSQL.Append("CLI_IssueReasonDR AS ReasonIssue");
        sbSQL.Append(" FROM CLF_ClientIssue");
        sbSQL.Append(" WHERE 1=1");
        DateTime dtDateFrom = DateTime.Parse(dateFrom).AddMonths(-1);
        if (dtDateFrom.ToString().Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredDate>= TO_DATE('", dtDateFrom.ToShortDateString(), "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (dateFrom.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ResolvedDate>= TO_DATE('", dateFrom, "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ResolvedDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (accessionNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredByUserDR  ='", enteredBy, "'"));
        }
        if (labLocation.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_LabLocationDR ='", labLocation, "'"));
        }
        if (priorityFlag.Length > 0)
        {
            if (priorityFlag.Trim().ToUpper() == "Y")
            {
                sbSQL.Append(" AND  (CLI_IsPriority = 'Y' OR CLI_IsPriority IS NULL)");
            }
            else if (priorityFlag.Trim().ToUpper() == "N")
            {
                sbSQL.Append(" AND CLI_IsPriority = 'N'");
            }
        }
        if (checkedBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_CheckedOutByUserDR ='", checkedBy, "'"));
        }
        if (assignedToGroup.Length > 0)
        {
            sbSQL.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sbSQL.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sbSQL.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sbSQL.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sbSQL.Append(" GROUP BY");
        if (isForDetailReport)
        {
            sbSQL.Append(" CLI_ClientDR->CLF_CLMNE");
        }
        else
        {
            sbSQL.Append(" CLI_IssueReasonDR,CLI_ClientDR->CLF_CLMNE");
        }
        sbSQL.Append(" ORDER BY CLI_ClientDR->CLF_CLMNE");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }

    /// <summary>
    /// Query for getting completed client issue details for generating "Completed Issues" report
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable GetClientSummaryDetails(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT CLI_ClientDR AS ClientMnee,");
        sbSQL.Append("CLI_AccessionDR AS Accession,");
        sbSQL.Append("CLI_IssueReasonDR->CIR_ReasonCode As Reason,");
        sbSQL.Append("%EXTERNAL(CLI_ProcessingStatus) As Status,");
        sbSQL.Append("CLI_EnteredDate AS EnteredDate,");
        sbSQL.Append("%EXTERNAL(CLI_IsPriority) As Priority,");
        sbSQL.Append("CLI_ResolvedDate AS ResolvedDate,");
        sbSQL.Append("CLI_ResolvedTime AS ResolvedTime,");
        sbSQL.Append("CLI_LabLocationDR As Lab,");
        sbSQL.Append("CLI_InitialProblemTypeDR As problemCat,");
        sbSQL.Append("CLI_ClientDR->CLF_CLNAM AS ClientName ");

        sbSQL.Append(" FROM CLF_ClientIssue");
        sbSQL.Append(" WHERE 1=1");

        DateTime dtDateFrom = DateTime.Parse(dateFrom).AddMonths(-1);
        if (dtDateFrom.ToString().Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredDate>= TO_DATE('", dtDateFrom.ToShortDateString(), "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (dateFrom.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ResolvedDate>= TO_DATE('", dateFrom, "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ResolvedDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (accountNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (labLocation.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_LabLocationDR ='", labLocation, "'"));
        }
        if (priorityFlag.Length > 0)
        {
            if (priorityFlag.Trim().ToUpper() == "Y")
            {
                sbSQL.Append(" AND  (CLI_IsPriority = 'Y' OR CLI_IsPriority IS NULL)");
            }
            else if (priorityFlag.Trim().ToUpper() == "N")
            {
                sbSQL.Append(" AND CLI_IsPriority = 'N'");
            }
        }
        if (checkedBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_CheckedOutByUserDR ='", checkedBy, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredByUserDR ='", enteredBy, "'"));
        }
        sbSQL.Append(" ORDER BY CLI_ResolvedDate DESC,CLI_ResolvedTime DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }
    #endregion

    #region Canned Reports

    /// <summary>
    /// Query for getting the client issue detail information reported by client used for "Reported Issue by Client" report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable ReportedIssueByAccountSummary(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory, bool isCalledFromDetailReport)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT CLI_ClientDR AS CLMNE, ");
        sb.Append("CLI_ClientDR->CLF_CLNUM AS CLNUM, CLI_ClientDR->CLF_CLNAM AS CLNAM, ");
        sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode, ");
        sb.Append("CLI_ClientDR->CLF_RSTOP, CLI_ClientDR->CLF_CLPHN, ");
        sb.Append("$$GETREVHIST^XT1(CLI_ClientDR) AS CLIENTREVENUE, ");
        sb.Append("CLI_IssueReasonDR->CIR_ReasonCode As ReasonIssue,");
        sb.Append("COUNT(*) AS CountIssue ");
        sb.Append("FROM ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("WHERE 1=1 ");

        if (accountNumber.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNumber + "'");
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNumber + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priorityFlag.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priorityFlag + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sb.Append("GROUP BY ");
        if (isCalledFromDetailReport)
        {
            sb.Append("CLI_ClientDR ");
        }
        else
        {
            sb.Append("CLI_IssueReasonDR->CIR_ReasonCode,CLI_ClientDR ");
        }
        sb.Append("ORDER BY ");
        sb.Append("CLI_ClientDR->CLF_CLNUM");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    /// <summary>
    /// Query for getting the issues detail by client for the child table which is used in the "Reported Issue by Client" detail report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable GetReportedIssuesByAccountChild(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT CLI_RowID AS RowId, ");
        sb.Append("CLI_ClientDR->CLF_CLNUM As AccountNo, ");
        sb.Append("CLI_AccessionDR As Accession,%EXTERNAL(CLI_IsPriority) As Priority, ");
        sb.Append("CLI_EnteredDate As EnteredDate,CLI_ProblemDR As ProbId,CLI_InitialProblemTypeDR->PTYP_ProblemType As ProbCat, ");
        sb.Append("CLI_LabLocationDR As Lab,CLI_IssueReasonDR As Reason,%EXTERNAL(CLI_ProcessingStatus) As Status ");
        sb.Append(" From ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("WHERE 1=1 ");

        if (accountNumber.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNumber + "'");
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNumber + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priorityFlag.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priorityFlag + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sb.Append("ORDER BY ");
        sb.Append("CLI_RowId");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    /// <summary>
    /// Query for getting the client issue detail information reported by laboratory used for "Reported Issue by Laboratory" report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable ReportedIssueByLabSummary(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory, bool isForDetailReport)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT CLI_LabLocationDR AS LabCode, ");
        sb.Append("CLI_LabLocationDR->LABLO_LabName AS LabName, ");
        sb.Append("CLI_IssueReasonDR As ReasonIssue,COUNT(*) AS TotalIssue ");
        sb.Append("FROM ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("WHERE 1=1 ");

        if (accountNumber.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNumber + "'");
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNumber + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priorityFlag.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priorityFlag + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sb.Append("GROUP BY ");
        if (isForDetailReport)
        {
            sb.Append("CLI_LabLocationDR ");
        }
        else
        {
            sb.Append("CLI_IssueReasonDR,CLI_LabLocationDR ");
        }
        sb.Append("ORDER BY ");
        sb.Append("CLI_LabLocationDR->LABLO_LabName");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    /// <summary>
    /// Query for getting the issues detail by lab for the child table which is used in the "Reported Issue by Laboratory" detail report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable GetReportedIssuesByLabChild(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT CLI_RowID AS RowId,CLI_ClientDR As Client, ");
        sb.Append("CLI_AccessionDR As Accession,%EXTERNAL(CLI_IsPriority) As Priority, ");
        sb.Append("CLI_EnteredDate As EnteredDate,CLI_ProblemDR As ProbId,CLI_InitialProblemTypeDR->PTYP_ProblemType As ProbCat, ");
        sb.Append("CLI_LabLocationDR As Lab,CLI_IssueReasonDR As Reason,%EXTERNAL(CLI_ProcessingStatus) As Status ");
        sb.Append(" From ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("WHERE 1=1 ");

        if (accountNumber.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNumber + "'");
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNumber + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priorityFlag.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priorityFlag + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sb.Append("ORDER BY ");
        sb.Append("CLI_RowId");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }
    #endregion


    /// <summary>
    /// Query for getting the client issue detail information reported by user agent used for "Reported Issue by User/Agent" report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable ClientIssueByUserAgentSummary(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory, bool isCalledFromDetailReport)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append(" CLI_EnteredByUserDR As EnteredBy,");
        sb.Append(" CLI_EnteredByUserDR->USER_LastFirstName AS UserEntered,");
        sb.Append("CLI_IssueReasonDR->CIR_ReasonCode As ReasonIssue,");
        sb.Append(" COUNT(*) AS TotalCount");
        sb.Append(" FROM");
        sb.Append(" CLF_ClientIssue ");
        sb.Append("WHERE 1=1 ");
        if (accountNumber.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNumber + "'");
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNumber + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priorityFlag.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priorityFlag + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sb.Append(" GROUP BY ");
        if (isCalledFromDetailReport)
        {
            sb.Append(" CLI_EnteredByUserDR");
        }
        else
        {
            sb.Append("CLI_IssueReasonDR,CLI_EnteredByUserDR ");
        }

        sb.Append(" ORDER BY ");
        sb.Append("CLI_EnteredByUserDR");
        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataSet returnDS = cache.FillCacheDataSet(selectStatement);
        if (returnDS.Tables.Count > 0)
        {
            returnDataTable = returnDS.Tables[0];
        }
        else
        {
            returnDataTable = null;
        }
        return returnDataTable;
    }

    /// <summary>
    /// Query for getting the issue detailed information for the "Reported Issue by User/Agent" detailed report generation
    /// </summary>
    /// <param name="enteredBy">User id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable ClientIssueByUserAgentDetails(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLI_EnteredByUserDR As UserId, CLI_EnteredByUserDR->USER_LastFirstName AS UserEntered, ");
        sb.Append("CLI_EnteredDate As EnteredDate, CLI_EnteredTime As EnteredTime, CLI_IssueReasonDR->CIR_ReasonText As Reason, CLI_AccessionDR->ACC_Accession As Accession");
        sb.Append(" FROM ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("WHERE 1=1");
        if (accountNumber.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNumber + "'");
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNumber + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priorityFlag.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priorityFlag + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sb.Append(" ORDER BY ");
        sb.Append("CLI_RowID");
        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataSet returnDS = cache.FillCacheDataSet(selectStatement);
        if (returnDS.Tables.Count > 0)
        {
            returnDataTable = returnDS.Tables[0];
        }
        else
        {
            returnDataTable = null;
        }
        return returnDataTable;
    }

    /// <summary>
    /// Query for getting the client issue detail information reported by territory used for "Reported Issue by Terrirory" report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable ReportedIssueByTerritorySummary(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory, bool isCalledFromDetailReport)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode AS TerritoryCode, ");
        sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepName AS RepName, ");
        sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesManagerName As ManagerName, ");
        if (isCalledFromDetailReport)
        {
            sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepCellPhone As RepPhone, ");
            sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepExtension As RepExten, ");
            sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepEmail As RepMail, ");
            sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesManagerCellPhone As ManagerPhone, ");
            sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesManagerExtension As ManagerExten, ");
            sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_SalesManagerEmail As ManagerMail, ");
            sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_LastUpdatedDate As LastUpdatedDate, ");
        }
        sb.Append("CLI_IssueReasonDR->CIR_ReasonCode As ReasonIssue,");
        sb.Append("COUNT(*) AS TotalIssue ");
        sb.Append("FROM ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("WHERE 1=1 ");

        if (accountNumber.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNumber + "'");
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNumber + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priorityFlag.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priorityFlag + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode !='' ");
        sb.Append("GROUP BY ");
        if (isCalledFromDetailReport)
        {
            sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ");
        }
        else
        {
            sb.Append("CLI_IssueReasonDR,CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ");
        }

        sb.Append("ORDER BY ");
        sb.Append("CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    /// <summary>
    /// Query for getting the issues detail by territory for the child table which is used in the "Reported Issue by Terrirory" detailed report generation
    /// </summary>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">Assigned To Group</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the issue details for the report</returns>
    internal static DataTable GetReportedIssuesByTerritoryChild(string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT CLI_RowID AS RowId,CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode AS TerrCode, ");
        sb.Append("CLI_AccessionDR As Accession,%EXTERNAL(CLI_IsPriority) As Priority, ");
        sb.Append("CLI_EnteredDate As EnteredDate,CLI_InitialProblemTypeDR->PTYP_ProblemType As ProbCat, ");
        sb.Append("CLI_LabLocationDR As Lab,CLI_IssueReasonDR As Reason,%EXTERNAL(CLI_ProcessingStatus) As Status ");
        sb.Append(" From ");
        sb.Append("CLF_ClientIssue ");
        sb.Append("WHERE 1=1 ");
        if (accountNumber.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_CLNUM ='" + accountNumber + "'");
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(" AND CLI_AccessionDR ='" + accessionNumber + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND CLI_EnteredDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(" AND CLI_EnteredByUserDR->USER_UserID  ='" + enteredBy + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND CLI_ProcessingStatus ='" + progressStatus + "'");
        }
        if (labLocation.Length > 0)
        {
            sb.Append(" AND CLI_LabLocationDR ='" + labLocation + "'");
        }
        if (priorityFlag.Length > 0)
        {
            sb.Append(" AND CLI_IsPriority ='" + priorityFlag + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND CLI_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (assignedToGroup.Length > 0)
        {
            sb.Append(" AND CLI_AssignedTo ='" + assignedToGroup + "'");
        }
        if (issueReason.Length > 0)
        {
            sb.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sb.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode !='' ");
        sb.Append("ORDER BY ");
        sb.Append("CLI_RowId");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    /// <summary>
    /// Re-Issues a final client issue
    /// </summary>
    /// <param name="clientIssueRow">RowID of the current client issue</param>
    /// <param name="userName">Current user from session</param>
    /// <returns>Status code</returns>
    public static string ReIssueClientIssue(string clientIssueRow, string userName)
    {
        Dictionary<String, String> ClientIssueData = new Dictionary<String, String>();
        ClientIssueData.Add("cliRow", clientIssueRow);
        ClientIssueData.Add("userName", userName);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ReIssueClientIssue(?,?)", ClientIssueData).Value.ToString();
    }

    #region Workflow Analysis Reports

    /// <summary>
    /// Query for getting the summary view for the workflow analysis report for a specific assigned to group.
    /// </summary>
    /// <param name="reportGeneratedForGroup">The assigned to group for which the report needs to be generated</param>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">LabC hard coded value</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the workflow of the issues that were assigned to lab c for the report</returns>
    internal static DataTable GetWorkflowAnalysisSummaryData(string reportGeneratedForGroup, string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT ");
        sbSQL.Append("DISTINCT(CLI_LabLocationDR) AS Lab");
        sbSQL.Append(",COUNT(1) As CountIssue");
        sbSQL.Append(" FROM CLF_ClientIssue");
        sbSQL.Append(" WHERE 1=1");
        DateTime dtDateFrom = DateTime.Parse(dateFrom).AddMonths(-1);
        if (dtDateFrom.ToString().Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredDate>= TO_DATE('", dtDateFrom.ToShortDateString(), "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (dateFrom.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ResolvedDate>= TO_DATE('", dateFrom, "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ResolvedDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (accessionNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredByUserDR  ='", enteredBy, "'"));
        }
        if (labLocation.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_LabLocationDR ='", labLocation, "'"));
        }
        if (priorityFlag.Length > 0)
        {
            if (priorityFlag.Trim().ToUpper() == "Y")
            {
                sbSQL.Append(" AND  (CLI_IsPriority = 'Y' OR CLI_IsPriority IS NULL)");
            }
            else if (priorityFlag.Trim().ToUpper() == "N")
            {
                sbSQL.Append(" AND CLI_IsPriority = 'N'");
            }
        }
        if (checkedBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_CheckedOutByUserDR ='", checkedBy, "'"));
        }

        sbSQL.Append(" AND CLI_AssignedTo ='" + reportGeneratedForGroup + "'");
        if (issueReason.Length > 0)
        {
            sbSQL.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sbSQL.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sbSQL.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sbSQL.Append(" GROUP BY CLI_LabLocationDR");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }

    /// <summary>
    /// Query for getting the detail view for the workflow analysis report for a specific assigned to group.
    /// </summary>
    /// <param name="reportGeneratedForGroup">The assigned to group for which the report needs to be generated</param>
    /// <param name="enteredBy">Entered by user id</param>
    /// <param name="accountNumber">Account number</param>
    /// <param name="accessionNumber">Accession number</param>
    /// <param name="dateFrom">From date</param>
    /// <param name="dateTo">To date</param>
    /// <param name="labLocation">Lab location</param>
    /// <param name="priorityFlag">Priority of the issue</param>
    /// <param name="progressStatus">Progress status of the issue</param>
    /// <param name="checkedBy">Checked out by user id</param>
    /// <param name="assignedToGroup">LabC hard coded value</param>
    /// <param name="issueReason">The Client Issue Reason</param>
    /// <param name="finalizedBy">Resolved By User Id</param>
    /// <param name="salesTerritory">The Sales Territory associated with the client account.</param>
    /// <returns>Datatable containing the workflow of the issues that were assigned to lab c for the report in detail</returns>
    internal static DataTable GetWorkflowAnalysisDetail(string reportGeneratedForGroup, string enteredBy, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string labLocation, string priorityFlag, string progressStatus, string checkedBy, string assignedToGroup, string issueReason, string finalizedBy, string salesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT CLI_ClientDR->CLF_CLNUM As ClientNum");
        sbSQL.Append(",CLI_ClientDR->CLF_CLNAM As ClientName");
        sbSQL.Append(",CLI_AccessionDR As Accession");
        sbSQL.Append(",CLI_LabLocationDR As Lab");
        sbSQL.Append(",CLI_EnteredDate As EnteredDate");
        sbSQL.Append(",CLI_EnteredTime As EnteredTime");
        sbSQL.Append(",CLI_EnteredByUserDR->USER_DisplayUserID As EnteredBy");
        sbSQL.Append(",CLI_ResolvedDate As FinalizedDate");
        sbSQL.Append(",CLI_ResolvedTime As FinalizedTime");
        sbSQL.Append(",CLI_ResolvedByUserDR->USER_DisplayUserID As FinalizedBy");
        sbSQL.Append(",$$GETROUTEDANDCLAIMEDDATA^XT145(CLI_RowID) AS RoutedAndClaimedData");
        sbSQL.Append(" FROM CLF_ClientIssue ISSUE");
        sbSQL.Append(" WHERE 1=1");
        DateTime dtDateFrom = DateTime.Parse(dateFrom).AddMonths(-1);
        if (dtDateFrom.ToString().Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredDate>= TO_DATE('", dtDateFrom.ToShortDateString(), "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (dateFrom.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ResolvedDate>= TO_DATE('", dateFrom, "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ResolvedDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (accessionNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_EnteredByUserDR  ='", enteredBy, "'"));
        }
        if (labLocation.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_LabLocationDR ='", labLocation, "'"));
        }
        if (priorityFlag.Length > 0)
        {
            if (priorityFlag.Trim().ToUpper() == "Y")
            {
                sbSQL.Append(" AND  (CLI_IsPriority = 'Y' OR CLI_IsPriority IS NULL)");
            }
            else if (priorityFlag.Trim().ToUpper() == "N")
            {
                sbSQL.Append(" AND CLI_IsPriority = 'N'");
            }
        }
        if (checkedBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CLI_CheckedOutByUserDR ='", checkedBy, "'"));
        }
        sbSQL.Append(" AND CLI_AssignedTo ='" + reportGeneratedForGroup + "'");
        if (issueReason.Length > 0)
        {
            sbSQL.Append(" AND CLI_IssueReasonDR->CIR_ReasonCode ='" + issueReason + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sbSQL.Append(" AND CLI_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        if (salesTerritory.Length > 0)
        {
            sbSQL.Append(" AND CLI_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode ='" + salesTerritory + "'");
        }
        sbSQL.Append(" ORDER BY CLI_RowId ");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }
    #endregion

    }
