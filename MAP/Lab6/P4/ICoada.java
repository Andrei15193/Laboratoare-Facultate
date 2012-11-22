import java.io.*;

public interface ICoada<T extends Comparable<T> & Serializable> extends Serializable{
    void insereaza(T element);

    T elimina() throws TADException;

    T element() throws TADException;

    Iterator<T> iterator();
    
    void scrieInFisier(String numeFisier) throws FileNotFoundException, IOException;

    int dimensiune();

    boolean eVida();
}
