using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.Caching;

namespace AtlasIndia.AntechCSM.UI
{
    public struct CacheKeys
    {
        public static String LabLocations = "LabLocations";
        public static String LabLocationsAddOn = "LabLocationsAddOn";
        public static String LabLocationsCI = "LabLocationsCI";
        public static String ChangeRequestType = "ChangeRequestType";
        public static String SalesTerritory = "SalesTerritory";
        public static String ProblemType = "ProblemType";
        public static String ProblemGroup = "ProblemGroup";
        public static String Departments = "Departments";
        public static String DiscountCodes = "DiscountCodes";
        public static String MessageCodes = "MessageCodes";
        public static String ILCStatusTypes = "ILCStatusTypes";
        public static String ILCMessages = "ILCMessages";
        public static String EmailLocations = "EmailLocations";
        public static String EventCategory = "EventCategory";
        public static String EditableEventCategory = "EditableEventCategory";
        public static String Speciality = "Speciality";
        public static String TransferSpeciality = "TransferSpeciality";
        public static String SupplyPrefix = "SupplyPrefix";
        public static String ClientIssueReasons = "ClientIssueReasons";
        public static string ClientIssueCallbackTypes = "ClientIssueCallbackTypes";
    }

    public delegate T GetCallback<T>();

    public class CacheHelper
    {
        static readonly Boolean _defaultEnableCaching = true;
        static readonly TimeSpan _defaultCacheDuration = new TimeSpan(1, 0, 0);

        #region Constructors

        static CacheHelper()
        {
            //
        }

        #endregion Constructors

        #region Properties

        #region Cache

        private static Cache cache
        {
            get { return HttpContext.Current.Cache; }
        }

        #endregion Cache

        #region Lab Locations

        public static DataTable LabLocations
        {
            get
            {
                return Get<DataTable>(CacheKeys.LabLocations, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getLabLocations));
            }
            set
            {
                Set<DataTable>(CacheKeys.LabLocations, value);
            }
        }

        public static DataTable ClientIssueReasons
        {
            get
            {
                return Get<DataTable>(CacheKeys.ClientIssueReasons, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getClientIssueReasons));
            }
            set
            {
                Set<DataTable>(CacheKeys.ClientIssueReasons, value);
            }
        }

        public static DataTable ClientIssueCallbackTypes
        {
            get
            {
                return Get<DataTable>(CacheKeys.ClientIssueCallbackTypes, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getIssueCallbackTypes));
            }
            set
            {
                Set<DataTable>(CacheKeys.ClientIssueCallbackTypes, value);
            }
        }

        public static DataTable LabLocationsAddOn
        {
            get
            {
                return Get<DataTable>(CacheKeys.LabLocationsAddOn, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getLabLocationsAddOn));
            }
            set
            {
                Set<DataTable>(CacheKeys.LabLocationsAddOn, value);
            }
        }

        public static DataTable LabLocationsCI
        {
            get
            {
                return Get<DataTable>(CacheKeys.LabLocationsCI, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getLabLocationsCI));
            }
            set
            {
                Set<DataTable>(CacheKeys.LabLocationsCI, value);
            }
        }

        #endregion Lab Locations

        #region Message Codes

        public static DataTable MessageCodes
        {
            get
            {
                return Get<DataTable>(CacheKeys.MessageCodes, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getMessage));
            }
            set
            {
                Set<DataTable>(CacheKeys.MessageCodes, value);
            }
        }

        #endregion Message Codes

        #region Change Request Type

        public static DataTable ChangeRequestType
        {
            get
            {
                return Get<DataTable>(CacheKeys.ChangeRequestType, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getAddOnVerificationRequestType));
            }
            set
            {
                Set<DataTable>(CacheKeys.ChangeRequestType, value);
            }
        }

        #endregion Change Request Type

        #region Sales Territory

        public static DataTable SalesTerritory
        {
            get
            {
                return Get<DataTable>(CacheKeys.SalesTerritory, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getSalesTerritory));
            }
            set
            {
                Set<DataTable>(CacheKeys.SalesTerritory, value);
            }
        }

        #endregion Sales Territory

        #region Problem Group

        public static DataTable ProblemGroup
        {
            get
            {
                return Get<DataTable>(CacheKeys.ProblemGroup, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getProblemGroups));
            }
            set
            {
                Set<DataTable>(CacheKeys.ProblemGroup, value);
            }
        }

        #endregion Problem Group

        #region Problem Type

        public static DataTable ProblemType
        {
            get
            {
                return Get<DataTable>(CacheKeys.ProblemType, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getProblemTypes));
            }
            set
            {
                Set<DataTable>(CacheKeys.ProblemType, value);
            }
        }

        #endregion Problem Type

        #region Departments

        public static DataTable Departments
        {
            get
            {
                return Get<DataTable>(CacheKeys.Departments, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getDepartments));
            }
            set
            {
                Set<DataTable>(CacheKeys.Departments, value);
            }
        }

        #endregion Departments

        #region Discount Codes

        public static DataTable DiscountCodes
        {
            get
            {
                return Get<DataTable>(CacheKeys.DiscountCodes, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getDiscountCodes));
            }
            set
            {
                Set<DataTable>(CacheKeys.DiscountCodes, value);
            }
        }

        #endregion Discount Codes

        #region InterLab Communication Status Values

        public static DataTable ILCStatusTypes
        {
            get
            {
                return Get<DataTable>(CacheKeys.ILCStatusTypes, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getILCStatusValues));
            }
            set
            {
                Set<DataTable>(CacheKeys.ILCStatusTypes, value);
            }
        }

        #endregion InterLab Communication Status Values

        #region InterLab Communication Messages

        public static DataTable ILCMessages
        {
            get
            {
                return Get<DataTable>(CacheKeys.ILCMessages, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getILCMessages));
            }
            set
            {
                Set<DataTable>(CacheKeys.ILCMessages, value);
            }
        }

        #endregion InterLab Communication Messages

        #region Email Locations

        public static DataTable EmailLocations
        {
            get
            {
                return Get<DataTable>(CacheKeys.EmailLocations, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getEmail));
            }
            set
            {
                Set<DataTable>(CacheKeys.EmailLocations, value);
            }
        }

        #endregion Email Locations

        #region Event Category

        public static DataTable EventCategory
        {
            get
            {
                return Get<DataTable>(CacheKeys.EventCategory, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getEventCategory));
            }
            set
            {
                Set<DataTable>(CacheKeys.EventCategory, value);
            }
        }

        #endregion Event Category

        #region Editable Event Category

        public static DataTable EditableEventCategory
        {
            get
            {
                return Get<DataTable>(CacheKeys.EditableEventCategory, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getEditableEventCategory));
            }
            set
            {
                Set<DataTable>(CacheKeys.EditableEventCategory, value);
            }
        }

        #endregion Event Category

        #region Speciality
        
        public static DataTable Speciality
        {
            get
            {
                return Get<DataTable>(CacheKeys.Speciality, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getSpeciality));
            }
            set
            {
                Set<DataTable>(CacheKeys.Speciality, value);
            }
        }
        
        #endregion Speciality

        #region Speciality

        public static DataTable TransferSpeciality
        {
            get
            {
                return Get<DataTable>(CacheKeys.TransferSpeciality, new GetCallback<DataTable>(AtlasIndia.AntechCSM.functions.getTransferSpeciality));
            }
            set
            {
                Set<DataTable>(CacheKeys.TransferSpeciality, value);
            }
        }

        #endregion Speciality

        #region Supply Prefix

        public static string SupplyOrderPrefix
        {
            get
            {
                return Get<String>(CacheKeys.SupplyPrefix, new GetCallback<String>(AtlasIndia.AntechCSM.functions.getSupplyOrderPrefix));
            }
            set
            {
                Set<String>(CacheKeys.SupplyPrefix, value);
            }
        }
        #endregion Supply Prefix

        #endregion Properties

        #region Methods

        private static T Get<T>(String key, GetCallback<T> callback)
        {
            if (cache != null && cache[key] != null && cache[key].GetType() == typeof(T))
            {
                return (T)cache[key];
            }
            else
            {
                object value = callback();
                Set<T>(key, (T)value);
                return (T)value;
            }
        }

        private static void Set<T>(String key, T value)
        {
            Set<T>(key, value, _defaultCacheDuration);
        }

        private static void Set<T>(String key, T value, TimeSpan cacheDuration)
        {
            cache.Insert(key, value, null, DateTime.Now.Add(cacheDuration), TimeSpan.Zero);
            //cache.Insert(key, value, null, Cache.NoAbsoluteExpiration, TimeSpan.FromMinutes(cacheDuration));
        }

        #endregion Methods
    }
}

