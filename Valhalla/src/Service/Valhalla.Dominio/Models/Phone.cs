using System;
using Valhalla.Dominio.Models.Enum;

namespace Valhalla.Dominio.Models
{
    public class Phone : Entity
    {
        public Guid ClientId { get; private set; }

        public string Ddd { get; private set; }
        public string Number { get; private set; }
        public PhoneType PhoneType { get; private set; }

        public Client Client { get; private set; }

        public Phone(Client client, string ddd, string number, PhoneType phoneType)
        {
            ClientId = client.Id;
            Client = client;

            Ddd = ddd;
            Number = number;
            PhoneType = phoneType;
        }
    }
}
