using ConsoleCalculatrice.Composite;

namespace ConsoleCalculatrice.Analyseurs
{
    public class AnalyseurNombre : IAnalyseurPrefix
    {
        private readonly Func<string, double> _modeleNombre;
        
        public AnalyseurNombre(Func<string, double> modeleNombre) => _modeleNombre = modeleNombre;

        public IExpression AnalyserExpression(AnalyseurExpression analyseur, Jeton jeton)
        {
            double nombre = _modeleNombre(jeton.Valeur);
            return new ExpressionNombre(nombre);
        }
    }
}
