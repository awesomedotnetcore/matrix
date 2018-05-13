using Matrix.Agent.Model;
using System.Threading.Tasks;

namespace Matrix.Agent.Business
{
    public interface IHealthService : IService
    {
        Task<HealthTestResult> Test();
    }
}