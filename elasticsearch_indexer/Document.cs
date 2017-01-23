using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace elasticsearch_indexer
{
    public class Document
    {
        public string author { get; set; }
        public DateTime creation_date { get; set; }
        public string file_name { get; set; }
        public string title { get; set; }
        //public string description { get; set; }


        public Document( string Author, DateTime creationDate, string fileName, string Title)
        {
            author = Author;
            creation_date = creationDate;
            file_name = fileName;
            title = Title;
           // description = Description;


        }
    }

  
}
