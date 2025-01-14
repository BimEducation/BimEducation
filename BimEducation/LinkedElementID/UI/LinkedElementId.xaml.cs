using Autodesk.Revit.UI;
using BimEducation.LinkedElementID.Module;
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

namespace BimEducation.LinkedElementID
{
    /// <summary>
    /// Interaction logic for LinkedElementId.xaml
    /// </summary>
    public partial class LinkedElementIdUI : Window
    {
        public UIOperationLinkedElement UIOperationLinkedElement { get; set; }
        public ExternalEvent ExternalEventforRevit { get; set; }
        public LinkedElementIdUI(UIOperationLinkedElement uIOperationObj)
        {
            InitializeComponent();
            UIOperationLinkedElement = uIOperationObj;
            ExternalEventforRevit = ExternalEvent.Create(new ExternalEventforLinkedEleId(UIOperationLinkedElement, this));
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            //this.Close();
            //UIOperationLinkedElement.SelectButton(this);
            ExternalEventforRevit.Raise();
            //this.txIdofElement.Text.con
        }
    }
}
