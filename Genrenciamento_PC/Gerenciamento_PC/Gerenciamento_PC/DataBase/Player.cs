using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

using System.Runtime.Serialization.Formatters.Binary;

namespace Gerenciamento_PC.DataBase
{
    [Serializable]
    class Player
    {
        public string Name;

        public float Record;

        public Player(string name , float record)
        {
            Name = name;

            Record = record;
        }
    }
}