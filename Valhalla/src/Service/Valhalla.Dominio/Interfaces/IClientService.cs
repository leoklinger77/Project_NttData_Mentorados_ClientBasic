using System.Threading.Tasks;
using Valhalla.Dominio.Models;

namespace Valhalla.Dominio.Interfaces
{
    public interface IClientService
    {
        Task AddClient(Client client);


        Task<PaginationViewModel<Client>> Pagination(int PageSize, int PageIndex, string query);
    }
}
