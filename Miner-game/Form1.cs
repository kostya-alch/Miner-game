using System;
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
        int width = 10;
        int height = 10;
        int distanceButtons = 35;
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            for(int x = 10; (x - 10) < width * distanceButtons; x+= distanceButtons) // цикл создания кнопок
            {
                for (int y = 10; (y - 10) < height * distanceButtons; y+= distanceButtons)
                {
                    Button button = new Button(); // создание кнопки в форме.
                    button.Location = new Point(x, y);
                    button.Size = new Size(30, 30);
                    Controls.Add(button);
                }
            }
        }
    }
}
