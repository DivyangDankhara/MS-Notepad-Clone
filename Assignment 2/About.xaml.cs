/*
*	 FILE          : About.xaml.ca
*    PROJECT       : Assignment #2 Windows And Mobile Programming
*	 PROGRAMMER    : Divyangbhai Dankhara
*	 FIRST VERSION : 2018-10-08
*	 Description   : this file contain the the about class which will initiate when user press the about button on the screen
*/

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
using System.Windows.Shapes;

namespace Assignment_2
{
    /// <summary>
    /// Interaction logic for About.xaml
    /// </summary>
    public partial class About : Window
    {

        /* --FUNCTION HADER COMMENT--
        *	Function Name	:	About  this is constructor
        *	Parameters		:	None
        *	Description		:   initiate the object of the this class
        *	Return Value	:   None
        */
        public About()
        {
            InitializeComponent();
        }



        /* --FUNCTION HADER COMMENT--
        *	Function Name	:	Button_Click
        *	Parameters		:	object sender       : contain the information about who call this event
        *	                    RoutedEventArgs e   : 
        *	Description		: close the about window
        *	Return Value	: None
        */
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
