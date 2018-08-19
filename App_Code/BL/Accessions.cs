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
/// Contains methods to fetch a set of Accessions
/// </summary>
public class Accessions
{
    public Accessions()
    {
        //
    }
    public static DataTable getAccessionDialogsDetails(string AccessionNumber)
    {
        return DL_Accessions.getAccessionDialogsDetails(AccessionNumber);
    }

    #region Reset Search Overloads

    public static DataTable getAccessionDetailsExtended_OnlyforTestName(String TestCode, Int32 currentIndex, Int32 pageSize, String pageName, Int32 timeout, out Boolean isError)
    {
        Int32 noOfRecords;
        noOfRecords = (pageSize * (currentIndex + 1) + 1);
        return DL_Accessions.getAccessionDetailsExtended_OnlyforTestName(TestCode, noOfRecords, pageName, timeout, out isError);
    }

    public static DataTable getAccessionDetailsExtended(String AccessionNumber, String AccountNumber, String ChartNumber, String PetName, String owner, String TestCode, String DateFrom, String DateTo, String ZoasisReq, String DoctorName, Int32 startIndex, Int32 noOfRecords, out String query, Boolean useSoundex, String pageName, Int32 timeout, out Boolean isError)
    {
        if (AccessionNumber.Length > 0)
        {
            query = String.Empty;
            return DL_Accessions.getAccessionDetailsByAccessionNumber(AccessionNumber, pageName, timeout, out isError);
        }
        else if (ZoasisReq.Length > 0)
        {
            query = String.Empty;
            return DL_Accessions.getAccessionDetailsByZoasisReq(ZoasisReq, pageName, timeout, out isError);
        }
        else
        {
            return DL_Accessions.getAccessionDetailsExtended(AccountNumber, ChartNumber, PetName, owner, TestCode, DateFrom, DateTo, DoctorName, startIndex, noOfRecords, out query, useSoundex, pageName, timeout, out isError);
        }
    }


    public static DataTable getAccessionDetailsExtended(String AccessionNumber, String AccountNumber, String ChartNumber, String PetName, String owner, String TestCode, String DateFrom, String DateTo, String ZoasisReq, String DoctorName, Int32 startIndex, Int32 noOfRecords, String query, Boolean useSoundex, String pageName, Int32 timeout, out Boolean isError)
    {
        if (AccessionNumber.Length > 0)
        {
            return DL_Accessions.getAccessionDetailsByAccessionNumber(AccessionNumber, pageName, timeout, out isError);
        }
        else if (ZoasisReq.Length > 0)
        {
            return DL_Accessions.getAccessionDetailsByZoasisReq(ZoasisReq, pageName, timeout, out isError);
        }
        else
        {
            if (query.Length > 0)
            {
                return DL_Accessions.getAccessionDetailsExtended(query, startIndex, noOfRecords, pageName, timeout, out isError);
            }
            else
            {
                return DL_Accessions.getAccessionDetailsExtended(AccountNumber, ChartNumber, PetName, owner, TestCode, DateFrom, DateTo, DoctorName, startIndex, noOfRecords, out query, useSoundex, pageName, timeout, out isError);
            }
        }
    }

    //+SSM 22/10/2011 #115232 AntechCSM 2B - Added Params to retrieve maximum of 200 records
    public static DataTable getRelatedAccessions(String PetName, String owner, String DateFrom, String DateTo, String AccountNo)
    {
        return DL_Accessions.getRelatedAccessions(PetName, owner, DateFrom, DateTo, AccountNo);
    }
    //-SSM

    #endregion Reset Search Overloads
}
