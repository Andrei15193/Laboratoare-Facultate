package utils;

import java.io.FileNotFoundException;
import java.io.FileOutputStream;

import data.iterators.StreamIterator;

public class Utils
{
    public static void renameFile(final String sourceName,
                    final String destinationName)
    {
        java.io.File file = new java.io.File(sourceName);
        java.io.File renameTo = new java.io.File(destinationName);
        file.renameTo(renameTo);
    }

    public static void removeFile(final String fileName)
    {
        java.io.File file = new java.io.File(fileName);
        file.delete();
    }

    public static void closeStream(final java.io.OutputStream outputStream)
    {
        try
        {
            outputStream.close();
        }
        catch (Exception e)
        {
        }
    }

    public static void createFile(final String fileName)
                    throws FileNotFoundException
    {
        FileOutputStream file = null;
        try
        {
            file = new FileOutputStream(fileName);
        }
        finally
        {
            Utils.closeStream(file);
        }
    }

    public static <E>StreamIterator<E> buildStreamIteratorOverEmptyCollection()
    {
        return new StreamIterator<E>()
        {
            @Override
            public boolean hasNext()
            {
                return false;
            }

            @Override
            public E next()
            {
                throw new java.util.NoSuchElementException(
                                "There is no element left in the current iteration.");
            }

            @Override
            public void remove()
            {
                throw new UnsupportedOperationException(
                                "The remove method is not supported for stream iterators.");
            }

            @Override
            public void close()
            {
            }
        };
    }
}
