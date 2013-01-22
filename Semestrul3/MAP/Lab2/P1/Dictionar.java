public class Dictionar implements IDictionar{
    public Dictionar(){
        this.dimensiune = 0;
        this.primul = null;
    }
    
    @Override
    public boolean adauga(Object cheie, Object valoare){
        if (this.element(cheie) != null)
            return false;
        else{
            if (this.primul == null)
                this.primul = new Nod(cheie, valoare);
            else
                this.primul = new Nod(cheie, valoare, this.primul);
            this.dimensiune++;
            return true;
        }
    }

    @Override
    public Object elimina(Object cheie){
        Nod pre = null, i = this.primul, sters;
        while (i != null && !i.cheie.equals(cheie)){
            pre = i;
            i = i.urmator;
        }
        if (i != null){
            sters = i;
            if (pre == null)
                this.primul = this.primul.urmator;
            else
                pre.urmator = i.urmator;
            this.dimensiune--;
            return sters.valoare;
        }
        else
            return null;
    }

    @Override
    public Object element(Object cheie){
        Nod i = this.primul;
        while (i != null && !i.cheie.equals(cheie))
            i = i.urmator;
        if (i != null)
            return i.valoare;
        else
            return null;
    }

    @Override
    public boolean contine(Object cheie, Object valoare){
        Nod i = this.primul;
        while (i != null && !i.cheie.equals(cheie))
            i = i.urmator;
        if (i == null)
            return false;
        else
            return i.valoare.equals(valoare);
    }

    @Override
    public Iterator iterator(){
        return new IteratorDictionar();
    }
    
    @Override
    public int dimensiune(){
        return this.dimensiune;
    }

    @Override
    public boolean eVid(){
        return this.dimensiune == 0;
    }
    
    private class Nod{
        public final Object valoare;
        public final Object cheie;
        public Nod urmator;
        
        public Nod(Object cheie, Object valoare){
            this(cheie, valoare, null);
        }
        
        public Nod(Object cheie, Object valoare, Nod urmator){
            this.cheie = cheie;
            this.valoare = valoare;
            this.urmator = urmator;
        }
    }
    
    private class IteratorDictionar implements Iterator{
        IteratorDictionar(){
            this.curent = primul;
        }
        
        @Override
        public void urmator(){
            if (this.curent != null)
                this.curent = this.curent.urmator;
        }

        @Override
        public Object element(){
            if (this.curent != null)
                return this.curent.valoare;
            else
                return null;
        }

        @Override
        public boolean eValid(){
            return this.curent != null;
        }
        
        private Nod curent;
    }

    private int dimensiune;
    private Nod primul;
}
