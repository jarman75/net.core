using System;

namespace components.Data
{
    public class Persona
    {
        public Persona(string nombre, string apellido1, string apellido2)
        {
            this.Nombre = nombre;
            this.Apellido1 = apellido1;
            this.Apellido2 = apellido2;

        }
        
        public Guid Id {get => Guid.NewGuid(); }
        public string Nombre { get; set; }
        public string Apellido1 { get; set; }
        public string Apellido2 { get; set; }

        public override string ToString()
        {
            return $"{Nombre} {Apellido1} {Apellido2}";
        }
    }
}