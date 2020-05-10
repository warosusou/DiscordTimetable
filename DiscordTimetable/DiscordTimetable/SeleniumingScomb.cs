using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DiscordTimetable
{
    class SeleniumingScomb
    {
        private const string scomb = @"http://scomb.shibaura-it.ac.jp/";
        private ChromeDriver chrome;

        public SeleniumingScomb()
        {
            chrome = new ChromeDriver(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location));
        }

        public Dictionary<DayOfWeek, Dictionary<int, string>> GetTimeTable()
        {
            Dictionary<DayOfWeek, Dictionary<int, string>> table = null;
            try
            {
                chrome.Url = scomb;
                if (Login())
                {
                    chrome.FindElementById("navi-lms").Click();
                    chrome.FindElementById("displayMode0").Click();
                    var classes = chrome.FindElementsByClassName("classTitle");
                    var elements = new List<IReadOnlyCollection<IWebElement>>();
                    foreach(var c in classes)
                    {
                        elements.Add(c.FindElements(By.TagName("td")));
                    }
                    table = new Dictionary<DayOfWeek, Dictionary<int, string>>();
                    for (int i = 0;i < 6;i++)
                    {
                        var d = new Dictionary<int, string>();
                        for(int j = 0;j < 7;j++)
                        {
                            d.Add(j + 1, elements[j].ElementAt(i).Text);
                        }
                        table.Add((DayOfWeek)Enum.ToObject(typeof(DayOfWeek), i + 1), d);
                    }
                }
            }
            catch(Exception e)
            {
                MessageBox.Show(e.Message,"",MessageBoxButtons.OK,MessageBoxIcon.Error);
            }
            finally
            {
                chrome.Quit();
            }
            return table;
        }

        private bool Login()
        {
            chrome.FindElementByXPath("//a[@href='/portal/dologin?initialURI=']").Click();
            var result = MessageBox.Show("メインページを開いたらOKボタンを押してください","",MessageBoxButtons.OKCancel,MessageBoxIcon.Information);
            if(result == DialogResult.OK)
            {
                try
                {
                    var info = chrome.FindElementByXPath("//h2[@class='informationPersonal']");
                    return true;
                }
                catch(Exception)
                {
                    MessageBox.Show("ログインに失敗しました","",MessageBoxButtons.OK,MessageBoxIcon.Error);
                    return false;
                }
            }
            else
            {
                return false;
            }
        }
    }
}
