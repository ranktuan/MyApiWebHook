namespace MinvoiceWebhook.MOD
{
    public class AuthorizeAttributeMOD
    {
        public int id_user { get; set; }
        public string Email { get; set; }
        public int id_Function { get; set; }
        public string name_GroupUser { get; set; }
        public string name_Function { get; set; }
        public int role { get; set; }
        public bool Read { get; set; } = false;
        public bool Add { get; set; } = false;
        public bool Update { get; set; } = false;
        public bool Delete { get; set; } = false;


        public AuthorizeAttributeMOD(int id_user, int id_Function, string name_GroupUser, string name_Function, int? role)
        {
            this.id_user = id_user;

            this.id_Function = id_Function;
            this.name_GroupUser = name_GroupUser;
            this.name_Function = name_Function;
            this.role = (int)role;
            if (role != null && role == 1)
            {
                this.Read = true;
            }
            else if (role != null && role == 2)
            {
                this.Add = true;
            }
            else if (role != null && role == 3)
            {
                this.Read = true;
                this.Add = true;
            }
            else if (role != null && role == 4)
            {
                this.Update = true;
            }
            else if (role != null && role == 5)
            {
                this.Update = true;
                this.Read = true;
            }
            else if (role != null && role == 6)
            {
                this.Add = true;
                this.Update = true;
            }
            else if (role != null && role == 7)
            {
                this.Read = true;
                this.Add = true;
                this.Update = true;
            }
            else if (role != null && role == 8)
            {
                this.Delete = true;
            }
            else if (role != null && role == 9)
            {
                this.Read = true;
                this.Delete = true;
            }
            else if (role != null && role == 10)
            {
                this.Add = true;
                this.Delete = true;
            }
            else if (role != null && role == 11)
            {
                this.Read = true;
                this.Add = true;
                this.Delete = true;
            }
            else if (role != null && role == 12)
            {
                this.Update = true;
                this.Delete = true;
            }
            else if (role != null && role == 13)
            {
                this.Read = true;
                this.Update = true;
                this.Delete = true;
            }
            else if (role != null && role == 14)
            {
                this.Add = true;
                this.Update = true;
                this.Delete = true;
            }
            else if (role != null && role == 15)
            {
                this.Read = true;
                this.Add = true;
                this.Update = true;
                this.Delete = true;
            }
            else
            {
                this.Read = false;
                this.Add = false;
                this.Update = false;
                this.Delete = false;
            }
        }

        public AuthorizeAttributeMOD()
        {

        }
    }
}
