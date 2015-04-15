using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text;
using System.IO;
namespace FolderSearcher
{
    public partial class frmOptions : Form
    {

        public frmOptions()
        {
            InitializeComponent();
        }

        private void frmOptions_Load(object sender, EventArgs e)
        {
            string showExcludeFileTypes = frmFolderSearcher.GetOptions()["ExcludeFilesOfTypes"].Split(',')[0];
            string showExcludeFilesLargerThan = frmFolderSearcher.GetOptions()["ExcludeFilesLargerThan"].Split(',')[0];
            string showProgress = frmFolderSearcher.GetOptions()["ShowProgress"].Split(',')[0];
            string largestSize = frmFolderSearcher.GetOptions()["ExcludeFilesLargerThan"].Split(',')[1];
            string searchSubfolders = frmFolderSearcher.GetOptions()["SearchSubfolders"].Split(',')[0];
            string caseSensitive = frmFolderSearcher.GetOptions()["CaseSensitive"].Split(',')[0];
            string defaultFileType = frmFolderSearcher.GetOptions()["DefaultFileType"].Split(',')[0];
            string defaultSearchString = frmFolderSearcher.GetOptions()["DefaultSearchString"].Split(',')[0];
            txtDefaultType.Text = defaultFileType;
            txtDefaultSearchString.Text = defaultSearchString;
            if (showExcludeFilesLargerThan == "True")
            {
                chkExcludeFilesLargerThan.Checked = true;
            }
            if (caseSensitive == "True")
            {
                chkCaseSensitive.Checked = true;
            }
            if (showExcludeFileTypes == "True")
            {
                chkExcludeFilesOfTheseTypes.Checked = true;
            }
            if (searchSubfolders == "True")
            {
                chkSearchRecursively.Checked = true;
            }
            if (showProgress == "True")
            {
                chkShowProgress.Checked = true;
            }
            numKB.Value = Convert.ToDecimal(largestSize);
            string[] exclusionFiles = File.ReadAllLines(Environment.CurrentDirectory + "\\ExclusionList.txt");
            txtExcludeFileTypes.Text = String.Join(",", exclusionFiles);
        }

        private void btnOK_Click(object sender, EventArgs e)
        {
            try
            {
                string content = String.Format(
@"ExcludeFilesOfTypes|{0}
ExcludeFilesLargerThan|{1},{2}
SearchSubfolders|{3}
DefaultFileType|{4}
DefaultSearchString|{5}
ShowProgress|{6}
CaseSensitive|{7}
", chkExcludeFilesOfTheseTypes.Checked.ToString(),//0
chkExcludeFilesLargerThan.Checked.ToString(), numKB.Value.ToString(),//1,2
chkSearchRecursively.Checked.ToString(),//3
txtDefaultType.Text,//4
txtDefaultSearchString.Text,//5
chkShowProgress.Checked.ToString(),//6
chkCaseSensitive.Checked.ToString());//7


                File.WriteAllText(Environment.CurrentDirectory + "\\FolderSearcher.txt",content);
                File.WriteAllText(Environment.CurrentDirectory + "\\ExclusionList.txt",txtExcludeFileTypes.Text);
                this.Close();
            }
            catch { }
        }

        private void btnDismiss_Click(object sender, EventArgs e)
        {
            this.Close();
        }

    
    }
}
