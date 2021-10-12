using System;
using System.Windows;
using System.Windows.Controls;

namespace Calculator
{
	/// <summary>
	/// Логика взаимодействия для MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		string leftTop = ""; // Левый операнд
		string operation = ""; // Знак операции
		string rightTop = ""; // Правый операнд

		public MainWindow()
		{
			InitializeComponent();
			// Добавляем обработчик для всех кнопок на gride
			foreach(UIElement c in LayoutRoot.Children)
			{
				if(c is Button)
				{
					((Button)c).Click += ButtonClick;
				}
			}
		}

		private void ButtonClick(object sender, RoutedEventArgs e)
		{
			// Получаем текст кнопки
			string s = (string)((Button)e.OriginalSource).Content;
			// Добавляем его в текстовое поле
			textBlock.Text += s;
			int num;
			// Пытаемся преобразовать его в число
			bool result = Int32.TryParse(s, out num);
			// Если текст - это число
			if(result == true)
			{
				// Если операция не задана
				if(operation == "")
				{
					// Добавляем к левому операнду
					leftTop += s;
				}
				else
				{
					// Иначе к правому операнду
					rightTop += s;
				}
			}
			// Если было введено не число
			else
			{
				// Если равно, то выводи результат операции
				if(s == "=")
				{
					UpdateRightOp();
					textBlock.Text += rightTop;
					operation = "";
				}
				// Очищаем поле с переменными
				else if(s == "CLEAR")
				{
					leftTop = "";
					rightTop = "";
					operation = "";
					textBlock.Text = "";
				}
				// Получаем операцию
				else
				{
					// Если правый операнд уже имеется, то присваиваем его значение левому
					// операнду, а правый операнд очищаем
					if(rightTop != "")
					{
						UpdateRightOp();
						leftTop = rightTop;
						rightTop = "";
					}
					operation = s;
				}
			}
		}
		// Обновляем значение правого операнда
		private void UpdateRightOp()
		{
			int num1 = Int32.Parse(leftTop);
			int num2 = Int32.Parse(rightTop);
			// И выполняем операцию 
			switch(operation)
			{
				case "+":
					rightTop = (num1 + num2).ToString();
					break;
				case "-":
					rightTop = (num1 - num2).ToString();
					break;
				case "*":
					rightTop = (num1 * num2).ToString();
					break;
				case "/":
					rightTop = (num1 / num2).ToString();
					break;
			}
		}
	}
}
