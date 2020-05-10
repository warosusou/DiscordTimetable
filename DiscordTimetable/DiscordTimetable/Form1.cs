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
        Dictionary<DayOfWeek, Dictionary<int, string>> table = null;
        const string path = "timeSchedule.json";
        public Form1()
        {
            InitializeComponent();
            LoadTimeSchedule();
        }

        private void GetTimeSchedule()
        {
            var s = new SeleniumingScomb();
            table = s.GetTimeTable();
            if(table != null)
            {
                File.WriteAllText(path, JsonConvert.SerializeObject(table));
            }
        }

        private void LoadTimeSchedule()
        {
            if (File.Exists(path))
            {
                var text = File.ReadAllText(path);
                table = JsonConvert.DeserializeObject<Dictionary<DayOfWeek, Dictionary<int, string>>>(text);
            }
            if (table == null)
            {
                var result = MessageBox.Show("時間割を読み込めませんでした 取得しますか", "", MessageBoxButtons.YesNo, MessageBoxIcon.Error);
                if (result == DialogResult.Yes)
                {
                    GetTimeSchedule();
                    LoadTimeSchedule();
                }
                else
                {
                    this.Close();
                }
            }
            timer1.Enabled = true;
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            var d = new DateTime(1, 1, 1, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
            for (int i = 0; i < TimeSchedule.StartingTimes.Count; i++)
            {
                var t = TimeSchedule.StartingTimes.ElementAt(i);
                if (d > t && t.AddMinutes(TimeSchedule.ClassInterval) > d)
                {
                    table.TryGetValue(DateTime.Now.DayOfWeek, out var dic);
                    dic.TryGetValue(i + 1, out var title);
                    label1.Text = title;
                }
            }
            timer1.Enabled = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            timer1.Enabled = false;
            GetTimeSchedule();
            if(table != null)
            {
                LoadTimeSchedule();
            }
        }
    }
}
