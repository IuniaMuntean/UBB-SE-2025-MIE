using System.ComponentModel;
using System.Threading.Tasks;
using System.Windows.Input;
using EuroTruck.AuthFeature.Helpers;
using EuroTruck.AuthFeature.Model;
using EuroTruck.AuthFeature.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EuroTruck.AuthFeature.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        private readonly AuthService _authService;
        public event PropertyChangedEventHandler PropertyChanged;

        public string Username { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public ICommand RegisterCommand { get; }

        public RegisterViewModel()
        {
            _authService = App.Services.GetRequiredService<AuthService>();
            RegisterCommand = new RelayCommand(async _ => await Register());
        }

        public async Task<bool> Register()
        {
            if (Password != ConfirmPassword)
                return false;

            var user = new User
            {
                Username = Username,
                Password = Password
            };

            return await _authService.RegisterAsync(user);
        }
    }
}
