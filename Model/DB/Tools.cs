namespace LifetimeToolManage.Model.DB
{
    public class Tools
    {
        public int Id { get; set; }
        public required string Code { get; set; }

        public ICollection<Lifetime>? Lifetime { get; set; }
    }
}
