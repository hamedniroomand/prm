namespace PRM.Contracts.Responses;

public class UserResponses
{
    public class User
    {
        private int _role;
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Role
        {
            get
            {
                switch (_role)
                {
                    case 1:
                        return "superAdmin";
                    case 2:
                        return "admin";
                    default:
                        return "employee";
                }
            }
            set => _role = int.Parse(value);
        }
    }
}