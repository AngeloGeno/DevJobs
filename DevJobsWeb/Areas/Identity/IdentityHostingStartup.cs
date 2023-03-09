﻿using System;
using DevJobsWeb.Areas.Identity.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

[assembly: HostingStartup(typeof(DevJobsWeb.Areas.Identity.IdentityHostingStartup))]
namespace DevJobsWeb.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<AspDevJobsWebContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("AspDevJobsWebContextConnection")));

                services.AddDefaultIdentity<DevJobsWebUser>(options => options.SignIn.RequireConfirmedAccount = true)
                    .AddEntityFrameworkStores<AspDevJobsWebContext>();
            });
        }
    }
}