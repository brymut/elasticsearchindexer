using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using System.IO;
using AvaloqDocu.Models;
using System.Globalization;

namespace elasticsearch_indexer
{
    class Program
    {
        public static Uri node;
        public static ConnectionSettings settings;
        public static ElasticClient client;
        static void Main(string[] args)
        {
            node = new Uri("http://localhost:9200");
            settings = new ConnectionSettings(node).
                DefaultIndex("avaloq_documents");
            client = new ElasticClient(settings);

            InsertData();
            
        }

        public static void InsertData()
        {
            List<Document> documents = new List<Document>();

            foreach (string line in File.ReadLines(@"c:/avaloq_docs.txt", Encoding.UTF8))
            {
                string[] docInfo = line.Split(',');
                int day, month, year;
                try
                {
                    day = int.Parse(docInfo[3].Substring(0, 2));
                    month = int.Parse(docInfo[3].Substring(3, 2));
                    year = int.Parse(docInfo[3].Substring(6, 2)) + 2000;

                }
                catch
                {
                    continue;
                }

                
                var document = new Document(docInfo[0], docInfo[1], int.Parse(docInfo[6]), docInfo[7], docInfo[5], docInfo[2], docInfo[4],new DateTime(year,month,day));
                documents.Add(document);
            }


           var result = client.IndexMany(documents);

        }
    }
}
