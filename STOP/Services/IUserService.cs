namespace STOP.Services
{
    public interface IUserService
    {
        Task<bool> MakeAdminAsync(string Id);
        Task<bool> RemoveAdminAsync(string Id);
    }
}
