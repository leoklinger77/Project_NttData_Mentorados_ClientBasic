using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Valhalla.Dominio.Interfaces;

namespace Valhalla.WebApp.Extension
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        private readonly INotifierService _notifierService;

        public BaseController(INotifierService notifierService)
        {
            _notifierService = notifierService;
        }

        protected bool OperationValid()
        {
            return _notifierService.HasError();
        }
    }
}
