using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace SmartCheckers1._0
{
    public partial class SmartCheckers : Form
    {
        int color = 1;
        int difficulty = 1;

        public SmartCheckers()
        {
            InitializeComponent();
        }

        private void SmartCheckers_Load(object sender, EventArgs e)
        {
            difCmb.SelectedIndex = 0;
        }

        private void startBtn_Click(object sender, EventArgs e)
        {
            if (blackRB.Checked)
            {
                this.color = 1;
            }
            else if (whiteRB.Checked)
            {
                this.color = -1;
            }

            this.difficulty = difCmb.SelectedIndex + 1;

            System.Threading.Thread thStart = new System.Threading.Thread(StartGame);
            thStart.Start();
            this.Dispose();
        }

        private void StartGame()
        {
            Game1 game = new Game1(false, this.color, this.difficulty);
            game.Run();
        }
    }
}
