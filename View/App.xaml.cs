﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using zLib;
using zWPFStartupProject.ViewModel;

namespace zWPFStartupProject.View {
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    /// 
    public partial class App : Application {
        public MainViewModel MainViewModel;
        private bool mainViewModelReady;
        public bool MainViewModelReady { get { return mainViewModelReady; } set { mainViewModelReady = value; } }

        protected override void OnStartup(StartupEventArgs e) {

            System.Threading.Thread.CurrentThread.CurrentUICulture =
            new System.Globalization.CultureInfo("lv");

            MainWindow =  new MainWindow();
            MainWindow.Resources = Resources;
            MainWindow.Show();

            Thread w = new Thread(() => {
                MainViewModel = new MainViewModel();
                MainViewModelReady = true;
            });
            w.Start();

            bool exit = false;
            while (!exit) {
                Thread.Sleep(5);
                if (mainViewModelReady) {
                    MainWindow.DataContext = MainViewModel;
                    MainViewModelReady = true;
                    exit = true;
                }
            }

        }

        protected override void OnExit(ExitEventArgs e) {
            //Save things
            base.OnExit(e);
        }
    }
}
