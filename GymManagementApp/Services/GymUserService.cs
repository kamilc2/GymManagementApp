using GymManagementApp.Data;
using GymManagementApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagementApp.Services
{
    public class GymUserService : IGymUserService
    {
        private readonly ApplicationDbContext _context;

        public GymUserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GymUser>> FindAll()
        {
            return await _context.GymUsers.ToListAsync();
        }

        public async Task<GymUser?> FindById(int id)
        {
            return await _context.GymUsers.FirstOrDefaultAsync(u => u.Id == id);
        }

        public async Task<int> Add(GymUser user)
        {
            _context.GymUsers.Add(user);
            await _context.SaveChangesAsync();
            return user.Id;
        }

        public async Task Update(GymUser user)
        {
            _context.GymUsers.Update(user);
            await _context.SaveChangesAsync();
        }

        public async Task Delete(int id)
        {
            var user = await _context.GymUsers.FindAsync(id);
            if (user != null)
            {
                _context.GymUsers.Remove(user);
                await _context.SaveChangesAsync();
            }
        }
    }
}
