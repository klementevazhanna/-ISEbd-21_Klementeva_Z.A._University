using System.Runtime.Serialization;

namespace UniversityContracts.BindingModels
{
    [DataContract]
    public class UserBindingModel
    {
        [DataMember]
        public int? Id { get; set; }

        [DataMember]
        public string FIO { get; set; }

        [DataMember]
        public string Email { get; set; }

        [DataMember]
        public string Password { get; set; }
    }
}
