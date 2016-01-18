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



        /// <summary>
        /// 调用LottoNumbers方法，来随机选择数字
        /// </summary>
        /// <returns>6 random numbers </returns>
        private static string LottoNumbers() //static静态方法，方便调用，但效率不高
        {
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
            string lottoNumbers = "";
            for (int i = 0; i < list.Count; i++)
            {
                lottoNumbers = lottoNumbers + list[i].ToString() + "-";
            }
            return lottoNumbers.Substring(0, lottoNumbers.Length - 1);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            lottoTxt.Text = "";
            lottoTxt.Text = pbForm.LottoNumbers(); //调用LottoNumbers()方法，产生lotto号码
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Random r = new Random();
            pbTxt.Text = r.Next(1, 11).ToString();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            lottoTxt.Text = "";
            lottoTxt.Text = pbForm.LottoNumbers();
            Random r = new Random();
            pbTxt.Text = r.Next(1, 11).ToString();
        }

        /// <summary>
        /// whn click this btn, change to "select no. page",but sec click, change "random page"
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void selectBtn_Click(object sender, EventArgs e)
        {
            if (selectBtn.Text == "Select numbers by hand") //show select no. page
            {
                groupBox1.Visible = true;
                selectBtn.Text = "Select numbers at random"; //change btn content
            }
            else //show random page
            {
                groupBox1.Visible = false;
                selectBtn.Text = "Select numbers by hand"; //change btn content
            }

        }


        /// <summary>
        /// select no. as ppl wish
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button6_Click(object sender, EventArgs e)
        {
            /* 1)judge if all txt have values or nt?
             * 2)select no. at random but no. filled by user
             */

            if (string.IsNullOrEmpty(txt1.Text.Trim()) && string.IsNullOrEmpty(txt2.Text.Trim()) &&
                string.IsNullOrEmpty(txt3.Text.Trim()) && string.IsNullOrEmpty(txt4.Text.Trim()) &&
                string.IsNullOrEmpty(txt5.Text.Trim()) && string.IsNullOrEmpty(txt6.Text.Trim()) &&
                string.IsNullOrEmpty(txt7.Text.Trim())) //all txt boxes have no values
            {   //string.IsNullOrEmpty(txt1.Text.Trim())判断是否为空值(inc空格，回车)
                MessageBox.Show("Please inpute a number at least");
            }
            else //a txt has val at least
            {
                ArrayList list = new ArrayList();
                Random r = new Random();
                //如果有值，则提取值，再随机选出剩下的数
                if (!string.IsNullOrEmpty(txt1.Text.Trim()))
                {
                    TestRange(txt1.Text, list);
                }
                else if (!string.IsNullOrEmpty(txt2.Text.Trim()))
                {
                    TestRange(txt2.Text, list);
                }
                else if (!string.IsNullOrEmpty(txt3.Text.Trim()))
                {
                    TestRange(txt3.Text, list);
                }
                else if (!string.IsNullOrEmpty(txt4.Text.Trim()))
                {
                    TestRange(txt4.Text, list);
                }
                else if (!string.IsNullOrEmpty(txt5.Text.Trim()))
                {
                    TestRange(txt5.Text, list);
                }
                else if (!string.IsNullOrEmpty(txt6.Text.Trim()))
                {
                    TestRange(txt6.Text, list);
                }
                else if (string.IsNullOrEmpty(txt7.Text.Trim())) //powerball is null
                {
                    txt7.Text = r.Next(1, 11).ToString();
                }
                else if (!string.IsNullOrEmpty(txt7.Text.Trim())) //powerball has value, judge range:1~10
                {
                    if (Convert.ToInt32(txt7.Text.Trim()) < 1 || Convert.ToInt32(txt7.Text.Trim()) > 10)
                    {
                        MessageBox.Show("Please input a number from 1 to 10");
                        Clear();
                        return;
                    }
                }

                //judge how many elements is in the list
                switch (list.Count)
                {
                    case 0: SelectNumbers(6, list, r);
                        break;
                    case 1: SelectNumbers(5, list, r);
                        break;
                    case 2: SelectNumbers(4, list, r);
                        break;
                    case 3: SelectNumbers(3, list, r);
                        break;
                    case 4: SelectNumbers(2, list, r);
                        break;
                    case 5: SelectNumbers(1, list, r);
                        break;
                    case 6: SelectNumbers(0, list, r); //if user input 6 no., the no. r ranked
                        break;
                }

            }// 1st else
        }

        /// <summary>
        /// select no. as user wish
        /// </summary>
        /// <param name="times">cirle times</param>
        /// <param name="list">arraylist to store numbers</param>
        /// <param name="r">random per</param>
        private void SelectNumbers(int times, ArrayList list, Random r)
        {
            for (int i = 0; i < times; i++) //不能写：i<6-list.Count;因为每次抽取随机数，list.Count的值都会+1
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
            txt1.Text = list[0].ToString();
            txt2.Text = list[1].ToString();
            txt3.Text = list[2].ToString();
            txt4.Text = list[3].ToString();
            txt5.Text = list[4].ToString();
            txt6.Text = list[5].ToString();
        }

        
        private void clearBtn_Click(object sender, EventArgs e)
        {
            Clear();
        }

        /// <summary>
        /// clear all txt boxes
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Clear()
        {
            txt1.Text = null;
            txt2.Text = null;
            txt3.Text = null;
            txt4.Text = null;
            txt5.Text = null;
            txt6.Text = null;
            txt7.Text = null;
        }

        /// <summary>
        /// 检验用户的输入是否达标
        /// </summary>
        /// <param name="txt"></param>
        /// <param name="list"></param>
        private void TestRange(string txt, ArrayList list)
        {
            if (Convert.ToInt32(txt.Trim()) > 0 && Convert.ToInt32(txt.Trim()) < 41)
            {
                list.Add(int.Parse(txt1.Text));
            }
            else
            {
                MessageBox.Show("Please input a number from 1 to 40");
                Clear();
                return;
            }
        }

        #region method by myself
        /// <summary>
        /// select no. as user input
        /// </summary>
        /// <param name="list">ArrayList</param>
        /// <param name="r">random number</param>
        //private static void SelectedNo(ArrayList list, Random r)
        //{
        //    for (int i = 0; i < 6 - list.Count; i++)
        //    {
        //        int lottoInt = r.Next(1, 41);
        //        if (list.Contains(lottoInt)) //Contains()方法:判断ArrayList数组中是否存在某个元素
        //        {
        //            i--; //若数组中存在该元素，则i自动减1，本次循环不算    
        //        }
        //        else //数组中不存在该随机数
        //        {
        //            list.Add(lottoInt); //把该随机数添加到数组中
        //        }
        //    }
        //    //把该数组从小到大排列
        //    list.Sort();
        //    txt1.Text = list[0].ToString();
        //    txt2.Text = list[1].ToString();
        //    txt3.Text = list[2].ToString();
        //    txt4.Text = list[3].ToString();
        //    txt5.Text = list[4].ToString();
        //    txt6.Text = list[5].ToString();
        //}
        #endregion

    }
}
