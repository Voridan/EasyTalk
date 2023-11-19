using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using wpfreg.View;
using DAL.Models;
using System.Collections;
using BLL.Services.Implementations;
using DAL.Data;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BLL.Models;
using wpfreg.Net;

namespace wpfreg
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        public static UserModel CurrentUser { get; set; }
        public static Server Server { get; private set; }
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((HostBuilderContext, services) =>
                {
                    services.AddSingleton<RegistrationView>();
                    services.AddSingleton<LoginView>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<UserService>();
                    services.AddSingleton<ChatView>();
                    services.AddSingleton<UserRepository>();
                    services.AddSingleton<EasyTalkContext>();
                    services.AddSingleton<Server>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<MainWindow>();
            startupForm.Show();
            Server = new Server();
            base.OnStartup(e);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            await AppHost!.StopAsync();
            base.OnExit(e);
        }
    }
}
