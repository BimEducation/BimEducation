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

        public Document LnkedRvtDocument { get; set; }

        public UIOperationLinkedElement(ExternalCommandData externalCommandData)
        {
            ExternalCommandData = externalCommandData;
            RvtDocument = ExternalCommandData.Application.ActiveUIDocument.Document;
            RvtSelection = ExternalCommandData.Application.ActiveUIDocument.Selection;
        }

        public void LinkedDocument()
        {
            var linkedDoc = new FilteredElementCollector(RvtDocument).OfClass(typeof(RevitLinkInstance));
            var t = linkedDoc.FirstOrDefault(); 
            if (t != null)
            {
                var l = t as RevitLinkInstance;
                LnkedRvtDocument = l.GetLinkDocument();
            }
        }

        public void SelectButton(LinkedElementIdUI uiObj)
        {
            var linkedRef = RvtSelection.PickObject(Autodesk.Revit.UI.Selection.ObjectType.LinkedElement, "Kindly select the Element from LinkedFile");

         
            var linkedInstance = RvtDocument.GetElement(linkedRef.ElementId) as RevitLinkInstance;
            var linkedDoc = linkedInstance.GetLinkDocument();
            var linkedElementId = linkedRef.LinkedElementId;
            var linkedElement = linkedDoc.GetElement(linkedElementId);

            if (linkedElement != null && uiObj?.txIdofElement != null && uiObj?.txNameofElement != null)
            {
                uiObj.txIdofElement.Text = linkedElement.Id.ToString();
                uiObj.txNameofElement.Text = linkedElement.Name;
            }
            else
            {
                TaskDialog.Show("Error", "Failed to retrieve linked element or update UI.");
            }


        }

    }
}
