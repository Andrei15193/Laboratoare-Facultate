<%@page import="java.net.HttpRetryException"%>
<%@page import="org.omg.CORBA.Request"%>
<%@ page language="java" contentType="text/html; charset=UTF-8"
	pageEncoding="UTF-8" import="java.sql.*,stuff.*,javax.servlet.http.*"%>
<%!public void jspInit()
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
	public void jspDestory()
	{
		try
		{
			_connection.close();
		}
		catch (SQLException e)
		{
		}
	}
	private Curs _getCursCurent(HttpServletRequest request)
	{
		HttpSession session = request.getSession();
		Curs cursCurent = (Curs) session.getAttribute("curs");
		if (cursCurent == null)
		{
			cursCurent = _primulCurs();
			session.setAttribute("curs", cursCurent);
		}
		else
		{
			cursCurent = (Curs) session.getAttribute("curs");
			String nume = request.getParameter("nume");
			String prof = request.getParameter("prof");
			if (nume != null && prof != null)
			{
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
										+ cursCurent.getNume()
										+ "' and prof = '"
										+ cursCurent.getProf() + "'");
					}
					catch (SQLException e)
					{
						e.printStackTrace();
					}
				cursCurent = (Curs) session.getAttribute("curs");
			}
		}
		return cursCurent;
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
	private Connection _connection;%>
<%
	Curs cursCurent = _getCursCurent(request);
%>
<!DOCTYPE html PUBLIC "-//W3C//DTD HTML 4.01 Transitional//EN" "http://www.w3.org/TR/html4/loose.dtd">
<html>
<head>
<meta http-equiv="Content-Type" content="text/html; charset=ISO-8859-1">
<title>Programare Web Laborator 9</title>
<style>
body {
	font-family: Arial;
}

label {
	display: inline-block;
	width: 100px;
}

input.text {
	margin-bottom: 10px;
	width: 300px;
}

input.buttonLeft {
	float: left;
}

input.buttonRight {
	float: right;
}

form {
	margin: 0 auto;
	width: 410px;
}
</style>
</head>
<body lang="ro">
	<form action="/L9/Default.jsp" method="POST">
		<h1>Cursuri</h1>
		<label>Nume curs: </label> <input class="text" type="text" name="nume"
			value="<%=cursCurent.getNume()%>" /><br /> <label>Profesor:
		</label> <input class="text" type="text" name="prof"
			value="<%=cursCurent.getProf()%>" /><br /> <input
			class="buttonLeft" type="submit" name="navigare" value="Anterior" />
		<input class="buttonRight" type="submit" name="navigare"
			value="Următor" />
	</form>
</body>
</html>