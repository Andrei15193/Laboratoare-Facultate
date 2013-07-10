namespace IALab406
{
    public interface IChromosome<T> where T : IChromosome<T>
    {
        T Mutate();

        T CrossOver(T chromosome);

        double Apply(params double[] input);
    }
}
