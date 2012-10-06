// Determina varsta (in numar de zile) pentru o persoana.

public class P3 {
    public static void main(String[] args){
        try{
            java.util.Date today = new java.util.Date(),
                           givenDate = (new java.text.SimpleDateFormat("dd/MM/yyyy")).parse(args[0] + "/" + args[1] + "/" + args[2]);
            if (today.after(givenDate))
                System.out.println("Varsta ta in zile este egala cu " + ((today.getTime() - givenDate.getTime()) / (1000 * 3600 * 24)));
            else
                System.out.println("Ai introdus o data mai tarzie decat cea curenta");
        }
        catch (java.lang.Throwable exception){
            System.out.println("Utilizare: ZZ LL AAAA, formatul numeric al zilei de nastere");
        }
    }
}
