/*
 using System;
using System.Collections.Generic;
using Sys = Cosmos.System;
using PenguinOS.text;

namespace PenguinOS.PengCS
{
    class code
    {
        public PenguinOS.text.textutil t = new();
        private int[] Insertarr(int pos, int insert, int[] arr)
        {
            if (arr.Length > 0)
            {
                int i;
                int[] newarr = new int[arr.Length + 1];
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
                int[] newarr = new int[1];
                newarr[0] = insert;
                return newarr;
            }
        }
        private int[] addarr(int add, int[] arr)
        {
            int i;
            int[] narr = new int[arr.Length + 1];
            for (i = 0; i < arr.Length; i++)
            {
                narr[i] = arr[i];
            }
            narr[arr.Length] = add;
            return narr;
        }

        object[] funcreturn = new object[1]();
        private int[] shortarr(int[] arr)
        {
            int[] narr = new int[arr.Length - 1];
            for (var i = 0; i < arr.Length - 1; i++)
            {
                narr[i] = arr[i];
            }
            return narr;
        }
        public Dictionary<string,Dictionary<string, string>> funcdict = new Dictionary<string, Dictionary<string, string>>();
        public void fparser(string[] fparam,string types,int[] int32params = null, string[] stringparams = null)
        {
            
            string funcname = fparam[fparam.Length - 1];
            fparam = t.shortarr(fparam);
            funcdict.Add(funcname, new Dictionary<string, string>(){
                {"fnaccess",fparam[0]},
                {"fnreturntype",fparam[1]},
                

            }) ;

            
        }

        public void exec()
        {

        }
        public void parse(string c) {
            //int bcount = 0;
            //int[] bindex = int[1];

           //foreach(char ch in c)
            //{
               // if (ch == '{')
                //{
                    //bcount++;
                    
                //}else if(ch == '}')
                //{
                    //bcount--;
                //}
            //}

            string[] codearray = c.Split("\n");
            foreach(string i in codearray)
            {
                if (i.Contains("def"))
                {
                    string[] funcdata = i.Split("(")[0].Split(" ");



                    //def pub main(){}
                }
            }
        }
    }
}*/