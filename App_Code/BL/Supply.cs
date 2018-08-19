using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Supply
/// </summary>
public class Supply
{
	public Supply()
	{
		//
		// TODO: Add constructor logic here
		//
    }

    public Supply(string OrdNum)
    {
        if (OrdNum.Length == 0)
        {
            return;
        }

        DataTable dtSupply = DL_Supply.getSupplyOrderDetails(OrdNum);
        if (dtSupply != null && dtSupply.Rows.Count > 0)
        {
            this.IsValid = true;
            this.OrderNumber = dtSupply.Rows[0]["OrderNo"].ToString();
            this.AccountNumber = dtSupply.Rows[0]["AccountNumber"].ToString();
            this.OrderDate = dtSupply.Rows[0]["OrdDate"].ToString();
            this.CallerName = dtSupply.Rows[0]["Caller"].ToString();
            this.OrderType = dtSupply.Rows[0]["OrdType"].ToString();
            this.ExpressShipping = dtSupply.Rows[0]["ExpressShipping"].ToString();
            this.TrackingInfo = dtSupply.Rows[0]["TrkInfo"].ToString();
            this.Notes = dtSupply.Rows[0]["Notes"].ToString();
            this.Requested = getOrderItemDetails(OrdNum);
        }
    }

    #region properties

    public string OrderNumber
    {
        get;
        set;
    }
    public string ExpressShipping
    {
        get;
        set;
    }

    public string OrderDate
    {
        get;
        set;
    }
    public string OrderType
    {
        get;
        set;
    }
    public string CallerName
    {
        get;
        set;
    }
    public string Notes
    {
        get;
        set;
    }

    public string AccountNumber
    {
        get;
        set;
    }

    public string TrackingInfo
    {
        get;
        set;
    }

    public Boolean IsValid
    {
        get;
        set;
    }

    public DataTable Requested
    {
        get;
        set;
    }

    #endregion properties

    public void getSupplyDetails()
    {
        //TODO:
    }

    public static DataTable getSupplySearchResult(string ordNo, string clientID, string expressShipping, string dateFrom, string dateTo, string ordType 
        , string status)
    {
        return DL_Supply.getSupplySearchResult(ordNo, clientID, expressShipping, dateFrom, dateTo, ordType, AtlasIndia.AntechCSM.UI.CacheHelper.SupplyOrderPrefix, status);
    }

    public static DataTable getOrderItemDetails(string ordID)
    {
        return DL_Supply.getOrderItemDetails(ordID);
    }

    public static DataTable getPrevoiusSupplyOrdersByAccount(string accountNo)
    {
        return DL_Supply.getPrevoiusSupplyOrdersByAccount(accountNo, AtlasIndia.AntechCSM.UI.CacheHelper.SupplyOrderPrefix);
    }

    public static string saveSupplyOrder(string strAccount, string strCallerName, string strExpressShipping, string strOrderDate, string strOrderType, string strNotes, string strItemsString)
    {
        string strMailSystem =  System.Configuration.ConfigurationSettings.AppSettings["MAILSYSTEM"];
        string strDir = System.Configuration.ConfigurationSettings.AppSettings["ANTECH_SUPPLY_HL7DIR"];
        string retVal = DL_Supply.saveSupplyOrder(strAccount, strCallerName, strExpressShipping, strOrderDate, strOrderType, strNotes, strItemsString
            , SessionHelper.UserContext.ID, strMailSystem, strDir);
        return retVal;
    }

    public static DataTable getUCCategory()
    {
        return DL_Supply.getUCCategory();
    }

    public static DataTable getUnitCodeByCategory(string strCategory)
    {
        return DL_Supply.getUnitCodeByCategory(strCategory);
    }

    public static DataTable getUnitCodeDetails(string strUnitCode)
    {
        return DL_Supply.getUnitCodeDetails(strUnitCode);
    }
}
