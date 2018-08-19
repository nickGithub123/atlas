using System;
using System.Data;
using System.Text;
using System.Configuration;
using System.Collections.Generic;

public class DL_Clientgram
{
    public DL_Clientgram()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static DataTable getCientGramDetails(string clientgramID)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT RCG_RowID AS CLIENTGRAMID,");
        sbSQL.Append(" RCG_FullText AS MESSAGEDETAILS,");
        sbSQL.Append(" RCG_Accession AS ACCESSION,");
        sbSQL.Append(" RCG_SSID AS AGENTID,");
        sbSQL.Append(" %EXTERNAL(RCG_DateFiled) AS COLLECTIONDATE,");
        sbSQL.Append(" RCG_TimeFiled AS COLLECTIONTIME,");
        sbSQL.Append(" CLF_CLNUM AS CLIENTNUM,");
        sbSQL.Append(" RCG_DateBatched AS BatchDate,");
        sbSQL.Append(" RCG_TimeBatched AS BatchTime,");
        sbSQL.Append(" $$CO17^XT58(RCG_SSID) AS AGENTIDDISPNAME ");
        sbSQL.Append(" FROM REP_ReportingClientGram");
        sbSQL.Append(" LEFT OUTER JOIN CLF_ClientFile ON RCG_AccountMnemonic = CLF_ClientFile.CLF_CLMNE");
        if (clientgramID != "")
        {
            sbSQL.Append(" WHERE RCG_RowID ='" + clientgramID + "'");
        }

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static String insertClientGram(String account, String patientID, String accessions, String msg, String trakVal, String problemCategory, String location, String comments, String resolution, String filedBy, String filedByDateTime, String batchInfo, String msgType, String autoDial, String inquiryNote)
    {
        Dictionary<String, String> _clientgram = new Dictionary<String, String>();
        _clientgram.Add("AccessionString", accessions.Trim());
        _clientgram.Add("ACCOUNT", account);
        _clientgram.Add("BATCHINFO", batchInfo);
        _clientgram.Add("COMMENTS", comments);
        _clientgram.Add("FILEDBY", filedBy);
        _clientgram.Add("FILEDBY_DATE_TIME", filedByDateTime);
        _clientgram.Add("LAB_LOCATION", location);
        _clientgram.Add("MESSAGE", msg);
        _clientgram.Add("PID", patientID);
        _clientgram.Add("PROB_CATEGORY", problemCategory);
        _clientgram.Add("TRAKING_VALUE", trakVal);
        _clientgram.Add("RESOLUTION", resolution);
        _clientgram.Add("MSGTYPE", msgType);
        _clientgram.Add("AUTODIAL", autoDial);
        _clientgram.Add("INQNOTE", inquiryNote);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_SaveClientGram(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", _clientgram, 99999).Value.ToString();
    }
    // AM IT#59635 AntechCSM 1.0.82.0 
    public static void ResendClientgram(String strAutodial, String strAccount, String strCGID, String strUser, String strLab)
    {
        Dictionary<String, String> _clientgram = new Dictionary<String, String>();
        _clientgram.Add("AUTODIAL", strAutodial);
        _clientgram.Add("ACCOUNT", strAccount);
        _clientgram.Add("CGID", strCGID);
        _clientgram.Add("USER", strUser);
        _clientgram.Add("LAB", strLab);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        String TempVal = cache.StoredProcedure("?=call SP2_ResendClientGram(?,?,?,?,?)", _clientgram, 99999).Value.ToString();
    }   
}
