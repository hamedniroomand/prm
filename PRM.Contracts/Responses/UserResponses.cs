namespace PRM.Contracts.Responses;

public class UserResponses
{
    public class User
    {
        private int _role;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }

        public string Role { get; set; }
    }
}