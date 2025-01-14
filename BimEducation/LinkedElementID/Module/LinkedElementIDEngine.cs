using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using BimEducation.LinkedElementID.Module;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimEducation.LinkedElementID
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class LinkedElementIDEngine : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            var documentObj = commandData.Application.ActiveUIDocument.Document;
            var selectionObj = commandData.Application.ActiveUIDocument.Selection;

#if false
            var linkedRef = selectionObj.PickObject(Autodesk.Revit.UI.Selection.ObjectType.LinkedElement, "Kindly select the Element from LinkedFile");

            var linkedElement = documentObj.GetElement(linkedRef).Id; 
#endif

            var uIOperationObj = new UIOperationLinkedElement(commandData);

            var linkedElementIdUIObj = new LinkedElementIdUI(uIOperationObj);
            linkedElementIdUIObj.DataContext = uIOperationObj;
            linkedElementIdUIObj.Show();

            //TaskDialog.Show("Message", "Tool is in Under Construction!");
            return Result.Succeeded;
        }
    }
}
