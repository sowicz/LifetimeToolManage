namespace LifetimeToolManage.Model.DB
{
    public class Lifetime
    {
        public int Id { get; set; }
        public uint quantity { get; set; }
        public DateTime? registerTime { get; set; }
        public int ToolId { get; set; }
        public Tools? Tool { get; set; }
    }
}
