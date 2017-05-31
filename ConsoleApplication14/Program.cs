using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApplication14
{
    internal class Program
    {
        private static readonly RequestFile RequestNewJobs = new RequestFile();
        private static readonly DBConnect Connect = new DBConnect();

        public static void Main(string[] args)
        {
            RequestNewJobs.RequestFromWeb();

            Connect.Insert();
        }
    }
}