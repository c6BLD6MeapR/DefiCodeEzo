namespace ConsoleCalculatrice.Composite
{
    public class ExpressionNombre : IExpression
    {
        private readonly double _nombre;

        public ExpressionNombre(double nombre) => _nombre = nombre;

        public double Evaluer() => _nombre;
    }
}
