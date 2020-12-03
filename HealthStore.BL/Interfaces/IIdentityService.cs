using HealthStore.Models.Common;
using System.Threading.Tasks;

namespace HealthStore.BL.Interfaces
{
    public interface IIdentityService
    {
        Task<AuthenticationResult> RegisterAsync(string userName, string password);
        Task<AuthenticationResult> LoginAsync(string userName, string password);
    }
}
