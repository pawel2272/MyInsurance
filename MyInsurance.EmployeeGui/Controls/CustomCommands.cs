using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MyInsurance.EmployeeGui.Controls
{
    /// <summary>
    /// Klasa <c>CustomCommands</c> zawiera komendy.
    /// </summary>
    class CustomCommands
    {
        /// <summary>
        /// Komenda Exit.
        /// </summary>
        public static readonly RoutedUICommand Exit = new RoutedUICommand("Wyjdź z programu", "Exit", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.E, ModifierKeys.Control)
        });
        /// <summary>
        /// Komenda Logout.
        /// </summary>
        public static readonly RoutedUICommand Logout = new RoutedUICommand("Wyloguj się", "Logout", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.L, ModifierKeys.Control)
        });
        /// <summary>
        /// Komenda About.
        /// </summary>
        public static readonly RoutedUICommand About = new RoutedUICommand("O programie...", "About", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.O, ModifierKeys.Control)
        });
        /// <summary>
        /// Komenda ManageAcc.
        /// </summary>
        public static readonly RoutedUICommand ManageAcc = new RoutedUICommand("Zarządzaj kontem", "ManageAcc", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.A, ModifierKeys.Control)
        });
        /// <summary>
        /// Komenda ManageEmp.
        /// </summary>
        public static readonly RoutedUICommand ManageEmp = new RoutedUICommand("Zarządzaj pracownikami", "ManageEmp", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.M, ModifierKeys.Control)
        });
        /// <summary>
        /// Komenda New.
        /// </summary>
        public static readonly RoutedUICommand New = new RoutedUICommand("Nowy", "New", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.N, ModifierKeys.Control)
        });
        /// <summary>
        /// Komenda Edit.
        /// </summary>
        public static readonly RoutedUICommand Edit = new RoutedUICommand("Edytuj", "Edit", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.I, ModifierKeys.Control)
        });
        /// <summary>
        /// Komenda Delete.
        /// </summary>
        public static readonly RoutedUICommand Delete = new RoutedUICommand("Usuń", "Delete", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.D, ModifierKeys.Control)
        });
        /// <summary>
        /// Komenda Back.
        /// </summary>
        public static readonly RoutedUICommand Back = new RoutedUICommand("Przejdź wstecz", "Back", typeof(CustomCommands), new InputGestureCollection
        {
            new KeyGesture(Key.B, ModifierKeys.Control)
        });
    }
}
