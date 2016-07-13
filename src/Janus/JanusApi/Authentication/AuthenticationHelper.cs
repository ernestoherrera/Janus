using FluentSql;
using FluentSql.Contracts;
using JanusCore.Extensions;
using JanusData.Factories;
using JanusData.Repositories;
using JanusModels.Models;
using Nancy.Security;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using System.Web;

namespace Janus.Authentication
{
    public class AuthenticationHelper
    {
        private readonly GenericFactory Factory;

        public AuthenticationHelper(GenericFactory factory)
        {
            Factory = factory;
        }

        public IUserIdentity ValidateRequest(RequestInfo request)
        {
            if (!request.IsWellFormed)
                return null;

            return ValidateBearerAuthorization(request.AccessToken);
        }

        private IUserIdentity ValidateBearerAuthorization(string accessToken)
        {
            var repo = Factory.Get<Repository>();
            var token = repo.Find<AccessToken>(t => t.Token == accessToken).FirstOrDefault();

            if (token == null || !token.IsValid) return null;

            var user = repo.Find<User>(u => u.Id == token.UserId).FirstOrDefault();

            if (user == null) return null;

            return new AuthenticatedUser(user);
        }

        public static string HashPassword(string password, string salt = null)
        {
            if (!string.IsNullOrWhiteSpace(password) && !string.IsNullOrWhiteSpace(salt))
                password += salt.ToMd5();

            return password.ToMd5();
        }

        public async Task<string> GenerateUniqueAccessToken()
        {
            string token = null;
            var topSearchNumber = 10;
            var store = Factory.Get<EntityStore>();
            Person userWithSameToken = null;
            var iCounter = 0;

            do
            {
                iCounter++;

                token = GenerateAuthorizationToken();

                userWithSameToken = await store.GetSingleAsync<Person>(u => u.ApiKey == token);

            }
            while (userWithSameToken != null && iCounter <= topSearchNumber);

            return await Task.Run(() => { return iCounter <= topSearchNumber ? token : string.Empty; });
        }

        public static string GenerateAuthorizationToken(int length = 32)
        {
            var accessKey = new byte[length];

            using (var rng = new RNGCryptoServiceProvider())
            {
                rng.GetBytes(accessKey);
            }

            return Convert.ToBase64String(accessKey);
        }
    }
}