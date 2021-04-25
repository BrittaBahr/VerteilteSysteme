namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Parrot : Animal
    {
        // can be null if the parrot doesn't hate anybody at the moment
        private Animal hatedAnimal;

        public Parrot() : base(2, false)
        {
        }

        public Animal HatedAnimal
        {
            get
            {
                return this.hatedAnimal;
            }

            set
            {
                this.hatedAnimal = value;
            }
        }

        public override T Accept<T>(IAnimalVisitor<T> visitor)
        {
            return visitor.Visit(this);
        }

        public override int CompareTo([AllowNull] Animal other)
        {
            if (this.Strength > other.Strength)
            {
                return -1;
            }
            else if (this.Strength > other.Strength)
            {
                return 0;
            }
            else
            {
                return 1;
            }
        }

        public override List<Animal> Queue(List<Animal> animals, int index, bool isPushing)
        {
            if (animals == null)
            {
                throw new ArgumentNullException(nameof(animals));
            }

            if (isPushing)
            {
                return animals;
            }

            if (this == this.HatedAnimal)
            {
                this.hatedAnimal = null;
                this.Dies();
                return animals;
            }

            if (animals.Count == 0 & index == -1 & this.HatedAnimal == null)
            {
                animals.Insert(0, this);
                return animals;
            }

            if (index >= animals.Count - 1 & index >= 0)
            {
                return animals;
            }

            foreach (Animal a in animals)
            {
                if (this.HatedAnimal != null & a == this.hatedAnimal)
                {
                    a.Dies();
                }
            }

            animals.Insert(0, this);
            this.hatedAnimal = null;

            return animals;
        }

        public override bool Visit(Lion lion)
        {
            return false;
        }

        public override bool Visit(Hippo hippo)
        {
            return false;
        }

        public override bool Visit(Crocodile crocodile)
        {
            return false;
        }

        public override bool Visit(Snake snake)
        {
            return false;
        }

        public override bool Visit(Giraffe giraffe)
        {
            return false;
        }

        public override bool Visit(Zebra zebra)
        {
            return false;
        }

        public override bool Visit(Seal seal)
        {
            return false;
        }

        public override bool Visit(Chameleon chameleon)
        {
            return false;
        }

        public override bool Visit(Monkey monkey)
        {
            return false;
        }

        public override bool Visit(Kangaroo kangaroo)
        {
            return false;
        }

        public override bool Visit(Parrot parrot)
        {
            return false;
        }

        public override bool Visit(Stunk stunk)
        {
            return false;
        }
    }
}
