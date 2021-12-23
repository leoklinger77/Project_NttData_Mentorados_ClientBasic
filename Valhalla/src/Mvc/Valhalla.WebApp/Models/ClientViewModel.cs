using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Valhalla.Dominio.Models.Enum;

namespace Valhalla.WebApp.Models
{
    public class ClientViewModel
    {
        public Guid Id { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [Display(Name = "Nome completo")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        [Display(Name = "Documento")]
        public string Document { get; set; }


        public PersonType PersonType { get; set; }
        public DateTime BirthDate { get; set; }

        public DateTime InsertDate { get; set; }

        public IEnumerable<PhoneViewModel> Phones { get; set; } = new List<PhoneViewModel>();

        
        [Required(ErrorMessage = "O campo {0} é obrigatorio.")]
        public string Celular { get; set; }
        public string Home { get; set; }
        public string TelComercial { get; set; }
    }

    public class PhoneViewModel
    {
        public Guid Id { get; set; }
        public string Ddd { get; set; }
        public string Number { get; set; }
        public PhoneType PhoneType { get; set; }
    }
}
