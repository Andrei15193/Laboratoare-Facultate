public interface ICoada<T extends Comparable<T>>{
    void insereaza(T element);

    T elimina() throws TADException;

    T element() throws TADException;

    Iterator<T> iterator();

    int dimensiune();

    boolean eVida();
}
