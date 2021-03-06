namespace Client.ViewModel
{
    using System;
    using BeastyBarGameLogic.GamePlayer;
    using Client.ClassesForView;
    using Client.Models;

    public class BeatyBarPlayerVM
    {
        private readonly BeastyBarPlayer model;

        private readonly AlphaRedGreenBlue colour;

        public BeatyBarPlayerVM(BeastyBarPlayer model, AlphaRedGreenBlue colour)
        {
            this.model = model ?? throw new ArgumentNullException(nameof(model));
            this.colour = colour;
        }

        public string Nickname
        {
            get
            {
                return model.Name;
            }
        }

        public AlphaRedGreenBlue Colour
        {
            get
            {
                return this.colour;
            }
        }
    }
}
