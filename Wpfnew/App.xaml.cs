using BLL.Services.Implementations;
using DAL.Data;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Wpfnew.View;
using DAL.Models;

namespace Wpfnew
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((HostBuilderContext, services) =>
                {
                    services.AddSingleton<RegistrationView>();
                    //services.AddSingleton<UserService>();
                    //services.AddSingleton<UserRepository>();
                    //services.AddSingleton<EasyTalkContext>();   
                    
                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<RegistrationView>();
            startupForm.Show();

            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
