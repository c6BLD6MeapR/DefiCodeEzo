namespace ConsoleCalculatrice.Composite
{
    public class ExpressionOperationBinaire : IExpression
    {
        private readonly IExpression _gauche;
        private readonly IExpression _droite;
        private readonly Func<double, double, double> _operationBinaire;

        public ExpressionOperationBinaire(IExpression gauche, IExpression droite, Func<double, double, double> operationBinaire)
        {
            _gauche = gauche;
            _droite = droite;
            _operationBinaire = operationBinaire;
        }

        public double Evaluer() => _operationBinaire(_gauche.Evaluer(), _droite.Evaluer());
       
    }
}
