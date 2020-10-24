using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphWinForms
{
    /// <summary>
    /// Default variables class
    /// </summary>
    static public class DefaultSettings
    {
        /// <summary>
        /// Default vertex radius
        /// </summary>
        public const int VertexRadius = 20;
        /// <summary>
        /// Default vertex radius expansion
        /// </summary>
        public const int VertexRadiusExpansion = 100;
        /// <summary>
        /// Default label width modifier
        /// </summary>
        public const int LabelWidthModifier = 5;
        /// <summary>
        /// Default label height modifier
        /// </summary>
        public const int LabelHeightModifier = 4;
        /// <summary>
        /// Default color
        /// </summary>
        public static Color Color { get; private set; } = Color.Black;
        /// <summary>
        /// Default selection color
        /// </summary>
        public static Color SelectionColor { get; private set; } = Color.Red;
        /// <summary>
        /// Default path color
        /// </summary>
        public static Color PathColor { get; private set; } = Color.DeepSkyBlue;
        /// <summary>
        /// Default color for the first vertex on the path
        /// </summary>
        public static Color PathBeginColor { get; private set; } = Color.LawnGreen;
        /// <summary>
        /// Default color for the last vertex on the path
        /// </summary>
        public static Color PathEndColor { get; private set; } = Color.Tomato;
        /// <summary>
        /// Default font for vertex labels
        /// </summary>
        public static Font VertexFont { get; private set; } = new Font("Consolas", 9);
        /// <summary>
        /// Default font for edge labels
        /// </summary>
        public static Font EdgeFont { get; private set; } = new Font("Consolas", 11);
        /// <summary>
        /// Default string format for labels
        /// </summary>
        public static StringFormat StringFormat { get; private set; } = new StringFormat();
        /// <summary>
        /// Default variables initializator
        /// </summary>
        /// <remarks>
        /// You can use it for initializing data need to be stated after compiling process
        /// </remarks>
        public static void Initialize()
        {
            StringFormat.Alignment = StringAlignment.Center;
            StringFormat.LineAlignment = StringAlignment.Center;
        }
    }
}
