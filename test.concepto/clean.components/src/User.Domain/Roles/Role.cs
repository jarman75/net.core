using System;
using User.Domain.ValueObjects;

namespace User.Domain.Roles
{
    public class Role : IRole
    {
        public Guid Id { get; protected set; }
        public ShortName Name { get; protected set; }
    }
}