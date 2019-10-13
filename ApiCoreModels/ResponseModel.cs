using System.Collections.Generic;
using System.Runtime.Serialization;

namespace ApiCoreModels
{
    [DataContract]
    public class ResponseModel
    {
        [DataMember (Name = "Search")]
        public IList<MovieModel> Search { get; set; } = new List<MovieModel>();
        
        [DataMember (Name = "totalResults")]
        public string TotalResults { get; set; }
        
        [DataMember (Name = "Response")]
        public string Response { get; set; }
    }
}