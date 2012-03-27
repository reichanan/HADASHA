using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace datagridview
{
    public class simplelogfile
    {

        static StreamWriter LogWriter;
        static string strLogText = "Default string to log";

       

        public static void LogToFile(string message)
        {
            try
            {
                string newmessage;
                
                if (!File.Exists("LogFile.txt")) { LogWriter = new StreamWriter("LogFile.txt"); }
                else { LogWriter = File.AppendText("LogFile.txt"); }

                if (string.IsNullOrEmpty(message))
                    newmessage = DateTime.Now.ToString() + ":  " + strLogText;
                else
                    newmessage = DateTime.Now.ToString() + ":  " + message;

                LogWriter.WriteLine(newmessage);
                LogWriter.Flush();
            }
            catch (Exception) { }
            finally { if (LogWriter != null) { LogWriter.Close(); } }
        }
   
    }
}
