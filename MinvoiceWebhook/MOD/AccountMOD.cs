namespace MinvoiceWebhook.Models
{
    public class AccountMOD
    {
        public string UserName { get; set; }
        public string Password { get; set; }
    }
    public class LoginMOD : AccountMOD
    {
        public string Ma_HocVien { get; set; }
    }
    public class ListUserMOD
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }        
        public string Email { get; set; }
    }
}
