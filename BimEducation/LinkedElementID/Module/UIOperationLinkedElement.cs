using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Selection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimEducation.LinkedElementID.Module
{
    public class UIOperationLinkedElement
    {
        public ExternalCommandData ExternalCommandData { get; set; }

        public Selection RvtSelection { get; set; }

        public Document RvtDocument { get; set; }
        
        public UIOperationLinkedElement(ExternalCommandData externalCommandData)
        {
            ExternalCommandData = externalCommandData;
            RvtDocument = ExternalCommandData.Application.ActiveUIDocument.Document;
            RvtSelection = ExternalCommandData.Application.ActiveUIDocument.Selection;
        }

        public void SelectButton(LinkedElementIdUI uiObj)
        {
            var linkedRef = RvtSelection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.LinkedElement, "Kindly select the Element from LinkedFile");

            var linkedElement = RvtDocument.GetElement(linkedRef).Id;

            uiObj.txIdofElement.Text = linkedElement.ToString();
            //uiObj.txIdofElement.Text.
        }

    }
}
