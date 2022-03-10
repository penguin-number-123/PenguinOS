using PenguinOS.graphics;
using Sys = Cosmos.System;
using Cosmos.System.Graphics;
using System.Drawing;
namespace PenguinOS.Window
{
    class driver
    {
        public PenguinOS.graphics.graphicdriver g = new();
        public void drawWindow(int posx, int posy, int sizex,int sizey,int bezel, Color edgecolor,Color fill= default)
        {
            ///<summary>
            ///Draw window at bottom left corner coords
            ///Note that bezel will be the radius of the edge, and thus will be doubled.
            ///</summary>
            if (fill == default)
            {
                fill = Color.Beige;
            }
            g.drawCircle(posx - bezel, posy - bezel,bezel,edgecolor,180,270);
            g.drawLine(posx, posy, posx + sizex, posy + sizey,edgecolor);
            g.drawCircle(posx - bezel, posy - bezel + sizey, bezel, edgecolor,270,360);
            g.drawLine(posx - bezel, posy + bezel, posx - bezel + sizex, posy + bezel + sizex, edgecolor);
            g.drawCircle(posx + sizex - bezel, posy + sizey - bezel,bezel,edgecolor,0,90);
            g.drawLine(posx + sizex + bezel, posy - bezel + sizey, posx + sizex - bezel, posy - sizey + bezel,edgecolor);
            g.drawCircle(posx + sizex - bezel, posy - bezel, bezel, edgecolor, 90, 180);
            g.drawLine(posx + bezel, posy - bezel, posx + bezel + sizex, posy - bezel, edgecolor);
        }
    }
}