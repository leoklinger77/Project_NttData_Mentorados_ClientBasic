﻿using System;

namespace Valhalla.Dominio
{
    public class DomainException : Exception
    {
        public DomainException(string message) : base(message)
        {
        }
    }
}
