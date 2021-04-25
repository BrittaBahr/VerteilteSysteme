namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Monkey : Animal
    {
        private List<Monkey> monkeyGang;

        public Monkey() : base(4, false)
        {
            this.monkeyGang = new List<Monkey>();
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

        public override T Accept<T>(IAnimalVisitor<T> visitor)
        {
            return visitor.Visit(this);
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

            for (int i = 0; i < animals.Count; i++)
            {
                Animal a = animals[i];
                bool areMonkey = a.Accept<bool>(this);

                if (areMonkey)
                {
                    animals.Remove(a);
                    --i;
                }
            }

            if (this.monkeyGang.Count > 0)
            {
                animals.InsertRange(animals.Count - 1, this.monkeyGang);
                animals.Insert(animals.Count - 1, this);
            }
            else
            {
                animals.Insert(0, this);
            }

            return animals;
        }

        public override bool Visit(Lion lion)
        {
            return false;
        }

        public override bool Visit(Hippo hippo)
        {
            if (this.monkeyGang.Count != 0)
            {
                hippo.Dies();
            }

            return false;
        }

        public override bool Visit(Crocodile crocodile)
        {
            if (this.monkeyGang.Count != 0)
            {
                crocodile.Dies();
            }

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
            this.monkeyGang.Insert(0, monkey);

            return true;
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
