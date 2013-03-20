package aop1.utils;

import java.io.File;
import java.io.FileNotFoundException;
import java.io.FileOutputStream;

import org.apache.log4j.Level;

import aop1.Application;

public class Utils
{
    public static void renameFile(final String sourceName,
                    final String destinationName)
    {
        Application.logger.log(Level.INFO, "Utils.renameFile(" + sourceName
                        + " :String, " + destinationName + " :String)");
        java.io.File file = new java.io.File(sourceName);
        java.io.File renameTo = new java.io.File(destinationName);
        file.renameTo(renameTo);
    }

    public static void removeFile(final String fileName)
    {
        Application.logger.log(Level.INFO, "Utils.removeFile(" + fileName
                        + " :String)");
        java.io.File file = new java.io.File(fileName);
        file.delete();
    }

    public static void closeStream(final java.io.OutputStream outputStream)
    {
        Application.logger.log(Level.INFO, "Utils.closeStream(" + outputStream
                        + " :OutputString)");
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
        Application.logger.log(Level.INFO, "Utils.closeStream(" + inputStream
                        + " :InputString)");
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
        Application.logger.log(Level.INFO, "Utils.createFile(" + fileName
                        + " :String)");
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
        Application.logger.log(Level.INFO, "Utils.checkIfFileExists("
                        + filePath + " :String)");
        File file = new File(filePath);
        return file.exists();
    }

    public static boolean checkIfFileIsNotDirectory(final String filePath)
    {
        Application.logger.log(Level.INFO, "Utils.checkIfFileIsNotDirectory("
                        + filePath + " :String)");
        File file = new File(filePath);
        return !file.isDirectory();
    }

    public static final String nameRegEx = "^([a-zA-Z]+[ -]?)*[a-zA-Z]+$";
}
