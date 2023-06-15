using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using System.Threading;

namespace Password_Keeper
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
            SplashScreen splash = new SplashScreen(4);
            splash.ShowSplashScreen(4);
            while (!splash.Ready())
            {
             
            }
            splash.UpdateProgress("Loading Step 1");
            Thread.Sleep(2000); 
            splash.UpdateProgress("Loading Step 2");
            Thread.Sleep(2000); 
            splash.UpdateProgress("Loading Step 3");
            Thread.Sleep(2000); 
            splash.UpdateProgress("Loading Step 4");
            Thread.Sleep(2000); 
            splash.CloseForm();
            Application.Run(new loginform());
        }
    }
}
