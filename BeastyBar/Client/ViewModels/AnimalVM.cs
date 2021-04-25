using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using BeastyBarGameLogic.Animals;
using Client.ClassesForView;

namespace Client.ViewModel
{
    public class AnimalVM
    {
        private readonly Animal model;

        public AnimalVM(Animal model)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(this.model));
        }

        public T Accept<T>(IAnimalVisitor<T> visitor)
        {
            return this.model.Accept(visitor);
        }
    }
}
