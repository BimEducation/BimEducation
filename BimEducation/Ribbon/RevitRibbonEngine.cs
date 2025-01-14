using Autodesk.Revit.UI;
using BimEducation.Login_Page;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media.Imaging;
using System.Xml.Linq;

namespace BimEducation
{
    public class RevitRibbonEngine : IExternalApplication
    {
         
        public Result OnShutdown(UIControlledApplication application)
        {
            throw new NotImplementedException();
        }

        public Result OnStartup(UIControlledApplication application)
        {
            try
            {
                RibbonStaticClas.rVTapplication = application as UIControlledApplication;   

                // Load the XML configuration file
                string xmlPath = Path.Combine(Directory.GetCurrentDirectory(), "BimEducationRibbon.xml");

                if (!File.Exists(xmlPath))
                    throw new FileNotFoundException("Ribbon configuration file not found.");

                XDocument ribbonXml = XDocument.Load(xmlPath);

                // Parse the XML to create the ribbon
                foreach (var tab in ribbonXml.Root.Elements("Tab"))
                {
                    string tabName = tab.Attribute("Name").Value;
                    string tabTitle = tab.Attribute("Title").Value;

                    //Here we are craeting Tab Name
                    application.CreateRibbonTab(tabName);

                    foreach (var panel in tab.Elements("Panel"))
                    {
                        string panelName = panel.Attribute("Name").Value;
                        string panelTitle = panel.Attribute("Title").Value;

                        //Here we are creating Panel Name
                        RibbonPanel ribbonPanel = application.CreateRibbonPanel(tabName, panelTitle);

                        foreach (var button in panel.Elements("Button"))
                        {
                            string buttonName = button.Attribute("Name").Value;
                            string buttonTitle = button.Attribute("Title").Value;
                            string buttonTooltip = button.Attribute("ToolTip").Value;
                            string buttonImage = button.Attribute("Image").Value;
                            string buttonCommand = button.Attribute("Command").Value;

                            //Here we are Creating the button
                            PushButtonData buttonData = new PushButtonData( buttonName,buttonTitle, Assembly.GetExecutingAssembly().Location, buttonCommand)
                            {
                                ToolTip = buttonTooltip,
                                LargeImage = LoadEmbeddedImage(buttonImage)
                            };
                            PushButton pushButton = ribbonPanel.AddItem(buttonData) as PushButton;
                            if (pushButton != null)
                            {
                                if (buttonName == "Login")
                                {
                                    pushButton.Enabled = true; 
                                }
                                else
                                {
                                    pushButton.Enabled = false; 
                                }
                            }

                            //ribbonPanel.AddItem(buttonData);
                        }
                    }
                }

                return Result.Succeeded;
            }
            catch (Exception ex)
            {
                TaskDialog.Show("Error", ex.Message);
                return Result.Failed;
            }
        }
        private bool ShowLoginDialog()
        {
            LoginUI loginWindow = new LoginUI();
            loginWindow.ShowDialog();

            // Validate user input
            return loginWindow.IsAuthorized;
        }

        private BitmapImage LoadEmbeddedImage(string resourcePath)
        {
            Assembly assembly = Assembly.GetExecutingAssembly();
            string fullPath = $"{assembly.GetName().Name}.{resourcePath.Replace("/", ".")}";
            using (Stream stream = assembly.GetManifestResourceStream(fullPath))
            {
                if (stream == null) return null;

                BitmapImage bitmap = new BitmapImage();
                //bitmap.BeginInit();
                bitmap.StreamSource = stream;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                //bitmap.EndInit();
                return bitmap;
            }
        }
        public static void EnableRibbonButtons( string tabName)
        {
            //UIControlledApplication VTapplication = rVTapplication.GetRibbonPanels(tabName);
            foreach (RibbonPanel panel in RibbonStaticClas.rVTapplication.GetRibbonPanels(tabName))
            {
                foreach (var item in panel.GetItems().OfType<PushButton>())
                {
                    item.Enabled = true;
                    //if (item.Enabled = false)
                    //{
                    //    item.Enabled = true; 
                    //}
                    //if (!item.Name.Equals("LoginButton", StringComparison.OrdinalIgnoreCase))
                    //{
                    //    item.Enabled = true;
                    //}
                }
            }
        }

    }
}

