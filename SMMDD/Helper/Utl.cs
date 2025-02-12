using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Serialization;

namespace SMMDD.Helper
{
    public static class Utl
    {
        public static void Serialize<T>(T obj)
        {
            XmlSerializer xs = new XmlSerializer(typeof(T));

            using (TextWriter txtWriter = new StreamWriter("SecurityList.xml"))
            {
                xs.Serialize(txtWriter, obj);
                txtWriter.Close();
            }
        }

        public static string FormatTwoDigit(this int value)
        {
            if (value < 10)
                return "0" + value;

            return value.ToString();
        }
    }
}
