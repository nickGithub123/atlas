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
using System.Text;

/// <summary>
/// Summary description for DL_ConsultRecorder
/// </summary>
public class DL_ConsultRecorder
{
    public DL_ConsultRecorder()
    {

    }

    internal static DataTable getConsultant(string SpecCode,bool IsTransferConsult)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT CSPEC_CONS_PR As RowId, ");
        sbSQL.Append("CSPEC_CONS_PR->CONS_Code As CONSCode, ");
        sbSQL.Append("CSPEC_CONS_PR->CONS_Name As CONSName, ");
        sbSQL.Append("CSPEC_CONS_PR->CONS_LastFirstName As CONSDisplayName, ");
        sbSQL.Append("%INTERNAL(CSPEC_CONS_PR->CONS_Location) As CONSLocation ");
        sbSQL.Append("FROM DIC_ConsultantSpecialty ");
        sbSQL.Append("WHERE CSPEC_SpecialtyDR='" + SpecCode + "'");
        if (IsTransferConsult)
        {
            sbSQL.Append(" AND (CSPEC_DisplayForTransfer<>'N' OR  CSPEC_DisplayForTransfer IS NULL)");
        }
        sbSQL.Append(" ORDER BY CONSDisplayName ASC");
        
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    internal static DataTable isExternalConsultant(string consultRow)
    {
        String selectStatement = "SELECT %INTERNAL(CONS_Location) AS ConsultLocation FROM DIC_Consultant WHERE CONS_RowID='" + consultRow + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    internal static DataTable getConsultantForAreaOfInterest(string areaOfInterest)
    {
        DataTable dtblConsultants = new DataTable();

        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT CINT_CONS_PR->CONS_LastFirstName AS ConsultantName,");
        sbSQL.Append("$$GETCNSLTSPECIALTY^XT120(CINT_CONS_PR) AS Specialty,");
        sbSQL.Append("CINT_CONS_PR AS ConsultantRow");
        sbSQL.Append(" FROM DIC_ConsultantInterest");
        sbSQL.Append(" WHERE LOWER(CINT_InterestDR) ='");
        sbSQL.Append(areaOfInterest);
        sbSQL.Append("'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        dtblConsultants = cache.FillCacheDataTable(sbSQL.ToString());


        return dtblConsultants;
    }

    internal static DataTable getCompletedConsultSummaryReport(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT COUNT(*) AS CnsltCount, CNSLT_ResolvedByConsultantDR,");
        sbSQL.Append("CNSLT_RequestedMethod AS RequestedMethod,");
        sbSQL.Append("CNSLT_ResolvedByConsultantDR AS ResolvedConsultantDR,");
        sbSQL.Append("CNSLT_ResolvedByConsultantDR->CONS_Name AS ConsultantName");
        sbSQL.Append(" FROM ORD_Consultation");
        sbSQL.Append(" WHERE 1=1");
        DateTime dtDateFrom = DateTime.Parse(dateFrom).AddMonths(-1);
        if (dtDateFrom.ToString().Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredDate>= TO_DATE('", dtDateFrom.ToShortDateString(), "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (dateFrom.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_DateResolved>= TO_DATE('", dateFrom, "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_DateResolved<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        //if (consultNumber.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        //}
        if (accountNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        //if (accessionNumber.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        //}
        //if (status.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        //}
        if (method.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sbSQL.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sbSQL.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sbSQL.Append(" GROUP BY CNSLT_RequestedMethod, CNSLT_ResolvedByConsultantDR");
        sbSQL.Append(" ORDER BY CNSLT_ResolvedByConsultantDR");
        
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }

    internal static DataTable getConsultOverview(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT COUNT(*) AS CnsltCount, CNSLT_ResolvedByConsultantDR,");
        sbSQL.Append("CNSLT_RequestedMethod AS RequestedMethod,");
        sbSQL.Append("CNSLT_DateResolved AS DateResolved,");
        sbSQL.Append("CNSLT_ResolvedByConsultantDR->CONS_Name AS ConsultantName");
        sbSQL.Append(" FROM ORD_Consultation");
        sbSQL.Append(" WHERE 1=1");
        DateTime dtDateFrom = DateTime.Parse(dateFrom).AddMonths(-1);
        if (dtDateFrom.ToString().Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredDate>= TO_DATE('", dtDateFrom.ToShortDateString(), "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (dateFrom.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_DateResolved>= TO_DATE('", dateFrom, "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_DateResolved<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        //if (consultNumber.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        //}
        if (accountNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        //if (accessionNumber.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        //}
        //if (status.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        //}
        if (method.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sbSQL.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sbSQL.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sbSQL.Append(" GROUP BY CNSLT_ResolvedByConsultantDR, CNSLT_DateResolved, CNSLT_RequestedMethod");
        sbSQL.Append(" ORDER BY CNSLT_ResolvedByConsultantDR,CNSLT_DateResolved");
        
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }

    internal static DataTable getPhysicianConsultSummaryDetails(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT CNSLT_ResolvedByConsultantDR->CONS_Name AS ConsultantName,");
        sbSQL.Append("CNSLT_ConsultationNumber,");
        sbSQL.Append("CNSLT_EnteredDate,");
        sbSQL.Append("CNSLT_EnteredTime,");
        sbSQL.Append("CNSLT_DateResolved,");
        sbSQL.Append("CNSLT_TimeResolved,");
        sbSQL.Append("%EXTERNAL(CNSLT_RequestedMethod) AS RequestedMethod");
        sbSQL.Append(" FROM ORD_Consultation");
        sbSQL.Append(" WHERE CNSLT_ResolvedByConsultantDR = '");
        sbSQL.Append(resolvedBy);
        sbSQL.Append("' ");
        DateTime dtDateFrom = DateTime.Parse(dateFrom).AddMonths(-1);
        if (dtDateFrom.ToString().Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredDate>= TO_DATE('", dtDateFrom.ToShortDateString(), "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (dateFrom.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_DateResolved>= TO_DATE('", dateFrom, "','MM/dd/yyyy')"));
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_DateResolved<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        //if (consultNumber.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        //}
        if (accountNumber.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        //if (accessionNumber.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        //}
        //if (status.Length > 0)
        //{
        //    sbSQL.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        //}
        if (method.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sbSQL.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sbSQL.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sbSQL.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sbSQL.Append(" ORDER BY CNSLT_DateResolved DESC,CNSLT_TimeResolved DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }

    //+SSM 10/11/2011  #116096 AntechCSM-2B  Searching for Consult Records
    internal static DataTable getConsultationDetails(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy,string reasonForConsult,string priority, string speciality,string enteredBy,string intExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CNSLT_RowID AS RowId,CNSLT_AccessionDR->ACC_Accession AS ACCESSION,");
        sb.Append("CLF_CLNUM AS ACCOUNT,");
        sb.Append("CNSLT_ConsultationNumber AS ConsultNo,");
        sb.Append("CNSLT_AccessionDR,");
        sb.Append("CNSLT_ContactPerson,");
        sb.Append("CNSLT_ContactDetails,");
        sb.Append("CNSLT_LastestCallbackDate AS CallbackDate,");
        sb.Append("CNSLT_LatestCallbackTime AS CallbackTime,");
        sb.Append("CNSLT_CallbackTimeZone AS CallbackTimeZone,");
        sb.Append("%EXTERNAL(CNSLT_Priority) AS Priority,");
        sb.Append("CNSLT_ProcessingStatus AS Status,");
        sb.Append("%EXTERNAL(CNSLT_ProcessingStatus) AS StatusText,");
        sb.Append("CNSLT_SpecialtyDR AS Specialty,");
        sb.Append("CNSLT_RequestedConsultantDR->CONS_LastFirstName As ReqCon,");
        sb.Append("CNSLT_RequestedConsultantDR As ReqConDR,");
        sb.Append("CNSLT_EnteredByUserDR,");
        sb.Append("CNSLT_EnteredDate,");
        sb.Append("CNSLT_EnteredTime,");
        sb.Append("CNSLT_ProcessedByUserDR As CurrentConsultant,");
        sb.Append("$$CO17^XT58(CNSLT_ProcessedByUserDR) As CurrentConsultantDispName,");
        sb.Append("CNSLT_ProcessedDate,");
        sb.Append("CNSLT_ProcessedTime,");
        sb.Append("CNSLT_ResolvedByConsultantDR,");
        sb.Append("CNSLT_IsUpdated,");
        sb.Append("CNSLT_PriorityScore,");
        sb.Append("CNSLT_IsSecondRequest,");

        sb.Append("CLF_CLNAM AS AccountName,");
        sb.Append("CLF_CLMNE AS AccountMnemonic,");
        sb.Append("CLF_CLPHN As ClientPhone,");
        sb.Append("CLF_CLAD1 AS ClientAddress1,");
        sb.Append("CLF_CLAD2 AS ClientAddress2,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sb.Append("CLF_CLADG AS AutodialGroup,");
        sb.Append("CLF_CLRTS AS RouteStop,");
        //sb.Append("CLF_TimeZone AS ClientTimeZone,");
        sb.Append("zoasis_num AS Zoasis,");
        sb.Append("$$GETCNSLTSNAPSHOT^XT120(CNSLT_RowID) AS ConsultSnapshot,");
        sb.Append("$$GETREVHIST^XT1(CNSLT_AccessionDR->ACC_ClientDR) AS ClientRevenue");

        sb.Append(" FROM ORD_Consultation");
        sb.Append(" LEFT OUTER JOIN");
        sb.Append(" CLF_ClientFile Client on CNSLT_ClientDR = Client.CLF_CLMNE ");
        sb.Append(" LEFT OUTER JOIN ");
        sb.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sb.Append(" WHERE 1=1");

        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredDate>= TO_DATE('", dateFrom, "','MM/dd/yyyy') AND CNSLT_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (intExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", intExternal, "'"));
        }
        if (method == "SC")
        {
            sb.Append(" ORDER BY CNSLT_PriorityScore ASC");
        }
        else
        {
            sb.Append(" ORDER BY CNSLT_EnteredDate DESC, CNSLT_EnteredTime DESC");
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    /// <summary>
    /// Fetches consult records for TAT report
    /// </summary>
    /// <param name="consultNumber">Consult Number</param>
    /// <param name="accountNumber">Account Number</param>
    /// <param name="accessionNumber">Accession Number</param>
    /// <param name="dateFrom">Date From</param>
    /// <param name="dateTo">Date To</param>
    /// <param name="status">Status</param>
    /// <param name="method">Method</param>
    /// <param name="requestedConsultant">Requested Consultant</param>
    /// <param name="resolvedBy">Resolved By</param>
    /// <param name="reasonForConsult">Reason For Consult</param>
    /// <param name="priority">Priority</param>
    /// <param name="speciality">Speciality</param>
    /// <param name="enteredBy">Entered By</param>
    /// <param name="intExternal">Internal External</param>	
    /// <param name="secondReqTAT">Flag indicating whether Report is Second Request TAT</param>
    /// <returns>Data table containing the report result</returns>
    internal static DataTable GetSTATConsultTurnAroundTime(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string intExternal, bool secondReqTAT)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT DISTINCT ");
        sb.Append("CLF_CLNUM AS ACCOUNT,");
        sb.Append("CLF_CLNAM AS AccountName,");
        sb.Append("CNSLT_ConsultationNumber AS ConsultNo,");
        sb.Append("%EXTERNAL(CNSLT_Priority) AS Priority,");
        sb.Append("%EXTERNAL(CNSLT_IsSecondRequest) AS IsSecondReq,");
        sb.Append("CNSLT_EnteredDate,");
        sb.Append("CNSLT_EnteredTime,");
        sb.Append("CNSLT_EnteredByUserDR,");
        sb.Append("$$CO17^XT58(CNSLT_EnteredByUserDR) AS EnteredByDispName,");
        sb.Append("CNSLT_ProcessedDate,");
        sb.Append("CNSLT_ProcessedTime,");
        sb.Append("CNSLT_ProcessedByUserDR As CurrentConsultant,");
        sb.Append("$$CO17^XT58(CNSLT_ProcessedByUserDR) As CurrentConsultantDispName,");
        sb.Append("CNSLT_DateResolved As FinalizedDate,");
        sb.Append("CNSLT_TimeResolved As FinalizedTime,");
        sb.Append("CNSLT_ClosedByUserDR As ClosedByUser,");
        sb.Append("$$CO17^XT58(CNSLT_ClosedByUserDR) As ClosedByUserDispName,");
        sb.Append("({fn CONVERT(CNSLT_DateResolved,SQL_INTEGER)}-{fn CONVERT(CNSLT_EnteredDate,SQL_INTEGER)}) AS DateDifference,");
        sb.Append("({fn CONVERT(CNSLT_TimeResolved,SQL_INTEGER)}-{fn CONVERT(CNSLT_EnteredTime,SQL_INTEGER)}) AS TimeDifference,");
        sb.Append("CNSLT_TATNoteDR AS CNSLT_TATNoteRef,");
        sb.Append("CNSLT_TATNoteDR->CNOTE_NotesText AS CNSLT_TATNoteText,");
        sb.Append("CNSLT_TATNoteDR->CNOTE_EnteredDate AS CNSLT_TATNoteAddedDate,");
        sb.Append("CNSLT_TATNoteDR->CNOTE_EnteredTime AS CNSLT_TATNoteAddedTime,");
        sb.Append("$$CO17^XT58(CNSLT_TATNoteDR->CNOTE_EnteredByUserDR) AS CNSLT_TATNoteAddedBy,");
        sb.Append("CNSLT_SecondRequestDate AS SecondReqDate,");
        sb.Append("CNSLT_SecondRequestTime AS SecondReqTime,");
        sb.Append("$$CO17^XT58(CNSLT_SecondRequestUserDR) AS SecondReqByDispName");
        sb.Append(" FROM ORD_Consultation");
        sb.Append(" LEFT OUTER JOIN");
        sb.Append(" CLF_ClientFile Client on CNSLT_ClientDR = Client.CLF_CLMNE");
        sb.Append(" LEFT OUTER JOIN ");
        sb.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        if (secondReqTAT)
        {
            sb.Append(" WHERE CNSLT_IsSecondRequest='Y'");
        }
        else
        {
            sb.Append(" WHERE CNSLT_Priority='S'");
        }

        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredDate>= TO_DATE('", dateFrom, "','MM/dd/yyyy') AND CNSLT_EnteredDate<= TO_DATE('", dateTo, "','MM/dd/yyyy')"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (intExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", intExternal, "'"));
        }
        sb.Append(" ORDER BY DateDifference DESC,TimeDifference DESC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    internal static String sendExternalConsultEmail(string mainAccession, string relatedAccString, string contactPerson,
        string contactDetails, string priority, string additionalNotes,string consultReason, string replyToUser, string emailSubject,
        string earliestCallbackDate, string earliestCallbackTime,string latestCallBackDate, string latestCallbackTime,
        string additionalCallBackInst, string timeZone)
    {
        return "";
    }

    internal static String insertConsultation(string MainAccession, string RelatedAccString, string ContactPerson, string ContactDetails,
        string Speciality, string Priority, string ReqConsultant, string Notes, string UserEnteredBy, string client, string method,
        string consultReason,string earliestCallbackDate,string earliestCallbackTime,string latestCallBackDate,string latestCallbackTime,
        string additionalCallBackInst, string timeZone, string isAllAccessionsRetrived, string personContactedAtClinic, string relatedAccessions,
        string Status,string serverLocation,string lab)
    {
        Dictionary<string, string> ConsultData = new Dictionary<string, string>();
        ConsultData.Add("MAINACC", MainAccession);
        ConsultData.Add("ACCSTRING", RelatedAccString);
        ConsultData.Add("CONTPERSON", ContactPerson);
        ConsultData.Add("CONTDETAILS", ContactDetails);
        ConsultData.Add("SPECIALTY", Speciality);
        ConsultData.Add("PRIORITY", Priority);
        ConsultData.Add("REQCONS", ReqConsultant);
        ConsultData.Add("NOTE", Notes);
        ConsultData.Add("STATUS", Status);
        ConsultData.Add("USER", UserEnteredBy);
        ConsultData.Add("Cleint", client);
        ConsultData.Add("method", method);
        ConsultData.Add("CONSLTREASON", consultReason);
        ConsultData.Add("EarliestCallBackDate", earliestCallbackDate);
        ConsultData.Add("EarliestCallBackTime", earliestCallbackTime);
        ConsultData.Add("LatestCallBackDate", latestCallBackDate);
        ConsultData.Add("LatestCallBackTime", latestCallbackTime);
        ConsultData.Add("AddCallBackInst", additionalCallBackInst);
        ConsultData.Add("TimeZone", timeZone);
        ConsultData.Add("IsAllAccessionsRetrived", isAllAccessionsRetrived);
        ConsultData.Add("PersonContactedAtClinic", personContactedAtClinic);
        ConsultData.Add("relatedAccessions", relatedAccessions);
        ConsultData.Add("serverLocation", serverLocation);
        ConsultData.Add("LAB", lab);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_SaveConsultation(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", ConsultData).Value.ToString();
    }
    //-SSM

    internal static string updateConsultation(string cnsltRow, string contactPerson, string alternateNo, string reasonForConsult
        , string addtionalComments,string earliestCallbackDate, string earliestCallbackTime, string latestCallBackDate, string latestCallbackTime
        , string additionalCallbackInst, string timeZone, string Priority, string user, string secondRequest)
    {
        Dictionary<string, string> ConsultData = new Dictionary<string, string>();
        ConsultData.Add("CNSLTROW",cnsltRow);
        ConsultData.Add("CONTACTPERSON", contactPerson);
        ConsultData.Add("ALTERNATENO", alternateNo);
        ConsultData.Add("REASONFORCONSULT", reasonForConsult);
        ConsultData.Add("ADDTIONALCOMMENTS", addtionalComments);
        ConsultData.Add("EARLIESTCALLBACKDATE", earliestCallbackDate);
        ConsultData.Add("EARLIESTCALLBACKTIME", earliestCallbackTime);
        ConsultData.Add("LATESTCALLBACKDATE", latestCallBackDate);
        ConsultData.Add("LATESTCALLBACKTIME", latestCallbackTime);
        ConsultData.Add("ADDITIONALCALLBACKINST", additionalCallbackInst);
        ConsultData.Add("TIMEZONE", timeZone);
        ConsultData.Add("PRIORITY", Priority);
        ConsultData.Add("USER",user);
        ConsultData.Add("SECONDREQUEST", secondRequest);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdateConsult(?,?,?,?,?,?,?,?,?,?,?,?,?,?)", ConsultData).Value.ToString();
    }

    internal static void sendExternalEmail(string consultRow, string mainAccession, string user, string requestedConsultant, 
        string contactPerson, string contactDetails, string consultReason, string priority, string note, string earliestCallBackDate, 
        string earliestCallBackTime, string latestCallBackDate, string latestCallBackTime, string timeZone, string addCallBackInst,
        string allAccReceived, string strPDFFileNames, string replyToUserMail)
    {
        Dictionary<string, string> ExternalConsultData = new Dictionary<string, string>();
        ExternalConsultData.Add("CONSULTROW", consultRow);
        ExternalConsultData.Add("MainAcc", mainAccession);
        ExternalConsultData.Add("User", user);
        ExternalConsultData.Add("RequestedCnsltant", requestedConsultant);
        ExternalConsultData.Add("ContactPerson", contactPerson);
        ExternalConsultData.Add("ContactDetails", contactDetails);
        ExternalConsultData.Add("CnsltReson", consultReason);
        ExternalConsultData.Add("Priority", priority);
        ExternalConsultData.Add("Note", note);
        ExternalConsultData.Add("EarliestCallBackDate", earliestCallBackDate);
        ExternalConsultData.Add("EarliestCallBackTime", earliestCallBackTime);
        ExternalConsultData.Add("LatestCallBackDate", latestCallBackDate);
        ExternalConsultData.Add("LatestCallBackTime", latestCallBackTime);
        ExternalConsultData.Add("TimeZone", timeZone);
        ExternalConsultData.Add("AddCallBackInst", addCallBackInst);
        ExternalConsultData.Add("AllAccReceived", allAccReceived);
        ExternalConsultData.Add("PDFFileNames", strPDFFileNames);
        ExternalConsultData.Add("ReplyToUserMail", replyToUserMail);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        cache.StoredProcedure("?=call SP2_SendExternalConsultEmail(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", ExternalConsultData).Value.ToString();
    }
    //+SSM 10/11/2011  #116096 AntechCSM-2B  Consult Record Check-in Check-Out Process
    internal static string updateCheckOutStatus(string CheckedOut, string ConsultRowId, string UserId, string specialty, string reqConsultant)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        String timeFormat = UIfunctions.getTimeFormat();
        if (specialty.Length > 0 && reqConsultant.Length > 0 && !CheckedOut.Equals("PEND"))
        {
            DataTable dtUser = cache.FillCacheDataTable("SELECT User_UserID FROM DIC_User WHERE USER_ConsultantDR= '" + reqConsultant + "'");
            if (dtUser.Rows.Count > 0)
            {
                if (dtUser.Rows[0]["User_UserID"].ToString().Length != 0)
                {
                    UserId = dtUser.Rows[0]["User_UserID"].ToString();
                }
            }
        }
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("Update ORD_Consultation set CNSLT_ProcessingStatus = '" + CheckedOut + "',");
        sb.Append("CNSLT_ProcessedByUserDR = '" + UserId + "',");
        sb.Append("CNSLT_ProcessedDate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',");
        sb.Append("CNSLT_ProcessedTime='" + DateTime.Now.ToString(timeFormat) + "'");
        if (specialty.Length > 0 && reqConsultant.Length > 0)
        {
            sb.Append(",CNSLT_SpecialtyDR='" + specialty + "'");
            sb.Append(",CNSLT_RequestedConsultantDR='" + reqConsultant + "'");
        }
        sb.Append("where CNSLT_RowID = '" + ConsultRowId + "'");
        sb.Append("");
        int rows = cache.Insert(sb.ToString());
        DataTable dtAccession = cache.FillCacheDataTable("SELECT CNSLT_AccessionDR FROM ORD_Consultation WHERE 	CNSLT_RowID = '" + ConsultRowId + "'");
        if (dtAccession.Rows.Count > 0)
        {
            Dictionary<String, String> updateCheckout = new Dictionary<String, String>();
            updateCheckout.Add("ACCESSION", dtAccession.Rows[0][0].ToString());
            updateCheckout.Add("ConsultROWID", ConsultRowId);
            updateCheckout.Add("USERID", UserId);
            
            string strSPMethod = string.Empty;
            if (CheckedOut.Equals("PEND"))
            {
                strSPMethod = "UnCheckOutConsultRecord";
            }
            else if (CheckedOut.Equals("PROG"))
            {
                strSPMethod = "CheckOutConsultRecord";
            }
            if (strSPMethod.Length > 0)
            {
                return cache.StoredProcedure(string.Concat("?=call SP2_", strSPMethod, "(?,?,?)"), updateCheckout).Value.ToString();
            }
        }
        return null;
    }
    //-SSM

    internal static DataTable getConsultationDetails(string consultNo, string setReadFlag)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CNSLT_RowID AS RowId,");
        sb.Append("CNSLT_AccessionDR AS ACCESSION,");
        sb.Append("CNSLT_AccessionDR->ACC_PetFirstName AS PETNAME,");
        sb.Append("CNSLT_AccessionDR->ACC_OwnerLastName AS OWNERNAME,");
        sb.Append("CNSLT_AccessionDR->ACC_MiniLogDate As ORDERDATE,");
        sb.Append("CNSLT_AccessionDR->ACC_MiniLogTime As ORDERTIME,");
        sb.Append("CNSLT_AccessionDR->ACC_TestsOrderedDisplayString As TestString,");
        //sb.Append("CNSLT_AccessionDR->ACC_ClientDR->CLF_CLNUM AS ACCOUNT,");
        sb.Append("CNSLT_ClientDR->CLF_CLNUM AS ACCOUNT,");
        sb.Append("CNSLT_ConsultationNumber,");
        sb.Append("CNSLT_ContactPerson,");
        sb.Append("CNSLT_ContactDetails,");
        sb.Append("CNSLT_ConsultReasonDR->CNSR_Name AS ConsultReason,");
        //sb.Append("CNSLT_CallbackDate AS CallbackDate,");
        //sb.Append("CNSLT_CallbackTime AS CallbackTime,");
        sb.Append("%EXTERNAL(CNSLT_Priority) AS Priority,");
        sb.Append("CNSLT_ProcessingStatus AS StatusCode,");
        sb.Append("%EXTERNAL(CNSLT_ProcessingStatus) AS Status,");
        sb.Append("CNSLT_SpecialtyDR->SPEC_Name AS Specialty,");
        sb.Append("CNSLT_SpecialtyDR,"); 
        sb.Append("CNSLT_RequestedConsultantDR,");
        sb.Append("CNSLT_RequestedConsultantDR->CONS_Name As RequestedConsultant,");
        sb.Append("CNSLT_EnteredByUserDR,");
        sb.Append("CNSLT_EnteredDate,");
        sb.Append("CNSLT_EnteredTime,");
        sb.Append("CNSLT_ProcessedByUserDR,");
        sb.Append("CNSLT_ProcessedDate,");
        sb.Append("CNSLT_ProcessedTime,");
        sb.Append("CNSLT_ResolvedByConsultantDR,");
        sb.Append("CNSLT_EarliestCallbackDate,");
        sb.Append("CNSLT_EarliestCallbackTime,");
        sb.Append("CNSLT_LastestCallbackDate,");
        sb.Append("CNSLT_LatestCallbackTime,");
        sb.Append("CNSLT_AdditionalCallbackInst,");
        sb.Append("CNSLT_CallbackTimeZone,");
        sb.Append("CNSLT_UnrelatedAccessions,");
        sb.Append("CNSLT_RequestedMethod,");
        sb.Append("CNSLT_PersonContactedAtClinic,");
        sb.Append("CNSLT_IsUpdated,");
        sb.Append("CNSLT_IsSecondRequest,");
        sb.Append("CNSLT_TATNoteDR,");
        sb.Append("CNSLT_TATNoteDR->CNOTE_NotesText AS CNOTE_NotesText");
        sb.Append(" FROM ORD_Consultation");
        sb.Append(" WHERE CNSLT_RowID='" + consultNo + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        if (returnDataTable.Rows.Count > 0)
        {
            if (setReadFlag=="Y" && returnDataTable.Rows[0]["CNSLT_IsUpdated"].ToString().Equals("Y"))
            {
                setConsultRead(consultNo, "N");
            }
        }
        else
        {
            returnDataTable = null;
        }
        return returnDataTable;
    }

    /// <summary>
    /// Retrieves the consult communication TAT Note
    /// </summary>
    /// <param name="rowId">The consult Row ID</param>
    /// <returns>A DataTable containing the TAT Note Details</returns>
    internal static DataTable GetConsultationCommunicationNote(string consultNo)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CNSLT_RowID AS RowID,");
        sb.Append("CNSLT_TATNoteDR AS TATNoteRow,");
        sb.Append("CNSLT_TATNoteDR->CNOTE_NotesText AS TATNoteText");
        sb.Append(" FROM ORD_Consultation");
        sb.Append(" WHERE CNSLT_RowID='" + consultNo + "'");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    internal static DataTable GetProgressNotes(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("%EXTERNAL(CNOTE_EnteredDate) AS EnteredDate,");
        sbSQL.Append("%EXTERNAL(CNOTE_EnteredTime) AS EnteredTime,");
        sbSQL.Append("$$CO17^XT58(CNOTE_EnteredByUserDR->USER_UserID) AS EnteredByUserID,");
        sbSQL.Append("CNOTE_NotesText AS ProgressNote ");

        sbSQL.Append("FROM CNSLT_ConsultationNotes ");
        sbSQL.Append("WHERE CNOTE_CNSLT_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");
        sbSQL.Append("AND CNOTE_NotesText <> '' ");
        sbSQL.Append("ORDER BY CNOTE_EnteredDate DESC,CNOTE_EnteredTime DESC, CNOTE_ChildSub DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    internal static DataTable GetAddedAccessions(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("CRA_AccessionDR AS Accession,");
        sbSQL.Append("CRA_AccessionDR->ACC_PatientName AS PatientName,");
        sbSQL.Append("CRA_AccessionDR->ACC_MaxiLogDate As MaxiLogDate, ");
        sbSQL.Append("CRA_AccessionDR->ACC_MaxiLogTime As MaxiLogTime, ");
        sbSQL.Append("CRA_AccessionDR->ACC_TestsOrderedDisplayString AS RequestedTests ");
        sbSQL.Append("FROM CNSLT_ConsultationRelatedAccession ");
        sbSQL.Append("WHERE CRA_CNSLT_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    //+SSM 11/11/2011 #116096 Added for Updating Consult Progress notes
    internal static string UpdateProgressNote(string rowID, string progressNote, string user)
    {
        Dictionary<String, String> ConsultNote = new Dictionary<String, String>();
        ConsultNote.Add("RowId", rowID);
        ConsultNote.Add("ProgressNote", progressNote);
        ConsultNote.Add("User", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_InsertConsultNote(?,?,?)", ConsultNote).Value.ToString();
    }
    //-SSM

    /// <summary>
    /// Insert / Update the TAT Note Added
    /// </summary>
    /// <param name="rowID"> The Consult Row Id </param>
    /// <param name="progressNote">The actual TAT Note content</param>
    /// <param name="user">The user adding/modifying the note</param>
    /// <returns>A SUCCESS string on sucessful updation/insertion</returns>
    internal static string AddCommunicationTATNote(string rowID, string progressNote, string user)
    {
        Dictionary<String, String> ConsultNote = new Dictionary<String, String>();
        ConsultNote.Add("RowId", rowID);
        ConsultNote.Add("ProgressNote", progressNote);
        ConsultNote.Add("User", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_InsertCommunicationNote(?,?,?)", ConsultNote).Value.ToString();
    }

    //+SSM 11/18/2011 AntechCSM 2a2 #Issue117055 Fetching all Consultants
    internal static DataTable getAllRequestedConsultants()
    {
        String selectStatement = "SELECT CONS_RowID As RowId, CONS_Code As CONSCode,CONS_Name As CONSName, CONS_LastFirstName AS DisplayName FROM DIC_Consultant ORDER BY CONS_LastFirstName ASC";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    //-SSM

    internal static DataTable getReasonsForConsult()
    {
        String selectStatement = "SELECT CNSR_RowID As RowId, CNSR_Code As CNSRCode,CNSR_Name As CNSRName FROM DIC_ConsultReason";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    internal static DataTable verifyExistingRecords(string clientAccountNo, string dateTo, string dateFrom,string status)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT CNSLT_ConsultationNumber AS ConsultNo,CNSLT_SpecialtyDR->SPEC_Name AS Specialty,");
        sbSQL.Append("CNSLT_AccessionDR AS Accession,CNSLT_UnrelatedAccessions AS UnrelatedAccessions,");
        sbSQL.Append("CNSLT_AccessionDR->ACC_PatientName As Onwer,CNSLT_AccessionDR->ACC_PetFirstName As PetFirstName,");
        sbSQL.Append("%EXTERNAL(CNSLT_EnteredDate) As EnteredDate");
        sbSQL.Append(" FROM ORD_Consultation");
        sbSQL.Append(" WHERE CNSLT_EnteredDate>= TO_DATE('");
        sbSQL.Append(dateFrom);
        sbSQL.Append("','MM/dd/yyyy') AND CNSLT_EnteredDate<= TO_DATE('");
        sbSQL.Append(dateTo);
        sbSQL.Append("','MM/dd/yyyy') AND CNSLT_ClientDR->CLF_CLNUM  ='");
        sbSQL.Append(clientAccountNo);
        sbSQL.Append("' AND CNSLT_ProcessingStatus <> '");
        sbSQL.Append(status);
        sbSQL.Append("' AND CNSLT_ProcessingStatus <> 'DEL'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    internal static string addCompleteConsultDetails(string consultID, string consultResaon, string consultNotes, string consultUser, string consultPersonContacted, string consultContactedOn, string callerName, string resolverByConsultant)
    {
        Dictionary<String, String> completeConsultDetails = new Dictionary<string, string>();
        completeConsultDetails.Add("CNSLTROW", consultID);
        completeConsultDetails.Add("CNSLTREASON", consultResaon);
        completeConsultDetails.Add("NOTE", consultNotes);
        completeConsultDetails.Add("USER", consultUser);
        completeConsultDetails.Add("PERCONT", consultPersonContacted);
        completeConsultDetails.Add("ONDATE", consultContactedOn);
        completeConsultDetails.Add("CALLERNAME", callerName);
        completeConsultDetails.Add("RESOLVEDBYCONSULTANT", resolverByConsultant);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_CompleteConsult(?,?,?,?,?,?,?,?)", completeConsultDetails).Value.ToString();
    }

    internal static string autoCompleteConsultDetails(string consultID, string consultUser,string reasonForComm)
    {
        Dictionary<String, String> completeConsultDetails = new Dictionary<string, string>();
        completeConsultDetails.Add("CNSLTROW", consultID);
        completeConsultDetails.Add("USER", consultUser);
        completeConsultDetails.Add("REASONFORCOMM", reasonForComm);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_AutoCompleteConsult(?,?,?)", completeConsultDetails).Value.ToString();
    }

    internal static DataTable getReasonsForCommunication()
    {
        String selectStatement = "SELECT CCR_RowID As RowId, CCR_Code As CCRCode,CCR_Name As CCRName FROM DIC_CnsltCommunicationReason";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    internal static DataTable ConsultSendToList()
    {
        DataTable returnDataTable = new DataTable();
        string selectStatement = "SELECT MGRP_GroupID GROUP_ID, MGRP_GroupName GROUP_NAME, MGRP_UserList USERS, MGRP_IsDefault IS_DEFAULT FROM DIC_MailGroup WHERE MGRP_ShowInConsultCommunications='Y'";
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

    internal static DataTable GetRelatedAccession(string strRelatedAccession)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_Accession,");
        sb.Append("ACC_OwnerLastName||', '||ACC_PetFirstName AS PATIENT,");
        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("'' As COMPUTEDDATETIME,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime As ORDERTIME ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("ACC_Accession IN (");
        sb.Append(strRelatedAccession);
        sb.Append(")");
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
        if (returnDataTable != null)
        {
            for (int iRowCount = 0; iRowCount < returnDataTable.Rows.Count; iRowCount++)
            {
                DateTime dtOrderDateTime = Convert.ToDateTime(returnDataTable.Rows[iRowCount]["ORDERDATE"]);
                TimeSpan dtOrderTimeSpan = (TimeSpan)returnDataTable.Rows[iRowCount]["ORDERTIME"];
                string strOrderTime = string.Empty;
                if (dtOrderTimeSpan.Hours > 11 && dtOrderTimeSpan.Hours < 24)
                {
                    if (dtOrderTimeSpan.Hours > 12)
                    {
                        string strHour = (dtOrderTimeSpan.Hours - 12).ToString();
                        if (strHour.Trim().Length == 1)
                        {
                            strHour = "0" + strHour.Trim();
                        }
                        strOrderTime = strHour + ":" + dtOrderTimeSpan.Minutes.ToString() + " PM";
                    }
                    else
                    {
                        strOrderTime = dtOrderTimeSpan.Hours.ToString() + ":" + dtOrderTimeSpan.Minutes.ToString() + " PM";
                    }

                }
                else if (dtOrderTimeSpan.Hours == 24)
                {
                    strOrderTime = "12:" + dtOrderTimeSpan.Minutes.ToString() + " AM";
                }
                else
                {
                    string strHour = dtOrderTimeSpan.Hours.ToString();
                    if (strHour.Trim().Length == 1)
                    {
                        strHour = "0" + strHour.Trim();
                    }
                    strOrderTime = strHour + ":" + dtOrderTimeSpan.Minutes.ToString() + " AM";
                }
                returnDataTable.Rows[iRowCount]["COMPUTEDDATETIME"] = dtOrderDateTime.ToShortDateString() + " " + strOrderTime;
            }
        }
        return returnDataTable;
    }

    internal static DataTable ConsultSummaryIES(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT CNSLT_EnteredDate, ");
        sb.Append("CNSLT_SpecialtyDR, ");
        sb.Append("CNSLT_RequestedConsultantDR->CONS_Location AS LOCATION, COUNT(*) AS CnsltCount ");
        sb.Append("FROM ");
        sb.Append("ORD_Consultation ");
        sb.Append("WHERE 1=1 ");
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sb.Append("GROUP BY ");
        sb.Append("CNSLT_EnteredDate, CNSLT_SpecialtyDR, CNSLT_RequestedConsultantDR->CONS_Location ");
        sb.Append("ORDER BY ");
        sb.Append("CNSLT_EnteredDate");
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

    internal static DataTable ConsultByAccountSummary(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT CNSLT_ClientDR, ");
        sb.Append("CNSLT_ClientDR->CLF_CLNUM, CNSLT_ClientDR->CLF_CLNAM, ");
        sb.Append("CNSLT_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode, ");
        sb.Append("CNSLT_ClientDR->CLF_RSTOP, CNSLT_ClientDR->CLF_CLPHN, ");
        sb.Append("CNSLT_ClientDR->CLF_CLNUM, CNSLT_ClientDR->CLF_CLNAM, ");
        sb.Append("$$GETREVHIST^XT1(CNSLT_ClientDR) AS CLIENTREVENUE, ");
        sb.Append("CNSLT_ConsultReasonDR, CNSLT_RequestedMethod, COUNT(*) AS CnsltCount ");
        sb.Append("FROM ");
        sb.Append("ORD_Consultation ");
        sb.Append("WHERE 1=1 ");
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sb.Append("GROUP BY ");
        sb.Append("CNSLT_ConsultReasonDR, CNSLT_RequestedMethod, CNSLT_ClientDR ");
        sb.Append("ORDER BY ");
        sb.Append("CNSLT_ClientDR->CLF_CLNUM");
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

    internal static DataTable GetCommByConsultantsDetailsTable(string cosultDR, string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append(" CNSLT_RequestedConsultantDR->CONS_Name AS Consultant,");
        sb.Append(" CNSLT_ConsultationNumber AS ConsNo,");
        sb.Append(" CNSLT_CommunicationSentDate AS DateValue,");
        sb.Append(" CNSLT_CommunicationSentTime AS TimeValue,");
        sb.Append(" CNSLT_CommunicationDest AS Dest,");
        sb.Append(" CNSLT_CommunicationReasonDR->CCR_Name AS Type,");
        sb.Append(" CNSLT_CommunicationAccns AS Accns,");
        sb.Append(" CNSLT_CommunicationTests AS Tests");
        sb.Append(" FROM");
        sb.Append(" ORD_Consultation ");
        sb.Append(String.Concat("WHERE CNSLT_RequestedConsultantDR='", cosultDR, "'"));
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sb.Append(" ORDER BY ");
        sb.Append(" CNSLT_ConsultationNumber");
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
    
    internal static DataTable GetCommunicationsByConsultantsSummary(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append(" CNSLT_RequestedConsultantDR,");
        sb.Append(" CNSLT_RequestedConsultantDR->CONS_Name AS Consultant,");
        sb.Append(" CNSLT_CommunicationReasonDR AS CommReason,  COUNT(*) AS CnsltCount");
        sb.Append(" FROM");
        sb.Append(" ORD_Consultation ");
        sb.Append("WHERE 1=1 ");
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sb.Append(" GROUP BY ");
        sb.Append(" CNSLT_RequestedConsultantDR, CNSLT_SpecialtyDR, CNSLT_CommunicationReasonDR");
        sb.Append(" ORDER BY ");
        sb.Append(" CNSLT_RequestedConsultantDR");
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

    internal static DataTable ConsultByAccountDetails(string strCoasultAccountNo, string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority,string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CNSLT_ConsultationNumber, CNSLT_RequestedConsultantDR->CONS_Name AS ConsultaneName, ");
        sb.Append("CNSLT_ConsultReasonDR->CNSR_Name As ConsultationReason, CNSLT_AccessionDR, ");
        sb.Append("%EXTERNAL(CNSLT_RequestedMethod) As RequestMethod, CNSLT_EnteredDate, %EXTERNAL(CNSLT_ProcessingStatus) AS ProcessStatus");
        sb.Append(" FROM ");
        sb.Append("ORD_Consultation ");
        sb.Append("WHERE CNSLT_ClientDR='");
        sb.Append(strCoasultAccountNo);
        sb.Append("' ");
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sb.Append("ORDER BY ");
        sb.Append("CNSLT_ConsultationNumber");
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

    internal static DataTable ConsultByUserAgentDetails(string strUserDR, string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CNSLT_ConsultationNumber, CNSLT_EnteredByUserDR->USER_LastFirstName AS UserEntered, ");
        sb.Append("CNSLT_EnteredDate, CNSLT_EnteredTime, ");
        sb.Append("%EXTERNAL(CNSLT_RequestedMethod) As RequestMethod, CNSLT_RequestedConsultantDR->CONS_Location As Type, ");
        sb.Append("CNSLT_SpecialtyDR->SPEC_Name As Specialty");
        sb.Append(" FROM ");
        sb.Append("ORD_Consultation ");
        sb.Append("WHERE CNSLT_EnteredByUserDR='");
        sb.Append(strUserDR);
        sb.Append("' ");
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sb.Append(" ORDER BY ");
        sb.Append("CNSLT_ConsultationNumber");
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

    internal static DataTable ConsultByUserAgentSummary(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append(" CNSLT_EnteredByUserDR,");
        sb.Append(" CNSLT_EnteredByUserDR->USER_LastFirstName AS UserEntered,");
        sb.Append(" CNSLT_EnteredDate, CNSLT_RequestedMethod,");
        sb.Append(" CNSLT_RequestedConsultantDR->CONS_Location AS IntExt,");
        sb.Append(" CNSLT_SpecialtyDR, CNSLT_SpecialtyDR->SPEC_Name SpecilityName,  COUNT(*) AS CnsltCount");
        sb.Append(" FROM");
        sb.Append(" ORD_Consultation ");
        sb.Append("WHERE 1=1 ");
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sb.Append(" GROUP BY ");
        sb.Append(" CNSLT_EnteredByUserDR, CNSLT_RequestedMethod, CNSLT_RequestedConsultantDR->CONS_Location, CNSLT_SpecialtyDR");
        sb.Append(" ORDER BY ");
        sb.Append(" CNSLT_EnteredByUserDR");
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

    internal static DataTable ConsultSummaryRequestedMethod(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CNSLT_EnteredDate, CNSLT_RequestedMethod As RequestMethod , COUNT(*) AS CnsltCount");
        sb.Append(" FROM ");
        sb.Append("ORD_Consultation ");
        sb.Append("WHERE 1=1 ");
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        sb.Append("GROUP BY ");
        sb.Append("CNSLT_RequestedMethod,CNSLT_EnteredDate ");
        sb.Append("ORDER BY ");
        sb.Append("CNSLT_EnteredDate");
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
    internal static DataTable getConsultGridRecordsReport(string consultNumber, string accountNumber, string accessionNumber, string dateFrom, string dateTo, string status, string method, string requestedConsultant, string resolvedBy, string reasonForConsult, string priority, string speciality, string enteredBy, string internalExternal)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM AS ACCOUNT,");
        sb.Append("CLF_CLNAM AS AccountName,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sb.Append("CNSLT_ConsultationNumber AS ConsultNo,");
        sb.Append("%EXTERNAL(CNSLT_Priority) AS Priority,");
        sb.Append("CNSLT_CallbackTimeZone AS TimeZone,");
        sb.Append("%EXTERNAL(CNSLT_RequestedMethod) As RequestedMethod,");
        sb.Append("CNSLT_SpecialtyDR->SPEC_Name AS Specialty,");
        sb.Append("CNSLT_RequestedConsultantDR->CONS_Name As RequestedConsultant,");
        sb.Append("CNSLT_ConsultReasonDR->CNSR_Name As ReasonForConsult,");
        sb.Append("%EXTERNAL(CNSLT_ProcessingStatus) AS StatusText,");
        sb.Append("CNSLT_EnteredByUserDR,");
        sb.Append("CNSLT_EnteredDate,");
        sb.Append("CNSLT_EnteredTime,");
        sb.Append("CNSLT_DateResolved,");
        sb.Append("CNSLT_TimeResolved,");
        sb.Append("CNSLT_ResolvedByConsultantDR->CONS_Name As CompletedBy,");
        sb.Append("CNSLT_ContactPerson,");
        sb.Append("CLF_CLPHN AS ACCOUNTPHONE,");
        sb.Append("CNSLT_ContactDetails,");
        sb.Append("CNSLT_AccessionDR,");
        sb.Append("CNSLT_UnrelatedAccessions As Accessions,");
        sb.Append("CNSLT_RelatedAccessions As RelatedAccessions,");
        sb.Append("$$GETPROGRESSNOTE^XT120(CNSLT_RowID) AS ProgressNote");
        sb.Append(" FROM ORD_Consultation");
        sb.Append(" LEFT OUTER JOIN");
        sb.Append(" CLF_ClientFile Client on CNSLT_ClientDR = Client.CLF_CLMNE ");
        sb.Append(" LEFT OUTER JOIN ");
        sb.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sb.Append("WHERE 1=1 ");
        if (dateFrom.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate>= TO_DATE('");
            sb.Append(dateFrom);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (dateTo.Length > 0)
        {
            sb.Append("AND CNSLT_EnteredDate<= TO_DATE('");
            sb.Append(dateTo);
            sb.Append("','MM/dd/yyyy') ");
        }
        if (consultNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RowID  ='", consultNumber, "'"));
        }
        if (accountNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ClientDR->CLF_CLNUM  ='", accountNumber, "'"));
        }
        if (accessionNumber.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_AccessionDR->ACC_Accession  ='", accessionNumber, "'"));
        }
        if (status.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ProcessingStatus ='", status, "'"));
        }
        if (method.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedMethod ='", method, "'"));
        }
        if (requestedConsultant.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR ='", requestedConsultant, "'"));
        }
        if (resolvedBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ResolvedByConsultantDR ='", resolvedBy, "'"));
        }
        if (reasonForConsult.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_ConsultReasonDR ='", reasonForConsult, "'"));
        }
        if (priority.Length > 0)
        {
            if (priority.Trim().ToUpper() == "NS")
            {
                sb.Append(" AND  (CNSLT_Priority = 'NS' OR CNSLT_Priority IS NULL)");
            }
            else if (priority.Trim().ToUpper() == "S")
            {
                sb.Append(" AND CNSLT_Priority = 'S'");
            }
        }
        if (speciality.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_SpecialtyDR ='", speciality, "'"));
        }
        if (enteredBy.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_EnteredByUserDR ='", enteredBy, "'"));
        }
        if (internalExternal.Length > 0)
        {
            sb.Append(String.Concat(" AND CNSLT_RequestedConsultantDR->CONS_Location ='", internalExternal, "' "));
        }
        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(selectStatement);
        return returnDataTable;
    }

    public static string getConsultMatrix(String year, String allSpecialty, String specialtyList, String allReason, String reasonList, String allClients, String clientList)
    {
        string strRetVal = "";

        Dictionary<String, String> cnsltMatrix = new Dictionary<String, String>();
        cnsltMatrix.Add("Year", year);
        cnsltMatrix.Add("AllSpecialty", allSpecialty);
        cnsltMatrix.Add("SpecialtyList", specialtyList);
        cnsltMatrix.Add("AllReason", allReason);
        cnsltMatrix.Add("ReasonList", reasonList);
        cnsltMatrix.Add("AllClients", allClients);
        cnsltMatrix.Add("ClientList", clientList);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        strRetVal = cache.StoredProcedure("?=call SP2_GetConsultMatrix(?,?,?,?,?,?,?)", cnsltMatrix, 32000).Value.ToString();
        return strRetVal;
    }

    public static void updateComnReason(string cnsltRow, string reasonForComm, string toGroup, string accessions, string tests)
    {
        Dictionary<String, String> updateConsult = new Dictionary<String, String>();
        updateConsult.Add("CNSLTROW", cnsltRow);
        updateConsult.Add("REASONFORCOMM", reasonForComm);
        updateConsult.Add("TOGROUP", toGroup);
        updateConsult.Add("ACCESSIONS", accessions);
        updateConsult.Add("TESTS", tests);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string str = cache.StoredProcedure("?=call SP2_UpdateCommunicationReason(?,?,?,?,?)", updateConsult).Value.ToString();
    }

    public static void deleteConsult(string cnsltRow,string user,string reason)
    {
        Dictionary<String, String> deleteConsult = new Dictionary<String, String>();
        deleteConsult.Add("CNSLTROW", cnsltRow);
        deleteConsult.Add("USER", user);
        deleteConsult.Add("REASON", reason);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string str = cache.StoredProcedure("?=call SP2_DeleteConsult(?,?,?)", deleteConsult).Value.ToString();
    }

    public static void updateConsultRowStatus(string cnsltRow, string status)
    {
        Dictionary<String, String> updateConsultStatus = new Dictionary<String, String>();
        updateConsultStatus.Add("CNSLTROW", cnsltRow);
        updateConsultStatus.Add("STATUS", status);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string str = cache.StoredProcedure("?=call SP2_UpdateConsultStatus(?,?)", updateConsultStatus).Value.ToString();
    }

    public static void updateQueueConsult(string cnsltRow, string status, string consultReason, string personContacted, string note, string user)
    {
        Dictionary<String, String> updateQueueCnslt = new Dictionary<string, string>();
        updateQueueCnslt.Add("CNSLTROW", cnsltRow);
        updateQueueCnslt.Add("STATUS", status);
        updateQueueCnslt.Add("CONSLTREASON", consultReason);
        updateQueueCnslt.Add("PERCONTACTED", personContacted);
        updateQueueCnslt.Add("NOTE", note);
        updateQueueCnslt.Add("USER", user);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string str = cache.StoredProcedure("?=call SP2_UpdateQueueConsult(?,?,?,?,?,?)", updateQueueCnslt).Value.ToString();
    }

    public static string sendConsultCommunication(string cnsltRow, string toGroups, string mailSubject, string mailBody, string ack, string ackMsgID, string fromUser, string mailSystem, string communicationReason, string testsToRecheck, string note)
    {
        Dictionary<String, String> cnsltComm = new Dictionary<String, String>();
        cnsltComm.Add("CnsltRow", cnsltRow);
        cnsltComm.Add("ToGroups", toGroups);
        cnsltComm.Add("MailSubject", mailSubject);
        cnsltComm.Add("MailBody", mailBody);
        cnsltComm.Add("Ack", ack);
        cnsltComm.Add("AckMsgID", ackMsgID);
        cnsltComm.Add("FromUser", fromUser);
        cnsltComm.Add("MailSystem", mailSystem);
        cnsltComm.Add("CommunicationReason", communicationReason);
        cnsltComm.Add("TestsToRecheck", testsToRecheck);
        cnsltComm.Add("Note", note);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();

        string strRetVal = cache.StoredProcedure("?=call SP2_SendConsultCommunication(?,?,?,?,?,?,?,?,?,?,?)", cnsltComm).Value.ToString();
        return strRetVal;
    }

    public static void setConsultRead(string consultNo,string isRecordRead)
    {
        Dictionary<String, String> _consultRead = new Dictionary<String, String>();
        _consultRead.Add("ConsultRow", consultNo);
        _consultRead.Add("Flag", isRecordRead);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        cache.StoredProcedure("?=call SP2_SetConsultReadFlag(?,?)", _consultRead);
    }
}