using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for DL_Supply
/// </summary>
public class DL_Supply
{
	public DL_Supply()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    internal static DataTable getSupplySearchResult(string ordNo, string clientID, string expressShipping, string dateFrom, string dateTo, string ordType
        , string supplyPrefix, string status)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("ACC_Accession As OrderNo, ");
        sbSQL.Append("ACC_ClientDR->CLF_CLNUM As AccountNumber, ");
        sbSQL.Append("%EXTERNAL(ACC_MiniLogDate) As OrdDate, ");
        sbSQL.Append("ACC_PetFirstName As Caller, ");
        //sbSQL.Append("%EXTERNAL(ACC_PetFirstName) As Caller, ");
        sbSQL.Append("ACC_PatientName As OrdType, ");
        sbSQL.Append("ACC_RequesitionNumber As ExpressShipping, ");
        sbSQL.Append("ACC_TestsOrderedDisplayString As Requested, ");
        sbSQL.Append("ACC_SupplyTrackNumber As TrkInfo, ");
        sbSQL.Append("ACC_ReportStatus As Status, ");
        sbSQL.Append("ACC_ClientDR->CLF_CLNAM AS AccountName,");
        sbSQL.Append("ACC_ClientDR->CLF_CLMNE AS AccountMnemonic,");
        sbSQL.Append("ACC_ClientDR->CLF_CLPHN As ClientPhone,");
        sbSQL.Append("ACC_ClientDR->CLF_CLAD1 AS ClientAddress1,");
        sbSQL.Append("ACC_ClientDR->CLF_CLAD2 AS ClientAddress2,");
        sbSQL.Append("ACC_ClientDR->CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sbSQL.Append("ACC_ClientDR->CLF_IsHot As ClientIsHot,");
        sbSQL.Append("ACC_ClientDR->CLF_IsAlliedClient As ClientIsAllied,");
        sbSQL.Append("ACC_ClientDR->CLF_IsNew As ClientIsNew,");
        sbSQL.Append("ACC_ClientDR->CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sbSQL.Append("ACC_ClientDR->CLF_CLADG AS AutodialGroup,");
        sbSQL.Append("ACC_ClientDR->CLF_CLRTS AS RouteStop,");
        sbSQL.Append("zoasis_num AS Zoasis,");
        sbSQL.Append("$$GETREVHIST^XT1(ACC_ClientDR) AS ClientRevenue");
        sbSQL.Append(" FROM ");
        sbSQL.Append("ORD_Accession ");
        sbSQL.Append("LEFT OUTER JOIN ");
        sbSQL.Append("CLF_ClientFile Client on ACC_ClientDR = Client.CLF_CLMNE ");
        sbSQL.Append("LEFT OUTER JOIN ");
        sbSQL.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sbSQL.Append("WHERE 1=1 ");
        sbSQL.Append("AND ACC_Accession %STARTSWITH '" + supplyPrefix + "' ");

        if (dateFrom.Length > 0)
        {
            sbSQL.Append(" AND ACC_MiniLogDate>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sbSQL.Append(" AND ACC_MiniLogDate<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (ordNo.Length > 0)
        {
            sbSQL.Append(" AND ACC_Accession='" + ordNo + "'");
        }
        if (clientID.Length > 0)
        {
            sbSQL.Append(" AND ACC_ClientDR->CLF_CLNUM='" + clientID + "'");
        }
        if (expressShipping.Length > 0)
        {
            sbSQL.Append(" AND ACC_RequesitionNumber='" + expressShipping + "'");
        }
        if (ordType.Length > 0)
        {
            sbSQL.Append(" AND ACC_PatientName='" + ordType + "'");
        }
        if (status.Length > 0)
        {
            sbSQL.Append(" AND ACC_ReportStatus='" + status + "'");
        }

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    internal static DataTable getSupplyOrderDetails(string ordID)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("ACC_Accession As OrderNo, ");
        sbSQL.Append("ACC_ClientDR->CLF_CLNUM As AccountNumber, ");
        sbSQL.Append("%EXTERNAL(ACC_MiniLogDate) As OrdDate, ");
        sbSQL.Append("ACC_PetFirstName As Caller, ");
        sbSQL.Append("ACC_PatientName As OrdType, ");
        sbSQL.Append("ACC_RequesitionNumber As ExpressShipping, ");
        sbSQL.Append("ACC_SupplyTrackNumber As TrkInfo, ");
        sbSQL.Append("ACC_ReportingComments As Notes ");
        sbSQL.Append(" FROM ");
        sbSQL.Append("ORD_Accession ");
        sbSQL.Append("WHERE 1=1 ");
        sbSQL.Append(" AND ACC_Accession='" + ordID + "'");
        

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    internal static DataTable getOrderItemDetails(string ordID)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("ATO_UnitCodeDR As UCRow, ");
        sbSQL.Append("ATO_UnitCodeDR->UC_OrderingMnemonics As Code, ");
        sbSQL.Append("ATO_UnitCodeDR->UC_DisplayReportingTitle As CodeDesc, ");
        sbSQL.Append("ATO_UnitCodeDR->UC_StandardQuantity As StdQuantity, ");
        sbSQL.Append("$$GETORDEREDQUANTITY^XT142(ATO_ACC_ParRef,ATO_UnitCodeDR) As OrdQuantity, ");
        sbSQL.Append("$$GETSHIPPEDQUANTITY^XT142(ATO_ACC_ParRef,ATO_UnitCodeDR) As DelQuantity ");
        sbSQL.Append("FROM ");
        sbSQL.Append("ORD_AccessionTestOrdered ");
        sbSQL.Append("WHERE 1=1 ");
        sbSQL.Append(" AND ATO_ACC_ParRef='" + ordID + "'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    internal static DataTable getPrevoiusSupplyOrdersByAccount(string accountNo, string supplyPrefix)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("ACC_Accession As OrderNo, ");
        sbSQL.Append("ACC_ClientDR->CLF_CLNUM As AccountNumber, ");
        sbSQL.Append("%EXTERNAL(ACC_MiniLogDate) As OrderDate, ");
        sbSQL.Append("ACC_PetFirstName As Caller, ");
        sbSQL.Append("ACC_PatientName As OrderType, ");
        sbSQL.Append("ACC_RequesitionNumber As Express, ");
        sbSQL.Append("ACC_TestsOrderedDisplayString As Requested, ");
        sbSQL.Append("ACC_SupplyTrackNumber As TrkInfo ");
        sbSQL.Append(" FROM ");
        sbSQL.Append("ORD_Accession ");
        sbSQL.Append("WHERE 1=1 ");
        sbSQL.Append("AND ACC_Accession %STARTSWITH '" + supplyPrefix + "' ");
        sbSQL.Append(" AND ACC_ClientDR->CLF_CLNUM='" + accountNo + "'");
        sbSQL.Append(" AND ACC_MiniLogDate>= TO_DATE('" + DateTime.Now.AddYears(-1).ToShortDateString() + "','MM/DD/YYYY')");
        sbSQL.Append(" AND ACC_MiniLogDate<= TO_DATE('" + DateTime.Now.ToShortDateString() + "','MM/DD/YYYY')");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    internal static string saveSupplyOrder(string account, string callerName, string expressShipping, string orderDate, string orderType, string notes, string itemsString,string user,string mailSystem, string dir)
    {
        Dictionary<String, String> _supplyOrder = new Dictionary<String, String>();
        _supplyOrder.Add("ACCOUNT", account);
        _supplyOrder.Add("CALLER", callerName);
        _supplyOrder.Add("EXPRESS", expressShipping);
        _supplyOrder.Add("ORDERDATE", orderDate);
        _supplyOrder.Add("ORDERTYPE", orderType);
        _supplyOrder.Add("NOTES", notes);
        _supplyOrder.Add("ITEMSTR", itemsString);
        _supplyOrder.Add("USER", user);
        _supplyOrder.Add("MAILSYSTEM", mailSystem);
        _supplyOrder.Add("DIR", dir);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_SaveSupplyOrder(?,?,?,?,?,?,?,?,?,?)", _supplyOrder).Value.ToString();
    }

    internal static DataTable getUCCategory()
    {
        string strSQL = "SELECT UCC_CategoryCode,UCC_CategoryName FROM DIC_UCCategory ORDER BY UCC_CategoryName";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(strSQL);
    }

    internal static DataTable getUnitCodeByCategory(string category)
    {
        string strSQL = "SELECT UC_RowID,UC_ReportingTitle FROM DIC_UNITCODE WHERE UC_CategoryDR='" + category + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(strSQL);
    }

    internal static DataTable getUnitCodeDetails(string unitCode)
    {
        string strSQL = "SELECT UC_RowID,UC_OrderingMnemonics,UC_ReportingTitle,UC_StandardQuantity,UC_OrderAs,UC_OrderingNotes,UC_CategoryDR->UCC_CategoryName As CAT FROM DIC_UNITCODE WHERE UC_RowID='" + unitCode + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(strSQL);
    }
}
