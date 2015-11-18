using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Test
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //initialize nothing just display window
        }

        private void button1_Click(object sender, EventArgs e)
        {
            treeView1.Nodes.Clear();
            x = InputTexbox.Text;
            x = x.Trim();
            CreateTree();
        }

        string x;
        int tlength;
        string equation = "+-*/";

        void CreateTree()
        {
            tlength = x.Length - 1;
            TreeNode mtree = LeafTree();
            treeView1.Nodes.Add(mtree);
        }

        TreeNode LeafTree()
        {
            if( tlength > -1)
            {
                if (equation.Contains(x[tlength]))
                {
                    char m = x[tlength];
                    tlength -= 1;
                    TreeNode t1 = LeafTree();
                    tlength -= 1;
                    TreeNode t2 = LeafTree();
                    TreeNode[] at = null;
                    if (t1 == null)
                        at = new TreeNode[] { t2 };
                    else if (t2 == null)
                        at = new TreeNode[] { t1 };
                    else
                        at = new TreeNode[] { t1, t2 };
                    return new TreeNode(m.ToString(), at);
                }
                else return new TreeNode(x[tlength].ToString());
            }
            return null;
        }

        private void treeView1_AfterSelect(object sender, TreeViewEventArgs e)
        {
            textBox2.Text = string.Empty;
            string m = treeView1.SelectedNode.Text;
            if (equation.Contains(m))
            {

                string n = treeView1.SelectedNode.Nodes[0].Text;
                TreeNode i = treeView1.SelectedNode;
                m = FindLeafNode(treeView1.SelectedNode);
                if (m.Length < 6)
                {
                    m = m.Replace('(', ' ');
                    m = m.Replace(')', ' ');
                    m = m.Trim();
                }
                else
                {
                    m = m.Substring(1, m.Length - 2);
                }
                double Result = CalLeafNode(treeView1.SelectedNode);
                m = m + " = " + Result.ToString();
                textBox2.Text = m;
            }
            else textBox2.Text = m;
        }

        string FindLeafNode(TreeNode i)
        {
            int k;
            string ist = i.Text;
            k = i.GetNodeCount(false);
            if (k > 0)
                return "(" + FindLeafNode(i.Nodes[1]) + ist + FindLeafNode(i.Nodes[0]) + ")";
            else return ist;
        }

        double CalLeafNode(TreeNode i)
        {
            int k;
            string ist = i.Text;
            k = i.GetNodeCount(false);
    
            if (k > 0 && equation.Contains(ist))
                switch (Convert.ToChar(ist))
                {
                    case '+': return CalLeafNode(i.Nodes[1]) + CalLeafNode(i.Nodes[0]); 
                    case '-': return CalLeafNode(i.Nodes[1]) - CalLeafNode(i.Nodes[0]);
                    case '*': return CalLeafNode(i.Nodes[1]) * CalLeafNode(i.Nodes[0]);
                    case '/': return CalLeafNode(i.Nodes[1]) / CalLeafNode(i.Nodes[0]);
                    default: return 0;
                }      
            else return Convert.ToDouble(ist);
        }
    }
}
