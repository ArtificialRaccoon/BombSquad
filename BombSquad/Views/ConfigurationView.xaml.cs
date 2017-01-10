using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using BombSquad.ViewModels;

namespace BombSquad.Views
{
    /// <summary>
    /// Interaction logic for ConfigurationView.xaml
    /// </summary>
    public partial class ConfigurationView : Window
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ConfigurationView"/> class.
        /// </summary>
        public ConfigurationView()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the Spin event of the spnAttempts control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Xceed.Wpf.Toolkit.SpinEventArgs"/> instance containing the event data.</param>
        private void spnAttempts_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            ConfigurationViewModel objConfig = (ConfigurationViewModel)this.DataContext;
            if (e.Direction == Xceed.Wpf.Toolkit.SpinDirection.Increase)
                objConfig.MaxAttempts++;
            else
            {
                if(objConfig.MaxAttempts > 1)
                    objConfig.MaxAttempts--;
            }
        }

        /// <summary>
        /// Handles the Spin event of the spnMaxTime control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Xceed.Wpf.Toolkit.SpinEventArgs"/> instance containing the event data.</param>
        private void spnMaxTime_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            ConfigurationViewModel objConfig = (ConfigurationViewModel)this.DataContext;
            if (e.Direction == Xceed.Wpf.Toolkit.SpinDirection.Increase)
                objConfig.DefaultStartTime = objConfig.DefaultStartTime.Add(TimeSpan.FromSeconds(1));
            else
            {
                if(objConfig.DefaultStartTime.TotalSeconds > 10)
                    objConfig.DefaultStartTime = objConfig.DefaultStartTime.Subtract(TimeSpan.FromSeconds(1));
            }
        }

        /// <summary>
        /// Handles the Spin event of the spnCodeLength control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="Xceed.Wpf.Toolkit.SpinEventArgs"/> instance containing the event data.</param>
        private void spnCodeLength_Spin(object sender, Xceed.Wpf.Toolkit.SpinEventArgs e)
        {
            ConfigurationViewModel objConfig = (ConfigurationViewModel)this.DataContext;
            if (e.Direction == Xceed.Wpf.Toolkit.SpinDirection.Increase)
            {
                if (objConfig.CodeLength < 6)
                    objConfig.CodeLength = ++objConfig.CodeLength;
            }
            else
            {
                if (objConfig.CodeLength > 4)
                    objConfig.CodeLength = --objConfig.CodeLength;
            }
        }
    }
}
