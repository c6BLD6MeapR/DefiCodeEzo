using ConsoleCalculatrice.Composite;

namespace ConsoleCalculatrice.Analyseurs
{
    public interface IAnalyseurPrefix
    {
        IExpression AnalyserExpression(AnalyseurExpression analyseur, Jeton jeton);
    }
}
