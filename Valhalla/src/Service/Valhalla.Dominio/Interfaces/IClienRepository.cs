using System;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Valhalla.Dominio.Models;

namespace Valhalla.Dominio.Interfaces
{
    public interface IClienRepository
    {
        Task<Client> FindByClient(Expression<Func<Client, bool>> predicate);
        Task<PaginationViewModel<Client>> Pagination(int PageSize, int PageIndex, string query);
        
        
        Task Insert(Client client);
        Task InsertPhone(Phone phone);



        Task<int> SaveChanges();
    }
}
