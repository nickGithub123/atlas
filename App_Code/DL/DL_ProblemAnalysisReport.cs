using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Text;

/// <summary>
/// Summary description for DL_ProblemAnalysisReport
/// </summary>
public class DL_ProblemAnalysisReport
{
	public DL_ProblemAnalysisReport()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static DataTable getProblemSuperCatReport()
    {
        DataTable returnDataTable = new DataTable();
        string strQuery = "Select DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef->PTYPG_GroupName As GroupName,PTYP_Description,PTYP_ProblemType From DIC_ProblemType INNER JOIN DIC_PTYPG_ProblemType ON DIC_ProblemType.PTYP_ProblemType=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR ORDER BY GroupName ASC";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(strQuery);
        return returnDataTable;
    }

    public static DataTable getProblemAnalysisByLabLocationSummary(string strDateFrom, string strDateTo, string strLabLocation, string strClient, string strProblemGroup, string strProblemType, string strSalesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef As Type,PBT_LabLocation As Location ");
        sb.Append(" From");
        sb.Append(" CSV_ProbTracking LEFT JOIN DIC_PTYPG_ProblemType ON CSV_ProbTracking.PBT_ProbTP=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR");
        sb.Append(" WHERE 1=1");
        if (strDateFrom.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate>=TO_DATE('");
            sb.Append(strDateFrom);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strDateTo.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate<=TO_DATE('");
            sb.Append(strDateTo);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strLabLocation.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_LabLocation IN (", strLabLocation, ")"));
        }
        if (strClient.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_CLNUM IN (", strClient, ")"));
        }
        if (strProblemGroup.Length > 0)
        {
            sb.Append(String.Concat(" AND DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef IN (", strProblemGroup, ")"));
        }
        if (strProblemType.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_ProbTP IN (", strProblemType, ")"));
        }
        if (strSalesTerritory.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_SalesTerritoryDR IN (", strSalesTerritory, ")"));
        }

        sb.Append(" ORDER BY PBT_LabLocation ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getProblemAnalysisByTerritorySummary(string strDateFrom, string strDateTo, string strLabLocation, string strClient, string strProblemGroup, string strProblemType, string strSalesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select PBT_CLDesRef->CLF_SalesTerritoryDR As Territory,PBT_CLDesRef->CLF_SalesTerritoryDR->ST_SalesRepName As SalesRepName,DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef As Type, ");
        sb.Append("$$GETTOTALPROBCOUNT^XBIUTIL('" + strDateFrom + "','" + strDateTo + "') As TotalProbsAll");
        sb.Append(" From");
        sb.Append(" CSV_ProbTracking LEFT JOIN DIC_PTYPG_ProblemType ON CSV_ProbTracking.PBT_ProbTP=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR");
        sb.Append(" WHERE 1=1");
        if (strDateFrom.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate>=TO_DATE('");
            sb.Append(strDateFrom);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strDateTo.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate<=TO_DATE('");
            sb.Append(strDateTo);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strLabLocation.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_LabLocation IN (", strLabLocation, ")"));
        }
        if (strClient.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_CLNUM IN (", strClient, ")"));
        }
        if (strProblemGroup.Length > 0)
        {
            sb.Append(String.Concat(" AND DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef IN (", strProblemGroup, ")"));
        }
        if (strProblemType.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_ProbTP IN (", strProblemType, ")"));
        }
        if (strSalesTerritory.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_SalesTerritoryDR IN (", strSalesTerritory, ")"));
        }

        sb.Append(" ORDER BY Territory ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getTotalProbAccCountForLab(string strDateFrom, string strDateTo, string strLabLocation)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select TOP 1 $$GETTOTALPROBCOUNT^XBIUTIL('" + strDateFrom + "','" + strDateTo + "') As TotalProbsAll,$$GETTOTALACCCOUNT^XBIUTIL('" + strDateFrom + "','" + strDateTo + "') As TotalAccAll ");
        //sb.Append("$$GETTOTALACCCOUNTBYLAB^XBIUTIL('" + strDateFrom + "','" + strDateTo + "','" + strLabLocation + "') As TotalAccLab");
        sb.Append(" From CSV_ProbTracking");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getTotalAccCountForLab(string strDateFrom, string strDateTo, string strLabLocation)
    {
        Dictionary<String, String> _getCount = new Dictionary<String, String>();
        _getCount.Add("FROMDATE", strDateFrom);
        _getCount.Add("TODATE", strDateTo);
        _getCount.Add("LABS", strLabLocation);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string strRetVal = cache.StoredProcedure("?=call SP2_GetTotalAccCountByLabs(?,?,?)", _getCount).Value.ToString();

        DataTable returnDataTable = AtlasIndia.AntechCSM.Data.DL_functions.StringToDataTable(strRetVal, ';', ',');
        if (returnDataTable.Columns.Count == 3)
        {
            returnDataTable.Columns[1].ColumnName ="Lab";
            returnDataTable.Columns[2].ColumnName ="Count";
        }
        return returnDataTable;
    }


    public static DataTable getTotalProbAccCountForComp(string strDateFrom, string strDateTo, string[] strLabLocation)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        for (int count = 0; count < strLabLocation.Length; count++)
        {
            if(count==0)
            {
                sb.Append("Select TOP 1 ");
            }
            if (sb.ToString().Length > 0 && count != 0)
            {
                sb.Append(" , ");
            }
            sb.Append(" $$GETTOTALPROBCOUNTBYLAB^XBIUTIL('" + strDateFrom + "','" + strDateTo + "','" + strLabLocation[count] + "') As TotalProbCount_Lab" + (count + 1));
            sb.Append(" , $$GETTOTALACCCOUNTBYLAB^XBIUTIL('" + strDateFrom + "','" + strDateTo + "','" + strLabLocation[count] + "') As TotalAccCount_Lab" + (count + 1));

        }
        sb.Append(" From CSV_ProbTracking");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getTotalProbCount(string strDateFrom, string strDateTo)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select TOP 1 $$GETTOTALPROBCOUNT^XBIUTIL('" + strDateFrom + "','" + strDateTo + "') As TotalProbsAll");
        sb.Append(" From CSV_ProbTracking");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getProbAnalysisByLab(string strDateFrom, string strDateTo, string strLabLocation, string strClient, string strProblemGroup, string strProblemType, string strSalesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select PBT_LabLocation As Location,COUNT(*) As CountValue ");
        sb.Append(" From CSV_ProbTracking");
        sb.Append(" LEFT JOIN DIC_PTYPG_ProblemType ON CSV_ProbTracking.PBT_ProbTP=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR");
        sb.Append(" WHERE 1=1");
        if (strDateFrom.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate>=TO_DATE('");
            sb.Append(strDateFrom);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strDateTo.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate<=TO_DATE('");
            sb.Append(strDateTo);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strLabLocation.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_LabLocation IN (", strLabLocation, ")"));
        }
        if (strClient.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_CLNUM IN (", strClient, ")"));
        }
        if (strProblemGroup.Length > 0)
        {
            sb.Append(String.Concat(" AND DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef IN (", strProblemGroup, ")"));
        }
        if (strProblemType.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_ProbTP IN (", strProblemType, ")"));
        }
        if (strSalesTerritory.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_SalesTerritoryDR IN (", strSalesTerritory, ")"));
        }

        sb.Append(" GROUP BY PBT_LabLocation");
        sb.Append(" ORDER BY PBT_LabLocation ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getProbAnalysisByLabChildDetails(string strDateFrom, string strDateTo, string strLabLocation, string strClient, string strProblemGroup, string strProblemType, string strSalesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef->PTYPG_GroupName As Type,PBT_ProbTP->PTYP_Description As DESCVALUE,PBT_LabLocation As Lab,COUNT(*) As CountValue ");
        sb.Append(" From CSV_ProbTracking LEFT JOIN DIC_PTYPG_ProblemType ON CSV_ProbTracking.PBT_ProbTP=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR");
        sb.Append(" WHERE 1=1");
        if (strDateFrom.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate>=TO_DATE('");
            sb.Append(strDateFrom);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strDateTo.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate<=TO_DATE('");
            sb.Append(strDateTo);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strLabLocation.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_LabLocation IN (", strLabLocation, ")"));
        }
        if (strClient.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_CLNUM IN (", strClient, ")"));
        }
        if (strProblemGroup.Length > 0)
        {
            sb.Append(String.Concat(" AND DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef IN (", strProblemGroup, ")"));
        }
        if (strProblemType.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_ProbTP IN (", strProblemType, ")"));
        }
        if (strSalesTerritory.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_SalesTerritoryDR IN (", strSalesTerritory, ")"));
        }

        sb.Append(" GROUP BY PBT_ProbTP->PTYP_ProblemType,PBT_LabLocation");
        sb.Append(" ORDER BY Type ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getProbAnalysisByTerritory(string strDateFrom, string strDateTo, string strLabLocation, string strClient, string strProblemGroup, string strProblemType, string strSalesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select PBT_CLDesRef->CLF_SalesTerritoryDR As Territory,PBT_CLDesRef->CLF_SalesTerritoryDR->ST_SalesRepName As SalesRepName,COUNT(*) As CountValue,$$GETTOTALPROBCOUNT^XBIUTIL('" + strDateFrom + "','" + strDateTo + "') As TotalProbsALL ");
        sb.Append(" From CSV_ProbTracking");
        sb.Append(" LEFT JOIN DIC_PTYPG_ProblemType ON CSV_ProbTracking.PBT_ProbTP=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR");
        sb.Append(" WHERE 1=1");
        if (strDateFrom.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate>=TO_DATE('");
            sb.Append(strDateFrom);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strDateTo.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate<=TO_DATE('");
            sb.Append(strDateTo);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strLabLocation.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_LabLocation IN (", strLabLocation, ")"));
        }
        if (strClient.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_CLNUM IN (", strClient, ")"));
        }
        if (strProblemGroup.Length > 0)
        {
            sb.Append(String.Concat(" AND DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef IN (", strProblemGroup, ")"));
        }
        if (strProblemType.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_ProbTP IN (", strProblemType, ")"));
        }
        if (strSalesTerritory.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_SalesTerritoryDR IN (", strSalesTerritory, ")"));
        }

        sb.Append(" GROUP BY PBT_CLDesRef->CLF_SalesTerritoryDR");
        sb.Append(" ORDER BY PBT_CLDesRef->CLF_SalesTerritoryDR ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getProbAnalysisByTerritoryChildDetails(string strDateFrom, string strDateTo, string strLabLocation, string strClient, string strProblemGroup, string strProblemType, string strSalesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef->PTYPG_GroupName As Type,PBT_ProbTP->PTYP_Description As DESCVALUE, PBT_CLDesRef->CLF_SalesTerritoryDR AS Territory, COUNT(*) As CountValue ");
        sb.Append(" From CSV_ProbTracking LEFT JOIN DIC_PTYPG_ProblemType ON CSV_ProbTracking.PBT_ProbTP=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR");
        sb.Append(" WHERE 1=1");
        if (strDateFrom.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate>=TO_DATE('");
            sb.Append(strDateFrom);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strDateTo.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate<=TO_DATE('");
            sb.Append(strDateTo);
            sb.Append("','MM/dd/yyyy')");
        }
        
        if (strLabLocation.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_LabLocation IN (", strLabLocation, ")"));
        }
        if (strClient.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_CLNUM IN (", strClient, ")"));
        }
        if (strProblemGroup.Length > 0)
        {
            sb.Append(String.Concat(" AND DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef IN (", strProblemGroup, ")"));
        }
        if (strProblemType.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_ProbTP IN (", strProblemType, ")"));
        }
        if (strSalesTerritory.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_SalesTerritoryDR IN (", strSalesTerritory, ")"));
        }

        sb.Append(" GROUP BY PBT_ProbTP->PTYP_ProblemType, PBT_CLDesRef->CLF_SalesTerritoryDR");
        sb.Append(" ORDER BY Type ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getProblemByAcctSummary(string strDateFrom, string strDateTo, string strLabLocation, string strClient, string strProblemGroup, string strProblemType, string strSalesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select PBT_CLDesRef->CLF_CLNUM As Acct, PBT_CLDesRef->CLF_CLNAM As AcctName,PBT_CLDesRef->CLF_SalesTerritoryDR As Territory,PBT_CLDesRef->CLF_SalesTerritoryDR->ST_SalesRepName As SalesRepName,DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef As Type, ");
        sb.Append("$$GETREVHIST^XT1(PBT_CLDesRef) AS ClientRevenue ");
        sb.Append(" From");
        sb.Append(" CSV_ProbTracking LEFT JOIN DIC_PTYPG_ProblemType ON CSV_ProbTracking.PBT_ProbTP=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR");
        sb.Append(" WHERE 1=1");
        if (strDateFrom.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate>=TO_DATE('");
            sb.Append(strDateFrom);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strDateTo.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate<=TO_DATE('");
            sb.Append(strDateTo);
            sb.Append("','MM/dd/yyyy')");
        }
        if (strLabLocation.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_LabLocation IN (", strLabLocation, ")"));
        }
        if (strClient.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_CLNUM IN (", strClient, ")"));
        }
        if (strProblemGroup.Length > 0)
        {
            sb.Append(String.Concat(" AND DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef IN (", strProblemGroup, ")"));
        }
        if (strProblemType.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_ProbTP IN (", strProblemType, ")"));
        }
        if (strSalesTerritory.Length > 0)
        {
            sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_SalesTerritoryDR IN (", strSalesTerritory, ")"));
        }

        sb.Append(" ORDER BY Acct ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }

    public static DataTable getProblemAnalysisLabComparison(string strDateFrom, string strDateTo, string strLabLocation, string strClient, string strProblemGroup, string strProblemType, string strSalesTerritory)
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        string[] strLabs = strLabLocation.Split(new char[] { ';' });
        string strSelect = "";
        string strClause = "";
        string[] arrTable = {"X","Y","Z","W"};

        for(int count=0;count<strLabs.Length ;count++)
        {
            if (count == 0)
            {
                strSelect = "Select " + arrTable[count] + ".PTYP_Description,CAST(" + arrTable[count] + ".Lab" + (count + 1) + "_Count As VARCHAR(10)) " + "Lab" + (count + 1) + "_Count";

            }
            else
            {
                strSelect = strSelect + " , CAST(" + arrTable[count] + ".Lab" + (count + 1) + "_Count As VARCHAR(10))  " + "Lab" + (count + 1) + "_Count ";
                if (strClause.Length > 0)
                {
                    strClause = strClause + " AND ";
                }
                strClause = strClause + arrTable[count - 1] + ".PTYP_ROWID=" + arrTable[count] + ".PTYP_ROWID ";
            }
            
            sb.Append("(SELECT A.PTYP_ROWID, A.PTYP_Description, B.Lab"+ (count+1) +"_Count FROM DIC_ProblemType A LEFT OUTER JOIN");
            sb.Append(" (SELECT PBT_ProbTP, Count(PBT_ProbTP) As Lab" + (count + 1) + "_Count From CSV_ProbTracking LEFT JOIN DIC_PTYPG_ProblemType ON CSV_ProbTracking.PBT_ProbTP=DIC_PTYPG_ProblemType.PTGPT_ProblemTypeDR");
            sb.Append(" WHERE 1=1");
            if (strDateFrom.Length > 0)
            {
                sb.Append(" AND PBT_LoggedDate>=TO_DATE('");
                sb.Append(strDateFrom);
                sb.Append("','MM/dd/yyyy')");
            }
            if (strDateTo.Length > 0)
            {
                sb.Append(" AND PBT_LoggedDate<=TO_DATE('");
                sb.Append(strDateTo);
                sb.Append("','MM/dd/yyyy')");
            }

            if (strLabLocation.Length > 0)
            {
                sb.Append(String.Concat(" AND PBT_LabLocation='", strLabs[count], "'"));
            }
            if (strClient.Length > 0)
            {
                sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_CLNUM IN (", strClient, ")"));
            }
            if (strProblemGroup.Length > 0)
            {
                sb.Append(String.Concat(" AND DIC_PTYPG_ProblemType.PTGPT_PTYPG_ParRef IN (", strProblemGroup, ")"));
            }
            if (strProblemType.Length > 0)
            {
                sb.Append(String.Concat(" AND PBT_ProbTP IN (", strProblemType, ")"));
            }
            if (strSalesTerritory.Length > 0)
            {
                sb.Append(String.Concat(" AND PBT_CLDesRef->CLF_SalesTerritoryDR IN (", strSalesTerritory, ")"));
            }
            sb.Append(" GROUP BY PBT_ProbTP) B");
            sb.Append(" ON A.PTYP_ProblemType=B.PBT_ProbTP) " + arrTable[count]+ ",");
        }
        string strQuery = strSelect + " From " + sb.ToString().TrimEnd(new char[] { ',' }) + " Where " + strClause + " ORDER BY PTYP_Description ASC";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(strQuery);
        return returnDataTable;
    }

    public static DataTable getClientAtRisk(string strDateFrom, string strDateTo, string groupString, out string sessionID)
    {
        Dictionary<String, String> _getBICAR = new Dictionary<String, String>();
        _getBICAR.Add("FROMDATE",strDateFrom);
        _getBICAR.Add("TODATE", strDateTo);
        _getBICAR.Add("GROUPSTRING", groupString);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        sessionID = cache.StoredProcedure("?=call SP2_ClientAtRisk(?,?,?)", _getBICAR).Value.ToString();
        
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        if (sessionID.Length > 0)
        {
            sb.Append("Select BICAR_RowID, BICAR_ClientDR, BICAR_ClientDR->CLF_CLNUM As AccountNumber, ");
            sb.Append("BICAR_ClientDR->CLF_CLNAM As AccountName, ");
            sb.Append("BICAR_ClientDR->CLF_CLPHN As AccountPhone, ");
            sb.Append("BICAR_ClientDR->CLF_CLSRT As Territory, ");
            sb.Append("BICAR_ClientDR->CLF_CLSRT->ST_SalesRepName As SalesRepName, ");
            sb.Append("$$GETREVHIST^XT1(BICAR_ClientDR) AS ClientRevenue");
            sb.Append(" From");
            sb.Append(" BI_TMP_ClientsAtRisk");
            sb.Append(" WHERE 1=1");

            sb.Append(String.Concat(" AND BICAR_SessionID=", sessionID, ""));
            sb.Append(" AND BICAR_AtRisk='Y'");
            
            cache = new CACHEDAL.ConnectionClass();
            returnDataTable = cache.FillCacheDataTable(sb.ToString());
        }
        return returnDataTable;
    }

    public static DataTable getClientAtRiskDetails()
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select BICD_ProblemTypeGroupDR,BICD_Count,BICD_BICAR_PR,BICD_RowID From BI_TMP_ClientsAtRiskDetail");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getClientAtRiskDetailsChild()
    {
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("Select CPTPL_ProblemTypeDR,CPTPL_Count,CPTPL_BICD_PR From BI_TMP_CAR_ProblemTypeList");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static string getTotalClientCount(string sessionID)
    {
        Dictionary<String, String> _getClientCount = new Dictionary<String, String>();
        _getClientCount.Add("SESSIONID", sessionID);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetTotalClientCount(?)", _getClientCount).Value.ToString();
    }
}
