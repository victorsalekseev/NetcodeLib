using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.Runtime.InteropServices;
using System.IO;
using System.Text;
using System.Security.Cryptography;
using Netcode.File.Archive.SelfZIP.CryptoArch;

namespace Sample
{
    public class MainForm : System.Windows.Forms.Form
    {
        private const string
            DemoRegistryKey = "Software\\AcedUtils.NET\\Demo",
            cfgStreamFileName = "StreamFileName",
            cfgCompressionLevel = "CompressionLevel";

        private static uint _tickCount;
        private static int _streamLength;

        private static string _streamFileName = String.Empty;
        private static AcedCompressionLevel _compressionLevel;

        private AcedMemoryWriter _writer;
        private AcedMemoryReader _reader;

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ColumnHeader columnHeader4;
        private System.Windows.Forms.ColumnHeader columnHeader5;
        private System.Windows.Forms.ColumnHeader columnHeader6;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Button saveStreamButton;
        private System.Windows.Forms.Button closeButton;
        private System.Windows.Forms.Button saveFileButton;
        private System.Windows.Forms.Label outputLengthLabel;
        private System.Windows.Forms.ListView outputList;
        private System.Windows.Forms.Label inputLengthLabel;
        private System.Windows.Forms.ListView inputList;
        private System.Windows.Forms.Button openFileButton;
        private System.Windows.Forms.Button openStreamButton;
        private System.Windows.Forms.OpenFileDialog openFileDialog;
        private System.Windows.Forms.SaveFileDialog saveStreamDialog;
        private System.Windows.Forms.OpenFileDialog openStreamDialog;
        private System.Windows.Forms.SaveFileDialog saveFileDialog;
        private System.Windows.Forms.Button clearOutputStreamButton;
        private System.Windows.Forms.CheckBox encryptCheck;
        private System.Windows.Forms.CheckBox decryptCheck;
        private System.Windows.Forms.CheckBox garbleDataCheck;
        private System.Windows.Forms.TextBox encryptPasswordEdit;
        private System.Windows.Forms.TextBox decryptPasswordEdit;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label storedSizeLabel;
        private System.Windows.Forms.ComboBox compressionLevelCombo;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button button2;
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.Container components = null;

        public MainForm()
        {
            InitializeComponent();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (components != null)
                {
                    components.Dispose();
                }
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label3 = new System.Windows.Forms.Label();
            this.compressionLevelCombo = new System.Windows.Forms.ComboBox();
            this.outputLengthLabel = new System.Windows.Forms.Label();
            this.garbleDataCheck = new System.Windows.Forms.CheckBox();
            this.encryptPasswordEdit = new System.Windows.Forms.TextBox();
            this.encryptCheck = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.saveStreamButton = new System.Windows.Forms.Button();
            this.clearOutputStreamButton = new System.Windows.Forms.Button();
            this.openFileButton = new System.Windows.Forms.Button();
            this.outputList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader3 = new System.Windows.Forms.ColumnHeader();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.storedSizeLabel = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.inputLengthLabel = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.closeButton = new System.Windows.Forms.Button();
            this.saveFileButton = new System.Windows.Forms.Button();
            this.openStreamButton = new System.Windows.Forms.Button();
            this.decryptPasswordEdit = new System.Windows.Forms.TextBox();
            this.decryptCheck = new System.Windows.Forms.CheckBox();
            this.inputList = new System.Windows.Forms.ListView();
            this.columnHeader4 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader5 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader6 = new System.Windows.Forms.ColumnHeader();
            this.openFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveStreamDialog = new System.Windows.Forms.SaveFileDialog();
            this.openStreamDialog = new System.Windows.Forms.OpenFileDialog();
            this.saveFileDialog = new System.Windows.Forms.SaveFileDialog();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.label3);
            this.groupBox1.Controls.Add(this.compressionLevelCombo);
            this.groupBox1.Controls.Add(this.outputLengthLabel);
            this.groupBox1.Controls.Add(this.garbleDataCheck);
            this.groupBox1.Controls.Add(this.encryptPasswordEdit);
            this.groupBox1.Controls.Add(this.encryptCheck);
            this.groupBox1.Controls.Add(this.label1);
            this.groupBox1.Controls.Add(this.saveStreamButton);
            this.groupBox1.Controls.Add(this.clearOutputStreamButton);
            this.groupBox1.Controls.Add(this.openFileButton);
            this.groupBox1.Controls.Add(this.outputList);
            this.groupBox1.Location = new System.Drawing.Point(8, 8);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(584, 208);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Запись в бинарный поток";
            // 
            // label3
            // 
            this.label3.Location = new System.Drawing.Point(12, 181);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(48, 16);
            this.label3.TabIndex = 6;
            this.label3.Text = "Сжатие:";
            // 
            // compressionLevelCombo
            // 
            this.compressionLevelCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.compressionLevelCombo.Items.AddRange(new object[] {
																	  "Без сжатия",
																	  "Сверхбыстрое",
																	  "Быстрое",
																	  "Нормальное",
																	  "Максимальное"});
            this.compressionLevelCombo.Location = new System.Drawing.Point(64, 178);
            this.compressionLevelCombo.Name = "compressionLevelCombo";
            this.compressionLevelCombo.Size = new System.Drawing.Size(100, 21);
            this.compressionLevelCombo.TabIndex = 7;
            // 
            // outputLengthLabel
            // 
            this.outputLengthLabel.Location = new System.Drawing.Point(428, 116);
            this.outputLengthLabel.Name = "outputLengthLabel";
            this.outputLengthLabel.Size = new System.Drawing.Size(148, 16);
            this.outputLengthLabel.TabIndex = 4;
            this.outputLengthLabel.Text = "(нет данных)";
            this.outputLengthLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // garbleDataCheck
            // 
            this.garbleDataCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.garbleDataCheck.Location = new System.Drawing.Point(416, 178);
            this.garbleDataCheck.Name = "garbleDataCheck";
            this.garbleDataCheck.Size = new System.Drawing.Size(166, 21);
            this.garbleDataCheck.TabIndex = 10;
            this.garbleDataCheck.Text = "Инвертировать третий байт";
            // 
            // encryptPasswordEdit
            // 
            this.encryptPasswordEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.encryptPasswordEdit.Location = new System.Drawing.Point(320, 178);
            this.encryptPasswordEdit.Name = "encryptPasswordEdit";
            this.encryptPasswordEdit.Size = new System.Drawing.Size(84, 21);
            this.encryptPasswordEdit.TabIndex = 9;
            this.encryptPasswordEdit.Text = "";
            // 
            // encryptCheck
            // 
            this.encryptCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.encryptCheck.Location = new System.Drawing.Point(176, 178);
            this.encryptCheck.Name = "encryptCheck";
            this.encryptCheck.Size = new System.Drawing.Size(144, 21);
            this.encryptCheck.TabIndex = 8;
            this.encryptCheck.Text = "Шифровать с паролем:";
            // 
            // label1
            // 
            this.label1.Location = new System.Drawing.Point(448, 88);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(112, 28);
            this.label1.TabIndex = 3;
            this.label1.Text = "Общий размер данных в потоке:";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // saveStreamButton
            // 
            this.saveStreamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveStreamButton.Location = new System.Drawing.Point(436, 145);
            this.saveStreamButton.Name = "saveStreamButton";
            this.saveStreamButton.Size = new System.Drawing.Size(132, 23);
            this.saveStreamButton.TabIndex = 5;
            this.saveStreamButton.Text = "Записать на диск...";
            this.saveStreamButton.Click += new System.EventHandler(this.saveStreamButton_Click);
            // 
            // clearOutputStreamButton
            // 
            this.clearOutputStreamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.clearOutputStreamButton.Location = new System.Drawing.Point(436, 52);
            this.clearOutputStreamButton.Name = "clearOutputStreamButton";
            this.clearOutputStreamButton.Size = new System.Drawing.Size(132, 23);
            this.clearOutputStreamButton.TabIndex = 2;
            this.clearOutputStreamButton.Text = "Очистить поток";
            this.clearOutputStreamButton.Click += new System.EventHandler(this.clearOutputStreamButton_Click);
            // 
            // openFileButton
            // 
            this.openFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openFileButton.Location = new System.Drawing.Point(436, 20);
            this.openFileButton.Name = "openFileButton";
            this.openFileButton.Size = new System.Drawing.Size(132, 23);
            this.openFileButton.TabIndex = 1;
            this.openFileButton.Text = "Добавить файл...";
            this.openFileButton.Click += new System.EventHandler(this.openFileButton_Click);
            // 
            // outputList
            // 
            this.outputList.AllowColumnReorder = true;
            this.outputList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.outputList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						 this.columnHeader1,
																						 this.columnHeader2,
																						 this.columnHeader3});
            this.outputList.FullRowSelect = true;
            this.outputList.GridLines = true;
            this.outputList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.outputList.Location = new System.Drawing.Point(12, 20);
            this.outputList.Name = "outputList";
            this.outputList.Size = new System.Drawing.Size(406, 148);
            this.outputList.TabIndex = 0;
            this.outputList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Имя файла";
            this.columnHeader1.Width = 193;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Размер";
            this.columnHeader2.Width = 70;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Цифровая сигнатура";
            this.columnHeader3.Width = 123;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.storedSizeLabel);
            this.groupBox2.Controls.Add(this.label2);
            this.groupBox2.Controls.Add(this.inputLengthLabel);
            this.groupBox2.Controls.Add(this.label4);
            this.groupBox2.Controls.Add(this.closeButton);
            this.groupBox2.Controls.Add(this.saveFileButton);
            this.groupBox2.Controls.Add(this.openStreamButton);
            this.groupBox2.Controls.Add(this.decryptPasswordEdit);
            this.groupBox2.Controls.Add(this.decryptCheck);
            this.groupBox2.Controls.Add(this.inputList);
            this.groupBox2.Location = new System.Drawing.Point(8, 224);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(584, 199);
            this.groupBox2.TabIndex = 1;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Чтение из бинарного потока";
            // 
            // storedSizeLabel
            // 
            this.storedSizeLabel.Location = new System.Drawing.Point(340, 23);
            this.storedSizeLabel.Name = "storedSizeLabel";
            this.storedSizeLabel.Size = new System.Drawing.Size(96, 16);
            this.storedSizeLabel.TabIndex = 3;
            this.storedSizeLabel.Text = "(нет данных)";
            // 
            // label2
            // 
            this.label2.Location = new System.Drawing.Point(280, 23);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(60, 16);
            this.label2.TabIndex = 2;
            this.label2.Text = "На диске:";
            this.label2.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // inputLengthLabel
            // 
            this.inputLengthLabel.Location = new System.Drawing.Point(428, 132);
            this.inputLengthLabel.Name = "inputLengthLabel";
            this.inputLengthLabel.Size = new System.Drawing.Size(148, 16);
            this.inputLengthLabel.TabIndex = 8;
            this.inputLengthLabel.Text = "(нет данных)";
            this.inputLengthLabel.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.Location = new System.Drawing.Point(448, 104);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(112, 28);
            this.label4.TabIndex = 7;
            this.label4.Text = "Общий размер данных в потоке:";
            this.label4.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // closeButton
            // 
            this.closeButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.closeButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.closeButton.Location = new System.Drawing.Point(440, 163);
            this.closeButton.Name = "closeButton";
            this.closeButton.Size = new System.Drawing.Size(128, 23);
            this.closeButton.TabIndex = 9;
            this.closeButton.Text = "Escape - Выход";
            this.closeButton.Click += new System.EventHandler(this.closeButton_Click);
            // 
            // saveFileButton
            // 
            this.saveFileButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.saveFileButton.Location = new System.Drawing.Point(436, 52);
            this.saveFileButton.Name = "saveFileButton";
            this.saveFileButton.Size = new System.Drawing.Size(132, 40);
            this.saveFileButton.TabIndex = 6;
            this.saveFileButton.Text = "Сохранить отмеченный файл...";
            this.saveFileButton.Click += new System.EventHandler(this.saveFileButton_Click);
            // 
            // openStreamButton
            // 
            this.openStreamButton.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.openStreamButton.Location = new System.Drawing.Point(436, 20);
            this.openStreamButton.Name = "openStreamButton";
            this.openStreamButton.Size = new System.Drawing.Size(132, 23);
            this.openStreamButton.TabIndex = 4;
            this.openStreamButton.Text = "Загрузить с диска...";
            this.openStreamButton.Click += new System.EventHandler(this.openStreamButton_Click);
            // 
            // decryptPasswordEdit
            // 
            this.decryptPasswordEdit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.decryptPasswordEdit.Location = new System.Drawing.Point(184, 20);
            this.decryptPasswordEdit.Name = "decryptPasswordEdit";
            this.decryptPasswordEdit.Size = new System.Drawing.Size(84, 21);
            this.decryptPasswordEdit.TabIndex = 1;
            this.decryptPasswordEdit.Text = "";
            // 
            // decryptCheck
            // 
            this.decryptCheck.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.decryptCheck.Location = new System.Drawing.Point(12, 20);
            this.decryptCheck.Name = "decryptCheck";
            this.decryptCheck.Size = new System.Drawing.Size(172, 21);
            this.decryptCheck.TabIndex = 0;
            this.decryptCheck.Text = "Расшифровывать с паролем:";
            // 
            // inputList
            // 
            this.inputList.AllowColumnReorder = true;
            this.inputList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.inputList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.columnHeader4,
																						this.columnHeader5,
																						this.columnHeader6});
            this.inputList.FullRowSelect = true;
            this.inputList.GridLines = true;
            this.inputList.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.Nonclickable;
            this.inputList.HideSelection = false;
            this.inputList.Location = new System.Drawing.Point(12, 52);
            this.inputList.Name = "inputList";
            this.inputList.Size = new System.Drawing.Size(406, 134);
            this.inputList.TabIndex = 5;
            this.inputList.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader4
            // 
            this.columnHeader4.Text = "Имя файла";
            this.columnHeader4.Width = 193;
            // 
            // columnHeader5
            // 
            this.columnHeader5.Text = "Размер";
            this.columnHeader5.Width = 70;
            // 
            // columnHeader6
            // 
            this.columnHeader6.Text = "Цифровая сигнатура";
            this.columnHeader6.Width = 123;
            // 
            // openFileDialog
            // 
            this.openFileDialog.Filter = "Все файлы (*.*)|*.*";
            this.openFileDialog.Title = "Выберите файл для добавления в бинарный поток";
            // 
            // saveStreamDialog
            // 
            this.saveStreamDialog.Filter = "Все файлы (*.*)|*.*";
            this.saveStreamDialog.Title = "Укажите файл для сохранения данных потока";
            // 
            // openStreamDialog
            // 
            this.openStreamDialog.Filter = "Все файлы (*.*)|*.*";
            this.openStreamDialog.Title = "Выберите файл, содержащий данные бинарного потока";
            // 
            // saveFileDialog
            // 
            this.saveFileDialog.Filter = "Все файлы (*.*)|*.*";
            this.saveFileDialog.Title = "Укажите имя выходного файла";
            // 
            // button1
            // 
            this.button1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button1.Location = new System.Drawing.Point(8, 432);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(224, 48);
            this.button1.TabIndex = 2;
            this.button1.Text = "Запись/чтение данных в MemoryStream посредством классов: AcedStreamWriter/AcedStr" +
                "eamReader";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // button2
            // 
            this.button2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button2.Location = new System.Drawing.Point(244, 432);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(348, 48);
            this.button2.TabIndex = 3;
            this.button2.Text = "Испытание канала записи: AcedStreamWriter - AcedWriterStream - AcedMemoryWriter -" +
                " byte[] и чтения: byte[] - AcedMemoryReader - AcedReaderStream - AcedStreamReade" +
                "r";
            this.button2.Click += new System.EventHandler(this.button2_Click);
            // 
            // MainForm
            // 
            this.AutoScaleBaseSize = new System.Drawing.Size(5, 14);
            this.CancelButton = this.closeButton;
            this.ClientSize = new System.Drawing.Size(600, 487);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.button1);
            this.Font = new System.Drawing.Font("Tahoma", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.Name = "MainForm";
            this.Text = "AcedUtils.NET Demo";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox2.ResumeLayout(false);
            this.ResumeLayout(false);

        }
        #endregion

        [DllImport("KERNEL32.DLL", ExactSpelling = true, EntryPoint = "GetTickCount", CharSet = CharSet.Auto)]
        private static extern uint GetTickCount();

        private static void StartCount()
        {
            _tickCount = GetTickCount();
        }

        private static void StopCount()
        {
            _tickCount = GetTickCount() - _tickCount;
        }

        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            LoadConfig();
            Application.Run(new MainForm());
            SaveConfig();
        }

        private static void LoadConfig()
        {
            using (AcedRegistry config = new AcedRegistry(AcedBaseKey.CurrentUser,
                DemoRegistryKey, false))
            {
                config.Get(cfgStreamFileName, ref _streamFileName);
                _compressionLevel = (AcedCompressionLevel)config.GetDef(cfgCompressionLevel, 0);
            }
        }

        private static void SaveConfig()
        {
            using (AcedRegistry config = new AcedRegistry(AcedBaseKey.CurrentUser,
                DemoRegistryKey, true))
            {
                config.Put(cfgStreamFileName, _streamFileName);
                config.Put(cfgCompressionLevel, (int)_compressionLevel);
            }
        }

        private void MainForm_Load(object sender, System.EventArgs e)
        {
            _writer = new AcedMemoryWriter();
            compressionLevelCombo.SelectedIndex = (int)_compressionLevel;
        }

        private void closeButton_Click(object sender, System.EventArgs e)
        {
            Close();
        }

        private void openFileButton_Click(object sender, System.EventArgs e)
        {
            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
                OpenFile(openFileDialog.FileName);
            }
        }

        private void clearOutputStreamButton_Click(object sender, System.EventArgs e)
        {
            ClearOutputStream();
        }

        private void saveStreamButton_Click(object sender, System.EventArgs e)
        {
            saveStreamDialog.FileName = _streamFileName;
            if (saveStreamDialog.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
                _streamFileName = saveStreamDialog.FileName;
                _compressionLevel = (AcedCompressionLevel)compressionLevelCombo.SelectedIndex;
                SaveStream();
                MessageBox.Show("Бинарный поток размером " + _streamLength.ToString() +
                    " байт сохранен в файле:\n" + _streamFileName +
                    "\n\nВремя преобразования: " + _tickCount.ToString() + " мс." +
                    "\n\nПроцент сжатия: " + ((double)(_writer.Length - _streamLength) /
                    _writer.Length * 100.00).ToString("G5") + " %     Остаток: " +
                    ((double)_streamLength / _writer.Length * 100.00).ToString("G5") + " %",
                    "Сохранение данных на диске");
            }
        }

        private void openStreamButton_Click(object sender, System.EventArgs e)
        {
            openStreamDialog.FileName = _streamFileName;
            if (openStreamDialog.ShowDialog() == DialogResult.OK)
            {
                Application.DoEvents();
                _streamFileName = openStreamDialog.FileName;
                OpenStream();
                if (_reader != null)
                    MessageBox.Show("Бинарный поток размером " + _streamLength.ToString() +
                        " байт прочитан из файла:\n" + _streamFileName +
                        "\n\nВремя преобразования: " + _tickCount.ToString() + " мс.",
                        "Загрузка данных с диска");
            }
        }

        private void saveFileButton_Click(object sender, System.EventArgs e)
        {
            ListViewItem item = null;
            if (_reader != null)
            {
                ListView.SelectedListViewItemCollection sc = inputList.SelectedItems;
                if (sc.Count > 0)
                    item = sc[0];
            }
            if (item != null)
            {
                saveFileDialog.FileName = item.Text;
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    Application.DoEvents();
                    SaveFile(saveFileDialog.FileName, item);
                }
            }
        }

        private void UpdateOutputLength()
        {
            outputLengthLabel.Text = _writer.Length.ToString() + " байт";
        }

        private void UpdateInputLength()
        {
            if (_reader != null)
            {
                inputLengthLabel.Text = _reader.Size.ToString() + " байт";
                storedSizeLabel.Text = _streamLength.ToString() + " байт";
            }
            else
            {
                inputLengthLabel.Text = "(нет данных)";
                storedSizeLabel.Text = "(нет данных)";
            }
        }

        private void OpenFile(string fileName)
        {
            FileStream fs = new FileStream(fileName, FileMode.Open);
            fileName = fileName.Substring(fileName.LastIndexOf('\\') + 1);
            int fileSize = (int)fs.Length;
            _writer.WriteString(fileName);
            _writer.WriteInt32(fileSize);
            int startIndex = _writer.Length;
            _writer.Skip(fileSize);
            fs.Read(_writer.GetBuffer(), startIndex, fileSize);
            fs.Close();
            int[] rmd = AcedRipeMD.Compute(_writer.GetBuffer(), startIndex, fileSize);
            ListViewItem item = new ListViewItem(fileName);
            item.SubItems.Add(fileSize.ToString());
            item.SubItems.Add(((uint)rmd[0]).ToString("X8") + ((uint)rmd[4]).ToString("X8"));
            outputList.Items.Add(item);
            UpdateOutputLength();
        }

        private void ClearOutputStream()
        {
            _writer.Reset();
            outputList.Items.Clear();
            UpdateOutputLength();
            GC.Collect();
        }

        private void SaveStream()
        {
            StartCount();
            byte[] bytes = _writer.ToArray(_compressionLevel, encryptCheck.Checked ?
                AcedRipeMD.ToGuid(AcedRipeMD.Compute(encryptPasswordEdit.Text)) : Guid.Empty);
            StopCount();
            _streamLength = bytes.Length;
            if (garbleDataCheck.Checked)
                bytes[2] = (byte)(~bytes[2]);
            FileStream fs = new FileStream(_streamFileName, FileMode.Create);
            fs.Write(bytes, 0, _streamLength);
            fs.Close();
            GC.Collect();
        }

        private void OpenStream()
        {
            FileStream fs = new FileStream(_streamFileName, FileMode.Open);
            _streamLength = (int)fs.Length;
            byte[] bytes = new byte[_streamLength];
            fs.Read(bytes, 0, _streamLength);
            fs.Close();
            inputList.Items.Clear();
            try
            {
                StartCount();
                _reader = new AcedMemoryReader(bytes, 0, _streamLength, decryptCheck.Checked ?
                    AcedRipeMD.ToGuid(AcedRipeMD.Compute(decryptPasswordEdit.Text)) : Guid.Empty);
                StopCount();
            }
            catch (AcedDataCorruptedException e)
            {
                MessageBox.Show(e.Message, "Ошибка при инициализации бинарного потока",
                    MessageBoxButtons.OK, MessageBoxIcon.Error);
                _reader = null;
                UpdateInputLength();
                GC.Collect();
                return;
            }
            while (_reader.Position < _reader.Size)
            {
                ListViewItem item = new ListViewItem(_reader.ReadString());
                item.Tag = _reader.Position;
                int fileSize = _reader.ReadInt32();
                int[] rmd = AcedRipeMD.Compute(_reader.GetBuffer(), _reader.Position, fileSize);
                _reader.Skip(fileSize);
                item.SubItems.Add(fileSize.ToString());
                item.SubItems.Add(((uint)rmd[0]).ToString("X8") + ((uint)rmd[4]).ToString("X8"));
                inputList.Items.Add(item);
            }
            UpdateInputLength();
            GC.Collect();
        }

        private void SaveFile(string fileName, ListViewItem item)
        {
            _reader.Reset();
            _reader.Skip((int)item.Tag);
            int fileSize = _reader.ReadInt32();
            FileStream fs = new FileStream(fileName, FileMode.Create);
            fs.Write(_reader.GetBuffer(), _reader.Position, fileSize);
            fs.Close();
        }

        private void PutData(Stream stream, byte[] bytes, short n, string s, int[] otherValues)
        {
            AcedStreamWriter w = AcedStreamWriter.Instance;
            w.AssignStream(stream, AcedCompressionLevel.Fast, new Guid("CA761232-ED42-11CE-BACD-00AA0057B223"));
            w.WriteByteArray(bytes);
            w.WriteInt16(10000);
            w.Write(otherValues, 10, 100);
            w.WriteString("Hello world!");
            w.Close(true);
        }

        private void GetData(Stream stream, ref byte[] bytes, ref short n, ref string s, int[] otherValues)
        {
            AcedStreamReader r = AcedStreamReader.Instance;
            r.AssignStream(stream, new Guid("CA761232-ED42-11CE-BACD-00AA0057B223"));
            bytes = r.ReadByteArray();
            n = r.ReadInt16();
            r.Read(otherValues, 10, 100);
            s = r.ReadString();
            r.Close(true);
        }

        private void TestStreams(bool useAcedStreams)
        {
            byte[] bytes = new byte[] { 5, 6, 7, 8, 9 };
            short n = 10000;
            int[] otherValues = new int[120];
            for (int i = 0; i < 120; i += 3)
            {
                otherValues[i] = 1;
                otherValues[i + 1] = 2;
                otherValues[i + 2] = 3;
            }
            string s = "Hello world!";
            byte[] buffer;
            if (!useAcedStreams)
            {
                MemoryStream ms = new MemoryStream(1000);
                PutData(ms, bytes, n, s, otherValues);
                buffer = ms.ToArray();
            }
            else
            {
                AcedMemoryWriter mw = new AcedMemoryWriter(1000);
                AcedWriterStream ws = new AcedWriterStream(mw);
                PutData(ws, bytes, n, s, otherValues);
                buffer = mw.ToArray(AcedCompressionLevel.Store);
            }
            MessageBox.Show("В бинарный поток записано " +
                buffer.Length.ToString() + " байт.", "AcedUtils.NET Demo");
            bytes = null;
            n = 0;
            Array.Clear(otherValues, 0, otherValues.Length);
            s = null;
            if (!useAcedStreams)
            {
                MemoryStream ms = new MemoryStream(buffer);
                GetData(ms, ref bytes, ref n, ref s, otherValues);
            }
            else
            {
                AcedMemoryReader mr = new AcedMemoryReader(buffer, 0, buffer.Length);
                AcedReaderStream rs = new AcedReaderStream(mr);
                GetData(rs, ref bytes, ref n, ref s, otherValues);
            }
            MessageBox.Show("Прочитано из потока: " + s, "AcedUtils.NET Demo");
        }

        private void button1_Click(object sender, System.EventArgs e)
        {
            TestStreams(false);
        }

        private void button2_Click(object sender, System.EventArgs e)
        {
            TestStreams(true);
        }
    }
}
