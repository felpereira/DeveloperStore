namespace Ambev.DeveloperEvaluation.Domain.ValueObjects
{

    public class Rating
    {
        private Rating() { }

        public Rating(double rate, int count)
        {
            Rate = rate;
            Count = count;
        }

        public double Rate { get; private set; }
        public int Count { get; private set; }
    }
}
