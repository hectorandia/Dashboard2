using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Dashboard
{
    /// <summary>
    /// subclass of TreeNode so that the treeNode knows the OptionPanel it belongs to
    /// </summary>
    public class TreeNodeObject : TreeNode
    {
        private OptionPanel optionControl = null;

        /// <summary>
        /// creates an TreeNodeObject
        /// </summary>
        /// <param name="text"></param>
        /// <param name="optionControl"></param>
        public TreeNodeObject(string text, OptionPanel optionControl) : base(text)
        {
            this.optionControl = optionControl;
        }

        /// <summary>
        /// creates an TreeNodeObject
        /// </summary>
        /// <param name="text"></param>
        /// <param name="children"></param>
        public TreeNodeObject(string text, TreeNode[] children ) : base(text, children)
        {
            
        }

        /// <summary>
        /// creates an TreeNodeObject
        /// </summary>
        /// <param name="text"></param>
        /// <param name="children"></param>
        /// <param name="optionControl"></param>
        public TreeNodeObject(string text, TreeNode[] children, OptionPanel optionControl) : base(text, children)
        {
            this.optionControl = optionControl;
        }

        /// <summary>
        /// get the OptionPanel to which this TreeNodeObject belongs to
        /// </summary>
        public OptionPanel OptionControl
        {
            get
            {
                return optionControl;
            }
        }
    }
}
