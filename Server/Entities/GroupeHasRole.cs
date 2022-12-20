namespace FSDOCS.Server.Entities
{
    public class GroupeHasRole
    {
        public string CodeGroupe { get; set; }
        public string CodeRole { get; set; }
        public Role? Role { get; set; }
        public Groupe? Groupe { get; set; }
    }
}
