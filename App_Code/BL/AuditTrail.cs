using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

/// <summary>
/// Summary description for AuditTrail
/// </summary>
public class AuditTrail
{
	public AuditTrail()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#46787 AntechCSM 1.0.20.0 10/24/2008
    public DataTable getAuditTrailDetails(String TestDelID)
    {
        return DL_AuditTrail.getAuditTrailDetails(TestDelID);
    }

    public DataTable getILCAuditTrailDetails(String ILCRowID)
    {
        return DL_AuditTrail.getILCAuditTrailDetails(ILCRowID);
    }

    public DataTable GetAuditTrailsForPurple(string purpleRowId)
    {
        return DL_AuditTrail.GetAuditTrailsForPurple(purpleRowId);
    }

    public DataTable GetAuditTrailForClientIssue(string clientIssueId)
    {
        return DL_AuditTrail.GetAuditTrailForClientIssue(clientIssueId);
    }
}
