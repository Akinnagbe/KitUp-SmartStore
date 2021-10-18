using SmartStore.Services;
using SmartStore.Services.Tasks;


namespace SmartStore.ShippingByDistance
{
    public class MyFirstTask : ITask
    {
        private readonly ICommonServices _services;


        public MyFirstTask(
            ICommonServices services)
        {
            _services = services;

        }

        public void Execute(TaskExecutionContext context)
        {
            // Do something
        }
    }
}