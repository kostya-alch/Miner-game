using System;
using System.Drawing;
using System.Windows.Forms;

namespace Miner_game
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// Ширина столбца поля.
        /// </summary>
        private int _width = 10;

        /// <summary>
        /// Высота столбца поля.
        /// </summary>
        private int _height = 10;

        /// <summary>
        /// Расстояние между каждой кнопкой.
        /// </summary>
        private readonly int _distanceBetweenButtons = 35;


        /// <summary>
        /// Массив кнопок.
        /// </summary>
        private ButtonExented[,] _buttons;

        public Form1()
        {
            InitializeComponent();
        }

        private void FieldClick(object sender, EventArgs e)
        {
            ButtonExented button = (ButtonExented)sender;
            // если бомба - вызов функции взрыва, если нет - игра продолжается.
            if (button.IsBomb) Explode(button);
            else EmptyField(button);
        }

        private void Explode(ButtonExented button) // логика проигрыша если нарвался на бомбу
        {
            for (int x = 0; x < _width; x++) // два цикла заполняют показывают где находятся мины после проигрыша
            {
                for (int y = 0; y < _width; y++)
                {
                    if (_buttons[x, y].IsBomb) // проверка на бомбу
                    {
                        _buttons[x, y].Text = "*"; // рисуем сами бомбочки по условию isBomb
                    }
                }
            }
            MessageBox.Show("Вы проиграли");
        }


        private void EmptyField(ButtonExented button) // логика если в клетке нету бомбы и игра продолжается
        {
            for (int x = 0; x < _width; x++)
            {
                for (int y = 0; y < _width; y++)
                {
                    if (_buttons[x, y] == button)
                    {
                        button.Text = "" + CountBombAround(x, y); // отображение в клетке числа бомб вокруг.
                    }
                }
            }
        }

        private int CountBombAround(int xB, int yB)
        // вспомогательная функция подсчета числа бомб размером 3х3 вокруг кнопки
        {
            int countBombs = 0; // счетчик бомб
            for (int x = xB - 1; x <= xB + 1; x++) // два цикла которые считают сами бомбы
            {
                for (int y = yB - 1; y <= yB + 1; y++)
                {
                    if (x >= 0 && x < _width && y >= 0 && y < _height)
                    {
                        if (_buttons[x, y].IsBomb)
                        {
                            countBombs++;
                        }
                    }
                }
            }
            return countBombs;
        }

        private void GenerateField()
        {
            _buttons = new ButtonExented[_width, _height]; // экземпляр класса
            Random rng = new Random(); // генерируем рандомные значения на полях

            for (int x = 10; (x - 10) < _width * _distanceBetweenButtons; x += _distanceBetweenButtons) // цикл создания кнопок
            {
                for (int y = 20; (y - 20) < _height * _distanceBetweenButtons; y += _distanceBetweenButtons)
                // цикл в котором мы заполняем игровое поле с рандомно взятыми значениями
                {
                    ButtonExented button = new ButtonExented(); // создание кнопки в форме.
                    button.Location = new Point(x, y); // присваиваем локацию кнопке
                    button.Size = new Size(30, 30); // размер кнопки

                    if (rng.Next(0, 101) < 20) // рандомно присваиваем кнопке бомбу
                    {
                        button.IsBomb = true;
                    }

                    _buttons[(x - 10) / _distanceBetweenButtons, (y - 20) / _distanceBetweenButtons] = button; // рассчитываем дистанцию
                    Controls.Add(button); // добавляем кнопки
                    button.Click += FieldClick; // вешаем обработчик клика
                }
            }
        }

        private void х10ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _width = 10;
            _height = 10;
            GenerateField();
        }

        private void х5ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _width = 5;
            _height = 5;
            GenerateField();
        }

        private void х5ToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            _width = 1;
            _height = 5;
            GenerateField();
        }
    }

    /// <summary>
    /// Наследуемый класс, добавляющий к кнопке поле бомбы.
    /// </summary>
    public class ButtonExented : Button // наследуемый класс, добавляющий к кнопке поле бомбы.
    {
        public bool IsBomb { get; set; }
    }
}
