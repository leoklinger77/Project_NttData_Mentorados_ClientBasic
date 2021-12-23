using FluentValidation;
using System;
using System.Collections.Generic;
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

        public ClientService(INotifierService notifierService)
        {
            _notifierService = notifierService;
        }

        public async Task AddClient(Client client)
        {            
            if (RunValidation(new ClientValidation(), client)) return;

            //Validar se ja existe o ducumento salvo no banco

            //Salva na memoria
            await _clienRepository.Insert(client);

            foreach (var item in client.Phones)
            {
                await _clienRepository.InsertPhone(item);
            }           
           

            //Salva no banco

            if(await _clienRepository.SaveChanges() < 1)
            {
                //Error
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
