package utils;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;

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

    public static void closeStream(final java.io.InputStream inputStream)
    {
        try
        {
            inputStream.close();
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

    public static boolean checkIfFileExists(final String filePath)
    {
        File file = new File(filePath);
        return file.exists();
    }

    public static boolean checkIfFileIsNotDirectory(final String filePath)
    {
        File file = new File(filePath);
        return !file.isDirectory();
    }

    public static final String nameRegEx = "^([a-zA-Z]+[ -]?)*[a-zA-Z]+$";
}
