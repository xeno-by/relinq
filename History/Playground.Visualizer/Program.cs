//Copyright (C) Microsoft Corporation.  All rights reserved.

using System;
using System.Windows.Forms;
using Playground.Visualizer;

namespace Playground.Visualizer
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }
}