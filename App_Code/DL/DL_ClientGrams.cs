using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for DL_ClientGrams
/// </summary>
public class DL_ClientGrams
{
	public DL_ClientGrams()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public DataTable getCientGramDetails(string clientgramID, string accountNumber, string labLocation, string salesTerritory, string dateFrom, string dateTo, string accessionNumber, string user)
    {
        DataTable returnDataTable = new DataTable();

        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT RCG_RowID AS CLIENTGRAMID,");
        sbSQL.Append(" CLF_CLNUM||', '||CLF_CLNAM AS SENTTO,");
        sbSQL.Append(" RCG_Accession AS ACCESSION,");
        sbSQL.Append(" $$CO17^XT58(USER_UserID) AS AGENTID,");
        sbSQL.Append(" RCG_AccountMnemonic AS CLIENTMNEMONIC,");
        sbSQL.Append(" RCG_ClientDR->CLF_LabLocationDR->LABLO_LabName AS LABLOCATION,");
        sbSQL.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS TERRITORY,");
        sbSQL.Append(" RCG_DateFiled AS COLLECTIONDATE,");
        sbSQL.Append(" RCG_TimeFiled AS COLLECTIONTIME,");
        sbSQL.Append(" CLF_CLNUM AS CLIENTNUM,");
        sbSQL.Append(" CLF_CLANAM AS CLIENTNAME,");
        sbSQL.Append(" CLF_CLPHN AS PHONE,");
        sbSQL.Append(" CLF_CLAD1 AS ADDRESS1,");
        sbSQL.Append(" CLF_CLAD2 AS ADDRESS2,");
        sbSQL.Append(" CLF_SalesTerritoryDR->ST_SalesRepName AS SALESREP,");
        sbSQL.Append(" CLF_CLADG AS AUTODIALGR,");
        sbSQL.Append(" Zoasis_num AS ZOASIS,");
        sbSQL.Append(" CLF_CLRTS AS RouteStop,");
        sbSQL.Append(" $$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue,");
        sbSQL.Append(" CLF_IsHot AS ClientIsHot,");
        sbSQL.Append(" CLF_IsNew AS ClientIsNew,");
        sbSQL.Append(" CLF_IsAlliedClient As ClientIsAllied");
        sbSQL.Append(" FROM REP_ReportingClientGram");
        sbSQL.Append(" LEFT OUTER JOIN CLF_ClientFile ON RCG_AccountMnemonic = CLF_ClientFile.CLF_CLMNE");
        sbSQL.Append(" LEFT OUTER JOIN zoasis ON CLF_CLNUM = zoasis.CLN_RowID");
        sbSQL.Append(" LEFT OUTER JOIN DIC_User ON RCG_SSID = DIC_User.USER_UserID");
        sbSQL.Append(" WHERE 1=1");
        if (clientgramID != "")
        {
            sbSQL.Append(" AND RCG_RowID ='" + clientgramID + "'");
        }
        if (accountNumber != "")
        {
            sbSQL.Append(" AND CLF_CLNUM ='" + accountNumber + "'");
        }
        if (labLocation != "")
        {
            sbSQL.Append(" AND RCG_ClientDR->CLF_LabLocationDR->LABLO_RowID ='" + labLocation + "'");
        }
        if (salesTerritory != "")
        {
            sbSQL.Append(" AND RCG_ClientDR->CLF_SalesTerritoryDR ='" + salesTerritory + "'");
        }
        if (accessionNumber != "")
        {
            sbSQL.Append(" AND RCG_Accession ='" + accessionNumber + "'");
        }
        if (dateFrom != "")
        {
            sbSQL.Append(" AND RCG_DateFiled >= TO_DATE('" + dateFrom + "','MM/DD/YYYY') AND RCG_DateFiled<=TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        if (user != "")
        {
            sbSQL.Append(" AND DIC_User.USER_UserID ='" + user + "'");
        }
        sbSQL.Append(" ORDER BY RCG_DateFiled DESC,RCG_RowID DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }
}
