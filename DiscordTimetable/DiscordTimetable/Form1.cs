using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordTimetable
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            var s = new SeleniumingScomb();
            var table = s.GetTimeTable();
            File.WriteAllText("test.json", JsonConvert.SerializeObject(table));
        }
    }
}
