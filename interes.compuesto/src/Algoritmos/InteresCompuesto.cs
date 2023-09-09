namespace Algoritmos;

public class InteresCompuesto
{
    public double Calcular(double capitalInicial, double aportacionMensual, double tasaInteresAnual, 
    int numeroDeVecesCapitalizacion, int plazoEnAnios)
    {
        tasaInteresAnual = tasaInteresAnual / 100;

        double factor = 1 + tasaInteresAnual / numeroDeVecesCapitalizacion;
        double potencia = numeroDeVecesCapitalizacion * plazoEnAnios;
        double factorElevado = Math.Pow(factor, potencia);
        double factorFinal = factorElevado - 1;
        double factorFinalDividido = factorFinal / (tasaInteresAnual / numeroDeVecesCapitalizacion);
        double factorFinalDivididoPorAportacion = factorFinalDividido * aportacionMensual;
        double capitalFinal = factorFinalDivididoPorAportacion + capitalInicial * factorElevado;

        return Math.Round(capitalFinal,2);
    
    }
}
    