using Janus.Authentication;
using JanusData.Factories;
using JanusData.Repositories;
using JanusModels.Models;
using Nancy;
using Nancy.ModelBinding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Janus.Modules
{
    public class UserModule : NancyModule
    {

        public UserModule(GenericFactory factory)
            : base()
        {
            //this.RequiresAuthentication();

            Get["/"] = _ => View["LoginPage"];

            Get["/Register"] = _ => View["Register"];

            Post["/AddUser", runAsync: true] = async (_, ct) =>
            {
                var userRequest = this.Bind<RegisterRequest>();

                var repo = factory.Get<Repository>();
                var authHelper = factory.Get<AuthenticationHelper>();

                var userInDb = await repo.FindSingleAsync<User>(u => u.UserName == userRequest.username);

                if (userInDb != null)
                    return Response.AsText("User already in db. Select another username");


                var password = AuthenticationHelper.HashPassword(userRequest.password, userRequest.username);
                var newPerson = new Person { FirstName = "Ernesto", LastName = "Herrera", Suffix = "Jr", Company = "TradePMR" };
                var newUser = new User { UserName = userRequest.username, Password = password, Enabled = true };

                newPerson = repo.Add<Person>(newPerson);

                newUser.PersonId = newPerson.Id;
                repo.Add<User>(newUser);

                return Response.AsText("You are registered");
            };
        }
    }
}