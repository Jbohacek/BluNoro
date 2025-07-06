using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BluNoro.Core.Contracts.Enums
{
    public class LogLevels
    {
        public enum Level
        {
            Informal,
            Success,
            Warning,
            ClientError,
            ServerError,
        }
        public static class LevelExtensions
        {
            public static ConsoleColor ToConsoleColor(Level level)
            {
                return level switch
                {
                    Level.Informal => ConsoleColor.Gray,
                    Level.Success => ConsoleColor.Green,
                    Level.Warning => ConsoleColor.Yellow,
                    Level.ClientError => ConsoleColor.Cyan,
                    Level.ServerError => ConsoleColor.Red,
                    _ => ConsoleColor.White
                };
            }
        }
    }
}
