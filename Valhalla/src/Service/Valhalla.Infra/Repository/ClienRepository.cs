using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Valhalla.Dominio.Interfaces;
using Valhalla.Dominio.Models;
using Valhalla.Infra.Data;
using X.PagedList;

namespace Valhalla.Infra.Repository
{
    public class ClienRepository : IClienRepository
    {
        private readonly ValhallaContext _context;

        public ClienRepository(ValhallaContext context)
        {
            _context = context;
        }

        public async Task<PaginationViewModel<Client>> Pagination(int PageSize, int PageIndex, string query)
        {
            IPagedList<Client> list;

            if (!string.IsNullOrEmpty(query))
            {
                list = await _context.Clients.Where(x => x.Document.Contains(query) || x.FullName.Contains(query)).AsNoTracking().ToPagedListAsync(PageIndex, PageSize);
            }
            else
            {
                list = await _context.Clients.AsNoTracking().ToPagedListAsync(PageIndex, PageSize);
            }

            return new PaginationViewModel<Client>()
            {
                List = list.ToList(),
                PageIndex = PageIndex,
                PageSize = PageSize,
                Query = query,
                TotalResult = list.TotalItemCount
            };
        }

        public async Task<Client> FindByClient(Expression<Func<Client, bool>> predicate)
        {
            return await _context.Clients.Where(predicate).AsNoTracking().FirstOrDefaultAsync();
        }

        public async Task Insert(Client client)
        {
            await _context.Clients.AddAsync(client);
        }

        public async Task InsertPhone(Phone phone)
        {
            await _context.Phones.AddAsync(phone);
        }

        public async Task<int> SaveChanges()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
