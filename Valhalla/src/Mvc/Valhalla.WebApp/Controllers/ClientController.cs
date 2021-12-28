using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Valhalla.Dominio.Interfaces;
using Valhalla.Dominio.Models;
using Valhalla.Dominio.Models.Enum;
using Valhalla.WebApp.Extension;
using Valhalla.WebApp.Models;

namespace Valhalla.WebApp.Controllers
{
    [AllowAnonymous]
    public class ClientController : BaseController
    {
        private readonly IClientService _clientService;
        private readonly IMapper _mapper;

        public ClientController(INotifierService notifierService, IClientService clientService, IMapper mapper) 
                                : base(notifierService)
        {
            _clientService = clientService;
            _mapper = mapper;
        }

        [HttpGet, Route("todos-os-cliente")]        
        public async Task<IActionResult> Index(int PageSize = 1, int PageIndex = 1, string query = null)
        {
            var result = await _clientService.Pagination(PageSize, PageIndex, query);

            return View(new PaginationViewModel<ClientViewModel>()
            {
                List = _mapper.Map<IEnumerable<ClientViewModel>>(result.List),
                PageIndex = result.PageIndex,
                PageSize = result.PageSize,
                Query = result.Query,
                TotalResult = result.TotalResult,
                ReferenceAction = "Index"
            });
        }

        [HttpGet, Route("todos-os-cliente/novo-cliente")]        
        public IActionResult NewClient()
        {
            return View();
        }

        [HttpPost, Route("todos-os-cliente/novo-cliente")]        
        [AutoValidateAntiforgeryToken]
        public async Task<IActionResult> NewClient(ClientViewModel viewModel)
        {
            if(!ModelState.IsValid) return View(viewModel);
            viewModel.Id = System.Guid.NewGuid();
            
            //var client = new Client(viewModel.FullName, viewModel.Document, viewModel.PersonType, viewModel.Celular[..2], viewModel.Celular[2..], PhoneType.Smarthphone);

            //if (!string.IsNullOrEmpty(viewModel.TelComercial)) client.AddPhone(viewModel.TelComercial[..2], viewModel.TelComercial[2..], PhoneType.Commercial);
            //if (!string.IsNullOrEmpty(viewModel.Home)) client.AddPhone(viewModel.Home[..2], viewModel.Home[2..], PhoneType.Home);

            var client = _mapper.Map<Client>(viewModel);

            if (!string.IsNullOrEmpty(viewModel.TelComercial)) 
                client.AddPhone(viewModel.TelComercial[..2], viewModel.TelComercial[2..], PhoneType.Commercial);
            
            if (!string.IsNullOrEmpty(viewModel.Home)) 
                client.AddPhone(viewModel.Home[..2], viewModel.Home[2..], PhoneType.Home);
            
            if (!string.IsNullOrEmpty(viewModel.Celular)) 
                client.AddPhone(viewModel.Celular[..2], viewModel.Celular[2..], PhoneType.Smarthphone);

            await _clientService.AddClient(client);

            if(OperationValid()) return View(viewModel);

            return RedirectToAction(nameof(Index));
        }
    }
}
