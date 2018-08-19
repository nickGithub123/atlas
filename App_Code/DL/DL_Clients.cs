using System;
using System.Data;
using System.Configuration;
public class DL_Clients
{
    public DL_Clients()
    {
    }

    public static DataTable getClientsByAccountNumber(string accountNumber)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        //+SSM
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        //-SSM
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("CLF_CLNUM = '" + accountNumber + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByAccountNumber(string accountNumber, string location) // AM 03/17/2008 - Adding search critria location also
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("CLF_CLNUM = '" + accountNumber + "'");
        sb.Append(" AND ");
        sb.Append("CLF_LabLocationDR->LABLO_RowID = '" + location + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByAccountName(string accountName)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");
        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("%SQLUPPER CLF_CLNAM %STARTSWITH %SQLUPPER '" + accountName + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByAccountName(String accountName, Int32 startIndex, Int32 noOfRecords)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");

        sb.Append("WHERE ");
        sb.Append("%SQLUPPER CLF_CLNAM %STARTSWITH %SQLUPPER '" + accountName + "'");
        
        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString(), startIndex, noOfRecords);
    }
    
    public static DataTable getClientsByAccountName(string accountName, string location) // AM 03/17/2008 - Adding search critria location also
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("%SQLUPPER CLF_CLNAM %STARTSWITH %SQLUPPER '" + accountName + "'");
        sb.Append(" AND ");
        sb.Append("CLF_LabLocationDR->LABLO_RowID = '" + location + "'");
        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getClientsByAccountMnemonic(string accountMnemonic)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("CLF_CLMNE = '" + accountMnemonic + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByAccountMnemonic(string accountMnemonic, string location) // AM 03/17/2008 - Adding search critria location also
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");

        sb.Append("WHERE ");
        sb.Append("CLF_CLMNE = '" + accountMnemonic + "'");
        sb.Append(" AND ");
        sb.Append("CLF_LabLocationDR->LABLO_RowID = '" + location + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByPhoneNumber(string accountPhoneNumber)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("CLF_CLPHN [ '" + accountPhoneNumber + "'"); //AM IT#59633 AntechCSM 1.0.82.0 - Added partial search facility on phone number.
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByPhoneNumber(String accountPhoneNumber, Int32 startIndex, Int32 noOfRecords)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("CLF_CLPHN [ '" + accountPhoneNumber + "'");
        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString(),startIndex,noOfRecords);
    }
    
    public static DataTable getClientsByPhoneNumber(string accountPhoneNumber, string location) // AM 03/17/2008 - Adding search critria location also
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("CLF_CLPHN [ '" + accountPhoneNumber + "'");
        sb.Append(" AND ");
        sb.Append("CLF_LabLocationDR->LABLO_RowID = '" + location + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByZoasisNumber(string accountZoasisNumber)
    {
        /// ASSUMPTION: There is a 1x1 relationship between zoasis and CLF_ClientFile, so there can be only one record, so No Sorting needed.

        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("zoasis.Zoasis_num = '" + accountZoasisNumber + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByZoasisNumber(String accountZoasisNumber, Int32 startIndex, Int32 noOfRecords)
    {
        #region Preparing SQL Statement
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("zoasis.Zoasis_num = '" + accountZoasisNumber + "'");
        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString(),startIndex,noOfRecords);
    }
    
    public static DataTable getClientsByZoasisNumber(string accountZoasisNumber, string location) // AM 03/17/2008 - Adding search critria location also
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("zoasis.Zoasis_num = '" + accountZoasisNumber + "'");
        sb.Append(" AND ");
        sb.Append("CLF_LabLocationDR->LABLO_RowID = '" + location + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsByRouteStop(string accountRouteStop)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("CLF_CLRTS = '" + accountRouteStop + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getClientsBySalesTerritory(string accountSalesTerritory)
    {
        #region Preparing SQL Statement

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("CLF_CLNUM,");
        sb.Append("CLF_CLNAM,");
        sb.Append("CLF_CLMNE,");
        sb.Append("CLF_CLPHN,");
        sb.Append("CLF_CLAD1,");
        sb.Append("CLF_CLAD2,");
        sb.Append("CLF_CLAD3,");
        sb.Append("CLF_CLAD4,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS CLF_CLTER,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALES_REP,");
        sb.Append("CLF_CLADG as AutoDial_Group,");
        sb.Append("zoasis_num AS ZOASIS_NUMBER,");
        sb.Append("CLF_CLRTS,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientFile ");
        sb.Append("left outer join ");
        sb.Append("zoasis ");
        sb.Append("on CLF_ClientFile.CLF_CLNUM  = zoasis.CLN_RowID ");
        sb.Append("WHERE ");
        sb.Append("UPPER(CLF_SalesTerritoryDR->ST_TerritoryCode) = '" + accountSalesTerritory + "'");
        String selectStatement = sb.ToString();

        #endregion Preparing SQL Statement

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
}
