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
        //static StreamReader reader;
        static int sL;
        static int[] s;//Серверная
        static int lL;
        static int[] l;//Локалка
        static int[] res; //Вывод общий
        static int[] diff; //Разница
        static int diffIndex = 0;
        static int readLnInt()
        {
            return int.Parse(Console.ReadLine());
        }
        static int[] splitLnInt()
        {
            string[] arr = Console.ReadLine().Split(' ');
            int[] r = new int[arr.Length];
            for(int i = 0; i < arr.Length; i++)
            {
                r[i] = int.Parse(arr[i]);
            }
            return r;
        }
        static void Main(string[] args)
        {
            Console.WriteLine("Длина серверного массива:");
            int serverGenLen = readLnInt();
            Console.WriteLine("Длина локального массива:");
            int localGenLen = readLnInt();
            Console.WriteLine("Отправная точка серверного массива:");
            int startGenPoint = readLnInt();
            int genTimestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute;
            genTest(serverGenLen, localGenLen, startGenPoint);
            genTimestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute - genTimestamp;
            Console.WriteLine("Генерация теста завершена за: " + genTimestamp + "ms.");
            /*Console.WriteLine("ТЕСТ");
            Console.WriteLine("Сервер:");
            for (int i = 0; i < sL; i++)
                Console.Write(s[i] + " ");
            Console.WriteLine();
            Console.WriteLine("Локальный:");
            for (int i = 0; i < lL; i++)
                Console.Write(l[i] + " ");
            Console.WriteLine();*/
            Console.WriteLine("Начало вычислений...");
            int timestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute;
            //reader = new StreamReader(File.OpenRead("input_server.txt"));
            //s = splitLnInt();
            //sL = s.Length;
            //l = splitLnInt();
            //lL = l.Length;
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
            /*Console.WriteLine("Синхронизированный список:");
            for (int i = 0; i < res.Length; i++)
                Console.Write(res[i] + " ");
            Console.WriteLine();
            Console.WriteLine("Недостающие сообщения:");
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
        static void genTest(int serverLength, int localLength, int serverPoint)
        {
            Random rnd = new Random();
            sL = serverLength;
            s = new int[sL];
            lL = localLength;
            l = new int[lL];
            int point = serverPoint;
            for(int i = 0; i < serverLength; i++)
            {
                s[i] = point;
                point += rnd.Next(2) + 1;
            }
            for (int i = 0; i < serverPoint; i++)
                l[i] = i;
            shuffle(s);
            for(int i = serverPoint; i < lL; i++)
            {
                l[i] = s[i];
            }
            Array.Sort(s);
            Array.Sort(l);
        }
        static void shuffle(int[] arr)
        {
            Random rnd = new Random();
            for(int i = 0; i < arr.Length; i++)
            {
                int index = rnd.Next(arr.Length);
                int temp = arr[index];
                arr[index] = arr[i];
                arr[i] = temp;
            }
        }

    }
}
