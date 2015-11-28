using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
//using ClassLibrary1.OutputStream;
using Windows.Storage.Streams;
using System.IO;
using Windows.Storage;
using System.Xml.Serialization;

namespace MyDomotik
{
    class Enregistrement
    {
    
       /* private string config = ""; // configuration à enregistrer
        private string fileName = "config.xml";
     //   private OutputStream o;
        private FileRandomAccessStream a = new FileRandomAccessStream();

        private Stream s = new Stream();

        StorageFile  f = new StorageFile();

        ApplicationData a = new ApplicationData();


        private StreamWriter sw = new System.IO.StreamWriter();
        // System.IO.File.WriteAllLines(@"C:\Users\Public\TestFolder\WriteLines.txt", lines);
        */
        public Enregistrement(Configuration c)
        {
            //config = c.toStringXML();
        //    o = new OutputStream(fileName);
        //    creerFichierEnr();

        }

     /*   public Enregistrement()
        {

        }


         public void creerFichierEnr()
        {
            string fileeName = "test.txt";
            string textToAdd = "Example text in file";

            using (o)
            {
                o.Write(textToAdd);
            }
        

            
            System.IO.Stream stream = new System.IO.MemoryStream();

            using (Stream fs = File.OpenWrite(path))

            using (StreamWriter file = new StreamWriter(stream))
            {
                file.Write(config);
            }
            

            
            string fileeName = "test.txt";
            string textToAdd = "Example text in file";

            using (StreamWriter writer = new StreamWriter(fileeName)
            {
                writer.Write(textToAdd);
            }
           
        

        } */
    }
}
