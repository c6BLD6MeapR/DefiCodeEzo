using ConsoleCalculatrice.Composite;

namespace ConsoleCalculatrice.Analyseurs
{
    public interface IAnalyseurInfix
    {
        int Priorite { get; }
        IExpression AnalyseurExpression(AnalyseurExpression analyseur, IExpression gauche, Jeton jeton);
    }
}
