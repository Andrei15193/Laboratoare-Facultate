class Patrat implements Comparable<Patrat>{
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
}
