namespace ASC.Web.Configuration
{
    public class ApplicationSettings
    {
        public string ApplicationTitle { get; set; }
        public string AdminEmail { get; set; }
        public string AdminName { get; set; }
        public string AdminPassword { get; set; }
        public List<string> Roles { get; set; } //Thay string thành List<string> tại vì bên file appsettings.jscon Role là một danh sách chuỗi [ "Admin", "User", "Engineer" ]
        public string EngineerEmail { get; set; }
        public string EngineerName { get; set; }
        public string EngineerPassword { get; set; }
        public string SMTPServer { get; set; }
        public int SMTPPort { get; set; }
        public string SMTPAccount { get; set; }
        public string SMTPPassword { get; set; }
    }
}

