﻿using System;
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
using System.Diagnostics;
using System.Data;
using System.Windows.Automation;

namespace fast_open_work_dir
{
    /// <summary>
    /// MainWindow.xaml 的交互逻辑
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            InitButtons();

            AutomationElement aelement =  AutomationElement.RootElement
                      .FindFirst( TreeScope.Descendants, new PropertyCondition( AutomationElement.ClassNameProperty, "Shell_TrayWnd" ) )
                      .FindFirst( TreeScope.Descendants, new PropertyCondition( AutomationElement.ClassNameProperty, "ReBarWindow32" ) )
                      .FindFirst( TreeScope.Descendants, new PropertyCondition( AutomationElement.ClassNameProperty, "MSTaskSwWClass" ) )
                      .FindFirst( TreeScope.Descendants, new PropertyCondition( AutomationElement.ClassNameProperty, "MSTaskListWClass" ) )
                      .FindFirst( TreeScope.Descendants, new PropertyCondition( AutomationElement.NameProperty, "目录打开快捷工具" ) );
            if( aelement != null ) {
                System.Windows.Rect rect = (System.Windows.Rect) aelement.GetCurrentPropertyValue( AutomationElement.BoundingRectangleProperty );
                this.Left = rect.Left;
                this.Top = rect.Top - this.Height;
            }
        }

        private void InitButtons()
        {
            var dataTable = DataSource.GetPathList();
            dataTable.Rows.Cast<DataRow>().ToList().ForEach(item =>
            {
                var name = item["Name"].ToString();
                var path = item["Path_Text"].ToString();
                var button = new Button();
                var text = new TextBlock();
                text.Text = name;
                button.Content = text;
                button.Style = Resources["ButtonNormal"] as Style;
                button.Click += (s, e) =>
                {
                    System.Diagnostics.Process.Start(path);
                };
                ButtonList.Children.Add(button);
            });
        }

        public void ForceShow()
        {
            this.Show();
            this.WindowState = WindowState.Normal;
            this.Visibility = System.Windows.Visibility.Visible;
            this.Activate();
        }

    }
}
