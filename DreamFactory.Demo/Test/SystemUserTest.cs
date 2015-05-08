﻿namespace DreamFactory.Demo.Test
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Linq;
    using System.Threading.Tasks;
    using DreamFactory.Api;
    using DreamFactory.Model.Database;
    using DreamFactory.Model.System.User;
    using DreamFactory.Rest;

    public class SystemUserTest : IRunnable
    {
        private const string NewEmail = "user@mail.com";

// ReSharper disable PossibleMultipleEnumeration
        public async Task RunAsync(IRestContext context)
        {
            ISystemApi systemApi = context.Factory.CreateSystemApi();

            IEnumerable<UserResponse> users = await systemApi.GetUsersAsync(new SqlQuery());
            Console.WriteLine("GetUsersAsync(): {0}", users.Select(x => x.display_name).ToStringList());

            UserResponse user = users.SingleOrDefault(x => x.email == NewEmail);
            if (user != null)
            {
                await DeleteUser(user, systemApi);
            }

            UserRequest newUser = new UserRequest
            {
                first_name = "Andrei",
                last_name = "Smirnov",
                display_name = "pinebit",
                email = NewEmail,
                password = "dream",
                is_active = true
            };

            users = await systemApi.CreateUsersAsync(new SqlQuery(), newUser);
            user = users.Single(x => x.email == NewEmail);
            Console.WriteLine("CreateUsersAsync(): {0}", context.ContentSerializer.Serialize(user));

            newUser.id = user.id;
            newUser.display_name = "Andrei Smirnov";
            user = (await systemApi.UpdateUsersAsync(new SqlQuery(), newUser)).Single(x => x.email == NewEmail);
            Console.WriteLine("UpdateUsersAsync(): new display_name={0}", user.display_name);

            await DeleteUser(user, systemApi);
        }

        private static async Task DeleteUser(UserResponse user, ISystemApi systemApi)
        {
            Debug.Assert(user.id.HasValue, "User ID must be set");
            await systemApi.DeleteUsersAsync(user.id.Value);
            Console.WriteLine("DeleteUsersAsync():: id={0}", user.id);
        }
    }
}