namespace UniversityContracts.BindingModels
{
    public class MailConfigBindingModel
    {
        public string SmtpClientHost { get; set; }

        public int SmtpClientPort { get; set; }

        public string MailLogin { get; set; }

        public string MailPassword { get; set; }
    }
}
