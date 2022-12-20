namespace FSDOCS.Shared.DataTransferObjects
{
    public class UserGet
    {
        public string UserID { get; set; }
        public string UserPwd { get; set; }
        public string? CodeGroupe { get; set; }

        public bool State { get; set; }
        public GroupeCreate? Groupe { get; set; }
    }
}
