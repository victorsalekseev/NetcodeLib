using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Windows.Forms;
using Netcode.Common.Calc;

namespace Netcode.Controls
{
    public partial class LTextBox : UserControl
    {
        private System.Boolean _isCanNull = true;
        /// <summary>
        /// Пустое поле
        /// </summary>
        public System.Boolean isCanNull
        {
            get { return _isCanNull;  }
            set { _isCanNull = value; }
        }

        private System.Boolean _isOnlyRealDigit = false;
        /// <summary>
        /// Только цифры, дробный разделитель и знак минуса
        /// </summary>
        public System.Boolean isOnlyDigit
        {
            get { return _isOnlyRealDigit; }
            set { _isOnlyRealDigit = value; }
        }

        private System.Boolean _isOnlyIntegerDigit = false;
        /// <summary>
        /// Только интегральные целые числа
        /// </summary>
        public System.Boolean isOnlyIntegerDigit
        {
            get { return _isOnlyIntegerDigit; }
            set { _isOnlyIntegerDigit = value; }
        }

        /// <summary>
        /// Имя лейбла
        /// </summary>
        public System.String LabelName
        {
            get { return lb_name.Text; }
            set { lb_name.Text = value; }
        }

        /// <summary>
        /// Текст в текстбоксе
        /// </summary>
        public System.String ControlText
        {
            get { return tb_text.Text; }
            set 
            {
                tb_text.Text = value; 
            }
        }

        /// <summary>
        /// Постоянная ширина лейбла
        /// </summary>
        public System.Int32 LabelWidth
        {
            get { return sC.SplitterDistance; }
            set { sC.SplitterDistance = value; }
        }

        /// <summary>
        /// Только для чтения
        /// </summary>
        public System.Boolean ReadOnlyText
        {
            get { return tb_text.ReadOnly; }
            set { tb_text.ReadOnly = value; }
        }

        /// <summary>
        /// при нажатии клавиши Enter
        /// </summary>
        public delegate void OnPressEnterKey();
        /// <summary>
        /// Вызывается при нажатии клавиши Enter
        /// </summary>
        public event OnPressEnterKey PressEnterKey;

        /// <summary>
        /// при изменнении текста в контроле
        /// </summary>
        public delegate void OnTextChangedContr();
        /// <summary>
        /// Вызывается при изменнении текста в контроле
        /// </summary>
        public event OnTextChangedContr TextChangedContr;

        /// <summary>
        /// при изменнении текста в контроле передает экз.контрола
        /// </summary>
        public delegate void OnTextChangedExContr(LTextBox ltb);
        /// <summary>
        /// Вызывается при изменнении текста в контроле
        /// </summary>
        public event OnTextChangedExContr TextChangedExContr;

        public LTextBox()
        {
            InitializeComponent();
            Init();
        }

        private void Init()
        {
            ActiveControl = tb_text;
            tb_text.KeyUp += new KeyEventHandler(tb_text_KeyUp);
            tb_text.TextChanged += new EventHandler(tb_text_TextChanged);
        }

        void tb_text_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (PressEnterKey != null)
                {
                    PressEnterKey.Invoke();
                }
            }
        }

        public LTextBox(System.String _LabelName)
        {
            InitializeComponent();
            Init();
            LabelName = _LabelName;
        }

        public LTextBox(System.String _LabelName, System.String _ControlText)
        {
            InitializeComponent();
            Init();
            LabelName = _LabelName;
            ControlText = _ControlText;
        }

        public LTextBox(System.String _LabelName, System.String _ControlText, System.Int32 _Width)
        {
            InitializeComponent();
            Init();
            LabelName = _LabelName;
            ControlText = _ControlText;
            Width = _Width;
        }

        private void label_text_Load(object sender, EventArgs e)
        {
            //string connection = "Data Source=localhost;Initial Catalog=DB_PP;Persist Security Info=True;User ID=sa;Password=adminadmin";//zz
            //string command = "SELECT [id_pp] ,[name_pp] FROM [DB_PP].[dbo].[pp]";
            //System.Data.SqlClient.SqlDataAdapter adapter = new System.Data.SqlClient.SqlDataAdapter(command, connection);
            //DataTable topics = new DataTable();
            //adapter.Fill(topics);

            //comboBox1.DisplayMember = topics.Columns[1].ColumnName;
        }

        /// <summary>
        /// Проверка поля ввода на пустоту
        /// </summary>
        /// <returns></returns>
        public bool IsNotNullInput()
        {
            bool ret_value = false;
            if (string.IsNullOrEmpty(tb_text.Text))
            {
                if (!_isCanNull)
                {
                    eP.SetError(tb_text, "Поле не может быть пустым");
                }
                ret_value = false;
            }
            else
            {
                eP.Clear();
                ret_value = true;
            }
            return ret_value;
        }

        /// <summary>
        /// Проверка поля на ввод только чисел, дробного разделителя и знака -
        /// </summary>
        /// <returns></returns>
        public bool IsDigitInput()
        {
            bool ret_value = false;
                if (!Digit.IsRealDigitInput(tb_text.Text))
                {
                    if (_isOnlyRealDigit)
                    {
                        eP.SetError(tb_text, "Поле может содержать только числа, разделитель дробной части и знак минуса");
                    }
                    ret_value = false;
                }
                else
                {
                    eP.Clear();
                    ret_value = true;
                }

            return ret_value;
        }

        public bool IsIntegerDigitInput()
        {
            bool ret_value = false;
            if (!Digit.IsIntegerDigitInput(tb_text.Text))
            {
                if (_isOnlyIntegerDigit)
                {
                    eP.SetError(tb_text, "Поле может содержать только целые числа и знак минуса");
                }
                ret_value = false;
            }
            else
            {
                eP.Clear();
                ret_value = true;
            }

            return ret_value;
        }

        private void tb_text_TextChanged(object sender, EventArgs e)
        {
            IsNotNullInput();
            IsDigitInput();
            IsIntegerDigitInput();
            if (TextChangedContr != null)
            {
                TextChangedContr.Invoke();
            }
            if (TextChangedExContr != null)
            {
                TextChangedExContr.Invoke(this);
            }
        }
    }
}