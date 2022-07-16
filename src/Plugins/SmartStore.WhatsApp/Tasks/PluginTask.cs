using SmartStore.Services;
using SmartStore.Services.Tasks;
using SmartStore.WhatsApp.Services;

namespace SmartStore.WhatsApp
{
    public class MyFirstTask : ITask
    {
        private readonly ICommonServices _services;
        private readonly IWhatsAppService _whatsAppService;

        public MyFirstTask(
            ICommonServices services,
            IWhatsAppService whatsAppService)
        {
            _services = services;
            _whatsAppService = whatsAppService;
        }

        public void Execute(TaskExecutionContext context)
        {
            // Do something
        }
    }
}