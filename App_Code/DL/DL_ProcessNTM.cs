using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Text;
using System.Data;

/// <summary>
/// Summary description for DL_ProcessNTM
/// </summary>
public class DL_ProcessNTM
{
	public DL_ProcessNTM()
	{
		//
		// TODO: Add constructor logic here
		//
	}

    public static string getNTMDetailsBySearchOptions(string monitoringLab, string ownerLab)
    {
        Dictionary<String, String> NTMData = new Dictionary<String, String>();
        NTMData.Add("MonitoringLab", monitoringLab);
        NTMData.Add("OwnerLab", ownerLab);
        
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetNTMDetails(?,?)", NTMData,32000).Value.ToString();
    }

    public static DataTable getNTMDetailsReport(string[] accessionList)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        string accessions = string.Join("','", accessionList);
        sb.Append("SELECT ");
        sb.Append("ACC_ClientDR->CLF_CLNUM As AccountNo, ");
        sb.Append("ACC_MiniLogDate As MiniLogDate, ");
        sb.Append("ACC_MiniLogTime As MiniLogTime, ");
        sb.Append("ACC_ClientDR->CLF_CLNAM As AccountName, ");
        sb.Append("ACC_ClientDR->CLF_SalesTerritoryDR As SalesTerritory, ");
        sb.Append("ACC_OwnerLabDR->LABLO_LabName As LabName, ");
        sb.Append("ACC_ClientDR->CLF_CLPHN As AccountPhone, ");
        sb.Append("ACC_Accession As Accession, ");
        sb.Append("ACC_Species As Species, ");
        sb.Append("ACC_PatientName As OnwerName, ");
        sb.Append("ACC_PetFirstName As PetName, ");
        sb.Append("ACC_TestsOrderedDisplayString As Tests, ");
        sb.Append("ACC_SpecimenSubmitted As Specs, ");
        sb.Append("$$GETINQNOTE^XT11(ACC_Accession) As InqNotes, ");
        sb.Append("$$GETSPECIMENLOCATION^XT51(ACC_Accession) AS TubeLocation ");
        sb.Append("FROM ORD_Accession");
        sb.Append(" WHERE ACC_Accession IN ('" + accessions + "')");
        sb.Append(" ORDER BY LabName ASC,AccountNo ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static string getNTMOwnerLab(string MonitoringLab)
    {
        Dictionary<String, String> NTMData = new Dictionary<String, String>();
        NTMData.Add("MonitoringLab", MonitoringLab);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetNTMOwnerLabList(?)", NTMData).Value.ToString();
    }

    public static string validateNTM(string date, string lab)
    {
        Dictionary<String, String> NTMData = new Dictionary<String, String>();
        NTMData.Add("Date", date);
        NTMData.Add("Lab", lab);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ValidateNTM(?,?)", NTMData).Value.ToString();
    }

    public static string processNTM(string accessionList, string user, string lab)
    {
        Dictionary<String, String> NTMData = new Dictionary<String, String>();
        NTMData.Add("AccessionList", accessionList);
        NTMData.Add("User", user);
        NTMData.Add("MonitoringLab", lab);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ProcessNTM(?,?,?)", NTMData).Value.ToString();
    }

    public static DataTable getNTMCallbackNotes(string accession)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("NTM_CallbackNotes  AS CallbackNotes ");

        sbSQL.Append("FROM ORD_NoTestMarked ");
        sbSQL.Append("WHERE NTM_AccessionDR  ='");
        sbSQL.Append(accession);
        sbSQL.Append("'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static bool updateNTMCallbackNotes(string accession, string notes)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("UPDATE ORD_NoTestMarked SET ");
        sbSQL.Append("NTM_CallbackNotes  = '");
        sbSQL.Append(notes);
        sbSQL.Append("' ");
        sbSQL.Append("WHERE NTM_AccessionDR  ='");
        sbSQL.Append(accession);
        sbSQL.Append("'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.Transaction(sbSQL.ToString());
    }
}
