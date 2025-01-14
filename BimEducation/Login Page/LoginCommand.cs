using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BimEducation.Login_Page
{
    [Autodesk.Revit.Attributes.Transaction(Autodesk.Revit.Attributes.TransactionMode.Manual)]
    public class LoginCommand : IExternalCommand
    {
        public Result Execute(ExternalCommandData commandData, ref string message, ElementSet elements)
        {
            try
            {
                // Show Login Dialog
                LoginUI loginWindow = new LoginUI();
                loginWindow.ShowDialog();

#if false
                if (!loginWindow.IsAuthorized)
                {
                    TaskDialog.Show("Login Failed", "Invalid credentials. Tools remain disabled.");
                    return Result.Failed;
                } 
#endif

                // Enable all buttons after successful login
                RevitRibbonEngine.EnableRibbonButtons("BimEducation");

                TaskDialog.Show("Login Successful", "You now have access to all tools.");
                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                message = ex.Message;
                return Result.Failed;
            }

            
        }
    }
}
