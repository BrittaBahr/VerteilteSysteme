namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Lion : Animal
    {
        private List<Animal> killedAnimals;

        public Lion(): base(12, false)
        {
            this.killedAnimals = new List<Animal>();
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

            Animal[] initialQueuedAnimals = animals.ToArray();

            foreach (Animal a in animals)
            {
                bool canPass = a.Accept<bool>(this);

                if (!canPass & !this.Life)
                {
                    return initialQueuedAnimals.ToList();
                }
            }

            animals.Insert(animals.Count, this);

            return animals;
        }

        public override bool Visit(Lion lion)
        {
            this.Dies();
            return false;
        }

        public override bool Visit(Hippo hippo)
        {
            return true;
        }

        public override bool Visit(Crocodile crocodile)
        {
            return true;
        }

        public override bool Visit(Snake snake)
        {
            return true;
        }

        public override bool Visit(Giraffe giraffe)
        {
            return true;
        }

        public override bool Visit(Zebra zebra)
        {
            return true;
        }

        public override bool Visit(Seal seal)
        {
            return true;
        }

        public override bool Visit(Chameleon chameleon)
        {
            return true;
        }

        public override bool Visit(Monkey monkey)
        {
            monkey.Dies();
            return true;
        }

        public override bool Visit(Kangaroo kangaroo)
        {
            return true;
        }

        public override bool Visit(Parrot parrot)
        {
            return true;
        }

        public override bool Visit(Stunk stunk)
        {
            return true;
        }
    }
}
