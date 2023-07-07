using MinvoiceWebhook.MOD;
using MinvoiceWebhook.Models;
using MinvoiceWebhook.Services;
using System.Data;
using System.Data.SqlClient;

namespace MinvoiceWebhook.DAL
{
    public class AccountDAL
    {
        public AccountMOD LoginDAL(string UserName, string Password)
        {
            AccountMOD item = null;
            SqlParameter[] parameters = new SqlParameter[]
            {
                new SqlParameter("@UserName", SqlDbType.NVarChar),
                new SqlParameter("@Password", SqlDbType.NVarChar),
            };
            parameters[0].Value = UserName;
            parameters[1].Value = Password;
            try
            {
                using (SqlDataReader dr = SQLHelper.ExecuteReader(SQLHelper.appConnectionStrings, System.Data.CommandType.StoredProcedure, "SingnInUser", parameters))
                {
                    while (dr.Read())
                    {
                        item = new AccountMOD();
                        item.UserName = Utils.ConvertToString(dr["UserName"], string.Empty);
                        item.Password = Utils.ConvertToString(dr["Password"], string.Empty);

                        break;
                    }
                    dr.Close();
                }
            }
            catch (Exception)
            {

                throw;
            }
            return item;
        }
        
    }
}
