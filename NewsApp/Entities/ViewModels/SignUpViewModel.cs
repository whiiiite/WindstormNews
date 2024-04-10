namespace NewsApp.Entities.ViewModels
{
    public class SignUpViewModel
    {
        public required string UserName { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public required string ConfirmPassword { get; set; }
    }
}