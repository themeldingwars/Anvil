using System;
using System.Threading.Tasks;
using Anvil;
using ImTool;

namespace Demo
{
    class Program
    {
        static async Task Main()
        {
            try
            {
                await AnvilTool.Run();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }

        }
    }
}