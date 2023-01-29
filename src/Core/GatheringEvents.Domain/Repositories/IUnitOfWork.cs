using System.Threading;
using System.Threading.Tasks;

namespace GatheringEvents.Domain.Repositories;

public interface IUnitOfWork
{
    Task SaveChangesAsync(CancellationToken cancelToken);
}