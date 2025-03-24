namespace PRM.Contracts.Requests.admin;

public class AdminUserRequests
{
    public class Create
    {
        public string Name { get; set; }
        public string Username { get; set; }
        public bool IsAdmin { get; set; } = false;
    }
}