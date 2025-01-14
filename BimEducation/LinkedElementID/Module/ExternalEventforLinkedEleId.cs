using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimEducation.LinkedElementID.Module
{
    public class ExternalEventforLinkedEleId : IExternalEventHandler
    {
        public UIOperationLinkedElement uIOperationLinkedElementObj {  get; set; } 

        public LinkedElementIdUI linkedElementIdUIObj { get; set; }
        public ExternalEventforLinkedEleId(UIOperationLinkedElement extrnUIOpObj, LinkedElementIdUI UIObj)
        {
            uIOperationLinkedElementObj = extrnUIOpObj;
            linkedElementIdUIObj = UIObj;   
        }
        public void Execute(UIApplication app)
        {
            uIOperationLinkedElementObj.SelectButton(linkedElementIdUIObj);
        }

        public string GetName()
        {
            return "";
        }
    }
}
