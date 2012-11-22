public class TADException extends Exception {
    public TADException(){
        super();
    }

    public TADException(String message){
        super(message);
    }

    public TADException(String message, Throwable cause){
        super(message, cause);
    }

    public TADException(Throwable cause){
        super(cause);
    }

    private static final long serialVersionUID = 1L;
}
