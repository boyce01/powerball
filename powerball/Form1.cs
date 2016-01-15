using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace powerball
{
    public partial class pbForm : Form
    {
        public pbForm()
        {
            InitializeComponent();
        }

        private void pbForm_Load(object sender, EventArgs e)
        {
            lottoTxt.Text = "请随机选择Lotto";
            pbTxt.Text = "请随机选择Powerball";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lottoTxt.Text = "";
            //1~40随机选6，并从小到达排列，中间以"-"隔开
            ArrayList list = new ArrayList(); //要添加using System.Collections命名空间
            Random r = new Random();
            for (int i = 0; i < 6; i++)
            {
                int lottoInt = r.Next(1, 41);
                if (list.Contains(lottoInt)) //Contains()方法:判断ArrayList数组中是否存在某个元素
                {
                    i--; //若数组中存在该元素，则i自动减1，本次循环不算    
                }
                else //数组中不存在该随机数
                {
                    list.Add(lottoInt); //把该随机数添加到数组中
                }
            }
            //把该数组从小到大排列
            list.Sort();

            //依次把数组中元素赋值给一个变量，并在每个数字用-连接
            for (int i = 0; i < list.Count; i++)
            {
                lottoTxt.Text = lottoTxt.Text + list[i].ToString() + "-";
            }
            lottoTxt.Text = lottoTxt.Text.Substring(0, lottoTxt.Text.Length - 1);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            pbTxt.Text = r.Next(1, 11).ToString();
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }


    }
}
