using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LifetimeToolManage.Model.DB;

namespace LifetimeToolManage.Model.DB
{
    public class ActiveToolService : IActiveToolService
    {
        private readonly AppDbContext _context;
        public ActiveToolService(AppDbContext context)
        {
            _context = context;
        }

        public string getActiveTool()
        {
            string activeToolCode = _context.ActiveTool.FirstOrDefault()?.Code ?? string.Empty;
            return activeToolCode;
        }

        public bool updateActiveTool(string code)
        { 

            var activeTool = _context.ActiveTool.FirstOrDefault();
            var tool = _context.Tools.FirstOrDefault(t => t.Code == code);
            if (activeTool != null && tool != null)
            {
                activeTool.Code = code;
                activeTool.ToolId = tool.Id;
                _context.SaveChanges();
                return true;
            }
            else if (activeTool == null && tool != null)
            {
                ActiveTool newActiveTool = new ActiveTool
                {
                    Code = code,
                    ToolId = tool.Id
                };
                _context.ActiveTool.Add(newActiveTool);
                _context.SaveChanges();
                return true;
            }
            else
            {
                return false;
            }
        }

        public bool deactivateTool()
        {
            var activeTool = _context.ActiveTool.FirstOrDefault();
            if(activeTool == null)
            {
                return false;
            }

            _context.ActiveTool.Remove(activeTool);
            return true;
        }
    }

}
