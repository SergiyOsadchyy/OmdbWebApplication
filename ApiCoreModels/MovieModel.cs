using System.Runtime.Serialization;

namespace ApiCoreModels
{
    [DataContract]
    public class MovieModel
    {
        [DataMember (Name = "Title")]
        public string Title { get; set; }
        
        [DataMember (Name = "Year")]
        public string Year { get; set; }
        
        [DataMember (Name = "imdbID")]
        public string ImdbId { get; set; }
        
        [DataMember (Name = "Type")]
        public string Type { get; set; }
        
        [DataMember (Name = "Poster")]
        public string Poster { get; set; }
    }
}