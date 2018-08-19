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

public class Clients
{
    public enum SearchOption
    {
        ACCOUNT_NUMBER = 0,
        ACCOUNT_NAME = 1,
        ACCOUNT_MNEMONIC = 2,
        PHONE_NUMBER = 3,
        ZOASIS_NUMBER = 4,
        ROUTE_STOP = 5,
        SALES_TERRITORY = 6
    }

    public Clients()
    {
    }

    public static DataTable getClients(string searchValue, SearchOption searchKey)
    {
        DataTable returnDataTable = new DataTable();
        switch (searchKey)
        {
            case SearchOption.ACCOUNT_NUMBER:
                returnDataTable = DL_Clients.getClientsByAccountNumber(searchValue);
                break;
            case SearchOption.ACCOUNT_NAME:
                returnDataTable = DL_Clients.getClientsByAccountName(searchValue);
                break;
            case SearchOption.ACCOUNT_MNEMONIC:
                returnDataTable = DL_Clients.getClientsByAccountMnemonic(searchValue);
                break;
            case SearchOption.PHONE_NUMBER:
                returnDataTable = DL_Clients.getClientsByPhoneNumber(searchValue);
                break;
            case SearchOption.ROUTE_STOP:
                returnDataTable = DL_Clients.getClientsByRouteStop(searchValue);
                break;
            case SearchOption.SALES_TERRITORY:
                returnDataTable = DL_Clients.getClientsBySalesTerritory(searchValue);
                break;
            case SearchOption.ZOASIS_NUMBER:
                returnDataTable = DL_Clients.getClientsByZoasisNumber(searchValue);
                break;
        }
        return returnDataTable;
    }

    public static DataTable getClients(string searchValue, SearchOption searchKey, string location)
    {
        DataTable returnDataTable = new DataTable();
        switch (searchKey)
        {
            case SearchOption.ACCOUNT_NUMBER:
                returnDataTable = DL_Clients.getClientsByAccountNumber(searchValue, location);
                break;
            case SearchOption.ACCOUNT_NAME:
                returnDataTable = DL_Clients.getClientsByAccountName(searchValue, location);
                break;
            case SearchOption.ACCOUNT_MNEMONIC:
                returnDataTable = DL_Clients.getClientsByAccountMnemonic(searchValue, location);
                break;
            case SearchOption.PHONE_NUMBER:
                returnDataTable = DL_Clients.getClientsByPhoneNumber(searchValue, location);
                break;
            case SearchOption.ZOASIS_NUMBER:
                returnDataTable = DL_Clients.getClientsByZoasisNumber(searchValue, location);
                break;
        }
        return returnDataTable;
    }

    public static DataTable getClients(string searchValue, SearchOption searchKey, Int32 startIndex, Int32 noOfRecords)
    {
        DataTable returnDataTable = new DataTable();
        switch (searchKey)
        {
            case SearchOption.ACCOUNT_NUMBER:
                returnDataTable = DL_Clients.getClientsByAccountNumber(searchValue);
                break;
            case SearchOption.ACCOUNT_NAME:
                returnDataTable = DL_Clients.getClientsByAccountName(searchValue, startIndex, noOfRecords);
                break;
            case SearchOption.ACCOUNT_MNEMONIC:
                returnDataTable = DL_Clients.getClientsByAccountMnemonic(searchValue);
                break;
            case SearchOption.PHONE_NUMBER:
                returnDataTable = DL_Clients.getClientsByPhoneNumber(searchValue,startIndex,noOfRecords);
                break;
            case SearchOption.ROUTE_STOP:
                returnDataTable = DL_Clients.getClientsByRouteStop(searchValue);
                break;
            case SearchOption.SALES_TERRITORY:
                returnDataTable = DL_Clients.getClientsBySalesTerritory(searchValue);
                break;
            case SearchOption.ZOASIS_NUMBER:
                returnDataTable = DL_Clients.getClientsByZoasisNumber(searchValue);
                break;
        }
        return returnDataTable;
    }

    public static Boolean  validatePriceInqPassword(String priceInqPasswd)
    {
        String returnString = DL_Client.validatePriceInqPassword(SessionHelper.ClientContext, priceInqPasswd, SessionHelper.UserContext.ID);
        if (returnString == "0")
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
