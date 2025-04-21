using Microsoft.EntityFrameworkCore;
using STOP.Data;
using STOP.Models;

namespace STOP.Services
{
    public class DogService : IDogService
    {
        private readonly ApplicationDbContext _context;

        public DogService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<bool> AddDogAsync(Dog newDog)
        {
            newDog.DogId = Guid.NewGuid();

            _context.Dog.Add(newDog);

            var saveResult = await _context.SaveChangesAsync();
            return saveResult == 1;
        }

        public async Task<bool> DeleteDogAsync(Guid id)
        {
            var existingDog = await _context.Dog.Where(x => x.DogId == id).FirstOrDefaultAsync();
            if (existingDog == null) return false;

            _context.Dog.Remove(existingDog);

            var saveResault = await _context.SaveChangesAsync();
            return saveResault == 1;
        }

        public async Task<bool> EditDogAsync(Dog editedDog)
        {
            var existingDog = await _context.Dog.Where(x => x.DogId == editedDog.DogId).FirstOrDefaultAsync();
            if (existingDog == null) return false;
            
            existingDog.Name = editedDog.Name;
            existingDog.DateOfBirth = editedDog.DateOfBirth;
            existingDog.Description = editedDog.Description;

            var saveResault = await _context.SaveChangesAsync();
            return saveResault == 1;
        }

        public async Task<Dog> GetDogByIdAsync(Guid id)
        {
            return await _context.Dog.Where(x => x.DogId == id).Include(d => d.Owner).FirstOrDefaultAsync();
        }

        public async Task<Dog[]> GetDogsAsync()
        {
            return await _context.Dog.Include(d => d.Owner).ToArrayAsync();
        }
    }
}
