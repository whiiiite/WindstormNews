namespace NewsApp.Entities.ViewModels
{
    /// <summary>
    /// Class for contains user email and role name to set role of user
    /// </summary>
    public class UserToRoleViewModel
    {
        public required string UserEmail { get; set; }
        public required string RoleName { get; set; }
    }
}
