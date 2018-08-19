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
/// Summary description for ProblemMatrix
/// </summary>
public class ProblemMatrix
{
	public ProblemMatrix()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM AntechCSM 1.0.34.0 12/08/2008
    public DataTable getProblemMatrixString(String YEAR, String ALLCLIENTS, String CLIENTLIST, String ALLPTYPE, String PTYPELIST, String ALLLAB, String LAB)
    {
        String strPM= DL_ProblemMatrix.getProblemMatrixString(YEAR, ALLCLIENTS, CLIENTLIST, ALLPTYPE, PTYPELIST,ALLLAB,LAB);
        String[] strPMArr = strPM.Split('^');
        if (strPMArr[0] == "" && strPMArr.Length==2)
            strPM = strPM.Replace("^","");
        return AtlasIndia.AntechCSM.functions.StringToDataTable(strPM, '^', '~', '!');
    }
}
