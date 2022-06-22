using SmartStore.Plugin1.Services;
using SmartStore.Services;
using SmartStore.Services.Tasks;

namespace SmartStore.Plugin1
{
    public class MyFirstTask : ITask
    {
        private readonly ICommonServices _services;
        private readonly IPlugin1Service _plugin1Service;

        public MyFirstTask(
            ICommonServices services,
            IPlugin1Service plugin1Service)
        {
            _services = services;
            _plugin1Service = plugin1Service;
        }

        public void Execute(TaskExecutionContext context)
        {
            // Do something
        }
    }
}