using App1.AuthFeature.Data;
using App1.AuthFeature.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using System;



namespace App1
{
    
    public partial class App : Application
    {
        public static IServiceProvider Services { get; private set; }
        public App()
        {
            this.InitializeComponent();
            SQL_Scripts.DBSync.syncDB();
            var services = new ServiceCollection();
            ConfigureServices(services);
            Services = services.BuildServiceProvider();
        }

        private void ConfigureServices(IServiceCollection services)
        {
            var connStr = "Host=localhost;Port=5432;Database=postgres;Username=postgres;Password=admin;";
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(connStr));
            services.AddScoped<AuthService>();

        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = new MainWindow();
            m_window.Activate();
        }

        private Window? m_window;
    }
}
