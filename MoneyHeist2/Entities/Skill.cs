namespace MoneyHeist2.Entities
{
    public class Skill
    {
        public Skill()
        {
            this.Members = new HashSet<Member>();
        }

        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Level { get; set; }
        public ICollection<Member> Members { get; set; }
    }
}
