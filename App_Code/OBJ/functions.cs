using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text.RegularExpressions;

public struct Address
{
    public string Address1;
    public string Address2;
    public string Address3;
    public string Address4;
}
public class functions
{
    public functions()
    {
    }

    public static DataTable SearchForAccounts(string searchValue, string searchKey, string location)
    {
        Clients.SearchOption key;
        switch (searchKey)
        {
            case "ACCOUNT_NUMBER":
                key = Clients.SearchOption.ACCOUNT_NUMBER;
                break;
            case "ACCOUNT_NAME":
                key = Clients.SearchOption.ACCOUNT_NAME;
                break;
            case "ACCOUNT_MNEMONIC":
                key = Clients.SearchOption.ACCOUNT_MNEMONIC;
                break;
            case "PHONE_NUMBER":
                key = Clients.SearchOption.PHONE_NUMBER;
                break;
            case "ZOASIS_NUMBER":
                key = Clients.SearchOption.ZOASIS_NUMBER;
                break;
            case "ROUTE_STOP":
                key = Clients.SearchOption.ROUTE_STOP;
                break;
            case "SALES_TERRITORY":
                key = Clients.SearchOption.SALES_TERRITORY;
                break;
            default:
                key = Clients.SearchOption.ACCOUNT_NUMBER;
                break;
        }
        DataTable returnDataTable = new DataTable();
        returnDataTable = (location == "" ? Clients.getClients(searchValue, key) : Clients.getClients(searchValue, key, location)); // AM 03/17/2008 - Adding search critria location also
        return returnDataTable;
    }

    public static DataTable SearchForUnits(String searchValue, String searchKey)
    {
        Catalog.SearchOption key;
        switch (searchKey)
        {
            case "UNIT_CODE":
                key = Catalog.SearchOption.UNIT_CODE;
                break;
            case "UNIT_NAME":
                key = Catalog.SearchOption.UNIT_NAME;
                break;
            case "COMBINED":
                key = Catalog.SearchOption.COMBINED;
                break;
            default:
                key = Catalog.SearchOption.COMBINED;
                break;
        }
        return Catalog.getUnits(searchValue, key);
    }

    public static DataTable SearchForUnits(String searchValue, String searchKey, String clientCountry)
    {
        Catalog.SearchOption key;
        switch (searchKey)
        {
            case "UNIT_CODE":
                key = Catalog.SearchOption.UNIT_CODE;
                break;
            case "UNIT_NAME":
                key = Catalog.SearchOption.UNIT_NAME;
                break;
            case "COMBINED":
                key = Catalog.SearchOption.COMBINED;
                break;
            default:
                key = Catalog.SearchOption.COMBINED;
                break;
        }
        return Catalog.getUnits(searchValue, key, clientCountry);
    }
    public static DataTable SearchForUnits(String clientCountry, String searchValue, bool yes,String showReplacedCode)
    {
        return Catalog.getUnits(clientCountry, searchValue, showReplacedCode);
    }

    public static DataTable SearchForUnits(String searchValue, String searchKey, String clientCountry, int startIndex, int noOfRecords)
    {
        Catalog.SearchOption key;
        switch (searchKey)
        {
            case "UNIT_CODE":
                key = Catalog.SearchOption.UNIT_CODE;
                break;
            case "UNIT_NAME":
                key = Catalog.SearchOption.UNIT_NAME;
                break;
            case "COMBINED":
                key = Catalog.SearchOption.COMBINED;
                break;
            default:
                key = Catalog.SearchOption.COMBINED;
                break;
        }
        return Catalog.getUnits(searchValue, key, clientCountry, startIndex, noOfRecords);
    }

    //AM Issue#37267 04/17/2008 0.0.0.9
    public static DataTable SearchForUsers(string searchValue, string searchKey)
    {
        Users.SearchOption key;
        switch (searchKey)
        {
            case "USER_ID":
                key = Users.SearchOption.USER_ID;
                break;
            case "USER_NAME":
                key = Users.SearchOption.USER_NAME;
                break;
            case "USER_FIRSTNAME":
                key = Users.SearchOption.USER_FIRSTNAME;
                break;
            default:
                key = Users.SearchOption.USER_NAME;
                break;
        }
        DataTable returnDataTable = new DataTable();
        returnDataTable = Users.getUsers(searchValue, key);
        return returnDataTable;
    }

    //AM Issue#37633 04/29/2008 0.0.0.9
    public static DataTable SearchForGroups(string searchValue, string searchKey)
    {
        Groups.SearchOption key;
        switch (searchKey)
        {
            case "GROUP_ID":
                key = Groups.SearchOption.GROUP_ID;
                break;
            case "GROUP_NAME":
                key = Groups.SearchOption.GROUP_NAME;
                break;
            default:
                key = Groups.SearchOption.GROUP_NAME;
                break;
        }
        DataTable returnDataTable = new DataTable();
        returnDataTable = Groups.getGroups(searchValue, key);
        return returnDataTable;
    }

    //AM Issue#32895 04/26/2008 Build Number 0.0.0.9
    public static HtmlMeta refreshPage(string mailRefreshTime)
    {
        HtmlMeta meta = new HtmlMeta();
        meta.HttpEquiv = "Refresh";
        meta.Content = mailRefreshTime;
        return meta;
    }

    // SG added SoundEx function
    // 05/09/2008

    public static string GetSoundEx(string name)
    {

        // sound ex operations 
        if (name == null || name == string.Empty)
            return string.Empty;

        // step 1: filter non-alphanumeric character and capitalize 
        name = Regex.Replace(name.ToUpper(), "[^A-Z0-9]", "");

        // step 2: if string starts CI and CE, replace with S 
        if (name.StartsWith("CI") | name.StartsWith("CE"))
        {
            name = "S" + name.Substring(2);
        }

        // step 3: set pre= first character of name, and set name = to be the rest 
        string prefix = name.Substring(0, 1);
        name = name.Substring(1);

        // step 4: remove all of these characters from name "AEIOUYHW" 
        name = Regex.Replace(name, "[AEIOUYHW]", "");

        // step 5: change all characters in name respectively "BPFVCSGJKQXZDTLMNR",111122222222334556 
        name = Regex.Replace(name, "[BPFV]", "1");
        name = Regex.Replace(name, "[CSGJKQXZ]", "2");
        name = Regex.Replace(name, "[DT]", "3");
        name = Regex.Replace(name, "[L]", "4");
        name = Regex.Replace(name, "[MN]", "5");
        name = Regex.Replace(name, "[R]", "6");

        // step 6: if string starts with K then replace to C 
        if (prefix == "K")
        {
            prefix = "C";
        }

        // return pre + name; pad then truncate to ensure a length of 4 
        return (prefix + (name + "000")).Substring(0, 4);
    }

    public static DataTable SearchForAccounts(String searchValue, String searchKey, String location, Int32 startIndex, Int32 noOfRecords)
    {
        Clients.SearchOption key;
        switch (searchKey)
        {
            case "ACCOUNT_NUMBER":
                key = Clients.SearchOption.ACCOUNT_NUMBER;
                break;
            case "ACCOUNT_NAME":
                key = Clients.SearchOption.ACCOUNT_NAME;
                break;
            case "ACCOUNT_MNEMONIC":
                key = Clients.SearchOption.ACCOUNT_MNEMONIC;
                break;
            case "PHONE_NUMBER":
                key = Clients.SearchOption.PHONE_NUMBER;
                break;
            case "ZOASIS_NUMBER":
                key = Clients.SearchOption.ZOASIS_NUMBER;
                break;
            case "ROUTE_STOP":
                key = Clients.SearchOption.ROUTE_STOP;
                break;
            case "SALES_TERRITORY":
                key = Clients.SearchOption.SALES_TERRITORY;
                break;
            default:
                key = Clients.SearchOption.ACCOUNT_NUMBER;
                break;
        }
        if (location.Length > 0)
        {
            return Clients.getClients(searchValue, key, location);
        }
        else
        {
            return Clients.getClients(searchValue, key, startIndex, noOfRecords);
        }
    }
}