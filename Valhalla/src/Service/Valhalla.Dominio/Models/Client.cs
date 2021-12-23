using System;
using System.Collections.Generic;
using Valhalla.Dominio.Models.Enum;

namespace Valhalla.Dominio.Models
{
    public class Client : Entity
    {
        public string FullName { get; private set; }
        public string Document { get; private set; }
        public PersonType PersonType { get; private set; }
        public DateTime BirthDate { get; private set; }

        private List<Phone> _phones = new List<Phone>();
        public IReadOnlyCollection<Phone> Phones => _phones;

        protected Client() { }

        public Client(string fullName, string document, PersonType personType, string ddd, string number, PhoneType phoneType)
        {
            //Validation.ValidateIsNullOrEmpty(fullName, "O nome completo é obrigatorio");
            //Validation.CharactersValidate(ddd, 100, 2, "O nome completo deve conter entre 2 e 100 catacteres");
            //Validation.ValidateIsNullOrEmpty(document, "O documento é obrigatorio");
            //Validation.ValidateIsNullOrEmpty(ddd, "");
            //Validation.CharactersValidate(ddd, 2, 2, "");
            //Validation.ValidateIsNullOrEmpty(number, "");

            FullName = fullName;
            Document = document;
            PersonType = personType;

            AddPhone(ddd, number, phoneType);
        }

        public void AddPhone(string ddd, string number, PhoneType phoneType)
        {
            if (_phones.Count >= 3) throw new DomainException("É permitido no máximo 3 telefones.");
            _phones.Add(new Phone(this, ddd, number, phoneType));
        }
    }
}
