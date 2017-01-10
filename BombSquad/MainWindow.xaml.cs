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
using System.Windows.Navigation;
using System.Windows.Shapes;
using BombSquad.Views;

namespace BombSquad
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private static BombSquad.ViewModels.BombViewModel vmCurrentBomb;

        /// <summary>
        /// Initializes a new instance of the <see cref="MainWindow"/> class.
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
            vmCurrentBomb = new ViewModels.BombViewModel();
            this.DataContext = vmCurrentBomb;
            
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(PressBombButtonCommand, PressBombButtonCommand_Executed, PressBombButtonCommand_CanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(NewGameCommand, NewGameCommand_Executed, NewGameCommand_CanExecute));
            CommandManager.RegisterClassCommandBinding(typeof(Window), new CommandBinding(ConfigurationCommand, ConfigurationCommand_Executed, ConfigurationCommand_CanExecute));
        }

        #region Commands
        private static RoutedUICommand pressBombButtoneCommand = new RoutedUICommand("PressBombButton", "PressBombButton", typeof(MainWindow));
        /// <summary>
        /// Gets the press bomb button command.
        /// </summary>
        /// <value>
        /// The press bomb button command.
        /// </value>
        public static RoutedUICommand PressBombButtonCommand
        {
            get { return pressBombButtoneCommand; }
        }

        private static RoutedUICommand newGameCommand = new RoutedUICommand("PressBombButton", "PressBombButton", typeof(MainWindow));
        /// <summary>
        /// Gets the new game command.
        /// </summary>
        /// <value>
        /// The new game command.
        /// </value>
        public static RoutedUICommand NewGameCommand
        {
            get { return newGameCommand; }
        }

        private static RoutedUICommand configurationCommand = new RoutedUICommand("Configuration", "Configuration", typeof(MainWindow));
        /// <summary>
        /// Gets the configuration command.
        /// </summary>
        /// <value>
        /// The configuration command.
        /// </value>
        public static RoutedUICommand ConfigurationCommand
        {
            get { return configurationCommand; }
        }

        #region Press Bomb Button Implimentation
        internal static void PressBombButtonCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            vmCurrentBomb.AddInput((Enumerations.InputEnum)e.Parameter);
            if(!vmCurrentBomb.GameOver)
                if (vmCurrentBomb.DefuseAttempts.Last().Completed && !vmCurrentBomb.Solved)
                    if (vmCurrentBomb.DefuseAttempts.Count < vmCurrentBomb.CurrentGameMaxAttempts)
                        vmCurrentBomb.DefuseAttempts.Add(new ViewModels.DefuseAttempt(vmCurrentBomb.InputSolution.Count));      
        }

        internal static void PressBombButtonCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            if (vmCurrentBomb.TimeRemaining.TotalSeconds >= 0 && vmCurrentBomb.DefuseAttempts.Count <= vmCurrentBomb.CurrentGameMaxAttempts)
                e.CanExecute = true;
        }
        #endregion

        #region New Game Implimentation
        internal static void NewGameCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            vmCurrentBomb.NewGame();
        }

        internal static void NewGameCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion

        #region Configuration Command Implimentation
        internal static void ConfigurationCommand_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ConfigurationView frmConfig = new ConfigurationView();
            if (e.OriginalSource is System.Windows.Controls.Control)
            {
                System.Windows.Controls.Control originalSource = (System.Windows.Controls.Control)e.OriginalSource;
                Window parentWindow = Window.GetWindow(originalSource);
                if (parentWindow != null)
                    frmConfig.Owner = parentWindow;
            }
            frmConfig.DataContext = new ViewModels.ConfigurationViewModel();
            frmConfig.ShowDialog();
        }

        internal static void ConfigurationCommand_CanExecute(object sender, CanExecuteRoutedEventArgs e)
        {
            e.CanExecute = true;
        }
        #endregion
        #endregion
    }
}
