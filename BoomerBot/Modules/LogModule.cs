using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BoomerBot.Controllers
{
    public static class LogModule
    {
        public static void Write(string message)
        {
            StreamWriter logFile = new StreamWriter("/log.txt", true);
            logFile.WriteLine(message);
            logFile.Close();
        }
    }
}