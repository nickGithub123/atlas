using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

public class DL_System
{
    public static String getSystemBroadcastMessage()
    {
        Dictionary<string, string> _systemBroadCastMessage = new Dictionary<string, string>();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_GetSystemBroadCastMessage()", _systemBroadCastMessage, 32000).Value.ToString();
    }

    public static string uploadConsultSpecialty(DataTable specDetails)
    {
        string blRetVal = "";
        string strCurrLine = "";
        int intLastCnt = 0;
        StringBuilder sbInput;
        try
        {
            char chFieldSeparator = Convert.ToChar(1);
            string strRowSeparator = "^";

            sbInput = new StringBuilder();
           
            for (int intCnt = 0; intCnt < specDetails.Rows.Count; intCnt++)
            {
                if (specDetails.Rows[intCnt][0].ToString().Trim().Length == 0)
                {
                    continue;
                }

                strCurrLine = specDetails.Rows[intCnt][0].ToString();
                if (sbInput.Length > 0)
                {
                    sbInput.Append(strRowSeparator);
                }

                sbInput.Append(specDetails.Rows[intCnt][0].ToString()); // Consult Code
                sbInput.Append(chFieldSeparator);
                sbInput.Append(specDetails.Rows[intCnt][1].ToString()); //Consult Name
                sbInput.Append(chFieldSeparator);
                sbInput.Append(specDetails.Rows[intCnt][2].ToString().Replace(",", "~"));   // Specialty String
                sbInput.Append(chFieldSeparator);
                sbInput.Append(specDetails.Rows[intCnt][3].ToString().Substring(0, 1)); //Internal/External
                sbInput.Append(chFieldSeparator);
                sbInput.Append(specDetails.Rows[intCnt][4].ToString().Replace(",", "~"));   //Area of Interest
                intLastCnt = intCnt;
            }

            Dictionary<String, String> specdetails = new Dictionary<String, String>();
            specdetails.Add("CNSLTSPECSTR", sbInput.ToString());

            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            blRetVal = cache.StoredProcedure("?=call SP2_UploadCnsltSpclty(?)", specdetails).Value.ToString();
        }
        catch(Exception expOccored)
        {
            string str = expOccored.Message;
        }
        return blRetVal;
    }

    public static string uploadSupplyItems(DataTable itemDetails)
    {
        string blRetVal = "";
        string strCurrLine = "";
        int intLastCnt = 0;
        StringBuilder sbInput;
        try
        {
            char chFieldSeparator = Convert.ToChar(1);
            string strRowSeparator = "^";

            sbInput = new StringBuilder();

            for (int intCnt = 0; intCnt < itemDetails.Rows.Count; intCnt++)
            {
                if (itemDetails.Rows[intCnt][0].ToString().Trim().Length == 0)
                {
                    continue;
                }

                strCurrLine = itemDetails.Rows[intCnt][0].ToString();
                if (sbInput.Length > 0)
                {
                    sbInput.Append(strRowSeparator);
                }

                sbInput.Append(itemDetails.Rows[intCnt][0].ToString()); // Category Code
                sbInput.Append(chFieldSeparator);
                sbInput.Append(itemDetails.Rows[intCnt][1].ToString()); //Category Name
                sbInput.Append(chFieldSeparator);
                sbInput.Append(itemDetails.Rows[intCnt][2].ToString());   // Mnemonic
                sbInput.Append(chFieldSeparator);
                sbInput.Append(itemDetails.Rows[intCnt][3].ToString()); // Description
                sbInput.Append(chFieldSeparator);
                sbInput.Append(itemDetails.Rows[intCnt][4].ToString());   //Multiples Of
                sbInput.Append(chFieldSeparator);
                sbInput.Append(itemDetails.Rows[intCnt][5].ToString());   //Order As
                sbInput.Append(chFieldSeparator);
                sbInput.Append(itemDetails.Rows[intCnt][6].ToString());   //Ordering Notes
                intLastCnt = intCnt;
            }

            Dictionary<String, String> itemdetails = new Dictionary<String, String>();
            itemdetails.Add("SUPPITEMSTR", sbInput.ToString());

            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            blRetVal = cache.StoredProcedure("?=call SP2_UploadSupplyItems(?)", itemdetails).Value.ToString();
        }
        catch (Exception expOccored)
        {
            string str = expOccored.Message;
        }
        return blRetVal;
    }
}
