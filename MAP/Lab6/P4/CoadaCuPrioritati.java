import java.io.*;

public class CoadaCuPrioritati<T extends Comparable<T> & Serializable> implements ICoada<T> {
    CoadaCuPrioritati(){
        this.primul = null;
        this.dimensiune = 0;
    }
    
    CoadaCuPrioritati(String numeFisier) throws FileNotFoundException, IOException{
        ObjectInputStream file = new ObjectInputStream(new FileInputStream(numeFisier));
        try{
            this.readObject(file);
        }
        catch (ClassNotFoundException e){
        }
        file.close();
    }
    
    @Override
    public void insereaza(T element){
        Nod pre = null, i = this.primul;
        if (this.primul == null)
            this.primul = new Nod(element);
        else{
            while (i != null && i.element.compareTo(element) < 0){
                pre = i;
                i = i.urmator;
            }
            if (pre == null)
                this.primul = new Nod(element, this.primul);
            else
                pre.urmator = new Nod(element, i);
        }
        this.dimensiune++;
    }

    @Override
    public T elimina() throws TADException{
        Nod elementEliminat = this.primul;
        if (this.primul != null){
            this.primul = this.primul.urmator;
            this.dimensiune--;
            return elementEliminat.element;
        }
        else
            throw new TADException("Lista cu prioritati este vida! Nu se pot elimina elemente!");
    }

    @Override
    public T element() throws TADException{
        if (this.primul != null)
            return this.primul.element;
        else
            throw new TADException("Lista cu prioritati este vida! Nu exista un cel mai prioritar element!");
    }

    @Override
    public Iterator<T> iterator(){
        return new IteratorCoadaCuPrioritati();
    }
    
    @Override
    public void scrieInFisier(String numeFisier) throws FileNotFoundException, IOException{
        ObjectOutputStream file = new ObjectOutputStream(new FileOutputStream(numeFisier));
        this.writeObject(file);
        file.close();
    }

    @Override
    public int dimensiune(){
        return this.dimensiune;
    }

    @Override
    public boolean eVida(){
        return this.dimensiune == 0;
    }
    
    private void writeObject(ObjectOutputStream stream) throws IOException{
        try{
            for (Iterator<T> it = iterator(); it.eValid(); it.urmator())
                stream.writeObject(it.element());
            stream.writeObject(null);
        }
        catch (IteratorException e){
        }
    }
    
    @SuppressWarnings("unchecked")
    private void readObject(ObjectInputStream stream) throws IOException, ClassNotFoundException{
        T element;
        Boolean ok = true;
        do{
            element = (T)stream.readObject();
            if (element != null)
                this.insereaza(element);
            else
                ok = false;
        }while (ok);
    }
    
    private class Nod{
        private final T element;
        private Nod urmator;
        
        public Nod(T element){
            this.element = element;
            this.urmator = null;
        }
        
        public Nod(T element, Nod urmator){
            this(element);
            this.urmator = urmator;
        }
    }
    
    private class IteratorCoadaCuPrioritati implements Iterator<T>{
        public IteratorCoadaCuPrioritati(){
            this.curent = primul;
        }
        
        @Override
        public void urmator() throws IteratorException{
            if (this.curent != null)
                this.curent = this.curent.urmator;
            else
                throw new IteratorException("Iteratorul este invalid!");
        }

        @Override
        public T element() throws IteratorException{
            if (this.curent != null)
                return this.curent.element;
            else
                throw new IteratorException("Iteratorul este invalid!");
        }

        @Override
        public boolean eValid(){
            return this.curent != null;
        }
        
        private Nod curent;
    }

    private Nod primul;
    private int dimensiune;
    private static final long serialVersionUID = 1L;
}
