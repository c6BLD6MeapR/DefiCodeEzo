using ConsoleCalculatrice;
using ConsoleCalculatrice.Analyseurs;
using ConsoleCalculatrice.Composite;

Lexer lexer = new Lexer();

string regleNombre = @"\d+(\.\d+)?";
string reglePlus = @"\+";
string regleMoins = @"-";

lexer.AjouterRegle(TypesJeton.Nombre, regleNombre);
lexer.AjouterRegle(TypesJeton.Plus, reglePlus);
lexer.AjouterRegle(TypesJeton.Moins, regleMoins);
 
string equation = "-1 + 1";
IEnumerable<Jeton> jetons = lexer.Extraire(equation);
AnalyseurExpression analyseur = new AnalyseurExpression(jetons);

analyseur.Enregistrer(TypesJeton.Nombre, new AnalyseurNombre());

Dictionary<TypesJeton, Func<double, double>> modelesUnaire = new()
{
    { TypesJeton.Moins, (n) => -n },
};

analyseur.Enregistrer(TypesJeton.Moins, new AnalyseurUnaire(50, modelesUnaire));

Dictionary<TypesJeton, Func<double, double, double>> modelesBinaire = new()
{
    { TypesJeton.Plus, (g, d) => g + d },
};

analyseur.Enregistrer(TypesJeton.Plus, new AnalyseurOperationBinaire(10, modelesBinaire));

IExpression expression = analyseur.AnalyserExpression();

Console.WriteLine($"{equation} = {expression.Evaluer()}");
