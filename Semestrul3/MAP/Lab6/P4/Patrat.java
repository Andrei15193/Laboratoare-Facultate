import java.io.Serializable;

class Patrat implements Comparable<Patrat>, Serializable{
    public Patrat(int latura){
        this.latura = latura;
    }
    
    @Override
    public int compareTo(Patrat dreapta){
        return this.latura - ((Patrat)dreapta).latura;
    }
    
    public int getLatura(){
        return this.latura;
    }
    
    public int getArie(){
        return this.latura * this.latura;
    }
    
    private int latura;
    private static final long serialVersionUID = 1L;
}
