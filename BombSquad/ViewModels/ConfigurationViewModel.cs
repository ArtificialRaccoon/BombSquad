using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel;

namespace BombSquad.ViewModels
{
    public class ConfigurationViewModel : INotifyPropertyChanged
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

        private int mMaxAttempts = 8;
        public int MaxAttempts
        {
            get { return mMaxAttempts; }
            set { mMaxAttempts = value; OnPropertyChanged("MaxAttempts"); }
        }

        private TimeSpan mDefaultStartTime = new TimeSpan(0, 1, 0);
        public TimeSpan DefaultStartTime
        {
            get { return mDefaultStartTime; }
            set { mDefaultStartTime = value; OnPropertyChanged("DefaultStartTime"); }
        }

        private int mCodeLength = 4;
        public int CodeLength
        {
            get { return mCodeLength; }
            set { mCodeLength = value; OnPropertyChanged("CodeLength"); }
        }
    }
}
