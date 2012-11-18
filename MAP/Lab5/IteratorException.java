public class IteratorException extends Exception {
    public IteratorException(){
        super();
    }
    
    public IteratorException(String message){
        super(message);
    }
    
    public IteratorException(String message, Throwable cause){
        super(message, cause);
    }
    
    public IteratorException(Throwable cause){
        super(cause);
    }
    
    private static final long serialVersionUID = 1L;
}
