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

namespace ArmBazaProject.UserControlWindows
{
    /// <summary>
    /// Логика взаимодействия для ResultTemplate.xaml
    /// </summary>
    public partial class ResultTemplate : UserControl
    {
        public ResultTemplate()
        {
            InitializeComponent();

            

        }

        private void svT_PreviewMouseWheel(object sender, MouseWheelEventArgs e)
        {
            ScrollViewer scrollviewer = sender as ScrollViewer;
            if (e.Delta > 0)
            {
                scrollviewer.LineUp();
            }
            else
            {
                scrollviewer.LineDown();
            }

        }
    }
}
