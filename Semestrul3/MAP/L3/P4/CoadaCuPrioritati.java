public class CoadaCuPrioritati<T extends Comparable<T>> implements ICoada<T> {
    CoadaCuPrioritati(){
        this.primul = null;
        this.dimensiune = 0;
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
    public T elimina(){
        Nod elementEliminat = this.primul;
        if (this.primul != null){
            this.primul = this.primul.urmator;
            this.dimensiune--;
            return elementEliminat.element;
        }
        else
            return null;
    }

    @Override
    public T element(){
        if (this.primul != null)
            return this.primul.element;
        else
            return null;
    }

    @Override
    public Iterator<T> iterator(){
        return new IteratorCoadaCuPrioritati();
    }

    @Override
    public int dimensiune(){
        return this.dimensiune;
    }

    @Override
    public boolean eVida(){
        return this.dimensiune == 0;
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
        public void urmator(){
            if (this.curent != null)
                this.curent = this.curent.urmator;
        }

        @Override
        public T element(){
            if (this.curent != null)
                return this.curent.element;
            else
                return null;
        }

        @Override
        public boolean eValid(){
            return this.curent != null;
        }
        
        private Nod curent;
    }

    private Nod primul;
    private int dimensiune;
}
