using System;
using System.Collections.Generic;
using System.Text;
using BeastyBarGameLogic.Animals;
using BeastyBarGameLogic.Animals.Species;
using BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs;

namespace BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider
{
    public class AnimalSettingsManagerVisitor : IAnimalVisitor<Animal>
    {
        private readonly IntAnimalSettingsProvider intAnimalSettingsProvider;

        private readonly AnimalAnimalSettingsProvider animalAnimalSettingsProvider;

        public AnimalSettingsManagerVisitor()
        {
            this.intAnimalSettingsProvider = new IntAnimalSettingsProvider();
            this.animalAnimalSettingsProvider = new AnimalAnimalSettingsProvider();
        }

        public event EventHandler<AnimalAnimalSettingsAreNeededEventArgs> AnimalAnimalSettingsAreNeeded;

        public event EventHandler<IntAnimalSettingsAreNeededEventArgs> IntAnimalSettingsAreNeeded;

        public Animal Visit(Lion lion)
        {
            return lion;
        }

        public Animal Visit(Hippo hippo)
        {
            return hippo;
        }

        public Animal Visit(Crocodile crocodile)
        {
            return crocodile;
        }

        public Animal Visit(Snake snake)
        {
            return snake;
        }

        public Animal Visit(Giraffe giraffe)
        {
            return giraffe;
        }

        public Animal Visit(Zebra zebra)
        {
            return zebra;
        }

        public Animal Visit(Seal seal)
        {
            return seal;
        }

        public Animal Visit(Chameleon chameleon)
        {
            this.OnAnimalAnimalSettingsAreNeeded(this.animalAnimalSettingsProvider);
            chameleon.TransformationForm = this.animalAnimalSettingsProvider.getSetting();
            return chameleon;
        }

        public Animal Visit(Monkey monkey)
        {
            return monkey;
        }

        public Animal Visit(Kangaroo kangaroo)
        {
            this.OnIntAnimalSettingsAreNeeded(this.intAnimalSettingsProvider);
            kangaroo.JumpWitdh = this.intAnimalSettingsProvider.getSetting();
            return kangaroo;
        }

        public Animal Visit(Parrot parrot)
        {
            this.OnAnimalAnimalSettingsAreNeeded(this.animalAnimalSettingsProvider);
            parrot.HatedAnimal = this.animalAnimalSettingsProvider.getSetting();
            return parrot;
        }

        public Animal Visit(Stunk stunk)
        {
            return stunk;
        }

        protected virtual void OnAnimalAnimalSettingsAreNeeded(AnimalAnimalSettingsProvider settings)
        {
            this.AnimalAnimalSettingsAreNeeded?.Invoke(this, new AnimalAnimalSettingsAreNeededEventArgs(settings));
        }

        protected virtual void OnIntAnimalSettingsAreNeeded(IntAnimalSettingsProvider settings)
        {
            this.IntAnimalSettingsAreNeeded?.Invoke(this, new IntAnimalSettingsAreNeededEventArgs(settings));
        }
    }
}
