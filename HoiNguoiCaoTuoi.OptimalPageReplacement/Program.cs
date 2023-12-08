using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoiNguoiCaoTuoi.OptimalPageReplacement
{
    class Program
    {
        static void Main(string[] args)
        {
            Memory oPT = new Memory();
            oPT.Input();
            oPT.TransToPageList();
            oPT.DoOPT();
            oPT.PrintResult();
            Console.ReadLine();
        }
    }
}
