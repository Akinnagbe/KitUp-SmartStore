using SmartStore.Plugin1.Domain;

namespace SmartStore.Plugin1.Services
{
    public partial interface IPlugin1Service
    {
        Plugin1Record GetPlugin1Record(int entityId, string entityName);
        Plugin1Record GetPlugin1RecordById(int id);
        void InsertPlugin1Record(Plugin1Record record);
        void UpdatePlugin1Record(Plugin1Record record);
        void DeletePlugin1Record(Plugin1Record record);
    }
}
