package servlets;
import java.io.IOException;
import java.sql.Connection;
import java.sql.DriverManager;
import java.sql.ResultSet;
import java.sql.SQLException;
import javax.servlet.ServletException;
import javax.servlet.annotation.WebServlet;
import javax.servlet.http.HttpServlet;
import javax.servlet.http.HttpServletRequest;
import javax.servlet.http.HttpServletResponse;
import javax.servlet.http.HttpSession;
@WebServlet("/Default")
public class Default extends HttpServlet
{
	@Override
	public void init() throws ServletException
	{
		String dataBaseURL = "jdbc:mysql://localhost:3306/local";
		String dataBaseUser = "root";
		String dataBasePassword = "";
		try
		{
			Class.forName("com.mysql.jdbc.Driver");
			_connection = DriverManager.getConnection(dataBaseURL,
					dataBaseUser, dataBasePassword);
		}
		catch (SQLException e)
		{
			System.err.println("No SQL Connection (start the server?)");
		}
		catch (ClassNotFoundException e)
		{
			System.err.println("No MySql Driver (fix it)");
		}
	}
	@Override
	public void destroy()
	{
		try
		{
			_connection.close();
		}
		catch (SQLException e)
		{
		}
	}
	@Override
	protected void doGet(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException
	{
		_ShowView(request, response);
	}
	@Override
	protected void doPost(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException
	{
		String nume = request.getParameter("nume");
		String prof = request.getParameter("prof");
		HttpSession session = request.getSession();
		Curs cursCurent = (Curs) session.getAttribute("curs");
		if (request.getParameter("navigare").equals("Anterior"))
			session.setAttribute("curs", _cursulAnterior(cursCurent));
		else
			session.setAttribute("curs", _urmatorulCurs(cursCurent));
		if (!cursCurent.getNume().equals(nume)
				|| !cursCurent.getProf().equals(prof))
			try
			{
				_connection.createStatement().execute(
						"update cursuri set nume = '" + nume
								+ "', prof = '" + prof
								+ "' where nume = '"
								+ cursCurent.getNume() + "' and prof = '"
								+ cursCurent.getProf() + "'");
			}
			catch (SQLException e)
			{
				e.printStackTrace();
			}
		_ShowView(request, response);
	}
	private void _ShowView(HttpServletRequest request,
			HttpServletResponse response) throws ServletException, IOException
	{
		HttpSession session = request.getSession();
		Curs cursCurent = (Curs) session.getAttribute("curs");
		if (cursCurent == null)
		{
			cursCurent = _primulCurs();
			session.setAttribute("curs", cursCurent);
		}
		else
			cursCurent = (Curs) session.getAttribute("curs");
		System.err.println("Curs curent: " + cursCurent.toString());
		response.setCharacterEncoding("UTF-8");
		response.getWriter()
				.printf("<!DOCTYPE html5>"
						+ "<html>"
						+ "<head>"
						+ "<title>%s</title>"
						+ "<meta http-equiv=\"content-type\" content=\"text/html; charset=UTF-8\">"
						+ "<style>"
						+ "body"
						+ "{"
						+ "font-family: Arial;"
						+ "}"
						+ "label"
						+ "{"
						+ "display: inline-block;"
						+ "width: 100px;"
						+ "}"
						+ "input.text"
						+ "{"
						+ "margin-bottom: 10px;"
						+ "width: 300px;"
						+ "}"
						+ "input.buttonLeft"
						+ "{"
						+ "float: left;"
						+ "}"
						+ "input.buttonRight"
						+ "{"
						+ "float: right;"
						+ "}"
						+ "form"
						+ "{"
						+ "margin:0 auto;"
						+ "width: 400px;"
						+ "}"
						+ "</style>"
						+ "</head>"
						+ "<body>"
						+ "<form action=\"/L8/Default\" method=\"POST\">"
						+ "<h1>Cursuri</h1>"
						+ "<label>Nume curs: </label><input class=\"text\" type=\"text\" name=\"nume\" value=\"%s\" /><br />"
						+ "<label>Profesor: </label><input class=\"text\" type=\"text\" name=\"prof\" value=\"%s\" /><br />"
						+ "<input class=\"buttonLeft\" type=\"submit\" name=\"navigare\" value=\"Anterior\" />"
						+ "<input class=\"buttonRight\" type=\"submit\" name=\"navigare\" value=\"Următor\" />"
						+ "</form>"
						+ "</body>"
						+ "</html>",
						"Programare Web Laborator 8",
						cursCurent.getNume(),
						cursCurent.getProf());
	}
	private Curs _urmatorulCurs(Curs cursCurent)
	{
		ResultSet resultSet = null;
		try
		{
			System.err.println("NEXT");
			resultSet = _connection
					.createStatement()
					.executeQuery(
							String.format("select nume, prof "
									+ " from cursuri"
									+ " where (nume = '%s' and prof > '%s')"
									+ " or nume > '%s'"
									+ " order by nume, prof",
									cursCurent.getNume(), cursCurent.getProf(),
									cursCurent.getNume()));
			if (resultSet.next())
				return new Curs(resultSet.getString("nume"),
						resultSet.getString("prof"));
			else
				return _primulCurs();
		}
		catch (SQLException e)
		{
			e.printStackTrace();
			return null;
		}
		finally
		{
			try
			{
				if (resultSet != null)
					resultSet.close();
			}
			catch (SQLException e)
			{
				e.printStackTrace();
			}
		}
	}
	private Curs _cursulAnterior(Curs cursCurent)
	{
		ResultSet resultSet = null;
		try
		{
			System.err.println("PREV");
			resultSet = _connection
					.createStatement()
					.executeQuery(
							String.format("select nume, prof "
									+ " from cursuri"
									+ " where (nume = '%s' and prof < '%s')"
									+ " or nume < '%s'"
									+ " order by nume, prof",
									cursCurent.getNume(), cursCurent.getProf(),
									cursCurent.getNume()));
			Curs curs = null;
			while (resultSet.next())
				curs = new Curs(resultSet.getString("nume"),
						resultSet.getString("prof"));
			if (curs == null)
				curs = _ultimulCurs();
			return curs;
		}
		catch (SQLException e)
		{
			e.printStackTrace();
			return null;
		}
		finally
		{
			try
			{
				if (resultSet != null)
					resultSet.close();
			}
			catch (SQLException e)
			{
				e.printStackTrace();
			}
		}
	}
	private Curs _primulCurs()
	{
		ResultSet resultSet = null;
		try
		{
			System.err.println("FIRST");
			resultSet = _connection
					.createStatement()
					.executeQuery(
							"select nume, prof"
									+ " from cursuri"
									+ " order by nume, prof");
			resultSet.next();
			return new Curs(resultSet.getString("nume"),
					resultSet.getString("prof"));
		}
		catch (SQLException e)
		{
			e.printStackTrace();
			return null;
		}
		finally
		{
			try
			{
				if (resultSet != null)
					resultSet.close();
			}
			catch (SQLException e)
			{
				e.printStackTrace();
			}
		}
	}
	private Curs _ultimulCurs()
	{
		ResultSet resultSet = null;
		try
		{
			System.err.println("LAST");
			resultSet = _connection
					.createStatement()
					.executeQuery(
							"select nume, prof"
									+ " from cursuri"
									+ " order by nume, prof");
			Curs curs = null;
			while (resultSet.next())
				curs = new Curs(resultSet.getString("nume"),
						resultSet.getString("prof"));
			return curs;
		}
		catch (SQLException e)
		{
			System.err.println(e.toString());
			return null;
		}
		finally
		{
			try
			{
				if (resultSet != null)
					resultSet.close();
			}
			catch (SQLException e)
			{
				e.printStackTrace();
			}
		}
	}
	private static final long serialVersionUID = 1L;
	private Connection _connection = null;
}