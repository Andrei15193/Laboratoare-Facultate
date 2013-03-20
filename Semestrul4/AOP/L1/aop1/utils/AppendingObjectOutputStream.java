package aop1.utils;

import java.io.IOException;
import java.io.ObjectOutputStream;
import java.io.OutputStream;

import org.apache.log4j.Level;

import aop1.Application;

public class AppendingObjectOutputStream extends ObjectOutputStream
{
    public AppendingObjectOutputStream(OutputStream outputStream)
                    throws IOException
    {
        super(outputStream);
        Application.logger.log(Level.INFO,
                        "AppendingObjectOutputStream.AppendingObjectOutputStream("
                                        + outputStream + " :OutputStream)");
    }

    @Override
    protected void writeStreamHeader() throws IOException
    {
        Application.logger.log(Level.INFO,
                        "AppendingObjectOutputStream.writeStreamHeader()");
        this.reset();
    }
}
