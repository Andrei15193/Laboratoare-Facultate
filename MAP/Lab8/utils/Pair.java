package utils;

public class Pair<T1, T2>
{
    public Pair(T1 first, T2 second)
    {
        this.first = first;
        this.second = second;
    }

    @Override
    public boolean equals(Object object)
    {
        boolean result;
        try
        {
            @SuppressWarnings("unchecked")
            Pair<T1, T2> pair = (Pair<T1, T2>)object;
            result = this.first.equals(pair.first)
                            && this.second.equals(pair.second);
        }
        catch (ClassCastException e)
        {
            result = false;
        }
        return result;
    }

    public T1 first;
    public T2 second;
}
