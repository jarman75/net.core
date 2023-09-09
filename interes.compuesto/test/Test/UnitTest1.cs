using Algoritmos;
namespace Test;

public class Tests
{
    InteresCompuesto? interesCompuesto;
    
    [SetUp]
    public void Setup()
    {
        interesCompuesto = new();
    }

    [Test]
    public void Test_interesCompuesto()
    {
        var result = interesCompuesto!.Calcular(capitalInicial: 1000, aportacionMensual: 100, 
        tasaInteresAnual: 5, numeroDeVecesCapitalizacion: 12, plazoEnAnios: 10);
        Assert.That(result, Is.EqualTo(17175.24));

        //17.175,24
    }
}