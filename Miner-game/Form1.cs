using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

// Компьютерная игра Miner/Сапёр. 
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
        private readonly int _bombPercent = 15;


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

                    button.xCoord = x;
                    button.yCoord = y;
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
                        //EmptyFieldButtonClick(clickedButton);
                        BombOpenRegion(clickedButton.xCoord, clickedButton.yCoord, clickedButton);
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

            for (int y = 0; y < _height; y++)
            {
                for (int x = 0; x < _width; x++)
                {
                    if (_buttons[x, y] == clickedButton)
                    {
                        // bombsAround = CountBombAround(x, y);
                        BombOpenRegion(x, y, clickedButton);
                    }
                }
            }

        }

        private void BombOpenRegion(int xCoord, int yCoord, ButtonExtented clickedButton) // метод для открытия соседний полей при клике
        {
            Queue<ButtonExtented> queue = new Queue<ButtonExtented>(); // создали очередь
            queue.Enqueue(clickedButton); // добавили в очередь кнопку

            while (queue.Count > 0)
            {
                ButtonExtented currentCell = queue.Dequeue(); // получаем первый элемент в очереди
                OpenCell(currentCell.xCoord, currentCell.yCoord, currentCell);

                if (CountBombAround(currentCell.xCoord, currentCell.yCoord) == 0)
                {
                    for (int y = currentCell.yCoord - 1; y <= currentCell.yCoord + 1; y++)// проход по полю 3х3 в рамках нажатой кнопки
                    {
                        for (int x = currentCell.xCoord - 1; x <= currentCell.xCoord + 1; x++)
                        {
                            if (x >= 0 && x < _width && y < _height && y >= 0)
                            {
                                if (!_buttons[x, y].WasAdded) // проверка если кнопка не была добавлена в очередь
                                {
                                    queue.Enqueue(_buttons[x, y]);
                                    _buttons[x, y].WasAdded = true;
                                }
                            }
                        }
                    }
                }
            }
        }

        private void OpenCell(int x, int y, ButtonExtented clickedButton)
        {
            int bombsAround = CountBombAround(x, y);

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
        public bool IsBomb { get; set; } // хранит инфу о том, бомба ли эта клетка или нет
        public bool IsClickable { get; set; } // хранит инфу можно ли нажать на кпопку или нет
        public bool WasAdded { get; set; } // хранит инфу о том была ли добавлена кнопка в очередь
        public int xCoord { get; set; } // позиция x клетки в двумерном массиве
        public int yCoord { get; set; }// позиция y клетки в двумерном массиве
    }
}
