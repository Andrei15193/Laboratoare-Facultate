package aop5.persistence.database;

import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.SQLException;

public class JdbcUtils
{
    public static JdbcUtils getInstance()
    {
        if (JdbcUtils.instance == null)
            JdbcUtils.instance = new JdbcUtils();
        return JdbcUtils.instance;
    }

    public Connection getConnection()
    {
        try
        {
            if (this.connection == null || this.connection.isClosed())
                this.connection = this.getNewConnection();
        }
        catch (SQLException e)
        {
            System.out.println("Error @ DB " + e);
        }
        return this.connection;
    }

    private Connection getNewConnection()
    {
        String driver = "sun.jdbc.odbc.JdbcOdbcDriver";
        String url = "jdbc:odbc:catalog";
        Connection con = null;
        try
        {
            Class.forName(driver);
            con = DriverManager.getConnection(url);
        }
        catch (ClassNotFoundException e)
        {
            System.out.println("Error loading driver " + e);
        }
        catch (SQLException e)
        {
            System.out.println("Error getting connection " + e);
        }
        return con;
    }

    private JdbcUtils()
    {
    }

    private static JdbcUtils instance = null;
    private Connection connection = null;
}
