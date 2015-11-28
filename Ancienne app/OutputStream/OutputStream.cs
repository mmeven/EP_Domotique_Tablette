using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace OutputStream
{
    public class OutputStream
    {
        public string fileName;
        public StreamWriter sw;

        public OutputStream(string name)
        {
            fileName = name;
            sw = new StreamWriter(fileName);
        }

        public void Write(string text)
        {
            sw.Write(text);
        }

        public void Close()
        {
            sw.Close();
        }
    }
}
