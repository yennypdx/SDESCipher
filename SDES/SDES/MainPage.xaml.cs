using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace SDES
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private String keyOne;
        private String keyTwo;
        public MainPage()
        {
            this.InitializeComponent();
        }

        private void GenerateKeyBtn_Click(object sender, RoutedEventArgs e)
        {
            var generateKey = new KeyGenerator();

            var key = generateKey.GenerateKeys(CreateKeyBox.Text);
            keyOne = key.Substring(0, 8);
            keyTwo = key.Substring(8, 8);

            KeyOne.Text = keyOne;
            KeyTwo.Text = keyTwo;
        }

        private void GenerateEncryptTextBtn_Click(object sender, RoutedEventArgs e)
        {
            var encrypt = new EncryptionSDES();

            CipherText.Text = encrypt.EncryptionMode(keyOne, keyTwo, PlainTextBox.Text);
        }
    }
}
