using System.Threading.Tasks;
using Valhalla.Dominio.Models;

namespace Valhalla.Dominio.Interfaces
{
    public interface IClientService
    {
        Task AddClient(Client client);
    }
}
