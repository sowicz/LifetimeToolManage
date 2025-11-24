using LifetimeToolManage.Model.DB;

namespace LifetimeToolManage.Model
{
    public class ToolsService : IToolsService
    {
        private readonly AppDbContext _context;
        public ToolsService(AppDbContext context)
        {
            _context = context;
        }

        public Tools? checkIfExistTool(string code)
        {
            var tool = _context.Tools.Where(t => t.Code == code).FirstOrDefault();
            return tool;
        }
        public bool addTool(Tools tool)
        {
            var existingTool = checkIfExistTool(tool.Code);
            if (existingTool != null)
            {
                return false;
            }
            _context.Tools.Add(tool);
            _context.SaveChanges();
            return true;
        }
        public List<Tools> getTools()
        {
            return _context.Tools.ToList();
        }
        public bool removeTool(string code)
        {
            var tool = _context.Tools.FirstOrDefault(t => t.Code == code);
            if (tool == null) return false;

            _context.Tools.Remove(tool);
            _context.SaveChanges();

            return true;
        }
    }
}
