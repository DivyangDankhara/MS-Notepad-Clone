/*
*	 FILE          : MainWindow.xaml.ca
*    PROJECT       : Assignment #2 Windows And Mobile Programming  (EditPad)
*	 PROGRAMMER    : Divyangbhai Dankhara
*	 FIRST VERSION : 2018-10-08
*	 Description   : this file contain background code for the Assignment 2 (Divyangbhai Editpad) Text editor application. this edit pad
*	                   provide functionality like open new file, open existing file, save file as well as some advance functionality like word wrap font dialog box.
*	                   this Application also contain code for about window which contain information about the application
*/

using System;
using System.Drawing;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.IO;
using Microsoft.Win32;
using System.Windows.Media.Imaging;
using System.Windows.Media;
using System.Windows.Forms;
using SaveFileDialog = Microsoft.Win32.SaveFileDialog;

namespace Assignment_2
{
    /// <summary>
    /// 
    /// </summary>
    public partial class MainWindow : Window
    {
        string currentFileName = "Untitled";            /// this string contain the file name for the current file
        string currentFilePath = "";                    /// this string contain the current file  which will help fill to store the file




        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	MainWindow
        *	Parameters		:   NONE
        *	Description		:   this is constructor method which initialize the MainWindow Class
        *	Return Value	:   None
        */
        public MainWindow()
        {
            InitializeComponent();
        }




        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	New
        *	Parameters		:   NONE
        *	Description		:   this is the new methode which will invoke when user press the new menu option to open new file
        *	Return Value	:   None
        */
        private void New()
        {

            if (String.IsNullOrEmpty(currentFilePath))
            {
                // File has never been saved, must prompt for name
                Save();
            }
            TextArea.Text = "";
            currentFileName = "Untitled";
            currentFilePath = null;
            UpdateCharacterCounter();
            UpdateWindowInfo();
        }





        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	Open
        *	Parameters		:   NONE
        *	Description		:   this is the open method which will provide the code which will open a open file dialog box from which user can open existing file
        *	Return Value	:   None
        */
        private void Open()
        {
            // Configure dialog box
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.FileName = currentFileName; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Open document
                currentFilePath = dlg.FileName;
                currentFileName = new FileInfo(currentFilePath).Name;
                using (TextReader tr = new StreamReader(currentFilePath))
                {
                    TextArea.Text = tr.ReadToEnd();
                }
                UpdateWindowInfo();
                UpdateCharacterCounter();
            }
        }




        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	Save
        *	Parameters		:   NONE
        *	Description		:   this function is used to save the current file in which user was typing and this function will open a save file dialog box and ask user that where they want to store this file
        *	Return Value	:   None
        */
        private void Save()
        {
            // Configure dialog box
            Microsoft.Win32.SaveFileDialog dlg = new SaveFileDialog();
            dlg.FileName = currentFileName; // Default file name
            dlg.DefaultExt = ".txt"; // Default file extension
            dlg.Filter = "Text documents (.txt)|*.txt"; // Filter files by extension

            // Show open file dialog box
            Nullable<bool> result = dlg.ShowDialog();

            // Process open file dialog box results
            if (result == true)
            {
                // Save document
                currentFilePath = dlg.FileName;
                using (TextWriter tr = new StreamWriter(currentFilePath))
                {
                    tr.Write(TextArea.Text);
                }
                UpdateWindowInfo();
                UpdateCharacterCounter();
            }
        }




        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	MenuItem_Click
        *	Parameters		:   object sender       :   store the name of the menu item which invoke this event handler
        *	                    RoutedEventArgs e   :   contain the RoutedEventArgs
        *	Description		:   this function is used to save the current file in which user was typing and this function will open a save file dialog box and ask user that where they want to store this file
        *	Return Value	:   None
        */
        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            System.Windows.Controls.MenuItem item = (System.Windows.Controls.MenuItem)sender;

            switch (item.Name)
            {
                case "NewFile":
                    New();              /// case identified that user selected the 'new' Manu item so it will call the New() function to perform tasks'
                    break;

                case "FileOpen":
                    Open();             /// case identified that user selected the 'Open' menu item so it will call Open() to perform file opening task
                    break;

                case "SaveAs":
                    Save();             /// case identified that user selected the 'SaveAs' menu item so it will call Save() to perform file saving task
                    break;

                case "WordWrap":
                    WrapText();         /// case identified that user selected the 'WordWrap' menu item so it will call wrapText() to perform textwraping task
                    break;

                case "Font":
                    ChangeFont();           /// case identified that user selected the 'Font' menu item so it will call font() to change font and size of task
                    break;

                case "About":
                    ShowAboutWindow();         ///case identified that user selected the 'about' menu item so it will call ShowAboutWindow() to open about window task
                    break;

                case "Close":
                    EditPad.Close();            ///case identified that used click on close icon so here program will exit
                    break;
            }

        }


        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	ShowAboutWindow
        *	Parameters		:   None
        *	Description		:   create a object of about class and initiate that about and show about window
        *	Return Value	:   None
        */
        private void ShowAboutWindow()
        {
            About ab = new About();
            ab.Owner = System.Windows.Application.Current.MainWindow;
            ab.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            ab.ShowDialog();
        }


        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	WrapText
        *	Parameters		:   None
        *	Description		:   turn on and off the wraptext function for the TextArea
        *	Return Value	:   None
        */
        private void WrapText()
        {
            if (TextArea.TextWrapping == TextWrapping.Wrap)
            {
                ///turn of the text wrapping
                TextArea.TextWrapping = TextWrapping.NoWrap;

                /// remove the image which indicate that wrapping is on
                WordWrap.Icon = null;
            }
            else
            {
                ///wrap all the text into the TextArea
                TextArea.TextWrapping = TextWrapping.Wrap;

                /// Adding image left side of menu to indicate that wrap option is on
                System.Windows.Controls.Image i = new System.Windows.Controls.Image();
                BitmapImage src = new BitmapImage();
                src.BeginInit();
                src.UriSource = new Uri("Selected.png", UriKind.Relative);
                src.EndInit();
                i.Source = src;
                WordWrap.Icon = i;
            }
        }



        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	UpdateWindowInfo
        *	Parameters		:   None
        *	Description		:   update the window information like title
        *	Return Value	:   None
        */
        private void UpdateWindowInfo()
        {
            EditPad.Title = currentFileName + " - " + "Divyangbhai EditPad";
        }




        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	TextArea_TextChanged
        *	Parameters		:   object sender           :contain information about the who inivoke this event handler
        *	                    TextChangedEventArgs    : 
        *	Description		:   call the updatecharactercounter function to set the current character counter
        *	Return Value	:   None
        */
        private void TextArea_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateCharacterCounter();
        }




        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	UpdateCharacterCounter
        *	Parameters		:   NOne
        *	Description		:   count that how many character user have written in the text area in this application    
        *	Return Value	:   print the counter on bottom of the app
        */
        private void UpdateCharacterCounter()
        {
            int totalCharacters = 0;           
            string text = TextArea.Text;
            foreach(char c in text)
            {
                if(c == '\n' || c== '\r')
                {
                    continue;
                }
                totalCharacters++;
            }
            TextCounterArea.Text = String.Format("Character Count: {0}", totalCharacters);
        }




        /*! --FUNCTION HADER COMMENT--
        *	Function Name	:	ChangeFont
        *	Parameters		:   NOne
        *	Description		:   open the font dialog and allow user to change the font style font family for the text area   
        *	Return Value	:   none
        */
        private void ChangeFont()
        {
            FontDialog fd = new FontDialog();
            if(fd.ShowDialog() == System.Windows.Forms.DialogResult.OK)
            {
                TextArea.FontFamily= new System.Windows.Media.FontFamily(fd.Font.Name);
                TextArea.FontSize = fd.Font.Size * 96.0 / 72.0;
                TextArea.FontWeight = fd.Font.Bold ? FontWeights.Bold : FontWeights.Regular;
                TextArea.FontStyle = fd.Font.Italic ? FontStyles.Italic : FontStyles.Normal;

            }
        }

       /// end the program

    }
}
