using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace SRtoolbox
{
    class Program
    {
        public static string[] arguments;

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// 

        [STAThread]
        static void Main(string[] cmdargs)
        {
            arguments = cmdargs;

            EmbeddedAssembly.Load("SRtoolbox.lib.Ionic.Zip.dll", "Ionic.Zip.dll");
            EmbeddedAssembly.Load("SRtoolbox.lib.Pfim.dll", "Pfim.dll");

            AppDomain.CurrentDomain.AssemblyResolve += new ResolveEventHandler(CurrentDomain_AssemblyResolve);

            Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
            {
                return EmbeddedAssembly.Get(args.Name);
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            Application.Run(new MainForm());
        }
    }
}
