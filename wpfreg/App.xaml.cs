using BLL.Models;
using BLL.Services.Implementations;
using DAL.Data;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Serilog;
using System;
using System.IO;
using System.Windows;
using wpfreg.View;

namespace wpfreg
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        public static UserModel CurrentUser { get; set; }
        public static IHost? AppHost { get; private set; }

        public App()
        {
            string logsDirectory = Path.Combine(Environment.CurrentDirectory, "logs");

            AppHost = Host.CreateDefaultBuilder()
                .UseSerilog((host, loggerConfiguration) =>
                {
                    loggerConfiguration
                    .WriteTo.File(Path.Combine(logsDirectory, "TestRegistrationLogic.txt"), rollingInterval: RollingInterval.Day)
                    .MinimumLevel.Error();
                })
                .ConfigureServices((HostBuilderContext, services) =>
                {
                    services.AddSingleton<RegistrationView>();
                    services.AddSingleton<LoginView>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<UserService>();
                    services.AddSingleton<UserRepository>();
                    services.AddSingleton<EasyTalkContext>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
           await AppHost!.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<LoginView>();

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
