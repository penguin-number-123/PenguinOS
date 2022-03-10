/*using Sys = Cosmos.System;
using System;
using System.Collections.Generic;
//also called ponc-30
namespace PenguinOS.ponc
{
    class arrayutil
    {
        public int[] Insertarr(int pos, int insert, int[] arr)
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
        public int[] addarr(int add, int[] arr)
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

        
        public int[] shortarr(int[] arr)
        {
            int[] narr = new int[arr.Length - 1];
            for (var i = 0; i < arr.Length - 1; i++)
            {
                narr[i] = arr[i];
            }
            return narr;
        }
        public int[] delfirst(int[] arr)
        {
            int[] narr = new int[arr.Length - 1];
            for(var i = 0; i < arr.Length - 2; i++)
            {
                narr[i] = arr[i + 1]; 
            }
            return narr;
        }
    }
    class interperter
    {
        public PenguinOS.ponc.arrayutil aru = new PenguinOS.ponc.arrayutil();
        public int[] stack = new int[] { };
        public Dictionary<int, int> ivardict = new();
        public Dictionary<int, int[]> svardict = new();
        public Dictionary<string, int[]> functions = new();
        public Dictionary<int,int[]> localargs = new Dictionary<int,int[]>();
        public void run(int[] code)
        {
            int temp = 0;
            int i = 0;
            while(i<code.Length-1)
            {
     
                    switch (code[i])
                    {
                        case 1:
                            //add value to stack
                            stack = aru.addarr(code[i+1],stack);
                            i++;
                            break;
                        case 2:
                            //Add
                            temp = stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            temp += stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            stack = aru.addarr(temp, stack);
                            break;
                        case 3:
                            //Multiply
                            temp = stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            temp *= stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            stack = aru.addarr(temp, stack);
                            break;
                        case 4:
                            //subtract
                            temp = stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            temp -= stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            stack = aru.addarr(temp, stack);
                            break;
                        case 5:
                            //floor div
                            temp = stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            temp = (int)Math.Floor((double)temp/(double)stack[stack.Length - 1]);
                            stack = aru.shortarr(stack);
                            stack = aru.addarr(temp, stack);
                            break;
                        case 6:
                            // print stack value
                            Console.WriteLine(stack[stack.Length - 1]);
                            break;
                        case 7:
                            //print next value
                            Console.WriteLine(code[i + 1]);
                            i++;
                            break;
                        case 8:
                            // print ascii from stack
                            try
                            {
                                Console.WriteLine((char)stack[stack.Length - 1]);
                            }catch(OverflowException)
                            {
                                Console.WriteLine("Out of range.");
                            }
                            break;
                        case 9:
                            // print string char of length a
                            int count = code[i + 1];
                            i++;
                            string t = "";
                            int j = 0;
                            while (j <= count)
                            {
                                t += (char)code[i + j];
                                j++;
                            }
                            i += j;
                            try
                            {
                                Console.WriteLine(t);
                                i++;
                            }
                            catch (OverflowException)
                            {
                                Console.WriteLine("Out of range.");
                            }
                            break;
                        case 10:
                            //get input
                            stack = aru.addarr(Int32.Parse(Console.ReadLine()),stack);
                            break;
                        case 11:
                            //do while top is not 0
                            int u = i;
                            int[] wcode = new int[1];
                            while(code[u]!= 12)
                            {
                                u++;
                                wcode = aru.addarr(code[u], wcode);
                            }
                            while (stack[stack.Length - 1] != 0)
                            {
                                run(wcode);
                            }
                            break;
                        //12 is reserved
                        case 12:
                            
                            break;
                        case 13:
                            //duplicate
                            temp = stack[stack.Length - 1];
                            stack = aru.addarr(temp, stack);
                            break;
                        case 14:
                            //equals
                            temp = stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            if(stack[stack.Length-1] == temp)
                            {
                                stack = aru.addarr(1, stack);
                            }
                            else
                            {
                                stack = aru.addarr(0, stack);
                            }
                            break;
                        case 15:
                            //larger
                            temp = stack[stack.Length - 1];
                            stack = aru.shortarr(stack);
                            if (stack[stack.Length - 1] > temp)
                            {
                                stack = aru.addarr(1, stack);
                            }
                            else
                            {
                                stack = aru.addarr(0, stack);
                            }
                            break;
                        case 16:
                            //invert, 0 to 1, any number to zero
                            if(stack[stack.Length-1] == 0)
                            {
                                stack = aru.shortarr(stack);
                                stack = aru.addarr(1, stack);
                            }
                            else
                            {
                                stack = aru.shortarr(stack);
                                stack = aru.addarr(0, stack);
                            }
                            break;
                        case 17:
                            //pop
                            stack = aru.shortarr(stack);
                            break;
                        case 18:
                            //if top is not zero
                            int[] ifcode = new int[1];
                            if (stack[stack.Length - 1] != 0)
                            {
                                while (code[i] != 18)
                                {
                                    ifcode = aru.addarr(code[i], ifcode);

                                }
                                run(ifcode);
                            }
                            break;
                        case 19:
                            break;
                        case 20:
                            //varaible decleration, numeric name, i.e. 20 1 0 5 == int 1 = 5

                        i++;
                        int num;
                        int name = code[i];
                        i++;
                        if (code[i] == 0)
                        {
                            //int 2
                            
                            i++;
                            ivardict.Add(name, code[i]);

                        }
                        else
                        {
                           
                            // string types , 20 3 97 98 99 = "abc"
                            i++;
                            int[] res = new int[] { };
                            int length = code[i];
                            temp = i;
                            while (i - temp <= length)
                            {
                                res= aru.addarr(code[i],res);
                                i++;
                            }

                           svardict.Add(name, res);
                        }
                        
                        break;
                    case 21:
                        i++;
                        if (svardict.ContainsKey(code[i])) {
                            foreach (int k in svardict[code[i]])
                            {
                                stack = aru.addarr(k, stack);
                            }
                        }
                        else
                        {
                            stack = aru.addarr(ivardict[code[i]], stack);
                        }
                        break;
                    case 22:
                        //function
                        i++;
                        int len = code[i];
                        i++; 
                        temp = i;
                        string fname = "";
                        while (i - temp<=len)
                        {
                            fname += (char)code[i];
                            i++;
                        }
                        int[] funcode = new int[] { };
                        while (code[i]!= 23)
                        {
                            funcode = aru.addarr(code[i], funcode);
                            i++;
                        }

                        break;
                    case 23:
                        //end args
                        break;
                    case 24:
                        //end function
                        break;
                    case 25:
                        //call function
                        i++;
                        int leng = code[i];
                        temp = i;
                        string callname = "";
                        while (i - temp <= leng)
                        {

                            callname += (char)code[i];
                            i++;
                        }
                        run(functions[callname]);
                        break;

                    }
                i++;
            }
        }

    }
}*/
