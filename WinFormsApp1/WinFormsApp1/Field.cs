using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sweeper
{
    internal class Field
    {
        public Cell[,] cells;                       //массив "клеток"
        public int N { get; set; }   //N - длинна стороны поля
        public event EventHandler<ChangeArgs> Change;
        public event EventHandler<EventArgs> Win; // событие победы
        public event EventHandler<EventArgs> Loose; // событие проигрыша 

        public Field(int n)     //конструктор
        {
            N = n;
            cells = new Cell[N, N];
            int amt = 10; //количество мин
            Random r = new Random();
            for (int i = 0; i < amt; i++)
            {
                int x = r.Next(N);
                int y = r.Next(N);
                if (cells[x, y].IsMine)
                {
                    i--;
                    continue;
                }
                cells[x, y].IsMine = true;
            }
        }

        public int MinesAround(int i, int j)        //количество мин вокруг
        {
            int res = 0;
            for (int k = 0; k < N; k++)
            {
                for (int t = 0; t < N; t++)
                {
                    if (Math.Abs(i - k) <= 1 && Math.Abs(t - j) <= 1 && cells[k, t].IsMine)
                    {
                        res++;
                    }
                }
            }
            return res;
        }

        public void OpenCell(int i, int j)
        {
            if (cells[i, j].IsMine)
            {
                Loose?.Invoke(this, new EventArgs());
            }
            cells[i, j].Active = true;
            if (Change != null)
            {
                Change(this, new ChangeArgs { I = i, J = j, MinesArround = Convert.ToString(MinesAround(i, j)) });
            }
            if (MinesAround(i, j) == 0)
            {
                for (int k = 0; k < N; k++)
                {
                    for (int p = 0; p < N; p++)
                    {
                        if (Math.Abs(i - k) <= 1 && Math.Abs(p - j) <= 1 && cells[k, p].Active == false)
                        {
                            OpenCell(k, p);
                        }
                    }
                }
            }
            OpenLast();
        }

        void OpenLast()                     
        {
            foreach (Cell t in cells)
            {
                if (!t.Active && !t.IsMine) return;
            }
            if (Win != null)
            {
                Win(this, new EventArgs());
            }
        }
    }
}
