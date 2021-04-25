using Client.Models;
using Client.Services;
using GameLibrary;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Security;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client.ViewModels
{
    public class LoginVM : INotifyPropertyChanged
    {
        public event EventHandler<AuthEventArgs> SuccessfulAuth;

        public event PropertyChangedEventHandler PropertyChanged;

        private RestService service = new RestService();

        private bool loginBtnEnabled = true;

        private string loginBtnContent = "Login";

        private bool SignUpBtnEnabled = true;

        private string signUpBtnContent = "Sign Up";

        public LoginVM()
        {
            this.ErrorHandler = new ErrorUserHandlerVM();
            this.LoginCommand = new Command(async obj => await this.ExecuteLoginCommand());
            this.SignupCommand = new Command(async obj => await this.ExecuteSignupCommand());
        }

        public ErrorUserHandlerVM ErrorHandler { get; set; }

        public string LoginUsername { get; set; }

        protected void FireOnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


        /// <summary>
        /// No clean code, in this case encryption is not really an issue
        /// </summary>
        public string LoginPassword { get; set; }

        public string SignupUsername { get; set; }


        /// <summary>
        /// No clean code, in this case encryption is not really an issue
        /// </summary>
        public string SignupPassword { get; set; }

        public ICommand SignupCommand { get; }

        public ICommand LoginCommand { get; }

        public bool SignupBtnEnabled
        {
            get { return this.SignUpBtnEnabled; }
            set
            {
                this.SignUpBtnEnabled = value;
                this.FireOnPropertyChanged();
            }
        }

        public string SignUpBtnContent
        {
            get { return this.signUpBtnContent; }
            set
            {
                this.signUpBtnContent = value;
                this.FireOnPropertyChanged();
            }
        }

        public bool LoginBtnEnabled
        {
            get { return this.loginBtnEnabled; }
            set
            {
                this.loginBtnEnabled = value;
                this.FireOnPropertyChanged();
            }
        }

        public string LoginBtnContent
        {
            get { return this.loginBtnContent; }
            set
            {
                this.loginBtnContent = value;
                this.FireOnPropertyChanged();
            }
        }

        /// <summary>
        /// Player types in his username and connects to the server.
        /// A request will be sent to the server containing the player name.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task ExecuteLoginCommand()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.LoginUsername))
                {
                    if (!string.IsNullOrEmpty(this.LoginPassword))
                    {
                        this.LoginBtnEnabled = false;
                        this.LoginBtnContent = "Loading...";
                        var hash = this.CreateHash(this.LoginPassword);
                        var response = await this.service.Login(this.LoginUsername, hash);

                        if (!response.WasSuccessful)
                        {
                            this.ErrorHandler.LoginPasswordErrorMessage = "Incorrect credentials.";
                            this.LoginBtnEnabled = true;
                            this.LoginBtnContent = "Login";
                        }
                        else
                        {
                            this.FireOnSuccessfulAuth(response.UserId, this.LoginUsername, response.JwToken);
                        }
                    }
                    else
                    {
                        this.ErrorHandler.LoginPasswordErrorMessage = "Empty password";
                    }
                }
                else
                {
                    this.ErrorHandler.LoginUsernameErrorMessage = "Enter a username";
                }
            }
            catch
            {
                this.ErrorHandler.LoginPasswordErrorMessage = "No connection";
                this.LoginBtnEnabled = true;
                this.LoginBtnContent = "Login";
            }
        }

        /// <summary>
        /// Player types in his username and connects to the server.
        /// A request will be sent to the server containing the player name.
        /// </summary>
        /// <returns>A Task that represents the asynchronous method.</returns>
        private async Task ExecuteSignupCommand()
        {
            try
            {
                if (!string.IsNullOrEmpty(this.SignupUsername))
                {
                    if (!string.IsNullOrEmpty(this.SignupPassword))
                    {
                        this.SignupBtnEnabled = false;
                        this.SignUpBtnContent = "Loading ...";
                        var hash = this.CreateHash(this.SignupPassword);
                        var response = await this.service.AddUser(this.SignupUsername, hash);

                        if (!response.WasSuccessful)
                        {
                            this.ErrorHandler.SignupPasswordErrorMessage = response.ErrorMessage;
                            this.SignupBtnEnabled = true;
                            this.SignUpBtnContent = "Sign Up";
                        }
                        else
                        {
                            this.FireOnSuccessfulAuth(response.UserId, this.SignupUsername, response.JwToken);
                        }
                    }
                    else
                    {
                        this.ErrorHandler.SignupPasswordErrorMessage = "Empty password";
                    }
                }
                else
                {
                    this.ErrorHandler.SignupUsernameErrorMessage = "Enter a username";
                }
            }
            catch
            {
                this.ErrorHandler.SignupPasswordErrorMessage = "No connection";
                this.SignupBtnEnabled = true;
                this.SignUpBtnContent = "Sign Up";
            }
        }

        private bool visibilityLogin = false;

        public bool VisibilityLogin
        {
            get { return visibilityLogin; }
            set { visibilityLogin = value; this.FireOnPropertyChanged(); }
        }


        protected virtual void FireOnSuccessfulAuth(int id, string playerName, string token = "")
        {
            this.SuccessfulAuth?.Invoke(this, new AuthEventArgs(id, playerName, token));
        }

        /// <summary>
        /// Hashing the data in SHA256
        /// </summary>
        /// <param name="data"></param>
        /// <returns></returns>
        private string CreateHash(string data)
        {
            using (SHA256 sha256Hash = SHA256.Create())
            {
                byte[] bytes = sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(data));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
