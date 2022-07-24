using System;
using System.IO;

namespace MaxSizeDirectoryApp // Note: actual namespace depends on the project name.
{
    internal class Program
    {
        static void Main(string[] args)
        {
           string NameDirMaxSize = "";
           long DirMaxSize = 0;
            Console.WriteLine("***Нахождение подкаталога с максимальным размером в заданном каталоге***");
            Console.Write("Введите путь: ");
            string? path = Console.ReadLine();
            if (Directory.Exists(path) == false)
            {            
                Console.WriteLine("Путь задан некорректно", path);
                return;
            }
            string[] directories = Directory.GetDirectories(path, "*");//считываем подкаталоги из корневого каталога 
           foreach (string dir in directories)//для каждого подкатолога 
            {
                long dir_size = 0;
                DirectoryInfo _dir = new DirectoryInfo(dir);
                dir_size = Directories(_dir, dir_size);//рекурсивно ищем файлы во вложенных катологах и ссумируем их размеры
                Console.WriteLine($"Размер каталога {_dir}: {dir_size/1024} КБ");
                NameDirMaxSize = (dir_size > DirMaxSize) ? _dir.Name : NameDirMaxSize;
                DirMaxSize = (dir_size > DirMaxSize)? dir_size : DirMaxSize;
                
            }
            Console.WriteLine($"Каталог {NameDirMaxSize} имеет максимальный размер {DirMaxSize/1024} КБ");

            static long Directories(DirectoryInfo dir, long dir_size)//функция возвращает сумму размеров файлов в подкаталоге
            {
                foreach (var file in dir.GetFiles())//если в подкаталогах нет папок, но есть файлы
                {
                    dir_size = dir_size + file.Length;
                }
                foreach (var subdir in dir.GetDirectories())//если в подкаталогах есть папки и файлы
                {
                    foreach (var file in subdir.GetFiles())
                    {
                        dir_size = dir_size + file.Length;
                    }
//                   Console.WriteLine($"Размер каталога {x.Name}: {dir_size/1024} КБ");
                    if (subdir.GetDirectories().Count() > 0)
                    {
                        dir_size = dir_size + Directories(subdir, dir_size);
                    }
                }
                return dir_size;
            }
        }
    }
 }

 