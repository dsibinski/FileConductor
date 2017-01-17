namespace FileConductor
{
    public class TargetTransformData
    {
        public TargetTransformData(string ipAddress, string path, string login, string password)
        {
            IpAddress = ipAddress;
            Path = path;
            Login = login;
            Password = password;
        }

        public string IpAddress { get; set; }
        public string Path { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}