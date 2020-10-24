using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GraphWinForms
{
    /// <summary>
    /// Listbox API Class
    /// </summary>
    public static class DisplayList
    {
        /// <summary>
        /// Listbox handler
        /// </summary>
        private static ListBox Display { get; set; }
        /// <summary>
        /// Index of selected list item
        /// </summary>
        public static int SelectedIndex => Display.SelectedIndex;
        /// <summary>
        /// Selected item
        /// </summary>
        public static object SelectedItem => Display.SelectedItem;
        /// <summary>
        /// List items
        /// </summary>
        public static ListBox.ObjectCollection Items => Display.Items;
        /// <summary>
        /// Check if listbox is empty
        /// </summary>
        public static bool IsEmpty => Display.Items.Count == 0;
        /// <summary>
        /// Listbox items count
        /// </summary>
        public static int ItemsCount => Display.Items.Count;
        /// <summary>
        /// Set listbox handler
        /// </summary>
        /// <param name="display">Listbox handler</param>
        public static void SetDisplayHandler(ListBox display)
        {
            if (display != null) Display = display;
            
            Display.SelectedIndexChanged += (s, e) =>
            {
                if (SelectedIndex != -1)
                {
                    DrawGraph.HighlightPath
                    (
                        GetItem<GraphPath>().StringPath,
                        DefaultSettings.PathColor,
                        DefaultSettings.PathBeginColor,
                        DefaultSettings.PathEndColor
                    );
                }
            };
        }
        /// <summary>
        /// Allows to add any object to list
        /// </summary>
        /// <param name="item">Object</param>
        public static void AddItem(object item)
        {
            if (item != null)
            {
                Items.Add(item);
            }
        }
        /// <summary>
        /// Allows to get selected item with type casting
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <returns>Converted to stated type object or default value of stated type</returns>
        public static T GetItem<T>()
        {
            if (Display.SelectedItem != null)
            {
                try { return (T)SelectedItem; } catch { }
            }
            return default(T);
        }
        /// <summary>
        /// Allows to get selected item with type casting
        /// </summary>
        /// <typeparam name="T">Any type</typeparam>
        /// <param name="itemIndex">Index of list item</param>
        /// <returns>Converted to stated type object or default value of stated type</returns>
        public static T GetItem<T>(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < ItemsCount)
            {
                try { return (T)Items[itemIndex]; } catch { }
            }
            return default(T);
        }
        /// <summary>
        /// Remove item at its index position in list
        /// </summary>
        /// <param name="itemIndex">Index of list item</param>
        public static void RemoveItem(int itemIndex)
        {
            if (itemIndex >= 0 && itemIndex < ItemsCount)
            {
                Items.RemoveAt(itemIndex);
            }
        }
        /// <summary>
        /// Remove item by object
        /// </summary>
        /// <param name="obj">Object to remove</param>
        public static void RemoveItem(object obj)
        {
            Items.Remove(obj);
        }
        /// <summary>
        /// Set selected index to -1
        /// </summary>
        public static void LoseFocus()
        {
            Display.SelectedIndex = -1;
        }
        /// <summary>
        /// Clear item list
        /// </summary>
        public static void Clear()
        {
            Display.Items.Clear();
        }
    }
}
