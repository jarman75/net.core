namespace Test
{    
    public class GatoService : AnimalService
    {
        public override Gato Create(string nombre, int edad)
        {
            return new(nombre, edad);
        }
    }
    
}