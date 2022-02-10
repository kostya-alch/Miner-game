using System;
using System.Drawing;
using System.Windows.Forms;

namespace Miner_game
{
    public partial class Form1 : Form
    {
        int width = 10; // ширина столбца поля
        int height = 10; // высота столбца поля
        int distanceButtons = 35; // расстояние между каждой кнопкой
        ButtonExented[,] allButtons; // двумерный массив кнопок, чтобы ими можно было оперировать
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            allButtons = new ButtonExented[width, height]; // экземпляр класса
            Random rng = new Random(); // генерируем рандомные значения на полях
            for (int x = 10; (x - 10) < width * distanceButtons; x += distanceButtons) // цикл создания кнопок
            {
                for (int y = 10; (y - 10) < height * distanceButtons; y += distanceButtons)
                // цикл в котором мы заполняем игровое поле с рандомно взятыми значениями
                {
                    ButtonExented button = new ButtonExented(); // создание кнопки в форме.
                    button.Location = new Point(x, y); // присваиваем локацию кнопке
                    button.Size = new Size(30, 30); // размер кнопки
                    if (rng.Next(0, 101) < 20) // рандомно присваиваем кнопке бомбу
                    {
                        button.isBomb = true;
                    }
                    allButtons[(x - 10) / distanceButtons, (y - 10) / distanceButtons] = button; // рассчитываем дистанцию
                    Controls.Add(button); // добавляем кнопки
                    button.Click += new EventHandler(FieldClick); // вешаем обработчик клика
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
                    if (allButtons[x, y].isBomb) // проверка на бомбу
                    {
                        allButtons[x, y].Text = "*"; // рисуем сами бомбочки по условию isBomb
                    }
                }
            }
            MessageBox.Show("Вы проиграли");
        }

        void EmptyField(ButtonExented button) // логика если в клетке нету бомбы и игра продолжается
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < width; y++)
                {
                    if (allButtons[x, y] == button)
                    {
                        button.Text = "" + CountBombAround(x, y); // отображение в клетке числа бомб вокруг.
                    }
                }
            }
        }
        int CountBombAround(int xB, int yB)
        // вспомогательная функция подсчета числа бомб размером 3х3 вокруг кнопки
        {
            int countBombs = 0; // счетчик бомб
            for (int x = xB - 1; x <= xB + 1; x++) // два цикла которые считают сами бомбы
            {
                for (int y = yB - 1; y <= yB + 1; y++)
                {
                    if (x >= 0 && x < width && y >= 0 && y < height)
                    {
                        if (allButtons[x, y].isBomb)
                        {
                            countBombs++;
                        }
                    }
                }
            }
            return countBombs;
        }
    }
    class ButtonExented : Button // наследуемый класс, добавляющий к кнопке поле бомбы.
    {
        public bool isBomb;
    }
}
