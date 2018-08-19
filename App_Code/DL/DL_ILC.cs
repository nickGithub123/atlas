using System;
using System.Collections.Generic;
using System.Data;

/// <summary>
/// Summary description for ILC
/// </summary>
public class DL_ILC
{
    public DL_ILC()
    {
        //
        // TODO : Add constructor logic here
        //
    }
    #region Unused Code
    //public static DataTable getILCMessages(String id)
    //{
    //    String selectStatement = "Select ILCML_MessageSenderUserDR As FromUser, ILCML_MessageSenderLabDR As FromLab,ILCML_MessageSentDate As SendDate, ILCML_MessageSentTime As SendTime, ILCML_MessageRecipientLabDR As ToLab, ILCML_MessageText As Message From ORD_ILCMessageList Where ILCML_ILC_ParRef = '" + id + "' Order By ILCML_ChildSub";
    //    CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
    //    return cache.FillCacheDataTable(selectStatement);
    //}
    #endregion Unused Code
    public static DataTable getILCMessages(String id, String user)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        Dictionary<String, String> _ILCData = new Dictionary<String, String>();
        _ILCData.Add("rowId", id);
        _ILCData.Add("fromLab", user);
        String retval = cache.StoredProcedure("?=call SP2_AuditILC(?,?)", _ILCData).Value.ToString();
        if (retval == "1")
        {
            String selectStatement = "Select ILCML_MessageSenderUserDR As FromUser, ILCML_MessageSenderLabDR As FromLab,ILCML_MessageSentDate As SendDate, ILCML_MessageSentTime As SendTime, ILCML_MessageRecipientLabDR As ToLab, ILCML_MessageText As Message From ORD_ILCMessageList Where ILCML_ILC_ParRef = '" + id + "' Order By ILCML_MessageSentDate DESC,ILCML_MessageSentTime DESC";
            return cache.FillCacheDataTable(selectStatement);
        }
        return null;
    }

    public static String addILCMessage(String rowId, String fromLab, String fromUser, String toLab, String message, String status, String tdTests, String tdLab, String tdDept, String tdReason, String mrMessageCode)
    {
        Dictionary<String, String> _ILCData = new Dictionary<String, String>();
        _ILCData.Add("rowId", rowId);
        _ILCData.Add("fromLab", fromLab);
        _ILCData.Add("fromUser", fromUser);
        _ILCData.Add("toLab", toLab);
        _ILCData.Add("message", message);
        _ILCData.Add("status", status);
        _ILCData.Add("tdTests", tdTests);
        _ILCData.Add("tdLab", tdLab);
        _ILCData.Add("tdDept", tdDept);
        _ILCData.Add("tdReason", tdReason);
        _ILCData.Add("MsgCode", mrMessageCode);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_AddILC(?,?,?,?,?,?,?,?,?,?,?)", _ILCData).Value.ToString();
    }

    public static String deleteILC(String rowId, String User, String AccessionNumber)
    {
        Dictionary<String, String> _ILCData = new Dictionary<String, String>();
        _ILCData.Add("rowId", rowId);
        _ILCData.Add("User", User);
        _ILCData.Add("AccessionNumber", AccessionNumber);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_DeleteILC(?,?,?)", _ILCData).Value.ToString();
    }

    public static String getILCTestsString(String rowId)
    {
        Dictionary<String, String> _ILCData = new Dictionary<String, String>();
        _ILCData.Add("rowId", rowId);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetILCTestsString(?)", _ILCData).Value.ToString();
    }

    public static String getDefaultILCToLab(String rowId)
    {
        Dictionary<String, String> _ILCData = new Dictionary<String, String>();
        _ILCData.Add("rowId", rowId);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetILCDefaultToLab(?)", _ILCData).Value.ToString();
    }

    public static String getILCMessageDefaultStatus(String MessageCode)
    {
        Dictionary<String, String> _ILCData = new Dictionary<String, String>();
        _ILCData.Add("MSGCODE", MessageCode);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetILCMessageDefaultStatus(?)", _ILCData).Value.ToString();
    } 
}
