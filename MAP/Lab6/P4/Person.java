import java.io.Serializable;

public class Person implements Comparable<Person>, Serializable{
    public Person(String nume, int cnp){
        this.nume = nume.trim();
        this.cnp = cnp;
    }
    
    public String getNume() {
        return nume;
    }
    
    public void setNume(String nume) {
        this.nume = nume;
    }

    public int getCnp() {
        return cnp;
    }

    public void setCnp(int cnp) {
        this.cnp = cnp;
    }
    
    @Override
    public int compareTo(Person p){
        return this.cnp - p.cnp;
    }
    
    public boolean equals(Object obj){
        Person p;
        if (obj instanceof Person){
            p = (Person)obj;
            return (this.nume.equals(p.nume) && this.cnp == p.cnp);
        }
        else
            return false;
    }

    private String nume;
    private int cnp;
    private static final long serialVersionUID = 1L;
}
