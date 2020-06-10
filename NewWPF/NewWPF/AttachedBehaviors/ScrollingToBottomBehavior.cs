using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;

namespace NewWPF.AttachedBehaviors
{
    /// <summary>
    /// Scroll end behavior
    /// Source <see cref="https://stackoverflow.com/a/21662718/6940144"/>
    /// </summary>
    public class ScrollingToBottomBehavior
    {
        #region Private Section

        private static ListView ListView = null;
        private static ICommand command = null;

        #endregion

        #region IsEnabledProperty

        public static readonly DependencyProperty IsEnabledProperty;

        public static void SetIsEnabled(DependencyObject DepObject, string value)
        {
            DepObject.SetValue(IsEnabledProperty, value);
        }

        public static bool GetIsEnabled(DependencyObject DepObject)
        {
            return (bool)DepObject.GetValue(IsEnabledProperty);
        }

        #endregion

        #region CommandProperty

        public static readonly DependencyProperty CommandProperty;

        public static void SetCommand(DependencyObject DepObject, ICommand value)
        {
            DepObject.SetValue(CommandProperty, value);
        }

        public static ICommand GetCommand(DependencyObject DepObject)
        {
            return (ICommand)DepObject.GetValue(CommandProperty);
        }

        static ScrollingToBottomBehavior()
        {
            IsEnabledProperty = DependencyProperty.RegisterAttached("IsEnabled",
            typeof(bool),
            typeof(ScrollingToBottomBehavior),
            new UIPropertyMetadata(false, IsFrontTurn));

            CommandProperty = DependencyProperty.RegisterAttached("Command",
            typeof(ICommand),
            typeof(ScrollingToBottomBehavior));
        }

        #endregion

        private static void IsFrontTurn(DependencyObject sender, DependencyPropertyChangedEventArgs e)
        {
            ListView = sender as ListView;

            if (ListView == null)
            {
                return;
            }

            if (e.NewValue is bool && ((bool)e.NewValue) == true)
            {
                ListView.Loaded += new RoutedEventHandler(ListViewLoaded);
            }
            else
            {
                ListView.Loaded -= new RoutedEventHandler(ListViewLoaded);
            }
        }

        private static void ListViewLoaded(object sender, RoutedEventArgs e)
        {
            var scrollViewer = GetFirstChildOfType<ScrollViewer>(ListView);

            if (scrollViewer != null)
            {
                scrollViewer.ScrollChanged += new ScrollChangedEventHandler(scrollViewerScrollChanged);
            }
        }

        #region GetFirstChildOfType

        private static T GetFirstChildOfType<T>(DependencyObject dependencyObject) where T : DependencyObject
        {
            if (dependencyObject == null)
            {
                return null;
            }

            for (var i = 0; i < VisualTreeHelper.GetChildrenCount(dependencyObject); i++)
            {
                var child = VisualTreeHelper.GetChild(dependencyObject, i);

                var result = (child as T) ?? GetFirstChildOfType<T>(child);

                if (result != null)
                {
                    return result;
                }
            }

            return null;
        }

        #endregion

        private static void scrollViewerScrollChanged(object sender, ScrollChangedEventArgs e)
        {
            var scrollViewer = sender as ScrollViewer;

            if (scrollViewer != null)
            {
                if (scrollViewer.VerticalOffset == scrollViewer.ScrollableHeight)
                {
                    command = GetCommand(ListView);

                    if (command != null)
                        command.Execute(ListView);
                }
            }
        }
    }
}
