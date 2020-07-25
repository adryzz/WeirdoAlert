using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

    public class Logger : IDisposable
    {
        public string LogName;
        StreamWriter Stream;
        public Logger(string logname)
        {
            LogName = logname;
            Stream = File.AppendText(LogName);
            Stream.AutoFlush = true;
        }

        public Logger()
        {
            DateTime now = DateTime.Now;
            LogName = String.Format("{0}-{1}-{2}--{3}-{4}-{5}.log", now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second);
            Stream = File.AppendText(LogName);
            Stream.AutoFlush = true;
        }

        public void Log(string data)
        {
            DateTime now = DateTime.Now;
            data = String.Format("{0}/{1}/{2} {3}:{4}:{5}:{6} - {7}", now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond, data);
            Stream.WriteLine(data);
        }

        public async Task LogAsync(string data)
        {
            DateTime now = DateTime.Now;
            data = String.Format("{0}/{1}/{2} {3}:{4}:{5}:{6} - {7}", now.Day, now.Month, now.Year, now.Hour, now.Minute, now.Second, now.Millisecond, data);
            await Stream.WriteLineAsync(data);
        }

        public void Dispose()
        {
            Stream.Flush();
            Stream.Close();
            Stream.Dispose();
        }
    }
