﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Owin;
using Microsoft.Owin.FileSystems;
using Microsoft.Owin.Security.Infrastructure;
using Microsoft.Owin.Security.OAuth;
using Microsoft.Owin.StaticFiles;
using Owin;

namespace Orleans.HttpApi
{
    public class ApplicationOAuthProvider : OAuthAuthorizationServerProvider
    {
        public override async Task ValidateClientAuthentication(OAuthValidateClientAuthenticationContext context)
        {
            context.Validated();
        }

        public override async Task GrantResourceOwnerCredentials(OAuthGrantResourceOwnerCredentialsContext context)
        {
            context.OwinContext.Response.Headers.Add("Access-Control-Allow-Origin", new[] { "*" });

            if (context.UserName != context.Password)
            {
                context.SetError("invalid_grant", "The user name or password is incorrect.");
                return;
            }

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);
            identity.AddClaim(new Claim("sub", context.UserName));
            identity.AddClaim(new Claim(ClaimTypes.Role, "user"));

            context.Validated(identity);

        }
    }
    public class WebServer
    {
        public WebServer(Router router, string username, string password)
        {
            this.Router = router;
            this.Username = username;
            this.Password = password;
        }

        public Router Router { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }

        async Task HandleRequest(IOwinContext context, Func<Task> func)
        {
            var result = this.Router.Match(context.Request.Path.Value);
            if (null != result)
            {
                try
                {
                    await result(context);
                    return;
                }
                catch (Exception ex)
                {
                    await context.ReturnError(ex);
                }
            }

            context.Response.StatusCode = 404;
        }

        Task BasicAuth(IOwinContext context, Func<Task> func)
        {
            if (!context.Request.Headers.ContainsKey("Authorization")) return context.ReturnUnauthorised();

            //Basic QWxhZGRpbjpvcGVuIHNlc2FtZQ==
            var value = context.Request.Headers["Authorization"];

            var decodedString = Encoding.UTF8.GetString(Convert.FromBase64String(value.Replace("Basic", "").Trim()));

            var parts = decodedString.Split(':');

            if (parts.Length != 2) return context.ReturnUnauthorised();

            if (parts[0] != this.Username && parts[1] != this.Password) return context.ReturnUnauthorised();

            return func();
        }

        public void Configure(IAppBuilder app)
        {
            if (!string.IsNullOrWhiteSpace(this.Username) && !string.IsNullOrWhiteSpace(this.Password))
            {
                // if a username and password are supplied, enable basic auth
                app.Use(BasicAuth);
            }


            string dir = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
            string path = Path.Combine(dir, @"Static");
            if (Directory.Exists(path))
            {
                app.UseFileServer(new FileServerOptions()
                {
                    RequestPath = PathString.Empty,
                    FileSystem = new PhysicalFileSystem(path),
                });

                app.UseStaticFiles(new StaticFileOptions()
                {
                    RequestPath = new PathString("/Static"),
                    FileSystem = new PhysicalFileSystem(path)
                });
            }
            app.Use(HandleRequest);
            var oAuthAuthorizationServerOptions = new OAuthBearerAuthenticationOptions()
            {
               
            };
            app.UseOAuthBearerAuthentication(oAuthAuthorizationServerOptions);
        }
    }
}
