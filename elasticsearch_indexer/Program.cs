using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Nest;
using System.IO;

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
                DefaultIndex("avaloq_docs");
            client = new ElasticClient(settings);

            //InsertData();
            SimpleQuery("apple");

            

        }

        public static void InsertData()
        {
            List<Document> documents = new List<Document>();

            foreach (string line in File.ReadLines(@"C:\Users\mutai_000\indexing_helper.txt"))
            {
                String[] line_text = line.Split(',');
                var doc = new Document(line_text[2], Convert.ToDateTime(line_text[3]), line_text[0], line_text[1]);
                documents.Add(doc);
                //Console.WriteLine(doc);
            }
            foreach (var item in documents)
            {
                client.Index(item);
                Console.WriteLine(item);
            }


        }

        public static void SimpleQuery(string term) {

            var result = client.Search<Document>(s => s
                                .Query(p => p.Term(q => q.title, term)));

            //Console.WriteLine(result.Documents);
 
        }
    }
}
