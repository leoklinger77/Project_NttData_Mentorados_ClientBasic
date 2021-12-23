using FluentValidation;
using System.Threading.Tasks;
using Valhalla.Dominio.Interfaces;
using Valhalla.Dominio.Models;
using Valhalla.Dominio.Models.Validation;

namespace Valhalla.Dominio.Services
{
    public class ClientService : IClientService
    {
        private readonly INotifierService _notifierService;
        private readonly IClienRepository _clienRepository;

        public ClientService(INotifierService notifierService, IClienRepository clienRepository)
        {
            _notifierService = notifierService;
            _clienRepository = clienRepository;
        }

        public async Task AddClient(Client client)
        {
            RunValidation(new ClientValidation(), client);
            foreach (var item in client.Phones)
                RunValidation(new PhoneValidation(), item);

            if (_notifierService.HasError()) return;


            var result = await _clienRepository.FindByClient(x => x.Document == client.Document);
            if(result != null)
            {
                _notifierService.AddError("Documento ja cadastrado para outro cliente.");
                return;
            }
            
            await _clienRepository.Insert(client);
            foreach (var item in client.Phones)
            {
                await _clienRepository.InsertPhone(item);
            }            

            if (await _clienRepository.SaveChanges() < 1)
            {
                _notifierService.AddError("Houve um erro ao persistir dados");

                //Primeira opção de erro - Exception

                //Segunda DominException

                return;
            }
            await Task.CompletedTask;
        }


        private bool RunValidation<Tv, Te>(Tv validacao, Te entidade) where Tv : AbstractValidator<Te> where Te : Entity
        {
            var validator = validacao.Validate(entidade);

            if (validator.IsValid) return true;

            foreach (var item in validator.Errors)
            {
                _notifierService.AddError(item.ErrorMessage);
            }
            return false;
        }
    }
}
