using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphWinForms
{
    /// <summary>
    /// Sorting type
    /// </summary>
    public enum SortType
    {
        /// <summary>
        /// Sorting by values ascending
        /// </summary>
        Ascending,
        /// <summary>
        /// Sorting by values descending
        /// </summary>
        Descending
    }
    /// <summary>
    /// Graph edge orientation (currently not in use)
    /// </summary>
    public enum EdgeOrientation
    {
        /// <summary>
        /// Single oriented edge
        /// </summary>
        Single,
        /// <summary>
        /// Unoriented edge
        /// </summary>
        Binary
    }
    /// <summary>
    /// Represents drawing tools constants
    /// </summary>
    public enum DrawTools
    {
        /// <summary>
        /// Default tool
        /// </summary>
        Cursor,
        /// <summary>
        /// Vertex creating tool
        /// </summary>
        Vertex,
        /// <summary>
        /// Edge creating tool
        /// </summary>
        Edge,
        /// <summary>
        /// Editing tool
        /// </summary>
        Edit,
        /// <summary>
        /// Deleting tool
        /// </summary>
        Delete,
        /// <summary>
        /// Deikstra algorithm vertex selecting tool
        /// </summary>
        Deikstra,
        /// <summary>
        /// Tool to find all shortest paths from any vertex
        /// </summary>
        Center
    }
    /// <summary>
    /// Graph savig type constants
    /// </summary>
    public enum SaveType
    {
        /// <summary>
        /// Saving methods will show all the dialogs
        /// </summary>
        Normal,
        /// <summary>
        /// Saving methods won't show any dialogs
        /// </summary>
        Silent
    }
    /// <summary>
    /// Graph loading type constants
    /// </summary>
    public enum LoadType
    {
        /// <summary>
        /// Loading methods will show all the dialogs
        /// </summary>
        Normal,
        /// <summary>
        /// Loading methods won't show any dialogs
        /// </summary>
        Silent
    }
    /// <summary>
    /// Shortest path searching field of graph edge
    /// </summary>
    public enum PathSearchingField
    {
        /// <summary>
        /// Edge weight
        /// </summary>
        Weight,
        /// <summary>
        /// Edge length
        /// </summary>
        Length
    }
}
