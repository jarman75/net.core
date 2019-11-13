using Domain.ValueObjects;
using System;

namespace Domain.Roles
{
    public class Role : IRole
    {
        public Guid Id { get; protected set; }
        public ShortName Name { get; protected set; }
    }
}