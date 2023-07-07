using MinvoiceWebhook.DAL;
using MinvoiceWebhook.Models;
using MinvoiceWebhook.Services;
namespace MinvoiceWebhook.BUS
{
    public class AccountBUS
    {
        public BaseResultMOD LoginBUS(AccountMOD login)
        {
            var Result = new BaseResultMOD();
            try
            {
                if (login.UserName == null || login.UserName == "")
                {
                    Result.Status = 0;
                    Result.Message = "UserName không được để trống";
                    return Result;
                }
                else if (login.Password == null || login.Password == "")
                {
                    Result.Status = 0;
                    Result.Message = "Mật khẩu không được để trống";
                    return Result;
                }
                else
                {
                    var userLogin = new AccountDAL().LoginDAL(login.UserName, login.Password);
                    if (userLogin != null && userLogin.UserName != null)
                    {
                        Result.Status = 1;
                        Result.Message = "Đăng nhập thành công";
                        Result.Data = userLogin;
                    }
                    else
                    {
                        Result.Status = 0;
                        Result.Message = "Tài khoản hoặc mật khẩu không đúng!";
                    }
                    return Result;
                }
            }
            catch (Exception)
            {
                Result.Status = -1;
                Result.Message = Constant.ERR_INSERT;
                throw;
            }
            return Result;
        }
    }
}
