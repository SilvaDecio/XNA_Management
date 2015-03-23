using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using System.IO;
using System.Runtime.Serialization.Formatters.Binary;

using Gerenciamento_PC.Management;

namespace Gerenciamento_PC.DataBase
{
    [Serializable]
    class Preferences
    {
        public static string PathForRecords = "Records.DecioSilva",
            PathForPreferences = "Preferences.DecioSilva";

        public static GameLanguage CurrentLanguage;

        public float SongVolume, EffectVolume;

        public Preferences()
        {
            SongVolume = 5f;

            EffectVolume = 5f;
        }

        public static void SavePreferences(Preferences settings)
        {
            using (FileStream Stream = new FileStream(PathForPreferences, FileMode.OpenOrCreate))
            {
                BinaryWriter Writer = new BinaryWriter(Stream);

                BinaryFormatter Formatter = new BinaryFormatter();

                Formatter.Serialize(Stream, settings);
            }
        }

        public static Preferences GetPreferences()
        {
            using (FileStream Stream = new FileStream(PathForPreferences, FileMode.OpenOrCreate))
            {
                BinaryFormatter Formatter = new BinaryFormatter();

                if (Stream.Length <= 0)
                    return new Preferences();

                return (Preferences)Formatter.Deserialize(Stream);
            }
        }
    }
}