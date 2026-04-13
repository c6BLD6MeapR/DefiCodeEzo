
using System.Text.RegularExpressions;

namespace ConsoleCalculatrice
{
    public class Lexer
    {
        private const string SEPARATEUR = "|";

        private List<(TypesJeton TypeJeton, string Pattern)> _regles = new();
        private Regex _regex;

        public void AjouterRegle(TypesJeton typeJeton, string pattern)
        {
            _regles.Add((typeJeton, pattern));

            IEnumerable<string> patterns = _regles.Select(r => $"(?<{r.TypeJeton}>{r.Pattern})");
            _regex = new Regex(string.Join(SEPARATEUR, patterns));
        }

        public IEnumerable<Jeton> Extraire(string equation)
        {
            foreach (Match match in _regex.Matches(equation))
            {
                foreach ((TypesJeton TypeJeton, string Pattern) regle in _regles)
                {
                    string typeJeton = regle.TypeJeton.ToString();

                    if (match.Groups[typeJeton].Success)
                    {
                        yield return new Jeton(regle.TypeJeton, match.Value);
                        break;
                    }
                }
            }

            yield return new Jeton(TypesJeton.Fin, string.Empty);
        }
    }
}
