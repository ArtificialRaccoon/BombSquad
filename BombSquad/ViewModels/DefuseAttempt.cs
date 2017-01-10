using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.ComponentModel;

namespace BombSquad.ViewModels
{
    public class DefuseAttempt : INotifyPropertyChanged
    {
        #region  OnPropertyChanged
        public event PropertyChangedEventHandler PropertyChanged;
        /// <summary>
        /// Called when [property changed].
        /// </summary>
        /// <param name="name">The name.</param>
        protected void OnPropertyChanged(string name)
        {
            PropertyChangedEventHandler handler = PropertyChanged;
            if (handler != null) { handler(this, new PropertyChangedEventArgs(name)); }
        }
        #endregion

        private int mInputLength = 4;

        public DefuseAttempt(int InputLength) { mInputLength = InputLength; }

        public List<BombSquad.Enumerations.InputEnum> CurrentInput
        {
            get
            {
                switch(mInputLength)
                {
                    case 5:
                        return new List<Enumerations.InputEnum>() { InputOne, InputTwo, InputThree, InputFour, InputFive }; 
                    case 6:
                        return new List<Enumerations.InputEnum>() { InputOne, InputTwo, InputThree, InputFour, InputFive, InputSix }; 
                    default:
                        return new List<Enumerations.InputEnum>() { InputOne, InputTwo, InputThree, InputFour }; 
                }                
            }
        }

        private BombSquad.Enumerations.InputEnum mInputOne = BombSquad.Enumerations.InputEnum.Unset;
        public BombSquad.Enumerations.InputEnum InputOne
        {
            get { return mInputOne; }
            set { mInputOne = value; OnPropertyChanged("InputOne"); OnPropertyChanged("CurrentInput"); }
        }

        private BombSquad.Enumerations.InputEnum mInputTwo = BombSquad.Enumerations.InputEnum.Unset;
        public BombSquad.Enumerations.InputEnum InputTwo
        {
            get { return mInputTwo; }
            set { mInputTwo = value; OnPropertyChanged("InputTwo"); OnPropertyChanged("CurrentInput"); }
        }

        private BombSquad.Enumerations.InputEnum mInputThree = BombSquad.Enumerations.InputEnum.Unset;
        public BombSquad.Enumerations.InputEnum InputThree
        {
            get { return mInputThree; }
            set { mInputThree = value; OnPropertyChanged("InputThree"); OnPropertyChanged("CurrentInput"); }
        }

        private BombSquad.Enumerations.InputEnum mInputFour = BombSquad.Enumerations.InputEnum.Unset;
        public BombSquad.Enumerations.InputEnum InputFour
        {
            get { return mInputFour; }
            set { mInputFour = value; OnPropertyChanged("InputFour"); OnPropertyChanged("CurrentInput"); }
        }

        private BombSquad.Enumerations.InputEnum mInputFive = BombSquad.Enumerations.InputEnum.Unset;
        public BombSquad.Enumerations.InputEnum InputFive
        {
            get { return mInputFive; }
            set { mInputFive = value; OnPropertyChanged("InputFive"); OnPropertyChanged("CurrentInput"); }
        }

        private BombSquad.Enumerations.InputEnum mInputSix = BombSquad.Enumerations.InputEnum.Unset;
        public BombSquad.Enumerations.InputEnum InputSix
        {
            get { return mInputSix; }
            set { mInputSix = value; OnPropertyChanged("InputSix"); OnPropertyChanged("CurrentInput"); }
        } 
      
        public void AddInput(BombSquad.Enumerations.InputEnum inputValue)
        {
            if (InputOne == Enumerations.InputEnum.Unset) { InputOne = inputValue; return; }
            if (InputTwo == Enumerations.InputEnum.Unset) { InputTwo = inputValue; return; }
            if (InputThree == Enumerations.InputEnum.Unset) { InputThree = inputValue; return; }
            if (InputFour == Enumerations.InputEnum.Unset) { InputFour = inputValue; return; }
            if (mInputLength >= 5 && InputFive == Enumerations.InputEnum.Unset) { InputFive = inputValue; return; }
            if (mInputLength == 6 && InputSix == Enumerations.InputEnum.Unset) { InputSix = inputValue; return; }
        }

        public bool Completed
        {
            get { return !(CurrentInput.Contains(Enumerations.InputEnum.Unset)); }
        }
    }
}
