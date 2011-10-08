using System;

namespace Pung
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (PungGame game = new PungGame())
            {
                game.Run();
            }
        }
    }
#endif
}

