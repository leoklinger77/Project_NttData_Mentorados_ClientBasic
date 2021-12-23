using System.Collections.Generic;
using System.Linq;
using Valhalla.Dominio.Interfaces;
using Valhalla.Dominio.Notifier;

namespace Valhalla.Dominio.Services
{
    public class NotifierService : INotifierService
    {
        private List<Notification> list = new List<Notification>();

        public NotifierService() { }

        public void AddError(string erro)
        {
            list.Add(new Notification(erro));
        }

        public IEnumerable<Notification> GetAll()
        {
            return list;
        }

        public bool HasError()
        {
            return list.Any();
        }
    }
}
