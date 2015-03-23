using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;
using System.Runtime.Serialization;

using Gerenciamento_PC.Management;
using Gerenciamento_PC.DataBase;

namespace Gerenciamento_PC.DataBase
{
    [Serializable]
    class PlayerManager
    {
        public List<Player> Players;
        
        public static void SavePlayers(PlayerManager manager)
        {
            using (FileStream Stream = new FileStream(Preferences.PathForRecords, FileMode.OpenOrCreate))
            {
                BinaryWriter Writer = new BinaryWriter(Stream);

                BinaryFormatter Formatter = new BinaryFormatter();

                Formatter.Serialize(Stream, manager);
            }
        }

        public static PlayerManager GetPlayers()
        {
            using (FileStream Stream = new FileStream(Preferences.PathForRecords, FileMode.OpenOrCreate))
            {
                BinaryFormatter Formatter = new BinaryFormatter();

                if (Stream.Length <= 0)
                    return new PlayerManager();

                return (PlayerManager)Formatter.Deserialize(Stream);
            }
        }
    }
}