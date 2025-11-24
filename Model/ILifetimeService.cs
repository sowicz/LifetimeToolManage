using LifetimeToolManage.Model.DB;

namespace LifetimeToolManage.Model
{
    public interface ILifetimeService
    {
        public bool registerQuantity(int toolId, DateTime registerTime, int quantity);
        public bool editLastQuantity(int activeToolId, int quantity);
        public bool deleteQuantity(int id);

        public Lifetime? getLifetimeByToolId(int id);

    }
}
