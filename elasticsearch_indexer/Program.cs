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

            //InsertData();
            int DocuID = 0;
            string Release = null;
            string FunctionalArea = null;
            string DocuType = "Avaloq Core - System Documentation";
            string SubType = null;
            //DateTime? LastModifiedFrom = new DateTime(2016,01,01);
            //DateTime? LastModifiedTo = new DateTime(2017,01,01);
            DateTime? LastModifiedFrom = null;
            DateTime? LastModifiedTo = null;


            var filters = new List<Func<QueryContainerDescriptor<Document>, QueryContainer>>();
            
            if (DocuID != 0)
            {
                filters.Add(r2 => r2.Match(q2 => q2
                                           .Query(DocuID.ToString())
                                                .Field(f3 => f3.DocuID)));
            }

            if ( FunctionalArea != null)
            {
                filters.Add(r2 => r2.Match(q2 => q2
                                            .Query(FunctionalArea)
                                                 .Field(f3 => f3.FunctionalArea)));
            }

            if (Release != null)
            {
                filters.Add(r2 => r2.Match(q2 => q2
                                           .Query(Release+".0.0.0")
                                                .Field(f3 => f3.Release)));
            }

            if (DocuType != null)
            {
                filters.Add(r2 => r2.Match(q2 => q2
                                           .Query(DocuType)
                                                .Field(f3 => f3.DocuType)));
            }


            var result = client.Search<Document>(x => x
                            .Query(q => q
                                .MultiMatch(mp => mp
                                    .Query("Swift")
                                        .Fields(f => f
                                            .Fields(f1 => f1.Title, f2 => f2.Subtitle))))
                                            .PostFilter(r => r
                                                .Bool(r1 => r1.Must(filters) 
                                                    )));



            var postFilter = result.Documents.AsQueryable();



            


            if (SubType != null)
            {
                postFilter = postFilter.Where(x => x.SubType == SubType);
            }



            if (LastModifiedFrom != null)
            {
                postFilter = postFilter.Where(x => x.LastModified >= LastModifiedFrom);
            }



            if (LastModifiedTo != null)
            {
                postFilter = postFilter.Where(x => x.LastModified <= LastModifiedTo);
            }




            var final = postFilter.OrderByDescending(r1 => r1.LastModified);

    

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

        public static void SimpleQuery(string term)
{

    
    //Console.WriteLine(result.Documents);

}
    }
}
