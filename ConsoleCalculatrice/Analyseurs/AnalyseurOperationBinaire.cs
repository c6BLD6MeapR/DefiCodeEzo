using ConsoleCalculatrice.Composite;

namespace ConsoleCalculatrice.Analyseurs
{
    public class AnalyseurOperationBinaire : IAnalyseurInfix
    {
        public int Priorite { get; }
        private readonly Dictionary<TypesJeton, Func<double, double, double>> _modelesOperationBinaire;

        public AnalyseurOperationBinaire(int priorite, Dictionary<TypesJeton, Func<double, double, double>> modelesOperationBinaire)
        {
            Priorite = priorite;
            _modelesOperationBinaire = modelesOperationBinaire;
        }

        public IExpression AnalyseurExpression(AnalyseurExpression analyseur, IExpression gauche, Jeton jeton)
        {
            IExpression droite = analyseur.AnalyserExpression(Priorite);

            if(_modelesOperationBinaire.ContainsKey(jeton.Type) == false)
            {
                throw new InvalidOperationException($"Le type de jeton {jeton.Type} n'a pas de modèle mathématique enregistré.");
            }

            Func<double, double, double> modele = _modelesOperationBinaire[jeton.Type];
            return new ExpressionOperationBinaire(gauche, droite, modele);
        }
    }
}
