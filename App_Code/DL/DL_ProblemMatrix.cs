using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Collections.Generic;
/// <summary>
/// Summary description for DL_ProblemMatrix
/// </summary>
public class DL_ProblemMatrix
{
	public DL_ProblemMatrix()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM AntechCSM 1.0.34.0 12/08/2008
    public static string getProblemMatrixString(String YEAR, String ALLCLIENTS, String CLIENTLIST, String ALLPTYPE, String PTYPELIST, String ALLLAB, String LAB)
    {
        Dictionary<string, string> _problemMatrix = new Dictionary<string, string>();
        _problemMatrix.Add("YEAR", YEAR);
        _problemMatrix.Add("ALLCLIENTS", ALLCLIENTS);
        _problemMatrix.Add("CLIENTLIST", CLIENTLIST);
        _problemMatrix.Add("ALLPTYPE", ALLPTYPE);
        _problemMatrix.Add("PTYPELIST", PTYPELIST);
        //AM AntechCSM 1.0.35.0 12/08/2009
        _problemMatrix.Add("ALLLAB", ALLLAB);
        _problemMatrix.Add("LAB", (ALLLAB == "1" ? "" : LAB));
        //-AM
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        //return cache.StoredProcedure("?=call SP2_GetProblemMatrix(?,?,?,?,?,?,?)", _problemMatrix).Value.ToString();
        return cache.StoredProcedure("?=call SP2_GetProblemMatrix(?,?,?,?,?,?,?)", _problemMatrix,99999).Value.ToString();
    }
}
