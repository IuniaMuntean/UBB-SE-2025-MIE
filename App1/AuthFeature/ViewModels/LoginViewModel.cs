using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EuroTruck.AuthFeature.Helpers;
using EuroTruck.AuthFeature.Model;
using EuroTruck.AuthFeature.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EuroTruck.AuthFeature.ViewModels
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public string Password { get; set; }

        public ICommand LoginCommand { get; }

        public LoginViewModel()
        {
            _authService = App.Services.GetRequiredService<AuthService>();
            LoginCommand = new RelayCommand(async _ => await Login());
        }

        public async Task<bool> Login()
        {
            var user = new User
            {
                Username = Username,
                Password = Password
            };

            return await _authService.LoginAsync(user);
        }
    }
}
