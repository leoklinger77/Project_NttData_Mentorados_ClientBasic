using Microsoft.AspNetCore.Mvc;
using Valhalla.Dominio.Interfaces;
using System.Linq;
using Valhalla.Dominio.Models;

namespace Valhalla.WebApp.Extension.ViewComponents
{
    public class SummaryViewComponent : ViewComponent
    {
        private readonly INotifierService _notifierService;

        public SummaryViewComponent(INotifierService notifierService)
        {
            _notifierService = notifierService;
        }

        public IViewComponentResult Invoke()
        {
            var erros = _notifierService.GetAll().Select(c => c.Error).ToList();

            erros.ForEach(x => ViewData.ModelState.AddModelError(string.Empty, x + " <br />"));

            return View(erros);
        }
    }
    public class PaginationViewComponent : ViewComponent
    {
        public IViewComponentResult Invoke(IPagedViewModel modelPagination)
        {
            return View(modelPagination);
        }
    }
}
