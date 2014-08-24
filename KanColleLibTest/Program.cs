using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using KanColleLib;

namespace KanColleLibTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string api_start2 = System.IO.File.ReadAllText("api_start2.txt");
            var start2 = KanColleLib.TransmissionData.Start2.fromDynamic(Codeplex.Data.DynamicJson.Parse(api_start2).api_data);

        }
    }
}
