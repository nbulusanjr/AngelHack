﻿using Microsoft.Owin;
using Owin;

[assembly: OwinStartupAttribute(typeof(WeDo.Startup))]
namespace WeDo
{
    public partial class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            ConfigureAuth(app);
            app.MapSignalR();
        }
    }
}
