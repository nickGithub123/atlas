using System;
using System.Data;
using System.Configuration;

public class Catalog
{
    public enum SearchOption
    {
        UNIT_CODE = 0,
        UNIT_NAME = 1,
        COMBINED = 2
    }

    public enum AndOrSearch
    {
        AND = 0,
        OR = 1
    }

    public Catalog()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable getUnits(String searchValue, SearchOption searchKey)
    {
        DataTable returnDataTable;
        switch (searchKey)
        {
            case SearchOption.UNIT_CODE:
                returnDataTable = DL_Catalog.getUnitsByCode(searchValue);
                break;
            case SearchOption.UNIT_NAME:
                returnDataTable = DL_Catalog.getUnitsByName(searchValue);
                break;
            case SearchOption.COMBINED:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(searchValue);
                break;
            default:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(searchValue);
                break;
        }
        return returnDataTable;
    }

    public static DataTable getUnits(String searchValue, SearchOption searchKey, String clientCountry)
    {
        DataTable returnDataTable;
        switch (searchKey)
        {
            case SearchOption.UNIT_CODE:
                returnDataTable = DL_Catalog.getUnitsByCode(searchValue, clientCountry);
                break;
            case SearchOption.UNIT_NAME:
                returnDataTable = DL_Catalog.getUnitsByName(searchValue, clientCountry);
                break;
            case SearchOption.COMBINED:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(searchValue, clientCountry);
                break;
            default:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(searchValue, clientCountry);
                break;
        }
        return returnDataTable;
    }
    public static DataTable getUnits(string account,String searchValue, String showReplacedCode)
    {
    DataTable returnDataTable = DL_Catalog.getUnitsBySearchOption(account, searchValue, showReplacedCode);
        return returnDataTable;
    }
    public static DataTable getUnits(String searchValue, SearchOption searchKey, String clientCountry, int startIndex, int noOfRecords)
    {
        DataTable returnDataTable;
        switch (searchKey)
        {
            case SearchOption.UNIT_CODE:
                returnDataTable = DL_Catalog.getUnitsByCode(searchValue, clientCountry, startIndex, noOfRecords);
                break;
            case SearchOption.UNIT_NAME:
                returnDataTable = DL_Catalog.getUnitsByName(searchValue, clientCountry, startIndex, noOfRecords);
                break;
            case SearchOption.COMBINED:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(searchValue, startIndex, noOfRecords, clientCountry);
                break;
            default:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(searchValue, startIndex, noOfRecords, clientCountry);
                break;
        }
        return returnDataTable;
    }

    public static DataTable getProfiles(String[] searchValues, AndOrSearch searchKey, String clientCountry)
    {
        switch (searchKey)
        {
            case AndOrSearch.OR:
                return DL_Catalog.getProfilesByCodeUsingOR(searchValues,clientCountry);
            case AndOrSearch.AND:
                return DL_Catalog.getProfilesByCodeUsingAND(searchValues,clientCountry);
        }
        return null;
    }

    public static DataTable getDetailedUnits(String clientID, String searchValue, SearchOption searchKey, Int32 startIndex, Int32 noOfRecords)
    {
        DataTable returnDataTable = new DataTable();
        switch (searchKey)
        {
            case SearchOption.UNIT_CODE:
                returnDataTable = DL_Catalog.getDetailedUnitsByCode(clientID, searchValue);
                break;
            case SearchOption.UNIT_NAME:
                returnDataTable = DL_Catalog.getDetailedUnitsByName(clientID, searchValue, startIndex, noOfRecords);
                break;
            case SearchOption.COMBINED:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(clientID, searchValue, startIndex, noOfRecords);
                break;
            default:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(clientID, searchValue, startIndex, noOfRecords);
                break;
        }
        return returnDataTable;
    }

    public static DataTable getDetailedUnits(String clientID, String searchValue, SearchOption searchKey, Int32 startIndex, Int32 noOfRecords, String clientCountry)
    {
        DataTable returnDataTable = new DataTable();
        switch (searchKey)
        {
            case SearchOption.UNIT_CODE:
                returnDataTable = DL_Catalog.getDetailedUnitsByCode(clientID, searchValue, clientCountry);
                break;
            case SearchOption.UNIT_NAME:
                returnDataTable = DL_Catalog.getDetailedUnitsByName(clientID, searchValue, startIndex, noOfRecords, clientCountry);
                break;
            case SearchOption.COMBINED:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(clientID, searchValue, startIndex, noOfRecords,clientCountry);
                break;
            default:
                returnDataTable = DL_Catalog.getUnitsByCombinedIndex(clientID, searchValue, startIndex, noOfRecords,clientCountry);
                break;
        }
        return returnDataTable;
    }

    public static DataTable getDetailedUnits(String clientID, String searchValue, String showReplacedCode)
    {
    return DL_Catalog.getUnitsdetailsBySearchOption(clientID, searchValue, showReplacedCode);
    }
}
