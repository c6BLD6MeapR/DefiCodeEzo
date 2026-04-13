
using ConsoleCalculatrice;

Lexer lexer = new Lexer();

string regleNombre = @"\d+(\.\d+)?";
string reglePlus = @"[\+]";

lexer.AjouterRegle(TypesJeton.Nombre, regleNombre);
lexer.AjouterRegle(TypesJeton.Plus, reglePlus);

string equation = "1.5445 + 1";
IEnumerable<Jeton> jetons = lexer.Extraire(equation);

 Console.WriteLine();





