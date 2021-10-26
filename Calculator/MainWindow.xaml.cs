using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Calculator
{
    public partial class MainWindow : Window
    {
        string leftop = ""; 
        string operation = "";
        string rightop = ""; 

        public MainWindow()
        {
            InitializeComponent();
            foreach (UIElement c in LayoutRoot.Children)
            {
                if (c is Button)
                {
                    ((Button)c).Click += Button_Click;
                }
            }
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            string s = (string)((Button)e.OriginalSource).Content;
            if (s != "=")
            {
                textBlock.Text += s;
            }
            double num;
            bool result = double.TryParse(s, out num);
            if (result == true)
            {
                if (rightop == "")
                {
                    if (operation == "")
                    {
                        leftop += s;
                    }
                    else
                    {
                        rightop += s;
                    }
                }
                else
                {
                    if (operation == "")
                    {
                        rightop = "";
                        leftop = s;
                        textBlock.Text = leftop;
                    }
                    else
                    {
                        rightop += s;
                    }
                }
            }
            else
            {
                if (s == "=" && rightop == "")
                {
                    rightop = leftop;
                    textBlock.Text = rightop;
                    operation = "";
                }
                else if (s == "=" && leftop == "")
                {
                    if (operation == "-")
                    {
                        leftop = "-" + rightop;
                        rightop = leftop;
                        textBlock.Text = rightop;
                        operation = "";
                    }
                    else
                    {
                        leftop = rightop;
                        textBlock.Text = rightop;
                        operation = "";
                    }
                }
                else if (s == "=")
                {
                    Update_rightop();
                    textBlock.Text = rightop;
                    operation = "";
                }
                else if (s == "ОЧИСТИТЬ")
                {
                    Clean();
                }
                else if (rightop == "Ошибка! Делить на ноль нельзя.")
                {
                    Clean();
                }
                else
                {
                    char[] lastSymbols = textBlock.Text.ToCharArray();
                    if (lastSymbols.Length > 1 && !int.TryParse(lastSymbols[lastSymbols.Length - 1] + "", out _) && !int.TryParse(lastSymbols[lastSymbols.Length - 2] + "", out _))
                    {
                        operation = s;
                        textBlock.Text = textBlock.Text.Substring(0, textBlock.Text.Length - 2) + operation;
                        return;
                    }
                    if (rightop != "" && rightop != "Ошибка! Делить на ноль нельзя.")
                    {
                        if (operation == "-")
                        {
                            leftop = "-" + rightop;
                            rightop = leftop;
                            operation = "";
                        }
                        else
                        {
                            leftop = rightop;
                            operation = "";
                        }
                        Update_rightop();
                        leftop = rightop;
                        rightop = "";
                    }
                    operation = s;
                }
            }
        }
        private void Update_rightop()
        {
            double firstop = double.Parse(leftop);
            double lastop = double.Parse(rightop);
            switch (operation)
            {
                case "+":
                    rightop = (firstop + lastop).ToString();
                    break;
                case "-":
                    rightop = (firstop - lastop).ToString();
                    break;
                case "*":
                    rightop = (firstop * lastop).ToString();
                    break;
                case "/":
                    if (lastop != 0)
                    {
                        rightop = (firstop / lastop).ToString();
                        break;
                    }
                    else
                    {
                        rightop = "Ошибка! Делить на ноль нельзя.";
                        break;
                    }
            }
        }
        private void Clean()
        {
            leftop = "";
            rightop = "";
            operation = "";
            textBlock.Text = "";
        }
    }
}
