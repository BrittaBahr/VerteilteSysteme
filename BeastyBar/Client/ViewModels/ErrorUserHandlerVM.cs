using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Text;

namespace Client.ViewModels
{
    public class ErrorUserHandlerVM : INotifyPropertyChanged
    {
        private string loginUserErrorMessage = "";

        private string signupUserErrorMessage = "";

        private string loginPwdError = "";

        private string signupPwdError = "";

        public string LoginUsernameErrorMessage
        {
            get
            {
                return this.loginUserErrorMessage;
            }

            set
            {
                this.loginUserErrorMessage = value;
                this.FireOnPropertyChanged();
            }
        }
        public string SignupUsernameErrorMessage
        {
            get
            {
                return this.signupUserErrorMessage;
            }

            set
            {
                this.signupUserErrorMessage = value;
                this.FireOnPropertyChanged();
            }
        }

        public string LoginPasswordErrorMessage
        {
            get
            {
                return this.loginPwdError;
            }

            set
            {
                this.loginPwdError = value;
                this.FireOnPropertyChanged();
            }
        }

        public string SignupPasswordErrorMessage
        {
            get
            {
                return this.signupPwdError;
            }

            set
            {
                this.signupPwdError = value;
                this.FireOnPropertyChanged();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void FireOnPropertyChanged([CallerMemberName]string propertyName = null)
        {
            this.PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }


    }
}
