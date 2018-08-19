using System;
using System.Data;
using System.Configuration;

namespace AtlasIndia.AntechCSM
{
    /// <summary>
    /// User [Customer Support Representative]
    /// </summary>
    public class CSUser
    {
        #region Constructors

        public CSUser(String userID, String password, String passwordExpiryPeriod)
        {
            this._ID = userID;
            string retVal = DL_User.validateUserLogin(userID, password, passwordExpiryPeriod);
            string[] arrMsg = retVal.Split(new char[] { '^' });
            string strCode = arrMsg[0];
            if (strCode == "0")
            {
                string strMsg = arrMsg[1];
                string[] arrData = strMsg.Split(new char[] { '~' });
                this._isValid = true;
                this._ID = arrData[0];
                this._name = arrData[1];
                this._newMessageCount = 0;
                this._labLocation = arrData[2];
                this._isCSUser = (arrData[3] == "Y");
                this._roleID = "TestData_Role1";
                this._isSupervisor = (arrData[4] == "Y");
                this._consultantID = arrData[5];
                this._canChangeClientPassword = (arrData[6] == "Y");
                this._isLabC = (arrData[7] == "Y");
                this._userEmail = arrData[8];
                this._userIsActive = (arrData[9]=="Y");
                this._userDispName = arrData[10];
                this._isSuperUser = (arrData[11] == "Y");
                this._returnMessage = "Successful";
            }
            else
            {
                this._isValid = false;
                this._returnMessage = retVal;
            }
        }

        public CSUser(String userID, String password)
        {
            this._ID = userID;
            DataTable userDetails = DL_User.getUserDetails(userID, password);
            if (userDetails == null)
            {
                this._isValid = false;
            }
            else if (userDetails.Rows.Count < 1)
            {
                this._isValid = false;
            }
            else if (userDetails.Rows.Count > 1)
            {
                ///TODO:log it.
                this._isValid = false;
            }
            else
            {
                this._isValid = true;
                DataRow dr = userDetails.Rows[0];
                this._ID = dr["UserID"].ToString();
                this._name = dr["Name"].ToString();
                this._newMessageCount = 0;
                this._labLocation = dr["LabID"].ToString();
                this._isCSUser = (dr["IsCSUser"].ToString() == "Y");
                this._roleID = "TestData_Role1";
                this._isSupervisor = (dr["IsSupervisor"].ToString() == "Y");
                this._consultantID = dr["ConsultantID"].ToString();
                this._canChangeClientPassword = (dr["CanChangeClientPassword"].ToString() == "Y");
                this._isLabC = (dr["IsLabC"].ToString() == "Y");
                this._userEmail = dr["UserEmail"].ToString();
                this._userIsActive = (dr["IsActive"].ToString() == "Y");
                this._userDispName= dr["UserDispName"].ToString();
                this._isSuperUser = (dr["IsSuperUser"].ToString() == "Y");
            }
        }

        public CSUser(string userID)
        {
            this._ID = userID;
            DataTable userDetails = DL_User.getUserDetails(userID);
            if (userDetails == null)
            {
                this._isValid = false;
            }
            else if (userDetails.Rows.Count < 1)
            {
                this._isValid = false;
            }
            else if (userDetails.Rows.Count > 1)
            {
                ///TODO:logit.
                this._isValid = false;
            }
            else
            {
                this._isValid = true;
                DataRow dr = userDetails.Rows[0];
                this._ID = dr["UserID"].ToString();
                this._name = dr["Name"].ToString();
                this._newMessageCount = 0;
                this._labLocation = dr["LabID"].ToString();
                this._isCSUser = (dr["IsCSUser"].ToString() == "Y");
                this._roleID = "TestData_Role1";
                this._isSupervisor = (dr["IsSupervisor"].ToString() == "Y");
                this._userEmail = dr["UserEmail"].ToString();
                this._userIsActive = (dr["IsActive"].ToString() == "Y");
                this._userDispName = dr["UserDispName"].ToString();
                this._isSuperUser = (dr["IsSuperUser"].ToString() == "Y");
            }
        }

        #endregion Constructors

        #region Properties

        #region User Validity Information

        private Boolean _isValid;
        public Boolean IsValid
        {
            get { return _isValid; }
        }

        #endregion User Validity Information

        #region canChangeClientPassword
        private bool _canChangeClientPassword;

        public bool canChangeClientPassword
        {
            get { return _canChangeClientPassword; }

        }
            
        #endregion canChangeClientPassword

        #region User ID

        private String _ID;
        public String ID
        {
            get { return _ID; }
        }

        #endregion User ID

        #region Consultant

        private String _consultantID;
        public String ConsultantID
        {
            get { return _consultantID; }
        }

        public bool IsConsultant
        {
            get
            {
                if (this._consultantID.Trim().Length == 0)
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }

        #endregion Consultant

        #region User Name

        private String _name;
        public String Name
        {
            get { return _name; }
        }

        #endregion User Name

        #region Number of New Message

        private Int32 _newMessageCount;
        public Int32 NewMessageCount
        {
            get { return _newMessageCount; }
        }

        #endregion Number of New Message

        #region Role ID

        private String _roleID;
        public String RoleID
        {
            get { return _roleID; }
        }

        #endregion Role ID

        #region Lab Location

        private String _labLocation;
        public String LabLocation
        {
            get { return _labLocation; }
        }

        #endregion Lab Location

        #region User Email

        private String _userEmail;
        public String UserEmail
        {
            get { return _userEmail; }
        }

        #endregion User Email

        #region User IsActive

        private bool _userIsActive;
        public bool IsActive
        {
            get { return _userIsActive; }
        }
        #endregion User IsActive

        #region User Display Name
        private string _userDispName;
        public string UserDispName
        {
            get { return _userDispName; }
        }
        #endregion User Display Name

        #region Is Super User
        private Boolean _isSuperUser;
        public Boolean IsSuperUser
        {
            get { return _isSuperUser; }
        }
        #endregion Is Super User

        #region Is Client Services User

        private Boolean _isCSUser;
        public Boolean IsCSUser
        {
            get { return _isCSUser; }
        }

        #endregion Is Client Services User

        #region Is Superviser
        private bool _isSupervisor;
        /// <summary>
        /// This user has privilege as supervisor
        /// </summary>
        public bool IsSupervisor
        {
            get{return this._isSupervisor;}
            set{this._isSupervisor= value;}
        }
        #endregion

        #region Is LabC
        private bool _isLabC;
        /// <summary>
        /// This user has privilege as LabC
        /// </summary>
        public bool IsLabC
        {
            get { return this._isLabC; }
        }
        #endregion

        #region Return Message for User Login
        private string _returnMessage;
        /// <summary>
        /// Return Message for User Login
        /// </summary>
        public string ReturnMessage
        {
            get { return this._returnMessage; }
        }
        #endregion 

        #endregion Properties

        #region Methods
        public static void getUserAlerts(String userId, out int unreadMailCount, out bool hasPendingCallBack)
        {
            string strUserAlerts = DL_User.getUserAlerts(userId);
            string[] arrAlerts = strUserAlerts.Split(new char[] { Convert.ToChar(1) });

            unreadMailCount = Convert.ToInt32(arrAlerts[0]);
            hasPendingCallBack = (arrAlerts[1] == "1" ? true : false);
        }

        public static string changeUserPassword(string userID, string oldPassword, string newPassword)
        {
            return DL_User.changeUserPassword(userID, oldPassword, newPassword);
        }
        #endregion Methods
    }
}   