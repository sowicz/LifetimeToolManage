using LifetimeToolManage.Model.DB;


namespace LifetimeToolManage.Model
{
    public class LifetimeService : ILifetimeService
    {
        private readonly AppDbContext _context;
        public LifetimeService(AppDbContext context)
        {
            _context = context;
        }

        private bool checkQuantity(int quantity)
        {
            if (quantity < 0)
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private Tools? checkToolExist(int toolId) 
        {
            var tool = _context.Tools.Find(toolId);
            return tool; 
        }

        public bool registerQuantity(int toolId, DateTime registerTime, int quantity) 
        {

            if (!checkQuantity(quantity)) return false;

            var tool = checkToolExist(toolId);
            if (tool == null) return false;

            var lifetime = new Lifetime
            {
                ToolId = toolId,
                registerTime = registerTime,
                quantity = (uint)quantity
            };
            _context.Lifetimes.Add(lifetime);
            _context.SaveChanges();
            return true; 
        }
        public bool editLastQuantity(int activeToolId, int quantity) 
        {
            if (!checkQuantity(quantity)) return false;
            // BLAD - nie moze szukac po ID lifetime, tylko po toolId i brac najnowszy wpis
            var lastLifetime = _context.Lifetimes
                .Where(l => l.ToolId == activeToolId)
                .OrderByDescending(l => l.Id)
                .FirstOrDefault();

            if (lastLifetime == null) return false;
            
            lastLifetime.quantity = (uint)quantity;
            _context.Lifetimes.Update(lastLifetime);
            _context.SaveChanges();

            return true; 
        }
        public bool deleteQuantity(int id) { return true; }

        public Lifetime? getLifetimeByToolId(int toolId) 
        {
            var tool = checkToolExist(toolId);
            if (tool == null) return null;
            var latestLifetime = _context.Lifetimes
                .Where(l => l.ToolId == toolId)
                .OrderByDescending(l => l.registerTime)
                .FirstOrDefault();
            return latestLifetime;

        }
    }
}
