using System.Threading;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public interface IAboutMeService
    {
	    Task<AboutItem> GetAboutItem(CancellationToken cancellationToken, string entryId);
    }
}