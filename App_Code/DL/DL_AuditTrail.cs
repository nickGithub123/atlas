using System;
using System.Data;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;

/// <summary>
/// Summary description for DL_AuditTrail
/// </summary>
public class DL_AuditTrail
{
	public DL_AuditTrail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#46787 AntechCSM 1.0.20.0 10/24/2008
    public static DataTable getAuditTrailDetails(String TestDelID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("TDAUD_RowID AS ROWID,");
        sb.Append("TDAUD_Date AS DATEENTERED,");
        sb.Append("TDAUD_Time AS TIMEENTERED,");
        sb.Append("$$CO17^XT58(TDAUD_User) AS USERID,");
        sb.Append("TDAUD_ActionDescription AS DESCRIPTION ");
        sb.Append("FROM ");
        sb.Append("ORD_TestDeletionAuditTrail ");
        sb.Append("Where 1=1 AND ");
        sb.Append("TDAUD_TD_ParRef =" + TestDelID );
        sb.Append(" ORDER BY TDAUD_Date ");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getILCAuditTrailDetails(String ILCRowID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ILAUD_RowID AS ROWID,");
        sb.Append("ILAUD_Date AS DATEENTERED,");
        sb.Append("ILAUD_Time AS TIMEENTERED,");
        sb.Append("$$CO17^XT58(ILAUD_User) AS USERID,");
        sb.Append("ILAUD_ActionDescription AS DESCRIPTION ");
        sb.Append("FROM ");
        sb.Append("ORD_ILCAuditTrail ");
        sb.Append("Where 1=1 AND ");
        sb.Append("ILAUD_ILC_ParRef =" + ILCRowID);
        sb.Append(" ORDER BY ILAUD_Date ");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable GetAuditTrailsForPurple(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("MSAUD_AuditRecordDR AS ROWID,");
        sbSQL.Append("MSAUD_AuditRecordDR->AUD_Date AS DATEENTERED,");
        sbSQL.Append("MSAUD_AuditRecordDR->AUD_Time AS TIMEENTERED,");
        sbSQL.Append("$$CO17^XT58(MSAUD_AuditRecordDR->AUD_User) AS USERID,");
        sbSQL.Append("MSAUD_AuditRecordDR->AUD_ActionDescription AS DESCRIPTION ");

        sbSQL.Append("FROM ORD_MissingSpecimenAudit ");
        sbSQL.Append("WHERE MSAUD_MISSS_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");
        sbSQL.Append("ORDER BY MSAUD_ChildSub DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }


    public static DataTable GetAuditTrailForClientIssue(string rowId)
    {
        StringBuilder sbSQL = new StringBuilder();
        sbSQL.Append("SELECT ");
        sbSQL.Append("CIAUD_AuditRecordDR AS ROWID,");
        sbSQL.Append("CIAUD_AuditRecordDR->AUD_Date AS DATEENTERED,");
        sbSQL.Append("CIAUD_AuditRecordDR->AUD_Time AS TIMEENTERED,");
        sbSQL.Append("$$CO17^XT58(CIAUD_AuditRecordDR->AUD_User) AS USERID,");
        sbSQL.Append("CIAUD_AuditRecordDR->AUD_ActionDescription AS DESCRIPTION ");

        sbSQL.Append("FROM CLF_ClientIssueAudit ");
        sbSQL.Append("WHERE CIAUD_CLI_PR ='");
        sbSQL.Append(rowId);
        sbSQL.Append("' ");
        sbSQL.Append("ORDER BY CIAUD_ChildSub DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

}
