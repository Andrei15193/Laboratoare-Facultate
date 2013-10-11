using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Threading;

namespace RegistProgramareWeb.Laborator1.Infrastructure
{
    public class AllUsers
    {
        public AllUsers(string fileName = "users")
        {
            if (fileName != null)
            {
                _fileName = fileName.Trim();
                if (_fileName.Length > 0)
                {
                    _fileName = fileName;
                    _fileMutex = new Mutex(false, fileName + "Mutex");
                }
            }
        }

        public bool TryGet(string name, string password, out KeyValuePair<User, DateTime?> user)
        {
            if (name != null && password != null)
                try
                {
                    _fileMutex.WaitOne();
                    using (Stream stream = File.Open(_fileName, FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        Dictionary<string, KeyValuePair<User, DateTime?>> users = (serializer.Deserialize(stream) as Dictionary<string, KeyValuePair<User, DateTime?>>);
                        if (users != null && users.TryGetValue(name, out user) && password == user.Key.Password)
                        {
                            users[name] = new KeyValuePair<User, DateTime?>(user.Key, DateTime.Now);
                            stream.Position = 0;
                            serializer.Serialize(stream, users);
                            return true;
                        }
                        else
                        {
                            user = new KeyValuePair<User, DateTime?>();
                            return false;
                        }
                    }
                }
                catch
                {
                    user = new KeyValuePair<User, DateTime?>();
                    return false;
                }
                finally
                {
                    _fileMutex.ReleaseMutex();
                }
            else
            {
                user = new KeyValuePair<User, DateTime?>();
                return false;
            }
        }

        public bool TryAdd(string name, string password)
        {
            if (name != null && password != null)
                try
                {
                    _fileMutex.WaitOne();
                    using (Stream stream = File.Open(_fileName, FileMode.OpenOrCreate))
                    {
                        BinaryFormatter serializer = new BinaryFormatter();
                        Dictionary<string, KeyValuePair<User, DateTime?>> users = null;
                        try
                        {
                            users = (serializer.Deserialize(stream) as Dictionary<string, KeyValuePair<User, DateTime?>>);
                        }
                        catch
                        {
                            users = (users ?? new Dictionary<string, KeyValuePair<User, DateTime?>>());
                        }
                        if (!users.ContainsKey(name))
                        {
                            users.Add(name, new KeyValuePair<User, DateTime?>(new User(name, password), null));
                            stream.Position = 0;
                            serializer.Serialize(stream, users);
                            return true;
                        }
                        else
                            return false;
                    }
                }
                catch
                {
                    return false;
                }
                finally
                {
                    _fileMutex.ReleaseMutex();
                }
            else
                return false;
        }

        private readonly Mutex _fileMutex;
        private readonly string _fileName;
    }
}
