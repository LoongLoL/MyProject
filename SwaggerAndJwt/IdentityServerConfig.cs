using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using IdentityServer4.Models;
using IdentityServer4.Test;

namespace SwaggerAndJwt
{
    public static class IdentityServerConfig
    {
        /// <summary>
        /// 测试的账号和密码
        /// </summary>
        /// <returns></returns>
        public static List<TestUser> GetTestUsers()
        {
            return new List<TestUser>
            {
                new TestUser()
                {
                    SubjectId = "1",
                    Username = "test",
                    Password = "123456"
                }
            };
        }

        private static readonly string apiName = "swagger_api";

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile(),
            };
        }
        /// <summary>
        /// API信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<ApiResource> GetApis()
        {
            return new[]
            {
                new ApiResource(apiName, apiName)
            };
        }
        /// <summary>
        /// 客服端信息
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<Client> GetClients()
        {
            return new[]
            {
                new Client
                {
                    //ClientId = "swagger_client",//客服端名称
                    //ClientName = "Swagger UI client",//描述
                    //AllowedGrantTypes = GrantTypes.Implicit,//Implicit 方式
                    //AllowAccessTokensViaBrowser = true,//是否通过浏览器为此客户端传输访问令牌
                    //RedirectUris =
                    //{
                    //    "http://localhost:5001/swagger/oauth2-redirect.html"
                    //},
                    //AllowedScopes = { apiName }

                    ClientId ="swagger_client",
                    AllowedGrantTypes = new List<string>()
                    {
                        GrantTypes.ResourceOwnerPassword.FirstOrDefault(),//Resource Owner Password模式
                    },
                    ClientSecrets = {new Secret("user_secret".Sha256()) },
                    AllowedScopes= {apiName},
                    AccessTokenLifetime = 36000,
                }
            };
        }
    }
}
