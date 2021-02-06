namespace Test
{
    public class AnimalService
    {
        public virtual Animal Create(string nombre, int edad)
        {
            return new(nombre, edad);
        }
    }
    
}