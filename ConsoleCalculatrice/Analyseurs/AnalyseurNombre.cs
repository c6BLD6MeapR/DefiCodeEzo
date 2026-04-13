using ConsoleCalculatrice.Composite;
using System.Globalization;

namespace ConsoleCalculatrice.Analyseurs
{
    public class AnalyseurNombre : IAnalyseurPrefix
    {
        public IExpression AnalyserExpression(AnalyseurExpression analyseur, Jeton jeton)
        {
            double nombre = double.Parse(jeton.Valeur, CultureInfo.InvariantCulture);
            return new ExpressionNombre(nombre);
        }
    }
}
