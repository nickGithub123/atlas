using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for DL_MissingBeforeAccn
/// </summary>
public class DL_MissingSpecimen
{
    public DL_MissingSpecimen()
	{
	}

    public static string InsertPink(string accessionNo, string comments, string testDetails, string user, string lab, string department)
    {
        Dictionary<String, String> PurpleData = new Dictionary<String, String>();
        PurpleData.Add("AccessionNumber", accessionNo);
        PurpleData.Add("Comment", comments);
        PurpleData.Add("User", user);
        PurpleData.Add("TestString", testDetails);
        PurpleData.Add("Lab", lab);
        PurpleData.Add("Department", department);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_AddPink(?,?,?,?,?,?)", PurpleData).Value.ToString();
    }

    public static string InsertPurple(string accountNo, string contactPerson, string altContactNo, string dateSubmitted, string comments, string testDetails, string user, string lab,string trfStatus, string noOfSamplesOnRoute, string noOfSamplesOnSystem)
    {
        Dictionary<String, String> PurpleData = new Dictionary<String, String>();
        PurpleData.Add("AccountNumber", accountNo);
        PurpleData.Add("ContactPerson", contactPerson);
        PurpleData.Add("AltContactNo", altContactNo);
        PurpleData.Add("DateSubmitted", dateSubmitted);
        PurpleData.Add("Comment", comments);
        PurpleData.Add("User", user);
        PurpleData.Add("PetTestString", testDetails);
        PurpleData.Add("Lab", lab);
        PurpleData.Add("TRFStatus", trfStatus);
        PurpleData.Add("SamplesOnRoute", noOfSamplesOnRoute);
        PurpleData.Add("SamplesOnSystem", noOfSamplesOnSystem);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_AddPurple(?,?,?,?,?,?,?,?,?,?,?)", PurpleData).Value.ToString();
    }

    public static string UpdatePurple(string rowID, string contactPerson, string altContactNo, string user, string noOfSamplesOnRoute, string noOfSamplesOnSystem, string attnRequireFlag, string routeStop, string dateSubmitted,string lab)
    {
        Dictionary<String, String> PurpleData = new Dictionary<String, String>();
        PurpleData.Add("RowId", rowID);
        PurpleData.Add("ContactPerson", contactPerson);
        PurpleData.Add("AltContactNo", altContactNo);
        PurpleData.Add("User", user);
        PurpleData.Add("SamplesOnRoute", noOfSamplesOnRoute);
        PurpleData.Add("SamplesOnSystem", noOfSamplesOnSystem);
        PurpleData.Add("AttnRequireFlag", attnRequireFlag);
        PurpleData.Add("RouteStop", routeStop);
        PurpleData.Add("DateSubmitted", dateSubmitted);
        PurpleData.Add("Lab", lab);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdatePurple(?,?,?,?,?,?,?,?,?,?)", PurpleData).Value.ToString();
    }

    public static string UpdateProgressNote(string rowID, string progressNote, string user, string addInqNote)
    {
        Dictionary<String, String> PurpleData = new Dictionary<String, String>();
        PurpleData.Add("RowId", rowID);
        PurpleData.Add("ProgressNote", progressNote);
        PurpleData.Add("User", user);
        PurpleData.Add("AddInqNote", addInqNote);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdateProgressNote(?,?,?,?)", PurpleData).Value.ToString();
    }

    public static string ResolveIssue(string rowID, string issueStatus, string user, string resolutionNote, string locatedAt, string personContected, string onDate, string problemCategory, string accessionNumber, string typeOfTest)
    {
        Dictionary<String, String> PurpleData = new Dictionary<String, String>();
        PurpleData.Add("RowId", rowID);
        PurpleData.Add("IssueStatus", issueStatus);
        PurpleData.Add("User", user);
        PurpleData.Add("ResolutionNote", resolutionNote);
        PurpleData.Add("LocatedAt", locatedAt);
        PurpleData.Add("PersonContacted", personContected);
        PurpleData.Add("ContectedOnDate", onDate);
        PurpleData.Add("ProblemCategory", problemCategory);
        PurpleData.Add("AccessionNumber", accessionNumber);
        PurpleData.Add("TypeOfTest", typeOfTest);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ResolveIssue(?,?,?,?,?,?,?,?,?,?)", PurpleData).Value.ToString();
    }

    public static string CheckOut(string rowID, string accessionNo, string user)
    {
        Dictionary<String, String> PurpleData = new Dictionary<String, String>();
        PurpleData.Add("AccessionNumber", accessionNo);
        PurpleData.Add("RowId", rowID);
        PurpleData.Add("User", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_MissingSpecimenCheckOut(?,?,?)", PurpleData).Value.ToString();
    }

    public static string UnCheckOut(string rowID, string accessionNo, string user)
    {
        Dictionary<String, String> PurpleData = new Dictionary<String, String>();
        PurpleData.Add("AccessionNumber", accessionNo);
        PurpleData.Add("RowId", rowID);
        PurpleData.Add("User", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_MissingSpecimenUnCheckOut(?,?,?)", PurpleData).Value.ToString();
    }

    public static DataTable getMissingBeforeAccnBySearchOptions(string type, string clientID, string user, string dateFrom, string dateTo, string lab, string progressStatus, string reqLabCAttn, string accession, string checkedBy,string labCResolution,string department,string processStatus, string finalizedBy)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("MISSS_RowID AS RowId,");
        sb.Append("$$CO17^XT58(MISSS_EnteredByUserDR->USER_UserID) AS EnteredByUser,");
        sb.Append("%EXTERNAL(MISSS_DateEntered) AS EnteredOnDate,");
        sb.Append("%EXTERNAL(MISSS_TimeEntered) AS EnteredOnTime,");
        sb.Append("MISSS_ClientDR->CLF_CLNUM AS AccountNo,");
        sb.Append("%EXTERNAL(MISSS_OutcomeStatus) AS OutcomeStatus,");
        sb.Append("%EXTERNAL(MISSS_ProcessingStatus) AS ProcessingStatus,");
        sb.Append("MISSS_ProcessingStatus AS StatusCode,");
        sb.Append("MISSS_AccessionDR->ACC_Accession AS AccessionNumber,");
        sb.Append("$$CO17^XT58(MISSS_CheckedOutByUserDR) AS CheckOutBy,");
        sb.Append("%EXTERNAL(MISSS_RequiresLabCAttention) AS ReqLabCAttn,");
        sb.Append("MISSS_MissingFromLabDR AS MissingByLab,");
        sb.Append("%EXTERNAL(MISSS_OutcomeStatusInterim) AS OutcomeStatusInterim,");
        sb.Append("$$CO17^XT58(MISSS_ResolvedByUserDR) AS ResolvedByUser,");
        sb.Append("MISSS_ClientDR->CLF_CLNAM AS AccountName,");
        sb.Append("MISSS_ClientDR->CLF_CLMNE AS AccountMnemonic,");
        sb.Append("MISSS_ClientDR->CLF_CLPHN As ClientPhone,");
        sb.Append("MISSS_ClientDR->CLF_CLAD1 AS ClientAddress1,");
        sb.Append("MISSS_ClientDR->CLF_CLAD2 AS ClientAddress2,");
        sb.Append("MISSS_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sb.Append("MISSS_ClientDR->CLF_IsHot As ClientIsHot,");
        sb.Append("MISSS_ClientDR->CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("MISSS_ClientDR->CLF_IsNew As ClientIsNew,");
        sb.Append("MISSS_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sb.Append("MISSS_ClientDR->CLF_CLADG AS AutodialGroup,");
        sb.Append("MISSS_ClientDR->CLF_CLRTS AS RouteStop,");
        sb.Append("zoasis_num AS Zoasis,");
        sb.Append("$$GETREVHIST^XT1(MISSS_ClientDR) AS ClientRevenue");

        sb.Append(" FROM ");
        sb.Append("ORD_MissingSpecimen ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("CLF_ClientFile Client on MISSS_ClientDR = Client.CLF_CLMNE ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sb.Append("WHERE 1=1 ");
        sb.Append(" AND MISSS_MissingType ='" + type + "'");
        if (clientID.Length > 0)
        {
            sb.Append(" AND MISSS_ClientDR->CLF_CLNUM ='" + clientID + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND MISSS_DateEntered>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND MISSS_DateEntered<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (user.Length > 0)
        {
            sb.Append(" AND MISSS_EnteredByUserDR->USER_UserID  ='" + user + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND MISSS_ProcessingStatus ='" + progressStatus + "'");
        }
        if (lab.Length > 0)
        {
            sb.Append(" AND MISSS_MissingFromLabDR ='" + lab + "'");
        }
        if (reqLabCAttn.Length > 0)
        {
            sb.Append(" AND MISSS_RequiresLabCAttention ='" + reqLabCAttn + "'");
        }
        if (accession.Length > 0)
        {
            sb.Append(" AND MISSS_AccessionDR ='" + accession + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND MISSS_CheckedOutByUserDR ='" + checkedBy + "'");
        }
        if (labCResolution.Length > 0)
        {
            sb.Append(" AND MISSS_OutcomeStatus ='" + labCResolution + "'");
        }
        if (department.Length > 0)
        {
            sb.Append(" AND MISSS_MissingFromDepartmentDR ='" + department + "'");
        }
        if (processStatus.Length > 0)
        {
            sb.Append(" AND MISSS_OutcomeStatusInterim ='" + processStatus + "'");
        }
        if (finalizedBy.Length > 0)
        {
            sb.Append(" AND MISSS_ResolvedByUserDR ='" + finalizedBy + "'");
        }
        sb.Append(" ORDER BY MISSS_DateEntered DESC,MISSS_TimeEntered DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getMissingBeforeAccnForReport(string clientID, string user, string dateFrom, string dateTo, string progressStatus, string lab, string reqLabCAttn,string checkedBy,string labCResolution)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("MISSS_RowID AS RowId,");
        sb.Append("$$CO17^XT58(MISSS_EnteredByUserDR->USER_UserID) AS EnteredByUser,");
        sb.Append("%EXTERNAL(MISSS_DateEntered) AS EnteredOnDate,");
        sb.Append("%EXTERNAL(MISSS_TimeEntered) AS EnteredOnTime,");
        sb.Append("%EXTERNAL(MISSS_OutcomeStatus) AS OutcomeStatus,");
        sb.Append("%EXTERNAL(MISSS_ProcessingStatus) AS ProcessingStatus,");
        sb.Append("MISSS_ProcessingStatus AS StatusCode,");
        sb.Append("MISSS_AccessionDR->ACC_Accession AS AccessionNumber,");
        sb.Append("$$CO17^XT58(MISSS_CheckedOutByUserDR) AS CheckOutBy,");
        sb.Append("MISSS_ResolvedByUserDR AS ResolvedByUser,");
        sb.Append("%EXTERNAL(MISSS_RequiresLabCAttention) AS ReqLabCAttn,");
        sb.Append("MISSS_MissingFromLabDR->LABLO_LabName AS MissingByLab,");
        sb.Append("MISSS_ContactPerson AS ContactPerson,");
        sb.Append("MISSS_AlternateContactNumber AS AlternateContactNumber,");
        sb.Append("MISSS_DateRangeSubmitted AS DateSubmitted,");
        sb.Append("MISSS_SamplesOnRouteSheet AS SamplesOnRouteSheet,");
        sb.Append("MISSS_SamplesOnSystem AS SamplesOnSystem,");
        sb.Append("%EXTERNAL(MISSS_TRFStatus) AS TRFStatus,");
        sb.Append("MISSS_ProblemDR->PBT_PSQ AS ProblemNumber,");

        sb.Append("$$GETMISSINGSPECIMENNOTE^XT112(MISSS_RowID) AS Comment,");
        sb.Append("$$GETMISSINGTESTDETAILS^XT112(MISSS_RowID) AS PetString,");

        sb.Append("$$GETRESOLUTIONNOTE^XT112(MISSS_RowID) AS ResolutionNote,");
        sb.Append("$$GETPROGRESSNOTE^XT112(MISSS_RowID) AS ProgressNote,");

        sb.Append("MISSS_ClientDR->CLF_CLNAM AS AccountName,");
        sb.Append("MISSS_ClientDR->CLF_CLPHN As ClientPhone,");
        sb.Append("MISSS_ClientDR->CLF_CLNUM AS AccountNo,");
        sb.Append("MISSS_RouteStop As RouteStop,");
        sb.Append("MISSS_PersonContactedOnDate As ReslovedDate,");
        sb.Append("MISSS_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");

        sb.Append("%EXTERNAL(MISSS_TypeOfTest) AS TypeOfTest,");
        sb.Append("MISSS_WhereLocated AS WhereLocated,");
        sb.Append("MISSS_PersonContactedAtClinic AS PersonContactedAtClinic");

        sb.Append(" FROM ");
        sb.Append("ORD_MissingSpecimen ");

        sb.Append("WHERE MISSS_MissingType ='B'");
        if (clientID.Length > 0)
        {
            sb.Append(" AND MISSS_ClientDR->CLF_CLNUM ='" + clientID + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND MISSS_DateEntered>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND MISSS_DateEntered<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (user.Length > 0)
        {
            sb.Append(" AND MISSS_EnteredByUserDR->USER_UserID ='" + user + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND MISSS_ProcessingStatus ='" + progressStatus + "'");
        }
        if (lab.Length > 0)
        {
            sb.Append(" AND MISSS_MissingFromLabDR ='" + lab + "'");
        }
        if (reqLabCAttn.Length > 0)
        {
            sb.Append(" AND MISSS_RequiresLabCAttention ='" + reqLabCAttn + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND MISSS_CheckedOutByUserDR->USER_UserID ='" + checkedBy + "'");
        }
        if (labCResolution.Length > 0)
        {
            sb.Append(" AND MISSS_OutcomeStatus='" + labCResolution + "'");
        }
        sb.Append(" ORDER BY MISSS_DateEntered DESC,MISSS_TimeEntered DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getMissingAfterAccnForReport(string clientID, string user, string dateFrom, string dateTo, string progressStatus, string lab, string accession, string checkedBy,string labCResolution, string department, string processStatus)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("MISSS_RowID AS RowId,");
        sb.Append("$$CO17^XT58(MISSS_EnteredByUserDR->USER_UserID) AS EnteredByUser,");
        sb.Append("%EXTERNAL(MISSS_DateEntered) AS EnteredOnDate,");
        sb.Append("%EXTERNAL(MISSS_TimeEntered) AS EnteredOnTime,");
        sb.Append("%EXTERNAL(MISSS_OutcomeStatus) AS OutcomeStatus,");
        sb.Append("%EXTERNAL(MISSS_ProcessingStatus) AS ProcessingStatus,");
        sb.Append("MISSS_MissingFromLabDR->LABLO_LabName AS MissingByLab,");
        sb.Append("$$CO17^XT58(MISSS_CheckedOutByUserDR) AS CheckOutBy,");
        sb.Append("MISSS_ProcessingStatus AS StatusCode,");
        sb.Append("MISSS_PersonContactedAtClinic AS PersonContactedAtClinic,");
        sb.Append("MISSS_WhereLocated AS LocatedAt,");
        sb.Append("%EXTERNAL(MISSS_DateResolvedBy) AS ResolutionDate,");
        sb.Append("%EXTERNAL(MISSS_TimeResolvedBy) AS ResolutionTime,");
        sb.Append("MISSS_TestString AS TestString,");

        sb.Append("$$GETRESOLUTIONNOTE^XT112(MISSS_RowID) AS ResolutionNote,");
        sb.Append("$$GETPROGRESSNOTE^XT112(MISSS_RowID) AS ProgressNote,");
        sb.Append("$$GETSPECIMENLOCATION^XT51(MISSS_AccessionDR->ACC_Accession) AS TubeLocation,");

        sb.Append("MISSS_AccessionDR->ACC_Accession AS AccessionNumber,");
        sb.Append("MISSS_AccessionDR->ACC_SpecimenSubmitted As SpecimenSubmitted, ");
        sb.Append("%EXTERNAL(MISSS_AccessionDR->ACC_MaxiLogDate) As OrderDate,");
        sb.Append("%EXTERNAL(MISSS_AccessionDR->ACC_MaxiLogTime) As OrderTime,");
        sb.Append("MISSS_ClientDR->CLF_CLNAM AS AccountName,");
        sb.Append("MISSS_ClientDR->CLF_CLPHN As ClientPhone,");
        sb.Append("MISSS_ClientDR->CLF_CLNUM AS AccountNo, ");
        sb.Append("%EXTERNAL(MISSS_OutcomeStatusInterim) AS InterimOutcomeStatus, ");
        sb.Append("MISSS_OutcomeStatusInterim As InterimStatus, ");
        sb.Append("MISSS_WhereLocatedInterim AS InterimLocatedAt, ");
        sb.Append("MISSS_ResolutionNotesInterim AS ResolutionNotesInterim, ");
        sb.Append("MISSS_DateProcessed As InterimResolutionDate, ");
        sb.Append("MISSS_TimeProcessed As InterimResolutionTime");
        
        sb.Append(" FROM ");
        sb.Append("ORD_MissingSpecimen ");

        sb.Append("WHERE MISSS_MissingType ='A'");
        if (clientID.Length > 0)
        {
            sb.Append(" AND MISSS_ClientDR->CLF_CLNUM ='" + clientID + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND MISSS_DateEntered>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND MISSS_DateEntered<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (user.Length > 0)
        {
            sb.Append(" AND MISSS_EnteredByUserDR->USER_UserID ='" + user + "'");
        }
        if (progressStatus.Length > 0)
        {
            sb.Append(" AND MISSS_ProcessingStatus ='" + progressStatus + "'");
        }
        if (lab.Length > 0)
        {
            sb.Append(" AND MISSS_MissingFromLabDR ='" + lab + "'");
        }
        if (accession.Length > 0)
        {
            sb.Append(" AND MISSS_AccessionDR ='" + accession + "'");
        }
        if (checkedBy.Length > 0)
        {
            sb.Append(" AND MISSS_CheckedOutByUserDR->USER_UserID ='" + checkedBy + "'");
        }
        if (labCResolution.Length > 0)
        {
            sb.Append(" AND MISSS_OutcomeStatus ='" + labCResolution + "'");
        }
        if (department.Length > 0)
        {
            sb.Append(" AND MISSS_MissingFromDepartmentDR ='" + department + "'");
        }
        if (processStatus.Length > 0)
        {
            sb.Append(" AND MISSS_OutcomeStatusInterim ='" + processStatus + "'");
        }
        sb.Append(" ORDER BY MISSS_DateEntered DESC,MISSS_TimeEntered DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getMissingBeforeAccnDetails(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("MISSS_RowID AS RowId,");
        sbSQL.Append("MISSS_EnteredByUserDR->USER_UserID AS EnteredByUser,");
        sbSQL.Append("MISSS_ClientDR AS AccountMnemonic,");
        sbSQL.Append("MISSS_ContactPerson AS ContactPerson,");
        sbSQL.Append("MISSS_AlternateContactNumber AS AltContactNo,");
        sbSQL.Append("MISSS_DateRangeSubmitted AS DateSubmitted,");
        sbSQL.Append("MISSS_OutcomeStatus AS OutcomeStatus,");
        sbSQL.Append("%INTERNAL(MISSS_ProcessingStatus) AS ProcessingStatus,");
        sbSQL.Append("MISSS_CheckedOutByUserDR AS CheckOutBy,");
        sbSQL.Append("%EXTERNAL(MISSS_DateEntered) As DateEntered,");
        sbSQL.Append("%EXTERNAL(MISSS_TimeEntered) As TimeEntered,");
        sbSQL.Append("%EXTERNAL(MISSS_TRFStatus) AS TRFStatusId,");
        sbSQL.Append("MISSS_SamplesOnRouteSheet AS SamplesOnRouteSheet,");
        sbSQL.Append("MISSS_SamplesOnSystem AS SamplesOnSystem,");
        sbSQL.Append("MISSS_RouteStop AS RouteStop,");
        sbSQL.Append("MISSS_MissingFromLabDR->LABLO_LabName AS MissingFromLab,");
        sbSQL.Append("MISSS_MissingFromLabDR AS MissingFromLabId,");
        sbSQL.Append("%EXTERNAL(MISSS_RequiresLabCAttention) AS ReqLabCAttn,");
        sbSQL.Append("$$CO17^XT58(MISSS_EnteredByUserDR->USER_UserID) As EnteredByUserDispName");

        sbSQL.Append(" FROM ORD_MissingSpecimen ");
        sbSQL.Append("WHERE MISSS_RowID ='");
        sbSQL.Append(rowId);
        sbSQL.Append("'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static DataTable getMissingAfterAccnDetails(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("MISSS_RowID AS RowId,");
        sbSQL.Append("MISSS_EnteredByUserDR->USER_UserID AS EnteredByUser,");
        sbSQL.Append("MISSS_ClientDR AS AccountMnemonic,");
        sbSQL.Append("%EXTERNAL(MISSS_DateEntered) As DateEntered,");
        sbSQL.Append("%EXTERNAL(MISSS_TimeEntered) As TimeEntered,");
        sbSQL.Append("MISSS_MissingFromDepartmentDR->DEPT_Name AS MissingFromDept,");
        sbSQL.Append("MISSS_MissingFromLabDR->LABLO_LabName AS MissingFromLab,");
        sbSQL.Append("%EXTERNAL(MISSS_OutcomeStatusInterim) AS OutcomeStatusInterim,");
        sbSQL.Append("%EXTERNAL(MISSS_OutcomeStatus) AS OutcomeStatus,");
        sbSQL.Append("MISSS_TestString AS TestDetails,");
        sbSQL.Append("%INTERNAL(MISSS_ProcessingStatus) AS ProcessingStatus,");
        sbSQL.Append("MISSS_CheckedOutByUserDR AS CheckOutBy,");
        sbSQL.Append("MISSS_AccessionDR->ACC_Accession AS AccessionNumber,");
        sbSQL.Append("$$CO17^XT58(MISSS_EnteredByUserDR->USER_UserID) As EnteredByUserDispName");

        sbSQL.Append(" FROM ORD_MissingSpecimen ");
        sbSQL.Append("WHERE MISSS_RowID ='");
        sbSQL.Append(rowId);
        sbSQL.Append("'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static DataTable GetProgressNotes(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("%EXTERNAL(MSN_EnteredDate) AS EnteredDate,");
        sbSQL.Append("%EXTERNAL(MSN_EnteredTime) AS EnteredTime,");
        sbSQL.Append("$$CO17^XT58(MSN_EnteredByUserDR->USER_UserID) AS EnteredByUserID,");
        sbSQL.Append("MSN_NotesText AS ProgressNote ");

        sbSQL.Append("FROM ORD_MissingSpecimenNotes ");
        sbSQL.Append("WHERE MSN_MISSS_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");
        sbSQL.Append("AND MSN_NotesText <> '' ");
        sbSQL.Append("ORDER BY MSN_EnteredDate DESC,MSN_EnteredTime DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static DataTable GetTestDetails(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("MSPL_PetName||' '||MSPL_OwnerName||' / '||MSPL_TestsRequested||' / '||MSPL_SamplesSubmitted  AS TestDetails ");
        
        sbSQL.Append("FROM ORD_MissingSpecimenPetList ");
        sbSQL.Append("WHERE MSPL_MISSS_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");
        sbSQL.Append("ORDER BY MSPL_ChildSub");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static DataTable GetProcessingResolutionDetails(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("%EXTERNAL(MISSS_OutcomeStatusInterim) AS OutcomeStatusInterim,");
        sbSQL.Append("MISSS_OutcomeStatusInterim AS OutcomeStatusInterimCode,");
        sbSQL.Append("MISSS_WhereLocatedInterim AS WhereLocatedInterim,");
        sbSQL.Append("MISSS_AccessionDR  AS Accession,");
        sbSQL.Append("MISSS_ResolutionNotesInterim AS ResolutionNotesInterim ");

        sbSQL.Append("FROM ORD_MissingSpecimen ");
        sbSQL.Append("WHERE MISSS_RowID ='");
        sbSQL.Append(rowId);
        sbSQL.Append("'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static string UpdateProcessingResolutionDetails(string rowId, string outcomeStatusInterim, string whereLocatedInterim, string resolutionNotesInterim,string userID)
    {
        Dictionary<String, String> resolvePinkInterim = new Dictionary<String, String>();
        resolvePinkInterim.Add("MISSSROW", rowId);
        resolvePinkInterim.Add("STATUS", outcomeStatusInterim);
        resolvePinkInterim.Add("WHERELOCATED", whereLocatedInterim);
        resolvePinkInterim.Add("RESOLUTIONNOTE", resolutionNotesInterim);
        resolvePinkInterim.Add("USER", userID);
        
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ResolvePinkInterim(?,?,?,?,?)", resolvePinkInterim).Value.ToString();
    }

    public static string UpdateMissingAfterAccession(string rowId, string lab, string reason, string user)
    {
        Dictionary<String, String> updateMissingAfterAccession = new Dictionary<String, String>();
        updateMissingAfterAccession.Add("RowId", rowId);
        updateMissingAfterAccession.Add("Lab", lab);
        updateMissingAfterAccession.Add("Reason", reason);
        updateMissingAfterAccession.Add("UserID", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdateMissingAfterAccession(?,?,?,?)", updateMissingAfterAccession).Value.ToString();
    }
}
