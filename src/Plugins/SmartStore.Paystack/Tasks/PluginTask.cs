
using SmartStore.Services;
using SmartStore.Services.Tasks;

namespace SmartStore.Paystack
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