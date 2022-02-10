﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Miner_game
{
    public partial class Form1 : Form
    {
        int width = 10; // ширина столбца поля
        int height = 10; // высота столбца поля
        int distanceButtons = 35; // расстояние между каждой кнопкой
        ButtonExented[,] allButtons; // массив кнопок
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            allButtons = new ButtonExented[width, height]; // экземпляр класса
            Random rng = new Random(); // генерируем рандомные значения на полях
            for(int x = 10; (x - 10) < width * distanceButtons; x+= distanceButtons) // цикл создания кнопок
            {
                for (int y = 10; (y - 10) < height * distanceButtons; y+= distanceButtons) 
                    // цикл в котором мы заполняем игровое поле с рандомно взятыми значениями
                {
                    ButtonExented button = new ButtonExented(); // создание кнопки в форме.
                    button.Location = new Point(x, y); // присваиваем локацию кнопке
                    button.Size = new Size(30, 30); // размер кнопки
                    if (rng.Next(0, 101) < 20) // рандомно присваиваем кнопке бомбу
                    {
                        button.isBomb = true; 
                    }
                    allButtons[(x - 10) / distanceButtons, (y - 10) / distanceButtons] = button; 
                    Controls.Add(button);
                    button.Click += new EventHandler(FieldClick);
                }
            }
        }
        void FieldClick(object sender, EventArgs e)
        {
            ButtonExented button = (ButtonExented)sender;
            // если бомба - вызов функции взрыва, если нет - игра продолжается.
            if (button.isBomb)
            {
                Explode(button);
            }
            else
            {
                EmptyField(button);
            }
        }
        void Explode(ButtonExented button) // логика проигрыша если нарвался на бомбу
        {
            for (int x = 0; x < width; x++) // два цикла заполняют показывают где находятся мины после проигрыша
            {
                for (int y = 0; y < width; y++)
                {
                    if (allButtons[x, y].isBomb)
                    {
                        allButtons[x, y].Text = "*";
                    }
                }
            }
            MessageBox.Show("Вы проиграли");
        }

        void EmptyField(ButtonExented button) // логика если в клетке нету бомбы и игра продолжается
        {
            // TODO method
        }
    }
    class ButtonExented: Button // наследуемый класс, добавляющий к кнопке поле бомбы.
    {
       public bool isBomb;
    }
}
