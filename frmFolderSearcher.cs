using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.IO;
using System.ComponentModel;
using System.Threading.Tasks;
using System.Threading;
namespace FolderSearcher
{
    public partial class frmFolderSearcher : Form
    {
        CancellationTokenSource ct = new CancellationTokenSource();
        string _path = "", _excludeFilesOfTypeList = "", _excludeFilesLargerThan = "";
        bool _sortList = true;
        DateTime _lastReportTime = DateTime.Now;
        HashSet<string> _uniquePaths, _uniqueFiles;
        DateTime _dtStart = DateTime.Now;
        Cursor _activeCursor = Cursors.Arrow;
        int _foldersFound = 0;
        private SynchronizationContext context;
        public frmFolderSearcher()
        {
            InitializeComponent();
            context = SynchronizationContext.Current;
            if (context == null)
            {
                context = new SynchronizationContext();
            }
        }
        private async void btnSearch_Click(object sender, EventArgs e)
        {
            if (txtTypes.Text == "") { txtTypes.Text = "*.*"; }
            _dtStart = DateTime.Now;
            _excludeFilesOfTypeList = Convert.ToBoolean(GetOptions()["ExcludeFilesOfTypes"]) ?
                File.ReadAllText(Environment.CurrentDirectory + "\\ExclusionList.txt") : "";
            _excludeFilesLargerThan = Convert.ToBoolean(GetOptions()["ExcludeFilesLargerThan"].Split(',')[0]) ?
                Convert.ToString(GetOptions()["ExcludeFilesLargerThan"].Split(',')[1]) : "0";
            Button_Click(btnClear, null);
            backgroundWorker1.RunWorkerAsync();
        }
        private void Form1_Load(object sender, EventArgs e)
        {
            lblStatus.Text = "";
            string searchString = GetOptions()["DefaultSearchString"].Split(',')[0];
            string fileType = GetOptions()["DefaultFileType"].Split(',')[0];
            txtTypes.Text = fileType;
            txtString.Text = searchString;
            string folders = File.ReadAllText(Environment.CurrentDirectory + "\\MostRecentFolders.txt");
            foreach (string path in folders.Split(','))
            {
                cboPath.Items.Add(path);
            }
            if (cboPath.Items.Count > 0)
            {
                cboPath.SelectedIndex = 0;
                _path = cboPath.Text;
            }
        }
        private async Task<List<string>> GetFiles(string path,string searchString, string pattern, IProgress<FileReadProgress> progress, int filesFound, bool recursive, int excludeFilesLargerThan,string excludeTypeList,bool firstTime )
        {
            try
            {
                if (path.Contains("mmSpatialWRSt"))
                {
                }
                List<string> files = new List<string>();
                try
                {
                    _uniquePaths.Add(path);
                    string[] filesInPath = Directory.GetFiles(path, pattern, SearchOption.TopDirectoryOnly);
                    if (recursive || firstTime)
                    {
                        foreach (string file in filesInPath)
                        {
                            bool addIt = true;

                            addIt = ShouldSearchFile(searchString, excludeFilesLargerThan, excludeTypeList, file, addIt);
                            if (addIt)
                            {
                                files.Add(file);
                                _uniqueFiles.Add(file);
                            }
                        }
                        filesFound += files.Count;
                        FileReadProgress frp = new FileReadProgress
                        {
                            FindingFilesOfType = true //We are looking for files of a certain type at this point (step #1 in search process)
                        };
                        TimeSpan ts = DateTime.Now - _lastReportTime;
                        if (progress != null)
                        {
                            progress.Report(frp);
                        }
                        foreach (var directory in Directory.GetDirectories(path))
                        {
                            files.AddRange(GetFiles(directory, searchString, pattern, progress, filesFound, recursive, excludeFilesLargerThan, excludeTypeList, false).Result);
                        }
                    }
                }
                catch (UnauthorizedAccessException) { } //Just ignore it and move on
                return files;
            }
            catch(Exception ex)
            {
                MessageBox.Show("Unfortunately, an error occurred: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private static bool ShouldSearchFile(string searchString, int excludeFilesLargerThan, string excludeTypeList, string file, bool addIt)
        {
            //if (searchString.Length > 0)
            //{
            //    if (file.ToUpper().Contains(searchString.ToUpper()) == false)
            //    {
            //        addIt = false;
            //    }
            //}
            if (excludeTypeList.Length > 0)
            {
                int suffixIndex = file.IndexOf(".");
                if (suffixIndex > -1)//The file doesn't have an extension if a period isn't found
                {
                    string suffix = file.Substring(suffixIndex);
                    if (excludeTypeList.Contains(suffix))
                    {
                        addIt = false;
                    }
                }
            }
            if (addIt && (excludeFilesLargerThan > 0))
            {
                int sizeInKB = Convert.ToInt32((new FileInfo(file)).Length / 1024);
                if (sizeInKB > excludeFilesLargerThan)
                {
                    addIt = false;
                }
            }
            return addIt;
        }
        public static void HighlightText(RichTextBox myRtb, string word, Color foreColor, Color backColor)
        {
            if (word.Length == 0) { return; }
            int s_start = myRtb.SelectionStart, startIndex = 0, index,scrollTo= 0;
            string textToSearch = (GetOptions()["CaseSensitive"] == "False") ? myRtb.Text.ToUpper() : myRtb.Text;
            word = (GetOptions()["CaseSensitive"] == "False") ? word.ToUpper() : word;
            while ((index = textToSearch.IndexOf(word, startIndex)) != -1)
            {
                myRtb.Select(index, word.Length);
                myRtb.SelectionColor = foreColor;
                myRtb.SelectionBackColor = backColor;
                startIndex = index + word.Length;
                if (scrollTo == 0)
                {
                    scrollTo = index;
                }
            }
            myRtb.SelectionStart = scrollTo;
            myRtb.SelectionLength = 0;
            myRtb.SelectionColor = Color.Black;
        }
        private void dgFiles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgFiles.SelectedCells.Count > 0)
            {
                FileName = dgFiles.SelectedCells[1].Value.ToString();
                richTextBox1.Text = File.ReadAllText(FileName);
                HighlightText(richTextBox1, txtString.Text, Color.Yellow, Color.Blue);
                richTextBox1.ScrollToCaret();
            }
        }
        private async void backgroundWorker1_DoWork(object sender, System.ComponentModel.DoWorkEventArgs ev)
        {
            bool showProgress = Convert.ToBoolean(GetOptions()["ShowProgress"]);
            BackgroundWorker worker = sender as BackgroundWorker;
            bool firstTime = true;
            context.Send(new SendOrPostCallback(async (j) =>
            {
                this.Cursor = Cursors.WaitCursor;
                _activeCursor = Cursors.WaitCursor;
                lblStatus.Text = "Finding Files...";
            }), null);
            Progress<FileReadProgress> progress = null;
            if (showProgress)
            {
                progress = new Progress<FileReadProgress>();
                progress.ProgressChanged += (s, e) =>
                {
                    if (e.FindingFilesOfType)
                    {
                        context.Send(new SendOrPostCallback(async (j) =>
                        {
                            _foldersFound++;
                            if (showProgress)
                            {
                                lblFoldersFound.Text = _uniquePaths.Count.ToString();
                                lblFilesOfTypeFound.Text = _uniqueFiles.Count.ToString();
                            }
                        }), null);
                    }
                    else
                    {
                        context.Send(new SendOrPostCallback(async (j) =>
                        {
                            if (firstTime)
                            {
                                firstTime = false;
                                lblStatus.Text = "Searching Inside Files...";
                                progressBar1.Maximum = Convert.ToInt32(lblFilesOfTypeFound.Text);
                            }
                            if (e.IsMatch)
                            {
                                string fileName = e.FileName.Substring(1 + e.FileName.LastIndexOf("\\"));
                                //matches.Add(fileName + "|" + e.FileName);
                                if (showProgress)
                                {
                                    this.dgFiles.Rows.Add(fileName, e.FileName);
                                    lblMatches.Text = dgFiles.RowCount.ToString();
                                }
                            }
                            if (showProgress)
                            {
                                progressBar1.Value++;
                            }
                        }), null);
                    }
                };
            }
            string typeString = txtTypes.Text;
            int excludeFilesLargerThan = Convert.ToInt16(_excludeFilesLargerThan);
            context.Send(new SendOrPostCallback(async (j) =>
            {
                btnCancel.Enabled = false;
                btnClear.Enabled = false;
                btnClose.Enabled = false;
                btnOptions.Enabled = false;
            }), null);
            //If the user isn't searching file content (just file names), then we need to pass SearchString in as the file name to look for
            string searchString = Convert.ToBoolean(GetOptions()["SearchSubfolders"]) ?
                txtString.Text : "";
            bool searchSubfolders = Convert.ToBoolean(GetOptions()["SearchSubfolders"]);
            List<string> files = await GetFiles(_path,searchString, typeString, progress, 0, searchSubfolders, excludeFilesLargerThan, _excludeFilesOfTypeList, true);
            context.Send(new SendOrPostCallback(async (j) =>
            {
                btnCancel.Enabled = true;
            }), null);
            var matches = files;
            if (txtString.Text.Length > 0)
            {
                bool caseSensitive = Convert.ToBoolean(GetOptions()["CaseSensitive"]);
                matches = await files.FindFilesMatchingStringWithProgressAsync(txtString.Text, _path, progress, caseSensitive, ct);
            }
            context.Send(new SendOrPostCallback(async (j) =>
            {
                btnCancel.Enabled = true;
                btnClear.Enabled = true;
                btnClose.Enabled = true;
                btnOptions.Enabled = true;
                this.Cursor = Cursors.Arrow;
                _activeCursor = Cursors.Arrow;
                lblStatus.Text = "";
                progressBar1.Value = 0;
                if (!showProgress || (txtString.Text.Length == 0))
                {
                    foreach (string s in matches)
                    {
                        string pipeSeparatedFileAndPath = s;
                        if (txtString.Text.Length == 0)
                        {
                            string fileName = s.Substring(1 + s.LastIndexOf("\\"));
                            dgFiles.Rows.Add(fileName, s);
                        }
                        else
                        {
                            dgFiles.Rows.Add(pipeSeparatedFileAndPath.Split('|')[0], pipeSeparatedFileAndPath.Split('|')[1]);
                        }
                    }
                    lblMatches.Text = matches.Count.ToString();
                    lblFoldersFound.Text = _uniquePaths.Count.ToString();
                    lblFilesOfTypeFound.Text = files.Count.ToString();
                }
                TimeSpan ts = DateTime.Now - _dtStart;
                MessageBox.Show("Search completed in " + Math.Round(ts.TotalSeconds,2) + " seconds." , "Finished", MessageBoxButtons.OK, MessageBoxIcon.Information);
                UpdateRecentFoldersList();
            } ), null);
        }

        private void UpdateRecentFoldersList()
        {
            string recentFolders = File.ReadAllText(Environment.CurrentDirectory + "\\MostRecentFolders.txt");
            string[] recentFoldersArray = recentFolders.Split(',');
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            string newText = cboPath.Text;
            sb.Append(newText);
            cboPath.Items.Clear();
            cboPath.Items.Add(newText);
            for (int i = 0; i < recentFoldersArray.Length; i++)
            {
                if (recentFoldersArray[i].ToUpper() != newText.ToUpper())
                {
                    sb.Append("," + recentFoldersArray[i]);
                    cboPath.Items.Add(recentFoldersArray[i]);
                    if (i > 10)
                    {
                        break;
                    }
                }
            }
            File.WriteAllText(Environment.CurrentDirectory + "\\MostRecentFolders.txt", sb.ToString());
        }
        public static Dictionary<string, string> GetOptions()
        {
            var ret = new Dictionary<string, string>();
            using (TextReader sr = File.OpenText(Environment.CurrentDirectory + "\\FolderSearcher.txt"))
            {
                string property = sr.ReadLine();
                while (property != null)
                {
                    ret.Add(property.Split('|')[0], property.Split('|')[1]);
                    property = sr.ReadLine();
                }
                return ret;
            }
            return ret;
        }
        
        private string FileName { get; set; }
        private void Button_Click(object sender, EventArgs e)
        {
            string name = (sender as Control).Name;
            switch (name)
            {
                case "cboPath":
                    _path = cboPath.Text;
                    break;
                case "btnCancel":
                    ct.Cancel();
                    break;
                case "btnClear":
                    dgFiles.Rows.Clear();
                    richTextBox1.Text = "";
                    progressBar1.Value = 0;
                    _uniqueFiles = new HashSet<string>();
                    _uniquePaths = new HashSet<string>();
                    lblFilesOfTypeFound.Text = "0";
                    lblFoldersFound.Text = "0";
                    lblMatches.Text = "0";
                    ct = new CancellationTokenSource();
                    break;
                case "btnOptions":
                    frmOptions opt = new frmOptions();
                    opt.ShowDialog();
                    break;
                case "btnClose":
                    this.Close();
                    break;
                case "btnFolder":
                    folderBrowserDialog1.ShowDialog();
                    cboPath.Text = folderBrowserDialog1.SelectedPath;
                    break;
                case "btnExplorer":
                    if (FileName != null)
                        System.Diagnostics.Process.Start("explorer.exe", string.Format("/select,\"{0}\"", FileName));
                    break;
                case "btnNotepad":
                    System.Diagnostics.Process.Start("notepad.exe", FileName);
                    break;
            }
        }

        private void dgFiles_MouseEnter(object sender, EventArgs e)
        {
            this.Cursor = Cursors.Arrow;
        }

        private void dgFiles_MouseLeave(object sender, EventArgs e)
        {
            this.Cursor = this._activeCursor;
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            
        }
        private static DateTime RetrieveLinkerTimestamp()
        {
            string filePath = System.Reflection.Assembly.GetCallingAssembly().Location;
            const int c_PeHeaderOffset = 60;
            const int c_LinkerTimestampOffset = 8;
            byte[] b = new byte[2048];
            System.IO.Stream s = null;

            try
            {
                s = new System.IO.FileStream(filePath, System.IO.FileMode.Open, System.IO.FileAccess.Read);
                s.Read(b, 0, 2048);
            }
            finally
            {
                if (s != null)
                {
                    s.Close();
                }
            }

            int i = System.BitConverter.ToInt32(b, c_PeHeaderOffset);
            int secondsSince1970 = System.BitConverter.ToInt32(b, i + c_LinkerTimestampOffset);
            DateTime dt = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            dt = dt.AddSeconds(secondsSince1970);
            dt = dt.ToLocalTime();
            return dt;
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == System.Windows.Forms.MouseButtons.Right )
            {
                MessageBox.Show("Compiled at: " + RetrieveLinkerTimestamp().ToString());
                string fileName = System.Reflection.Assembly.GetExecutingAssembly().CodeBase.ToUpper().Replace("FILE:///", ""); ;

                MessageBox.Show("Mod date: " + File.GetLastWriteTime(fileName));
            }
        }
    }
    public static class RiversFileInfosExtension
    {
        public static async Task<List<string>> FindFilesMatchingStringWithProgressAsync(this List<string> files, String searchString, string path, IProgress<FileReadProgress> progressDelegate, bool caseSensitive, CancellationTokenSource cts)
        {
            var retList = new List<string>();
            int matches = 0, counter = 0, len = 2; //len is number of bytes read from each file at one time
            while (searchString.Length > len)
            {
                len *= 2;//len should be d^n, but we need to make sure that it is more than twice the size of the search string, otherwise string compares may not work in boundary conditions
            }
            if (caseSensitive == false)
            {
                searchString = searchString.ToUpper();
            }
            if (files == null)
            {
                return null;
            }
            foreach (string fi in files)
            {
                List<string> strings = new List<string>();
                strings.Add("");
                bool isMatch = false;
                try
                {
                    using (StreamReader sr = new StreamReader(fi))
                    {
                        int streamLength = (int)sr.BaseStream.Length;
                        len = streamLength < 4096 ? streamLength : len; //Read entire contents if it is small enough
                        
                        int readLen = 1;
                        while (readLen > 0)
                        {
                            char[] result = new char[len];
                            readLen = await sr.ReadAsync(result, 0, len);
                            strings.Add(new string(result));
                            string combinedString = strings[strings.Count - 2] + strings[strings.Count - 1];
                            if (caseSensitive == false)
                            {
                                combinedString = combinedString.ToUpper();
                            }
                            if (combinedString.Contains(searchString))
                            {
                                isMatch = true;
                                matches++;
                                break;
                            }
                        }
                    }
                }
                catch{}
                await Task.Yield();
                counter++;
                if (progressDelegate != null)
                {
                    FileReadProgress frp = new FileReadProgress
                    {
                        FilesProcessed = counter,
                        FindingFilesOfType = false, //We are looking inside files at this point (step #2 in search process)
                        IsMatch = isMatch,
                        FileName = fi
                    };
                    if (progressDelegate != null)
                    {
                        progressDelegate.Report(frp);
                    }
                }
                else 
                {
                    if (isMatch)
                    {
                        string fileName = fi.Substring(1 + fi.LastIndexOf("\\"));
                        retList.Add(fileName + "|" + fi );
                    }
                }
                if (cts.IsCancellationRequested)
                {
                    break;
                }
            }
            return retList; //matches + " files found out of " + counter + " files searched.";
        }
    }
    public class FileReadProgress
    {
        public int FilesProcessed { get; set; }
        public bool FindingFilesOfType { get; set; }
        public bool IsMatch { get; set; }
        public string FileName { get; set; }
    }
}
