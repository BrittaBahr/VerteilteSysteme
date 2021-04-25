namespace BeastyBarGameLogic.Animals.Species
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics.CodeAnalysis;
    using System.Linq;
    using System.Management.Automation;
    using BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider;
    using BeastyBarGameLogic.GamePlayer.GamePlayerEventArgs;
    using BeastyBarGameLogic.GamePlayer.NetworkCommunication;

    public class Chameleon : Animal
    {
        private readonly AnimalSettingsManagerVisitor animalSettingManager;

        private readonly IsKangarooVisitor isKangarooVisitor;

        // can be null if the Chamelion doesn't want to transform yet
        private Animal transformationForm;

        public Chameleon() : base(5, false)
        {
            this.isKangarooVisitor = new IsKangarooVisitor();
            this.animalSettingManager = new AnimalSettingsManagerVisitor();
            this.animalSettingManager.AnimalAnimalSettingsAreNeeded += AnimalSettingManagerFiredAnimalAnimalSettingsAreNeeded;
            this.animalSettingManager.IntAnimalSettingsAreNeeded += AnimalSettingManagerFiredIntAnimalSettingsAreNeeded;
        }

        public event EventHandler<AnimalAnimalSettingsAreNeededEventArgs> AnimalAnimalSettingsAreNeeded;

        public event EventHandler<IntAnimalSettingsAreNeededEventArgs> IntAnimalSettingsAreNeeded;

        public Animal TransformationForm
        {
            get
            {
                return this.transformationForm;
            }

            set
            {
                this.transformationForm = value;
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

            if (this == this.transformationForm)
            {
                this.transformationForm = null;
                animals.Insert(index, this);
                return animals;
            }

            if (this.transformationForm == null & index == -1 & animals.Count == 0)
            {
                animals.Insert(0, this);
                return animals;
            }

            if (index >= animals.Count - 1 & index >= 0)
            {
                return animals;
            }

            if (isPushing | (index >= 0))
            {
                return animals;
            }

            bool isKangaroo = this.transformationForm.Accept(this.isKangarooVisitor);

            this.transformationForm.Accept(this.animalSettingManager);
            animals = this.TransformationForm.Queue(animals, index, isPushing);
            animals = this.TransformationForm.Queue(animals, index + 1, true);
            int newIndex = animals.IndexOf(this.TransformationForm);

            if (isKangaroo)
            {
                newIndex = animals.LastIndexOf(this.TransformationForm);
            }

            animals.RemoveAt(newIndex);
            animals.Insert(newIndex, this);
            this.transformationForm = null;

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

        private void AnimalSettingManagerFiredIntAnimalSettingsAreNeeded(object sender, GamePlayer.GamePlayerEventArgs.IntAnimalSettingsAreNeededEventArgs e)
        {
            this.OnIntAnimalSettingsAreNeeded(e.Settings);
        }

        private void AnimalSettingManagerFiredAnimalAnimalSettingsAreNeeded(object sender, GamePlayer.GamePlayerEventArgs.AnimalAnimalSettingsAreNeededEventArgs e)
        {
            this.OnAnimalAnimalSettingsAreNeeded(e.Settings);
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
