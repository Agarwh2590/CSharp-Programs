using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NumberToWordConverter
{
    class Program
    {
        static string[] singleDigit = { "Zero", "One", "Two", "Three", "Four", "Five", "Six", "Seven", "Eight", "Nine" };
        static string[] twoDigit = { "", "Ten", "Eleven", "Twelve", "Thirteen", "Fourteen", "Fifteen", "Sixteen", "Seventeen", "Eighteen", "Nineteen" };
        static string[] tens = { "", "", "Twenty", "Thirty", "Forty", "Fifty", "Sixty", "Seventy", "Eighty", "Ninety" };
        static string[] hundreds = {"Hundred", "Thousand", "Million" };
        static void Main()
        {
            string wordFormat = string.Empty;
            int len = 0;
            Console.WriteLine("Enter the number");
            string number = Console.ReadLine();
            
            if(string.IsNullOrEmpty(number) || String.IsNullOrWhiteSpace(number))
            {
                Console.WriteLine("Invalid Input");
            }
            else
            {
                len = number.Trim().TrimStart('0').Length;
            }

            if(len == 0)
            {
                wordFormat = "Zero";
            }
            else if(len == 1)
            {
                Console.WriteLine(singleDigit[Convert.ToInt32(number)]);
            }
            else
            {
                number = number.PadLeft(9, '0');
                wordFormat = NumToWord(number).Trim();
                if(String.Equals(wordFormat.Substring(wordFormat.Length - 3).ToUpper(),"AND"))
                {
                    wordFormat = wordFormat.Trim().Substring(0, wordFormat.Length - 3).Trim();
                }
            }

            Console.WriteLine(wordFormat.Trim());
            Console.ReadKey();
        }

        public static string ConvertTwoDigitNumberToWord(string number)
        {
            StringBuilder sb = new StringBuilder();
            int len = number.Length;
            char[] arr = number.ToCharArray();
            if(len == 2)
            {
                if(Convert.ToInt32(number) > 9 && Convert.ToInt32(number) < 20)
                {
                    int sum = arr[0] - '0' + arr[1] - '0';
                    sb.Append(twoDigit[sum]);
                }
                else
                {
                    int x = 0;
                        int n = arr[x++] - '0';
                        sb.Append(tens[n] + " ");
                        if(arr[x] - '0' != 0)
                        {
                            sb.Append(singleDigit[arr[x] - '0']);
                        }
                }
            }
            return sb.ToString();
        }


        public static string ConvertThreeDigitToWord(string number)
        {
            int len = number.Length;
            char[] arr = number.ToCharArray();
            StringBuilder sb = new StringBuilder();
            if(len == 3)
            {
                int x = 0;
                if(arr[x] - '0' != 0)
                {
                    sb.Append(singleDigit[arr[x] - '0'] + " ");
                    sb.Append(hundreds[x] + " ");
                }
               
                var str = ConvertTwoDigitNumberToWord(number.Substring(x + 1));
                if(!String.IsNullOrWhiteSpace(str))
                {
                    sb.Append("And "+str);
                }
            }
            return sb.ToString();
        }

        public static string ConvertOneDigitToWord(string number)
        {
            return singleDigit[Convert.ToInt32(number)].ToString();
        }

        public static string NumToWord(string number)
        {
            //divide the number into 3 different parts
            string[] arr = new string[3];
            int len = number.Length;
            int x = 0,ctr =0,i=0;
            string num = string.Empty;
            while(i<len)
            {
                
                if(ctr == 3)
                {
                    ctr = 0;
                    arr[x++] = num.ToString();
                    num = string.Empty;
                }
                num = num + number[i];
                ctr++;
                i++;
            }
            arr[x] = num;
            StringBuilder sb = new StringBuilder();
            int y = 2;
            
            for(int j = 0; j< 3; j++)
            {
                string suffix = string.Empty;
                if(arr[j].TrimStart('0').Length == 2)
                {
                    string str = ConvertTwoDigitNumberToWord(arr[j].TrimStart('0'));
                    if(!String.IsNullOrWhiteSpace(str))
                    {
                        sb.Append(" "+str);
                        
                        if (j != 2)
                        {
                            suffix = " And ";
                            sb.Append(" " + hundreds[y]);
                        }
                            
                    }  
                }
                else if(arr[j].TrimStart('0').Length == 3)
                {
                    string str = ConvertThreeDigitToWord(arr[j].TrimStart('0'));
                    if (!String.IsNullOrWhiteSpace(str))
                    {
                        sb.Append(" "+str);
                       
                        if (j != 2)
                        {
                            suffix = " And ";
                            sb.Append(" " + hundreds[y]);
                        }
                           
                    }
                }
                else if(arr[j].TrimStart('0').Length == 1)
                {
                    string str = ConvertOneDigitToWord(arr[j].TrimStart('0'));
                    if (!String.IsNullOrWhiteSpace(str))
                    {
                        sb.Append(" "+str);
                        
                        if (j != 2)
                        {
                            suffix = " And ";
                            sb.Append(" " + hundreds[y]);
                        }
                           
                    }
                }
                sb.Append(suffix);
                y--;
              
            }
            
            return sb.ToString();
        }
    }
}
