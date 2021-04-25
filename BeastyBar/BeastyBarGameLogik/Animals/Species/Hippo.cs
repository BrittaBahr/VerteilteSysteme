namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Management.Automation;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Hippo : Animal
    {
        public Hippo() : base(11, true)
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

                if (canPass & index >= 0)
                {
                    index++;
                }
                else
                {
                    break;
                }
            }

            animals.Remove(this);
            animals.Insert(index, this);

            return animals;
        }

        public override bool Visit(Lion lion)
        {
            return lion.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Hippo hippo)
        {
            return hippo.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Crocodile crocodile)
        {
            return crocodile.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Snake snake)
        {
            return snake.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Giraffe giraffe)
        {
            return giraffe.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Zebra zebra)
        {
            return false;
        }

        public override bool Visit(Seal seal)
        {
            return seal.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Chameleon chameleon)
        {
            return chameleon.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Monkey monkey)
        {
            return monkey.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Kangaroo kangaroo)
        {
            return kangaroo.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Parrot parrot)
        {
            return parrot.Strength < this.Strength ? true : false;
        }

        public override bool Visit(Stunk stunk)
        {
            return stunk.Strength < this.Strength ? true : false;
        }
    }
}
