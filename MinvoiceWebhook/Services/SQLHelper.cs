using System;
using System.Data;
using System.Data.SqlClient;
using System.Collections;
using System.Configuration;


namespace MinvoiceWebhook.Services
{

    public class SQLHelper
    {
        [Obsolete]
        public static string appConnectionStrings = "Data Source=ONII-CHAN\\TUAN;Initial Catalog=MinvoiceWebhook;Integrated Security=True";

        public static string backupPath = "";
        public static string dbName = "";

        
        public static Hashtable parmCache = Hashtable.Synchronized(new Hashtable());

        public static int ExecuteNonQuery(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                int val = cmd.ExecuteNonQuery(); 
                cmd.Parameters.Clear();
                return val;
            }
        }
        public static int ExecuteNonQuery(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {

            SqlCommand cmd = new SqlCommand();

            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();
            cmd.Parameters.Clear();
            return val;
        }
        public static int ExecuteNonQuery(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            //cmd.Parameters.Clear();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            int val = cmd.ExecuteNonQuery();

            return val;
        }
        public static object ExecuteScalar(SqlTransaction trans, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            PrepareCommand(cmd, trans.Connection, trans, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            //cmd.Parameters.Clear();
            return val;
        }

        
        public static SqlDataReader ExecuteReader(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            SqlConnection conn = new SqlConnection(connString);
            cmd.Parameters.Clear();
            if (conn.State == ConnectionState.Open)
            {
                conn.Close();
            }
            
            try
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                SqlDataReader rdr = cmd.ExecuteReader(CommandBehavior.CloseConnection);
                //cmd.Parameters.Clear();
                return rdr;
            }
            catch (Exception ex)
            {
                conn.Close();
                throw;
            }
        }

 
        public static object ExecuteScalar(string connString, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();

            using (SqlConnection conn = new SqlConnection(connString))
            {
                PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
                object val = cmd.ExecuteScalar();
                //cmd.Parameters.Clear();
                return val;
            }
        }

        public static object ExecuteScalar(SqlConnection conn, CommandType cmdType, string cmdText, params SqlParameter[] cmdParms)
        {
            SqlCommand cmd = new SqlCommand();
            cmd.Parameters.Clear();
            PrepareCommand(cmd, conn, null, cmdType, cmdText, cmdParms);
            object val = cmd.ExecuteScalar();
            //cmd.Parameters.Clear();
            return val;
        }

        
        public static void CacheParameters(string cacheKey, params SqlParameter[] cmdParms)
        {
            parmCache[cacheKey] = cmdParms;
        }

        public static SqlParameter[] GetCachedParameters(string cacheKey)
        {
            SqlParameter[] cachedParms = (SqlParameter[])parmCache[cacheKey];

            if (cachedParms == null)
                return null;

            SqlParameter[] clonedParms = new SqlParameter[cachedParms.Length];

            for (int i = 0, j = cachedParms.Length; i < j; i++)
                clonedParms[i] = (SqlParameter)((ICloneable)cachedParms[i]).Clone();

            return clonedParms;
        }

        private static void PrepareCommand(SqlCommand cmd, SqlConnection conn, SqlTransaction trans, CommandType cmdType, string cmdText, SqlParameter[] cmdParms)
        {

            if (conn.State != ConnectionState.Open)
                conn.Open();

            cmd.Connection = conn;
            cmd.CommandText = cmdText;

            if (trans != null)
            {
                cmd.Transaction = trans;
            }

            cmd.CommandType = cmdType;

            if (cmdParms != null)
            {
                foreach (SqlParameter parm in cmdParms)
                {
                    cmd.Parameters.Add(parm);
                }
            }
            // conn.Close();
        }


        public static bool IsNullOrEmpty(string value)
        {
            if (value != null)
                return (value.Trim().Length == 0);

            return true;
        }
        
        public static bool IsNullOrEmpty(string[] values)
        {
            if (values != null)
                return (values.Length == 0);

            return true;
        }
        
        public static string IfNotNullOrEmpty(string oldValue, string newValue)
        {
            return IsNullOrEmpty(oldValue) ? string.Empty : newValue;
        }
        
        public static string IfNotNullOrEmpty(string[] oldValues, string newValue)
        {
            return IsNullOrEmpty(oldValues) ? string.Empty : newValue;
        }

        
        public static string CreateQueryString(string criterias, string priorities)
        {

            string queryString = string.Empty;
            queryString += IfNotNullOrEmpty(criterias, "WHERE " + criterias);
            queryString += IfNotNullOrEmpty(priorities, "ORDER BY " + priorities);
            if (queryString.IndexOf(";") > -1) return string.Empty;
            return queryString;
        }

        public static string CreateSearchQueryString(string keyword, string priorities)
        {
            string queryString = string.Empty;
            queryString += IfNotNullOrEmpty(keyword, String.Format("WHERE CONTAINS(*,'\"{0}\"')", keyword));
            queryString += IfNotNullOrEmpty(priorities, "ORDER BY " + priorities);
            if (queryString.IndexOf(";") > -1) return string.Empty;
            return queryString;
        }
        
        public static string CreateSearchQueryString(string keyword, string criterias, string priorities)
        {
            string queryString = string.Empty;
            queryString += IfNotNullOrEmpty(keyword, String.Format("WHERE CONTAINS(*,'\"{0}\"')", keyword));
            if (IsNullOrEmpty(queryString))
            {
                queryString += IfNotNullOrEmpty(criterias, " WHERE " + criterias);
            }
            else
            {
                queryString += IfNotNullOrEmpty(criterias, " AND " + criterias);
            }
            queryString += IfNotNullOrEmpty(priorities, "ORDER BY " + priorities);
            if (queryString.IndexOf(";") > -1) return string.Empty;
            return queryString;
        }
    }
}