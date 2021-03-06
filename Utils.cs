﻿using Extension;
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
        /// Random unit
        /// </summary>
        private static Random Random { get; } = new Random();
        /// <summary>
        /// Generates random double number between two stated double values
        /// </summary>
        /// <param name="firstNumber">First value</param>
        /// <param name="secondNumber">Second value</param>
        /// <returns>Random double number between two stated double values</returns>
        public static double Rand(double firstNumber, double secondNumber)
        {
            return Random.NextDouble() * (secondNumber - firstNumber) + firstNumber;
        }
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
            if (!IsAdministrator())
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
                return;
            }
            if (!FileAssociation.IsAssociated)
            {
                FileAssociation.Associate("Graph Builder File", Application.ExecutablePath);
            }
        }
        /// <summary>
        /// Unassociates application extension with files
        /// </summary>
        public static void UnAssociateExtension()
        {
            if (!IsAdministrator())
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
                return;
            }
            if (FileAssociation.IsAssociated)
            {
                FileAssociation.Remove();
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
        /// <summary>
        /// Represents equation checking method
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="firstObject">First object</param>
        /// <param name="secondObject">Second object</param>
        /// <returns>Return <c>true</c> if objects are equal and <c>false</c> otherwise</returns>
        public static bool Equals<T>(T firstObject, T secondObject) where T : IComparable<T>
        {
            return firstObject.CompareTo(secondObject) == 0;
        }
        /// <summary>
        /// Find mimimal value in stated sequence
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="Collection">Sequence of values</param>
        /// <returns>Minimal value</returns>
        public static T FindMinValue<T>(params T[] Collection) where T : IComparable<T>
        {
            return Collection.Aggregate((a, b) => b.CompareTo(a) > 0 ? a : b);
        }
        /// <summary>
        /// Find mimimal value in collection
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="Collection">Collection</param>
        /// <returns>Minimal value</returns>
        public static T FindMinValue<T>(IEnumerable<T> Collection) where T : IComparable<T>
        {
            return Collection.Aggregate((a, b) => b.CompareTo(a) > 0 ? a : b);
        }
        /// <summary>
        /// Find maximal value in stated sequence
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="Collection">Sequence of values</param>
        /// <returns>Maximal value</returns>
        public static T FindMaxValue<T>(params T[] Collection) where T : IComparable<T>
        {
            return Collection.Aggregate((a, b) => a.CompareTo(b) > 0 ? a : b);
        }
        /// <summary>
        /// Find maximal value in collection
        /// </summary>
        /// <typeparam name="T">Any comparable type</typeparam>
        /// <param name="Collection">Collection</param>
        /// <returns>Maximal value</returns>
        public static T FindMaxValue<T>(IEnumerable<T> Collection) where T : IComparable<T>
        {
            return Collection.Aggregate((a, b) => a.CompareTo(b) > 0 ? a : b);
        }
    }
}