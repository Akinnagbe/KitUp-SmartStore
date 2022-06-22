using SmartStore.Collections;
using SmartStore.Plugin1.Security;
using SmartStore.Web.Framework.UI;

namespace SmartStore.Plugin1
{
    public class AdminMenu : AdminMenuProvider
    {
        protected override void BuildMenuCore(TreeNode<MenuItem> pluginsNode)
        {
            var menuItem = new MenuItem().ToBuilder()
                .Text("My Plugin")
                .ResKey("Plugins.FriendlyName.SmartStore.Plugin1")
                .Icon("far fa-images")
                .PermissionNames(Plugin1Permissions.Read)
                .Action("ConfigurePlugin", "Plugin", new { systemName = "SmartStore.Plugin1", area = "Admin" })
                .ToItem();

            pluginsNode.Prepend(menuItem);
        }

        public override int Ordinal
        {
            get
            {
                return -200;
            }
        }
    }
}
