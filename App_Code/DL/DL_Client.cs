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
public class DL_Client
{
    public DL_Client()
    {

    }

    public static DataTable getClientDetails(String clientID)
    {

        #region Summary
        // CLF_CLNUM                                        - Account Number   (Client Number)
        // CLF_CLNAM                                        - Account Name     (Client Name)
        // CLF_CLMNE                                        - Account Mnemonic (Client Mnemonic)
        // CLF_CLPHN                                        - Phone Number 
        // CLF_CLAPH                                        - After Hours
        // CLF_CLAD1                                        - Address1
        // CLF_CLAD2                                        - Address2
        // CLF_CLAD3                                        - Address3
        // CLF_CLAD4                                        - Address4
        // CLF_CLTER                                        - Sales Territory
        // ZIP_SalespersonLastName||', '||ZIP_SalespersonFirstName AS SALES_REP               - Sales Representative
        // CLF_CLADG as 'AutoDial Group'                    - Autodial Groups
        // zoasis_num AS ZOASIS_NUMBER              - Zoasis Number
        // CLF_CLRTS                                        - Route Stop
        // CLF_LabLocationDR->LABLO_LabName AS LOCATION                 - Location
        // CLF_CLANAM                                       - Alpha Name
        // CLF_CLSRT                                        - Sort Key
        // CLF_CLFAX                                        - Fax
        // CLF_CLCON                                        - Ofc Contact (Office Contact)
        // CLF_CLTYP                                        - Description
        // CLF_PART                                         - Reporting Partials
        // CLF_PMB                                          - Report Micro Partials
        // CLF_PCOMP                                        - Comprehensive Finals
        // 'Courier Contact Test Data' as 'Courier_Data'    - Courier Contacts
        // CLF_FRUC                                         - Custom Unit Codes
        // CLF_FLASH                                        - Order Entry Flash Message
        // 'OEMOEMSG test data' as OEMOEMSG                 - Order Entry Instruction
        // ZIP_Abbreviation as Region                       - Region
        // ZIP_SalespersonExtension as SalesRepExt          - Sales Rep Extension
        // ZIP_SalespersonEmail as SalesRepEmail            - Sales Rep Email
        // CLF_TOUC                                         - Account Notes
        // isnull(REV_FirstMonthRevenue,0) As ClientFirstMonthRevenue
        // isnull(REV_SecondMonthRevenue,0) As ClientSecondMonthRevenue
        // isnull(REV_ThirdMonthRevenue,0) As ClientThirdMonthRevenue 
        #endregion Summary

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_InquiryMessage AS InquiryMessages,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLAPH,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG AS AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_CLANAM,");
        sb.Append("CLF_CLSRT,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLFAX,");
        sb.Append("CLF_CLCON,");
        sb.Append("CLF_CLTYP,");
        sb.Append("CLF_PART,");
        sb.Append("CLF_PMB,");
        sb.Append("CLF_PCOMP,");
        sb.Append("CLF_CustomUnitCodeString As CustomUnitCode,");
        sb.Append("CLF_FLASH,");
        sb.Append("CLF_OEInstructions AS OEMOEMSG,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepCellPhone AS SalesRepCellPh,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepExtension AS SalesRepExt,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepEmail AS SalesRepEmail,");
        sb.Append("CLF_SalesTerritoryDR->ST_LastUpdatedDate AS LastUpdated,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesManagerCellPhone AS SalesRepMgrCellPh,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesManagerExtension AS SalesRepMgrExt,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesManagerEmail AS SalesRepMgrEmail,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesManagerName AS SalesRepMgrName,");
        sb.Append("CLF_CustomerServiceNotes As AccountNotes,");
        sb.Append("CLF_PasswordPriceInquiry As PriceInqPasswd, ");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_TimeZone As ClientTimeZone,");
        sb.Append("CLF_Country As Country,");
        sb.Append("CLF_IsNew As ClientIsNew");

        sb.Append(" FROM");
        sb.Append(" CLF_ClientFile");
        sb.Append(" LEFT OUTER JOIN");
        sb.Append(" zoasis ON");
        sb.Append(" CLF_ClientFile.CLF_CLNUM = zoasis.CLN_RowID");
        sb.Append(" WHERE");
        sb.Append(" CLF_CLNUM = '" + clientID + "'");
        String selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    /// <summary>
    /// Get Client Revenue and IsHot flag.
    /// </summary>
    /// <param name="clientMnemonic">Client Mnemonic</param>
    /// <returns></returns>
    public static DataTable getClientRevenueDetails(String clientMnemonic)
    {
        System.Text.StringBuilder sbSQL = new System.Text.StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue,");
        sbSQL.Append("CLF_IsHot As ClientIsHot,");
        sbSQL.Append("CLF_IsAlliedClient As ClientIsAllied");

        sbSQL.Append(" FROM CLF_ClientFile");
        sbSQL.Append(" WHERE CLF_CLMNE = '");
        sbSQL.Append(clientMnemonic);
        sbSQL.Append("'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }
    public static DataTable getClientDetailsByMnemonic(String clientMnemonic)
    {

        #region Summary
        // CLF_CLNUM                                        - Account Number   (Client Number)
        // CLF_CLNAM                                        - Account Name     (Client Name)
        // CLF_CLMNE                                        - Account Mnemonic (Client Mnemonic)
        // CLF_CLPHN                                        - Phone Number 
        // CLF_CLAPH                                        - After Hours
        // CLF_CLAD1                                        - Address1
        // CLF_CLAD2                                        - Address2
        // CLF_CLAD3                                        - Address3
        // CLF_CLAD4                                        - Address4
        // CLF_CLTER                                        - Sales Territory
        // ZIP_SalespersonLastName||', '||ZIP_SalespersonFirstName AS SALES_REP               - Sales Representative
        // CLF_CLADG as 'AutoDial Group'                    - Autodial Groups
        // zoasis_num AS ZOASIS_NUMBER              - Zoasis Number
        // CLF_CLRTS                                        - Route Stop
        // CLF_LabLocationDR->LABLO_LabName AS LOCATION                 - Location
        // CLF_CLANAM                                       - Alpha Name
        // CLF_CLSRT                                        - Sort Key
        // CLF_CLFAX                                        - Fax
        // CLF_CLCON                                        - Ofc Contact (Office Contact)
        // CLF_CLTYP                                        - Description
        // CLF_PART                                         - Reporting Partials
        // CLF_PMB                                          - Report Micro Partials
        // CLF_PCOMP                                        - Comprehensive Finals
        // 'Courier Contact Test Data' as 'Courier_Data'    - Courier Contacts
        // CLF_FRUC                                         - Custom Unit Codes
        // CLF_FLASH                                        - Order Entry Flash Message
        // 'OEMOEMSG test data' as OEMOEMSG                 - Order Entry Instruction
        // ZIP_Abbreviation as Region                       - Region
        // ZIP_SalespersonExtension as SalesRepExt          - Sales Rep Extension
        // ZIP_SalespersonEmail as SalesRepEmail            - Sales Rep Email
        // CLF_TOUC                                         - Account Notes
        // isnull(REV_FirstMonthRevenue,0) As ClientFirstMonthRevenue
        // isnull(REV_SecondMonthRevenue,0) As ClientSecondMonthRevenue
        // isnull(REV_ThirdMonthRevenue,0) As ClientThirdMonthRevenue 
        #endregion Summary

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_InquiryMessage AS InquiryMessages,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLAPH,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG AS AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_CLANAM,");
        sb.Append("CLF_CLSRT,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLFAX,");
        sb.Append("CLF_CLCON,");
        sb.Append("CLF_CLTYP,");
        sb.Append("CLF_PART,");
        sb.Append("CLF_PMB,");
        sb.Append("CLF_PCOMP,");
        sb.Append("CLF_CustomUnitCodeString As CustomUnitCode,");
        sb.Append("CLF_FLASH,");
        sb.Append("CLF_OEInstructions AS OEMOEMSG,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepCellPhone AS SalesRepCellPh,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepExtension AS SalesRepExt,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepEmail AS SalesRepEmail,");
        sb.Append("CLF_SalesTerritoryDR->ST_LastUpdatedDate AS LastUpdated,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesManagerCellPhone AS SalesRepMgrCellPh,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesManagerExtension AS SalesRepMgrExt,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesManagerEmail AS SalesRepMgrEmail,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesManagerName AS SalesRepMgrName,");
        sb.Append("CLF_CustomerServiceNotes As AccountNotes,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("CLF_Country As Country");
        sb.Append(" FROM CLF_ClientFile");
        sb.Append(" LEFT OUTER JOIN zoasis ON");
        sb.Append(" CLF_ClientFile.CLF_CLNUM = zoasis.CLN_RowID");
        sb.Append(" WHERE CLF_CLMNE = '");
        sb.Append(clientMnemonic);
        sb.Append("'");
        String selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getRecentIssuesByClientID(Int32 noOfTopRecords, String clientID)
    {
        // PBT_PSQ As ProblemID                     = Problem ID [Primary Key]
        // PBT_ProbTP As ProblemCategory            = Problem Category/Type
        // PBT_LOGID As ProblemEnteredBy            = Problem Entered/Logged By
        // PBT_RESID As ProblemResolvedBy           = Problem Resolved By
        // PBT_Comment As ProblemDescription        = Problem Description/Comment
        // PBT_AccessionDR As ProblemAccessionNo    = Accession No [Foreign Key]
        // PBT_CLDesRef->CLF_CLNUM                  = Client ID [Foreign Key]
        // PBT_LoggedDate As ProblemLoggedDate      = Problem Entry/Log Date
        // PBT_LoggedTime As ProblemLoggedTime      = Problem Entry/Log Time
        // PBT_RESDate As ProblemResolutionDate     = Problem Resolution Date
        // PBT_RESTime As ProblemResolutionTime     = Problem Resolution Time
        // 'Tests Ordered Test Data' As TestOrdered = Tests Ordered
        // PBT_Resolution As ProblemResolution      = Problem Resolution

        #region Reference Query
        //SELECT TOP 5 
        //    PBT_PSQ As ProblemID, 
        //    PBT_ProbTP->PTYP_ProblemType As ProblemCategory,
        //    PBT_LOGID As ProblemEnteredBy,
        //    PBT_LoggedDate As ProblemLoggedDate,
        //    PBT_LoggedTime As ProblemLoggedTime,
        //    PBT_RESID As ProblemResolvedBy,
        //    PBT_RESDate As ProblemResolutionDate,
        //    PBT_REStime As ProblemResolutionTime, 
        //    PBT_Comment As ProblemDescription,
        //    PBT_Resolution As ProblemResolution,
        //    PBT_AccessionDR->ACC_Accession As ProblemAccessionNo,
        //    PBT_AccessionDR->ACC_TestsOrderedDisplayString
        //FROM 
        //    CSV_ProbTracking
        //WHERE
        //    PBT_CLDesRef->CLF_CLNUM = '99'
        //ORDER BY 
        //    PBT_LoggedDate, 
        //    PBT_LoggedTime
        #endregion Reference Query

        DataTable recentIssues = new DataTable();
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("TOP " + noOfTopRecords.ToString());
        sb.Append(" PBT_PSQ As ProblemID, ");
        sb.Append("PBT_ProbTP->PTYP_ProblemType As ProblemCategory, ");
        sb.Append("PBT_LOGID As ProblemEnteredBy, ");
        sb.Append("PBT_RESID As ProblemResolvedBy, ");
        sb.Append("PBT_Comment As ProblemDescription, ");
        sb.Append("PBT_AccessionDR->ACC_Accession As ProblemAccessionNo, ");
        sb.Append("PBT_LoggedDate As ProblemLoggedDate, ");
        sb.Append("PBT_LoggedTime As ProblemLoggedTime, ");
        sb.Append("PBT_RESDate As ProblemResolutionDate, ");
        sb.Append("PBT_RESTime As ProblemResolutionTime, ");
        sb.Append("PBT_AccessionDR->ACC_TestsOrderedDisplayString As TestOrdered, ");
        sb.Append("PBT_Resolution As ProblemResolution ");
        sb.Append("FROM ");
        sb.Append("CSV_PROBTRACKING ");
        sb.Append("WHERE ");
        sb.Append("PBT_CLDesRef->CLF_CLNUM = '" + clientID + "'");
        sb.Append("ORDER BY ");
        sb.Append("PBT_LoggedDate DESC, PBT_LoggedTime DESC, PBT_ROWID DESC");


        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        recentIssues = cache.FillCacheDataTable(selectStatement);
        return recentIssues;
    }

    public static DataTable getRecentIssuesByClientMnemonic(Int32 noOfTopRecords, String clientMnemonic)
    {
        #region Prepare SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("TOP " + noOfTopRecords.ToString());
        sb.Append(" PBT_PSQ As ProblemID, ");
        sb.Append("PBT_ProbTP->PTYP_ProblemType As ProblemCategory, ");
        sb.Append("$$CO17^XT58(PBT_LOGID) As ProblemEnteredBy, ");
        sb.Append("$$CO17^XT58(PBT_RESID) As ProblemResolvedBy, ");
        sb.Append("PBT_Comment As ProblemDescription, ");
        sb.Append("PBT_AccessionDR->ACC_Accession As ProblemAccessionNo, ");
        sb.Append("PBT_LoggedDate As ProblemLoggedDate, ");
        sb.Append("PBT_LoggedTime As ProblemLoggedTime, ");
        sb.Append("PBT_RESDate As ProblemResolutionDate, ");
        sb.Append("PBT_RESTime As ProblemResolutionTime, ");
        sb.Append("PBT_AccessionDR->ACC_TestsOrderedDisplayString As TestOrdered, ");
        sb.Append("PBT_Resolution As ProblemResolution ");
        sb.Append("FROM ");
        sb.Append("CSV_PROBTRACKING ");
        sb.Append("WHERE ");
        sb.Append("PBT_CLDesRef = '" + clientMnemonic + "'");
        sb.Append("ORDER BY ");
        sb.Append("PBT_LoggedDate DESC, PBT_LoggedTime DESC, PBT_ROWID DESC");
        String selectStatement = sb.ToString();

        #endregion Prepare SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getEventsByClientID(String clientID)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("EVENT_RowID AS EventRowId, ");
        sb.Append("EVENT_DateLogged  As EventLogDate, ");
        sb.Append("EVENT_TimeLogged  As EventLogTime, ");
        sb.Append("$$CO17^XT58(EVENT_Agent) As EventAgent, ");
        sb.Append("EVENT_Lab As EventLab, ");
        sb.Append("EVENT_AccessionDR As AccessionNumber, ");
        sb.Append("EVENT_CategoryDR As EventCategory, ");
        sb.Append("EVENT_LogDesc As EventLogDescription, ");
        sb.Append("EVENT_CategoryDR->EVECT_IsEditable AS IsEditable ");
        sb.Append("FROM ");
        sb.Append("CLF_ClientEventLog ");
        sb.Append("WHERE ");
        sb.Append("EVENT_CLF_ParRef->CLF_CLNUM = '" + clientID + "'");
        sb.Append(" ORDER BY ");
        sb.Append("EVENT_DateLogged DESC, EVENT_TimeLogged DESC, EVENT_RowID DESC");


        #endregion Preparing SQL Statement

        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getEventsByClientMnemonic(String clientMnemonic)
    {
        #region Reference Query

        ////SELECT 
        ////EVENT_DateLogged  As EventLogDate, 
        ////EVENT_TimeLogged  As EventLogTime, 
        ////EVENT_Agent As EventAgent, 
        ////EVENT_Lab As EventLab, 
        ////EVENT_AccessionDR As AccessionNumber, 
        ////EVENT_CategoryDR As EventCategory, 
        ////EVENT_LogDesc As EventLogDescription 
        ////FROM CLF_ClientEventLog 
        ////WHERE EVENT_CLF_ParRef = 'CATHY'

        #endregion Reference Query

        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("EVENT_RowID AS EventRowId, ");
        sb.Append("EVENT_DateLogged As EventLogDate, ");
        sb.Append("EVENT_TimeLogged As EventLogTime, ");
        sb.Append("$$CO17^XT58(EVENT_Agent) As EventAgent, ");
        sb.Append("EVENT_Lab As EventLab, ");
        sb.Append("EVENT_AccessionDR As AccessionNumber, ");
        sb.Append("EVENT_CategoryDR As EventCategory, ");
        sb.Append("EVENT_LogDesc As EventLogDescription, ");
        sb.Append("EVENT_CategoryDR->EVECT_IsEditable AS IsEditable ");
        sb.Append("FROM ");
        sb.Append("CLF_ClientEventLog ");
        sb.Append("WHERE ");
        sb.Append("EVENT_CLF_ParRef = '" + clientMnemonic + "'");
        sb.Append(" ORDER BY ");
        sb.Append("EVENT_DateLogged DESC, EVENT_TimeLogged DESC, EVENT_RowID DESC");

        #endregion Preparing SQL Statement

        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static String insertReportOption(String mnemonic, String autoDialGroup, String fromDate, String toDate, String userId, String userLab)
    {
        Dictionary<String, String> reportOptionData = new Dictionary<String, String>();
        reportOptionData.Add("CLIENT", mnemonic);
        reportOptionData.Add("AUTODIAL", autoDialGroup);
        reportOptionData.Add("ALLREPORTS", fromDate);
        reportOptionData.Add("DATE", toDate);
        reportOptionData.Add("UserId", userId);
        reportOptionData.Add("UserLab", userLab);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP_SaveReportOption(?,?,?,?,?,?)", reportOptionData).Value.ToString();
    }

    #region Unused Code
    /*public static DataTable getSpecialPricing(String clientID, String unitCode, String dateOfPrice)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        //sb.Append("{fn CONVERT(SP_Price,SQL_VARCHAR)} AS SpecialPrice,");
        sb.Append("SP_Price+'' AS SpecialPrice,");
        sb.Append("SP_OptionPrice As SpecialOptionPrice,");
        sb.Append("SP_UnitCodeDR->UC_DisplayReportingTitle As UnitCodeTitle,");
        sb.Append("SP_RowID As SpecialPriceRowID ");
        sb.Append("FROM ");
        sb.Append("FIN_SpecialPricing ");
        sb.Append("WHERE ");
        sb.Append("SP_ACT_ParRef='" + clientID + "'");
        sb.Append(" AND ");
        //sb.Append("SP_UnitCodeDR='" + unitCode + "'");
        sb.Append("SP_UnitCodeDR->UC_DisplayReportingTitle='" + unitCode + "'");
        sb.Append(" AND ");
        sb.Append("SP_DateSubscript>99999-CAST(TO_DATE('" + dateOfPrice + "','MM/DD/YYYY') AS FLOAT)");
        //sb.Append("TO_DATE(99999 - SP_DateSubscript,'MM/DD/YYYY') > '" + dateOfPrice + "'");
        //sb.Append("SP_DateSubscript>'" + dateOfPrice + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    
    public static DataTable getSpecialPricingOptionsList(String specialPriceID)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("SPOPT_OptionUnitCodeDR As UnitCode,");
        sb.Append("SPOPT_OptionUnitCodeDR->UC_DisplayReportingTitle As UnitCodeTitle ");
        sb.Append("FROM ");
        sb.Append("FIN_SpecialPricingOptionList ");
        sb.Append("WHERE ");
        sb.Append("SPOPT_SP_ParRef='" + specialPriceID + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    

    public static DataTable getNormalFeeSchedule(String clientID, String dateOfPrice)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("NP_FeeSchedule ");
        sb.Append("FROM ");
        sb.Append("FIN_NormalPricing ");
        sb.Append("WHERE ");
        sb.Append("NP_ACT_ParRef ='" + clientID + "'");
        sb.Append(" AND ");
        sb.Append("NP_DateSubscript>99999-CAST(TO_DATE('" + dateOfPrice + "','MM/DD/YYYY') AS FLOAT)");
        //sb.Append("TO_DATE(99999-NP_DateSubscript,'MM/DD/YYYY') > '" + dateOfPrice + "'");
        //sb.Append("NP_DateSubscript > '" + dateOfPrice + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    
    public static DataTable getNormalPrice(String dateOfPrice, String unitCode, String normalFeeScheduleID)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("UCFS_Price,");
        sb.Append("UCFS_OptionPrice,");
        sb.Append("UCFS_UC_ParRef->UC_DisplayReportingTitle As UnitCodeTitle,");
        sb.Append("UCFS_RowID ");
        sb.Append("FROM ");
        sb.Append("DIC_UnitCodeFeeSchedule ");
        sb.Append("WHERE ");
        sb.Append("UCFS_UC_ParRef->UC_DisplayReportingTitle ='" + unitCode + "'");
        //sb.Append("UCFS_UC_ParRef ='" + unitCode + "'");
        sb.Append(" AND ");
        sb.Append("UCFS_DateSubscript>99999-CAST(TO_DATE('" + dateOfPrice + "','MM/DD/YYYY') AS FLOAT)");
        //sb.Append("TO_DATE(99999-UCFS_DateSubscript,'MM/DD/YYYY') > '" + dateOfPrice + "'");
        //sb.Append("UCFS_DateSubscript >'" + dateOfPrice + "'");
        sb.Append(" AND ");
        sb.Append("UCFS_FeeScheduleID ='" + normalFeeScheduleID + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    
    public static DataTable getNormalPriceOptions(String NormalPriceID)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("UCFSO_OptionUnitCodeDR As UnitCode,");
        sb.Append("UCFSO_OptionUnitCodeDR->UC_DisplayReportingTitle As UnitCodeTitle");
        sb.Append(" FROM ");
        sb.Append("DIC_UCFSOptionList ");
        sb.Append("WHERE ");
        sb.Append("UCFSO_UCFS_ParRef ='" + NormalPriceID + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }*/
    #endregion Unused Code
    public static Int32 updateAccountNote(String clientMnemonic, String newNote,String priceInqPasswd,Boolean canChangeClientPasswd)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("UPDATE ");
        sb.Append("CLF_CLIENTFILE ");
        sb.Append("SET ");
        sb.Append("CLF_CustomerServiceNotes = '" + newNote + "' ");
        if (canChangeClientPasswd)
        {
            sb.Append(", CLF_PasswordPriceInquiry = '" + priceInqPasswd + "' ");
        }
        sb.Append("WHERE ");
        sb.Append("CLF_CLMNE = '" + clientMnemonic + "'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.Insert(sb.ToString());
    }

    public static String validatePriceInqPassword(String clientID,String priceInqPasswd,String User)
    {
        Dictionary<string, string> _validatePriceInqPasswd = new Dictionary<string, string>();
        _validatePriceInqPasswd.Add("CLIENT", clientID);
        _validatePriceInqPasswd.Add("PASSWORD", priceInqPasswd);
        _validatePriceInqPasswd.Add("USER", User);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ValidatePriceInqPasswd(?,?,?)", _validatePriceInqPasswd).Value.ToString();
    }

    public static string validateAutodial(String CLID)
    {
        Dictionary<string, string> _validateAutodial = new Dictionary<string, string>();
        _validateAutodial.Add("CLID", CLID);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ValidateAutodial(?)", _validateAutodial).Value.ToString();
    }

    public static String getClientCountry(String clientID)
    {
        Dictionary<string, string> _getClientCountry = new Dictionary<string, string>();
        _getClientCountry.Add("ClientID", clientID);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP_getClientCountry(?)", _getClientCountry).Value.ToString();
    }

    public static DataTable getTestsInquiry(String clientID, String unitCode, String date, out String UnitCodeTitle, out String SpecialPrice, out String SpecialOptionPrice, String clientMnemonic, String userId, String userLab, String testName, String logEvent)
    {
        UnitCodeTitle = String.Empty;
        SpecialPrice = String.Empty;
        SpecialOptionPrice = String.Empty;
        Dictionary<String, String> _getTestsInquiry = new Dictionary<String, String>();
        _getTestsInquiry.Add("ClientID", clientID);
        _getTestsInquiry.Add("UnitCode", unitCode);
        _getTestsInquiry.Add("Date", date);
        _getTestsInquiry.Add("ClientMnemonic", clientMnemonic);
        _getTestsInquiry.Add("UserId", userId);
        _getTestsInquiry.Add("UserLab", userLab);
        _getTestsInquiry.Add("TestName", testName);
        _getTestsInquiry.Add("LogEvent", logEvent);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        String fullOutput = cache.StoredProcedure("?=call SP2_PRICEINQ(?,?,?,?,?,?,?,?)", _getTestsInquiry, 999999).Value.ToString();
        String[] values = fullOutput.Split('^');
        if (values.Length == 4)
        {
            UnitCodeTitle = values[0];
            SpecialPrice = values[1];
            SpecialOptionPrice = values[2];
            DataTable returnDataTable = AtlasIndia.AntechCSM.Data.DL_functions.StringToDataTable(values[3], ';', '~');
            if (returnDataTable.Columns.Count==3)
            {
                returnDataTable.Columns[1].ColumnName = "UnitCode";
                returnDataTable.Columns[2].ColumnName = "UnitCodeTitle";
                returnDataTable.AcceptChanges();
                return returnDataTable;
            }            
        }
        return null;
    }
}
