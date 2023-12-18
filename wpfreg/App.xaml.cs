using System.Windows;
using BLL.Models;
using BLL.Services.Implementations;
using DAL.Data;
using DAL.Repositories;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using wpfreg.Net;
using wpfreg.View;
using wpfreg.ViewModel;

namespace wpfreg
{
    public partial class App : Application
    {
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

        public static UserModel? CurrentUser { get; set; }

        public static UserModel? SelectedUser { get; set; }

        public static ChatModel? SelectedChat { get; set; }

        public static Server? Server { get; private set; }

        public static IHost? AppHost { get; private set; }

        protected override async void OnStartup(StartupEventArgs e)
        {
            await AppHost!.StartAsync();
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
