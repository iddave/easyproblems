using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweeper
{
    internal class ControlButton : Button
    {
        Field field;
        public int i, j;

        public ControlButton(Field f, int i, int j)
        {
            field = f;
            this.i = i;
            this.j = j;
        }

        protected override void OnMouseDown(MouseEventArgs mevent) // правая кнопка
        {
            //if (mevent.Button == MouseButtons.Right)
            //{
            //    if (!field.cells[i, j].IsOpen)
            //    {
            //        BackgroundImage = (System.Drawing.Image)saper.Properties.Resources.cellflag;
            //    }
            //}
            
            
                field.OpenCell(i, j);
            
        }
    }
}
