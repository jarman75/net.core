using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace User.Domain.Users
{
    public sealed class RoleCollection
    {
        private readonly IList<Guid> _rolesIds;

        public RoleCollection()
        {
            _rolesIds = new List<Guid>();
        }

        public void Add(IEnumerable<Guid> roles)
        {
            foreach (var role in roles)
            {
                Add(role);
            }
        }

        public void Add(Guid roleId) => _rolesIds.Add(roleId);

        public IReadOnlyCollection<Guid> GetRoleIds()
        {
            IReadOnlyCollection<Guid> rolesIds = new ReadOnlyCollection<Guid>(_rolesIds);
            return rolesIds;
        }

    }
}
