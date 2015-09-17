using System;
using System.IO;
using KanColleLib;

namespace Miotsukushi.Model.Debugger
{
    class PacketSaver
    {
        KanColleNotifier _kclib;

        readonly string _filename = "log/" + DateTime.Now.ToString("yyyyMMdd-HHmmss") + ".dat";
        uint _packetCounter = 0;

        bool _enable = true;

        public PacketSaver(KanColleNotifier kclib)
        {

            this._kclib = kclib;
            kclib.GetKcsAPIData += Kclib_GetKcsAPIData;
            kclib.GetFiddlerLogString += Kclib_GetFiddlerLogString;
            kclib.KcsAPIDataAnalyzeFailed += Kclib_KcsAPIDataAnalyzeFailed;
            ((App)App.Current).UnhandledExceptionRaised += PacketSaver_UnhandledExceptionRaised;
        }

        private void PacketSaver_UnhandledExceptionRaised(object sender, UnhandledExceptionRaisedEventArgs e)
        {
            ++_packetCounter;

            if (!_enable)
                return;

            DirectoryConfirm();

            using (var sw = new StreamWriter(_filename, true))
            {
                sw.WriteLine("================== !!! Unhandled Exception !!! ==================");
                sw.WriteLine("No." + _packetCounter + "  " + DateTime.Now.ToString());
                sw.WriteLine("Type: " + e.ExceptionType);
                if (e.InnerException != null)
                {
                    sw.WriteLine("Exception: " + e.InnerException.Message);
                    sw.WriteLine("Source: " + e.InnerException.Source);
                    sw.WriteLine(e.InnerException.StackTrace);
                }
                if(e.ExceptionObject != null)
                    sw.WriteLine("Object: " + e.ExceptionObject.ToString() + " (Type: " + e.ExceptionObject.GetType().ToString() + ")");
            }
        }

        private void Kclib_KcsAPIDataAnalyzeFailed(object sender, KanColleLib.EventArgs.KcsAPIDataAnalyzeFailedEventArgs e)
        {
            ++_packetCounter;

            if (!_enable)
                return;

            DirectoryConfirm();

            using (var sw = new StreamWriter(_filename, true))
            {
                sw.WriteLine("================== !!! Error !!! ==================");
                sw.WriteLine("No." + _packetCounter + "  " + DateTime.Now.ToString());
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
            ++_packetCounter;

            if (!_enable)
                return;

            DirectoryConfirm();

            using (var sw = new StreamWriter(_filename, true))
            {
                sw.WriteLine("================== Fiddler Log ==================");
                sw.WriteLine("No." + _packetCounter + "  " + DateTime.Now.ToString());
                sw.WriteLine(e.logtext);
            }
        }

        private void Kclib_GetKcsAPIData(object sender, KanColleLib.EventArgs.GetKcsAPIDataEventArgs e)
        {
            ++_packetCounter;

            if (!_enable)
                return;

            DirectoryConfirm();

            using (var sw = new StreamWriter(_filename, true))
            {
                sw.WriteLine("================== KcsAPI Data ==================");
                sw.WriteLine("No." + _packetCounter + "  " + DateTime.Now.ToString());
                sw.WriteLine("URL: " + e.kcsapiurl);
                sw.WriteLine("[[Request]]");
                sw.WriteLine(e.request);
                sw.WriteLine("[[Response]]");
                sw.WriteLine(e.response);
            }
        }
    }
}
