﻿using RoomNumberTA.Interfaces;
using RoomNumberTA.ViewModels;
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

namespace RoomNumberTA.Views
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private MainViewModel viewModel;
        public MainWindow(List<IParameter> parameters, IElementsSelector elementsSelector, IElementSelector elementSelector)
        {
            InitializeComponent();
            DataContext = viewModel = new MainViewModel(parameters,elementsSelector,elementSelector);
        }
    }
}
