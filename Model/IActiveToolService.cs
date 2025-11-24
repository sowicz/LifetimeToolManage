using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifetimeToolManage.Model
{
    public interface IActiveToolService
    {
        string getActiveTool();
        bool updateActiveTool(string code);
    }
}
