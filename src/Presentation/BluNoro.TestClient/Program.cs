using BluChat.TestClient;
using System.Configuration;
using System.Diagnostics;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

namespace BluNoro.TestClient
{
    internal static class Program
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            // To customize application configuration such as set high DPI settings or default font,
            // see https://aka.ms/applicationconfiguration.
            ApplicationConfiguration.Initialize();

            List<Task> tasks = new List<Task>();

            NumberOfWindows count = new NumberOfWindows();
            count.ShowDialog();

            if(count.DialogResult != DialogResult.OK)
                Environment.Exit(0);

            int numberOfWindows = count.NumberOfWindowsResult;

            for (int i = 0; i < numberOfWindows; i++)
            {
                var window = new Main();
                window.Text = window.Text + " - " + i;


                var task = Task.Run(new Action(() =>
                        {
                            Application.Run(window);
                }));

                tasks.Add(task);
            }

            while(tasks.Any(x => x.IsCompleted == false))
            {
                Thread.Sleep(100);
            }


        }
    }
}