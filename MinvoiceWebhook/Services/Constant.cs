using System;
using System.Collections.Generic;
using System.Text;

namespace MinvoiceWebhook.Services
{
    public class Constant
    {
        public static readonly DateTime DEFAULT_DATE = DateTime.ParseExact("01/01/1753 12:00:00 AM", "dd/MM/yyyy hh:mm:ss tt", System.Globalization.CultureInfo.InvariantCulture);   
        public static readonly String NO_DATA = "Không có dữ liệu.";
        public static readonly String ERR_INSERT = "Xảy ra lỗi trong quá trình thêm mới dữ liệu. Vui lòng liên hệ quản trị để biết thêm thông tin!";      
        public static readonly String ERR_EXP_TOKEN = "Phiên làm việc đã hết hạn! Xin vui lòng đăng nhập lại.";
        public static readonly String NOT_ACCESS = "Người dùng không có quyền sử dụng chức năng này.";

        public static String GetUserMessage(int status)
        {
            switch (status)
            {
                default: return String.Empty;
                case 0: return NO_DATA;
                case -99: return ERR_EXP_TOKEN;
                case -98: return NOT_ACCESS;
            }
        }

        public static String GetSysMessage(int status)
        {
            switch (status)
            {
                default: return String.Empty;
                case 0: return NO_DATA;
                case -99: return ERR_EXP_TOKEN;
            }
        }
    }
}
