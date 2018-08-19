using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

public class DL_ProblemManagement
{
    public DL_ProblemManagement()
    {
        //
    }

    //AM Issue#32865 1.0.0.9 03/18/2008
    public static DataTable ProblemDetails(String problemID)
    {
        String selectStatement = "SELECT PBT_PSQ,PBT_RowID PROBLEMID, PBT_LOGID AS ENTEREDBY,PBT_RESID AS RESOLVEDBY,%EXTERNAL(PBT_LoggedDate) AS ENTEREDDATE,PBT_LoggedTime AS ENTEREDTIME,%EXTERNAL(PBT_RESDate) AS RESOLUTIONDATE,PBT_REStime AS RESOLUTIONTIME,PBT_Comment AS COMMENTS,PBT_Resolution AS RESOLUTION,PBT_AccessionDR AS ACCESSION, PBT_LabLocation LABLOCATION, PBT_ProbTP AS PROBLEMTYPE, $$CO17^XT58(PBT_RESID) AS RESOLVEDBYDISPNAME, $$CO17^XT58(PBT_LOGID) AS ENTEREDBYDISPNAME";
        selectStatement = selectStatement + " FROM CSV_ProbTracking WHERE PBT_RowID = '" + problemID + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    //AM Issue#32823
    public static DataTable getProblemDetailsBySearchOptions(string AccountNumber, string AccessionNumber, string ProblemCategory, string ProblemStatus, string Location, string EnteredBy, string ResolvedBy, string ProblemNumber, string DateFrom, string DateTo, string SalesTerritory)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("PBT_PSQ,");
        sb.Append("PBT_RowID PROBLEMID,");
        sb.Append("CLF_CLNUM  AS ACCOUNTNUM,");
        sb.Append("CLF_CLMNE AS AccountMnemonic,");
        sb.Append("CLF_CLNAM AS ACCOUNTNAME,");
        sb.Append("CLF_CLPHN As PHONE,");
        sb.Append("CLF_CLAD1 AS ADDRESS1,");
        sb.Append("CLF_CLAD2 AS ADDRESS2,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sb.Append("CLF_CLADG AS AUTODIALGR,");
        sb.Append("zoasis_num AS ZOASIS,");
        sb.Append("CLF_CLRTS AS RouteStop,");
        sb.Append("PBT_ProbTP->PTYP_ProblemType AS PBT_ProbTP,");
        sb.Append("$$CO17^XT58(PBT_LOGID) As PBT_LOGID,");
        sb.Append("PBT_LoggedDate,");
        sb.Append("PBT_LoggedTime,");
        sb.Append("SUBSTRING(PBT_Comment,1,40) AS PBT_Comment,");
        sb.Append("PBT_LabLocation AS LOCATION,");
        sb.Append("PBT_Status AS STATUS,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue,");
        sb.Append("CLF_IsHot AS ClientIsHot,");
        sb.Append("CLF_IsAlliedClient AS ClientIsAllied,");
        sb.Append("CLF_IsNew AS ClientIsNew ");

        sb.Append("FROM ");
        sb.Append("CSV_ProbTracking ");
        sb.Append("LEFT OUTER JOIN CLF_ClientFile Client on PBT_CLDesRef = Client.CLF_CLMNE ");
        sb.Append("LEFT OUTER JOIN zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");

        sb.Append("WHERE 1=1 ");
        if (AccountNumber.Length > 0)
        {
            sb.Append(" AND PBT_CLDesRef->CLF_CLNUM ='" + AccountNumber + "'");
        }
        if (AccessionNumber.Length > 0)
        {
            sb.Append(" AND PBT_AccessionDR  ='" + AccessionNumber + "'");
        }
        if (ProblemCategory.Length > 0)
        {
            sb.Append(" AND PBT_ProbTP  ='" + ProblemCategory + "'");
        }
        if (ProblemStatus == "Resolved")
        {
            sb.Append(" AND PBT_Status ='R'");
        }
        else if (ProblemStatus == "Unresolved")
        {
            sb.Append(" AND PBT_Status ='UR'");
        }
        if (Location.Length > 0)
        {
            sb.Append(" AND PBT_LabLocation ='" + Location + "'");
        }
        if (EnteredBy.Length > 0)
        {
            sb.Append(" AND PBT_LOGID ='" + EnteredBy + "'");
        }
        if (ResolvedBy.Length > 0)
        {
            sb.Append(" AND PBT_RESID ='" + ResolvedBy + "'");
        }
        if (ProblemNumber.Length > 0)
        {
            sb.Append(" AND PBT_PSQ ='" + ProblemNumber + "'");
        }
        //AM AntechCSM 1.0.15.0 09/25/2008
        if (SalesTerritory.Length > 0)
        {
            //sb.Append(" AND ZIP_Territory ='" + SalesTerritory + "'");
            sb.Append(" AND PBT_CLDesRef->CLF_SalesTerritoryDR ='" + SalesTerritory + "'");
        }
        //-AM
        if (DateFrom.Length > 0 && DateTo.Length > 0)
        {
            sb.Append(" AND PBT_LoggedDate>= TO_DATE('" + DateFrom + "','MM/DD/YYYY') AND PBT_LoggedDate<= TO_DATE('" + DateTo + "','MM/DD/YYYY')");
        }
        ///<Commented aagrawal Issue #00000 08/13/2008>
        //sb.Append(" AND len(CLF_CLNUM)>0 ORDER BY PBT_LoggedDate DESC,PBT_PSQ DESC");
        ///</Commented aagrawal Issue #00000 08/13/2008>
        sb.Append(" ORDER BY PBT_LoggedDate DESC,PBT_PSQ DESC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    //+SSM 11/14/2011 AntechCSM 2a2 #Issue116544 Added to Fetch details for Problem Tracking report
    public static DataTable getProblemSearchReport(String strQS)
    {
        String[] QS = strQS.Split('^');
        DataTable returnDataTable = new DataTable();
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("PBT_PSQ,");
        sb.Append("PBT_RowID AS PROBLEMID,");
        sb.Append("CLF_CLNUM  AS ACCOUNTNUM,");
        sb.Append("CLF_CLNAM AS ACCOUNTNAME,");
        sb.Append("CLF_CLPHN As PHONE,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS SALESTERRITORY,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SALESREP,");
        sb.Append("PBT_ProbTP->PTYP_ProblemType AS TYPE,");
        sb.Append("PBT_ProbTP->PTYP_Description As DESCRIPTION,");
        sb.Append("PBT_LabLocation->LABLO_LabName AS LOCATION,");   //SSM 11/15/2011 AntechCSM 2a2 #Issue116544   Added Decriptive Lab Location
        sb.Append("%EXTERNAL(PBT_Status) AS STATUS,");
        sb.Append("PBT_AccessionDR AS ACCESSION,");
        sb.Append("PBT_AccessionDR->ACC_OwnerLastName||', '||PBT_AccessionDR->ACC_PetFirstName AS PATIENT,");
        sb.Append("PBT_AccessionDR->ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("PBT_AccessionDR->ACC_MiniLogDate As ORDERDATE,");
        sb.Append("PBT_AccessionDR->ACC_MiniLogTime As ORDERTIME,");
        sb.Append("PBT_Comment As COMMENTS,");
        sb.Append("PBT_LoggedDate As LOGGEDDATE,");
        sb.Append("PBT_LoggedTime As LOGGEDTIME,");
        sb.Append("$$CO17^XT58(PBT_LOGID) As ENTEREDBY,");
        sb.Append("PBT_Resolution As RESOLUTION,");
        sb.Append("PBT_RESDate As RESOLVEDDATE,");
        sb.Append("PBT_REStime As RESOLVEDTIME,");
        sb.Append("$$CO17^XT58(PBT_RESID) As RESOLVEDBY ");

        sb.Append("FROM ");
        sb.Append("CSV_ProbTracking ");
        sb.Append("LEFT OUTER JOIN CLF_ClientFile Client on PBT_CLDesRef = Client.CLF_CLMNE ");

        sb.Append("WHERE 1=1 ");

        if (QS[0].Length > 0) sb.Append(" AND PBT_CLDesRef->CLF_CLNUM ='" + QS[0] + "'");   //AccountNo
        if (QS[1].Length > 0) sb.Append(" AND PBT_AccessionDR  ='" + QS[1] + "'");  //AccessionNo
        if (QS[2].Length > 0) sb.Append(" AND PBT_ProbTP  ='" + QS[2] + "'");   //ProblemCategory
        if (QS[3] == "Resolved")    //ProblemStatus
        {
            sb.Append(" AND PBT_Status ='R'");
        }
        else if (QS[3] == "Unresolved")
        {
            sb.Append(" AND PBT_Status ='UR'");
        }
        if (QS[4].Length > 0)   //Location
        {
            sb.Append(" AND PBT_LabLocation ='" + QS[4] + "'");
        }
        if (QS[5].Length > 0)   //EnteredBy
        {
            sb.Append(" AND PBT_LOGID ='" + QS[5] + "'");
        }
        if (QS[6].Length > 0)   //ResolvedBy
        {
            sb.Append(" AND PBT_RESID ='" + QS[6] + "'");
        }
        if (QS[7].Length > 0)   //ProblemNumber
        {
            sb.Append(" AND PBT_PSQ ='" + QS[7] + "'");
        }
        if (QS[8].Length > 0)   //SalesTerritory
        {
            sb.Append(" AND PBT_CLDesRef->CLF_SalesTerritoryDR ='" + QS[8] + "'");
        }
        if (QS[9].Length > 0 && QS[10].Length > 0)  //DateFrom DateTo
        {
            sb.Append(" AND PBT_LoggedDate>= TO_DATE('" + QS[9] + "','MM/DD/YYYY') AND PBT_LoggedDate<= TO_DATE('" + QS[10] + "','MM/DD/YYYY')");
        }
        sb.Append(" ORDER BY PBT_LoggedDate DESC,PBT_PSQ DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sb.ToString());
        return returnDataTable;
    }
    //-SSM

    //AM Issue#32823 04/28/2008
    public static string insertProblemDetails(string ClientID, string AccessionNumber, string ProblemCategory, string location, string commentText, string resolutionText, string enteredBy, string resolvedBy)
    {
        Dictionary<string, string> _problemData = new Dictionary<string, string>();
        _problemData.Add("CLIENTID", ClientID);
        _problemData.Add("ACCESSIONNUMBER", AccessionNumber);
        _problemData.Add("PROBLEMCATEGORY", ProblemCategory);
        _problemData.Add("LOCATION", location);
        _problemData.Add("COMMENTTEXT", commentText);
        _problemData.Add("RESOLUTIONTEXT", resolutionText);
        _problemData.Add("ENTEREDBY", enteredBy);
        _problemData.Add("RESOLVEDBY", resolvedBy);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_SaveProblemDetails(?,?,?,?,?,?,?,?)", _problemData).Value.ToString();
    }

    public static string updateProblemDetails(String ClientID, String ProblemID, String UserName, String NewComment, String NewResolution, String Location, String problemCategory)
    {
        Dictionary<string, string> _problemData = new Dictionary<string, string>();
        _problemData.Add("CLIENTID", ClientID);
        _problemData.Add("ProblemID", ProblemID);
        _problemData.Add("UserName", UserName);
        _problemData.Add("NewComment", NewComment);
        _problemData.Add("NewResolution", NewResolution);
        _problemData.Add("Location", Location);
        _problemData.Add("ProblemCategory", problemCategory);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdateProblemUsingRoutine(?,?,?,?,?,?,?)", _problemData).Value.ToString();
    }
}