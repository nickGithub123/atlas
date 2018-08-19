using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for DiscountAuthorization
/// </summary>
public class DL_DiscountAuthorization
{
    public DL_DiscountAuthorization()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable DiscountAuthorization(String AuthID)
    {
        DataTable returnDataTable = new DataTable();

        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT CLF_CLNUM AS CLIENTNUMBER,");
        sbSQL.Append(" CLF_CLNAM AS CLIENTNAME,");
        sbSQL.Append(" DAUTH_AccessionDR AS ACCESSION,");
        sbSQL.Append(" %EXTERNAL(DAUTH_InHouseResub) AS INHOUSERESUB,");
        sbSQL.Append(" DAUTH_LocationDR->LABLO_LabName AS LOCATION,");
        sbSQL.Append(" CLF_LabLocationDR->LABLO_LabName AS CLIENTLOCATION,");
        sbSQL.Append(" DAUTH_AuthorizationNumber AS AUTHORIZATIONNUMBER,");
        sbSQL.Append(" DAUTH_DiscountCodeDR AS DISCOUNTCODE,");
        sbSQL.Append(" DAUTH_UserID AS UID,");
        sbSQL.Append(" %EXTERNAL(DAUTH_DateOfAuthorization) AS DATEOFAUTHORIZATION,");
        sbSQL.Append(" DAUTH_Status AS STATUS,");
        sbSQL.Append(" DAUTH_Territory AS TERRITORY,");
        sbSQL.Append(" 'PHONE 1' AS PHONE,");
        sbSQL.Append(" CLF_CLAD1 AS ADDRESS1,");
        sbSQL.Append(" CLF_CLAD2 AS ADDRESS2,");
        sbSQL.Append(" CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sbSQL.Append(" CLF_AGNO AS AUTODIALGR,");
        sbSQL.Append(" Zoasis_num AS ZOASIS,");
        sbSQL.Append(" CLF_CLRTS AS RouteStop,");
        sbSQL.Append(" DAUTH_CLIENTMNEMONICDR AS AccountMnemonic,");
        sbSQL.Append(" DAUTH_AnimalName AS ANIMALNAME,");
        sbSQL.Append(" DAUTH_AddTestCode AS ADDTESTCODE,");
        sbSQL.Append(" DAUTH_DeleteTestCode AS DELTESTCODE,");
        sbSQL.Append(" DAUTH_Amount AS AMOUNT,");
        sbSQL.Append(" DAUTH_Reason AS REASON,");
        sbSQL.Append(" ACC_Sex as SEX,");
        sbSQL.Append(" ACC_Species as SPECIES,");
        sbSQL.Append(" ACC_AgeDob as AGEDOB,");
        sbSQL.Append(" %EXTERNAL(ACC_CollectionDateText) AS COLLECTIONDATE,");
        sbSQL.Append(" ACC_CollectionTimeHours AS COLLECTIONTIME,");
        sbSQL.Append(" ACC_TestsOrderedDisplayString AS TESTORDEREDSTRING,");
        sbSQL.Append(" $$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue");

        sbSQL.Append(" FROM FIN_DiscountAuthorization");
        sbSQL.Append(" LEFT OUTER JOIN ORD_Accession ON DAUTH_AccessionDR=ACC_Accession");
        sbSQL.Append(" LEFT OUTER JOIN CLF_ClientFile ON DAUTH_CLIENTMNEMONICDR=CLF_CLMNE");
        sbSQL.Append(" LEFT OUTER JOIN zoasis ON CLF_CLNUM = zoasis.CLN_RowID");
        sbSQL.Append(" WHERE DAUTH_AuthorizationNumber ='" + AuthID + "'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        returnDataTable = cache.FillCacheDataTable(sbSQL.ToString());
        return returnDataTable;
    }

    public static DataTable getDiscountDetails(String ClientID, String Accession, String IR, String Location, String AuthorizationNumber, String DateSentFrom, String DateSentTo, String Status, String Territory, String DiscountCode, String UID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("DAUTH_ClientNumber AS CLIENTNUMBER,");
        sb.Append("CLF_CLNAM AS CLIENTNAME,");
        sb.Append("DAUTH_AccessionDR AS ACCESSION,");
        sb.Append("%EXTERNAL(DAUTH_InHouseResub) AS INHOUSERESUB,");
        sb.Append("DAUTH_LocationDR->LABLO_LabName AS LOCATION,");
        sb.Append("CLF_LabLocationDR->LABLO_LabName AS CLIENTLOCATION,");
        sb.Append("DAUTH_AuthorizationNumber AS AUTHORIZATIONNUMBER,");
        sb.Append("DAUTH_DiscountCodeDR AS DISCOUNTCODE,");
        sb.Append("$$CO17^XT58(DAUTH_UserID) AS UID,");
        sb.Append("%EXTERNAL(DAUTH_DateOfAuthorization) AS DATEOFAUTHORIZATION,");
        sb.Append("DAUTH_Status AS STATUS,");
        sb.Append("CLF_SalesTerritoryDR->ST_TerritoryCode AS TERRITORY,");
        sb.Append("CLF_CLPHN AS PHONE,");
        sb.Append("CLF_CLAD1 AS ADDRESS1,");
        sb.Append("CLF_CLAD2 AS ADDRESS2,");
        sb.Append("CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sb.Append("CLF_CLADG AS AUTODIALGR,");
        sb.Append("Zoasis_num AS ZOASIS,");
        sb.Append("CLF_CLRTS AS RouteStop,");
        sb.Append("DAUTH_CLIENTMNEMONICDR AS AccountMnemonic,");
        sb.Append("DAUTH_AnimalName AS ANIMALNAME,");
        sb.Append("DAUTH_AddTestCode AS ADDTESTCODE,");
        sb.Append("DAUTH_DeleteTestCode AS DELTESTCODE,");
        sb.Append("DAUTH_Amount AS AMOUNT,");
        sb.Append("DAUTH_Reason AS REASON,");
        sb.Append("ACC_Sex AS SEX,");
        sb.Append("ACC_Species AS SPECIES,");
        sb.Append("ACC_AgeDob AS AGEDOB,");
        sb.Append("%EXTERNAL(ACC_CollectionDateText) AS COLLECTIONDATE,");
        sb.Append("ACC_CollectionTimeHours AS COLLECTIONTIME,");
        sb.Append("ACC_TestsOrderedDisplayString AS TESTORDEREDSTRING,");
        sb.Append("$$GETREVHIST^XT1(CLF_CLMNE) AS ClientRevenue,");
        sb.Append("CLF_IsHot AS ClientIsHot,");
        sb.Append("CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("CLF_IsNew As ClientIsNew ");

        sb.Append("FROM ");
        sb.Append("FIN_DiscountAuthorization LEFT OUTER JOIN ORD_Accession ON DAUTH_AccessionDR = ACC_Accession ");
        sb.Append("LEFT OUTER JOIN CLF_ClientFile Client ON DAUTH_CLIENTMNEMONICDR = CLF_CLMNE ");
        sb.Append("LEFT OUTER JOIN zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");

        sb.Append("WHERE 1=1 ");
        if (ClientID.Length > 0)
        {
            sb.Append("AND DAUTH_ClientNumber ='" + ClientID + "'");
        }
        if (Accession.Length > 0)
        {
            sb.Append(" AND DAUTH_AccessionDR  ='" + Accession + "'");
        }
        if (AuthorizationNumber.Length > 0)
        {
            sb.Append(" AND DAUTH_AuthorizationNumber  ='" + AuthorizationNumber + "'");
        }
        if (IR.Length > 0)
        {
            sb.Append(" AND %EXTERNAL(DAUTH_InHouseResub)  ='" + IR + "'");
        }
        if (Location.Length > 0)
        {
            sb.Append(" AND DAUTH_LocationDR  ='" + Location + "'");
        }
        if (DiscountCode.Length > 0)
        {
            sb.Append(" AND DAUTH_DiscountCodeDR ='" + DiscountCode + "'");
        }
        if (Territory.Length > 0)
        {
            sb.Append(" AND DAUTH_Territory ='" + Territory + "'");
        }
        if (UID.Length > 0)
        {
            sb.Append(" AND DAUTH_UserID ='" + UID + "'");
        }
        if (Status.Length > 0)
        {
            sb.Append(" AND %EXTERNAL(DAUTH_Status) ='" + Status + "'");
        }
        if (DateSentFrom.Length > 0 && DateSentTo.Length > 0)
        {
            sb.Append(" AND DAUTH_DateOfAuthorization >= TO_DATE('" + DateSentFrom + "','MM/DD/YYYY') AND DAUTH_DateOfAuthorization<=TO_DATE('" + DateSentTo + "','MM/DD/YYYY')");
        }
        sb.Append(" ORDER BY DAUTH_DateOfAuthorization DESC,DAUTH_AuthorizationNumber DESC");
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static String insertDiscountAuthorization(String ACCOUNT, String MNEMONIC, String CLIENTNAME, String ACCESSION, String TERRITORY, String ANIMALNAME, String DISCOUNTCODE, String ADDTESTCODE, String DELTESTCODE, String AMOUNT, String IHRESUB, String DATE, String LAB, String REASON, String USER)
    {
        Dictionary<String, String> _discauth = new Dictionary<String, String>();
        _discauth.Add("ACCOUNT", ACCOUNT);
        _discauth.Add("MNEMONIC", MNEMONIC);
        _discauth.Add("CLIENTNAME", CLIENTNAME);
        _discauth.Add("ACCESSION", ACCESSION);
        _discauth.Add("TERRITORY", TERRITORY);
        _discauth.Add("ANIMALNAME", ANIMALNAME);
        _discauth.Add("DISCOUNTCODE", DISCOUNTCODE);
        _discauth.Add("ADDTESTCODE", ADDTESTCODE);
        _discauth.Add("DELTESTCODE", DELTESTCODE);
        _discauth.Add("AMOUNT", AMOUNT);
        _discauth.Add("IHRESUB", IHRESUB);
        _discauth.Add("DATE", DATE);
        _discauth.Add("LAB", LAB);
        _discauth.Add("REASON", REASON);
        _discauth.Add("USER", USER);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_SaveDiscAuth(?,?,?,?,?,?,?,?,?,?,?,?,?,?,?)", _discauth).Value.ToString();
    }
}
