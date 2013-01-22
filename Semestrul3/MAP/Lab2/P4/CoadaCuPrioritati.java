public class CoadaCuPrioritati implements ICoada{
    CoadaCuPrioritati(IRelatieDeOrdine relatie){
        this.relatie = relatie;
        this.primul = null;
        this.dimensiune = 0;
    }

    @Override
    public void insereaza(Object element){
        Nod pre = null, i = this.primul;
        if (this.primul == null)
        	this.primul = new Nod(element);
        else{
	        while (i != null && this.relatie.compara(i.element, element)){
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
    public Object elimina(){
        Object element = null;
        if (this.primul != null){
            element = this.primul.element;
            this.primul = this.primul.urmator;
            this.dimensiune--;
        }
        return element;
    }

    @Override
    public Object element(){
        if (this.primul != null)
            return this.primul.element;
        else
            return null;
    }

    @Override
    public Iterator iterator() {
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
        public final Object element;
        public Nod urmator = null;

        public Nod(Object element){
            this.element = element;
        }

        public Nod(Object element, Nod urmator){
            this(element);
            this.urmator = urmator;
        }
    }

    private class IteratorCoadaCuPrioritati implements Iterator{
        private IteratorCoadaCuPrioritati(){
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
                return this.curent.element;
            else
                return null;
        }

        @Override
        public boolean eValid() {
            return this.curent != null;
        }

        private Nod curent;
    }

    private IRelatieDeOrdine relatie;
    private Nod primul;
    private int dimensiune;
}
