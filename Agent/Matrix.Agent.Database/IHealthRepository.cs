using Matrix.Agent.Model;
using System.Threading.Tasks;

namespace Matrix.Agent.Database
{
    public interface IHealthRepository : IRepository
    {
        Task<HealthTestResult> Test();
    }
}