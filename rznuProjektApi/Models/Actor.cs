using Newtonsoft.Json;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace rznuProjektApi.Models
{
    public class Actor
    {
        [Key]
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        public string FirstName { get; set; }

        [DataMember]
        public string LastName { get; set; }

        [DataMember]
        public int MovieId { get; set; }

        [JsonIgnore]
        public virtual Movie Movie { get; set; }
    }
}