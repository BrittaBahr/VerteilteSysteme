namespace BeastyBarGameLogic.Animals
{
    public interface IAnimalVisitable
    {
        T Accept<T>(IAnimalVisitor<T> visitor);
    }
}
