using FluentSql;
using Janus.Authentication;
using Janus.Support;
using JanusData.Factories;
using JanusData.Repositories;
using JanusModels.Models;
using Nancy;
using Nancy.ModelBinding;
using System.Linq;

namespace Janus.Modules
{
    public class AuthenticationModule : NancyModule
    {
        private const string FAILED_AUTHENTICATION = "Invalid username or password.";
        private const string BYPASS_AUTHENTICATION = "User is in the database, but Authentication is disabled.You are in.";
        private string AUTHENTICATION_SUCCESSFUL = "User {0} was authenticated {1}";

        public AuthenticationModule(GenericFactory factory)
        //            : base("auth")
        {
            Post["/Authentication", runAsync: true] = async (_, ct) =>
            {
                var loginRequest = this.Bind<LoginRequest>();
                var repo = factory.Get<Repository>();
                var store = factory.Get<EntityStore>();

                var persons = repo.Find<Person>(u => u.Username == loginRequest.username);
                var user = persons.FirstOrDefault();                
                

                if (user == null)
                    return Response.AsJson(new AuthenticationResponse(false, FAILED_AUTHENTICATION));

                if (user.Password == null)
                    Response.AsJson(new AuthenticationResponse(false, FAILED_AUTHENTICATION));

                if (Constants.DISABLE_LOGIN_AUTHENTICATION)
                {
                    //TODO: Log this most unlikely scenario. Only Dev 
                    return Response.AsText(BYPASS_AUTHENTICATION);
                }

                var hashedPassword = AuthenticationHelper.HashPassword(loginRequest.password, loginRequest.username);                

                if (user.Password != null && user.Password != hashedPassword)
                {
                    //TODO: LOG the failed attempt to log in
                    Response.AsJson(new AuthenticationResponse(false, FAILED_AUTHENTICATION));
                }

                if (user == null)
                {
                    var authHelper = factory.Get<AuthenticationHelper>();
                    var apiKey = await authHelper.GenerateUniqueAccessToken();

                    if (string.IsNullOrEmpty(apiKey))
                    {
                        //TODO: Log failed token creationg
                        Response.AsJson(new AuthenticationResponse(false, FAILED_AUTHENTICATION));
                    }

                    user.ApiKey = apiKey;
                    repo.Update<Person>(user);

                }

                //TODO: LOG successful login AUTHENTICATION_SUCCESSFUL
                return Response.AsJson(new AuthenticationResponse(user, user.ApiKey, false));
            };
        }
    }
}