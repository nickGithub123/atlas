using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// The business logic class for ProcessNTM.
/// </summary>
public class ProcessNTM
{
	public ProcessNTM()
	{
	}

    public static DataTable getNTMDetailsBySearchOptions(string monitoringLab, string ownerLab)
    {
        DataTable dtblNTM = new DataTable();
        dtblNTM.Columns.Add("OwnerLab");
        dtblNTM.Columns.Add("Accession");
        dtblNTM.Columns.Add("UnitCodeRow");
        dtblNTM.Columns.Add("ClientgramSentDate");
        dtblNTM.Columns.Add("ClientgramSentTime");
        dtblNTM.Columns.Add("SendCG", Type.GetType("System.Boolean"));
        //+ SSM Issue#113429 09/22/2011 AntechCSM2.1.006 Added 2Columns(AccNo,TestedOrders)
        dtblNTM.Columns.Add("AccountNo", Type.GetType("System.Int32"));
        dtblNTM.Columns.Add("TestsOrdered");
        dtblNTM.Columns.Add("OwnerName");
        //- SSM
        string strNTMDetails = DL_ProcessNTM.getNTMDetailsBySearchOptions(monitoringLab, ownerLab);
        if (strNTMDetails.Trim().Length == 0)
        {
            return dtblNTM;
        }

        string[] arrRows = strNTMDetails.Split(new Char[] { '^' });

        for (int intCount = 0; intCount < arrRows.Length; intCount++)
        {
            string[] arrData = arrRows[intCount].Split(new Char[] { '~' });

            DataRow drowNewRow = dtblNTM.NewRow();
            drowNewRow["OwnerLab"] = arrData[0].Trim();
            drowNewRow["Accession"] = arrData[1].Trim();
            drowNewRow["UnitCodeRow"] = arrData[2].Trim();
            drowNewRow["ClientgramSentDate"] = arrData[3].Trim();
            drowNewRow["ClientgramSentTime"] = arrData[4].Trim();
            drowNewRow["SendCG"] = false;
            drowNewRow["AccountNo"] = arrData[5].Trim();
            drowNewRow["TestsOrdered"] = arrData[6].Trim();
            drowNewRow["OwnerName"] = arrData[7].Trim();
            dtblNTM.Rows.Add(drowNewRow);
        }
        dtblNTM.DefaultView.Sort = "OwnerLab ASC,AccountNo ASC";
        return dtblNTM;
    }

    public static DataTable getNTMDetailsReport(string monitoringLab, string ownerLab)
    {
        DataTable dtNTMReport = new DataTable();
        string strNTMDetails = DL_ProcessNTM.getNTMDetailsBySearchOptions(monitoringLab, ownerLab);
        if (strNTMDetails.Trim().Length == 0)
        {
            return dtNTMReport;
        }
        string[] arrRows = strNTMDetails.Split(new Char[] { '^' });
        string[] arrAccessionList = new string[arrRows.Length];
        for (int intCount = 0; intCount < arrRows.Length; intCount++)
        {
            string[] arrData = arrRows[intCount].Split(new Char[] { '~' });
            arrAccessionList[intCount] = arrData[1].Trim();
        }
        dtNTMReport = DL_ProcessNTM.getNTMDetailsReport(arrAccessionList);
        return dtNTMReport;
    }

    public static DataTable getNTMOwnerLab(string monitoringLab)
    {
        DataTable dtblNTMLab = new DataTable();
        dtblNTMLab.Columns.Add("ID");
        dtblNTMLab.Columns.Add("Name");

        string strNTMOwnerLabList = DL_ProcessNTM.getNTMOwnerLab(monitoringLab);

        // Adding blank row
        DataRow drowBlankRow = dtblNTMLab.NewRow();
        drowBlankRow["ID"] = "";
        drowBlankRow["Name"] = "";
        dtblNTMLab.Rows.Add(drowBlankRow);

        if (strNTMOwnerLabList.Trim().Length == 0)
        {
            return dtblNTMLab;
        }

        string[] arrRows = strNTMOwnerLabList.Split(new Char[] { '^' });

        for (int intCount = 0; intCount < arrRows.Length; intCount++)
        {
            string[] arrData = arrRows[intCount].Split(new Char[] { '~' });

            DataRow drowNewRow = dtblNTMLab.NewRow();
            drowNewRow["ID"] = arrData[0];
            drowNewRow["Name"] = arrData[1];

            dtblNTMLab.Rows.Add(drowNewRow);
        }

        return dtblNTMLab;
    }

    public static bool validateNTM(string date, string lab)
    {

        string strRetval = DL_ProcessNTM.validateNTM(date, lab);

        if (strRetval == "1")
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static bool processNTM(string accessionList, string user, string lab)
    {
        string strRetVal = DL_ProcessNTM.processNTM(accessionList, user, lab);

        if (strRetVal.Equals("1"))
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public static string getNTMCallbackNotes(string accession)
    {
        DataTable dtblNotes = DL_ProcessNTM.getNTMCallbackNotes(accession);
        
        return dtblNotes.Rows[0]["CallbackNotes"].ToString();
    }

    public static bool hasNTMCallbackNotes(string accession)
    {
        DataTable dtblNotes = DL_ProcessNTM.getNTMCallbackNotes(accession);

        if (dtblNotes.Rows.Count == 0)
        {
            return false;
        }
        return true;
    }
    public static bool updateNTMCallbackNotes(string accession, string notes)
    {
        return DL_ProcessNTM.updateNTMCallbackNotes(accession, notes);
    }
}
