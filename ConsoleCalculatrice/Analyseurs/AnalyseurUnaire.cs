using ConsoleCalculatrice.Composite;
using ConsoleCalculatrice.Expressions;

namespace ConsoleCalculatrice.Analyseurs
{
    public class AnalyseurUnaire : IAnalyseurPrefix
    {
        public int Priorite { get; }

        private readonly Dictionary<TypesJeton, Func<double, double>> _modelesUnaire;

        public AnalyseurUnaire(int priorite, Dictionary<TypesJeton, Func<double, double>> modelesUnaire)
        {
            Priorite = priorite;
            _modelesUnaire = modelesUnaire;
        }

        public IExpression AnalyserExpression(AnalyseurExpression analyseur, Jeton jeton)
        {
            IExpression expression = analyseur.AnalyserExpression(Priorite);

            if (_modelesUnaire.ContainsKey(jeton.Type) == false)
            {
                throw new InvalidOperationException($"Le type de jeton {jeton.Type} n'a pas de modèle mathématique enregistré.");
            }

            return new ExpressionUnaire(expression, _modelesUnaire[jeton.Type]);
        }
    }
}
