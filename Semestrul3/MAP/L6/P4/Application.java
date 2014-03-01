public class Application {
    public static void main(String[] args){
        int suma = 0, latura, arie = 0;
        ICoada<Patrat> cp = new CoadaCuPrioritati<Patrat>();
        cp.insereaza(new Patrat(2));
        cp.insereaza(new Patrat(3));
        cp.insereaza(new Patrat(1));
        cp.insereaza(new Patrat(5));
        System.out.println("Laturile patratelor ordonate crescator sunt: ");
        try{
            for (Iterator<Patrat> it = cp.iterator(); it.eValid(); it.urmator()){
                latura = it.element().getLatura();
                suma += latura;
                arie += latura * latura;
                System.out.println(latura);
            }
        }
        catch (IteratorException ex){
            System.out.println("--> EROARE: Iteratorul este valid <--");
        }

        System.out.println("Suma laturilor patratelor este " + suma);
        System.out.println("Iar aria totala este " + suma * suma + " sau " + arie + "?");
    }
}
