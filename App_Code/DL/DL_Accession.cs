using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Data.Odbc;
using AtlasIndia.AntechCSM.UI;
using System.Collections.Generic;

/// <summary>
/// Data Access Layer of Accession
/// </summary>
public class DL_Accession
{
    public DL_Accession()
    {
    }

    public static Int32 AccessionCount(String accessionNumber)
    {
        String selectStatement = "SELECT COUNT(*) FROM ORD_Accession WHERE ACC_Accession = '" + accessionNumber + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return (Int32)cache.CacheExScalar(selectStatement);
    }

    public static DataTable getExtendedAccessionDetails(string accessionNumber)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_MiniLogDate As MiniLogDate, ");
        sb.Append("ACC_MiniLogTime As MiniLogTime, ");
        sb.Append("ACC_MiniLogInitials As MiniLoggerInitial, ");
        sb.Append("$$CO17^XT58(ACC_MiniLogInitials) As MiniLoggerInitialDispName, ");
        sb.Append("ACC_CollectionDateText As CollectionDateText, ");
        sb.Append("ACC_CollectionTimeHours As CollectionTimeText, ");
        sb.Append("ACC_MaxiLogDate As MaxiLogDate, ");
        sb.Append("ACC_MaxiLogTime As MaxiLogTime, ");
        sb.Append("ACC_MaxilogInitials As MaxiLoggerInitials, ");
        sb.Append("$$CO17^XT58(ACC_MaxilogInitials) As MaxiLoggerInitialsDispName, ");
        sb.Append("'01/01/2001' As ReceivedDate, ");
        sb.Append("'00:00:00' As ReceivedTime, ");
        sb.Append("ACC_IsStat As CriticalInfo, ");
        sb.Append("ACC_MedicalRecordsNumber As ChartNumber, ");
        //sb.Append("ACC_AutoDial As AutoDial, ");
        sb.Append("ACC_ClientDR->CLF_CLADG As AutoDial, ");
        sb.Append("ACC_PetFirstName As PetName, ");
        sb.Append("ACC_ClientDR->CLF_CLNUM As ClientNumber, ");
        sb.Append("ACC_ClientDR->CLF_CLNAM As ClientName, ");
        sb.Append("ACC_ClientDR->CLF_CLMNE As ClientMnemonic, ");
        sb.Append("ACC_PatientName As OwnerName, ");
        sb.Append("ACC_Sex As Sex, ");
        sb.Append("ACC_Species As Species, ");
        sb.Append("ACC_Breed As Breed, ");
        sb.Append("ACC_AgeDob As Age, ");
        sb.Append("ACC_RequestingPhysician As Doctor, ");
        sb.Append("ACC_ClientDR->CLF_CLPHN As DoctorPhone, ");
        sb.Append("ACC_RequesitionNumber As RequisitionNumber, ");
        sb.Append("ACC_BillTo As BillTo, ");
        sb.Append("ACC_RecheckNo As RecheckNumber, ");
        sb.Append("ACC_RequesitionNumber As ZoasisRequest, "); // AM Issue#39680 AntechCSM-0.7 06/26/2008
        sb.Append("ACC_TestsOrderedDisplayString As TestsOrdered, ");
        sb.Append("ACC_SpecimenSubmitted As SpecimenSubmitted, ");
        sb.Append("ACC_NotesAndInstructions As LabMessage, ");
        sb.Append("ACC_ReportingComments As ReportNotes, ");
        sb.Append("ACC_SpecimenAgeInDays As SpecimenAge, ");
        sb.Append("ACC_ClientDR->CLF_TimeZone As ClientTimeZone "); //SSM 20/10/2011 #114978 AntechCSM 2B - Added ClientTimeZone
        sb.Append("FROM ORD_Accession");
        sb.Append(" WHERE ACC_Accession = '" + accessionNumber + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getAccessionDetails(string accessionNumber)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_CollectionDateText As CollectionDateText, ");
        sb.Append("ACC_CollectionTimeHours As CollectionTimeText, ");
        sb.Append("ACC_IsStat As CriticalInfo, ");
        sb.Append("ACC_MedicalRecordsNumber As ChartNumber, ");
        sb.Append("ACC_ClientDR->CLF_CLADG As AutoDial, ");
        //sb.Append("ACC_AutoDial As AutoDial, ");
        sb.Append("ACC_PetFirstName As PetName, ");
        sb.Append("ACC_ClientDR->CLF_CLNUM As ClientNumber, ");
        sb.Append("ACC_ClientDR->CLF_CLNAM As ClientName, ");
        sb.Append("ACC_ClientDR->CLF_CLMNE As ClientMnemonic, ");
        sb.Append("ACC_PatientName As OwnerName, ");
        sb.Append("ACC_Sex As Sex, ");
        sb.Append("ACC_Species As Species, ");
        sb.Append("ACC_Breed As Breed, ");
        sb.Append("ACC_AgeDob As Age, ");
        sb.Append("ACC_RequestingPhysician As Doctor, ");
        sb.Append("ACC_ClientDR->CLF_CLPHN As DoctorPhone, ");
        sb.Append("ACC_RequesitionNumber As RequisitionNumber, ");
        sb.Append("ACC_BillTo As BillTo, ");
        sb.Append("ACC_RecheckNo As RecheckNumber, ");
        sb.Append("ACC_LabRefNum As ZoasisRequest, ");
        sb.Append("ACC_MiniLogDate As MiniLogDate, ");
        sb.Append("ACC_MiniLogTime As MiniLogTime, ");
        sb.Append("ACC_TestsOrderedDisplayString As TestsOrdered, ");
        sb.Append("ACC_SpecimenSubmitted As SpecimenSubmitted, ");
        sb.Append("ACC_NotesAndInstructions As LabMessage, ");
        sb.Append("ACC_ReportingComments As ReportNotes, ");
        sb.Append("ACC_HasInquiryNotes As HasInquiryNotes, ");
        sb.Append("ACC_ClientDR->CLF_TimeZone As ClientTimeZone,"); //SSM 20/10/2011 #114978 AntechCSM 2B - Added ClientTimeZone
        sb.Append("ACC_OwnerLastName As OwnerLastName ");   //SSM 28/11/2011 AntechCSM2a2 Issue117056 Fetching OwnerLastName
        sb.Append("FROM ORD_Accession");
        sb.Append(" WHERE ACC_Accession = '" + accessionNumber + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static string getInquiries(string accessionNumber)
    {
        Dictionary<String, String> _getInqNote = new Dictionary<String, String>();
        _getInqNote.Add("AccessionNumber", accessionNumber);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetInqNotes(?)", _getInqNote,32000).Value.ToString();
    }

    public static int addInquiryNote(string accessionNumber, string userId, string noteText)
    {
        if (noteText.IndexOf("'") > -1)
        {
            noteText = noteText.Replace("'", "''");
        }

        Dictionary<String, String> _addInqNote = new Dictionary<String, String>();
        _addInqNote.Add("AccessionNumber", accessionNumber);
        _addInqNote.Add("InquiryNote", noteText);
        _addInqNote.Add("UserId", userId);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string retVal = cache.StoredProcedure("?=call SP2_AddInquiryNote(?,?,?)", _addInqNote).Value.ToString();
        return Convert.ToInt32(retVal);
    }

    public static DataTable getRequests(string accessionNumber)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ADDON_ConfirmationNumber As ConfirmationNumber, ");
        sb.Append("ADDON_CallerName As CallerName, ");
        sb.Append("ADDON_Date As RequestDate, ");
        sb.Append("ADDON_Time As RequestTime, ");
        sb.Append("ADDON_RequestType As RequestType, ");
        sb.Append("ADDON_Email As Email, ");
        sb.Append("ADDON_SpecialInstructions As SpecialInstructions, ");
        sb.Append("ADDON_Tests As Tests, ");
        sb.Append("ADDON_LabLocationDR As LabID, ");
        sb.Append("ADDON_LabLocationDR->LABLO_LabName As LabLocation, ");
        sb.Append("ADDON_ReasonForRequest As ReasonForReq, ");
        sb.Append("ADDON_PathologistRequested As PathalogistRequested, ");
        sb.Append("ADDON_OriginalPathologist As OriginalPathalogist, ");
        sb.Append("ADDON_CheckTubeType As CheckTubeType ");
        sb.Append("FROM ORD_AddOn");
        sb.Append(" WHERE ADDON_AccessionDR = '" + accessionNumber + "' AND ADDON_RequestType <> 'X' ORDER BY ADDON_Date DESC, ADDON_Time DESC"); //AM Issue#42941 AntechCSM 1.0.26.0 11/11/2008

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    // AM Isssue#32876 AntechCSM 1.0.14.0 09/22/2008
    public static DataTable getUnitsOrderedByAccession(string accessionNumber)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("AWLC_ComponentUnitCodeDR As TESTCODE, ");
        sb.Append("CASE AWLC_ComponentUnitCodeDR->UC_IsProfile WHEN 'Y' then {fn CONCAT(AWLC_ComponentUnitCodeDR->UC_DisplayReportingTitle, AWLC_NameToDisplay)} ELSE AWLC_NameToDisplay END As TESTNAME, ");
        sb.Append("AWLC_ComponentUnitCodeDR->UC_OrderingMnemonics As TestMnemonic ");
        sb.Append("FROM ORD_AccessionWorklistComponent ");
        sb.Append("WHERE AWLC_AWL_ParRef->AWL_ACC_ParRef->ACC_Accession = '" + accessionNumber + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getUnitsOrdered(string accessionNumber)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("AWLC_ComponentUnitCodeDR->UC_UnitCode As UnitCode, ");
        //sb.Append("AWLC_ComponentUnitCodeDR->UC_IsProfile As IsProfile, ");
        sb.Append("AWLC_ComponentUnitCodeDR->UC_UnitCode As ComponentUnitCode, ");
        sb.Append("AWLC_IsAbnormal As IsAbnormal, ");
        sb.Append("AWLC_ReportingWorklist As WorkList, ");
        sb.Append("CASE AWLC_ComponentUnitCodeDR->UC_IsProfile WHEN 'Y' then {fn CONCAT(AWLC_ComponentUnitCodeDR->UC_DisplayReportingTitle, AWLC_NameToDisplay)} ELSE AWLC_NameToDisplay END As ReportingTitle, ");
        sb.Append("ARPT_WorklistEnteredBy As WorkListEnteredBy, ");
        sb.Append("ARPT_WorklistReleasedBy As WorkListReleasedBy, ");
        sb.Append("ARPT_WorklistReleasedDate As WorklistReleasedDate, ");
        sb.Append("ARPT_WorklistReleasedTime As WorklistReleasedTime, ");
        sb.Append("ARPT_WorklistLoad As WorklistLoad, ");
        sb.Append("ARPT_WorklistSequence As WorklistSequence, ");
        sb.Append("ARPT_WorklistDateBuilt As WorklistDateBuilt, ");
        sb.Append("ARPT_WorklistTimeBuilt As WorklistTimeBuilt, ");
        sb.Append("ARPT_WorklistBuiltBy As WorklistBuiltBy, ");
        sb.Append("ARPT_TestPerformedAtLabDR->LABLO_LabName As TestPerformedAtLabName, ");
        sb.Append("ARPT_ReportedBatchDR->ACRB_BatchToDisplay As WorkListBatchToDisplay, ");
        sb.Append("ARPT_ReportedBatchDR->ACRB_ReportDate As WorkListReportDate, ");
        sb.Append("ARPT_ReportedBatchDR->ACRB_ReportTime As WorkListReportTime, ");
        sb.Append("ARPT_WorklistOwnedByLabDR->LABLO_LabName As WorklistOwnedByLabName, ");
        sb.Append("ARPT_WorklistEffectiveDate As WorklistEffectiveDate, ");
        sb.Append("AWLC_ComponentUnitCodeDR->UC_DaysTestSetUp As UnitCodeDaysTestSetUp, ");
        sb.Append("AWLC_ComponentUnitCodeDR->UC_TimeOfDay As UnitCodeTimeOfDay, ");
        sb.Append("AWLC_ComponentUnitCodeDR->UC_MaxLabTime As UnitCodeMaxLabTime, ");
        sb.Append("AWLC_ComponentUnitCodeDR->UC_CannedMessageToDisplayDR->MSG_MessageText As CannedMessage, ");
        sb.Append("%Internal(ARPT_ReportNotes) As ReportNotes ");
        sb.Append("FROM ORD_AccessionWorklistComponent ");
        sb.Append("LEFT OUTER JOIN ORD_AccessionWorklist ON AWL_RowID = AWLC_AWL_ParRef ");
        sb.Append("LEFT OUTER JOIN ORD_AccessionReport ON AWL_ACC_ParRef=ARPT_ACC_ParRef and AWLC_ReportingWorklist=ARPT_UnitCodeWorklist ");
        sb.Append("WHERE AWLC_AWL_ParRef->AWL_ACC_ParRef->ACC_Accession = '" + accessionNumber + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getSatelliteInformation(string accessionNumber)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ASI_ComponentUnitCodeDR As UnitCode, ");
        sb.Append("ASI_ComponentUnitCodeDR->UC_DisplayReportingTitle As UnitTitle, ");
        sb.Append("ASI_ComponentUnitCodeDR->UC_SpecimenType As UnitSpecimenType, ");
        sb.Append("ASI_Date As ReceivedDate, ");
        sb.Append("ASI_Time As ReceivedTime, ");
        sb.Append("Case ASI_AutoFlag When 1 Then 'Auto-Received' Else 'Received' End As  ReceivedMode, ");
        sb.Append("ASI_LabDR->LABLO_RowID As LabID, ");
        sb.Append("ASI_LabDR->LABLO_LabName As LabName, ");
        sb.Append("ASI_UserDR->USER_UserID As UserID, ");
        sb.Append("ASI_UserDR->USER_LastFirstName As UserFirstLastName ");
        sb.Append("FROM ORD_AccessionSatelliteInformation");
        sb.Append(" WHERE ASI_ACC_ParRef = '" + accessionNumber + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getSpecimenLocation(string accessionNumber)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("SPLOC_OwnerLabLocationDR->LABLO_LabName As LabName, ");
        sb.Append("SPLOC_Date As LocationDate, ");
        sb.Append("SPLOC_Rack As LocationRack, ");
        sb.Append("SPLOC_Position As LocationPosition, ");
        sb.Append("$$CO17^XT58(SPLOC_ScannedByUser) As UserID ");
        sb.Append("FROM ORD_SpecimenLocation");
        sb.Append(" WHERE SPLOC_AccessionDR = '" + accessionNumber + "'");
        sb.Append(" ORDER BY SPLOC_OwnerLabLocationDR, SPLOC_Date DESC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static string getSpecimenLocation(string accessionNumber, bool sp)
    {
        Dictionary<String, String> _SpecimenLoc = new Dictionary<String, String>();
        _SpecimenLoc.Add("AccessionNumber", accessionNumber);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetSpecimenLocation(?)", _SpecimenLoc).Value.ToString();        
    }

    public static String updateAddOnVerificationRequests(String accessionNumber, String callerName, String changeRequestType, String testsAddOnVerify, String Email, String labLocation, String SpecialInstructions, String ClientMnemonic, String UserID, String UserLab, String ReasonForReq, String PathologistRequested, String OriginalPathologist, String CheckTubeType)
    {
        #region Obsolete Codeset
        //System.Text.StringBuilder sb = new System.Text.StringBuilder();
        //sb.Append("INSERT INTO ");
        //sb.Append("ORD_ADDON ");
        //sb.Append("( ");
        //sb.Append("ADDON_ConfirmationNumber, ");
        //sb.Append("ADDON_AccessionDR, ");
        //sb.Append("ADDON_CallerName, ");
        //sb.Append("ADDON_Date, ");
        //sb.Append("ADDON_Time, ");
        //sb.Append("ADDON_RequestType, ");
        //sb.Append("ADDON_Email, ");
        //sb.Append("ADDON_LabLocationDR, ");
        //sb.Append("ADDON_SpecialInstructions, ");
        //sb.Append("ADDON_Tests, ");
        //sb.Append("ADDON_ClientDR");
        //sb.Append(") ");
        //sb.Append("SELECT ");
        //sb.Append("MAX(ADDON_ConfirmationNumber)+1, ");
        //sb.Append("'" + accessionNumber + "', ");
        //sb.Append("'" + callerName + "', ");
        //sb.Append("TO_DATE(CURRENT_DATE,'MM-DD-YYYY'), ");
        //sb.Append("TO_DATE({fn now},'HH24:MI'), ");
        //if (changeRequestType != "") //AM Issue#42549 AntechCSM 1.0.20.0 10/17/2008
        //    sb.Append("'" + changeRequestType + "', ");
        //else
        //    sb.Append("NULL, ");
        //sb.Append("'" + Email + "', ");
        //sb.Append("'" + labLocation + "', ");
        //sb.Append("'" + SpecialInstructions + "', ");
        //sb.Append("'" + testsAddOnVerify + "', ");
        //sb.Append("'" + clientID + "' ");
        //sb.Append("FROM ");
        //sb.Append("ORD_ADDON");
        //string insertStatement = sb.ToString();
        //CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        //int rowsAffected = cache.Insert(insertStatement);
        //DataTable dt = cache.FillCacheDataTable("SELECT MAX(ADDON_ConfirmationNumber),ADDON_Time FROM ORD_ADDON");
        //String lastAddOnID = dt.Rows[0][0].ToString();
        //String addonTime = dt.Rows[0][1].ToString();
        //Dictionary<string, string> _addonReport = new Dictionary<string, string>();
        //_addonReport.Add("ACCNO", accessionNumber);
        //_addonReport.Add("CONFNO", lastAddOnID);
        //_addonReport.Add("USER", UserID);
        //_addonReport.Add("ClientMnemonic", ClientMnemonic);
        ////_addonReport.Add("Lab", UserLab);labLocation
        //_addonReport.Add("Lab", labLocation);//AM 11/29/2008 - ROWID of selected Lablocation parameter from the Lab Location dropdown
        //_addonReport.Add("LogString", changeRequestType+"^"+testsAddOnVerify+"^"+callerName);
        //_addonReport.Add("TestToVerify", testsAddOnVerify); //AM 11/26/2008 - Passing Test Code String at the end of the enquiry note while saving Add On/Verification request.
        //_addonReport.Add("addonTime", addonTime); //AM 11/28/2008
        //String dummyreturn = cache.StoredProcedure("?=call SP_SaveAddonReport(?,?,?,?,?,?,?,?)", _addonReport).Value.ToString();
        //return rowsAffected;
        #endregion Obsolete Codeset

        Dictionary<String, String> _addonReport = new Dictionary<String, String>();
        _addonReport.Add("AccNo", accessionNumber);
        _addonReport.Add("CallerName", callerName);
        _addonReport.Add("ChangeReqType", changeRequestType);
        _addonReport.Add("Email", Email);
        _addonReport.Add("Lab", labLocation);
        _addonReport.Add("SpecialInst", SpecialInstructions);
        _addonReport.Add("TestsToVerify", testsAddOnVerify);
        _addonReport.Add("UserID", UserID);
        _addonReport.Add("Mnemonic", ClientMnemonic);
        _addonReport.Add("ReasonForReq", ReasonForReq);
        _addonReport.Add("PathologistRequested", PathologistRequested);
        _addonReport.Add("OriginalPathologist", OriginalPathologist);
        _addonReport.Add("CheckTubeType", CheckTubeType);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP_SaveAddonReport(?,?,?,?,?,?,?,?,?,?,?,?,?)", _addonReport).Value.ToString();        
    }

    public static Int32 updateAddOnVerificationRequests(String confirmationID, String callerName, String changeRequestType, String email, String labLocation, String specialInstructions, String addOnTests, String userID, String ReasonForReq, String PathologistRequested, String OriginalPathologist, String CheckTubeType, bool isModify)
    {
        #region ObsoleteCode
        /*System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("UPDATE ORD_ADDON SET ");
        sb.Append("ADDON_CALLERNAME = '" + callerName + "',");
        sb.Append(" ADDON_RequestType = '" + changeRequestType + "',");
        sb.Append(" ADDON_Email = '" + email + "',");
        sb.Append(" ADDON_LabLocationDR = '" + labLocation + "',");
        sb.Append(" ADDON_SpecialInstructions = '" + specialInstructions + "',");
        sb.Append(" ADDON_Tests = '" + addOnTests + "',");
        sb.Append(" ADDON_User = '" + userID + "'");
        sb.Append(" WHERE ADDON_ConfirmationNumber = '" + confirmationID + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.Insert(sb.ToString());*/
        #endregion ObsoleteCode
        Dictionary<String, String> _addonReport = new Dictionary<String, String>();
        _addonReport.Add("ConfirmationNumber", confirmationID);
        _addonReport.Add("Caller", callerName);
        _addonReport.Add("RequestType", changeRequestType);
        _addonReport.Add("Email", email);
        _addonReport.Add("Lab", labLocation);
        _addonReport.Add("SpecialInst", specialInstructions);
        _addonReport.Add("Tests", addOnTests);
        _addonReport.Add("UserID", userID);
        _addonReport.Add("ReasonForReq", ReasonForReq);
        _addonReport.Add("PathologistRequested", PathologistRequested);
        _addonReport.Add("OriginalPathologist", OriginalPathologist);
        _addonReport.Add("CheckTubeType", CheckTubeType);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return Convert.ToInt32(cache.StoredProcedure("?=call SP2_UpdateAddon(?,?,?,?,?,?,?,?,?,?,?,?)", _addonReport).Value);
    }

    public static String sendAutoDialFax(String accessionNumber, String clientMnemonic, String autoDial, String userName)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        Dictionary<string, string> _sendAutoDialFax = new Dictionary<string, string>();
        _sendAutoDialFax.Add("AccessionNumber", accessionNumber);
        _sendAutoDialFax.Add("ClientMnemonic", clientMnemonic);
        _sendAutoDialFax.Add("AutoDial", autoDial);
        _sendAutoDialFax.Add("UserName", userName);
        return cache.StoredProcedure("?=call SP2_sendAutoDialFax(?,?,?,?)", _sendAutoDialFax).Value.ToString();
    }

    public static String printInternalReport(String accessionNumber, String printerName, String userName)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        Dictionary<String, String> _printInternalReport = new Dictionary<String, String>();
        _printInternalReport.Add("AccessionNumber", accessionNumber);
        _printInternalReport.Add("PrinterName", printerName);
        _printInternalReport.Add("UserName", userName);
        return cache.StoredProcedure("?=call SP2_printInternalReport(?,?,?)", _printInternalReport).Value.ToString();
    }

    public static String sendAltFax(String accessionNumber, String clientMnemonic, String autoDial, String fax, String userName)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        Dictionary<string, string> _sendAltFax = new Dictionary<string, string>();
        _sendAltFax.Add("AccessionNumber", accessionNumber);
        _sendAltFax.Add("ClientMnemonic", clientMnemonic);
        _sendAltFax.Add("AutoDial", autoDial);
        _sendAltFax.Add("Fax", fax);
        _sendAltFax.Add("UserName", userName);
        return cache.StoredProcedure("?=call SP2_sendAltFax(?,?,?,?,?)", _sendAltFax).Value.ToString();
    }

    public static String printClientReport(String accessionNumber, String printerName, String userName)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        Dictionary<string, string> _printClientReport = new Dictionary<string, string>();
        _printClientReport.Add("AccessionNumber", accessionNumber);
        _printClientReport.Add("PrinterName", printerName);
        _printClientReport.Add("UserName", userName);
        return cache.StoredProcedure("?=call SP2_printClientReport(?,?,?)", _printClientReport).Value.ToString();
    }

    public static void logAccessionDetailsView(String accountNumber, String userID, String lab, String accessionNumber)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        Dictionary<string, string> logDetails = new Dictionary<string, string>();
        logDetails.Add("AccountNumber", accountNumber);
        logDetails.Add("UserId", userID);
        logDetails.Add("Lab", lab);
        logDetails.Add("AccessionNumber", accessionNumber);
        cache.StoredProcedure("?=call SP2_LogAccessionDetailsView(?,?,?,?)", logDetails);
    }

    public static void logZOAView(String accountNumber, String userID, String lab, String accessionNumber)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        Dictionary<string, string> logDetails = new Dictionary<string, string>();
        logDetails.Add("AccountNumber", accountNumber);
        logDetails.Add("UserId", userID);
        logDetails.Add("Lab", lab);
        logDetails.Add("AccessionNumber", accessionNumber);
        cache.StoredProcedure("?=call SP2_LogZOAView(?,?,?,?)", logDetails);
    }

}
