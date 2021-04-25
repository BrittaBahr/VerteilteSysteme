namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Stunk : Animal
    {
        public Stunk() : base(1, false)
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

            foreach (Animal a in animals)
            {
                a.Accept<bool>(this);
            }

            animals.Insert(0, this);

            return animals;
        }

        public override bool Visit(Lion lion)
        {
            lion.Dies();

            return false;
        }

        public override bool Visit(Hippo hippo)
        {
            hippo.Dies();

            return false;
        }

        public override bool Visit(Crocodile crocodile)
        {
            crocodile.Dies();

            return false;
        }

        public override bool Visit(Snake snake)
        {
            snake.Dies();

            return false;
        }

        public override bool Visit(Giraffe giraffe)
        {
            giraffe.Dies();

            return false;
        }

        public override bool Visit(Zebra zebra)
        {
            zebra.Dies();

            return false;
        }

        public override bool Visit(Seal seal)
        {
            seal.Dies();

            return false;
        }

        public override bool Visit(Chameleon chameleon)
        {
            chameleon.Dies();

            return false;
        }

        public override bool Visit(Monkey monkey)
        {
            monkey.Dies();

            return false;
        }

        public override bool Visit(Kangaroo kangaroo)
        {
            kangaroo.Dies();

            return false;
        }

        public override bool Visit(Parrot parrot)
        {
            parrot.Dies();

            return false;
        }

        public override bool Visit(Stunk stunk)
        {
            return false;
        }
    }
}
