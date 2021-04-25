namespace BeastyBarGameLogic.Animals
{
    using BeastyBarGameLogic.Animals.Species;

    public class IsKangarooVisitor : IAnimalVisitor<bool>
    {

        public bool Visit(Lion lion)
        {
            return false;
        }

        public bool Visit(Hippo hippo)
        {
            return false;
        }

        public bool Visit(Crocodile crocodile)
        {
            return false;
        }

        public bool Visit(Snake snake)
        {
            return false;
        }

        public bool Visit(Giraffe giraffe)
        {
            return false;
        }

        public bool Visit(Zebra zebra)
        {
            return false;
        }

        public bool Visit(Seal seal)
        {
            return false;
        }

        public bool Visit(Chameleon chameleon)
        {
            return false;
        }

        public bool Visit(Monkey monkey)
        {
            return false;
        }

        public bool Visit(Kangaroo kangaroo)
        {
            return true;
        }

        public bool Visit(Parrot parrot)
        {
            return false;
        }

        public bool Visit(Stunk stunk)
        {
            return false;
        }
    }
}
