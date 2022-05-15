namespace MoneyHeist2.Helpers
{
    public class MemberParams
    {
        private const int MaxPageSize = 50;
        public int PageNumber { get; set; } = 1;
        private int pageSize = 10;
        public int PageSize
        {
            get { return pageSize; }
            set { pageSize = (value > MaxPageSize) ? MaxPageSize : value; }
        }

        public Guid? UserID { get; set; }
        public ICollection<Guid>? SkillIDs { get; set; }
        public List<Guid>? SexIDs { get; set; }
        public List<Guid> MemberStatusIDs { get; set; } = new List<Guid>() { };
        public string? OrderBy { get; set; }
    }
}
