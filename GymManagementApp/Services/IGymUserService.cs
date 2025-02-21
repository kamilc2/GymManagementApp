using GymManagementApp.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace GymManagementApp.Services
{
    public interface IGymUserService
    {
        Task<List<GymUser>> FindAll();
        Task<GymUser?> FindById(int id);
        Task<int> Add(GymUser user);
        Task Update(GymUser user);
        Task Delete(int id);
    }
}
