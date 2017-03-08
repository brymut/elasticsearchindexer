using AvaloqDocu.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AvaloqDocu.Models
{
    /// <summary>
    /// 
    /// </summary>
    public class Document
    {

        /// <summary>
        ///  Initialises a new instance of the Document class.
        /// </summary>
        /// <param name="Title"
        /// <param name="Subtitle"
        /// <param name="DocuID"
        /// <param name="Release"
        /// <param name="FunctionalArea"
        /// <param name="DocuType"
        /// <param name="SubType"
        /// <param name="LastModified"
        /// <param name="Filepath"
        public Document(string Title = null, string Subtitle = null, int DocuID = 0, string Release = null, string FunctionalArea = null, string DocuType = null, string SubType = null, DateTime? LastModified = null, string Filepath = null)
        {
            this.Title = Title;
            this.Subtitle = Subtitle;
            this.DocuID = DocuID;
            this.Release = Release;
            this.FunctionalArea = FunctionalArea;
            this.DocuType = DocuType;
            this.SubType = SubType;
            this.LastModified = LastModified ?? DateTime.MinValue;
            this.FilePath = FilePath;

        }



        public int DocumentID { get; set; }
        public string Title { get; set; }
        public string Subtitle { get; set; }
        public int DocuID { get; set; }
        public string Release { get; set; }
        public string FunctionalArea { get; set; }
        public string DocuType { get; set; }
        public string SubType { get; set; }
        public DateTime LastModified { get; set; }
        public int FilePathId { get; set; }
        public int FileSize { get; set; }
        public virtual string FilePath { get; set; }

        /// <summary>
        ///  Hash code implementation, that might help with finding duplicates later on.
        /// </summary>
        public override int GetHashCode()
        {
            // http://stackoverflow.com/a/263416/677735

            unchecked
            {
                int hash = 41;

                if (this.Title != null)
                    hash = hash * 59 + this.Title.GetHashCode();
                if (this.Subtitle != null)
                    hash = hash * 59 + this.Subtitle.GetHashCode();
                // figure something out about the tags.
                //if (this.DocuID != null)
                hash = hash * 59 + this.DocuID.GetHashCode();
                if (this.Release != null)
                    hash = hash * 59 + this.Release.GetHashCode();
                if (this.FunctionalArea != null)
                    hash = hash * 59 + this.FunctionalArea.GetHashCode();
                if (this.DocuType != null)
                    hash = hash * 59 + this.DocuType.GetHashCode();
                if (this.SubType != null)
                    hash = hash * 59 + this.SubType.GetHashCode();
                if (this.LastModified != null)
                    hash = hash * 59 + this.LastModified.GetHashCode();
                if (this.FilePath != null)
                    hash = hash * 59 + this.LastModified.GetHashCode();

                return hash;
            }
        }

    }
}