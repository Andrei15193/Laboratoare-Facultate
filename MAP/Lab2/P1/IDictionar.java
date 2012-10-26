public interface IDictionar{
    boolean adauga(Object cheie, Object valoare);

    Object elimina(Object cheie);

    Object element(Object cheie);

    boolean contine(Object cheie, Object valoare);

    Iterator iterator();

    int dimensiune();

    boolean eVid();
}
