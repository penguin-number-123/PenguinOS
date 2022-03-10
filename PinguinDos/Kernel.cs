using System;
using System.Collections.Generic;
using System.Text;
using Sys = Cosmos.System;
using System.Drawing;
using Cosmos.Core.IOGroup;
using System.IO;
using IL2CPU.API.Attribs;
using Cosmos.System.Graphics;
using System.Net;
using Cosmos.HAL;
using RTC = Cosmos.HAL.RTC;
using PenguinOS.graphics;
using PenguinOS.text;
//using PenguinOS.ponc;
namespace PinguinDos 
{
    

    public class Kernel : Sys.Kernel
    {

        VBECanvas cvs = new(new(1280, 960, (ColorDepth)32));
        public PenguinOS.graphics.graphicdriver g = new PenguinOS.graphics.graphicdriver();




        public void tree(string sDir, int tabs = 1)
        {
            string tabstr = new string('\t', tabs);
            foreach (var f in Directory.GetFiles(sDir))
            {

                Console.WriteLine(tabstr + f);
                if (!(f.Contains(".")))
                {
                    tabs++;
                    tree(sDir + @"\" + f, tabs);
                }
            }
        }
        
        string bmode = "";
        Sys.FileSystem.CosmosVFS fs = new Sys.FileSystem.CosmosVFS();
        public PenguinOS.text.textutil t = new();

        protected override void BeforeRun()
        {
            
            
            Sys.FileSystem.VFS.VFSManager.RegisterVFS(fs);

            fs.Initialize(true);
            Console.Write("Is this your first time installing Penguin OS v 0.0.2? [Y/N]");

            string ftime = Console.ReadLine();
            if (ftime.ToUpper().Contains("Y"))
            {
                foreach (var i in fs.Disks)
                {
                    try
                    {
                        i.CreatePartition(i.Size);
                        Console.WriteLine("Partition Created.");
                        i.FormatPartition(0, "FAT32", true);
                        i.Mount();
                        Console.WriteLine("Partition Mounted");
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e.Message);
                    }
                }
            


            }



            try
            {
                Console.WriteLine("Pinguin OS v0.0.2");
                Console.Write("Select boot mode: CLI, GUI:");
                bmode = Console.ReadLine();
                if (bmode.ToUpper().Contains("GUI"))
                {
                    g.init();
                    g.Clear(Color.Azure);
                    g.Update();
                    
                }
                else
                {
                    Console.Clear();
                    Directory.SetCurrentDirectory(@"0:\");
                    Console.WriteLine("Pinguin OS v0.0.2");
                }



            }
            catch (Exception e)
            {
                
                Console.WriteLine(e);
            }
        }
        
        
        
        protected override void Run()
        {
           
            if (bmode.ToUpper() == "GUI")
            {
                try
                {

                    //g.handlemouse();
                    //g.Drawrect(700, 700, 600, 650, Color.Gray);

                    
                }
                catch (Exception e)
                {
                    //g.exit();
                    Console.WriteLine(e);
                    while (true) { }
                }
            }
            else
            {
                try
                {
                    /*int deltaT = 0;
                    int fps = 0;
                    int frames = 0;
                    if (deltaT != RTC.Second)
                    {
                        fps = frames;
                        frames = 0;
                        deltaT = RTC.Second;
                    }
                    frames++;*/
                    //PenguinOS.ponc.interperter inter = new();
                    Console.Write($"Penguin Shell@{Directory.GetCurrentDirectory()}>");
                    string cmd = Console.ReadLine();
                    //bool debug = false;


                    //Handle cases with trailing data
                    if (cmd.Contains(" "))
                    {
                        cmd = cmd.Replace("/", @"\");
                        string[] cmdarr = cmd.Split(" "); //cd c:/users/... -> ["cd", "c:\users\..."]

                        //Console.WriteLine($"{cmdarr[0]}, {cmdarr[1]}"); 
                        switch (cmdarr[0])
                        {
                            case "run":
                                int[] commands = Array.ConvertAll(cmdarr[1].Split(" "), s => Int32.Parse(s));
                                //inter.run(commands);
                                break;
                            case "color":
                                bool itype = new();
                                if (cmdarr.Length > 3) { itype = (cmdarr[4] == "-n"); }
                                if (cmdarr.Length == 3 || itype)
                                {
                                    Console.WriteLine("Interpreting as -n (name)");
                                    ConsoleColor c = new();
                                    switch (cmdarr[2])
                                    {
                                        case "Black":
                                            c = ConsoleColor.Black;
                                            break;
                                        case "Blue":
                                            c = ConsoleColor.Blue;
                                            break;
                                        case "Cyan":
                                            c = ConsoleColor.Cyan;
                                            break;
                                        case "Dark Cyan":
                                            c = ConsoleColor.DarkCyan;
                                            break;
                                        case "Dark Gray":
                                            c = ConsoleColor.DarkGray;
                                            break;
                                    }
                                    if (cmdarr[1] == "-f") {
                                        Console.ForegroundColor = c;
                                    }
                                    else if (cmdarr[1] == "-b")
                                    {
                                        Console.BackgroundColor = c;
                                    }else
                                    {
                                        Console.Write($"Incorrect flag {cmdarr[1]}.");
                                    }
                                    
                                }
                                break;
                            case "cd":
                                try
                                {
                                    if (cmdarr[1].Contains(":"))
                                    {
                                        Directory.SetCurrentDirectory(cmdarr[1]);
                                        Console.WriteLine($"Set directory to {cmdarr[1]}");
                                    }
                                    else
                                    {
                                        Console.WriteLine("Directory invalid.");
                                    }
                                }
                                catch (DirectoryNotFoundException)
                                {
                                    Console.WriteLine("OS cannot find the needed dir.");
                                };
                                break;
                            case "mkf":
                                try
                                {


                                    // file does not exist, create it
                                    Sys.FileSystem.VFS.VFSManager.CreateFile(@"0:\hi.txt");
                                   Console.WriteLine($"Created file {cmdarr[1]}");
                                    
                                  
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                                break;

                            case "mkd":
                                try
                                {
                                    if (cmdarr[1].Contains(":"))
                                    {
                                        fs.CreateDirectory(cmdarr[1]);
                                    }
                                    else
                                    {
                                        fs.CreateDirectory(Directory.GetCurrentDirectory() + cmdarr[1]);
                                    }
                                    Console.WriteLine($"Created Directory {cmdarr[1]}");
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e.Message);
                                }
                                break;
                            case "prt":
                                t.prt(cmdarr[1]);
                                break;
                            case "hex":
                                try
                                {
                                    string path = Directory.GetCurrentDirectory() + cmdarr[1];
                                    FileStream fils = new FileStream(path, FileMode.Open);
                                    int hexIn;
                                    String hex = "";

                                    for (int i = 0; (hexIn = fils.ReadByte()) != -1; i++)
                                    {
                                        hex = string.Format("{0:X2}", hexIn);
                                    }
                                    for (int i = 0; i < hex.Length; i++)
                                    {
                                        if (i > 0 & (i % 2) == 0)
                                        {
                                            hex.Insert(i, " ");
                                        }
                                        if (i > 0 & (i % 48) == 0)
                                        {
                                            hex.Insert(i, "\n");
                                        }
                                    }
                                    Console.WriteLine(hex);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                                break;

                            case "txedit":
                                try
                                {
                                    t.texedit(cmdarr[1]);
                                }
                                catch (Exception e)
                                {
                                    Console.WriteLine(e);
                                }
                                break;
                            default:
                                Console.WriteLine($"The command '{cmdarr[0].Replace(" ", "")}' does not exist.");
                                break;

                        }

                    }
                    else
                    {
                        //handle program cases (no trailing data)
                        switch (cmd)
                        {
                            case "dspace":
                                string data = $"|Available Free Space: {String.Format("{0:n0}", fs.GetAvailableFreeSpace(Directory.GetCurrentDirectory()))} bytes|";
                                string hyphen = new String('-', (data.Length - 2));
                                Console.WriteLine($"+{hyphen}+");
                                Console.WriteLine(data);
                                Console.WriteLine($"+{hyphen}+");
                                break;
                            case "dir":
                                string dir = Directory.GetCurrentDirectory();
                                Console.WriteLine(dir);
                                string[] filelist = Directory.GetFiles(dir);
                                Console.WriteLine(filelist);
                                if (!(filelist == null || filelist.Length == 0))
                                {
                                    foreach (var file in filelist)
                                    {
                                        Console.WriteLine(file);
                                    }
                                }
                                else { Console.WriteLine("There are no files yet."); }
                                break;
                            case "tree":
                                tree(Directory.GetCurrentDirectory());
                                break;
                            case "cls":
                                Console.Clear();
                                break;
                            case "cd":
                                Console.WriteLine(Directory.GetCurrentDirectory());
                                break;
                            case "shutdown":
                                Sys.Power.Shutdown();
                                Environment.Exit(0);
                                break;
                            case "reboot":
                                Sys.Power.Reboot();
                                break;
                            
                            case "gtest":
                                /*─	━	│	┃	┄	┅	┆	┇	┈	┉	┊	┋	┌	┍	┎	┏
                                    U+251x	┐	┑	┒	┓	└	┕	┖	┗	┘	┙	┚	┛	├	┝	┞	┟
                                    U+252x	┠	┡	┢	┣	┤	┥	┦	┧	┨	┩	┪	┫	┬	┭	┮	┯
                                    U+253x	┰	┱	┲	┳	┴	┵	┶	┷	┸	┹	┺	┻	┼	┽	┾	┿
                                    U+254x	╀	╁	╂	╃	╄	╅	╆	╇	╈	╉	╊	╋	╌	╍	╎	╏
                                    U+255x	═	║	╒	╓	╔	╕	╖	╗	╘	╙	╚	╛	╜	╝	╞	╟
                                    U+256x	╠	╡	╢	╣	╤	╥	╦	╧	╨	╩	╪	╫	╬	╭	╮	╯
                                    U+257x	╰	╱	╲	╳	╴	╵	╶	╷	╸	╹	╺	╻	╼	╽	╾	╿
                                ░	▒	▓  █*/
                                Console.Clear();
                                Console.BackgroundColor = ConsoleColor.DarkBlue;
                                Console.ForegroundColor = ConsoleColor.Blue;
                                //Encoding.RegisterProvider(CosmosEncodingProvider.Instance);
                                Console.InputEncoding = Encoding.GetEncoding(437); //CP437 for example
                                Console.OutputEncoding = Encoding.GetEncoding(437);
                                Console.WriteLine("╔═════════════╗");
                                Console.WriteLine("║             ║▓");
                                Console.WriteLine("║     Test    ║▓");
                                Console.WriteLine("║             ║▓");
                                Console.WriteLine("╚═════════════╝▓");
                                Console.WriteLine(" ░░░░░▒▒▒▒▒▓▓▓▓▓");
                                
                                Console.ForegroundColor = ConsoleColor.White;
                                int j = 0;
                                int pos = 0;
                                while (j < 10)
                                {
                                    if (j == 1)
                                    {
                                        Console.WriteLine("#");
                                    }
                                    else
                                    {
                                        Console.WriteLine("*");
                                    }
                                    j++;
                                }
                                while (true) {

                                    if (Sys.KeyboardManager.TryReadKey(out var Key)){
                                        if (Key.Key == Sys.ConsoleKeyEx.UpArrow)
                                        {
                                            Console.Clear();
                                            pos++;
                                            while (j < 10)
                                            {
                                                if(j == pos)
                                                {
                                                    Console.WriteLine("#");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("*");
                                                }
                                                j++;
                                            }
                                        }
                                        else if (Key.Key == Sys.ConsoleKeyEx.DownArrow)
                                        {
                                            Console.Clear();
                                            pos--;
                                            while (j < 10)
                                            {
                                                if (j == pos)
                                                {
                                                    Console.WriteLine("#");
                                                }
                                                else
                                                {
                                                    Console.WriteLine("*");
                                                }
                                                j++;
                                            }
                                        }
                                        else if (Key.Key == Sys.ConsoleKeyEx.Escape)
                                        {
                                            break;
                                        }
                                    }
                                }
                                break;

                            default:
                                Console.WriteLine($"The command '{cmd}' does not exist.");
                                break;
                        }
                    }
                }
                catch(Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
        }
    }
}
