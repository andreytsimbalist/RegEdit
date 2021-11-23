using System;
using System.Collections.ObjectModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using Microsoft.Win32;
using RegEdit.initializer;

namespace RegEdit
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow
    {
        private RegItem selectedRegItem;
        
        
        public MainWindow()
        {
            InitializeComponent();
            paramType.ItemsSource =Enum.GetValues(typeof(RegistryValueKind));
            treeView.Items.Add(Initializer.Init());
        }

        private void TreeView_OnSelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {
            var reg = (RegItem) e.NewValue;
            selectedRegItem = reg;
            var collection =  new ObservableCollection<Parameter>();
            reg.Parameters.ForEach((param) => collection.Add(param));
            dataGrid.ItemsSource = collection;
            reg.Load();
        }

        private void ParamAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if(selectedRegItem == null) return;
        if (selectedRegItem == null) return;
        if (paramName.Text.Equals("")) return;
        if (paramValue.Text.Equals("")) return;
        try
        {
            selectedRegItem.Key.SetValue(paramName.Text, paramValue.Text, (RegistryValueKind) paramType.SelectedItem );
        }
        catch (Exception exception)
        {
            MessageBox.Show(exception.Message);
            return;
        }
        var parameter = new Parameter(paramName.Text,paramType.Text,paramValue.Text);
        var collection =(ObservableCollection<Parameter>) dataGrid.ItemsSource;
        selectedRegItem.Parameters.Add(parameter);
        collection.Add(parameter);
        }

        private void ParamDelete_OnClick(object sender, RoutedEventArgs e)
        {
            if(selectedRegItem == null) return;
            var item = (Parameter) dataGrid.SelectedItem ;
            if (item == null)
            {
                return;
            }
            var collection =(ObservableCollection<Parameter>) dataGrid.ItemsSource;
            collection.Remove(item);
            selectedRegItem.Parameters.Remove(item);
            try
            {
                selectedRegItem.Key.DeleteValue(item.Name);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void RegAdd_OnClick(object sender, RoutedEventArgs e)
        {
            if(selectedRegItem == null) return;
            if(regName.Text.Equals("")) return;
            try
            {
                var newRegItem = selectedRegItem.Key.CreateSubKey(regName.Text);
                var regItem = new RegItem(newRegItem);
                selectedRegItem.Items.Add(regItem);
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
            }
        }

        private void RegDelete_OnClick(object sender, RoutedEventArgs e)
        {
           if(selectedRegItem == null) return;
           var parent = GetParentRegItem(selectedRegItem);
           try
           {
               parent.Key.DeleteSubKeyTree(selectedRegItem.Header.ToString()); 
               parent.Items.Remove(selectedRegItem);
               selectedRegItem = null;
           }
           catch (Exception exception)
           {
               MessageBox.Show(exception.Message);
           }
        }
        
        private static RegItem GetParentRegItem(DependencyObject item)
        {
            var parent = VisualTreeHelper.GetParent(item);
            while (!(parent is TreeViewItem || parent is TreeView))
            {
                parent = VisualTreeHelper.GetParent(parent);
            }

            return parent as RegItem;
        }
    }
}