using System;
using System.Diagnostics;

namespace gha.mobile.common.helpers
{
    public static class Logger
    {
        public static void Log(string log)
        {
#if DEBUG
            Debug.WriteLine($"[GHALogger] {DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss")} {log}");
#endif
        }
    }
}
