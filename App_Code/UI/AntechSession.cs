using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.SessionState;
using AtlasIndia.AntechCSM;
using System.Collections.Generic;
using Antech.Atlas.Objects;

public struct SessionKeys
{
    public static String ClientContext = "AccountNumber";
    public static String PatientContext = "AccessionNumber";
    public static String UserContext = "LoggedInCSUser";
    public static String TempSessionCountry = "TempSessionCountry";
    public static String IsAuthenticated = "IsAuthenticated";
    public static string CurrentUserToken = "UserToken";
    // Page Level Data
    public static String AccessionDetails2_accession = "AccessionDetails2_accession";
    public static String ClientDetails_client = "ClientDetails_client";
    public static string TestPriceViewed = "TestPriceViewed";
    public static string AddedAccession = "AddedAccession"; //SSM Issue#114138 12Oct2011 AntechCSM-2B SessionKey added to store the Added Accessions
    public static String IsClientPriceInqPasswdValidated = "IsClientPriceInqPasswdValidated";
    public static String TestSearchResult = "TestSearchResult";
    public static String TestSearchComponentResult = "TestSearchComponentResult";
    public static String ProfileSearchResult = "ProfileSearchResult";
    public static String ProfileSearchComponentResult = "ProfileSearchComponentResult";
    public static String EmailDisplayOption = "EmailDisplayOption";
}

public static class SessionHelper
{
    #region Constructors

    static SessionHelper ()
    {
        //
    }

    #endregion Constructors

    #region Properties

    #region Session
    private static HttpSessionState Session
    {
        get { return HttpContext.Current.Session; }
    }
    #endregion Session

    #region Client Context

    public static String ClientContext
    {
        get
        {
            return Get<String>(SessionKeys.ClientContext, string.Empty);
        }
        set
        {
            if (value != string.Empty && value != ClientContext)
            {
                Set<Boolean>(SessionKeys.IsClientPriceInqPasswdValidated, false);
            }
            Set<String>(SessionKeys.ClientContext, value);
        }
    }

    #endregion Client Context

    #region Client Price Inquiry Password Validated

    public static Boolean IsClientPriceInqPasswdValidated
    {
        get
        {
            return Get<Boolean>(SessionKeys.IsClientPriceInqPasswdValidated, false);
        }
        set
        {
            Set<Boolean>(SessionKeys.IsClientPriceInqPasswdValidated, value);
        }
    }
    #endregion Client Price Inquiry Password Validated

    #region Patient Context

    public static String PatientContext
    {
        get
        {
            return Get<String>(SessionKeys.PatientContext, string.Empty);
        }
        set
        {
            Set<String>(SessionKeys.PatientContext, value);
        }
    }

    #endregion Patient Context

    #region Test Price Viewed

    public static Dictionary<String, String> TestPriceViewed
    {
        get
        {
            Dictionary<String, String> retVal = Get<Dictionary<String, String>>(SessionKeys.TestPriceViewed, null);

            if (retVal == null)
            {
                retVal = new Dictionary<string, string>();
                Set<Dictionary<String, String>>(SessionKeys.TestPriceViewed, retVal);
            }
            return retVal;
        }
        set
        {
            Set<Dictionary<String, String>>(SessionKeys.TestPriceViewed, value);
        }
    }

    #endregion Test Price Viewed

    #region User Context

    public static CSUser UserContext
    {
        get
        {
            CSUser user = Get<CSUser>(SessionKeys.UserContext, null);
            if (user == null)
            {
                user = new CSUser(HttpContext.Current.User.Identity.Name);
                Set<CSUser>(SessionKeys.UserContext, user);
            }
            return user;
        }
        set
        {
            Set<CSUser>(SessionKeys.UserContext, value);
        }
    }

    #endregion User Context

    #region User Context

    public static string IsAuthenticated
    {
        get
        {
            return Get<string>(SessionKeys.IsAuthenticated, null);
        }
        set
        {
            Set<string>(SessionKeys.IsAuthenticated, value);
        }
    }

    #endregion User Context

    #region User Token
    public static AOLToken SessionToken
    {
        get
        {
            AOLToken token = Get<AOLToken>(SessionKeys.CurrentUserToken, null);
            return token;
        }
        set
        {
            Set<AOLToken>(SessionKeys.CurrentUserToken, value);
        }
    }
    #endregion User Token

    #region Temperory Session Values - Country

    public static String TempSessionCountry
    {
        get
        {
            return Get<String>(SessionKeys.TempSessionCountry, String.Empty);
        }
        set
        {
            Set<String>(SessionKeys.TempSessionCountry, value);
        }
    }


    #endregion Temperory Session Values - Country

    #region AccessionDetails2 accession

    public static AccessionExtended AccessionDetails2_accession
    {
        get
        {
            return Get<AccessionExtended>(SessionKeys.AccessionDetails2_accession, null);
        }
        set
        {
            Set<AccessionExtended>(SessionKeys.AccessionDetails2_accession, value);
        }
    }

    #endregion AccessionDetails2 accession

    #region ClientDetails client

    public static Client ClientDetails_client
    {
        get
        {
            return Get<Client>(SessionKeys.ClientDetails_client, null);
        }
        set
        {
            Set<Client>(SessionKeys.ClientDetails_client, value);
        }
    }

    #endregion ClientDetails client

    #region ConsultRecorder
    //+SSM Issue#114138 13Oct2011 AntechCSM-2B SessionKey added to store the Added Accessions
    public static DataTable AddedAccessions
    {
        get
        {
            return Get<DataTable>(SessionKeys.AddedAccession, null);
        }
        set
        {
            Set<DataTable>(SessionKeys.AddedAccession, value);
        }
    }
    //-SSM
    #endregion ConsultRecorder

    #region testSearchResult
    public static DataTable TestSearchResults
    {
        get
        {
            return Get<DataTable>(SessionKeys.TestSearchResult, null);
        }
        set
        {
            Set<DataTable>(SessionKeys.TestSearchResult, value);
        }
    }
    #endregion testSearchResult

    #region testSearchComponentResult
    public static DataTable TestSearchComponentResults
    {
        get
        {
            return Get<DataTable>(SessionKeys.TestSearchComponentResult, null);
        }
        set
        {
            Set<DataTable>(SessionKeys.TestSearchComponentResult, value);
        }
    }
    #endregion testSearchComponentResult

    #region profileSearchResult
    public static DataTable ProfileSearchResults
    {
        get
        {
            return Get<DataTable>(SessionKeys.ProfileSearchResult, null);
        }
        set
        {
            Set<DataTable>(SessionKeys.ProfileSearchResult, value);
        }
    }
    #endregion profileSearchResult

    #region profileSearchComponentResult
    public static DataTable ProfileSearchComponentResults
    {
        get
        {
            return Get<DataTable>(SessionKeys.ProfileSearchComponentResult, null);
        }
        set
        {
            Set<DataTable>(SessionKeys.ProfileSearchComponentResult, value);
        }
    }
    #endregion profileSearchResult

    #region EmailDisplayOption
    public static string EmailDisplayOption
    {
        get
        {
            return Get<string>(SessionKeys.EmailDisplayOption, "");
        }
        set
        {
            Set<string>(SessionKeys.EmailDisplayOption, value);
        }
    }
    #endregion EmailDisplayOption
    #endregion Properties

    #region Methods

    public static void Clear()
    {
        HttpContext.Current.Session.Clear();
        HttpContext.Current.Session.Abandon();  //SP Custom Authentication
    }

    private static T Get<T>(String key, T defaultValue)
    {
        if (Session != null && Session[key] != null && Session[key].GetType() == typeof(T))
        {
            return (T)Session[key];
        }
        return defaultValue;
    }

    private static void Set<T>(String key, T value)
    {
        if (Session != null)
        {
            Session[key] = value;
        }
    }

    #endregion
}