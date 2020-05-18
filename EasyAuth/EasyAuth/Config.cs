using IdentityModel;
using IdentityServer4.Models;
using IdentityServer4.Test;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyAuth
{
    public static class Config
    {
        public static List<ApiResource> GetApiResources()
        {
            var result = new List<ApiResource>()
            {
                new ApiResource("api1")
            };

            return result;
        }

        public static List<IdentityResource> GetIdentityResources() =>
            new List<IdentityResource>
            {
                new IdentityResources.OpenId()
            };

        public static List<Client> GetClients()
        {
            var result = new List<Client>();

            //ClientCredentials - 客户端秘钥模式，用于无用户的内部客户端
            result.Add(new Client
            {
                ClientId = "mvc1",
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ClientCredentials,
                AllowedScopes = new List<string> { "api1" },
                AccessTokenType = AccessTokenType.Jwt,
                AccessTokenLifetime = 30,
                AllowOfflineAccess = true, //本模式由于安全原因无法使用refresh_token，这里设置也没用
            });

            //Password - 密码模式，简单的用户名密码授权
            result.Add(new Client
            {
                ClientId = "mvc2",
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                AllowedScopes = new List<string> { "api1", "openid" },
                AllowOfflineAccess = true
            });

            //Code - 授权码模式
            result.Add(new Client
            {
                ClientId = "mvc3",
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Code,
                AllowedScopes = new List<string> { "api1", "openid" },
                AllowOfflineAccess = true,
                RedirectUris = new List<string> { "https://localhost:5003/signin-oidc" },
                RequireConsent = false, //是否弹出用户同意页面
                FrontChannelLogoutUri = "https://localhost:5003/user/frontchannel-logout",
                FrontChannelLogoutSessionRequired = false,
                BackChannelLogoutUri = "https://localhost:5003/signout-callback-oidc"
            });

            //Hybrid - 混合流模式
            result.Add(new Client
            {
                ClientId = "mvc4",
                ClientSecrets = new List<Secret> { new Secret("secret".Sha256()) },
                AllowedGrantTypes = GrantTypes.Hybrid,
                AllowedScopes = new List<string> { "api1", "openid" },
                AllowOfflineAccess = true,
                RedirectUris = new List<string> { "https://localhost:5004/signin-oidc" },
                RequireConsent = false, //是否弹出用户同意页面
                FrontChannelLogoutUri = "https://localhost:5004/user/frontchannel-logout",
                FrontChannelLogoutSessionRequired = false,
                BackChannelLogoutUri = "https://localhost:5004/signout-callback-oidc"
            });

            return result;
        }

        public static List<TestUser> GetTestUsers()
        {
            var result = new List<TestUser>();

            result.Add(new TestUser
            {
                SubjectId = "gyt", 
                Username = "gyt",
                Password = "gyt"
            });

            return result;
        }
    }
}
