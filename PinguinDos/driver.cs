using System;
using Sys = Cosmos.System;
using System.Drawing;
using System.IO;
using Cosmos.System.Graphics;
using VBEDriver = Cosmos.HAL.Drivers.VBEDriver;
using IL2CPU.API.Attribs;

namespace PenguinOS.graphics
{
    
    unsafe public class graphicdriver
    {
        //[ManifestResourceStream(ResourceName="PinguinDos.data.cursor.bmp")] static byte[] c;
        public int[] Buffer;
        public VBEDriver VBE;
        public int Width = 1280;
        public int Height = 960;
        //public Bitmap cursor = new(c);
        
        public int cycles = 0;
        public void init()
        {
            Sys.MouseManager.ScreenHeight = 960;
            Sys.MouseManager.ScreenWidth = 1280;
            Console.Clear();
            Clear(Color.Azure);

            Update();

            //DrawBitmap(cursor, 222, 222, 4, 4);



           
        }
        public void drawLine(int x,int y, int x1, int y1, Color c)
        {
            int i = x;
            int j = y;
            while(i!=x1 & j != y1)
            {
                j++;
                setpixel(i, j, c);
                i++;
                setpixel(i, j, c);
            }
                
        }
        public void setpixel(int x, int y, Color c)
        {
            Buffer[(Width * y) + x] = (int)c.ToArgb();
        }
        public void handlemouse()
        {
            

            int X = checked((int)Sys.MouseManager.X);
            int Y = checked((int)Sys.MouseManager.Y);
            Pen pen = new Pen(Color.Black);
            Clear(Color.Azure);
            setpixel(X, Y,Color.Black);

        }
        
        public void Drawrect(int x, int y,int x1,int y1 , Color c)
        {
            for(int i = 0; i <= Math.Abs(x - x1); i++)
            {
                for(int j = 0; j <= Math.Abs(y - y1);j++){
                    setpixel(x + i, y + j,c);
                }
            }
            
        }
        public void DrawBitmap(Bitmap bmp, int x, int y, int offsetx = 0, int offsety = 0)
        {
            ///<summary>
            ///Draw bitmap file at position x for bottom left corner.
            ///</summary>
            for(int imx = 0; imx< bmp.Width; imx++)
            {
                for(int imy = 0; imy<bmp.Height; imy++)
                {
                    setpixel(x + imx+offsetx, y + imy+offsety, Color.FromArgb(bmp.rawData[(bmp.Width * imy) + imx]));
                }
            }
        }
       
        public Color Blend(Color topcolor, Color bottomcolor)
        {
            
            int R = (topcolor.R * 2 + bottomcolor.R) / 3;
            int G = (topcolor.G * 2 + bottomcolor.G) / 3;
            int B = (topcolor.B * 2 + bottomcolor.B) / 3;
            return Color.FromArgb(topcolor.A, R, G, B);
        }
        public Color Blend(Color topcolor, Color bottomcolor, int topweight)
        {
            if (topweight == 0)
            {
                throw new("Function 'Blend' does not accept zero as topweight arguement.");
            }

            int R = (topcolor.R * topweight + bottomcolor.R) / (1 + topweight);
            int G = (topcolor.G * topweight + bottomcolor.G) / (1 + topweight);
            int B = (topcolor.B * topweight + bottomcolor.B) / (1 + topweight);
            return Color.FromArgb(topcolor.A, R, G, B);
        }
        public void drawCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0)
                return;

            for (; StartAngle < EndAngle; StartAngle++)
            {
                setpixel(
                   (int)(X + (Radius * Math.Sin(Math.PI * StartAngle / 180))),
                   (int)(Y + (Radius * Math.Cos(Math.PI * StartAngle / 180))),
                   Color);
            }
        }

        public void drawFilledCircle(int X, int Y, int Radius, Color Color, int StartAngle = 0, int EndAngle = 360)
        {
            if (Radius == 0)
                return;

            for (int I = 0; I < Radius; I++)
            {
                drawCircle(X, Y, I, Color, StartAngle, EndAngle);
            }
        }
        public void Update()
        {
            /*cycles++;
            if (cycles == 100)
            {
                cycles = 0;
                Cosmos.Core.Memory.Heap.Collect();
            }*/
            if (Buffer.Length < Width * Height)
            {
                VBE.VBESet((ushort)Width, (ushort)Height, 32, true);
                Buffer = new int[Width * Height];
            }

            Cosmos.Core.Global.BaseIOGroups.VBE.LinearFrameBuffer.Copy((int[])Buffer);
        }
        
        public void Clear(Color Color = default)
        {
            if (Color == default)
            {
                Color = Color.Black;
            }

            for (int I = 0; I < Width * Height; I++)
            {
                Buffer[I] = (int)Color.ToArgb();
            }
        }
    }
}