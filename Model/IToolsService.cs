using LifetimeToolManage.Model.DB;



namespace LifetimeToolManage.Model
{
    internal interface IToolsService
    {
        bool addTool(Tools tool);
        List<Tools> getTools();
        bool removeTool(string code);
        Tools? checkIfExistTool(string code);
    }
}
