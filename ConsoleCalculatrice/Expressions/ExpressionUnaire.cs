using ConsoleCalculatrice.Composite;

namespace ConsoleCalculatrice.Expressions
{
    public class ExpressionUnaire : IExpression
    {
        private readonly IExpression _expression;
        private readonly Func<double, double> _operationUnaire;

        public ExpressionUnaire(IExpression expression, Func<double, double> operationUnaire)
        {
            _expression = expression;
            _operationUnaire = operationUnaire;
        }

        public double Evaluer()
        {
            return _operationUnaire(_expression.Evaluer());
        }
    }
}
