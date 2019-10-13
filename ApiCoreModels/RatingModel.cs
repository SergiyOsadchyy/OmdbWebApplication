using System.Runtime.Serialization;

namespace ApiCoreModels
{
    [DataContract]
    public class RatingModel
    {
        [DataMember (Name = "Source")]
        public string Source { get; set; }
        
        [DataMember (Name = "Value")]
        public string Value { get; set; }
    }
}