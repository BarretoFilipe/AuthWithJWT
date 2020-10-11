namespace Server.API.Models
{
    public class UserLoginCommand
    {
        public string Email { get; set; }
        public string Password { get; set; }
    }
}