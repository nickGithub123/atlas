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
using System.Collections.Generic;
using AtlasIndia.AntechCSM.UI;

/// <summary>
/// Summary description for DL_TestDeletion
/// </summary>
public class DL_TestDeletion
{
    public DL_TestDeletion()
    {
        //
    }

    //AM Issue#38926 05/26/2008 Build Number 1.0.0.9
    public static DataTable getReason(string DepartmentID)
    {
        DataTable returnData = new DataTable();
        string selectStatement = "SELECT DRDEL_ReasonDR->RDEL_ReasonText ReasonText, DRDEL_ReasonDR->RDEL_ReasonID ReasonID FROM DIC_DepartmentReasonList WHERE DIC_DepartmentReasonList.DRDEL_DEPT_ParRef = '" + DepartmentID + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnData = cache.FillCacheDataTable(selectStatement);
        return returnData;
    }

    public static DataTable getReason()
    {
        DataTable returnData = new DataTable();
        string selectStatement = "SELECT RDEL_ReasonText ReasonText, RDEL_ReasonID ReasonID FROM DIC_ReasonForDeletion ORDER BY ReasonText";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnData = cache.FillCacheDataTable(selectStatement);
        return returnData;
    }
    #region Unused code
    /*
    public DataTable getAccessionDetails(string AccessionNumber)
    {
        // ACC_Accession
        // ACC_PatientName
        // ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM
        // ACC_ClientDR->CLF_CLMNE AS AccountMnemonic
        // ACC_ClientDR->CLF_CLNAM AS ACCOUNTNAME
        // ACC_ClientDR->CLF_CLPHN As PHONE
        // ACC_ClientDR->CLF_CLAD1 AS ADDRESS1
        // ACC_ClientDR->CLF_CLAD2 AS ADDRESS2
        // ACC_ClientDR->CLF_CLTER AS SalesTerritory
        // 'SALES REP TEST DATA 1' AS SalesRepresentative
        // ACC_ClientDR->CLF_AGNO AS AUTODIALGR
        // zoasis_num AS ZOASIS
        // ACC_ClientDR->CLF_CLRTS AS RouteStop
        // ACC_ClientDR->CLF_LabLocationDR->LABLO_LabName AS LOCATION
        // ACC_CollectionDateText As ORDERDATE
        // ACC_TestsOrderedDisplayString AS TESTSORDERED
        // 'ReportStatus' As ReportStatus
        // 'S' As StatDesignation
        // zoasis.CLN_RowID

        string selectStatement = "SELECT  DISTINCT UC_UnitCode AS TESTCODE, UC_DisplayReportingTitle AS TESTNAME, ACC_ClientDR->CLF_LabLocationDR AS LABCODE FROM ORD_Accession ";
        //selectStatement = selectStatement + " left outer join zoasis on ACC_ClientDR->CLF_CLNUM = zoasis.CLN_RowID";
        selectStatement = selectStatement + " left outer join ORD_AccessionTestOrdered on ACC_Accession=ORD_AccessionTestOrdered.ATO_ACC_ParRef";
        selectStatement = selectStatement + " left outer join DIC_UnitCode on ATO_UnitCodeDR=DIC_UnitCode.UC_RowID";
        selectStatement = selectStatement + " left outer join DIC_UnitCodeTestCode on UC_RowID=DIC_UnitCodeTestCode.UCTC_UC_ParRef";
        // selectStatement = selectStatement + " left outer join DIC_TestCode on UCTC_TestCodeDR=DIC_TestCode.TC_RowID";

        selectStatement = selectStatement + " WHERE 1=1";

        selectStatement = (AccessionNumber != "" ? selectStatement + " AND ACC_Accession  ='" + AccessionNumber + "'" : selectStatement);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        //DataSet returnDS = cache.StoredProcedure("call SUPAccessionDetails_AccessionSearch(?)", AccessionNumber);
        DataSet returnDS = cache.FillCacheDataSet(selectStatement);
        if (returnDS.Tables.Count > 0)
        {
            return returnDS.Tables[0];
        }
        else
        {
            return null;
        }
    }
    */
    #endregion Unused code
    //AM Issue#32875 03/26/2008 Build Number 1.0.0.9
    public DataTable getTestDeletionDetails(String testDelID)
    {
        DataTable returnDataTable = new DataTable();
        String selectStatement = "SELECT TD_RowID AS TESTDELROWID,ACC_Accession AS ACCESSIONNUM,TD_AccessionDR->ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,LABLO_LabName AS LABLOCATION,TO_DATE(TD_DateEntered,'MM/dd/yyyy') AS DATEENTERED, TD_TimeEntered AS TIMEENTERED,TO_DATE(TD_DatePrinted,'MM/dd/yyyy') AS DATECHECKED,TD_TimePrinted AS TIMECHECKED,TO_DATE(TD_DateProcessed,'MM/dd/yyyy') AS DATEPROCESSED,TD_TimeProcessed AS TIMEPROCESSED,TD_TestsToDelete AS TESTDELETED,TD_ProcessingStatus AS STATUS,TD_DepartmentDR->DEPT_Name AS DEPARTMENT,TD_REASONFORDELETIONDR AS REASON, TD_Comments AS COMMENTS, TD_UserProcessedBy AS PROCESSEDBY, TD_UserPrintedBy AS PRINTEDBY, TD_UserEnteredBy ENTEREDBY";
        selectStatement = selectStatement + " FROM ORD_TestDeletion";
        selectStatement = selectStatement + " left outer join DIC_LabLocation on TD_LabLocationDR=LABLO_RowID";
        selectStatement = selectStatement + " left outer join ORD_Accession on TD_AccessionDR=ACC_Accession";
        selectStatement = selectStatement + " left outer join DIC_ReasonForDeletion on TD_REASONFORDELETIONDR=RDEL_ReasonID";
        selectStatement = selectStatement + " WHERE 1=1 AND TD_RowID ='" + testDelID + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(selectStatement);
        return returnDataTable;
    }

    //AM Issue#32875 03/26/2008 Build Number 1.0.0.9
    public DataTable getTestDeletionDetails(string accountNumber, string accessionNumber, string dateDeletedFrom, string dateDeletedTo, string department, string location, string status, string reason, string EnteredBy, String checkedBy)
    {
        DataTable returnDataTable = new DataTable();
        string selectStatement = "SELECT TD_RowID AS TESTDELROWID,ACC_Accession AS ACCESSIONNUM,TD_AccessionDR->ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,LABLO_LabName AS LABLOCATION,TO_DATE(TD_DateEntered,'MM/dd/yyyy') AS DATETIMESTAMP, TD_TimeEntered AS TIMEENTERED,TD_TestsToDelete AS TESTDELETED,TD_ProcessingStatus AS STATUS, TD_ProcessingStatus AS STATUSCODE,TD_DepartmentDR->DEPT_Name AS DEPARTMENT,TD_REASONFORDELETIONDR AS REASON, $$CO17^XT58(TD_UserPrintedBy) AS PRINTEDBY, $$CO17^XT58(TD_UserEnteredBy) ENTEREDBY";
        selectStatement = selectStatement + " FROM ORD_TestDeletion";
        selectStatement = selectStatement + " left outer join DIC_LabLocation on TD_LabLocationDR=LABLO_RowID";
        selectStatement = selectStatement + " left outer join ORD_Accession on TD_AccessionDR=ACC_Accession";
        selectStatement = selectStatement + " left outer join DIC_ReasonForDeletion on TD_REASONFORDELETIONDR=RDEL_ReasonID";
        selectStatement = selectStatement + " WHERE 1=1";
        selectStatement = (accountNumber != "" ? selectStatement + " AND TD_AccessionDR->ACC_ClientDR->CLF_CLNUM  ='" + accountNumber + "'" : selectStatement);
        selectStatement = (accessionNumber != "" ? selectStatement + " AND ACC_Accession  ='" + accessionNumber + "'" : selectStatement);
        selectStatement = (department != "" ? selectStatement + " AND TD_DepartmentDR  ='" + department + "'" : selectStatement);
        selectStatement = (location != "" ? selectStatement + " AND LABLO_RowID  ='" + location + "'" : selectStatement);
        selectStatement = (reason != "" ? selectStatement + " AND TD_REASONFORDELETIONDR  ='" + reason + "'" : selectStatement);
        selectStatement = (status != "" ? selectStatement + " AND TD_ProcessingStatus  ='" + status + "'" : selectStatement);
        selectStatement = (dateDeletedFrom != "" ? selectStatement + " AND TD_DateEntered>= TO_DATE('" + dateDeletedFrom + "','MM/dd/yyyy') AND TD_DateEntered<= TO_DATE('" + dateDeletedTo + "','MM/dd/yyyy')" : selectStatement);
        selectStatement = (EnteredBy != "" ? selectStatement + " AND %SQLUPPER TD_UserEnteredBy LIKE %SQLUPPER '" + EnteredBy + "'" : selectStatement); //AM Issue#46178 1.0.0.18 10/13/2008 - adding new search criteria 'Entered By'
        if (checkedBy.Length > 0)
        {
            selectStatement = selectStatement + "AND %SQLUPPER TD_UserPrintedBy LIKE %SQLUPPER '" + checkedBy + "'";
        }
        selectStatement = selectStatement + " ORDER BY TD_RowID ASC";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(selectStatement);
        return returnDataTable;
    }

    public static String insertTestDeletion(string Accession, string DateEntered, string Department, string LabLocation, string ReasonForDeletion, string TestsToDeleteString, string Comments, string ProcessingStatus, string UserEnteredBy)
    {
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("Insert into ORD_TestDeletion (TD_AccessionDR,TD_DateEntered,TD_DepartmentDR, TD_LabLocationDR, TD_REASONFORDELETIONDR, TD_TestsToDelete, TD_Comments,TD_ProcessingStatus, TD_UserEnteredBy) VALUES( ");
        //sb.Append("'" + Accession + "'" + ", ");
        //string EnteredDate = "TO_DATE('" + DateEntered + "','MM/DD/YYYY')";
        //sb.Append(EnteredDate + ", ");
        //sb.Append("'" + Department + "'" + ", ");
        //sb.Append("'" + LabLocation + "'" + ", ");
        //sb.Append("'" + ReasonForDeletion + "'" + ", ");
        //sb.Append("'" + TestsToDeleteString + "'" + ", ");
        //sb.Append("'" + Comments + "'" + ", ");
        //sb.Append("'" + ProcessingStatus + "'" + ",");
        //sb.Append("'" + UserEnteredBy + "')");
        //string insertStatement = sb.ToString();

        //CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        //return Convert.ToString(cache.Insert(insertStatement));

        Dictionary<string, string> _testDeletionData = new Dictionary<string, string>();
        _testDeletionData.Add("ACCESSION", Accession);
        _testDeletionData.Add("DEPARTMENT", Department);
        _testDeletionData.Add("LAB", LabLocation);
        _testDeletionData.Add("REASON", ReasonForDeletion);
        _testDeletionData.Add("COMMENTTEXT", Comments);
        _testDeletionData.Add("TESTS", TestsToDeleteString);
        _testDeletionData.Add("USER", UserEnteredBy);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_SaveTestDeletion(?,?,?,?,?,?,?)", _testDeletionData).Value.ToString();

    }
    #region Unused code
    //public static DataTable getMaxCientGram()
    //{
    //    DataTable returnDataTable = new DataTable();
    //    string selectStatement = "SELECT TOP 1 RCG_RowID  AS CLIENTGRAMID, RCG_DateFiled FROM REP_ReportingClientGram ORDER BY RCG_DateTimeFiled DESC";
    //    CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
    //    returnDataTable = cache.FillCacheDataTable(selectStatement);
    //    return returnDataTable;
    //}
    #endregion Unused code
    //AM Issue#46793 AntechCSM 1.0.21.0 10/27/2008
    public static DataTable getSearchResult(String strQS)
    {
        String[] QS = strQS.Split('^');
        DataTable returnDataTable = new DataTable();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("TD_AccessionDR As AccessionNumber, ");
        sb.Append("TD_AccessionDR->ACC_ClientDR->CLF_CLNUM As AccountNumber, ");
        sb.Append("TD_AccessionDR->ACC_ClientDR->CLF_CLADG As AutodialGroups, ");
        sb.Append("TD_AccessionDR->ACC_ClientDR->CLF_CLNAM As AccountName, ");
        sb.Append("TD_AccessionDR->ACC_ClientDR->CLF_CLPHN As PhoneNumber, ");
        sb.Append("TD_AccessionDR->ACC_PetFirstName As OwnerName_PetName, ");
        sb.Append("TD_AccessionDR->ACC_Species As Species, ");
        sb.Append("TD_AccessionDR->ACC_Breed As Breed, ");
        sb.Append("TD_AccessionDR->ACC_AgeDob As Age_Dob, ");
        sb.Append("TO_DATE(TD_DateEntered,'MM/dd/yyyy') As DateCollected, ");
        sb.Append("TD_TimeEntered As TimeCollected, ");
        sb.Append("TD_AccessionDR->ACC_ReportingComments As ReportNotes, ");
        sb.Append("TD_AccessionDR->ACC_NotesAndInstructions As LabMessage, ");
        sb.Append("TD_AccessionDR->ACC_TestsOrderedDisplayString As TestsOrdered, ");
        sb.Append("TD_AccessionDR->ACC_SpecimenSubmitted As SpecimenSubmitted, ");
        sb.Append("TD_DepartmentDR->DEPT_Name As Department, ");
        sb.Append("TD_ReasonForDeletionDR As Reason, ");
        sb.Append("TD_TestsToDelete As TestName, ");
        sb.Append("TD_Comments As Comments, ");
        sb.Append("TD_UserEnteredBy As EnteredBy, ");
        //sb.Append("TD_UserProcessedBy As CheckedBy "); // Commented by AA - Checked by is taken from TD_UserPrintedBy field, see the Test Deletions Edit Page.
        sb.Append("TD_UserPrintedBy As CheckedBy ");
        sb.Append("FROM ");
        sb.Append("ORD_TestDeletion ");
        sb.Append("WHERE 1=1 ");

        if (QS[0].Length > 0) sb.Append(" AND TD_AccessionDR='" + QS[0] + "'");
        if (QS[1].Length > 0) sb.Append(" AND TD_DepartmentDR='" + QS[1] + "'");
        if (QS[2].Length > 0) sb.Append(" AND TD_ReasonForDeletionDR='" + QS[2] + "'");
        if (QS[3].Length > 0) sb.Append(" AND TD_ProcessingStatus='" + QS[3] + "'");
        if (QS[4].Length > 0) sb.Append(" AND TD_LabLocationDR='" + QS[4] + "'");
        if (QS[5].Length > 0 && QS[6].Length > 0)
        {
            sb.Append(" AND TD_DateEntered>= TO_DATE('" + QS[5] + "','MM/DD/YYYY') AND TD_DateEntered<= TO_DATE('" + QS[6] + "','MM/DD/YYYY')");
        }
        if (QS[7].Length > 0) sb.Append(" AND %SQLUPPER TD_UserEnteredBy LIKE %SQLUPPER '" + QS[7] + "'");
        if (QS[8].Length > 0) sb.Append(" AND TD_AccessionDR->ACC_ClientDR->CLF_CLNUM ='" + QS[8] + "'");
        if (QS[9].Length > 0) sb.Append(" AND %SQLUPPER TD_UserPrintedBy LIKE %SQLUPPER '" + QS[9] + "'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static void insertTestDeletion(string Accession, string DateEntered, string Department, string LabLocation, string ReasonForDeletion, string Comments, string TestsToDeleteString, string userID)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        cache.StoredProcedure("call SP_SaveTestDeletion(?,?,?,?,?,?,?,?)", Accession, DateEntered, Department, LabLocation, ReasonForDeletion, Comments, TestsToDeleteString, userID);

        //string returnValue = cache.StoredProcedure("?=call SP_getCurrDate()", Microsoft.Data.Odbc.OdbcType.NVarChar).Value.ToString();
    }

    public static void updateTestDelStatus(string testID, string CheckedOut, string checkedBy)
    {
        String timeFormat = UIfunctions.getTimeFormat();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("Update ORD_TestDeletion set TD_ProcessingStatus = '" + CheckedOut + "', TD_UserPrintedBy = '" + checkedBy + "',TD_DatePrinted='" + DateTime.Now.ToString("yyyy-MM-dd") + "',TD_TimePrinted='" + DateTime.Now.ToString(timeFormat) + "' where TD_RowId = '" + testID + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        int rows = cache.Insert(sb.ToString());
        DataTable dtAccession = cache.FillCacheDataTable("SELECT TD_AccessionDR FROM ORD_TESTDELETION WHERE TD_ROWID = '" + testID + "'");
        if (dtAccession.Rows.Count > 0)
        {
            Dictionary<String, String> _updateTestDelStatus = new Dictionary<String, String>();
            _updateTestDelStatus.Add("ACCESSION", dtAccession.Rows[0][0].ToString());
            _updateTestDelStatus.Add("TESTROWID", testID);
            _updateTestDelStatus.Add("USERID", checkedBy);
            _updateTestDelStatus.Add("CHECKEDOUT", CheckedOut);
            String strTemp = cache.StoredProcedure("?=call SP2_TDCheckInOut(?,?,?,?)", _updateTestDelStatus).Value.ToString();
        }
    }

    public static void updateTestDelStatus(String testID, String processedBy, String CheckedOut, String Accession)
    {
        String timeFormat = UIfunctions.getTimeFormat();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("Update ORD_TestDeletion set TD_ProcessingStatus = 'PROC' ,TD_UserProcessedBy ='" + processedBy + "',TD_DateProcessed='" + DateTime.Now.ToString("yyyy-MM-dd") + "',TD_TimeProcessed='" + DateTime.Now.ToString(timeFormat) + "' where TD_RowId = '" + testID + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        int rows = cache.Insert(sb.ToString());

        Dictionary<String, String> _updateTestDelStatus = new Dictionary<String, String>();
        _updateTestDelStatus.Add("ACCESSION", Accession);
        _updateTestDelStatus.Add("TESTROWID", testID);
        _updateTestDelStatus.Add("USERID", processedBy);
        _updateTestDelStatus.Add("CHECKEDOUT", CheckedOut);
        String strTemp = cache.StoredProcedure("?=call SP2_TDCheckInOut(?,?,?,?)", _updateTestDelStatus).Value.ToString();
    }

    //AM Issue#32877 AntechCSM 1.0.15.0 09/24/2008
    public static void deleteTestDeletion(String TESTROW, String Accession, String UserID)
    {
        Dictionary<String, String> _testDeletionData = new Dictionary<String, String>();
        _testDeletionData.Add("TESTROW", TESTROW);
        _testDeletionData.Add("Accession", Accession);
        _testDeletionData.Add("UserID", UserID);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        String strTemp = cache.StoredProcedure("?=call SP2_DeleteTestDeletion(?,?,?)", _testDeletionData).Value.ToString();
    }

    //AM Issue#32877 AntechCSM 1.0.15.0 09/24/2008
    public static void sendClientgram(String ACCOUNT, String ACCESSION, String USERID, String TESTS, String LAB, String REASON, String AUTODIAL)
    {
        Dictionary<string, string> _sendClientgram = new Dictionary<string, string>();
        _sendClientgram.Add("ACCOUNT", ACCOUNT);
        _sendClientgram.Add("ACCESSION", ACCESSION);
        _sendClientgram.Add("USERID", USERID);
        _sendClientgram.Add("TESTS", TESTS);
        _sendClientgram.Add("LAB", LAB);
        _sendClientgram.Add("REASON", REASON);
        _sendClientgram.Add("AUTODIAL", AUTODIAL);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        String strTemp = cache.StoredProcedure("?=call SP2_SendClientGramTestDeletion(?,?,?,?,?,?,?)", _sendClientgram).Value.ToString();
    }
    public static String verifyTestDeletion(String Reason)
    {
        Dictionary<string, string> _sendClientgram = new Dictionary<string, string>();
        _sendClientgram.Add("REASON", Reason);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_VerifyTestDeletion(?)", _sendClientgram).Value.ToString();
    }
}
