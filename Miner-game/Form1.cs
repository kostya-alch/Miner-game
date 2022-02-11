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
        private readonly int _offset = 30;

        /// <summary>
        /// Процент бомб на карте.
        /// </summary>
        private readonly int _bombPercent = 30;


        /// <summary>
        /// Переменная проверяет первый клик в начале игры, чтобы не нарваться на бомбу.
        /// </summary>
        private bool _isFirstClick = true;

        /// <summary>
        /// Массив кнопок.
        /// </summary>
        private ButtonExtented[,] _buttons;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load_1(object sender, EventArgs e)
        {
            _buttons = new ButtonExtented[_width, _height];
            GenerateField();
        }

        private void GenerateField()
        {
            Random random = new Random(); // генерируем рандомные значения на полях

            for (int y = 0; y < _height; y++) // цикл создания кнопок
            {
                for (int x = 0; x < _width; x++)
                // цикл в котором мы заполняем игровое поле с рандомно взятыми значениями
                {
                    ButtonExtented button = new ButtonExtented(); // создание кнопки в форме.
                    button.Location = new Point(x * _offset, y * _offset); // присваиваем локацию кнопке
                    button.Size = new Size(_offset, _offset); // размер кнопки
                    button.IsClickable = true; // устанавливаем флаг true

                    if (random.Next(0, 100) <= _bombPercent) // рандомно присваиваем кнопке бомбу
                    {
                        button.IsBomb = true;
                    }

                    Controls.Add(button); // добавляем кнопки
                    button.MouseUp += new MouseEventHandler(FieldClick); // вешаем обработчик клика
                    _buttons[x, y] = button;
                }
            }
        }

        private void FieldClick(object sender, MouseEventArgs e)
        {
            ButtonExtented clickedButton = (ButtonExtented)sender;
            // если бомба - вызов функции взрыва, если нет - игра продолжается.
            if (e.Button == MouseButtons.Left && clickedButton.IsClickable) // если левая кнопка кликнута 
            {
                if (clickedButton.IsBomb)
                {
                    if (_isFirstClick) // проверка, чтобы не проиграть на первом ходу
                    {
                        clickedButton.IsBomb = false;
                        _isFirstClick = false;
                        EmptyFieldButtonClick(clickedButton);
                        // открытие соседней клетки
                    }
                    else // если не первый ход - то взрываемся
                    {
                        Explode();
                    }
                }
                else // если клетка реально пустая - идем даже
                {
                    EmptyFieldButtonClick(clickedButton);
                }
                _isFirstClick = false; // после первого хода флаг становится ложным в любом случае
            }
            if (e.Button == MouseButtons.Right) // нажатие правой кнопки мыши
            {
                clickedButton.IsClickable = !clickedButton.IsClickable; // переключение активности кнопки с помощью отрицания
                if (!clickedButton.IsClickable) clickedButton.Text = "B"; // отображение флага бомбы на поле 
                else clickedButton.Text = "";
            }
        }

        private void Explode() // логика проигрыша если нарвался на бомбу
        {
            foreach (ButtonExtented button in _buttons)
            {
                if (button.IsBomb)
                {
                    button.Text = "*";  // пробегаемся циклом по всем кнопкам, чтобы нарисовать бомбы
                }
            }
            MessageBox.Show("Вы проиграли");
            Application.Restart();
        }


        private void EmptyFieldButtonClick(ButtonExtented clickedButton) // логика если в клетке нету бомбы и игра продолжается
        {
            int bombsAround = 0;
            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_buttons[x, y] == clickedButton)
                    {
                        bombsAround = CountBombAround(x, y);
                    }
                }
            }
            if (bombsAround == 0)
            {

            }
            else
            {
                clickedButton.Text = "" + bombsAround;
            }
            clickedButton.Enabled = false;
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
    }

    /// <summary>
    /// Наследуемый класс, добавляющий к кнопке поле бомбы.
    /// </summary>
    public class ButtonExtented : Button // наследуемый класс, добавляющий к кнопке поле бомбы.
    {
        public bool IsBomb { get; set; } // поле, хранит инфу о том, бомба ли эта клетка или нет
        public bool IsClickable { get; set; } // поле, хранит инфу можно ли нажать на неё или нет
    }
}
