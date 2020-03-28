using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OrelCodesChallengeCore
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
        static StreamReader reader;
        static int readLnInt()
        {
            return int.Parse(reader.ReadLine());
        }
        static int[] splitLnInt()
        {
            string[] arr = reader.ReadToEnd().Split('\n');
            int[] r = new int[arr.Length];
            for (int i = 0; i < arr.Length - 1; i++)
            {
                r[i] = int.Parse(arr[i]);
            }
            return r;
        }
        static void Main(string[] args)
        {
            /*Console.WriteLine("Длина серверного массива:");
            int serverGenLen = readLnInt();
            Console.WriteLine("Длина локального массива:");
            int localGenLen = readLnInt();
            Console.WriteLine("Отправная точка серверного массива:");
            int startGenPoint = readLnInt();
            int genTimestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute;
            genTest(serverGenLen, localGenLen, startGenPoint);
            genTimestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute - genTimestamp;
            Console.WriteLine("Генерация теста завершена за: " + genTimestamp + "ms.");*/
            /*Console.WriteLine("ТЕСТ");
            Console.WriteLine("Сервер:");
            for (int i = 0; i < sL; i++)
                Console.Write(s[i] + " ");
            Console.WriteLine();
            Console.WriteLine("Локальный:");
            for (int i = 0; i < lL; i++)
                Console.Write(l[i] + " ");
            Console.WriteLine();*/
            int[] res; //Вывод общий
            int[] diff; //Разница
            int diffIndex = 0;
            int timestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute;
            reader = new StreamReader(File.Open("input_local.txt", FileMode.Open));
            l = splitLnInt();
            reader = new StreamReader(File.Open("input_server.txt", FileMode.Open));
            s = splitLnInt();
            sL = s.Length;
            lL = l.Length;
            Console.WriteLine("Начало вычислений...");
            //int[] timestamps = new int[1000];
            //for (int r = 0; r < 1000; r++)
            //{
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
                for (int i = 0; i < sL; i++)
                {
                    res[i + index] = s[i];
                    if (locIndex < lL && l[locIndex] == s[i])
                    {
                        locIndex++;
                    }
                    else
                    {
                        diff[diffIndex] = s[i];
                        diffIndex++;
                    }
                }
            StreamWriter writer = new StreamWriter(File.Create("output_result.txt"));
            for(int i = 0; i < res.Length; i++)
            {
                writer.WriteLine(res[i]);
            }
            writer.Close();
            writer = new StreamWriter(File.Create("output_missing.txt"));
            for (int i = 0; i < diffIndex; i++)
            {
                writer.WriteLine(diff[i]);
            }
            writer.Close();
            timestamp = DateTime.Now.Millisecond + 1000 * DateTime.Now.Second + 60000 * DateTime.Now.Minute - timestamp;
            //timestamps[r] = timestamp;
            //diffIndex = 0;
            /*Console.WriteLine("Синхронизированный список:");
            for (int i = 0; i < res.Length; i++)
                Console.Write(res[i] + " ");
            Console.WriteLine();
            Console.WriteLine("Недостающие сообщения:");
            for (int i = 0; i < diffIndex; i++)
                Console.Write(diff[i] + " ");
            Console.WriteLine();*/
            //Console.Write(timestamp + ", ");
            //}
            //Console.WriteLine();
            //Array.Sort(timestamps);
            Console.WriteLine(timestamp + "ms");
            //Console.WriteLine("Медианное время: " + timestamps[500] + "ms. Максимальное время: " + timestamps[999] + "ms. Минимальное время: " + timestamps[0] + "ms.");
            Console.ReadKey();
        }
        static int indexOf(int[] arr, int el)
        {
            int l = -1;
            int r = arr.Length;
            while (l + 1 != r)
            {
                int mid = (l + r) / 2;
                if (arr[mid] < el)
                    l = mid;
                else
                    r = mid;
            }
            return r; //arr[r] >= el
        }
        static void genTest(int serverLength, int localLength, int serverPoint) //Генератор теста. На вход: длина серверного массива, длина локального массива, точка начала на сервере (то есть первый элемент массива)
        {
            Random rnd = new Random();
            sL = serverLength;
            s = new int[sL];
            lL = localLength;
            l = new int[lL];
            int point = serverPoint; //Текущее значение для заполнения серверного массива
            for (int i = 0; i < serverLength; i++) //Добавляет элементы рандомно в серверный массив
            {
                s[i] = point;
                point += rnd.Next(2) + 1;
            }
            for (int i = 0; i < serverPoint; i++)//Добавляет в локальный массив сообщения, которые с сервером уже синхронизированы
                l[i] = i;
            shuffle(s); //Перемешать серверный массив
            for (int i = serverPoint; i < lL; i++)//Получить случайные значения из серверного массива для заполнения локального
            {
                l[i] = s[i];
            }
            Array.Sort(s); //Сортировка обоих, т.к. серверный был перемешан
            Array.Sort(l);
        }
        static void shuffle(int[] arr) //Перемешивание массива. Работает тупо за счет обмена элементов, ничего интересного
        {
            Random rnd = new Random();
            for (int i = 0; i < arr.Length; i++)
            {
                int index = rnd.Next(arr.Length);
                int temp = arr[index];
                arr[index] = arr[i];
                arr[i] = temp;
            }
        }

    }
}
