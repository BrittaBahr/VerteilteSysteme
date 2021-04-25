using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using BeastyBarGameLogic.Animals;

namespace BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider
{
    public class AnimalAnimalSettingsProvider : IAnimalSettingProvider<Animal>
    {
        private Animal animalSetting;

        public AnimalAnimalSettingsProvider()
        {
            this.animalSetting = null;
            this.IsFirstAnimal = false;
        }

        public Animal AnimalSetting
        {
            get
            {
                return this.animalSetting;
            }

            set
            {
                this.animalSetting = value;
            }
        }

        public bool IsFirstAnimal
        {
            get;
            set;
        }

        public Animal getSetting()
        {
            while (this.AnimalSetting == null & !this.IsFirstAnimal)
            {
                Thread.Sleep(2000);
            }

            return this.AnimalSetting;
        }
    }
}
