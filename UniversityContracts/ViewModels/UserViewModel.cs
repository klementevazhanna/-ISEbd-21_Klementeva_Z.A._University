using System.ComponentModel;
using System.Runtime.Serialization;

namespace UniversityContracts.ViewModels
{
    [DataContract]
    public class UserViewModel
    {
        [DataMember]
        public int Id { get; set; }

        [DataMember]
        [DisplayName("Ф.И.О.")]
        public string FIO { get; set; }

        [DataMember]
        [DisplayName("Почта")]
        public string Email { get; set; }

        [DataMember]
        [DisplayName("Пароль")]
        public string Password { get; set; }
    }
}
