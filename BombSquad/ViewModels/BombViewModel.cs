using System;
using System.Collections.ObjectModel;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Threading;
using System.Threading.Tasks;
using System.ComponentModel;
using System.Windows.Media;
using BombSquad.Views;

namespace BombSquad.ViewModels
{
    /// <summary>
    /// View Model for the Bomb Instance.  Each Bomb is considered a separate game.
    /// </summary>
    /// <seealso cref="System.ComponentModel.INotifyPropertyChanged" />
    public class BombViewModel : INotifyPropertyChanged
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

        #region Private Variables
        private DispatcherTimer mCountdownTimer = null;
        private Random mRandom = new Random(0);
        #endregion

        #region Properties
        private int mCurrentGameMaxAttempts = -1;
        /// <summary>
        /// Gets the current games maximum attempts.
        /// </summary>
        /// <value>
        /// The current games maximum attempts.
        /// </value>
        public int CurrentGameMaxAttempts
        {
            get { return mCurrentGameMaxAttempts; }
        }

        private List<BombSquad.Enumerations.InputEnum> mInputSolution = new List<Enumerations.InputEnum>(4);
        /// <summary>
        /// Gets the input solution.
        /// </summary>
        /// <value>
        /// The input solution.
        /// </value>
        public List<BombSquad.Enumerations.InputEnum> InputSolution
        {
            get { return mInputSolution; }
        }

        private ObservableCollection<DefuseAttempt> mDefuseAttempts = new ObservableCollection<DefuseAttempt>();
        /// <summary>
        /// Gets or sets the defuse attempts.
        /// </summary>
        /// <value>
        /// The defuse attempts.
        /// </value>
        public ObservableCollection<DefuseAttempt> DefuseAttempts
        {
            get { return mDefuseAttempts; }
            set { mDefuseAttempts = value; OnPropertyChanged("DefuseAttempts"); }
        }

        /// <summary>
        /// Gets a value indicating whether this <see cref="BombViewModel"/> is solved.
        /// </summary>
        /// <value>
        ///   <c>true</c> if solved; otherwise, <c>false</c>.
        /// </value>
        public bool Solved
        {
            get
            {
                bool isSolved = true;
                for(int i = 0; i < InputSolution.Count; i++)
                {
                    if (DefuseAttempts.Last().CurrentInput[i] != InputSolution[i])
                        isSolved = false;
                }

                if (isSolved)
                    mCountdownTimer.Stop();

                return isSolved;
            }
        }

        /// <summary>
        /// Gets a value indicating whether [game over].
        /// </summary>
        /// <value>
        ///   <c>true</c> if [game over]; otherwise, <c>false</c>.
        /// </value>
        public bool GameOver
        {
            get
            {
                bool isGameOver = false;
                if(!Solved)
                {
                    if (DefuseAttempts.Count > mCurrentGameMaxAttempts)
                        isGameOver = true;
                    if (TimeRemaining.TotalSeconds <= 0)
                        isGameOver = true;
                }
                return isGameOver;
            }
        }

        private TimeSpan mTimeRemaining;
        /// <summary>
        /// Gets or sets the time remaining.
        /// </summary>
        /// <value>
        /// The time remaining.
        /// </value>
        public TimeSpan TimeRemaining
        {
            get { return mTimeRemaining; }
            set { mTimeRemaining = value; OnPropertyChanged("TimeRemaining"); OnPropertyChanged("GameOver"); OnPropertyChanged("GameStatus"); }
        }

        public string GameStatus
        {
            get
            {
                string statusMessage = string.Empty;
                if (!GameOver)
                {
                    if (Solved) { statusMessage = "BOMB DEFUSED"; }
                    else
                    {
                        statusMessage = string.Format("{0} out of {1} attempts remaining", mCurrentGameMaxAttempts, mCurrentGameMaxAttempts);
                    }
                }
                else { statusMessage = "GAME OVER"; }
                return statusMessage;
            }
        }
        #endregion

        #region Methods
        /// <summary>
        /// Initializes a new instance of the <see cref="BombViewModel"/> class.  Automatically starts a new game.
        /// </summary>
        public BombViewModel() { NewGame(); }

        /// <summary>
        /// Starts a new Bomb Defusal Game
        /// </summary>
        public void NewGame()
        {
            mInputSolution.Clear();
            DefuseAttempts.Clear();
            mTimeRemaining = StaticClasses.Configuration.DefaultStartTime;
            mCurrentGameMaxAttempts = StaticClasses.Configuration.MaxAttempts;

            if (mCountdownTimer != null && mCountdownTimer.IsEnabled) mCountdownTimer.Stop();

            Array enumValues = Enum.GetValues(typeof(BombSquad.Enumerations.InputEnum));
            mInputSolution.Add((BombSquad.Enumerations.InputEnum)enumValues.GetValue(mRandom.Next(1, enumValues.Length)));
            mInputSolution.Add((BombSquad.Enumerations.InputEnum)enumValues.GetValue(mRandom.Next(1, enumValues.Length)));
            mInputSolution.Add((BombSquad.Enumerations.InputEnum)enumValues.GetValue(mRandom.Next(1, enumValues.Length)));
            mInputSolution.Add((BombSquad.Enumerations.InputEnum)enumValues.GetValue(mRandom.Next(1, enumValues.Length)));
            if (StaticClasses.Configuration.CodeLength >= 5)
                mInputSolution.Add((BombSquad.Enumerations.InputEnum)enumValues.GetValue(mRandom.Next(1, enumValues.Length)));
            if (StaticClasses.Configuration.CodeLength == 6)
                mInputSolution.Add((BombSquad.Enumerations.InputEnum)enumValues.GetValue(mRandom.Next(1, enumValues.Length)));

            DefuseAttempts.Add(new DefuseAttempt(StaticClasses.Configuration.CodeLength));
            mCountdownTimer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, delegate
            {
                if (TimeRemaining == TimeSpan.Zero)
                    mCountdownTimer.Stop();
                TimeRemaining = TimeRemaining.Add(TimeSpan.FromSeconds(-1));
            }, System.Windows.Application.Current.Dispatcher);
            mCountdownTimer.Start();

            OnPropertyChanged("DefuseAttempts");
            OnPropertyChanged("TimeRemaining");
            OnPropertyChanged("GameStatus");
        }

        /// <summary>
        /// Adds an input enumation into the current defuse attempt.
        /// </summary>
        /// <param name="inputValue">The input value.</param>
        public void AddInput(BombSquad.Enumerations.InputEnum inputValue)
        {
            DefuseAttempts.Last().AddInput(inputValue);
            OnPropertyChanged("GameOver");
            OnPropertyChanged("GameStatus");
        }
        #endregion
    }
}
