using System;
using System.Net;
using System.Collections.Generic;

using System.IO;
using System.IO.IsolatedStorage;
using System.Xml.Serialization;
using System.Xml;

using Gerenciamento_WP7.Management;

namespace Gerenciamento_WP7.DataBase
{
    public class Player
    {
        public string Name;

        public float Record;

        public Player()
        {}

        public Player(string name , float record)
        {
            Name = name;
            Record = record;
        }
        
        public static void Save(List<Player> Players)
        {
            using (IsolatedStorageFile MyIsolatedStorageFile =
                IsolatedStorageFile.GetUserStoreForApplication())
            {
                using(IsolatedStorageFileStream Stream =
                    MyIsolatedStorageFile.OpenFile(StateManager.PathForRecords,
                    FileMode.Open))
                {
                    XmlSerializer Serializer = new XmlSerializer(typeof(List<Player>));

                    Serializer.Serialize(Stream,Players);
                }
            }
        }

        public static List<Player> Load()
        {
            using (IsolatedStorageFile MyIsolatedStorageFile =
                IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (MyIsolatedStorageFile.FileExists(StateManager.PathForRecords))
                {
                    using (IsolatedStorageFileStream Stream =
                        MyIsolatedStorageFile.OpenFile(StateManager.PathForRecords,
                        FileMode.Open))
                    {
                        XmlSerializer Serializer = new XmlSerializer(typeof(List<Player>));

                        return (List<Player>)Serializer.Deserialize(Stream);
                    }
                }
            }
            return new List<Player>();
        }

        public static void Creating()
        {
            using (IsolatedStorageFile MyIsolatedStorageFile =
                IsolatedStorageFile.GetUserStoreForApplication())
            {
                if (!MyIsolatedStorageFile.FileExists(StateManager.PathForRecords))
                {
                    using (IsolatedStorageFileStream Stream =
                        MyIsolatedStorageFile.OpenFile(StateManager.PathForRecords,
                        FileMode.CreateNew))
                    {
                        XmlSerializer Serializer = new XmlSerializer(typeof(List<Player>));

                        Serializer.Serialize(Stream, new List<Player>());
                    }
                }
            }
        }
    }
}
