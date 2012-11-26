public class IntegerDestringer implements Destringer<Integer>{
    public Integer createObjectFromString(String str){
        try{
            return new Integer(str);
        }
        catch (Exception e){
            return 0;
        }
    }
    
    public String createStringFromObject(Integer i){
        return i.toString();
    }
}
