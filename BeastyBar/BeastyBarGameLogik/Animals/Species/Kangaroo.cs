namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Management.Automation;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Kangaroo : Animal
    {
        private int jumpWitdh;

        public Kangaroo() : base(3, false)
        {
            this.jumpWitdh = -1;
        }

        public int JumpWitdh
        {
            get
            {
                return this.jumpWitdh;
            }

            set
            {
                if (value != 2 & value != 1 & value != -1)
                {
                    throw new ArgumentOutOfRangeException(nameof(jumpWitdh), "The jump witdh must be 1 or 2.");
                }

                this.jumpWitdh = value;
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

            if (this.JumpWitdh == -1 & animals.Count == 0)
            {
                animals.Insert(0, this);
                return animals;
            }

            if (index >= animals.Count - 1 & index >= 0)
            {
                return animals;
            }

            int ind = this.JumpWitdh;

            while (animals.Count < ind)
            {
                --ind;
            }

            animals.Insert(ind, this);
            this.jumpWitdh = -1;

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
