using System;
using System.Data;
using System.Configuration;
using System.Text;
/// <summary>
/// Summary description for ConsultRecorder
/// </summary>
public class ConsultRecorder
{
    #region Properties
    public string RowId
    {
        get;
        set;
    }
    public string UnrelatedAccessions
    {
        get;
        set;
    }
    public string ContactPerson
    {
        get;
        set;
    }

    public string ContactDetails
    {
        get;
        set;
    }
    //public string CallbackDate
    //{
    //    get;
    //    set;
    //}
    //public string CallbackTime
    //{
    //    get;
    //    set;
    //}
    public string Specialty
    {
        get;
        set;
    }
    public string RequestedConsultant
    {
        get;
        set;
    }
    public string Priority
    {
        get;
        set;
    }
    public string Status
    {
        get;
        set;
    }
    public string StatusCode
    {
        get;
        set;
    }
    public string Accession
    {
        get;
        set;
    }
    public string CheckOutBy
    {
        get;
        set;
    }

    public string TestString
    {
        get;
        set;
    }

    public string ConsultReason
    {
        get;
        set;
    }

    public string EarliestCallbackDate
    {
        get;
        set;
    }

    public string EarliestCallbackTime
    {
        get;
        set;
    }

    public string LastestCallbackDate
    {
        get;
        set;
    }

    public string LatestCallbackTime
    {
        get;
        set;
    }

    public string AdditionalCallbackInst
    {
        get;
        set;
    }

    public string CallbackTimeZone
    {
        get;
        set;
    }

    public string RequestedMethod
    {
        get;
        set;
    }

    public string PersonContactedAtClinic
    {
        get;
        set;
    }
    private bool isInvalidConsultNo = false;
    public bool IsInvalidConsultNo
    {
        get
        {
            return this.isInvalidConsultNo;
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
            if (this.StatusCode.Equals("PROG") && this.CheckOutBy == SessionHelper.UserContext.ID)
            {
                return true;
            }
            return false;
        }
    }

    /// <summary>
    /// This property is added for Enabling the Consult communication button after the Final status
    /// Previous functionalities are same.      //SG 11/30/2015 Requirement: CS communication box to remain active after resulting consult.
    /// </summary>
    public bool IsConsultCommunicationEnabled
    {
        get
        {
            bool isConsultCommunicationEnabled = false;
            if (this.StatusCode.Equals("PROG") && this.CheckOutBy == SessionHelper.UserContext.ID)
            {
                isConsultCommunicationEnabled = true;
            }
            else if (this.StatusCode.Equals("F"))
            {
                isConsultCommunicationEnabled = true;
            }
            return isConsultCommunicationEnabled;
        }
    }

    public bool IsDeletable
    {
        get
        {
            if (this.StatusCode.Equals("PEND") || (this.StatusCode.Equals("PROG") && this.CheckOutBy == SessionHelper.UserContext.ID))
            {
                return true;
            }
            return false;
        }
    }
    public string Account
    {
        get;
        set;
    }
    public string PetFirstName
    {
        get;
        set;
    }
    public string OwnerNameLastName
    {
        get;
        set;
    }
    public DateTime? AccessionOrderDateTime
    {
        get;
        set;
    }
    /// <summary>
    /// TAT note reference
    /// </summary>
    public string TATNoteRef
    {
        get;
        set;
    }

    /// <summary>
    /// TAT note value
    /// </summary>
    public string TATNoteValue
    {
        get;
        set;
    }
    #endregion Properties

    public ConsultRecorder(string rowId, string setReadFlag)
    {
        if (!string.IsNullOrEmpty(rowId))
        {
            getConsultDetails(rowId, setReadFlag);
        }
    }
    public string RequestedConsultantDR
    {
        get;
        set;
    }

    public string IsSecondRequest
    {
        get;
        set;
    }

    public string SpecialtyDR
    {
        get;
        set;
    }

    public void getConsultDetails(string rowId, string setReadFlag)
    {
        DataTable dtConsultDetails = DL_ConsultRecorder.getConsultationDetails(rowId, setReadFlag);
        if (dtConsultDetails != null && dtConsultDetails.Rows.Count > 0)
        {
            this.RowId = dtConsultDetails.Rows[0]["RowId"].ToString();
            this.ContactPerson = dtConsultDetails.Rows[0]["CNSLT_ContactPerson"].ToString();
            this.ContactDetails = dtConsultDetails.Rows[0]["CNSLT_ContactDetails"].ToString();
            //this.CallbackDate = dtConsultDetails.Rows[0]["CallbackDate"].ToString();
            //this.CallbackTime = dtConsultDetails.Rows[0]["CallbackTime"].ToString();
            this.Specialty = dtConsultDetails.Rows[0]["Specialty"].ToString();
            this.SpecialtyDR = dtConsultDetails.Rows[0]["CNSLT_SpecialtyDR"].ToString();
            this.RequestedConsultant = dtConsultDetails.Rows[0]["RequestedConsultant"].ToString();
            this.Priority = dtConsultDetails.Rows[0]["Priority"].ToString();
            this.Status = dtConsultDetails.Rows[0]["Status"].ToString();
            this.StatusCode = dtConsultDetails.Rows[0]["StatusCode"].ToString();
            this.Accession = dtConsultDetails.Rows[0]["ACCESSION"].ToString();
            this.CheckOutBy = dtConsultDetails.Rows[0]["CNSLT_ProcessedByUserDR"].ToString();
            //+SSM 11/21/2011 #117226 Fetching Accession Pet details
            this.Account = dtConsultDetails.Rows[0]["ACCOUNT"].ToString();
            this.PetFirstName = dtConsultDetails.Rows[0]["PETNAME"].ToString();
            this.OwnerNameLastName = dtConsultDetails.Rows[0]["OWNERNAME"].ToString();
            this.AccessionOrderDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dtConsultDetails.Rows[0]["ORDERDATE"].ToString(), dtConsultDetails.Rows[0]["ORDERTIME"].ToString());
            //-SSM
            this.UnrelatedAccessions = dtConsultDetails.Rows[0]["CNSLT_UnrelatedAccessions"].ToString();

            this.TestString = dtConsultDetails.Rows[0]["TestString"].ToString();
            this.ConsultReason = dtConsultDetails.Rows[0]["ConsultReason"].ToString();
            this.EarliestCallbackDate = dtConsultDetails.Rows[0]["CNSLT_EarliestCallbackDate"].ToString();
            this.EarliestCallbackTime = dtConsultDetails.Rows[0]["CNSLT_EarliestCallbackTime"].ToString();
            this.LastestCallbackDate = dtConsultDetails.Rows[0]["CNSLT_LastestCallbackDate"].ToString();
            this.LatestCallbackTime = dtConsultDetails.Rows[0]["CNSLT_LatestCallbackTime"].ToString();
            this.AdditionalCallbackInst = dtConsultDetails.Rows[0]["CNSLT_AdditionalCallbackInst"].ToString();
            this.CallbackTimeZone = dtConsultDetails.Rows[0]["CNSLT_CallbackTimeZone"].ToString();
            this.RequestedMethod = dtConsultDetails.Rows[0]["CNSLT_RequestedMethod"].ToString();
            this.PersonContactedAtClinic = dtConsultDetails.Rows[0]["CNSLT_PersonContactedAtClinic"].ToString();
            this.RequestedConsultantDR = dtConsultDetails.Rows[0]["CNSLT_RequestedConsultantDR"].ToString();
            this.IsSecondRequest = dtConsultDetails.Rows[0]["CNSLT_IsSecondRequest"].ToString();
            this.TATNoteRef = dtConsultDetails.Rows[0]["CNSLT_TATNoteDR"].ToString();
            this.TATNoteValue = dtConsultDetails.Rows[0]["CNOTE_NotesText"].ToString();
        }
        else
        {
            this.isInvalidConsultNo = true;
        }
    }

    /// <summary>
    /// Retrieves the consult communication TAT Note
    /// </summary>
    /// <param name="rowId">The consult Row ID</param>
    /// <returns>A DataTable containing the TAT Note Details</returns>
    public static DataTable GetConsultationCommunicationNote(string rowId)
    {
        return DL_ConsultRecorder.GetConsultationCommunicationNote(rowId);
    }

    public static DataTable ConsultSendToList()
    {
        DataTable returnDataTable = new DataTable();
        returnDataTable = DL_ConsultRecorder.ConsultSendToList();
        return returnDataTable;
    }

    public static DataTable GetRelatedAccession(string strRelatedAccession)
    {
        DataTable returnDataTable = new DataTable();
        returnDataTable = DL_ConsultRecorder.GetRelatedAccession(strRelatedAccession);
        return returnDataTable;
    }
    public static DataTable GetProgressNotes(string rowId)
    {
        return DL_ConsultRecorder.GetProgressNotes(rowId);
    }

    public DataTable BindAddedAccessions(string rowId)
    {
        return DL_ConsultRecorder.GetAddedAccessions(rowId);
    }

    public static DataTable getConsultant(string SpecCode,bool IsTransferConsult)
    {
        DataTable dtResult = DL_ConsultRecorder.getConsultant(SpecCode, IsTransferConsult);

        for (int intRowCnt = 0; intRowCnt < dtResult.Rows.Count; intRowCnt++)
        {
            dtResult.Rows[intRowCnt]["CONSDisplayName"] = dtResult.Rows[intRowCnt]["CONSDisplayName"].ToString() + " (" + dtResult.Rows[intRowCnt]["CONSLocation"].ToString() + ")";
        }
        return dtResult;
    }
    public static bool isExternalConsultant(string consultRow)
    {
        bool blRetVal = false;
        DataTable dtblResult= DL_ConsultRecorder.isExternalConsultant(consultRow);

        if (dtblResult != null && dtblResult.Rows.Count > 0)
        {
            if (dtblResult.Rows[0]["ConsultLocation"].ToString().Equals("O"))
            {
                blRetVal = true;
            }
        }
        return blRetVal;
    }
    //+SSM 11/18/2011 AntechCSM 2a2 #Issue117055 Load all Consultants
    public static DataTable getAllRequestedConsultants()
    {
        return DL_ConsultRecorder.getAllRequestedConsultants();
    }
    //-SSM

    public static DataTable getReasonsForConsult()
    {
        return DL_ConsultRecorder.getReasonsForConsult();
    }

    public static DataTable verifyExistingRecords(string clientAccountNo)
    {
        string strDateTo=DateTime.Now.ToString("MM/dd/yyyy");
        string strDateFrom = DateTime.Now.AddDays(-7).ToString("MM/dd/yyyy");
        string strStatus="F";

        return DL_ConsultRecorder.verifyExistingRecords(clientAccountNo, strDateTo, strDateFrom, strStatus);
    }
    public static DataTable getTimeZones()
    {
        DataTable dtblTimeZone = new DataTable();
        dtblTimeZone.Columns.Add("TimeZoneID");
        dtblTimeZone.Columns.Add("TimeZoneName");

        DataRow drowNewRow2 = dtblTimeZone.NewRow();
        drowNewRow2["TimeZoneID"] = "PST";
        drowNewRow2["TimeZoneName"] = "PST";
        dtblTimeZone.Rows.Add(drowNewRow2);

        DataRow drowNewRow3 = dtblTimeZone.NewRow();
        drowNewRow3["TimeZoneID"] = "MST";
        drowNewRow3["TimeZoneName"] = "MST";
        dtblTimeZone.Rows.Add(drowNewRow3);

        DataRow drowNewRow4 = dtblTimeZone.NewRow();
        drowNewRow4["TimeZoneID"] = "CST";
        drowNewRow4["TimeZoneName"] = "CST";
        dtblTimeZone.Rows.Add(drowNewRow4);

        DataRow drowNewRow5 = dtblTimeZone.NewRow();
        drowNewRow5["TimeZoneID"] = "EST";
        drowNewRow5["TimeZoneName"] = "EST";
        dtblTimeZone.Rows.Add(drowNewRow5);

        return dtblTimeZone;
    }

    public static DataTable getConsultantForAreaOfInterest(string areaOfInterest)
    {
        DataTable dtAreaOfInterest = DL_ConsultRecorder.getConsultantForAreaOfInterest(areaOfInterest);
        for (int i = 0; i < dtAreaOfInterest.Rows.Count; i++)
        {
            if (dtAreaOfInterest.Rows[i]["Specialty"].ToString().Contains(","))
            {
                String[] arrSpeciality = dtAreaOfInterest.Rows[i]["Specialty"].ToString().Split(',');
                for (int j = 0; j < arrSpeciality.Length; j++)
                {
                    DataRow dr = dtAreaOfInterest.NewRow();
                    dr[0] = dtAreaOfInterest.Rows[i][0].ToString();
                    dr[1] = arrSpeciality[j].TrimStart();
                    dr[2] = dtAreaOfInterest.Rows[i][2].ToString();
                    dtAreaOfInterest.Rows.InsertAt(dr,(i + j + 1));
                }
                dtAreaOfInterest.Rows.RemoveAt(i);
            }
        }
        dtAreaOfInterest.AcceptChanges();
        return dtAreaOfInterest;
    }

    public static DataTable getCompletedConsultSummaryReport(string strQueryString, out int totalViaQueue, out int totalViaTransfer, out int totalViaCnslt, out string dateRequested)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtblRawData = DL_ConsultRecorder.getCompletedConsultSummaryReport(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);

        DataTable dtblReport = new DataTable();

        dtblReport.Columns.Add("ConsultantName", typeof(string));
        dtblReport.Columns.Add("RequestedDate", typeof(string));
        dtblReport.Columns.Add("CompletedCnslts", typeof(int));
        dtblReport.Columns.Add("ReceivedViaQueue", typeof(int));
        dtblReport.Columns.Add("ReceivedViaTransfer", typeof(int));
        dtblReport.Columns.Add("ReceivedViaConsult", typeof(int));

        totalViaQueue = 0;
        totalViaTransfer = 0;
        totalViaCnslt = 0;
        dateRequested = QS[3]+" to "+ QS[4];

        string strOldCnsltRow = string.Empty;
        string strCnsltntName = string.Empty;
        string strCnsltntRowID = string.Empty;

        int intViaQueue = 0;
        int intViaTransfer = 0;
        int intViaCnslt = 0;

        for (int intCnt = 0; intCnt < dtblRawData.Rows.Count; intCnt++)
        {
            string strCnsltRow = dtblRawData.Rows[intCnt]["ResolvedConsultantDR"].ToString();

            if (strCnsltRow.Length == 0)
            {
                continue;
            }
            string strCnsltType = dtblRawData.Rows[intCnt]["RequestedMethod"].ToString();
            if (strCnsltRow.Equals(strOldCnsltRow) || strOldCnsltRow.Length == 0)
            {
                strCnsltntName = dtblRawData.Rows[intCnt]["ConsultantName"].ToString();
                strCnsltntRowID = dtblRawData.Rows[intCnt]["CNSLT_ResolvedByConsultantDR"].ToString();
                switch (strCnsltType)
                {
                    case "SC":
                        intViaCnslt += Convert.ToInt32(dtblRawData.Rows[intCnt]["CnsltCount"]);
                        break;
                    case "DC":
                        intViaQueue += Convert.ToInt32(dtblRawData.Rows[intCnt]["CnsltCount"]);
                        break;
                    case "TC":
                        intViaTransfer += Convert.ToInt32(dtblRawData.Rows[intCnt]["CnsltCount"]);
                        break;
                }
            }
            else
            {
                int iTotalCount = intViaQueue + intViaCnslt + intViaTransfer;
                if (iTotalCount > 0)
                {
                    DataRow drowNewRow = dtblReport.NewRow();
                    drowNewRow["ConsultantName"] = strCnsltntName;
                    drowNewRow["RequestedDate"] = QS[3] + " to " + QS[4];
                    drowNewRow["CompletedCnslts"] = iTotalCount;
                    drowNewRow["ReceivedViaQueue"] = intViaQueue;
                    drowNewRow["ReceivedViaTransfer"] = intViaTransfer;
                    drowNewRow["ReceivedViaConsult"] = intViaCnslt;
                    dtblReport.Rows.Add(drowNewRow);
                }

                totalViaQueue += intViaQueue;
                totalViaTransfer += intViaTransfer;
                totalViaCnslt += intViaCnslt;

                intViaQueue = 0; intViaTransfer = 0; intViaCnslt = 0;
                intCnt--;
            }
            strOldCnsltRow = strCnsltRow;
        }

        int iLastTotalCount = intViaQueue + intViaCnslt + intViaTransfer;
        if (iLastTotalCount > 0)
        {
            DataRow drowLastRow = dtblReport.NewRow();
            drowLastRow["ConsultantName"] = strCnsltntName;
            drowLastRow["RequestedDate"] = QS[3] + " to " + QS[4];
            drowLastRow["CompletedCnslts"] = iLastTotalCount;
            drowLastRow["ReceivedViaQueue"] = intViaQueue;
            drowLastRow["ReceivedViaTransfer"] = intViaTransfer;
            drowLastRow["ReceivedViaConsult"] = intViaCnslt;
            dtblReport.Rows.Add(drowLastRow);
        }

        totalViaQueue += intViaQueue;
        totalViaTransfer += intViaTransfer;
        totalViaCnslt += intViaCnslt;

        return dtblReport;
    }

    public static DataTable getCompletedConsultDetailReport(string strQueryString, out int totalViaQueue, out int totalViaTransfer, out int totalViaCnslt, out string dateRequested)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtblRawData = DL_ConsultRecorder.getCompletedConsultSummaryReport(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);

        DataTable dtblReport = new DataTable();

        dtblReport.Columns.Add("ConsultantName", typeof(string));
        dtblReport.Columns.Add("RequestedDate", typeof(string));
        dtblReport.Columns.Add("CompletedCnslts", typeof(int));
        dtblReport.Columns.Add("ReceivedViaQueue", typeof(int));
        dtblReport.Columns.Add("ReceivedViaTransfer", typeof(int));
        dtblReport.Columns.Add("ReceivedViaConsult", typeof(int));
        dtblReport.Columns.Add("DetailsTable", typeof(DataTable));

        totalViaQueue = 0;
        totalViaTransfer = 0;
        totalViaCnslt = 0;
        dateRequested = QS[3] + " to " + QS[4];

        string strOldCnsltRow = string.Empty;
        string strCnsltntName = string.Empty;
        string strCnsltntRowID = string.Empty;

        int intViaQueue = 0;
        int intViaTransfer = 0;
        int intViaCnslt = 0;

        for (int intCnt = 0; intCnt < dtblRawData.Rows.Count; intCnt++)
        {
            string strCnsltRow = dtblRawData.Rows[intCnt]["ResolvedConsultantDR"].ToString();

            if (strCnsltRow.Length == 0)
            {
                continue;
            }
            string strCnsltType = dtblRawData.Rows[intCnt]["RequestedMethod"].ToString();
            if (strCnsltRow.Equals(strOldCnsltRow) || strOldCnsltRow.Length == 0)
            {
                strCnsltntName = dtblRawData.Rows[intCnt]["ConsultantName"].ToString();
                strCnsltntRowID = dtblRawData.Rows[intCnt]["CNSLT_ResolvedByConsultantDR"].ToString();
                switch (strCnsltType)
                {
                    case "SC":
                        intViaCnslt += Convert.ToInt32(dtblRawData.Rows[intCnt]["CnsltCount"]);
                        break;
                    case "DC":
                        intViaQueue += Convert.ToInt32(dtblRawData.Rows[intCnt]["CnsltCount"]);
                        break;
                    case "TC":
                        intViaTransfer += Convert.ToInt32(dtblRawData.Rows[intCnt]["CnsltCount"]);
                        break;
                }
            }
            else
            {
                int iTotalCount = intViaQueue + intViaCnslt + intViaTransfer;
                if (iTotalCount > 0)
                {
                    DataTable dtConsultantDetails = GetPhysicianConsultSummaryDetails(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], strCnsltntRowID, QS[9], QS[10], QS[11], QS[12], QS[13]);
                    DataRow drowNewRow = dtblReport.NewRow();
                    drowNewRow["ConsultantName"] = strCnsltntName;
                    drowNewRow["RequestedDate"] = QS[3] + " to " + QS[4];
                    drowNewRow["CompletedCnslts"] = iTotalCount;
                    drowNewRow["ReceivedViaQueue"] = intViaQueue;
                    drowNewRow["ReceivedViaTransfer"] = intViaTransfer;
                    drowNewRow["ReceivedViaConsult"] = intViaCnslt;
                    drowNewRow["DetailsTable"] = dtConsultantDetails;
                    dtblReport.Rows.Add(drowNewRow);
                }

                totalViaQueue += intViaQueue;
                totalViaTransfer += intViaTransfer;
                totalViaCnslt += intViaCnslt;

                intViaQueue = 0; intViaTransfer = 0; intViaCnslt = 0;
                intCnt--;
            }
            strOldCnsltRow = strCnsltRow;
        }

        int iLastTotalCount = intViaQueue + intViaCnslt + intViaTransfer;
        if (iLastTotalCount > 0)
        {
            DataTable dtLastConsultantDetails = GetPhysicianConsultSummaryDetails(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], strCnsltntRowID, QS[9], QS[10], QS[11], QS[12], QS[13]);
            DataRow drowLastRow = dtblReport.NewRow();
            drowLastRow["ConsultantName"] = strCnsltntName;
            drowLastRow["RequestedDate"] = QS[3] + " to " + QS[4];
            drowLastRow["CompletedCnslts"] = iLastTotalCount;
            drowLastRow["ReceivedViaQueue"] = intViaQueue;
            drowLastRow["ReceivedViaTransfer"] = intViaTransfer;
            drowLastRow["ReceivedViaConsult"] = intViaCnslt;
            drowLastRow["DetailsTable"] = dtLastConsultantDetails;
            dtblReport.Rows.Add(drowLastRow);
        }

        totalViaQueue += intViaQueue;
        totalViaTransfer += intViaTransfer;
        totalViaCnslt += intViaCnslt;

        return dtblReport;
    }

    //+SSM 11/18/2011 AntechCSM 2a2 #Issue117055 Added ConsultNo,Status,ResolvedBy search field,Removed Specialty search field
    public static DataTable getConsultationDetails(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string intExternal)
    {
        return DL_ConsultRecorder.getConsultationDetails(consultNumber, accountNumber, accessionNumber, dateFrom, dateTo, status, method, requestedConsultant, resolvedBy, reasonForConsult, priority, speciality, enteredBy, intExternal);
    }
    //-SSM

    /// <summary>
    /// This method calculates Consult Turn Around Time
    /// </summary>
    /// <param name="strQS">Search params as query string</param>
    /// <param name="isSecondReqTAT">Flag indicating whether Report is Second Request TAT</param>
    /// <returns>Data table containing the report result</returns>
    public static DataTable GetSTATConsultTurnAroundTime(string strQS, bool isSecondReqTAT)
    {
        String[] QS = strQS.Split('^');
        DataTable dtOriginalValue = DL_ConsultRecorder.GetSTATConsultTurnAroundTime(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13], isSecondReqTAT);
        DataTable dtResultValue = new DataTable();
        dtResultValue = GetSTATConsultTATResultStructure(dtResultValue);
        dtResultValue = GetCalculatedTATReport(dtOriginalValue, dtResultValue);
        return dtResultValue;
    }

    /// <summary>
    /// Provides the table structure of the report
    /// </summary>
    /// <param name="dtSource">Report data source</param>
    /// <returns>Empty data table with report table structure</returns>
    private static DataTable GetSTATConsultTATResultStructure(DataTable dtSource)
    {
        dtSource = new DataTable();
        DataColumn dcAcct = new DataColumn("Acct", typeof(string));
        DataColumn dcAcctName = new DataColumn("AcctName", typeof(string));
        DataColumn dcConsultNo = new DataColumn("ConsultNo", typeof(string));
        DataColumn dcStat = new DataColumn("Stat", typeof(string));
        DataColumn dcSecReqTAT = new DataColumn("SecReqTAT", typeof(string));
        DataColumn dcRequestGenerated = new DataColumn("RequestGenerated", typeof(string));
        DataColumn dcRequestClaimed = new DataColumn("RequestClaimed", typeof(string));
        DataColumn dcSecReqGenDetails = new DataColumn("SecReqGenDetails", typeof(string));
        DataColumn dcRequestFinalized = new DataColumn("RequestFinalized", typeof(string));
        DataColumn dcConsultComment = new DataColumn("ConsultComment", typeof(string));
        DataColumn dcTAT = new DataColumn("TAT", typeof(string));
        dtSource.Columns.Add(dcAcct);
        dtSource.Columns.Add(dcAcctName);
        dtSource.Columns.Add(dcConsultNo);
        dtSource.Columns.Add(dcStat);
        dtSource.Columns.Add(dcSecReqTAT);
        dtSource.Columns.Add(dcRequestGenerated);
        dtSource.Columns.Add(dcRequestClaimed);
        dtSource.Columns.Add(dcSecReqGenDetails);
        dtSource.Columns.Add(dcRequestFinalized);
        dtSource.Columns.Add(dcConsultComment);
        dtSource.Columns.Add(dcTAT);
        return dtSource;
    }

    /// <summary>
    /// Calculates the TAT report
    /// </summary>
    /// <param name="dtSource">Original Data source</param>
    /// <param name="dtTarget">Result data table</param>
    /// <returns>Result data table</returns>
    private static DataTable GetCalculatedTATReport(DataTable dtSource, DataTable dtTarget)
    {
        if (dtSource != null && dtTarget != null)
        {
            DataRow drNewTargetRow = dtTarget.NewRow();
            for (int src = 0; src < dtSource.Rows.Count; src++)
            {
                drNewTargetRow = dtTarget.NewRow();
                drNewTargetRow["Acct"] = dtSource.Rows[src]["ACCOUNT"].ToString();
                drNewTargetRow["AcctName"] = dtSource.Rows[src]["AccountName"].ToString();
                drNewTargetRow["ConsultNo"] = dtSource.Rows[src]["ConsultNo"].ToString();
                drNewTargetRow["Stat"] = dtSource.Rows[src]["Priority"].ToString();
                drNewTargetRow["SecReqTAT"] = dtSource.Rows[src]["IsSecondReq"].ToString().StartsWith("Y", StringComparison.OrdinalIgnoreCase) ? "2nd Req" : string.Empty;
                drNewTargetRow["RequestGenerated"] = GetRequestProgressInfo(dtSource.Rows[src]["CNSLT_EnteredDate"].ToString(), dtSource.Rows[src]["CNSLT_EnteredTime"].ToString(), dtSource.Rows[src]["EnteredByDispName"].ToString(), false, string.Empty, string.Empty);
                drNewTargetRow["RequestClaimed"] = GetRequestProgressInfo(dtSource.Rows[src]["CNSLT_ProcessedDate"].ToString(), dtSource.Rows[src]["CNSLT_ProcessedTime"].ToString(), dtSource.Rows[src]["CurrentConsultantDispName"].ToString(), false, string.Empty, string.Empty);
                drNewTargetRow["SecReqGenDetails"] = GetRequestProgressInfo(dtSource.Rows[src]["SecondReqDate"].ToString(), dtSource.Rows[src]["SecondReqTime"].ToString(), dtSource.Rows[src]["SecondReqByDispName"].ToString(), false, string.Empty, string.Empty);
                drNewTargetRow["RequestFinalized"] = GetRequestProgressInfo(dtSource.Rows[src]["FinalizedDate"].ToString(), dtSource.Rows[src]["FinalizedTime"].ToString(), dtSource.Rows[src]["ClosedByUserDispName"].ToString(), false, string.Empty, string.Empty);
                drNewTargetRow["ConsultComment"] = GetRequestProgressInfo(dtSource.Rows[src]["CNSLT_TATNoteAddedDate"].ToString(), dtSource.Rows[src]["CNSLT_TATNoteAddedTime"].ToString(), dtSource.Rows[src]["CNSLT_TATNoteAddedBy"].ToString(), true, dtSource.Rows[src]["CNSLT_TATNoteText"].ToString(), dtSource.Rows[src]["CNSLT_TATNoteRef"].ToString());
                drNewTargetRow["TAT"] = GetTurnAroundTime(dtSource.Rows[src]["CNSLT_EnteredDate"].ToString(), dtSource.Rows[src]["CNSLT_EnteredTime"].ToString(), dtSource.Rows[src]["FinalizedDate"].ToString(), dtSource.Rows[src]["FinalizedTime"].ToString());
                dtTarget.Rows.Add(drNewTargetRow);
            }
        }
        return dtTarget;
    }

    /// <summary>
    /// Gets the request progress info
    /// </summary>
    /// <param name="date">Date</param>
    /// <param name="time">Time</param>
    /// <param name="displayName">Display Name</param>
    /// <param name="isNote">Flag indicating whether the text provided is a note type </param>
    /// <param name="note">Actual Note retrieved</param>
    /// <param name="noteRowID">The note row reference</param>
    /// <returns>Request Progress Info</returns>
    private static string GetRequestProgressInfo(string date, string time, string displayName, bool isNote, string note, string noteRowID)
    {
        string strInfo = string.Empty;
        DateTime dtValidator = new DateTime();
        TimeSpan tsValidator = new TimeSpan();
        if (DateTime.TryParse(date, out dtValidator) && TimeSpan.TryParse(time, out tsValidator))
        {
            if (isNote)
            {
                if (!string.IsNullOrEmpty(noteRowID))
                {
                    strInfo = string.IsNullOrEmpty(displayName) ? string.Empty : ("Added By" + displayName + " on <br/>") + DateTime.Parse(date).ToString(AtlasIndia.AntechCSM.UI.UIfunctions.getDateFormat()) + " " + DateTime.Parse(time).ToString(AtlasIndia.AntechCSM.UI.UIfunctions.getTimeFormat()) + (string.IsNullOrEmpty(note) ? string.Empty : "<br/>" + note);
                }
            }
            else
            {
                strInfo = string.IsNullOrEmpty(displayName) ? string.Empty : (displayName + " on <br/>") + DateTime.Parse(date).ToString(AtlasIndia.AntechCSM.UI.UIfunctions.getDateFormat()) + " " + DateTime.Parse(time).ToString(AtlasIndia.AntechCSM.UI.UIfunctions.getTimeFormat());
            }
        }
        return strInfo;
    }

    /// <summary>
    /// Gets turn around time
    /// </summary>
    /// <param name="requestDate">Request Date</param>
    /// <param name="requestTime">Request Time</param>
    /// <param name="finalizeDate">Finalized Date</param>
    /// <param name="finalizeTime">Finalized Time</param>
    /// <returns>Turn Around Time</returns>
    private static string GetTurnAroundTime(string requestDate, string requestTime, string finalizeDate, string finalizeTime)
    {
        string turnAroundTime = string.Empty;
        DateTime dtValidator = new DateTime();
        TimeSpan tsValidator = new TimeSpan();
        if (DateTime.TryParse(requestDate, out dtValidator) && TimeSpan.TryParse(requestTime, out tsValidator) && DateTime.TryParse(finalizeDate, out dtValidator) && TimeSpan.TryParse(finalizeTime, out tsValidator))
        {
            DateTime dtRequestDateTime = DateTime.Parse(requestDate).AddTicks(TimeSpan.Parse(requestTime).Ticks);
            DateTime dtFinalizeDateTime = DateTime.Parse(finalizeDate).AddTicks(TimeSpan.Parse(finalizeTime).Ticks);
            TimeSpan tsDifference = dtFinalizeDateTime.Subtract(dtRequestDateTime);
            turnAroundTime = string.Format("{0:00}:{1:00}:{2:00}", tsDifference.Days, tsDifference.Hours, tsDifference.Minutes);// +":" + string.Format("{0:00}", tsDifference.Hours) + ":" + string.Format("{0:00}", tsDifference.Minutes);
        }
        return turnAroundTime;
    }

    public static String insertConsultation(string MainAccession, string RelatedAccString, string ContactPerson, string ContactDetails,
       string Speciality, string Priority, string ReqConsultant, string Notes, string UserEnteredBy,string client, string method,string consultReason,
        string earliestCallbackDate, string earliestHour, string earliestMinutes, string earliestAMPM, string latestCallbackDate, string latestHour,
        string latestMinutes, string latestAMPM, string additionalCallbackInst, string timeZone, string isExternal, string replyToUserEmail, string personContactedAtClinic, string relatedAccessions,string status)
    {
        string strServerLocation = AtlasIndia.AntechCSM.UI.UIfunctions.GetServerLocation();
        string strEarliestCallBackTime = "";
        if (earliestHour.Length > 0 && earliestMinutes.Length > 0 && earliestAMPM.Length > 0)
        {
            strEarliestCallBackTime = earliestHour + ":" + earliestMinutes + earliestAMPM;
        }
        string strLatestCallBackTime = "";
        if (latestHour.Length > 0 && latestMinutes.Length > 0 && latestAMPM.Length > 0)
        {
            strLatestCallBackTime = latestHour + ":" + latestMinutes + latestAMPM;
        }

        string strConsultNo= DL_ConsultRecorder.insertConsultation(MainAccession, RelatedAccString, ContactPerson, ContactDetails,
         Speciality, Priority, ReqConsultant, Notes, UserEnteredBy, client, method, consultReason, earliestCallbackDate,
         strEarliestCallBackTime, latestCallbackDate, strLatestCallBackTime, additionalCallbackInst, timeZone, "N", personContactedAtClinic, 
         relatedAccessions, status, strServerLocation,SessionHelper.UserContext.LabLocation);

        string strAccessionsToBeRetrived = relatedAccessions + ";" + RelatedAccString;

        if (isExternal == "Y")
        {
            sendExternalEmail(strConsultNo, MainAccession, strAccessionsToBeRetrived, UserEnteredBy, ReqConsultant, ContactPerson, ContactDetails, consultReason, Priority, Notes, earliestCallbackDate, strEarliestCallBackTime, latestCallbackDate, strLatestCallBackTime, timeZone, additionalCallbackInst, replyToUserEmail);
        }
        return strConsultNo;
    }

    public static void sendExternalEmail(string consultRow, string mainAccession, string relatedAccString, string user, string requestedConsultant, 
        string contactPerson, string contactDetails, string consultReason, string priority, string note, string earliestCallBackDate, 
        string earliestCallBackTime, string latestCallBackDate, string latestCallBackTime, string timeZone, string addCallBackInst,string replyToUserMail)
    {
        string strAllAccessionsRet = "Y";
        string strAccessions = relatedAccString;
        string strPDFFileNames = "";
        string[] arrAccessions = strAccessions.Split(new char[] { ';' },System.StringSplitOptions.RemoveEmptyEntries);

        String strFileServerURL = System.Configuration.ConfigurationManager.AppSettings["FileServerURL"];
        String strFileServerAttribute = System.Configuration.ConfigurationManager.AppSettings["FileServerAttribute"];
        String strFolderPath = System.Configuration.ConfigurationManager.AppSettings["EXTERNAL_EMAIL_FOLDER_PATH"];
        int iPDfCounter = 1;

        try
        {
            if (!System.IO.Directory.Exists(strFolderPath))
            {
                System.IO.Directory.CreateDirectory(strFolderPath);
            }

            for (int i = 0; i < arrAccessions.Length; i++)
            {
                string url = String.Format(String.Concat(strFileServerURL, "?", strFileServerAttribute, "={0}"), arrAccessions[i]);
                string strRTF = "";
                try
                {
                    System.Net.WebClient serviceRequest = new System.Net.WebClient();
                    strRTF = serviceRequest.DownloadString(new Uri(url));
                }
                catch (System.Net.WebException ex)
                {
                    //Do not throw exception
                    strAllAccessionsRet = "N";
                    continue;
                }

                WebSupergoo.ABCpdf8.Doc pdfDoc = new WebSupergoo.ABCpdf8.Doc();
                WebSupergoo.ABCpdf8.XReadOptions readOptions = new WebSupergoo.ABCpdf8.XReadOptions();
                readOptions.FileExtension = "rtf";

                System.Text.Encoding ASCIIEncoder = System.Text.Encoding.ASCII;
                int byteCount = ASCIIEncoder.GetByteCount(strRTF);
                byte[] myByteArray = new byte[byteCount];
                myByteArray = ASCIIEncoder.GetBytes(strRTF);

                for (int j = 0; j < byteCount; j++)
                {
                    Char c = strRTF[j];
                    int ascival = Convert.ToInt32(c);
                    if (ascival > 127)
                    {
                        if (((ascival >= 8192) && (ascival <= 8303)) || (ascival == 8364) || (ascival == 8482))
                        {
                            System.Text.Encoding targetEncoding;
                            byte[] onebyte = new byte[1];
                            targetEncoding = System.Text.Encoding.GetEncoding(1258);
                            onebyte = targetEncoding.GetBytes(c.ToString());
                            myByteArray[j] = onebyte[0];
                        }
                        else if (((ascival >= 256) && (ascival <= 563)) || ((ascival >= 688) && (ascival <= 750)))
                        {
                            System.Text.Encoding targetEncoding;
                            byte[] onebyte = new byte[1];
                            targetEncoding = System.Text.Encoding.GetEncoding(1252);
                            onebyte = targetEncoding.GetBytes(c.ToString());
                            myByteArray[j] = onebyte[0];
                        }
                        else
                        {
                            System.Text.Encoding unicode = System.Text.Encoding.Unicode;
                            byte[] onebyte = new byte[1];
                            onebyte = unicode.GetBytes(c.ToString());
                            myByteArray[j] = onebyte[0];
                        }
                    }
                }
                Byte[] data = new PdfUtilities().ReadRTFToPdfDoc(myByteArray);

                pdfDoc.Read(data, readOptions);

                pdfDoc.Save(strFolderPath + consultRow + "_" + iPDfCounter.ToString() + ".pdf");
                strPDFFileNames += (strPDFFileNames == "" ? consultRow + "_" + iPDfCounter.ToString() + ".pdf" : "~" + consultRow + "_" + iPDfCounter.ToString() + ".pdf");
                iPDfCounter += 1;
            }
        }
        catch (Exception ex)
        {
            strAllAccessionsRet = "N";
        }
        DL_ConsultRecorder.sendExternalEmail(consultRow, mainAccession, user, requestedConsultant, contactPerson, contactDetails, consultReason, 
            priority, note,earliestCallBackDate, earliestCallBackTime, latestCallBackDate, latestCallBackTime, timeZone, addCallBackInst,
            strAllAccessionsRet, strPDFFileNames, replyToUserMail);
    }

    public static String insertConsultation(string MainAccession, string RelatedAccString, string ContactPerson, string ContactDetails,
      string Speciality, string Priority, string ReqConsultant, string Notes, string UserEnteredBy, string client, string method, string consultReason,
       string earliestCallbackDate, string earliestHour, string earliestMinutes, string earliestAMPM, string latestCallbackDate, string latestHour,
       string latestMinutes, string latestAMPM, string additionalCallbackInst, string timeZone, string personContactedAtClinic, string relatedAccessions,string status)
    {
        return ConsultRecorder.insertConsultation(MainAccession, RelatedAccString, ContactPerson, ContactDetails,
      Speciality, Priority, ReqConsultant, Notes, UserEnteredBy, client, method, consultReason,
       earliestCallbackDate, earliestHour, earliestMinutes, earliestAMPM, latestCallbackDate, latestHour,
       latestMinutes, latestAMPM, additionalCallbackInst, timeZone, "", "", personContactedAtClinic, relatedAccessions,status);
    }

    public static bool updateConsultation(string cnsltRow, string contactPerson, string alternateNo, string reasonForConsult, string addtionalComments
        , string earliestCallbackDate, string earliestHour, string earliestMinutes, string earliestAMPM, string latestCallbackDate, string latestHour,
       string latestMinutes, string latestAMPM, string additionalCallbackInst, string timeZone, string Priority, string SecondRequest)
    {
        string strEarliestCallBackTime = "";
        if (earliestHour.Length > 0 && earliestMinutes.Length > 0 && earliestAMPM.Length > 0)
        {
            strEarliestCallBackTime = earliestHour + ":" + earliestMinutes + earliestAMPM;
        }
        string strLatestCallBackTime = "";
        if (latestHour.Length > 0 && latestMinutes.Length > 0 && latestAMPM.Length > 0)
        {
            strLatestCallBackTime = latestHour + ":" + latestMinutes + latestAMPM;
        }

        string retValue = "";

        retValue= DL_ConsultRecorder.updateConsultation(cnsltRow, contactPerson, alternateNo, reasonForConsult, addtionalComments,earliestCallbackDate, strEarliestCallBackTime
            , latestCallbackDate, strLatestCallBackTime, additionalCallbackInst, timeZone, Priority, SessionHelper.UserContext.ID, SecondRequest);
        if (retValue == "0")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static String sendExternalConsultEmail(string consultNo, string mainAccession, string relatedAccString, string contactPerson,
        string contactDetails, string priority, string additionalNotes, string consultReason, string replyToUser, string earliestCallbackDate,
        string earliestHour, string earliestMinutes, string earliestAMPM, string latestCallbackDate, string latestHour,
        string latestMinutes, string latestAMPM, string additionalCallbackInst, string timeZone)
    {
        string strEarliestCallBackTime = "";
        if (earliestHour.Length > 0 && earliestMinutes.Length > 0 && earliestAMPM.Length > 0)
        {
            strEarliestCallBackTime = earliestHour + ":" + earliestMinutes + earliestAMPM;
        }
        string strLatestCallBackTime = "";
        if (latestHour.Length > 0 && latestMinutes.Length > 0 && latestAMPM.Length > 0)
        {
            strLatestCallBackTime = latestHour + ":" + latestMinutes + latestAMPM;
        }

        string strEmailSubject = "New Consult Request.  Consult Request ID:  " + consultNo;

        string strLineBreak = "\r\n";
        string strTab = "\t";
        StringBuilder sbEmailBody = new StringBuilder();

        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Contact Person : ");
        sbEmailBody.Append(contactPerson);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Alternate Number : ");
        sbEmailBody.Append(contactDetails);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Reason For Consult : ");
        sbEmailBody.Append(consultReason);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("STAT : ");
        sbEmailBody.Append(priority);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Additional Comments : ");
        sbEmailBody.Append(additionalNotes);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Earliest Availability Date/Time :");
        sbEmailBody.Append(earliestCallbackDate + " " + strEarliestCallBackTime);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Latest Availability Date/Time : ");
        sbEmailBody.Append(latestCallbackDate + " " + strLatestCallBackTime);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Time Zone of the client : ");
        sbEmailBody.Append(timeZone);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Callback Schedule and Additional Callback Instructions : ");
        sbEmailBody.Append(additionalCallbackInst);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append("Reply to User : ");
        sbEmailBody.Append(replyToUser);


        return DL_ConsultRecorder.sendExternalConsultEmail(mainAccession, relatedAccString, contactPerson,
        contactDetails, priority, additionalNotes, consultReason, replyToUser, strEmailSubject,
        earliestCallbackDate, strEarliestCallBackTime, latestCallbackDate, strLatestCallBackTime,
        additionalCallbackInst, timeZone);
    }

    public static string updateCheckOutStatus(string CheckedOut, string ConsultRowId, string UserId, string specialty, string reqConsultant)
    {
        return DL_ConsultRecorder.updateCheckOutStatus(CheckedOut, ConsultRowId, UserId, specialty, reqConsultant);
    }

    public static string UpdateProgressNote(string rowID, string progressNote, string user)
    {
        return DL_ConsultRecorder.UpdateProgressNote(rowID, progressNote, user);
    }

    /// <summary>
    /// Insert / Update the TAT Note Added
    /// </summary>
    /// <param name="rowID"> The Consult Row Id </param>
    /// <param name="progressNote">The actual TAT Note content</param>
    /// <param name="user">The user adding/modifying the note</param>
    /// <returns>A SUCCESS string on sucessful updation/insertion</returns>
    public static string AddCommunicationTATNote(string rowID, string progressNote, string user)
    {
        return DL_ConsultRecorder.AddCommunicationTATNote(rowID, progressNote, user);
    }

    public static string AddCompleteConsultInfo(string consultID, string consultResaon, string consultNotes,string consultUser, string consultPersonContacted, string consultContactedOn,string callerName, string resolverByConsultant)
    {
        return DL_ConsultRecorder.addCompleteConsultDetails(consultID, consultResaon, consultNotes, consultUser, consultPersonContacted, consultContactedOn, callerName, resolverByConsultant);
    }

    public static string AutoCompleteConsultInfo(string consultID,string consultUser,string reasonForComm)
    {
        return DL_ConsultRecorder.autoCompleteConsultDetails(consultID, consultUser, reasonForComm);
    }

    public static string SendConsultCommunication( string consultID, string toGroups, string CommunicationReason, string accountNo,
        string accountName, string phoneNo, string consultNo, string[] relatedAccessions, string unrelatedAccessionString,
        string consultReason, string accessionString, string testString, string communicationNote, string consultantName)
    {
        string strLineBreak = "\r\n";
        string strTab = "\t";
        StringBuilder sbEmailBody = new StringBuilder();

        sbEmailBody.Append("Account Number");
        sbEmailBody.Append(strTab);
        sbEmailBody.Append(accountNo);
        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append("Account Name");
        sbEmailBody.Append(strTab);
        sbEmailBody.Append(accountName);
        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append("Phone Number");
        sbEmailBody.Append(strTab);
        sbEmailBody.Append(phoneNo);
        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append("Consult Number");
        sbEmailBody.Append(strTab);
        sbEmailBody.Append(consultNo);
        sbEmailBody.Append(strLineBreak);

        // Related Accession
        if (relatedAccessions != null && relatedAccessions.Length > 0)
        {
            sbEmailBody.Append(strLineBreak);
            sbEmailBody.Append("Related Accession(s):");
            
            for (int intCnt = 0; intCnt < relatedAccessions.Length; intCnt++)
            {
                sbEmailBody.Append(strLineBreak);
                sbEmailBody.Append(relatedAccessions[intCnt]);
                sbEmailBody.Append(strLineBreak);
            }
        }

        // Unrelated accessions
        if (unrelatedAccessionString != null && unrelatedAccessionString.Length > 0)
        {
            sbEmailBody.Append(strLineBreak);
            sbEmailBody.Append("Unrelated Accession(s):");
            sbEmailBody.Append(strLineBreak);
            sbEmailBody.Append(unrelatedAccessionString);
        }

        // Reason for Consult
        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append("Reason for Consult:  ");
        sbEmailBody.Append(consultReason);
        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append("..................................................");
        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append("Accession(s)");
        sbEmailBody.Append(strTab);
        sbEmailBody.Append(accessionString);
        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append("Test(s)");
        sbEmailBody.Append(strTab);
        sbEmailBody.Append(strTab);
        sbEmailBody.Append(testString);
        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append(communicationNote.ToUpper());
        sbEmailBody.Append(strLineBreak);
        sbEmailBody.Append(strLineBreak);

        sbEmailBody.Append("Requested by:  ");
        sbEmailBody.Append(consultantName);
        sbEmailBody.Append(strLineBreak);

        string strMailSubject = "Consultant Request - " + CommunicationReason;
        string strMailSystem = ConfigurationSettings.AppSettings["MAILSYSTEM"];
        string strAck = "0";
        string strAckMessageID = "";

        return DL_ConsultRecorder.sendConsultCommunication(consultID, toGroups, strMailSubject, sbEmailBody.ToString(), strAck, strAckMessageID, SessionHelper.UserContext.ID, strMailSystem, CommunicationReason, testString, communicationNote);
    }

    public static DataTable getReasonsForCommunication()
    {
        return DL_ConsultRecorder.getReasonsForCommunication();
    }

    public static DataTable ConsultByUserAgentSummary(string strQueryString, out int[] arrGrandTotal)
    {
        DataTable dtConsultByUserAgent = new DataTable();
        String[] QS = strQueryString.Split('^');
        dtConsultByUserAgent = GetConsultByUserAgentSummaryTable(false);
        arrGrandTotal = new int[7];
        DataTable returnDataTable = DL_ConsultRecorder.ConsultByUserAgentSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            int iTransfer = 0, iAgent = 0, iQueue=0, iInternal=0, iExternal=0, iAvianExotic = 0;
            int iTransferTotal = 0, iAgentTotal = 0, iQueueTotal = 0, iInternalTotal = 0, iExternalTotal=0, iAvianExoticTotal = 0;
            string strOldUserDR = returnDataTable.Rows[0]["CNSLT_EnteredByUserDR"].ToString().Trim();
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strNewUserDR = returnDataTable.Rows[iRowCount]["CNSLT_EnteredByUserDR"].ToString().Trim();
                if (strNewUserDR == strOldUserDR)
                {
                    string strRequestedMethod = returnDataTable.Rows[iRowCount]["CNSLT_RequestedMethod"].ToString().Trim();
                    string strLocation = returnDataTable.Rows[iRowCount]["IntExt"].ToString().Trim();
                    string strSpeciality = returnDataTable.Rows[iRowCount]["CNSLT_SpecialtyDR"].ToString().Trim();
                    string strCount = returnDataTable.Rows[iRowCount]["CnsltCount"].ToString().Trim();
                    int iCount = 0;
                    if (strCount.Length > 0)
                    {
                        iCount = Convert.ToInt32(strCount);
                    }
                    switch (strLocation)
                    {
                        case "I":
                            iInternal += iCount;
                            break;
                        case "O":
                            iExternal += iCount;
                            break;
                        default:
                            break;
                    }
                    switch (strSpeciality)
                    {
                        case "EXO":
                            iAvianExotic += iCount;
                            break;
                        default:
                            break;
                    }
                    switch (strRequestedMethod)
                    {
                        case "SC":
                            iAgent += iCount;
                            break;
                        case "DC":
                            iQueue += iCount;
                            break;
                        case "TC":
                            iTransfer += iCount;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    string strUserName = returnDataTable.Rows[iRowCount - 1]["UserEntered"].ToString().Trim();
                    string strRequested = QS[3] + " to " + QS[4];
                    int iConsultTotal = iTransfer + iAgent + iQueue;
                    if (iConsultTotal > 0)
                    {
                        dtConsultByUserAgent.Rows.Add(strUserName, strRequested, iConsultTotal, iTransfer, iAgent, iQueue, iInternal, iExternal, iAvianExotic);
                    }

                    iTransferTotal += iTransfer;
                    iAgentTotal += iAgent;
                    iQueueTotal += iQueue;
                    iInternalTotal += iInternal;
                    iExternalTotal += iExternal;
                    iAvianExoticTotal += iAvianExotic;
                    
                    iTransfer = 0; iAgent = 0; iQueue = 0; iInternal = 0; iExternal = 0; iAvianExotic = 0;
                    iRowCount--;
                }
                strOldUserDR = strNewUserDR;
            }
            string strUsertName1 = returnDataTable.Rows[returnDataTable.Rows.Count - 1]["UserEntered"].ToString().Trim();
            string strRequested1 = QS[3] + " to " + QS[4];
            int iConsultTotal1 = iTransfer + iAgent + iQueue;
            if (iConsultTotal1 > 0)
            {
                dtConsultByUserAgent.Rows.Add(strUsertName1, strRequested1, iConsultTotal1, iTransfer, iAgent, iQueue, iInternal, iExternal, iAvianExotic);
            }
            iTransferTotal += iTransfer;
            iAgentTotal += iAgent;
            iQueueTotal += iQueue;
            iInternalTotal += iInternal;
            iExternalTotal += iExternal;
            iAvianExoticTotal += iAvianExotic;

            int[] intarrGrandTotal = new int[9];
            intarrGrandTotal[0] = iTransferTotal + iAgentTotal + iQueueTotal;
            intarrGrandTotal[1] = iTransferTotal;
            intarrGrandTotal[2] = iAgentTotal;
            intarrGrandTotal[3] = iQueueTotal;
            intarrGrandTotal[4] = iInternalTotal;
            intarrGrandTotal[5] = iExternalTotal;
            intarrGrandTotal[6] = iAvianExoticTotal;
            Array.Copy(intarrGrandTotal, arrGrandTotal, 7);
        }
        
        return dtConsultByUserAgent;
    }

    public static DataTable ConsultByUserAgentDetails(string strQueryString,out string strDateRequested)
    {
        DataTable dtConsultByUserAgent = new DataTable();
        String[] QS = strQueryString.Split('^');
        strDateRequested = QS[3] + " to " + QS[4];
        dtConsultByUserAgent = GetConsultByUserAgentSummaryTable(true);
        DataTable returnDataTable = DL_ConsultRecorder.ConsultByUserAgentSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            int iTransfer = 0, iAgent = 0, iQueue = 0, iInternal = 0, iExternal = 0, iAvianExotic = 0;
            int iTransferTotal = 0, iAgentTotal = 0, iQueueTotal = 0, iInternalTotal = 0, iExternalTotal = 0, iAvianExoticTotal = 0;
            string strOldUserDR = returnDataTable.Rows[0]["CNSLT_EnteredByUserDR"].ToString().Trim();
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strNewUserDR = returnDataTable.Rows[iRowCount]["CNSLT_EnteredByUserDR"].ToString().Trim();
                if (strNewUserDR == strOldUserDR)
                {
                    string strRequestedMethod = returnDataTable.Rows[iRowCount]["CNSLT_RequestedMethod"].ToString().Trim();
                    string strLocation = returnDataTable.Rows[iRowCount]["IntExt"].ToString().Trim();
                    string strSpeciality = returnDataTable.Rows[iRowCount]["CNSLT_SpecialtyDR"].ToString().Trim();
                    string strCount = returnDataTable.Rows[iRowCount]["CnsltCount"].ToString().Trim();
                    int iCount = 0;
                    if (strCount.Length > 0)
                    {
                        iCount = Convert.ToInt32(strCount);
                    }
                    switch (strLocation)
                    {
                        case "I":
                            iInternal += iCount;
                            break;
                        case "O":
                            iExternal += iCount;
                            break;
                        default:
                            break;
                    }
                    switch (strSpeciality)
                    {
                        case "EXO":
                            iAvianExotic += iCount;
                            break;
                        default:
                            break;
                    }
                    switch (strRequestedMethod)
                    {
                        case "SC":
                            iAgent += iCount;
                            break;
                        case "DC":
                            iQueue += iCount;
                            break;
                        case "TC":
                            iTransfer += iCount;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    string strUserName = returnDataTable.Rows[iRowCount - 1]["UserEntered"].ToString().Trim();
                    string strRequested = QS[3] + " to " + QS[4];
                    int iConsultTotal = iTransfer + iAgent + iQueue;
                    if (iConsultTotal > 0)
                    {
                        DataTable dtDatailsTable = GetConsultByUserAgentDetailsTable(strOldUserDR, QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
                        dtConsultByUserAgent.Rows.Add(strUserName, strRequested, iConsultTotal, iTransfer, iAgent, iQueue, iInternal, iExternal, iAvianExotic, dtDatailsTable);
                    }

                    iTransferTotal += iTransfer;
                    iAgentTotal += iAgent;
                    iQueueTotal += iQueue;
                    iInternalTotal += iInternal;
                    iExternalTotal += iExternal;
                    iAvianExoticTotal += iAvianExotic;

                    iTransfer = 0; iAgent = 0; iQueue = 0; iInternal = 0; iExternal = 0; iAvianExotic = 0;
                    iRowCount--;
                }
                strOldUserDR = strNewUserDR;
            }
            string strUserName1 = returnDataTable.Rows[returnDataTable.Rows.Count - 1]["UserEntered"].ToString().Trim();
            string strRequested1 = QS[3] + " to " + QS[4];
            int iConsultTotal1 = iTransfer + iAgent + iQueue;
            if (iConsultTotal1 > 0)
            {
                DataTable dtDatailsTable = GetConsultByUserAgentDetailsTable(strOldUserDR, QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
                dtConsultByUserAgent.Rows.Add(strUserName1, strRequested1, iConsultTotal1, iTransfer, iAgent, iQueue, iInternal, iExternal, iAvianExotic, dtDatailsTable);
            }
        }

        return dtConsultByUserAgent;
    }

    public static DataTable ConsultSummaryIES(string strQueryString, out string strFromDate, out string strToDate, out int[] arrGrandTotal)
    {
        DataTable dtConsultSummaryIES = new DataTable();
        dtConsultSummaryIES = GetConsultSummaryIESTable();
        String[] QS = strQueryString.Split('^');
        strFromDate = QS[3];
        strToDate = QS[4];
        arrGrandTotal = new int[9];

        DataTable returnDataTable = DL_ConsultRecorder.ConsultSummaryIES(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            int iInternalConsultant = 0, iExternalConsultant = 0, iAvianExotic = 0, iBehavior = 0, iCardio = 0, iEndo = 0, iGastro = 0, iIntMed = 0, iNeuro = 0;
            int iInternalConsultantTotal = 0, iExternalConsultantTotal = 0, iAvianExoticTotal = 0, iBehaviorTotal = 0, iCardioTotal = 0, iEndoTotal = 0, iGastroTotal = 0, iIntMedTotal = 0, iNeuroTotal = 0;
            string strOldCreateDate = (Convert.ToDateTime(returnDataTable.Rows[0]["CNSLT_EnteredDate"].ToString().Trim())).ToShortDateString();

            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strNewCreateDate = (Convert.ToDateTime(returnDataTable.Rows[iRowCount]["CNSLT_EnteredDate"].ToString().Trim())).ToShortDateString();
                if (strNewCreateDate.Length > 0)
                {
                    if (strNewCreateDate == strOldCreateDate)
                    {
                        string strLocation = returnDataTable.Rows[iRowCount]["LOCATION"].ToString().Trim();
                        string strSpeciality = returnDataTable.Rows[iRowCount]["CNSLT_SpecialtyDR"].ToString().Trim();
                        string strCount = returnDataTable.Rows[iRowCount]["CnsltCount"].ToString().Trim();
                        int iCount = 0;
                        if (strCount.Length > 0)
                        {
                            iCount = Convert.ToInt32(strCount);
                        }
                        switch (strLocation)
                        {
                            case "I":
                                iInternalConsultant += iCount;
                                break;
                            case "O":
                                iExternalConsultant += iCount;
                                break;
                            default:
                                break;
                        }
                        switch (strSpeciality)
                        {
                            case "AVEX":
                                iAvianExotic += iCount;
                                break;
                            case "BEHAV":
                                iBehavior += iCount;
                                break;
                            case "CARD":
                                iCardio += iCount;
                                break;
                            case "ENDO":
                                iEndo += iCount;
                                break;
                            case "GAS":
                                iGastro += iCount;
                                break;
                            case "IM":
                                iIntMed += iCount;
                                break;
                            case "NEURO":
                                iNeuro += iCount;
                                break;
                            default:
                                break;
                        }
                    }
                    else
                    {
                        dtConsultSummaryIES.Rows.Add(strOldCreateDate, iInternalConsultant, iExternalConsultant, iAvianExotic, iBehavior, iCardio, iEndo, iGastro, iIntMed, iNeuro);
                        iInternalConsultantTotal += iInternalConsultant; 
                        iExternalConsultantTotal += iExternalConsultant;
                        iAvianExoticTotal +=iAvianExotic;
                        iBehaviorTotal += iBehavior;
                        iCardioTotal += iCardio;
                        iEndoTotal += iEndo;
                        iGastroTotal += iGastro;
                        iIntMedTotal += iIntMed;
                        iNeuroTotal += iNeuro;

                        iInternalConsultant = 0; iExternalConsultant = 0; iAvianExotic = 0; iBehavior = 0; iCardio = 0; iEndo = 0; iGastro = 0; iIntMed = 0; iNeuro = 0;
                        iRowCount--;
                    }
                }
                strOldCreateDate = strNewCreateDate;
            }

            dtConsultSummaryIES.Rows.Add(strOldCreateDate, iInternalConsultant, iExternalConsultant, iAvianExotic, iBehavior, iCardio, iEndo, iGastro, iIntMed, iNeuro);
            iInternalConsultantTotal += iInternalConsultant;
            iExternalConsultantTotal += iExternalConsultant;
            iAvianExoticTotal += iAvianExotic;
            iBehaviorTotal += iBehavior;
            iCardioTotal += iCardio;
            iEndoTotal += iEndo;
            iGastroTotal += iGastro;
            iIntMedTotal += iIntMed;
            iNeuroTotal += iNeuro;
           
            int[] intarrGrandTotal = new int[9];
            intarrGrandTotal[0] = iInternalConsultantTotal;
            intarrGrandTotal[1] = iExternalConsultantTotal;
            intarrGrandTotal[2] = iAvianExoticTotal;
            intarrGrandTotal[3] = iBehaviorTotal;
            intarrGrandTotal[4] = iCardioTotal;
            intarrGrandTotal[5] = iEndoTotal;
            intarrGrandTotal[6] = iGastroTotal;
            intarrGrandTotal[7] = iIntMedTotal;
            intarrGrandTotal[8] = iNeuroTotal;
            Array.Copy(intarrGrandTotal, arrGrandTotal, 9);
        }
        return dtConsultSummaryIES;
    }

    public static DataTable CommunicationsByConsultantsSummary(string strQueryString, out string[] arrGrandTotal)
    {
        DataTable dtCommByConsultantsSummary = new DataTable();
        String[] QS = strQueryString.Split('^');
        arrGrandTotal = new string[5];
        dtCommByConsultantsSummary = GetCommByConsultantsSummaryTable(false);
        DataTable returnDataTable = DL_ConsultRecorder.GetCommunicationsByConsultantsSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            string strRequested = QS[3] + " to " + QS[4];
            int iRecheckRequests = 0, iAdditionalTest = 0, iReportProblem = 0, iMiscellaneous = 0;
            int iRecheckRequestsTotal = 0, iAdditionalTestTotal = 0, iReportProblemTotal = 0, iMiscellaneousTotal = 0;
            string strOldConsultDR = returnDataTable.Rows[0]["CNSLT_RequestedConsultantDR"].ToString().Trim();
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strNewConsultDR = returnDataTable.Rows[iRowCount]["CNSLT_RequestedConsultantDR"].ToString().Trim();
                if (strNewConsultDR == strOldConsultDR)
                {
                    string strCommReason = returnDataTable.Rows[iRowCount]["CommReason"].ToString().Trim();
                    string strCount = returnDataTable.Rows[iRowCount]["CnsltCount"].ToString().Trim();
                    int iCount = 0;
                    if (strCount.Length > 0)
                    {
                        iCount = Convert.ToInt32(strCount);
                    }
                    switch (strCommReason)
                    {
                        case "RR":
                            iRecheckRequests += iCount;
                            break;
                        case "AOV":
                            iAdditionalTest += iCount;
                            break;
                        case "AC":
                            iReportProblem += iCount;
                            break;
                        case "OTH":
                            iMiscellaneous += iCount;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    string strConsultantName = returnDataTable.Rows[iRowCount - 1]["Consultant"].ToString().Trim();
                    dtCommByConsultantsSummary.Rows.Add(strConsultantName, strRequested, iRecheckRequests, iAdditionalTest, iReportProblem, iMiscellaneous);
                    iRecheckRequestsTotal += iRecheckRequests;
                    iAdditionalTestTotal += iAdditionalTest;
                    iReportProblemTotal += iReportProblem;
                    iMiscellaneousTotal += iMiscellaneous;

                    iRecheckRequests = 0; iAdditionalTest = 0; iReportProblem = 0; iMiscellaneous = 0;
                    iRowCount--;
                }
                strOldConsultDR = strNewConsultDR;
            }
            string strConsultantName1 = returnDataTable.Rows[returnDataTable.Rows.Count - 1]["Consultant"].ToString().Trim();
            dtCommByConsultantsSummary.Rows.Add(strConsultantName1, strRequested, iRecheckRequests, iAdditionalTest, iReportProblem, iMiscellaneous);
            iRecheckRequestsTotal += iRecheckRequests;
            iAdditionalTestTotal += iAdditionalTest;
            iReportProblemTotal += iReportProblem;
            iMiscellaneousTotal += iMiscellaneous;

            string[] intarrGrandTotal = new string[5];
            intarrGrandTotal[0] = "Totals " + strRequested;
            intarrGrandTotal[1] = iRecheckRequestsTotal.ToString();
            intarrGrandTotal[2] = iAdditionalTestTotal.ToString();
            intarrGrandTotal[3] = iReportProblemTotal.ToString();
            intarrGrandTotal[4] = iMiscellaneousTotal.ToString();
            Array.Copy(intarrGrandTotal, arrGrandTotal, 5);
        }
        return dtCommByConsultantsSummary;
    }

    public static DataTable CommunicationsByConsultantsDetails(string strQueryString, out string[] arrGrandTotal)
    {
        DataTable dtCommByConsultantsSummary = new DataTable();
        String[] QS = strQueryString.Split('^');
        arrGrandTotal = new string[5];
        dtCommByConsultantsSummary = GetCommByConsultantsSummaryTable(true);
        DataTable returnDataTable = DL_ConsultRecorder.GetCommunicationsByConsultantsSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            string strRequested = QS[3] + " to " + QS[4];
            int iRecheckRequests = 0, iAdditionalTest = 0, iReportProblem = 0, iMiscellaneous = 0;
            int iRecheckRequestsTotal = 0, iAdditionalTestTotal = 0, iReportProblemTotal = 0, iMiscellaneousTotal = 0;
            string strOldConsultDR = returnDataTable.Rows[0]["CNSLT_RequestedConsultantDR"].ToString().Trim();
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strNewConsultDR = returnDataTable.Rows[iRowCount]["CNSLT_RequestedConsultantDR"].ToString().Trim();
                if (strNewConsultDR == strOldConsultDR)
                {
                    string strCommReason = returnDataTable.Rows[iRowCount]["CommReason"].ToString().Trim();
                    string strCount = returnDataTable.Rows[iRowCount]["CnsltCount"].ToString().Trim();
                    int iCount = 0;
                    if (strCount.Length > 0)
                    {
                        iCount = Convert.ToInt32(strCount);
                    }
                    switch (strCommReason)
                    {
                        case "RR":
                            iRecheckRequests += iCount;
                            break;
                        case "AOV":
                            iAdditionalTest += iCount;
                            break;
                        case "AC":
                            iReportProblem += iCount;
                            break;
                        case "OTH":
                            iMiscellaneous += iCount;
                            break;
                        default:
                            break;
                    }
                }
                else
                {
                    string strConsultantName = returnDataTable.Rows[iRowCount - 1]["Consultant"].ToString().Trim();
                    if (strOldConsultDR.Trim().Length > 0)
                    {
                        DataTable dtDetailsTable = GetCommByConsultantsDetailsTable(strOldConsultDR, QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
                        int Total = iRecheckRequests + iAdditionalTest + iReportProblem + iMiscellaneous;
                        dtCommByConsultantsSummary.Rows.Add(strConsultantName, strRequested, iRecheckRequests, iAdditionalTest, iReportProblem, iMiscellaneous, Total, dtDetailsTable);
                    }
                    iRecheckRequestsTotal += iRecheckRequests;
                    iAdditionalTestTotal += iAdditionalTest;
                    iReportProblemTotal += iReportProblem;
                    iMiscellaneousTotal += iMiscellaneous;

                    iRecheckRequests = 0; iAdditionalTest = 0; iReportProblem = 0; iMiscellaneous = 0;
                    iRowCount--;
                }
                strOldConsultDR = strNewConsultDR;
            }
            string strConsultantName1 = returnDataTable.Rows[returnDataTable.Rows.Count - 1]["Consultant"].ToString().Trim();
            if (strOldConsultDR.Trim().Length > 0)
            {
                DataTable dtDetailsTable = GetCommByConsultantsDetailsTable(strOldConsultDR, QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
                int Total = iRecheckRequests + iAdditionalTest + iReportProblem + iMiscellaneous;
                dtCommByConsultantsSummary.Rows.Add(strConsultantName1, strRequested, iRecheckRequests, iAdditionalTest, iReportProblem, iMiscellaneous, Total, dtDetailsTable);
            }
            
            iRecheckRequestsTotal += iRecheckRequests;
            iAdditionalTestTotal += iAdditionalTest;
            iReportProblemTotal += iReportProblem;
            iMiscellaneousTotal += iMiscellaneous;

            string[] intarrGrandTotal = new string[5];
            intarrGrandTotal[0] = "Totals " + strRequested;
            intarrGrandTotal[1] = iRecheckRequestsTotal.ToString();
            intarrGrandTotal[2] = iAdditionalTestTotal.ToString();
            intarrGrandTotal[3] = iReportProblemTotal.ToString();
            intarrGrandTotal[4] = iMiscellaneousTotal.ToString();
            Array.Copy(intarrGrandTotal, arrGrandTotal, 5);
        }
        return dtCommByConsultantsSummary;
    }

    public static DataTable GetCommByConsultantsDetailsTable(string cosultDR, string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable dtDetailsTable = DL_ConsultRecorder.GetCommByConsultantsDetailsTable(cosultDR, consultNumber, accountNumber, accessionNumber, dateFrom, dateTo, status, method, requestedConsultant, resolvedBy, reasonForConsult, priority, speciality, enteredBy, internalExternal);
        for (int iRowCount = 0; iRowCount < dtDetailsTable.Rows.Count; iRowCount++)
        {
            string strAccn = AddBreakLine(dtDetailsTable.Rows[iRowCount]["Accns"].ToString());
            dtDetailsTable.Rows[iRowCount]["Accns"] = strAccn;
            string strTests = AddBreakLine(dtDetailsTable.Rows[iRowCount]["Tests"].ToString());
            dtDetailsTable.Rows[iRowCount]["Tests"] = strTests;
        }
        return dtDetailsTable;
    }

    public static string AddBreakLine(string strInput)
    {
        string strOutput = string.Empty;
        string[] arrString = strInput.Split(new char[] { ',' });
        for (int iLength = 0; iLength < arrString.Length; iLength++)
        {
            if (iLength == 0)
            {
               strOutput = arrString[iLength];   
            }
            else
            {
                strOutput = strOutput + ",<br/>" + arrString[iLength];
            }
        }
        return strOutput;
    }

    public static DataTable GetCommByConsultantsSummaryTable(Boolean isDetailTable)
    {
        DataTable table = new DataTable();
        table.Columns.Add("Consultant", typeof(string));
        table.Columns.Add("Requested", typeof(string));
        table.Columns.Add("RecheckReq", typeof(int));
        table.Columns.Add("AddTest", typeof(int));
        table.Columns.Add("RptProblem", typeof(int));
        table.Columns.Add("Misc", typeof(int));
        if (isDetailTable)
        {
            table.Columns.Add("Total", typeof(string));
            table.Columns.Add("DetailTable", typeof(DataTable));
        }
        return table;
    }

    public static DataTable ConsultByAccountSummary(string strQueryString, out string strFromDate, out string strToDate, out int[] arrGrandTotal)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtConsultByAccountSummary = new DataTable();
        dtConsultByAccountSummary = GetConsultByAccountSummaryTable(false);
        strFromDate = QS[3];
        strToDate = QS[4];
        arrGrandTotal = new int[8];

        DataTable returnDataTable = DL_ConsultRecorder.ConsultByAccountSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            int iTotalConsult = 0, iDiscrepResult = 0, iResultInterpet = 0, iTreatRecmnd = 0, iTestRecmnd = 0, iReceivedQueue = 0, iReceivedTransfer = 0, iReceivedConsult = 0;
            int iGrandTotalConsult = 0, iGrandDiscrepResult = 0, iGrandResultInterpet = 0, iGrandTreatRecmnd = 0, iGrandTestRecmnd = 0, iGrandReceivedQueue = 0, iGrandReceivedTransfer = 0, iGrandReceivedConsult = 0;


            double dRevenue1 = 0, dRevenue2 = 0, dRevenue3 = 0;
            string strOldAccountNo = returnDataTable.Rows[0]["CLF_CLNUM"].ToString().Trim();
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strNewAccountNo = returnDataTable.Rows[iRowCount]["CLF_CLNUM"].ToString().Trim();
                if (strNewAccountNo.Length > 0)
                {
                    if (strNewAccountNo == strOldAccountNo)
                    {
                        string strConsultReason = returnDataTable.Rows[iRowCount]["CNSLT_ConsultReasonDR"].ToString().Trim();
                        string strRequestedMethod = returnDataTable.Rows[iRowCount]["CNSLT_RequestedMethod"].ToString().Trim();
                        string strCount = returnDataTable.Rows[iRowCount]["CnsltCount"].ToString().Trim();
                        string strClientRevenue = returnDataTable.Rows[iRowCount]["CLIENTREVENUE"].ToString().Trim();
                        int iCount = 0;
                        if (strCount.Length > 0)
                        {
                            iCount = Convert.ToInt32(strCount);
                        }
                        switch (strRequestedMethod)
                        {
                            case "SC":
                                iReceivedConsult += iCount;
                                break;
                            case "DC":
                                iReceivedQueue += iCount;
                                break;
                            case "TC":
                                iReceivedTransfer += iCount; 
                                break;
                            default:
                                break;
                        }
                        switch (strConsultReason)
                        {
                            case "DR":
                                iDiscrepResult += iCount;
                                break;
                            case "RI":
                                iResultInterpet += iCount; 
                                break;
                            case "TRR":
                                iTreatRecmnd += iCount;
                                break;
                            case "TSR":
                                iTestRecmnd += iCount; 
                                break;
                            default:
                                break;
                        }
                        if (strClientRevenue.Length > 0)
                        {
                            string[] arrRevenue = strClientRevenue.Split(new Char[] { '^' });
                            if (arrRevenue.Length > 2)
                            {
                                dRevenue1 += (arrRevenue[0].Length > 0) ? Convert.ToDouble(arrRevenue[0].ToString()) : 0;
                                dRevenue2 += (arrRevenue[1].Length > 0) ? Convert.ToDouble(arrRevenue[1].ToString()) : 0;
                                dRevenue3 += (arrRevenue[2].Length > 0) ? Convert.ToDouble(arrRevenue[2].ToString()) : 0;
                            }
                            else if (arrRevenue.Length > 1)
                            {
                                dRevenue1 += (arrRevenue[0].Length > 0) ? Convert.ToDouble(arrRevenue[0].ToString()) : 0;
                                dRevenue2 += (arrRevenue[1].Length > 0) ? Convert.ToDouble(arrRevenue[1].ToString()) : 0;
                            }
                            else if (arrRevenue.Length > 0)
                            {
                                dRevenue1 += (arrRevenue[0].Length > 0) ? Convert.ToDouble(arrRevenue[0].ToString()) : 0;
                            }
                        }
                    }
                    else
                    {
                        string strCosultDR = returnDataTable.Rows[iRowCount - 1]["CNSLT_ClientDR"].ToString().Trim();
                        string strAccountName = returnDataTable.Rows[iRowCount - 1]["CLF_CLNAM"].ToString().Trim();
                        string strAccountPhoneNumber = returnDataTable.Rows[iRowCount - 1]["CLF_CLPHN"].ToString().Trim();
                        string strRouteStop = returnDataTable.Rows[iRowCount - 1]["CLF_RSTOP"].ToString().Trim();
                        string strSalesTerritory = returnDataTable.Rows[iRowCount - 1]["ST_TerritoryCode"].ToString().Trim();
                        iTotalConsult = iReceivedConsult + iReceivedQueue + iReceivedTransfer;
                        if (iTotalConsult > 0)
                        {
                            dtConsultByAccountSummary.Rows.Add(strOldAccountNo, strAccountName, strAccountPhoneNumber, strRouteStop, strSalesTerritory, iTotalConsult, dRevenue1.ToString("#,###,##0.00"), dRevenue2.ToString("#,###,##0.00"), dRevenue3.ToString("#,###,##0.00"), iDiscrepResult, iResultInterpet, iTreatRecmnd, iTestRecmnd, iReceivedQueue, iReceivedTransfer, iReceivedConsult);
                        }
                        iGrandDiscrepResult += iDiscrepResult;
                        iGrandReceivedConsult += iReceivedConsult;
                        iGrandReceivedQueue += iReceivedQueue;
                        iGrandReceivedTransfer += iReceivedTransfer;
                        iGrandResultInterpet += iResultInterpet;
                        iGrandTreatRecmnd += iTreatRecmnd;
                        iGrandTestRecmnd += iTestRecmnd;
                        iTotalConsult = 0; dRevenue1 = 0; dRevenue2 = 0; dRevenue3 = 0; iDiscrepResult = 0; iResultInterpet = 0; iTreatRecmnd = 0; iTestRecmnd = 0; iReceivedQueue = 0; iReceivedTransfer = 0; iReceivedConsult = 0;
                        iRowCount--;
                    }
                }
                strOldAccountNo = strNewAccountNo;
            }
            int iLastCount = returnDataTable.Rows.Count;
            string strLastCosultDR = returnDataTable.Rows[iLastCount - 1]["CNSLT_ClientDR"].ToString().Trim();
            string strLastAccountName = returnDataTable.Rows[iLastCount - 1]["CLF_CLNAM"].ToString().Trim();
            string strLastAccountPhoneNumber = returnDataTable.Rows[iLastCount - 1]["CLF_CLPHN"].ToString().Trim();
            string strLastRouteStop = returnDataTable.Rows[iLastCount - 1]["CLF_RSTOP"].ToString().Trim();
            string strLastSalesTerritory = returnDataTable.Rows[iLastCount - 1]["ST_TerritoryCode"].ToString().Trim();
            iTotalConsult = iReceivedConsult + iReceivedQueue + iReceivedTransfer;
            if (iTotalConsult > 0)
            {
                dtConsultByAccountSummary.Rows.Add(strOldAccountNo, strLastAccountName, strLastAccountPhoneNumber, strLastRouteStop, strLastSalesTerritory, iTotalConsult, dRevenue1.ToString("#,###,##0.00"), dRevenue2.ToString("#,###,##0.00"), dRevenue3.ToString("#,###,##0.00"), iDiscrepResult, iResultInterpet, iTreatRecmnd, iTestRecmnd, iReceivedQueue, iReceivedTransfer, iReceivedConsult);
            }

            iGrandDiscrepResult += iDiscrepResult;
            iGrandReceivedConsult += iReceivedConsult;
            iGrandReceivedQueue += iReceivedQueue;
            iGrandReceivedTransfer += iReceivedTransfer;
            iGrandResultInterpet += iResultInterpet;
            iGrandTreatRecmnd += iTreatRecmnd;
            iGrandTestRecmnd += iTestRecmnd;

            iGrandTotalConsult = iGrandReceivedQueue + iGrandReceivedTransfer + iGrandReceivedConsult;
            int[] intarrGrandTotal = new int[8];
            intarrGrandTotal[0] = iGrandTotalConsult;
            intarrGrandTotal[1] = iGrandDiscrepResult;
            intarrGrandTotal[2] = iGrandResultInterpet;
            intarrGrandTotal[3] = iGrandTreatRecmnd;
            intarrGrandTotal[4] = iGrandTestRecmnd;
            intarrGrandTotal[5] = iGrandReceivedQueue;
            intarrGrandTotal[6] = iGrandReceivedTransfer;
            intarrGrandTotal[7] = iGrandReceivedConsult;
            Array.Copy(intarrGrandTotal, arrGrandTotal, 8);
        }
        return dtConsultByAccountSummary;
    }

    public static DataTable ConsultByAccountDetails(string strQueryString, out string strFromDate, out string strToDate, out int[] arrGrandTotal)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtConsultByAccountSummary = new DataTable();
        dtConsultByAccountSummary = GetConsultByAccountSummaryTable(true);
        strFromDate = QS[3];
        strToDate = QS[4];
        arrGrandTotal = new int[8];

        DataTable returnDataTable = DL_ConsultRecorder.ConsultByAccountSummary(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            int iTotalConsult = 0, iDiscrepResult = 0, iResultInterpet = 0, iTreatRecmnd = 0, iTestRecmnd = 0, iReceivedQueue = 0, iReceivedTransfer = 0, iReceivedConsult = 0;
            int iGrandTotalConsult = 0, iGrandDiscrepResult = 0, iGrandResultInterpet = 0, iGrandTreatRecmnd = 0, iGrandTestRecmnd = 0, iGrandReceivedQueue = 0, iGrandReceivedTransfer = 0, iGrandReceivedConsult = 0;


            double dRevenue1 = 0, dRevenue2 = 0, dRevenue3 = 0;
            string strOldAccountNo = returnDataTable.Rows[0]["CLF_CLNUM"].ToString().Trim();
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strNewAccountNo = returnDataTable.Rows[iRowCount]["CLF_CLNUM"].ToString().Trim();
                if (strNewAccountNo.Length > 0)
                {
                    if (strNewAccountNo == strOldAccountNo)
                    {
                        string strConsultReason = returnDataTable.Rows[iRowCount]["CNSLT_ConsultReasonDR"].ToString().Trim();
                        string strRequestedMethod = returnDataTable.Rows[iRowCount]["CNSLT_RequestedMethod"].ToString().Trim();
                        string strCount = returnDataTable.Rows[iRowCount]["CnsltCount"].ToString().Trim();
                        string strClientRevenue = returnDataTable.Rows[iRowCount]["CLIENTREVENUE"].ToString().Trim();
                        int iCount = 0;
                        if (strCount.Length > 0)
                        {
                            iCount = Convert.ToInt32(strCount);
                        }
                        switch (strRequestedMethod)
                        {
                            case "SC":
                                iReceivedConsult += iCount;
                                break;
                            case "DC":
                                iReceivedQueue += iCount;
                                break;
                            case "TC":
                                iReceivedTransfer += iCount;
                                break;
                            default:
                                break;
                        }
                        switch (strConsultReason)
                        {
                            case "DR":
                                iDiscrepResult += iCount;
                                break;
                            case "RI":
                                iResultInterpet += iCount;
                                break;
                            case "TRR":
                                iTreatRecmnd += iCount;
                                break;
                            case "TSR":
                                iTestRecmnd += iCount;
                                break;
                            default:
                                break;
                        }
                        if (strClientRevenue.Length > 0)
                        {
                            string[] arrRevenue = strClientRevenue.Split(new Char[] { '^' });
                            if (arrRevenue.Length > 2)
                            {
                                dRevenue1 += (arrRevenue[0].Length > 0) ? Convert.ToDouble(arrRevenue[0].ToString()) : 0;
                                dRevenue2 += (arrRevenue[1].Length > 0) ? Convert.ToDouble(arrRevenue[1].ToString()) : 0;
                                dRevenue3 += (arrRevenue[2].Length > 0) ? Convert.ToDouble(arrRevenue[2].ToString()) : 0;
                            }
                            else if (arrRevenue.Length > 1)
                            {
                                dRevenue1 += (arrRevenue[0].Length > 0) ? Convert.ToDouble(arrRevenue[0].ToString()) : 0;
                                dRevenue2 += (arrRevenue[1].Length > 0) ? Convert.ToDouble(arrRevenue[1].ToString()) : 0;
                            }
                            else if (arrRevenue.Length > 0)
                            {
                                dRevenue1 += (arrRevenue[0].Length > 0) ? Convert.ToDouble(arrRevenue[0].ToString()) : 0;
                            }
                        }
                    }
                    else
                    {
                        string strCosultDR = returnDataTable.Rows[iRowCount - 1]["CNSLT_ClientDR"].ToString().Trim();
                        string strAccountName = returnDataTable.Rows[iRowCount - 1]["CLF_CLNAM"].ToString().Trim();
                        string strAccountPhoneNumber = returnDataTable.Rows[iRowCount - 1]["CLF_CLPHN"].ToString().Trim();
                        string strRouteStop = returnDataTable.Rows[iRowCount - 1]["CLF_RSTOP"].ToString().Trim();
                        string strSalesTerritory = returnDataTable.Rows[iRowCount - 1]["ST_TerritoryCode"].ToString().Trim();
                        iTotalConsult = iReceivedConsult + iReceivedQueue + iReceivedTransfer;
                        if (iTotalConsult > 0)
                        {
                            DataTable dtDatailsTable = GetConsultByAccountDetailsTable(strCosultDR, QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
                            dtConsultByAccountSummary.Rows.Add(strOldAccountNo, strAccountName, strAccountPhoneNumber, strRouteStop, strSalesTerritory, iTotalConsult, dRevenue1.ToString("#,###,##0.00"), dRevenue2.ToString("#,###,##0.00"), dRevenue3.ToString("#,###,##0.00"), iDiscrepResult, iResultInterpet, iTreatRecmnd, iTestRecmnd, iReceivedQueue, iReceivedTransfer, iReceivedConsult, dtDatailsTable);
                        }
                        iGrandDiscrepResult += iDiscrepResult;
                        iGrandReceivedConsult += iReceivedConsult;
                        iGrandReceivedQueue += iReceivedQueue;
                        iGrandReceivedTransfer += iReceivedTransfer;
                        iGrandResultInterpet += iResultInterpet;
                        iGrandTreatRecmnd += iTreatRecmnd;
                        iGrandTestRecmnd += iTestRecmnd;
                        iTotalConsult = 0; dRevenue1 = 0; dRevenue2 = 0; dRevenue3 = 0; iDiscrepResult = 0; iResultInterpet = 0; iTreatRecmnd = 0; iTestRecmnd = 0; iReceivedQueue = 0; iReceivedTransfer = 0; iReceivedConsult = 0;
                        iRowCount--;
                    }
                }
                strOldAccountNo = strNewAccountNo;
            }
            int iLastCount = returnDataTable.Rows.Count;
            string strLastCosultDR = returnDataTable.Rows[iLastCount - 1]["CNSLT_ClientDR"].ToString().Trim();
            string strLastAccountName = returnDataTable.Rows[iLastCount - 1]["CLF_CLNAM"].ToString().Trim();
            string strLastAccountPhoneNumber = returnDataTable.Rows[iLastCount - 1]["CLF_CLPHN"].ToString().Trim();
            string strLastRouteStop = returnDataTable.Rows[iLastCount - 1]["CLF_RSTOP"].ToString().Trim();
            string strLastSalesTerritory = returnDataTable.Rows[iLastCount - 1]["ST_TerritoryCode"].ToString().Trim();
            iTotalConsult = iReceivedConsult + iReceivedQueue + iReceivedTransfer;
            if (iTotalConsult > 0)
            {
                DataTable dtLastDatailsTable = GetConsultByAccountDetailsTable(strLastCosultDR, QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
                dtConsultByAccountSummary.Rows.Add(strOldAccountNo, strLastAccountName, strLastAccountPhoneNumber, strLastRouteStop, strLastSalesTerritory, iTotalConsult, dRevenue1.ToString("#,###,##0.00"), dRevenue2.ToString("#,###,##0.00"), dRevenue3.ToString("#,###,##0.00"), iDiscrepResult, iResultInterpet, iTreatRecmnd, iTestRecmnd, iReceivedQueue, iReceivedTransfer, iReceivedConsult, dtLastDatailsTable);
            }

            iGrandDiscrepResult += iDiscrepResult;
            iGrandReceivedConsult += iReceivedConsult;
            iGrandReceivedQueue += iReceivedQueue;
            iGrandReceivedTransfer += iReceivedTransfer;
            iGrandResultInterpet += iResultInterpet;
            iGrandTreatRecmnd += iTreatRecmnd;
            iGrandTestRecmnd += iTestRecmnd;

            iGrandTotalConsult = iGrandReceivedQueue + iGrandReceivedTransfer + iGrandReceivedConsult;
            int[] intarrGrandTotal = new int[8];
            intarrGrandTotal[0] = iGrandTotalConsult;
            intarrGrandTotal[1] = iGrandDiscrepResult;
            intarrGrandTotal[2] = iGrandResultInterpet;
            intarrGrandTotal[3] = iGrandTreatRecmnd;
            intarrGrandTotal[4] = iGrandTestRecmnd;
            intarrGrandTotal[5] = iGrandReceivedQueue;
            intarrGrandTotal[6] = iGrandReceivedTransfer;
            intarrGrandTotal[7] = iGrandReceivedConsult;
            Array.Copy(intarrGrandTotal, arrGrandTotal, 8);
        }
        return dtConsultByAccountSummary;
    }

    public static DataTable getRequestedConsultSummaryReport(string strQueryString, out string strFromDate, out string strToDate, out int[] arrGrandTotal)
    {
        String[] QS = strQueryString.Split('^');
        DataTable dtConsultByRequestMethodSummary = new DataTable();
        dtConsultByRequestMethodSummary = GetConsultSummaryRequestedMethodTable();
        strFromDate = QS[3];
        strToDate = QS[4];
        arrGrandTotal = new int[3];

        DataTable returnDataTable = DL_ConsultRecorder.ConsultSummaryRequestedMethod(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
        if (returnDataTable != null && returnDataTable.Rows.Count > 0)
        {
            int iQueueConsultant = 0, iTransferConsultant = 0, iAgentGeneratedConsultant = 0;
            int iQueueConsultantTotal = 0, iTransferConsultantTotal = 0, iAgentGeneratedConsultantTotal = 0;
            string strOldCreateDate = (Convert.ToDateTime(returnDataTable.Rows[0]["CNSLT_EnteredDate"].ToString().Trim())).ToShortDateString();
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                string strNewCreateDate = (Convert.ToDateTime(returnDataTable.Rows[iRowCount]["CNSLT_EnteredDate"].ToString().Trim())).ToShortDateString();
                if (strNewCreateDate.Length > 0)
                {
                    if (strNewCreateDate == strOldCreateDate)
                    {
                        string strRequestedMethod = returnDataTable.Rows[iRowCount]["RequestMethod"].ToString().Trim();
                        string strCount = returnDataTable.Rows[iRowCount]["CnsltCount"].ToString().Trim();
                        int iCount = 0;
                        if (strCount.Length > 0)
                        {
                            iCount = Convert.ToInt32(strCount);
                        }
                        switch (strRequestedMethod)
                        {
                            case "TC":
                                iTransferConsultant += iCount;
                                break;
                            case "DC":
                                iQueueConsultant += iCount;
                                break;
                            case "SC":
                                iAgentGeneratedConsultant += iCount;
                                break;
                        }
                    }
                    else
                    {
                        dtConsultByRequestMethodSummary.Rows.Add(strOldCreateDate, iQueueConsultant, iTransferConsultant, iAgentGeneratedConsultant);
                        iQueueConsultantTotal += iQueueConsultant;
                        iTransferConsultantTotal += iTransferConsultant;
                        iAgentGeneratedConsultantTotal += iAgentGeneratedConsultant;
                        iQueueConsultant = 0; iTransferConsultant = 0; iAgentGeneratedConsultant = 0;
                        iRowCount--;
                    }
                }
                strOldCreateDate = strNewCreateDate;
            }
            dtConsultByRequestMethodSummary.Rows.Add(strOldCreateDate, iQueueConsultant, iTransferConsultant, iAgentGeneratedConsultant);
            iQueueConsultantTotal += iQueueConsultant;
            iTransferConsultantTotal += iTransferConsultant;
            iAgentGeneratedConsultantTotal += iAgentGeneratedConsultant;

            int[] intarrGrandTotal = new int[3];
            intarrGrandTotal[0] = iQueueConsultantTotal;
            intarrGrandTotal[1] = iTransferConsultantTotal;
            intarrGrandTotal[2] = iAgentGeneratedConsultantTotal;
            Array.Copy(intarrGrandTotal, arrGrandTotal, 3);

        }
        return dtConsultByRequestMethodSummary;
    }

    public static DataTable GetConsultByAccountSummaryTable(Boolean isDetailTable)
    {
        DataTable table = new DataTable();
        table.Columns.Add("AccountNo", typeof(string));
        table.Columns.Add("AccountName", typeof(string));
        table.Columns.Add("AccountPhoneNo", typeof(string));
        table.Columns.Add("RouteStop", typeof(string));
        table.Columns.Add("SalesTerritory", typeof(string));
        table.Columns.Add("TotalConsult", typeof(int));
        table.Columns.Add("Revenue1", typeof(string));
        table.Columns.Add("Revenue2", typeof(string));
        table.Columns.Add("Revenue3", typeof(string));
        table.Columns.Add("DiscrepResult", typeof(int));
        table.Columns.Add("ResultInterpet", typeof(int));
        table.Columns.Add("TreatRecmnd", typeof(int));
        table.Columns.Add("TestRecmnd", typeof(int));
        table.Columns.Add("ReceivedQueue", typeof(int));
        table.Columns.Add("ReceivedTransfer", typeof(int));
        table.Columns.Add("ReceivedConsult", typeof(int));
        if (isDetailTable)
        {
            table.Columns.Add("DetailTable", typeof(DataTable));
        }
        return table;
    }

    public static DataTable GetConsultByUserAgentSummaryTable(Boolean isDetailTable)
    {
        DataTable table = new DataTable();
        table.Columns.Add("UserEntered", typeof(string));
        table.Columns.Add("Requested", typeof(string));
        table.Columns.Add("Consult", typeof(int));
        table.Columns.Add("Transfer", typeof(int));
        table.Columns.Add("AgentGenerated", typeof(int));
        table.Columns.Add("Queue", typeof(int));
        table.Columns.Add("Internal", typeof(int));
        table.Columns.Add("External", typeof(int));
        table.Columns.Add("AvianExotic", typeof(int));
        if (isDetailTable)
        {
            table.Columns.Add("DetailTable", typeof(DataTable));
        }
        return table;
    }

    public static DataTable GetConsultSummaryIESTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Date", typeof(string));
        table.Columns.Add("InternalConsult", typeof(string));
        table.Columns.Add("ExternalConsult", typeof(string));
        table.Columns.Add("AvianExotic", typeof(string));
        table.Columns.Add("Behavior", typeof(string));
        table.Columns.Add("Cardio", typeof(int));
        table.Columns.Add("Endo", typeof(string));
        table.Columns.Add("Gastro", typeof(string));
        table.Columns.Add("IntMed", typeof(string));
        table.Columns.Add("Neuro", typeof(int));
        return table;
    }

    public static DataTable GetConsultSummaryRequestedMethodTable()
    {
        DataTable table = new DataTable();
        table.Columns.Add("Date", typeof(string));
        table.Columns.Add("QueueConsult", typeof(string));
        table.Columns.Add("TransferConsult", typeof(string));
        table.Columns.Add("AgentGeneratedConsult", typeof(string));
        return table;
    }

    public static DataTable GetConsultByAccountDetailsTable(string strCosultDR, string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority,string speciality, string enteredBy, string internalExternal)
    {
        DataTable dtDatailsTable = new DataTable();
        dtDatailsTable = DL_ConsultRecorder.ConsultByAccountDetails(strCosultDR, consultNumber, accountNumber, accessionNumber, dateFrom, dateTo, status, method, requestedConsultant, resolvedBy, reasonForConsult, priority,speciality ,enteredBy ,internalExternal);
        if (dtDatailsTable != null && dtDatailsTable.Rows.Count > 0)
        {
            for (int iRowCount = 0; iRowCount < dtDatailsTable.Rows.Count; iRowCount++)
            {
                string strReqCons = dtDatailsTable.Rows[iRowCount]["ConsultaneName"].ToString().Trim();
                string strRequestedMethod = dtDatailsTable.Rows[iRowCount]["RequestMethod"].ToString().Trim();
                if (strReqCons.Length == 0)
                {
                    dtDatailsTable.Rows[iRowCount]["ConsultaneName"] = "N/A";
                }
                if (strRequestedMethod.Length > 0)
                {
                    int iSpacePosition = strRequestedMethod.IndexOf(" ");
                    if (iSpacePosition > 0)
                    {
                        dtDatailsTable.Rows[iRowCount]["RequestMethod"] = strRequestedMethod.Substring(0, iSpacePosition);
                    }
                }
            }
        }
        return dtDatailsTable;
    }

    public static DataTable GetConsultByUserAgentDetailsTable(string strUserDR, string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable dtDatailsTable = new DataTable();
        dtDatailsTable = DL_ConsultRecorder.ConsultByUserAgentDetails(strUserDR, consultNumber, accountNumber, accessionNumber, dateFrom, dateTo, status, method, requestedConsultant, resolvedBy, reasonForConsult, priority, speciality, enteredBy, internalExternal);
        if (dtDatailsTable != null && dtDatailsTable.Rows.Count > 0)
        {
            for (int iRowCount = 0; iRowCount < dtDatailsTable.Rows.Count; iRowCount++)
            {
                string strReqCons = dtDatailsTable.Rows[iRowCount]["UserEntered"].ToString().Trim();
                string strRequestedMethod = dtDatailsTable.Rows[iRowCount]["RequestMethod"].ToString().Trim();
                string strType = dtDatailsTable.Rows[iRowCount]["Type"].ToString().Trim();
                switch (strType)
                {
                    case "I":
                        strType = "Internal";
                        break;
                    case "O":
                        strType = "External";
                        break;
                    default:
                        strType = "Avian/Exotic";
                        break;
                }
                dtDatailsTable.Rows[iRowCount]["Type"] = strType;
                if (strReqCons.Length == 0)
                {
                    dtDatailsTable.Rows[iRowCount]["UserEntered"] = "N/A";
                }
                
                if (strRequestedMethod.Length > 0)
                {
                    int iSpacePosition = strRequestedMethod.IndexOf(" ");
                    if (iSpacePosition > 0)
                    {
                        dtDatailsTable.Rows[iRowCount]["RequestMethod"] = strRequestedMethod.Substring(0, iSpacePosition);
                    }
                }
            }
        }
        return dtDatailsTable;
    }

    public static DataTable GetPhysicianConsultSummaryDetails(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable dtReturn = new DataTable();
        dtReturn.Columns.Add("DateResolved", typeof(string));
        dtReturn.Columns.Add("TotalCount", typeof(int));
        dtReturn.Columns.Add("DetailsTable1", typeof(DataTable));

        DataTable dtDatailsTable = new DataTable();
        dtDatailsTable = DL_ConsultRecorder.getPhysicianConsultSummaryDetails(consultNumber, accountNumber, accessionNumber, dateFrom, dateTo, status, method, requestedConsultant, resolvedBy, reasonForConsult, priority,speciality ,enteredBy ,internalExternal);

        if (dtDatailsTable != null && dtDatailsTable.Rows.Count > 0)
        {
            string strOldDate = Convert.ToDateTime(dtDatailsTable.Rows[0]["CNSLT_DateResolved"]).ToString("MM/dd/yyyy");
            string strNewDate = "";
            int iCount = 0;
            DataTable dtChild = dtDatailsTable.Clone();
            for (int iRowCount = 0; iRowCount < dtDatailsTable.Rows.Count; iRowCount++)
            {
                strNewDate = Convert.ToDateTime(dtDatailsTable.Rows[iRowCount]["CNSLT_DateResolved"]).ToString("MM/dd/yyyy");
                string strRequestedMethod = dtDatailsTable.Rows[iRowCount]["RequestedMethod"].ToString().Trim();
                if (strRequestedMethod.Length > 0)
                {
                    int iSpacePosition = strRequestedMethod.IndexOf(" ");
                    if (iSpacePosition > 0)
                    {
                        dtDatailsTable.Rows[iRowCount]["RequestedMethod"] = strRequestedMethod.Substring(0, iSpacePosition);
                    }
                }
                
                if (strNewDate.Equals(strOldDate))
                {
                    dtChild.Rows.Add(dtDatailsTable.Rows[iRowCount][0], dtDatailsTable.Rows[iRowCount][1], dtDatailsTable.Rows[iRowCount][2], dtDatailsTable.Rows[iRowCount][3], dtDatailsTable.Rows[iRowCount][4], dtDatailsTable.Rows[iRowCount][5], dtDatailsTable.Rows[iRowCount][6]);
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

    public static DataTable checkDuplicateConsultEntry(string clientID)
    {
        DataTable dtVerify = ConsultRecorder.verifyExistingRecords(clientID);
        return dtVerify;
    }
    public static DataTable getConsultGridRecordsReport(string strQueryString)
    {
        String[] QS = strQueryString.Split('^');
        return DL_ConsultRecorder.getConsultGridRecordsReport(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);
    }

    public static DataTable getConsultMatrix(String year, String allSpecialty, String specialtyList, String allReason, String reasonList, String allClients, String clientList)
    {
        DataTable dtblCnsltMatrix = new DataTable();
        dtblCnsltMatrix.Columns.Add("TYPE");
        dtblCnsltMatrix.Columns.Add("JAN");
        dtblCnsltMatrix.Columns.Add("FEB");
        dtblCnsltMatrix.Columns.Add("MAR");
        dtblCnsltMatrix.Columns.Add("APR");
        dtblCnsltMatrix.Columns.Add("MAY");
        dtblCnsltMatrix.Columns.Add("JUN");
        dtblCnsltMatrix.Columns.Add("JUL");
        dtblCnsltMatrix.Columns.Add("AUG");
        dtblCnsltMatrix.Columns.Add("SEP");
        dtblCnsltMatrix.Columns.Add("OCT");
        dtblCnsltMatrix.Columns.Add("NOV");
        dtblCnsltMatrix.Columns.Add("DEC");

        string strCnsltMatrix = DL_ConsultRecorder.getConsultMatrix(year, allSpecialty, specialtyList, allReason, reasonList, allClients, clientList);

        string[] arrDetails = strCnsltMatrix.Split(new char[] { '*' });

        for (int intCnt = 0; intCnt < arrDetails.Length; intCnt++)
        {
            string strDetails = arrDetails[intCnt].Trim();
            if (strDetails.Length == 0)
            {
                continue;
            }

            string[] arrRowData = strDetails.Split(new char[] { '^' });

            for (int intRowCnt = 0; intRowCnt < arrRowData.Length; intRowCnt++)
            {
                string strRow = arrRowData[intRowCnt].Trim();

                string[] arrColumn = strRow.Split(new char[] { '~' });

                DataRow drwoNewRow = dtblCnsltMatrix.NewRow();
                drwoNewRow["TYPE"] = "";
                drwoNewRow["JAN"] = "0";
                drwoNewRow["FEB"] = "0";
                drwoNewRow["MAR"] = "0";
                drwoNewRow["APR"] = "0";
                drwoNewRow["MAY"] = "0";
                drwoNewRow["JUN"] = "0";
                drwoNewRow["JUL"] = "0";
                drwoNewRow["AUG"] = "0";
                drwoNewRow["SEP"] = "0";
                drwoNewRow["OCT"] = "0";
                drwoNewRow["NOV"] = "0";
                drwoNewRow["DEC"] = "0";

                if (arrColumn.Length > 0)
                {
                    drwoNewRow[0] = arrColumn[0];
                    for (int intColCnt = 1; intColCnt < arrColumn.Length; intColCnt++)
                    {
                        string[] arrCell = arrColumn[intColCnt].Split(new char[] { '!' });
                        int intColIndex = Convert.ToInt32(arrCell[0]);
                        drwoNewRow[intColIndex] = (arrCell[1].Length == 0 ? "0" : arrCell[1]);
                    }
                }

                dtblCnsltMatrix.Rows.Add(drwoNewRow);
            }

            if (intCnt < arrDetails.Length - 1)
            {
                DataRow drwoNewBlankRow = dtblCnsltMatrix.NewRow();
                drwoNewBlankRow["TYPE"] = "";
                drwoNewBlankRow["JAN"] = "";
                drwoNewBlankRow["FEB"] = "";
                drwoNewBlankRow["MAR"] = "";
                drwoNewBlankRow["APR"] = "";
                drwoNewBlankRow["MAY"] = "";
                drwoNewBlankRow["JUN"] = "";
                drwoNewBlankRow["JUL"] = "";
                drwoNewBlankRow["AUG"] = "";
                drwoNewBlankRow["SEP"] = "";
                drwoNewBlankRow["OCT"] = "";
                drwoNewBlankRow["NOV"] = "";
                drwoNewBlankRow["DEC"] = "";
                dtblCnsltMatrix.Rows.Add(drwoNewBlankRow);
            }
        }

        return dtblCnsltMatrix;
    }

    public static DataTable getConsultOverviewRpt(string strQS)
    {
        String[] QS = strQS.Split('^');
        DataTable dtblRawData = DL_ConsultRecorder.getConsultOverview(QS[0], QS[1], QS[2], QS[3], QS[4], QS[5], QS[6], QS[7], QS[8], QS[9], QS[10], QS[11], QS[12], QS[13]);

        DataTable dtblReport = new DataTable();

        dtblReport.Columns.Add("ConsultantName", typeof(string));
        dtblReport.Columns.Add("RequestedDate", typeof(string));
        dtblReport.Columns.Add("CompletedCnslts", typeof(int));
        dtblReport.Columns.Add("ReceivedViaQueue", typeof(int));
        dtblReport.Columns.Add("ReceivedViaTransfer", typeof(int));
        dtblReport.Columns.Add("ReceivedViaConsult", typeof(int));

        if (dtblRawData != null && dtblRawData.Rows.Count > 0)
        {

            string strOldCnsltntRow = string.Empty;
            string strCnsltntName = string.Empty;
            string strOldDate = string.Empty;
            string strCnsltntNameOld = string.Empty;

            int intViaQueue = 0, intViaTransfer = 0, intViaCnslt = 0;
            int intTotalViaQueue = 0, intTotalTransfer = 0, intTotalViaCnslt = 0;
            int intTotalConsult = 0;

            String dateFormat = AtlasIndia.AntechCSM.UI.UIfunctions.getDateFormat();
            string strFirstDate = ""; string strDate = "";
            int recCnt = 0;


            for (int intCnt = 0; intCnt < dtblRawData.Rows.Count; intCnt++)
            {
                string strCnsltntRow = dtblRawData.Rows[intCnt]["CNSLT_ResolvedByConsultantDR"].ToString();

                if (strCnsltntRow.Length == 0)
                {
                    continue;
                }
                string strCnsltType = dtblRawData.Rows[intCnt]["RequestedMethod"].ToString();
                strDate = Convert.ToDateTime(dtblRawData.Rows[intCnt]["DateResolved"]).ToString(dateFormat);
                strCnsltntName = dtblRawData.Rows[intCnt]["ConsultantName"].ToString();
                int count = Convert.ToInt32(dtblRawData.Rows[intCnt]["CnsltCount"]);
                if (strCnsltntRow.Equals(strOldCnsltntRow) || strOldCnsltntRow.Length == 0)
                {
                    if (strDate.Equals(strOldDate) || strOldDate.Length == 0)
                    {
                        switch (strCnsltType)
                        {
                            case "SC":
                                intViaCnslt += count;
                                break;
                            case "DC":
                                intViaQueue += count;
                                break;
                            case "TC":
                                intViaTransfer += count;
                                break;
                        }
                    }
                    else
                    {
                        if (recCnt == 0)
                        {
                            strFirstDate = strOldDate;
                        }
                        else
                        {
                            strCnsltntNameOld = "";
                        }
                        int iTotalCount = intViaQueue + intViaTransfer + intViaCnslt;
                        if (iTotalCount > 0)
                        {
                            addRecRow(ref dtblReport, iTotalCount, intViaQueue, intViaTransfer, intViaCnslt, strCnsltntNameOld, strOldDate);

                            intTotalViaQueue += intViaQueue;
                            intTotalTransfer += intViaTransfer;
                            intTotalViaCnslt += intViaCnslt;
                            intTotalConsult += iTotalCount;

                            intViaQueue = 0; intViaTransfer = 0; intViaCnslt = 0;
                            intCnt--;
                            recCnt++;
                        }
                    }
                    strOldDate = strDate;
                }
                else
                {
                    // Adding the last row for the last consultant
                    int iTotalCount = intViaQueue + intViaCnslt + intViaTransfer;
                    if (iTotalCount > 0)
                    {
                        if (recCnt == 0)
                        {
                            strFirstDate = strOldDate;
                        }
                        else
                        {
                            strCnsltntNameOld = "";
                        }
                        addRecRow(ref dtblReport, iTotalCount, intViaQueue, intViaTransfer, intViaCnslt, strCnsltntNameOld, strOldDate);

                        intTotalViaQueue += intViaQueue;
                        intTotalTransfer += intViaTransfer;
                        intTotalViaCnslt += intViaCnslt;
                        intTotalConsult += iTotalCount;
                        intViaQueue = 0; intViaTransfer = 0; intViaCnslt = 0;
                    }
                    // Adding the total count row for the consultant
                    addTotalRow(ref dtblReport, strFirstDate, strOldDate, intTotalConsult, intTotalViaQueue, intTotalTransfer, intTotalViaCnslt);

                    intTotalViaQueue = 0; intTotalTransfer = 0; intTotalViaCnslt = 0; intTotalConsult = 0;
                    intCnt--;
                    recCnt = 0;
                    strOldDate = "";
                }
                strOldCnsltntRow = strCnsltntRow;
                strCnsltntNameOld = strCnsltntName;
            }

            int iLastTotalCount = intViaQueue + intViaCnslt + intViaTransfer;

            if (recCnt == 0)
            {
                strFirstDate = strOldDate;
            }
            else
            {
                strCnsltntNameOld = "";
            }

            addRecRow(ref dtblReport, iLastTotalCount, intViaQueue, intViaTransfer, intViaCnslt, strCnsltntNameOld, strOldDate);

            intTotalViaQueue += intViaQueue;
            intTotalTransfer += intViaTransfer;
            intTotalViaCnslt += intViaCnslt;
            intTotalConsult += iLastTotalCount;

            if (intTotalConsult > 0)
            {
                addTotalRow(ref dtblReport, strFirstDate, strOldDate, intTotalConsult, intTotalViaQueue, intTotalTransfer, intTotalViaCnslt);
            }
        }
        return dtblReport;
    }

    private static void addRecRow(ref DataTable dtFinal, int iTotalCount, int intViaQueue, int intViaTransfer, int intViaCnslt, string strCnsltName, string strDate)
    {
      DataRow drowNewRow = dtFinal.NewRow();
      drowNewRow["ConsultantName"] = strCnsltName;
      drowNewRow["RequestedDate"] = strDate;
      drowNewRow["CompletedCnslts"] = iTotalCount;
      drowNewRow["ReceivedViaQueue"] = intViaQueue;
      drowNewRow["ReceivedViaTransfer"] = intViaTransfer;
      drowNewRow["ReceivedViaConsult"] = intViaCnslt;
      dtFinal.Rows.Add(drowNewRow);
    }

    private static void addTotalRow(ref DataTable dtFinal, string strFirstDate, string strLastDate, int intTotalConsult, int intTotalViaQueue, int intTotalTransfer, int intTotalViaCnslt)
    {
        DataRow drowNewRow = dtFinal.NewRow();
        drowNewRow["ConsultantName"] = "TOTAL";
        drowNewRow["RequestedDate"] = "Total " + strFirstDate + " to " + strLastDate;
        drowNewRow["CompletedCnslts"] = intTotalConsult;
        drowNewRow["ReceivedViaQueue"] = intTotalViaQueue;
        drowNewRow["ReceivedViaTransfer"] = intTotalTransfer;
        drowNewRow["ReceivedViaConsult"] = intTotalViaCnslt;
        dtFinal.Rows.Add(drowNewRow);
    }

    public static void updateComnReason(string cnsltRow, string reasonForComm, string toGroup, string accessions, string tests)
    {
        DL_ConsultRecorder.updateComnReason(cnsltRow, reasonForComm, toGroup, accessions, tests);
    }

    public static void deleteConsult(string cnsltRow,string user,string reason)
    {
        DL_ConsultRecorder.deleteConsult(cnsltRow,user,reason);
    }

    public static void updateConsultRowStatus(string cnsltRow, string status)
    {
        DL_ConsultRecorder.updateConsultRowStatus(cnsltRow, status);
    }

    public static void updateQueueConsult(string cnsltRow, string status, string consultReason, string personContacted, string note, string user)
    {
        DL_ConsultRecorder.updateQueueConsult(cnsltRow, status, consultReason, personContacted, note, user);
    }
}
