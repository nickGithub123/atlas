using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for DL_Transit
/// </summary>
public class DL_Transit
{
	public DL_Transit()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable getCITDetails(string strRowID)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
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
        sb.Append("ADDON_User As UserValue, ");
        sb.Append("ADDON_ClientDR->CLF_CLNUM As Account ");
        sb.Append("FROM ORD_AddOn");
        sb.Append(" WHERE ADDON_ConfirmationNumber = '" + strRowID + "'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getTransitRecords(string strAccountNo, string strConfNo,string strFromDate, string strToDate)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ADDON_ConfirmationNumber As RowId,ADDON_ClientDR->CLF_CLNUM As Account,");
        sb.Append("ADDON_LabLocationDR As LabId,");
        sb.Append("ADDON_LabLocationDR->LABLO_LabName As LabName,");
        sb.Append("ADDON_Email,");
        sb.Append("ADDON_Date,");
        sb.Append("ADDON_Time,");
        sb.Append("ADDON_User,");
        sb.Append("ADDON_RequestType,");
        sb.Append("ADDON_CallerName,");
        sb.Append("CLF_CLNAM AS AccountName,");
        sb.Append("CLF_CLMNE AS AccountMnemonic,");
        sb.Append("CLF_CLPHN As ClientPhone,");
        sb.Append("CLF_CLAD1 AS ClientAddress1,");
        sb.Append("CLF_CLAD2 AS ClientAddress2,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sb.Append("CLF_IsHot As ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sb.Append("CLF_CLADG AS AutodialGroup,");
        sb.Append("CLF_IsNew AS ClientIsNew,");
        sb.Append("CLF_CLRTS AS RouteStop,");
        sb.Append("zoasis_num AS Zoasis,");
        sb.Append("$$GETREVHIST^XT1(ADDON_ClientDR) AS ClientRevenue");
        sb.Append(" FROM");
        sb.Append(" ORD_AddOn");
        sb.Append(" LEFT OUTER JOIN");
        sb.Append(" CLF_ClientFile Client on ADDON_ClientDR = Client.CLF_CLMNE ");
        sb.Append(" LEFT OUTER JOIN ");
        sb.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sb.Append("WHERE 1=1");
        sb.Append(" AND ADDON_RequestType='X'");
        if (strAccountNo.Length > 0)
        {
            sb.Append(String.Concat(" AND ADDON_ClientDR->CLF_CLNUM ='", strAccountNo, "'"));
        }
        if (strFromDate.Length > 0)
        {
            sb.Append(String.Concat(" AND ADDON_Date>= TO_DATE('", strFromDate, "','MM/dd/yyyy')"));
        }
        if (strToDate.Length > 0)
        {
            sb.Append(String.Concat(" AND ADDON_Date<= TO_DATE('", strToDate, "','MM/dd/yyyy')"));
        }
        if (strConfNo.Length > 0)
        {
            sb.Append(String.Concat(" AND ADDON_ConfirmationNumber='", strConfNo, "'"));
        }

        sb.Append(String.Concat(" ORDER BY ADDON_Date DESC, ADDON_Time DESC"));

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }
}
