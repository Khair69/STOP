using STOP.Models;

namespace STOP.Services
{
    public interface IDogService
    {
        Task<Dog[]> GetDogsAsync();
        Task<bool> AddDogAsync(Dog newDog);
        Task<Dog> GetDogByIdAsync(Guid id);
        Task<bool> EditDogAsync(Dog editedDog);
        Task<bool> DeleteDogAsync(Guid id);
    }
}
