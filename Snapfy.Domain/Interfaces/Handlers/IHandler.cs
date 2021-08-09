using Shoalace.Domain.Commands;
using Shoalace.Domain.Interfaces.Commands;
using System.Threading.Tasks;

namespace Shoalace.Domain.Interfaces.Handlers
{
    public interface IHandler<T> where T : Command
    {
        Task<IResultadoCommand> ManipularAsync(T comando);
    }
}
