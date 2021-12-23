using System;
using System.Collections.Generic;
using System.Text;
using Valhalla.Dominio.Notifier;

namespace Valhalla.Dominio.Interfaces
{
    public interface INotifierService
    {
        void AddError(string erro);
        IEnumerable<Notification> GetAll();
        bool HasError();
    }
}
