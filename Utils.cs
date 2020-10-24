using Extension;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphWinForms
{
    public static class Utils
    {
        /// <summary>
        /// Allows to show basic Yes/No dialog
        /// </summary>
        /// <param name="ConfirmationText">Text shown within dialog</param>
        /// <param name="ConfirmationTitle">Text shown in dialog title</param>
        /// <param name="DefaultButton">Default selected dialog button</param>
        /// <returns>Returns <c>true</c> if user clicked Yes button and <c>false</c> otherwise</returns>
        public static bool Confirmation(string ConfirmationText, string ConfirmationTitle, MessageBoxDefaultButton DefaultButton = MessageBoxDefaultButton.Button1)
        {
            if (ConfirmationText == String.Empty || ConfirmationTitle == String.Empty)
                throw new ArgumentNullException();

            var ConfirmationResult = MessageBox.Show
            (
                ConfirmationText,
                ConfirmationTitle,
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Question,
                DefaultButton
            );

            if (ConfirmationResult == DialogResult.Yes) return true;
            return false;
        }
        /// <summary>
        /// Shows input dialog form
        /// </summary>
        /// <param name="inputDialogTitle">Dialog title</param>
        /// <param name="inputDialogText">Dialog text</param>
        /// <param name="Type">Dialog type</param>
        /// <returns>Line that user entered in dialog</returns>
        public static string ShowInputDialog(string inputDialogTitle, string inputDialogText, InputDialog.DialogType Type = InputDialog.DialogType.Text)
        {
            using (var inputDialog = new InputDialog(inputDialogTitle, inputDialogText, Type))
            {
                inputDialog.ShowDialog();
                return inputDialog.Line;
            }
        }
        /// <summary>
        /// Check if application has administrator rights
        /// </summary>
        /// <returns>Returns <c>true</c> if application has administrator right and <c>false</c> otherwise</returns>
        public static bool IsAdministrator()
        {
            WindowsIdentity identity = WindowsIdentity.GetCurrent();
            WindowsPrincipal principal = new WindowsPrincipal(identity);
            return principal.IsInRole(WindowsBuiltInRole.Administrator);
        }
        /// <summary>
        /// Associates application extension with files
        /// </summary>
        public static void AssociateExtension()
        {
            if (!FileAssociation.IsAssociated)
            {
                if (IsAdministrator())
                    FileAssociation.Associate("Graph Builder File", Application.ExecutablePath);
                else
                    MessageBox.Show
                    (
                        "Application has to be run with administrator rights to associate its file extension with itself.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly
                    );
            }
        }
        /// <summary>
        /// Unassociates application extension with files
        /// </summary>
        public static void UnAssociateExtension()
        {
            if (FileAssociation.IsAssociated)
            {
                if (IsAdministrator())
                    FileAssociation.Remove();
                else
                {
                    MessageBox.Show
                    (
                        "Application has to be run with administrator rights to associate its file extension with itself.",
                        "Information",
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Information,
                        MessageBoxDefaultButton.Button1,
                        MessageBoxOptions.DefaultDesktopOnly
                    );
                }
            }
        }
        /// <summary>
        /// Represents equation checking method
        /// </summary>
        /// <param name="firstObject">First object</param>
        /// <param name="secondObject">Second object</param>
        /// <returns>Return <c>true</c> if objects are equal and <c>false</c> otherwise</returns>
        public static bool Equals(object firstObject, object secondObject)
        {
            return JsonConvert.SerializeObject(firstObject) == JsonConvert.SerializeObject(secondObject);
        }
    }
}
