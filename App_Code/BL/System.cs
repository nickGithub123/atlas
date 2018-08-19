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
using System.Data.OleDb;
using System.Collections.Generic;

public class AntechSystem
{
    public AntechSystem()
    {
        //
    }
    public static String getSystemBroadcastMessage()
    {
        
        String strBroadcastMessage = DL_System.getSystemBroadcastMessage();
        if (strBroadcastMessage.Length  > 0)
        {
            return strBroadcastMessage;
        }
        return String.Empty;
    }
    public static string uploadConsultSpecialty(string filePath)
    {
        string blRetVal = "";
        try
        {
            OleDbConnection MyConnection = null;
            DataSet DtSet = null;
            OleDbDataAdapter MyCommand = null;

            string fileExtension = filePath.Substring(filePath.LastIndexOf(".") + 1);
            if (fileExtension.Equals("xls"))
            {
                //Connection for MS Excel 2003 .xls format
                MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + filePath + "';Extended Properties=Excel 8.0;");
            }
            else if (fileExtension.Equals("xlsx"))
            {
                //Connection for .xslx 2007 format
                MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filePath + "';Extended Properties=Excel 12.0;");
            }

            //Select your Excel file
            MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Consultants Chart$]", MyConnection);
            DtSet = new System.Data.DataSet();
            //Bind all excel data in to data set
            MyCommand.Fill(DtSet, "[Consultants Chart$]");
            DataTable dtblRecords = DtSet.Tables[0];
            MyConnection.Close();
            //Check datatable have records

            blRetVal=DL_System.uploadConsultSpecialty(dtblRecords);

            //Delete temporary Excel file from the Server path
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        catch (Exception exp)
        {
        }

        return blRetVal;
    }

    public static string uploadSupplyItems(string filePath)
    {
        string blRetVal = "";
        try
        {
            OleDbConnection MyConnection = null;
            DataSet DtSet = null;
            OleDbDataAdapter MyCommand = null;

            string fileExtension = filePath.Substring(filePath.LastIndexOf(".") + 1);
            if (fileExtension.Equals("xls"))
            {
                //Connection for MS Excel 2003 .xls format
                MyConnection = new OleDbConnection("provider=Microsoft.Jet.OLEDB.4.0; Data Source='" + filePath + "';Extended Properties=Excel 8.0;");
            }
            else if (fileExtension.Equals("xlsx"))
            {
                //Connection for .xslx 2007 format
                MyConnection = new OleDbConnection("Provider=Microsoft.ACE.OLEDB.12.0;Data Source='" + filePath + "';Extended Properties=Excel 12.0;");
            }

            //Select your Excel file
            MyCommand = new System.Data.OleDb.OleDbDataAdapter("select * from [Supply$]", MyConnection);
            DtSet = new System.Data.DataSet();
            //Bind all excel data in to data set
            MyCommand.Fill(DtSet, "[Supply$]");
            DataTable dtblRecords = DtSet.Tables[0];
            MyConnection.Close();
            //Check datatable have records

            blRetVal = DL_System.uploadSupplyItems(dtblRecords);

            //Delete temporary Excel file from the Server path
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
        catch (Exception exp)
        {
        }

        return blRetVal;
    }
}
