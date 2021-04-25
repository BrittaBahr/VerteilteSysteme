namespace BeastyBarGameLogic.Animals
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Management.Automation;
    using System.Text;
    using BeastyBarGameLogic.Animals.Species;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public abstract class Animal : IAnimalVisitable, IAnimalVisitor<bool>, IComparable<Animal>
    {
        private bool life;

        public readonly bool SuperPower;

        public readonly int Strength;

        public Animal(int strength, bool superPower)
        {
            if (strength < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(strength));
            }

            this.SuperPower = superPower;
            this.life = true;
            this.Strength = strength;
        }

        public bool Life
        {
            get
            {
                return this.life;
            }
        }

        public void Dies()
        {
            if (this.Life)
            {
                this.life = false;
            }
        }

        public abstract List<Animal> Queue(List<Animal> animals, int index, bool isPushing);

        public abstract T Accept<T>(IAnimalVisitor<T> visitor);

        public abstract bool Visit(Lion lion);

        public abstract bool Visit(Hippo hippo);

        public abstract bool Visit(Crocodile crocodile);

        public abstract bool Visit(Snake snake);

        public abstract bool Visit(Giraffe giraffe);

        public abstract bool Visit(Zebra zebra);

        public abstract bool Visit(Seal seal);

        public abstract bool Visit(Chameleon chameleon);

        public abstract bool Visit(Monkey monkey);

        public abstract bool Visit(Kangaroo kangaroo);

        public abstract bool Visit(Parrot parrot);

        public abstract bool Visit(Stunk stunk);

        public abstract int CompareTo([AllowNull] Animal other);
    }
}
