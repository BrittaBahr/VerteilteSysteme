namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Management.Automation;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Crocodile : Animal
    {
        public Crocodile() : base(10, true)
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

            if (index < 0 & !isPushing)
            {
                animals.Insert(0, this);
                return animals;
            }

            for (int i = index; i < animals.Count; i++)
            {
                Animal a = animals[i];

                if (a == this)
                {
                    continue;
                }

                bool canPass = a.Accept<bool>(this);

                if (!canPass)
                {
                    animals.Remove(this);
                    animals.Insert(index, this);
                    break;
                }

                a.Dies();
                ++index;
            }

            return animals;
        }

        public override bool Visit(Lion lion)
        {
            bool isYummy = lion.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                lion.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Hippo hippo)
        {
            bool isYummy = hippo.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                hippo.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Crocodile crocodile)
        {
            bool isYummy = crocodile.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                crocodile.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Snake snake)
        {
            bool isYummy = snake.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                snake.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Giraffe giraffe)
        {
            bool isYummy = giraffe.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                giraffe.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Zebra zebra)
        {
            return false;
        }

        public override bool Visit(Seal seal)
        {
            bool isYummy = seal.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                seal.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Chameleon chameleon)
        {
            bool isYummy = chameleon.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                chameleon.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Monkey monkey)
        {
            bool isYummy = monkey.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                monkey.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Kangaroo kangaroo)
        {
            bool isYummy = kangaroo.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                kangaroo.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Parrot parrot)
        {
            bool isYummy = parrot.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                parrot.Dies();
            }

            return isYummy;
        }

        public override bool Visit(Stunk stunk)
        {
            bool isYummy = stunk.Strength < this.Strength ? true : false;

            if (isYummy)
            {
                stunk.Dies();
            }

            return isYummy;
        }
    }
}
