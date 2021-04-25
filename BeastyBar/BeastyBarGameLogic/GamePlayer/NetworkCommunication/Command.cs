using System;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Client
{
    internal class Command : ICommand
    {
        private Func<object, Task> p;

        public Command(Func<object, Task> p)
        {
            this.p = p;
        }
    }
}