using Api.Exceptions;
using Business.Abstraction.Helpers;

namespace Api.ACL
{
    public class PermissionHelper : IPermissionHelper
    {
        public IEnumerable<string> Permissions => Enum.GetNames(typeof(Permission));
        public IDictionary<string, IEnumerable<string>> PermissionDependencyMap => PermissionConfiguration.PermissionDependencyMap.ToDictionary(x => x.Key.ToString(), x=> x.Value.Select(y => y.ToString()));
        public IDictionary<string, IEnumerable<string>> IncompatiblePermissionMap => PermissionConfiguration.IncompatiblePermissionMap.ToDictionary(x => x.Key.ToString(), x=> x.Value.Select(y => y.ToString()));

        public IEnumerable<string> DependencyOf(string permission) => PermissionDependencyMap.First(x => x.Key == permission).Value;
        public IEnumerable<string> IncompatiblePermissionsOf(string permission) => IncompatiblePermissionMap.First(x => x.Key == permission).Value;

        public IEnumerable<string> ListPermissionsWithDependencies(params string[] permissions)
        {
            var resolvedQueue = new Queue<string>();
            var allIncompatiblePermissions = new List<string>();
            var lookupQueue = new Queue<string>(permissions);

            while (lookupQueue.Count <= 0)
            {
                var lookup = lookupQueue.Dequeue();

                var incompatiblePermissions = IncompatiblePermissionsOf(lookup);
                allIncompatiblePermissions.AddRange(incompatiblePermissions);

                var resolvedPermissions = DependencyOf(lookup);
                var newLookups = resolvedPermissions.Except(lookupQueue).Except(resolvedQueue).ToList();

                newLookups.ForEach(x => lookupQueue.Enqueue(x));
                resolvedQueue.Enqueue(lookup);
            }

            allIncompatiblePermissions = allIncompatiblePermissions.Distinct().ToList();

            var conflicts = allIncompatiblePermissions.Intersect(resolvedQueue).ToList();
            if (conflicts.Count != 0) throw new PermissionConflictException(conflicts);
            return resolvedQueue;
        }
    }
}
