using System;

namespace Api.Entities
{
    public sealed class Customer
    {
        private Customer() { }

        public Customer(string nome, string codigo)
        {
            Name = nome;
            Code = codigo;
        }

        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; private set; }
        public string Code { get; private set; }
    }
}
