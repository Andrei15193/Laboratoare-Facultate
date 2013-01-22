public interface Destringer<T>{
    T createObjectFromString(String str);
    
    String createStringFromObject(T element);
}
