using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace TaskManager.Client.Forms
{
	/// <summary>
	/// Interaction logic for Info.xaml
	/// </summary>
	public partial class Info : Window
	{
		public Info()
		{
			InitializeComponent();
		}

		private void BtnCloseClick(object sender, RoutedEventArgs e)
		{
			this.Close();
		}
	}
}
