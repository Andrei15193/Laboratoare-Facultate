using RegistProgramareWeb.Laborator1.Infrastructure;
using System;
using System.Collections.Generic;

namespace ProgramareWeb.Laborator1.Login
{
    internal static class Program
    {
        static private void Main(string[] args)
        {
            string username, password;
            KeyValuePair<User, DateTime?> user;
            IDictionary<string, string> parameters = _GetParameters(Console.ReadLine());

            if (parameters.TryGetValue("username", out username)
                && parameters.TryGetValue("password", out password)
                && new AllUsers().TryGet(username, password, out user))
                _WriteUserPage(user);
            else
                _WriteInvalidUserPage();
        }

        static private void _WriteUserPage(KeyValuePair<User, DateTime?> user)
        {
            Console.WriteLine("Content-type: text/html");
            Console.WriteLine();
            if (user.Value.HasValue)
                Console.WriteLine("<!DOCTYPE html><html><head><title>User: {0}</title><link href=\"default.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><div class=\"successful\"><h1>Log in successful!</h1><p>Your last login was on {1}.</p></div></body>", user.Key.Name, user.Value.Value);
            else
                Console.WriteLine("<!DOCTYPE html><html><head><title>User: {0}</title><link href=\"default.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><div class=\"successful\"><h1>Log in successful!</h1><p>You are a new user!</p></div></body>", user.Key.Name);
        }

        static private void _WriteInvalidUserPage()
        {
            Console.WriteLine("Content-type: text/html");
            Console.WriteLine();
            Console.WriteLine("<!DOCTYPE html><html><head><title>Log in failed</title><link href=\"default.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><div class=\"error\"><h1>Log in failed!</h1><p>Your log in credentials are invalid!</p></div></body>");
        }

        static private IDictionary<string, string> _GetParameters(string queryString)
        {
            IDictionary<string, string> parameters = new Dictionary<string, string>(StringComparer.CurrentCultureIgnoreCase);

            foreach (string parameterNameValue in queryString.Split('&'))
            {
                string[] parameter = parameterNameValue.Split(new[] { '=' }, 2);

                if (parameter.Length == 2)
                    parameters.Add(parameter[0], parameter[1]);
            }

            return parameters;
        }
    }
}
