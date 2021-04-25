using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace BeastyBarGameLogic.GamePlayer.AnimalSettingsProvider
{
    public class IntAnimalSettingsProvider : IAnimalSettingProvider<int>
    {
        private int intSetting;

        public IntAnimalSettingsProvider()
        {
            this.intSetting = -1;
        }

        public int IntSetting
        {
            get
            {
                return this.intSetting;
            }

            set
            {
                if (value < 0)
                {
                    throw new ArgumentOutOfRangeException(nameof(this.intSetting));
                }

                this.intSetting = value;
            }
        }

        public int getSetting()
        {
            while (this.IntSetting < 0)
            {
                Thread.Sleep(2000);
            }

            return this.IntSetting;
        }
    }
}
