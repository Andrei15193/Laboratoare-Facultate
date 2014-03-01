public class PersonDestringer implements Destringer<Person>{
    public Person createObjectFromString(String str){
        String[] fields = str.split(" \\| ");
        return new Person(fields[0], new Integer(fields[1].trim()));
    }
    
    public String createStringFromObject(Person p){
        return String.format("%-30.30s | %-13d", p.getNume(), p.getCnp());
    }
}
