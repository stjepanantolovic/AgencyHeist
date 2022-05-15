namespace MoneyHeist2.Entities
{
    public class Member
    {
        public Member()
        {
            this.Skills = new HashSet<Skill>();
        }
        public Guid ID { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public virtual ICollection<Skill> Skills { get; set; }
        public virtual MemberStatus Status { get; set; }
        public virtual Skill MainSkill { get; set; }
        public virtual Sex Sex { get; set; }
        public Guid? MainSkillID { get; set; }
        public Guid? SexID { get; set; }
        public Guid? StatusID { get; set; }

    }
}
