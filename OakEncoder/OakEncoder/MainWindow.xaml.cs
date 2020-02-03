using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
//using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.IO;

namespace OakEncoder
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        Encoder encoder;
        EncodeParametersModel model = new EncodeParametersModel();

        public MainWindow()
        {
            InitializeComponent();

            this.gridLayout.DataContext = model;
            //LblPath.DataContext = model;
            //SetPlaceholder(txtNewFileName, "Enter output file name here");
            SetPlaceholder(txtBoxPassword, "Enter password integer here");
            //txtBoxPassword.DataContext = encoder;
        }

        private void BtnBrowse_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Forms.OpenFileDialog dialog = new System.Windows.Forms.OpenFileDialog();
            dialog.InitialDirectory = Directory.GetCurrentDirectory();

            if(dialog.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                model.SetFilePath(dialog.FileName);
                //model.FilePath = dialog.FileName;
                //txtNewFileName.Text = 
                //if(encoder)
                //encoder.SetPath(dialog.FileName);
            }
        }

        private static void SetPlaceholder(TextBox txtbox, string placeholder)
        {
            Action a = () =>
            {
                if (string.IsNullOrWhiteSpace(txtbox.Text))
                {
                    txtbox.Text = placeholder;
                    txtbox.Foreground = Brushes.Gray;
                }
            };

            txtbox.LostFocus += (object sender, RoutedEventArgs e) =>
            {
                a();
            };
            a();

            txtbox.GotFocus += (sender, e) =>
            {
                if (txtbox.Text == placeholder)
                    txtbox.Text = "";
                    txtbox.Foreground = Brushes.Black;
            };
        }

        private void BtnEncode_Click(object sender, RoutedEventArgs e)
        {
            this.encoder?.Dispose();
            this.encoder = new Encoder(model.FilePath, model.OutputPath,  model.KeyInt);
            this.encoder.Start(
                () => MessageBox.Show("Done!")
            );
        }
    }
}
