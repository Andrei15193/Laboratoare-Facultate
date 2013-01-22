public interface ICoada<T extends Comparable<T>>{
    void insereaza(T element);

    T elimina();

    T element();

    Iterator<T> iterator();

    int dimensiune();

    boolean eVida();
}
