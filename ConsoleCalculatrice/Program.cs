using ConsoleCalculatrice;
using ConsoleCalculatrice.Analyseurs;
using ConsoleCalculatrice.Composite;
using System.Globalization;

Lexer lexer = new Lexer();

string regleNombre = @"\d+(\.\d+)?";
string reglePlus = @"\+";
string regleMoins = @"-";
string regleMultiplication = @"\*";
string regleDivision = @"/";

lexer.AjouterRegle(TypesJeton.Nombre, regleNombre);
lexer.AjouterRegle(TypesJeton.Plus, reglePlus);
lexer.AjouterRegle(TypesJeton.Moins, regleMoins);
lexer.AjouterRegle(TypesJeton.Multiplication, regleMultiplication);
lexer.AjouterRegle(TypesJeton.Division, regleDivision);

string equation = "-2 * -3";
IEnumerable<Jeton> jetons = lexer.Extraire(equation);
AnalyseurExpression analyseur = new AnalyseurExpression(jetons);

Func<string, double> modeleNombre = (s) =>
{
    if(double.TryParse(s, CultureInfo.InvariantCulture, out double d)) { return d; }

    throw new FormatException($"La valeur du jeton {s} n'est pas un nombre valide.");
};

analyseur.Enregistrer(TypesJeton.Nombre, new AnalyseurNombre(modeleNombre));

Dictionary<TypesJeton, Func<double, double>> modelesUnaire = new()
{
    { TypesJeton.Moins, (n) => -n },
};

analyseur.Enregistrer(TypesJeton.Moins, new AnalyseurUnaire(50, modelesUnaire));

Dictionary<TypesJeton, Func<double, double, double>> modelesBinaire = new()
{
    { TypesJeton.Plus, (g, d) => g + d },
    { TypesJeton.Moins, (g, d) => g - d },
    { TypesJeton.Multiplication, (g, d) => g * d },
    { TypesJeton.Division, (g, d) => g / d },
};

analyseur.Enregistrer(TypesJeton.Plus, new AnalyseurOperationBinaire(10, modelesBinaire));
analyseur.Enregistrer(TypesJeton.Moins, new AnalyseurOperationBinaire(10, modelesBinaire));
analyseur.Enregistrer(TypesJeton.Multiplication, new AnalyseurOperationBinaire(20, modelesBinaire));
analyseur.Enregistrer(TypesJeton.Division, new AnalyseurOperationBinaire(20, modelesBinaire));

IExpression expression = analyseur.AnalyserExpression();

Console.WriteLine($"{equation} = {expression.Evaluer()}");