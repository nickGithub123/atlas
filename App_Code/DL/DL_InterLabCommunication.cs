using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;

public class DL_InterLabCommunication
{
    public DL_InterLabCommunication()
    {
        //		
    }

    public static DataTable getInterLabCommDetails(string AccessionNumber, string InitiatingUser, string MessageToLab, string CurrentStatus, string DateFrom, string DateTo, string MessageFromLab, string AccountNumber, string InnitiatingMessageCode)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ILC_RowID AS ROWID,");
        sb.Append("ILC_AccessionDR AS ACCESSION,");
        sb.Append("$$CO17^XT58(ILC_InitiatingUserDR->USER_UserID) AS INITIATINGUSER,");
        sb.Append("ILC_InitiatingLabDR->LABLO_LabName AS INITIATINGLAB,");
        sb.Append("ILC_CurrentStatus AS CURRENTSTATUS,");
        sb.Append("ILC_FirstMessage AS MESSAGE,");
        sb.Append("ILC_DateEntered AS DATEENTERED,");
        sb.Append("ILC_TimeEntered AS TIMEENTERED, ");
        sb.Append("ILC_CurrentOwnerLabDR->LABLO_LabName AS  CURRENTLYWITHLAB ");
        sb.Append("FROM ");
        sb.Append("ORD_InterlabCommunication ");        
        sb.Append("Where 1=1 ");
        if (AccessionNumber.Length > 0)
        {
            sb.Append(" AND ILC_AccessionDR ='" + AccessionNumber + "'");
        }
        if (InitiatingUser.Length > 0)
        {
            sb.Append(" AND ILC_InitiatingUserDR  ='" + InitiatingUser + "'");
        }
        if (MessageToLab.Length > 0)
        {
            sb.Append(" AND ILC_CurrentOwnerLabDR  ='" + MessageToLab + "'");
        }
        if (CurrentStatus.Length > 0)
        {
            sb.Append(" AND ILC_CurrentStatus ='" + CurrentStatus + "'");
        }
        if (DateFrom.Length > 0 && DateTo.Length > 0)
        {
            sb.Append(" AND ILC_DateEntered>= TO_DATE('" + DateFrom + "','MM/DD/YYYY') AND ILC_DateEntered<= TO_DATE('" + DateTo + "','MM/DD/YYYY')");
        }
        if (MessageFromLab.Length > 0)
        {
            sb.Append(" AND ILC_InitiatingLabDR ='" + MessageFromLab + "'");
        }
        if (AccountNumber.Length > 0)
        {
            sb.Append(" AND ILC_ClientDR->CLF_CLNUM ='" + AccountNumber + "'");
        }
        if (InnitiatingMessageCode.Length > 0)
        {
            sb.Append(" AND ILC_InitiatingMessageCodeDR ='" + InnitiatingMessageCode + "'");
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static String insertNewInterLabCommunication(String strAccession,String strInitUser,String strInitLab,String strMessage,String strToLab,String strTestString, String strMessageCode)
    {
        Dictionary<string, string> _ILCData = new Dictionary<string, string>();
        _ILCData.Add("ACCESSION", strAccession);
        _ILCData.Add("INITUSER", strInitUser);
        _ILCData.Add("INITLAB", strInitLab);
        _ILCData.Add("MESSAGE", strMessage);
        _ILCData.Add("TOLAB", strToLab);
        _ILCData.Add("TESTSTRING", strTestString);
        _ILCData.Add("MSGCODE", strMessageCode); 
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_SaveILC(?,?,?,?,?,?,?)", _ILCData).Value.ToString();
    }

    #region Unused code
    /*
    public static DataTable getInterLabCommDetails(String ID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ILC_AccessionDR AS Accession,");
        sb.Append("ILC_ClientDR AS Client,");
        sb.Append("ILC_InitiatingUserDR AS InitiatingUser,");
        sb.Append("ILC_InitiatingLabDR AS InitiatingLab,");
        sb.Append("ILC_DateEntered AS DateEntered,");
        sb.Append("ILC_TimeEntered AS TimeEntered,");
        sb.Append("%External(ILC_IsResolved) AS Status ");
        sb.Append("FROM ");
        sb.Append("ORD_InterlabCommunication ");
        sb.Append("Where ILC_RowID='" + ID + "' ");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }
    
    public static DataTable getInterLabHistory(String ID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ILCML_MessageSenderLabDR AS SenderLab,");
        sb.Append("ILCML_MessageSenderUserDR AS SenderUser,");
        sb.Append("ILCML_MessageSenderUserDR->USER_DepartmentDR AS SenderDepartment,");
        sb.Append("ILCML_MessageSentDate AS SendDate,");
        sb.Append("ILCML_MessageSentTime AS SentTime,");
        sb.Append("ILCML_MessageRecipientLabDR AS RecipientLab,");
        sb.Append("ILCML_MessageRecipientUserDR AS RecipientUser,");
        sb.Append("ILCML_MessageText AS Message ");
        sb.Append("FROM ");
        sb.Append("ORD_ILCMessageList ");
        sb.Append("Where ILCML_ILC_ParRef='" + ID + "' ");
        sb.Append("ORDER BY ILCML_MessageSentDate ASC, ILCML_MessageSentTime ASC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }*/
    #endregion Unused code
}
