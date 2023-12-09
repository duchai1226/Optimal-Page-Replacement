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
        /// <summary>
        ///     Imoport inputString
        /// </summary>
        public void Input()
        {
            Console.Write("Nhap so Frame: ");
            int.TryParse(Console.ReadLine(), out frameCount);
            Console.Write("Nhap chuoi dau vao: ");
            inputString=Console.ReadLine();
        }
        /// <summary>
        /// Trans inputString into PageList
        /// </summary>
        public void TransToPageList()
        {
            for(int i=0;i<inputString.Length;i++)
            {
                if (inputString[i] == '-')
                {
                    StringBuilder builder = new StringBuilder();
                    builder.Append(inputString[i]);
                    builder.Append(inputString[i + 1]);
                    string temp = builder.ToString();
                    pageList.Add(temp);
                }
                else
                    pageList.Add(inputString[i].ToString());
            }
        }

        /// <summary>      
        /// Do FiFo and make listIn to only have one elment is longest exist
        /// </summary>
        /// <param name="indexStart"></param>
        /// <param name="listIn"></param>
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
        /// <summary>
        /// Return index of physicalMemory will be change
        /// </summary>
        /// <param name="indexStart"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Return index of string a in list listIn
        /// </summary>
        /// <param name="listIn"></param>
        /// <param name="a"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Do OPT ( Optimal Page Replacement )
        /// </summary>
        public void DoOPT()
        {
            for(int i=0;i<pageList.Count;i++)
            {
                if (physicalMemory.Contains(pageList[i]))
                {
                    List<string> nullTemp = new List<string> { " ", " ", " " };
                    resultList.Add(nullTemp);
                    goto end;
                }
                else if (physicalMemory.Count() < frameCount)
                    physicalMemory.Add(pageList[i]);
                else
                    physicalMemory[CheckFrame(i)] = pageList[i];
                List<string> temp = new List<string>(physicalMemory);
                resultList.Add(temp);
            end:;
            }
        }
        /// <summary>
        /// Check negative number in list
        /// Return 0 if positive numbers, return 1 if negative number.
        /// </summary>
        /// <returns></returns>
        public int CheckNegative(List<string> listIn)
        {
            int result = 0;
            foreach(string t in listIn)
            {
                if (int.Parse(t) < 0)
                    result = 1;
            }
            return result;
        }
        /// <summary>
        /// Print result by columns
        /// </summary>
        public void PrintByCol()
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
        /// <summary>
        /// Print result by Row
        /// </summary>
        public void PrintByRow()
        {
            int index = 0;
            foreach(List<string> t in resultList)
            {
                if(int.Parse(pageList[index])<0)
                    Console.Write("{0}\t", pageList[index]);
                else
                    Console.Write(" {0}\t", pageList[index]);

                index++;
                foreach(string i in t)
                {
                    if (i==" "||int.Parse(i) < 0)
                        Console.Write("{0}  ",i);
                    else
                        Console.Write(" {0}  ",i);
                }
                Console.WriteLine();
            }
        }
        /// <summary>
        /// Print result, function know when print by column and when print by row
        /// </summary>
        public void PrintResult()
        {
            if (pageList.Count() > 40|| CheckNegative(pageList) == 1)
            {
                Console.WriteLine("Chuoi nhap vao qua dai hoac co so am, co the gay ra loi trinh bay\nHe thong tu dong chuyen sang dang in ngang");
                PrintByRow();
            }
            else
                PrintByCol();
        }
    }
}
