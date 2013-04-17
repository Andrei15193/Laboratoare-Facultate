package aop3.utils;

import java.io.IOException;
import java.io.ObjectOutputStream;
import java.io.OutputStream;

public class AppendingObjectOutputStream extends ObjectOutputStream
{
    public AppendingObjectOutputStream(OutputStream outputStream)
                    throws IOException
    {
        super(outputStream);
    }

    @Override
    protected void writeStreamHeader() throws IOException
    {
        this.reset();
    }
}
