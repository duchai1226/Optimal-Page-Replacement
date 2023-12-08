using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HoiNguoiCaoTuoi.OptimalPageReplacement
{
    class Memory
    {
        string inputString;
        int frameCount;
        List<string> pageList = new List<string>();
        List<string> physicalMemory=new List<string>();
        List<List<string>> resultList = new List<List<string>>();
        public string InputString
        {
            get
            {
                return inputString;
            }
            set
            {
                if(value.All(char.IsDigit))
                    inputString = value;
            }
        }
        public void Input()
        {
            Console.Write("Nhap so Frame: ");
            int.TryParse(Console.ReadLine(), out frameCount);
            Console.Write("Nhap chuoi dau vao: ");
            inputString=Console.ReadLine();
        }
        public void TransToPageList()
        {
            foreach(char t in inputString)
            {
                pageList.Add(t.ToString());
            }
        }
        public void DoFiFo(int indexStart,List<string> listIn)
        {
            int distance = 0;
            while(listIn.Count()>1)
            {
                for (int i = indexStart; i >= 0; i--)
                {
                    foreach (string t in listIn)
                    {
                        if (t == pageList[i])
                        {
                            distance = i;
                            goto remove;
                        }
                    }
                }
                remove:
                    listIn.Remove(pageList[distance]);
            }
        }
        public int CheckFrame(int indexStart)
        {
            List<string> checkList = new List<string>(physicalMemory);
            for(int i = indexStart; i<pageList.Count;i++)
            {
                foreach(string t in checkList.ToList())
                {
                    if (checkList.Count() == 1)
                        break;
                    if (pageList[i] == t)
                        checkList.Remove(t);
                }
            }
            if(checkList.Count()!=1)
            {
                DoFiFo(indexStart, checkList);
            }
            int n = 0; //Vi chi thay 1 trang nen tra ve vi tri cua trang duy nhat can thay trong checkList
            return ReturnIndex(physicalMemory, checkList[n]);
        }
        int ReturnIndex(List<string> listIn,string a)
        {
            int index = -1;
            for(int i=0;i<listIn.Count;i++)
            {
                if (listIn[i] == a)
                    index = i;
            }
            return index;
        }
        public void DoOPT()
        {
            for(int i=0;i<pageList.Count;i++)
            {
                if (physicalMemory.Contains(pageList[i]))
                { }
                else if (physicalMemory.Count() < frameCount)
                    physicalMemory.Add(pageList[i]);
                else
                    physicalMemory[CheckFrame(i)] = pageList[i];
                List<string> temp = new List<string>(physicalMemory);
                resultList.Add(temp);
            }
        }
        public void PrintResultByCol()
        {
            foreach (string t in pageList)
                Console.Write("{0}  ", t);
            Console.WriteLine("\n");
            for(int col=0;col<frameCount;col++)
            {
                for(int row=0;row<resultList.Count;row++)
                {
                    if (col < resultList[row].Count())
                        Console.Write("{0}  ", resultList[row][col]);
                    else
                        Console.Write("   ");
                }
                Console.WriteLine();
            }
        }
        public void PrintResultByRow()
        {
            int index = 0;
            foreach(List<string> t in resultList)
            {
                Console.Write("{0}\t", pageList[index]);
                index++;
                foreach(string i in t)
                {
                    Console.Write("{0} ", i);
                }
                Console.WriteLine();
            }
        }
        public void PrintResult()
        {
            if (pageList.Count() > 40)
            {
                Console.WriteLine("Chuoi nhap vao qua dai va co the gay ra loi trinh bay\nHe thong tu dong chuyen sang dang in ngang");
                PrintResultByRow();
            }
            else
                PrintResultByCol();
        }
    }
}
