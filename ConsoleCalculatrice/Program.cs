
using ConsoleCalculatrice;
using ConsoleCalculatrice.Analyseurs;
using ConsoleCalculatrice.Composite;

Lexer lexer = new Lexer();

string regleNombre = @"\d+(\.\d+)?";
string reglePlus = @"[\+]";

lexer.AjouterRegle(TypesJeton.Nombre, regleNombre);
lexer.AjouterRegle(TypesJeton.Plus, reglePlus);

string equation = "1 + 2 + 3";
IEnumerable<Jeton> jetons = lexer.Extraire(equation);
AnalyseurExpression analyseur = new AnalyseurExpression(jetons);
analyseur.Enregistrer(TypesJeton.Nombre, new AnalyseurNombre()); 

Dictionary<TypesJeton, Func<double, double, double>> modele = new()
{
    { TypesJeton.Plus, (g, d) => g + d },
};

analyseur.Enregistrer(TypesJeton.Plus, new AnalyseurOperationBinaire(10, modele));

IExpression expression = analyseur.AnalyserExpression();

Console.WriteLine($"{equation} = {expression.Evaluer()}");





