using System;
using System.IO;
using KanColleLib;

namespace Miotsukushi.Model.Debugger
{
    class PacketSaver
    {
        KanColleNotifier kclib;

        string filename;
        uint packet_counter = 0;

        bool enable = true;

        public PacketSaver(KanColleNotifier kclib)
        {
            filename = "log/" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".dat";

            this.kclib = kclib;
            kclib.GetKcsAPIData += Kclib_GetKcsAPIData;
            kclib.GetFiddlerLogString += Kclib_GetFiddlerLogString;
            kclib.KcsAPIDataAnalyzeFailed += Kclib_KcsAPIDataAnalyzeFailed;
            ((App)App.Current).UnhandledExceptionRaised += PacketSaver_UnhandledExceptionRaised;
        }

        private void PacketSaver_UnhandledExceptionRaised(object sender, UnhandledExceptionRaisedEventArgs e)
        {
            ++packet_counter;

            if (!enable)
                return;

            DirectoryConfirm();

            using (var sw = new StreamWriter(filename, true))
            {
                sw.WriteLine("================== !!! Unhandled Exception !!! ==================");
                sw.WriteLine("No." + packet_counter + "  " + DateTime.Now.ToString());
                sw.WriteLine("Type: " + e.exceptionType);
                if (e.innerException != null)
                {
                    sw.WriteLine("Exception: " + e.innerException.Message);
                    sw.WriteLine("Source: " + e.innerException.Source);
                    sw.WriteLine(e.innerException.StackTrace);
                }
                if(e.exceptionObject != null)
                    sw.WriteLine("Object: " + e.exceptionObject.ToString() + " (Type: " + e.exceptionObject.GetType().ToString() + ")");
            }
        }

        private void Kclib_KcsAPIDataAnalyzeFailed(object sender, KanColleLib.EventArgs.KcsAPIDataAnalyzeFailedEventArgs e)
        {
            ++packet_counter;

            if (!enable)
                return;

            DirectoryConfirm();

            using (var sw = new StreamWriter(filename, true))
            {
                sw.WriteLine("================== !!! Error !!! ==================");
                sw.WriteLine("No." + packet_counter + "  " + DateTime.Now.ToString());
                sw.WriteLine("URL: " + e.kcsapiurl);
                sw.WriteLine("Exception: " + e.originalexception.Message);
                sw.WriteLine("Source: " + e.originalexception.Source);
                sw.WriteLine(e.originalexception.StackTrace);
            }
        }

        private void DirectoryConfirm()
        {
            var d = new DirectoryInfo("log");
            if (!d.Exists)
                d.Create();
            
        }

        private void Kclib_GetFiddlerLogString(object sender, KanColleLib.EventArgs.GetFiddlerLogStringEventArgs e)
        {
            ++packet_counter;

            if (!enable)
                return;

            DirectoryConfirm();

            using (var sw = new StreamWriter(filename, true))
            {
                sw.WriteLine("================== Fiddler Log ==================");
                sw.WriteLine("No." + packet_counter + "  " + DateTime.Now.ToString());
                sw.WriteLine(e.logtext);
            }
        }

        private void Kclib_GetKcsAPIData(object sender, KanColleLib.EventArgs.GetKcsAPIDataEventArgs e)
        {
            ++packet_counter;

            if (!enable)
                return;

            DirectoryConfirm();

            using (var sw = new StreamWriter(filename, true))
            {
                sw.WriteLine("================== KcsAPI Data ==================");
                sw.WriteLine("No." + packet_counter + "  " + DateTime.Now.ToString());
                sw.WriteLine("URL: " + e.kcsapiurl);
                sw.WriteLine("[[Request]]");
                sw.WriteLine(e.request);
                sw.WriteLine("[[Response]]");
                sw.WriteLine(e.response);
            }
        }
    }
}
