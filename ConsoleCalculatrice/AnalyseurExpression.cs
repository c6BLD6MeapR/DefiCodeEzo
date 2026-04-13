using ConsoleCalculatrice.Analyseurs;
using ConsoleCalculatrice.Composite;

namespace ConsoleCalculatrice
{
    public class AnalyseurExpression
    {
        private readonly IReadOnlyList<Jeton> _jetons;
        private int _position = 0;
        public Jeton JetonActuel => _jetons[_position];

        private Dictionary<TypesJeton, IAnalyseurPrefix> _analyseursPrefix = [];
        private Dictionary<TypesJeton, IAnalyseurInfix> _analyseursInfix = [];

        public AnalyseurExpression(IEnumerable<Jeton> jetons) => _jetons = jetons.ToList();

        public IExpression AnalyserExpression(int priorite = 0)
        {
            Jeton jeton = ProchainJeton();

            if (_analyseursPrefix.ContainsKey(jeton.Type) == false)
            {
                throw new InvalidOperationException("Syntaxe invalide");
            }

            IAnalyseurPrefix analyseurPrefix = _analyseursPrefix[jeton.Type];
            IExpression expression = analyseurPrefix.AnalyserExpression(this, jeton);

            while(priorite < ObtenirPrioriteActuelle())
            {
                jeton = ProchainJeton();
                IAnalyseurInfix analyseurInfix = _analyseursInfix[jeton.Type];
                expression = analyseurInfix.AnalyseurExpression(this, expression, jeton);
            }

            return expression;

        }

        public void Enregistrer(TypesJeton typeJeton, IAnalyseurPrefix analyseur)
        {
            _analyseursPrefix[typeJeton] = analyseur;
        }

        public void Enregistrer(TypesJeton typeJeton, IAnalyseurInfix analyseur)
        {
            _analyseursInfix[typeJeton] = analyseur;
        }

        public Jeton ProchainJeton() => _jetons[_position++];

        public int ObtenirPrioriteActuelle()
        {
            if(_analyseursInfix.TryGetValue(JetonActuel.Type, out var analyseur))
            {
                return analyseur.Priorite;
            }

            return 0;
        }
    }
}
