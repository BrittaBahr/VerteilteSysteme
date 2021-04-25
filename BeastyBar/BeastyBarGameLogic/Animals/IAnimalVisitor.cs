namespace BeastyBarGameLogic.Animals
{
    using BeastyBarGameLogic.Animals.Species;

    public interface IAnimalVisitor<T>
    {
        T Visit(Lion lion);

        T Visit(Hippo hippo);

        T Visit(Crocodile crocodile);

        T Visit(Snake snake);

        T Visit(Giraffe giraffe);

        T Visit(Zebra zebra);

        T Visit(Seal seal);

        T Visit(Chameleon chameleon);

        T Visit(Monkey monkey);

        T Visit(Kangaroo kangaroo);

        T Visit(Parrot parrot);

        T Visit(Stunk stunk);
    }
}
