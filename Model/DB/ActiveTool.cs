using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LifetimeToolManage.Model.DB
{
    public class ActiveTool
    { 
        public int Id { get; set; }
        public required string Code { get; set; }
        public int ToolId { get; set; }
        public Tools? Tool { get; set; }
    }
}
