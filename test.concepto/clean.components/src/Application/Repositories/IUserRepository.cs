﻿using Domain.Users;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Application.Repositories
{
    public interface IUserRepository
    {
        Task<IUser> Get(Guid id);
        Task Add(IUser user);
        Task Update(IUser user);
        IEnumerable<User> Get();
    }
}
