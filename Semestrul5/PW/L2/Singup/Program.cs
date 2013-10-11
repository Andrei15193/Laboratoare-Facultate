using RegistProgramareWeb.Laborator1.Infrastructure;
using System;
using System.Collections.Generic;

namespace ProgramareWeb.Laborator1.Singup
{
    internal static class Program
    {
        static private void Main(string[] args)
        {
            string username, password, password2;
            IDictionary<string, string> parameters = _GetParameters(Console.ReadLine());

            if (parameters.TryGetValue("username", out username)
                && parameters.TryGetValue("password", out password)
                && parameters.TryGetValue("password2", out password2))
                if (password == password2)
                    if (new AllUsers().TryAdd(username, password))
                        _WriteUserPage(username);
                    else
                        _WriteUsernameAlreadyPresentPage();
                else
                    _WritePasswordsDoNotMatchPage();
        }

        static private void _WriteUserPage(string username)
        {
            Console.WriteLine("Content-type: text/html");
            Console.WriteLine();
            Console.WriteLine("<!DOCTYPE html><html><head><title>User: {0}</title><link href=\"default.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><div class=\"successful\"><h1>Success!</h1><p>Your account has been created!</p></div></body>", username);
        }

        private static void _WriteUsernameAlreadyPresentPage()
        {
            Console.WriteLine("Content-type: text/html");
            Console.WriteLine();
            Console.WriteLine("<!DOCTYPE html><html><head><title>Sing up failed</title><link href=\"default.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><div class=\"successful\"><h1>Username unavailable!</h1><p>The entered username is already taken!</p></div></body>");
        }

        private static void _WritePasswordsDoNotMatchPage()
        {
            Console.WriteLine("Content-type: text/html");
            Console.WriteLine();
            Console.WriteLine("<!DOCTYPE html><html><head><title>Sing up failed</title><link href=\"default.css\" rel=\"stylesheet\" type=\"text/css\" /></head><body><div class=\"successful\"><h1>Passwords do not match!</h1><p>The provided passwords do not match!</p></div></body>");
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
