// Compara doua obiecte de tip Integer
public class ComparatieInt implements IRelatieDeOrdine{
    @Override
    // Returneaza true daca stanga <= cu dreapta, fals altfel.
    public boolean compara(Object stanga, Object dreapta){
        if (stanga instanceof Integer && dreapta instanceof Integer)
            return ((Integer)stanga).compareTo((Integer)dreapta) <= 0;
        else
            return false;
    }
}
