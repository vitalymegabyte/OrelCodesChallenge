using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrelCodesChallenge
{
    class Program
    {
        /*Найти пробелы в локальной истории сообщений, сравнивая с серверной.

Пример входных данных:
Локальная история: 1, 2, 3, 4, 5, 8, 9
Серверная история: 5, 6, 7, 8, 9, 10

Часто должно получиться на выходе:
Список сообщений 1, 2, 3, 4, 5, 6, 7 , 8, 9, 10
Список отсутствовавших сообщений:
6, 7, 10

Потестить можно на списках с длинной 10к - наверное, разница будет сильно заметна при разных подходах

P.s. 
Предполагаем, что самый маленький id от сервера - этого точка начала синхронизации. Т.е. до него все сообщения локально уже есть.*/
        static StreamReader reader;
        static int sL;
        static int[] s;//Серверная
        static int lL;
        static int[] l;//Локалка
        static int[] res; //Вывод общий
        static int[] diff; //Разница
        static int diffIndex = 0;
        static int readLnInt()
        {
            return int.Parse(reader.ReadLine());
        }
        static int[] splitLnInt()
        {
            string[] arr = reader.ReadLine().Split(' ');
            int[] r = new int[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                r[i] = int.Parse(arr[i]);
            }
            return r;
        }
        static void Main(string[] args)
        {
            int timestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute;
            reader = new StreamReader(File.OpenRead("input_server.txt"));
            s = splitLnInt();
            sL = s.Length;
            l = splitLnInt();
            lL = l.Length;
            int index = indexOf(l, s[0]);
            res = new int[index + sL];
            diff = new int[sL];
            for (int i = 0; i < index; i++)
                res[i] = l[i];
            int locIndex = index;
            for(int i = 0; i < sL; i++)
            {
                res[i + index] = s[i];
                if(locIndex < lL && l[locIndex] == s[i])
                {
                    locIndex++;
                }
                else
                {
                    diff[diffIndex] = s[i];
                    diffIndex++;
                }
            }
            timestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute - timestamp;
            /*for (int i = 0; i < res.Length; i++)
                Console.Write(res[i] + " ");
            Console.WriteLine();
            for (int i = 0; i < diffIndex; i++)
                Console.Write(diff[i] + " ");
            Console.WriteLine();*/
            Console.WriteLine("Total time: " + timestamp);
            Console.ReadKey();
        }
        static int indexOf(int[] arr, int el)
        {
            int l = -1;
            int r = arr.Length;
            while(l + 1 != r)
            {
                int mid = (l + r) / 2;
                if (arr[mid] < el)
                    l = mid;
                else
                    r = mid;
            }
            return r; //arr[r] >= el
        }
    }
}
