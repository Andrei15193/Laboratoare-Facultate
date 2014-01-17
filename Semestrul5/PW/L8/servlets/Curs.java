package servlets;
public class Curs
{
	public Curs(String nume, String prof)
	{
		_nume = nume;
		_prof = prof;
	}
	public String getNume()
	{
		return _nume;
	}
	public String getProf()
	{
		return _prof;
	}
	@Override
	public String toString()
	{
		return _nume + " " + _prof;
	}
	private String _nume;
	private String _prof;
}