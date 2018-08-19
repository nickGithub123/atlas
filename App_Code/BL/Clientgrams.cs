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
/// Summary description for Clientgrams
/// </summary>
public class Clientgrams
{
	public Clientgrams()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    //AM Issue#32868 04/09/2008 0.0.0.9
    public static DataTable getCientGramDetails(string clientgramID, string accountNumber, string labLocation, string salesTerritory, string dateFrom, string dateTo, string accessionNumber, string user)
    {
        DataTable returnDataTable = new DataTable();
        DL_ClientGrams cg = new DL_ClientGrams();
        returnDataTable = cg.getCientGramDetails(clientgramID, accountNumber, labLocation, salesTerritory, dateFrom, dateTo, accessionNumber, user);
        if (returnDataTable.Rows.Count > 0)
        {
            return returnDataTable;
        }
        else
        {
            return null;
        }
    }
}
