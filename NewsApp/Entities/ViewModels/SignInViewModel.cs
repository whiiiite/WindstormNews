namespace NewsApp.Entities.ViewModels
{
    /// <summary>
    /// Class for sign in data from form or other source
    /// </summary>
    public class SignInViewModel
    {
        public required string Email { get; set; }
        public required string Password { get; set; }
    }
}
