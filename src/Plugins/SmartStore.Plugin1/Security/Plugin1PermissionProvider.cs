using SmartStore.Core.Domain.Customers;
using SmartStore.Core.Domain.Security;
using SmartStore.Core.Security;
using System.Collections.Generic;
using System.Linq;

namespace SmartStore.Plugin1.Security
{
    public static class Plugin1Permissions
    {
        public const string Self = "Plugin1";
        public const string Read = "Plugin1.read";
        public const string Update = "Plugin1.update";
        public const string Display = "Plugin1.display";
        public const string Edit = "Plugin1.edit";
    }


    public class Plugin1PermissionProvider : IPermissionProvider
    {
        public IEnumerable<PermissionRecord> GetPermissions()
        {
            var permissionSystemNames = PermissionHelper.GetPermissions(typeof(Plugin1Permissions));
            var permissions = permissionSystemNames.Select(x => new PermissionRecord { SystemName = x });

            return permissions;
        }

        public IEnumerable<DefaultPermissionRecord> GetDefaultPermissions()
        {
            return new[]
            {
                new DefaultPermissionRecord
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Administrators,
                    PermissionRecords = new[]
                    {
                        new PermissionRecord { SystemName = Plugin1Permissions.Self }
                    }
                },
                new DefaultPermissionRecord
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.ForumModerators,
                    PermissionRecords = new[]
                    {
                        new PermissionRecord { SystemName = Plugin1Permissions.Display }
                    }
                },
                new DefaultPermissionRecord
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Guests,
                    PermissionRecords = new[]
                    {
                        new PermissionRecord { SystemName = Plugin1Permissions.Display }
                    }
                },
                new DefaultPermissionRecord
                {
                    CustomerRoleSystemName = SystemCustomerRoleNames.Registered,
                    PermissionRecords = new[]
                    {
                        new PermissionRecord { SystemName = Plugin1Permissions.Display }
                    }
                }
            };
        }
    }
}