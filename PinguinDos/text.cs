using System;
using System.IO;
using Sys = Cosmos.System;
namespace PenguinOS.text
{
    public class textutil
    {
        public string clipboard = "";

        public void copy(string sel)
        {
            clipboard = sel;
        }
        public string paste()
        {
            return clipboard;
        }
        public string[] Insertarr(int pos, string insert, string[] arr)
        {
            if (arr.Length > 0)
            {
                int i;
                string[] newarr = new string[arr.Length + 1];
                for (i = 0; i < (arr.Length + 1); i++)
                {
                    if (i < pos - 1)
                        newarr[i] = arr[i];
                    else if (i == pos - 1)
                        newarr[i] = insert;
                    else
                        newarr[i] = arr[i - 1];
                }
                return newarr;
            }
            else
            {
                string[] newarr = new string[1];
                newarr[0] = insert;
                return newarr;
            }
        }
        public string[] addarr(string add, string[] arr)
        {
            int i;
            string[] narr = new string[arr.Length + 1];
            for (i = 0; i < arr.Length; i++)
            {
                narr[i] = arr[i];
            }
            narr[arr.Length] = add;
            return narr;
        }
        public string[] shortarr(string[] arr)
        {
            string[] narr = new string[arr.Length - 1];
            for (var i = 0; i < arr.Length - 1; i++)
            {
                narr[i] = arr[i];
            }
            return narr;
        }
        public void texedit(string filepath)
        {
            bool running = true;
            string[] fcontent = File.ReadAllText(filepath).Split("\n");

            int[] cindex = new int[] { File.ReadAllText(filepath).Length, 0 };
            string[] l = Path.GetFileName(filepath).Split(".");
            int tl = 0;

            if (l[l.Length - 1] != "txt")
            {
                Console.WriteLine("The file specified was not in .txt format.");
                return;
            }
            //init
            Console.WriteLine("Texedit controls: Esc = exit, ALT+S = Save");
            Console.WriteLine("Press any key to continue.");
            Console.ReadKey();
            Console.Clear();

            Console.WriteLine($"+===Texedit v0.0.1 - {Path.GetFileName(filepath)}===========+");
            foreach (string line in fcontent)
            {
                tl++;
                Console.Write($"|{tl} |");
                Console.WriteLine(line);

            }

            while (running)
            {

                var key = Console.ReadKey();
                char keystroke = key.KeyChar;


                if (key.Key == ConsoleKey.Escape)
                {
                    string text = "";
                    foreach (var i in fcontent)
                    {

                        text = text + i;
                        text = text + "\n";
                    }
                    StreamWriter sw = new StreamWriter(Directory.GetCurrentDirectory() + filepath);
                    sw.Write(text);
                    Console.Clear();
                    running = false;
                }
                else if (key.Key == ConsoleKey.Enter)
                {
                    if (cindex[1] + 1 < tl)
                    {
                        fcontent = Insertarr(cindex[1], "", fcontent);
                        Console.Clear();
                    }
                    else
                    {
                        fcontent = addarr("", fcontent);
                        Console.Clear();
                    }
                    cindex[1]++;
                    cindex[0] = 0;
                    tl++;
                }
                else if (key.Key == ConsoleKey.Backspace)
                {
                    if (cindex[0] - 1 >= 0)
                    {
                        fcontent[cindex[1]] = fcontent[cindex[1]].Remove(fcontent[cindex[1]].Length - 1);
                        cindex[0]--;
                    }
                    else
                    {

                        if (cindex[1] > 0)
                        {
                            fcontent = shortarr(fcontent);
                            cindex[1]--;
                        }
                    }
                    Console.Clear();
                }
                else if (key.Key == ConsoleKey.UpArrow)
                {
                    //moves to last line
                    if (cindex[1] > 0) { cindex[1]--; }
                    Console.Clear();
                }
                else if (key.Key == ConsoleKey.DownArrow)
                {
                    //moves to next line
                    if (cindex[1] < tl) { cindex[1]++; }
                    Console.Clear();
                }
                else if (((key.Modifiers & ConsoleModifiers.Alt) != 0) & (key.Key.ToString() == "s"))
                {
                    string text = "";
                    foreach (var i in fcontent)
                    {

                        text = text + i;
                        text = text + "\n";
                    }
                    StreamWriter sw = new StreamWriter(filepath);
                    sw.Write(text);
                }

                else
                {

                    //should handle all other cases (unless delete)
                    fcontent[cindex[1]] = fcontent[cindex[1]] + keystroke.ToString();
                    cindex[0]++;
                    Console.Clear();
                }
                Console.WriteLine($"+===Texedit v0.0.1 - {Path.GetFileName(filepath)}===========+");
                var ln = 0;
                foreach (string line in fcontent)
                {
                    ln++;
                    Console.Write($"|{ln} |");
                    Console.WriteLine(line);
                }
                Console.WriteLine($"(Ln {cindex[0]}, Col{cindex[1]})");

            }
        }
        public void prt(string path)
        {
            try
            {
                if (path.Contains(":"))
                {
                    string content = File.ReadAllText(path);
                    Console.WriteLine($"File {Path.GetFileName(path)} in {path} (Approximate size: {content.Length} bytes)");
                    Console.WriteLine(content);
                }
                else
                {
                    string content = File.ReadAllText(Directory.GetCurrentDirectory() + path);
                    Console.WriteLine($"File {Path.GetFileName(path)} in {path} (Approximate size: {content.Length} bytes)");
                    Console.WriteLine(content);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
    }
}