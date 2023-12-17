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
using wpfreg.ViewModel;

namespace wpfreg
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application

    {
        public static UserModel CurrentUser { get; set; }
        public static UserModel SelectedUser { get; set; } 
        public static ChatModel SelectedChat { get; set; }
        public static Server Server { get; private set; }
        public static IHost? AppHost { get; private set; }

        public App()
        {
            AppHost = Host.CreateDefaultBuilder()
                .ConfigureServices((HostBuilderContext, services) =>
                {
                    services.AddTransient<RegistrationView>();
                    services.AddTransient<LoginView>();
                    services.AddSingleton<MainWindow>();
                    services.AddSingleton<UserService>();
                    services.AddSingleton<ChatView>();
                    services.AddSingleton<UserRepository>();
                    services.AddSingleton<ChatRepository>();
                    services.AddSingleton<MessageRepository>();
                    services.AddSingleton<EasyTalkContext>();
                    services.AddSingleton<Server>();
                    services.AddSingleton<SearchList>();
                    services.AddSingleton<ChatService>();
                    services.AddSingleton<NavigationViewModel>();

                })
                .Build();
        }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost.StartAsync();
            var startupForm = AppHost.Services.GetRequiredService<LoginView>();
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
