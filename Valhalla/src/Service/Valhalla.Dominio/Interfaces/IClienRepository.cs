using System.Threading.Tasks;
using Valhalla.Dominio.Models;

namespace Valhalla.Dominio.Interfaces
{
    public interface IClienRepository
    {
        Task Insert(Client client);
        Task InsertPhone(Phone phone);



        Task<int> SaveChanges();
    }
}
