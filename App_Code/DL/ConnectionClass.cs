using System;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.Common;
using System.Configuration;
using System.Collections;
using System.Data.Odbc;
using System.IO;

namespace CACHEDAL
{
    public class ConnectionClass
    {
        private OdbcConnection oConn;
        private OdbcCommand oCommand = new OdbcCommand();
        private OdbcDataReader oReader;
        private OdbcDataAdapter oDataAdapder = new OdbcDataAdapter();

        //// Read Connection String
        private string ReadConnectionString()
        {
            string conString = string.Empty;
            try
            {
                conString = ConfigurationManager.ConnectionStrings["AntechODBC"].ConnectionString;
            }
            catch (Exception ex)
            {
                string errorMessage = ex.Message;
            }
            return conString;
        }

        //// Open Connection
        private void OpenCacheConnection()
        {
            try
            {
                oConn = new OdbcConnection(ReadConnectionString());
                if (oConn.State != ConnectionState.Closed)
                {
                    oConn.Close();
                }
                oConn.Open();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
        }

        //// Close Connection 
        private void CloseCacheConnection()
        {
            try
            {
                if (oConn.State != ConnectionState.Closed)
                {
                    oConn.Close();
                    oConn.Dispose();
                    oConn = null;
                }
            }
            catch (Exception Ex)
            {
                String errorMessage = Ex.Message;
            }
        }

        //// ODBC Data Reader
        public OdbcDataReader ReaderData(string mySelectQuery)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                oCommand = new OdbcCommand(mySelectQuery, oConn);
                oCommand.CommandTimeout = 90;
                oReader = oCommand.ExecuteReader();
                return oReader;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }

        // Insert Routine
        public int Insert(string myInsertQuery)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                oCommand = new OdbcCommand(myInsertQuery, oConn);
                oCommand.CommandTimeout = 90;
                int rowsAffected = 0;
                rowsAffected = oCommand.ExecuteNonQuery();
                return rowsAffected;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }

        //Execute routine
        public object CacheExScalar(string myInsertQuery)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                oCommand = new OdbcCommand(myInsertQuery, oConn);
                oCommand.CommandTimeout = 90;
                return oCommand.ExecuteScalar();
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }

        //// Fill DataTable Using Stored Procedure
        public DataTable FillCacheSPDataTable(string MySelectQuery)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                oCommand = new OdbcCommand(MySelectQuery, oConn);
                DataTable MyDataTable = new DataTable();
                oDataAdapder = new OdbcDataAdapter(oCommand);
                oCommand.CommandTimeout = 300;
                oCommand.CommandType = CommandType.StoredProcedure;
                oDataAdapder.Fill(MyDataTable);
                return MyDataTable;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oDataAdapder = null;
                oCommand = null;
                CloseCacheConnection();
            }
        }

        //// Override for reset search option.
        public DataTable FillCacheDataTable(string MySelectQuery, String PageName, Int32 TimeOut, out Boolean isError)
        {
            isError = false;
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                setTimeout(PageName, TimeOut.ToString());

                oCommand = new OdbcCommand(MySelectQuery, oConn);
                DataTable MyDataTable = new DataTable();
                oDataAdapder = new OdbcDataAdapter(oCommand);
                oCommand.CommandTimeout = 300;
                oCommand.CommandType = CommandType.Text;
                oDataAdapder.Fill(MyDataTable);
                return MyDataTable;
            }
            catch (Exception Ex)
            {
                //Request timed out due to user timeout
                //Socket: Connection reset by peer (due to timeout or reboot)
                //Message sequencing error
                //Object reference not set to an instance of an object
                //Connection ERROR
                // UNKNOWN MSG
                if ((Ex.Message.IndexOf("Request timed out due to user timeout") != -1) || (Ex.Message.IndexOf("Socket: Connection reset by peer (due to timeout or reboot)") != -1) || (Ex.Message.IndexOf("Message sequencing error")) != -1 || Ex.Message.IndexOf("Object reference not set to an instance of an object") != -1 || Ex.Message.IndexOf("Connection ERROR") != -1 || Ex.Message.IndexOf("UNKNOWN MSG") != -1)
                {
                    isError = true;
                }
            }
            finally
            {
                oDataAdapder = null;
                oCommand = null;
                CloseCacheConnection();
            }            
            return null;
        }

        //// Fill DataTable with Paging Parameters
        public DataTable FillCacheDataTable(String MySelectQuery, Int32 startIndex, Int32 noOfRecords)
        {
            String SQL_WR = System.Configuration.ConfigurationManager.AppSettings["SQL_WR"];
            //SQL_WR = "2";
            String fileToRead = System.Web.HttpContext.Current.Server.MapPath("SQL_ReadIn.txt");
            String fileToWrite = System.Web.HttpContext.Current.Server.MapPath("SQL_WriteOut.txt");
            switch (SQL_WR)
            {
                case "0":
                    // Do Nothing
                    break;
                case "1":
                    MySelectQuery = AtlasIndia.AntechCSM.TextFileReadWrite.ReadFile(fileToRead);
                    break;
                case "2":
                    AtlasIndia.AntechCSM.TextFileReadWrite.WriteFile(fileToWrite, MySelectQuery);
                    break;
                case "3":
                    AtlasIndia.AntechCSM.TextFileReadWrite.WriteFile(fileToWrite, MySelectQuery);
                    String content = AtlasIndia.AntechCSM.TextFileReadWrite.ReadFile(fileToRead);
                    if (content.Length > 0)
                    {
                        MySelectQuery = content;
                    }
                    break;
                default:
                    // Do Nothing
                    break;
            }
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                //oCommand = new OdbcCommand(MySelectQuery, oConn);
                DataTable MyDataTable = new DataTable();
                oDataAdapder = new OdbcDataAdapter(MySelectQuery, oConn);
                //oCommand.CommandTimeout = 300;
                //oCommand.CommandType = CommandType.Text;
                oDataAdapder.Fill(startIndex, noOfRecords, MyDataTable);
                return MyDataTable;
            }
            catch (Exception Ex)
            {
                String errorMessage = Ex.Message;
            }
            finally
            {
                //oCommand = null;
                oDataAdapder = null;
                CloseCacheConnection();
            }
            return null;
        }

        //// Override for reset search option.
        public DataTable FillCacheDataTable(String MySelectQuery, Int32 startIndex, Int32 noOfRecords, String PageName, Int32 TimeOut, out Boolean isError)
        {
            isError = false;
            String SQL_WR = System.Configuration.ConfigurationManager.AppSettings["SQL_WR"];
            String fileToRead = System.Web.HttpContext.Current.Server.MapPath("SQL_ReadIn.txt");
            String fileToWrite = System.Web.HttpContext.Current.Server.MapPath("SQL_WriteOut.txt");
            switch (SQL_WR)
            {
                case "0":
                    // Do Nothing
                    break;
                case "1":
                    MySelectQuery = AtlasIndia.AntechCSM.TextFileReadWrite.ReadFile(fileToRead);
                    break;
                case "2":
                    AtlasIndia.AntechCSM.TextFileReadWrite.WriteFile(fileToWrite, MySelectQuery);
                    break;
                case "3":
                    AtlasIndia.AntechCSM.TextFileReadWrite.WriteFile(fileToWrite, MySelectQuery);
                    String content = AtlasIndia.AntechCSM.TextFileReadWrite.ReadFile(fileToRead);
                    if (content.Length > 0)
                    {
                        MySelectQuery = content;
                    }
                    break;
                default:
                    // Do Nothing
                    break;
            }
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                setTimeout(PageName, TimeOut.ToString());
                DataTable MyDataTable = new DataTable();
                oDataAdapder = new OdbcDataAdapter(MySelectQuery, oConn);
                oDataAdapder.Fill(startIndex, noOfRecords, MyDataTable);
                return MyDataTable;
            }
            catch (Exception Ex)
            {
                //Request timed out due to user timeout
                //Socket: Connection reset by peer (due to timeout or reboot)
                //Message sequencing error
                //Object reference not set to an instance of an object
                //Connection ERROR
                if ((Ex.Message.IndexOf("Request timed out due to user timeout") != -1) || (Ex.Message.IndexOf("Socket: Connection reset by peer (due to timeout or reboot)") != -1) || (Ex.Message.IndexOf("Message sequencing error")) != -1 || Ex.Message.IndexOf("Object reference not set to an instance of an object") != -1 || Ex.Message.IndexOf("Connection ERROR") != -1 || Ex.Message.IndexOf("UNKNOWN MSG") != -1)
                {
                    isError = true;
                }
            }
            finally
            {
                oDataAdapder = null;
                CloseCacheConnection();
            }            
            return null;
        }

        //// Note a generalized method
        public void setTimeout(String pageName, String timeout) // Uses existing open connection
        {
            oCommand = new OdbcCommand("?=call SP2_SetTimeout(?,?)", oConn);
            oDataAdapder = new OdbcDataAdapter(oCommand);
            oCommand.CommandType = CommandType.StoredProcedure;

            OdbcParameter returnValue = new OdbcParameter("return_value", OdbcType.VarChar, 255);
            returnValue.Direction = ParameterDirection.ReturnValue;
            oCommand.Parameters.Add(returnValue);

            OdbcParameter pPageName = new OdbcParameter("PageName", OdbcType.VarChar);
            pPageName.Direction = ParameterDirection.Input;
            pPageName.Value = pageName;
            if (pageName.Length == 0)
            {
                pPageName.Value = DBNull.Value;
            }
            oCommand.Parameters.Add(pPageName);

            OdbcParameter pTimeout = new OdbcParameter("Timeout", OdbcType.VarChar);
            pTimeout.Direction = ParameterDirection.Input;
            pTimeout.Value = timeout;
            if (timeout.Length == 0)
            {
                pTimeout.Value = DBNull.Value;
            }
            oCommand.Parameters.Add(pTimeout);
            int irows = oCommand.ExecuteNonQuery();
        }

        //// Fill DataTable
        public DataTable FillCacheDataTable(string MySelectQuery)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                oCommand = new OdbcCommand(MySelectQuery, oConn);
                DataTable MyDataTable = new DataTable();
                oDataAdapder = new OdbcDataAdapter(oCommand);
                oCommand.CommandTimeout = 300;
                oCommand.CommandType = CommandType.Text;
                oDataAdapder.Fill(MyDataTable);
                return MyDataTable;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oDataAdapder = null;
                oCommand = null;
                CloseCacheConnection();
            }
        }

        // AM AntechCSM 1.0.67.0 03/10/2009
        public DataTable ErrorTable(String ErrorMsg)
        {
            DataTable ErrTable = new DataTable("ErrorMsgTbl");
            ErrTable.Columns.Add("ExceptionVal");
            DataRow ErrRow = ErrTable.NewRow();
            ErrRow["ExceptionVal"] = ErrorMsg;
            ErrTable.Rows.Add(ErrRow);
            return ErrTable;
        }

        //// Fill Dataset
        public DataSet FillCacheDataSet(String MySelectQuery)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                oCommand = new OdbcCommand(MySelectQuery, oConn);
                DataSet MyDataSet = new DataSet();
                oDataAdapder = new OdbcDataAdapter(oCommand);
                oCommand.CommandTimeout = 300;
                oCommand.CommandType = CommandType.Text;
                oDataAdapder.Fill(MyDataSet, "myTable");
                return MyDataSet;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oDataAdapder = null;
                oCommand = null;
                CloseCacheConnection();
            }
        }

        public DataTable FillCacheDataSet(string ProcedureName, Dictionary<String, String> _parameterList)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                oCommand = new OdbcCommand();
                oCommand.Connection = oConn;
                oCommand.CommandText = ProcedureName;
                oCommand.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnValue = new OdbcParameter("return_value", OdbcType.VarChar, 32000);
                returnValue.Direction = ParameterDirection.ReturnValue;
                oCommand.Parameters.Add(returnValue);

                foreach (string key in _parameterList.Keys)
                {
                    OdbcParameter inputValue = new OdbcParameter(Convert.ToString(key), OdbcType.VarChar);
                    inputValue.Direction = ParameterDirection.Input;
                    inputValue.Value = _parameterList[key];
                    if (inputValue.Value.ToString().Length == 0)
                    {
                        inputValue.Value = DBNull.Value;
                    }
                    oCommand.Parameters.Add(inputValue);
                }
                _parameterList.Clear();
                DataTable table = new DataTable();
                DataSet ds = new DataSet();
                oDataAdapder = new OdbcDataAdapter(oCommand);
                oDataAdapder.Fill(ds, "CallbackNoteTable");

                if (ds.Tables.Count > 0)
                {
                    table = ds.Tables[0];
                }
                else
                {
                    table = null;
                }
                return table;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }
        // Transaction
        public bool Transaction(string sqlStatement)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }

                OdbcTransaction transInsert = oConn.BeginTransaction();
                try
                {
                    oCommand = new OdbcCommand(sqlStatement, oConn, transInsert);
                    oCommand.CommandText = sqlStatement;
                    oCommand.ExecuteNonQuery();
                    transInsert.Commit();
                    return true;
                }
                catch
                {
                    try
                    {
                        transInsert.Rollback();
                        return false;
                    }
                    catch (Exception ex)
                    {
                        throw new Exception("Error Source: " + ex.ToString());
                    }
                    finally
                    {
                        oCommand = null;
                        transInsert = null;
                        CloseCacheConnection();
                    }
                }
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }

        // Stored Procedure [No Input Parameters, Single Output Parameter]
        public OdbcParameter StoredProcedure(String ProcedureName)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                oCommand = new OdbcCommand();
                oCommand.Connection = oConn;
                oCommand.CommandText = ProcedureName;
                oCommand.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnValue = new OdbcParameter("return_value", OdbcType.VarChar, 255);
                returnValue.Direction = ParameterDirection.ReturnValue;
                oCommand.Parameters.Add(returnValue);

                oCommand.ExecuteNonQuery();
                return returnValue;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }

        public void StoredProcedure(string ProcedureName, string Accession, string DateEntered, string Department, string LabLocation, string ReasonForDeletion, string Comments, string TestsToDeleteString, string userID)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                DataSet ds = new DataSet();
                oDataAdapder = new OdbcDataAdapter();
                oCommand = new OdbcCommand();
                oCommand.Connection = oConn;
                oCommand.CommandText = ProcedureName;
                oCommand.CommandType = CommandType.StoredProcedure;

                OdbcParameter inputValue1 = new OdbcParameter("Accession", OdbcType.VarChar);
                inputValue1.Direction = ParameterDirection.Input;
                inputValue1.Value = Accession;
                oCommand.Parameters.Add(inputValue1);

                OdbcParameter inputValue2 = new OdbcParameter("DateEntered", OdbcType.VarChar);
                inputValue2.Direction = ParameterDirection.Input;
                inputValue2.Value = DateEntered;
                oCommand.Parameters.Add(inputValue2);

                OdbcParameter inputValue3 = new OdbcParameter("Department", OdbcType.VarChar);
                inputValue3.Direction = ParameterDirection.Input;
                inputValue3.Value = Department;
                oCommand.Parameters.Add(inputValue3);

                OdbcParameter inputValue4 = new OdbcParameter("LabLocation", OdbcType.VarChar);
                inputValue4.Direction = ParameterDirection.Input;
                inputValue4.Value = LabLocation;
                oCommand.Parameters.Add(inputValue4);

                OdbcParameter inputValue5 = new OdbcParameter("ReasonForDeletion", OdbcType.VarChar);
                inputValue5.Direction = ParameterDirection.Input;
                inputValue5.Value = ReasonForDeletion;
                oCommand.Parameters.Add(inputValue5);

                OdbcParameter inputValue6 = new OdbcParameter("Comments", OdbcType.VarChar);
                inputValue6.Direction = ParameterDirection.Input;
                inputValue6.Value = Comments;
                oCommand.Parameters.Add(inputValue6);

                OdbcParameter inputValue7 = new OdbcParameter("TestsToDeleteString", OdbcType.VarChar);
                inputValue7.Direction = ParameterDirection.Input;
                inputValue7.Value = TestsToDeleteString;
                oCommand.Parameters.Add(inputValue7);

                OdbcParameter inputValue8 = new OdbcParameter("UserID", OdbcType.VarChar);
                inputValue8.Direction = ParameterDirection.Input;
                inputValue8.Value = userID;
                oCommand.Parameters.Add(inputValue8);

                int irows = oCommand.ExecuteNonQuery();

            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }

        //AM Issue#37331 04/19/2008 - Generalized StoredProcedure for insertion
        public OdbcParameter StoredProcedure(string ProcedureName, Dictionary<String, String> _parameterList)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                DataSet ds = new DataSet();
                //oDataAdapder = new OdbcDataAdapter();
                oCommand = new OdbcCommand();
                oCommand.Connection = oConn;
                oCommand.CommandText = ProcedureName;
                oCommand.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnValue = new OdbcParameter("return_value", OdbcType.VarChar, 255);
                returnValue.Direction = ParameterDirection.ReturnValue;
                oCommand.Parameters.Add(returnValue);

                foreach (string key in _parameterList.Keys)
                {
                    OdbcParameter inputValue = new OdbcParameter(Convert.ToString(key), OdbcType.VarChar);
                    inputValue.Direction = ParameterDirection.Input;
                    inputValue.Value = _parameterList[key];
                    if (inputValue.Value.ToString().Length == 0)
                    {
                        inputValue.Value = DBNull.Value;
                    }
                    oCommand.Parameters.Add(inputValue);
                }
                _parameterList.Clear();
                int irows = oCommand.ExecuteNonQuery();
                return returnValue;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }

        // Reset Search Overload
        public OdbcParameter StoredProcedure(string ProcedureName, Dictionary<String, String> _parameterList, String PageName, Int32 TimeOut, out Boolean isError)
        {
            isError = false;
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                setTimeout(PageName, TimeOut.ToString());
                
                DataSet ds = new DataSet();
                //oDataAdapder = new OdbcDataAdapter();
                oCommand = new OdbcCommand();
                oCommand.Connection = oConn;
                oCommand.CommandText = ProcedureName;
                oCommand.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnValue = new OdbcParameter("return_value", OdbcType.VarChar, 255);
                returnValue.Direction = ParameterDirection.ReturnValue;
                oCommand.Parameters.Add(returnValue);

                foreach (string key in _parameterList.Keys)
                {
                    OdbcParameter inputValue = new OdbcParameter(Convert.ToString(key), OdbcType.VarChar);
                    inputValue.Direction = ParameterDirection.Input;
                    inputValue.Value = _parameterList[key];
                    if (inputValue.Value.ToString().Length == 0)
                    {
                        inputValue.Value = DBNull.Value;
                    }
                    oCommand.Parameters.Add(inputValue);
                }
                _parameterList.Clear();
                int irows = oCommand.ExecuteNonQuery();
                return returnValue;
            }
            catch (Exception Ex)
            {
                //throw new Exception("Error Source: Connection " + Ex.Message);
                if ((Ex.Message.IndexOf("Request timed out due to user timeout") != -1) || (Ex.Message.IndexOf("Socket: Connection reset by peer (due to timeout or reboot)") != -1) || (Ex.Message.IndexOf("Message sequencing error")) != -1 || Ex.Message.IndexOf("Object reference not set to an instance of an object") != -1 || Ex.Message.IndexOf("Connection ERROR") != -1 || Ex.Message.IndexOf("UNKNOWN MSG") != -1)
                {
                    isError = true;                    
                }                
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
            return new OdbcParameter("error","");
        }

        public OdbcParameter StoredProcedure(String ProcedureName, Dictionary<String, String> _parameterList, Int32 lengthOfReturnValue)
        {
            try
            {
                object objcon = oConn;
                if (objcon == null)
                {
                    OpenCacheConnection();
                }
                DataSet ds = new DataSet();
                //oDataAdapder = new OdbcDataAdapter();
                oCommand = new OdbcCommand();
                oCommand.Connection = oConn;
                oCommand.CommandText = ProcedureName;
                oCommand.CommandType = CommandType.StoredProcedure;

                OdbcParameter returnValue = new OdbcParameter("return_value", OdbcType.Text, lengthOfReturnValue);
                returnValue.Direction = ParameterDirection.ReturnValue;
                oCommand.Parameters.Add(returnValue);

                foreach (string key in _parameterList.Keys)
                {
                    OdbcParameter inputValue = new OdbcParameter(Convert.ToString(key), OdbcType.VarChar);
                    inputValue.Direction = ParameterDirection.Input;
                    inputValue.Value = _parameterList[key];
                    if (inputValue.Value.ToString().Length == 0)
                    {
                        inputValue.Value = DBNull.Value;
                    }
                    oCommand.Parameters.Add(inputValue);
                }
                _parameterList.Clear();
                int irows = oCommand.ExecuteNonQuery();
                return returnValue;
            }
            catch (Exception Ex)
            {
                throw new Exception("Error Source: " + Ex.ToString());
            }
            finally
            {
                oCommand = null;
                CloseCacheConnection();
            }
        }
    }
}

namespace AtlasIndia.AntechCSM
{
    public class TextFileReadWrite
    {
        public static void WriteFile(String fullFileName, String content)
        {
            TextWriter writer = new StreamWriter(fullFileName);
            writer.Write(content);
            writer.Close();
        }

        public static String ReadFile(String fullFileName)
        {
            String content = String.Empty;
            if (File.Exists(fullFileName))
            {
                TextReader reader = new StreamReader(fullFileName);
                content = reader.ReadToEnd();
                reader.Close();
            }
            return content;
        }

        public static void AppendLine(String fullFileName, String content)
        {
            TextWriter writer = new StreamWriter(fullFileName,true);
            writer.WriteLine(content);
            writer.Close();
        }        
    }
}