namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Management.Automation;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Snake : Animal
    {
        public Snake() : base(9, false)
        {
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

            if (index >= animals.Count - 1 & index >= 0)
            {
                return animals;
            }

            if (isPushing)
            {
                return animals;
            }

            animals.Insert(0, this);
            animals.Sort();
            animals.Reverse();

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
