package controller;

// Represents the interface of an iterator on domain entities.
public interface EntityReader<T>{
    // Gets the current entity in the iteration. If there is no current entity null
    // is returned.
    T getCurrentEntity();
    
    // Moves the iterator on the next position. Returns true if succeeded, false
    // otherwise. If false is returned then the iterator is closed automatically.
    // A next() call must be performed before attempting to read any data.
    Boolean next();
    
    // Closes the iterator. This is useful when there is no need to iterate through
    // the entire container and only a few Firms are of interest. To make sure all
    // allocated resources are freed call this method.
    // In some cases there is no need for a close method (iterating through a
    // collection that's stored in internal memory), however when iterating through
    // a file it is most likely that the burden of "closing the file" lays upon the
    // iterator. A similar scenario can happen when iterating a database response.
    void close();
}
