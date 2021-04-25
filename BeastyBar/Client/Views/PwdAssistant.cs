using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace Client.Views
{
    public static class PwdAssistant
    {
        public static readonly DependencyProperty BoundPassword =
            DependencyProperty.RegisterAttached("BoundPassword", typeof(string), typeof(PwdAssistant), new PropertyMetadata(string.Empty, OnBoundPasswordChanged));

        public static readonly DependencyProperty BindPassword = DependencyProperty.RegisterAttached(
            "BindPassword", typeof(bool), typeof(PwdAssistant), new PropertyMetadata(false, OnBindPwdChanged));

        private static readonly DependencyProperty UpdatingPassword =
            DependencyProperty.RegisterAttached("UpdatingPassword", typeof(bool), typeof(PwdAssistant), new PropertyMetadata(false));

        public static bool GetBindPassword(DependencyObject dependency)
        {
            return (bool)dependency.GetValue(BindPassword);
        }

        public static void SetBoundPassword(DependencyObject dependency, string value)
        {
            dependency.SetValue(BoundPassword, value);
        }

        private static bool GetUpdatingPassword(DependencyObject dependency)
        {
            return (bool)dependency.GetValue(UpdatingPassword);
        }

        private static void SetUpdatingPassword(DependencyObject dependency, bool value)
        {
            dependency.SetValue(UpdatingPassword, value);
        }

        private static void OnBoundPasswordChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = d as PasswordBox;

            // handle this event when the property is attached to a PasswordBox + BindPassword attached property has been set to true
            if (d == null || !GetBindPassword(d))
            {
                return;
            }

            // avoid recursive updating
            box.PasswordChanged -= HandlePwdChanged;
            string newPassword = (string)e.NewValue;
            if (!GetUpdatingPassword(box))
            {
                box.Password = newPassword;
            }

            box.PasswordChanged += HandlePwdChanged;
        }

        private static void OnBindPwdChanged(DependencyObject dependency, DependencyPropertyChangedEventArgs e)
        {
            PasswordBox box = dependency as PasswordBox;
            if (box == null)
            {
                return;
            }

            bool wasBound = (bool)(e.OldValue);
            bool needToBind = (bool)(e.NewValue);

            if (wasBound)
            {
                box.PasswordChanged -= HandlePwdChanged;
            }

            if (needToBind)
            {
                box.PasswordChanged += HandlePwdChanged;
            }
        }

        private static void HandlePwdChanged(object sender, RoutedEventArgs e)
        {
            PasswordBox box = sender as PasswordBox;
            SetUpdatingPassword(box, true);
            SetBoundPassword(box, box.Password);
            SetUpdatingPassword(box, false);
        }

        public static void SetBindPassword(DependencyObject dependency, bool value)
        {
            dependency.SetValue(BindPassword, value);
        }
    }
}
