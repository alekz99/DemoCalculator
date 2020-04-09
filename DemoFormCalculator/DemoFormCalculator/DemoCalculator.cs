using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DemoFormCalculator
{
    public partial class DemoCalculator : Form
    {
        Double resultValue = 0;
        String operation = "";
        bool isOperationPerfomed = false;

        public DemoCalculator()
        {
            InitializeComponent();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            textBox.Text = "0";
            resultValue = 0.0d;
            labelTime.Text = "";
        }

        private void equalsButton_Click(object sender, EventArgs e)
        {
            /*
             * Обновляем метку
             * Выбираем необходимое действие
             * Запоминаем результат для дальнейших вычислений
             */
            labelTime.Text = labelTime.Text + " " + textBox.Text + " =";
            switch (operation)
            {
                case "+":
                    textBox.Text = (resultValue + Double.Parse(textBox.Text)).ToString();
                    break;
                case "-":
                    textBox.Text = (resultValue - Double.Parse(textBox.Text)).ToString();
                    break;
                case "/":
                    if (Double.Parse(textBox.Text) == 0.0)
                    {
                        textBox.Text = "0";
                    } else
                    {
                        textBox.Text = (resultValue / Double.Parse(textBox.Text)).ToString();
                    }
                    
                    break;
                case "*":
                    textBox.Text = (resultValue * Double.Parse(textBox.Text)).ToString();
                    break;
                default:
                    break;
            }
            resultValue = Double.Parse(textBox.Text);
            Clipboard.SetText(textBox.Text);
        }

        private void operator_Click(object sender, EventArgs e)
        {
            /*
             * Создаем обработчик событий
             * Если есть значение,поле метки непустое и последнее действие это не "=", то вызываем кнопку "=", обновляем метку
             * Иначе запоминаем первое слагаемое/делимое/множитель, обновляем метку
             */
            Button button = (Button)sender;
            if (resultValue != 0 && labelTime.Text != "" && !labelTime.Text.Contains("="))
            {
                equalsButton.PerformClick();
                operation = button.Text;
                labelTime.Text = resultValue + " " + operation;
                isOperationPerfomed = true;
            } else
            {
                operation = button.Text;
                resultValue = Double.Parse(textBox.Text);
                isOperationPerfomed = true;
                labelTime.Text = resultValue + " " + operation;
            }
        }

        private void button_Click(object sender, EventArgs e)
        {
            /*
             * Если метка сожержит "=", то при нажатии цифры вызовем кнопку "CE"
             * Если была выполнена операция или поле содержит ноль, то очищаем поле
             * Создаем обработчик событий
             * Не даем пользователю возможность добавить много запятых
             */
            if (labelTime.Text.Contains("="))
            {
                clearButton.PerformClick();
            }
            if (isOperationPerfomed || textBox.Text == "0")
                textBox.Clear();
            isOperationPerfomed = false;
            Button button = (Button)sender;
            if (button.Text == ",")
            {
                if (!textBox.Text.Contains(","))
                {
                    textBox.Text = textBox.Text + ",";
                }
            } else
            {
                textBox.Text = textBox.Text + button.Text;
            }
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            char number = e.KeyChar;
            //нельзя добавить много запятых
            if (textBox.Text.Contains(",") && number == 44)
            {
                e.Handled = true;
            }
            //цифры, клавиша BackSpace и запятая а ASCII
            if ((e.KeyChar <= 47 || e.KeyChar >= 58) && number != 8 && number != 44) 
            {
                e.Handled = true;
            }
        }
    }
}
