using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using System.Data;
using System.IO;
using Bvv.Pop3;

namespace bvv_pop3
{
	/// <summary>
	/// Summary description for Form1.
	/// </summary>
	public class Form1 : System.Windows.Forms.Form
	{
		Pop3 p;
		private System.Windows.Forms.ListView listView1;
		private System.Windows.Forms.Button butLoad;
		private System.Windows.Forms.TextBox textStat;
		private System.Windows.Forms.ColumnHeader ID;
		private System.Windows.Forms.ListBox listBox2;
		private System.Windows.Forms.Label label1;
		private System.Windows.Forms.Label label2;
		private System.Windows.Forms.Button butAllLoad;
		private System.Windows.Forms.Button butDel;
		private System.Windows.Forms.Button butQuit;
		private System.Windows.Forms.Button butMsgLoad;
		private System.Windows.Forms.ColumnHeader subject;
		private System.Windows.Forms.ColumnHeader from;
		private System.Windows.Forms.ColumnHeader to;
		private System.Windows.Forms.TextBox textPop3;
		private System.Windows.Forms.Label label3;
		private System.Windows.Forms.Label label4;
		private System.Windows.Forms.Label label5;
		private System.Windows.Forms.ListBox listPop3;
		private System.Windows.Forms.Button butAdd;
		private System.Windows.Forms.Button butDelete;
		private System.Windows.Forms.Button butSave;
		private System.Windows.Forms.TextBox textLogin;
		private System.Windows.Forms.TextBox textPassword;
		private System.Windows.Forms.Label label6;
		private System.Windows.Forms.ToolTip toolTip1;
		private System.Windows.Forms.TextBox textBody;
		private System.Windows.Forms.Button butReadToText;
		private System.Windows.Forms.Button butOpen;
		private System.Windows.Forms.Label label7;
		private System.Windows.Forms.TextBox textMemo;
		private System.Windows.Forms.ColumnHeader size;
		private System.Windows.Forms.ColumnHeader date;
		private System.ComponentModel.IContainer components;

		public Form1()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			GetINI();
			
		}

		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}
			}
			base.Dispose( disposing );
		}

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			this.components = new System.ComponentModel.Container();
			this.listView1 = new System.Windows.Forms.ListView();
			this.ID = new System.Windows.Forms.ColumnHeader();
			this.date = new System.Windows.Forms.ColumnHeader();
			this.size = new System.Windows.Forms.ColumnHeader();
			this.subject = new System.Windows.Forms.ColumnHeader();
			this.from = new System.Windows.Forms.ColumnHeader();
			this.to = new System.Windows.Forms.ColumnHeader();
			this.butLoad = new System.Windows.Forms.Button();
			this.textStat = new System.Windows.Forms.TextBox();
			this.butMsgLoad = new System.Windows.Forms.Button();
			this.listBox2 = new System.Windows.Forms.ListBox();
			this.label1 = new System.Windows.Forms.Label();
			this.label2 = new System.Windows.Forms.Label();
			this.butQuit = new System.Windows.Forms.Button();
			this.butAllLoad = new System.Windows.Forms.Button();
			this.butDel = new System.Windows.Forms.Button();
			this.textPop3 = new System.Windows.Forms.TextBox();
			this.label3 = new System.Windows.Forms.Label();
			this.label4 = new System.Windows.Forms.Label();
			this.textLogin = new System.Windows.Forms.TextBox();
			this.label5 = new System.Windows.Forms.Label();
			this.textPassword = new System.Windows.Forms.TextBox();
			this.listPop3 = new System.Windows.Forms.ListBox();
			this.butAdd = new System.Windows.Forms.Button();
			this.butDelete = new System.Windows.Forms.Button();
			this.butOpen = new System.Windows.Forms.Button();
			this.butSave = new System.Windows.Forms.Button();
			this.label6 = new System.Windows.Forms.Label();
			this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
			this.textBody = new System.Windows.Forms.TextBox();
			this.butReadToText = new System.Windows.Forms.Button();
			this.label7 = new System.Windows.Forms.Label();
			this.textMemo = new System.Windows.Forms.TextBox();
			this.SuspendLayout();
			// 
			// listView1
			// 
			this.listView1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listView1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listView1.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
																						this.ID,
																						this.date,
																						this.size,
																						this.subject,
																						this.from,
																						this.to});
			this.listView1.FullRowSelect = true;
			this.listView1.GridLines = true;
			this.listView1.HideSelection = false;
			this.listView1.Location = new System.Drawing.Point(0, 144);
			this.listView1.Name = "listView1";
			this.listView1.Size = new System.Drawing.Size(656, 96);
			this.listView1.TabIndex = 2;
			this.listView1.View = System.Windows.Forms.View.Details;
			// 
			// ID
			// 
			this.ID.Text = "N";
			this.ID.Width = 30;
			// 
			// date
			// 
			this.date.Text = "Дата";
			this.date.Width = 160;
			// 
			// size
			// 
			this.size.Text = "Размер";
			this.size.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
			// 
			// subject
			// 
			this.subject.Text = "Тема";
			this.subject.Width = 150;
			// 
			// from
			// 
			this.from.Text = "От";
			this.from.Width = 120;
			// 
			// to
			// 
			this.to.Text = "Кому";
			this.to.Width = 120;
			// 
			// butLoad
			// 
			this.butLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butLoad.Location = new System.Drawing.Point(0, 112);
			this.butLoad.Name = "butLoad";
			this.butLoad.Size = new System.Drawing.Size(72, 32);
			this.butLoad.TabIndex = 3;
			this.butLoad.Text = "Загрузить почту";
			this.butLoad.Click += new System.EventHandler(this.butLoad_Click);
			// 
			// textStat
			// 
			this.textStat.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textStat.Location = new System.Drawing.Point(1, 86);
			this.textStat.Name = "textStat";
			this.textStat.Size = new System.Drawing.Size(304, 20);
			this.textStat.TabIndex = 4;
			this.textStat.Text = "";
			// 
			// butMsgLoad
			// 
			this.butMsgLoad.Enabled = false;
			this.butMsgLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butMsgLoad.Location = new System.Drawing.Point(158, 112);
			this.butMsgLoad.Name = "butMsgLoad";
			this.butMsgLoad.Size = new System.Drawing.Size(64, 32);
			this.butMsgLoad.TabIndex = 5;
			this.butMsgLoad.Text = "Получить письмо";
			this.butMsgLoad.Click += new System.EventHandler(this.button2_Click);
			// 
			// listBox2
			// 
			this.listBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listBox2.Location = new System.Drawing.Point(64, 248);
			this.listBox2.Name = "listBox2";
			this.listBox2.Size = new System.Drawing.Size(592, 41);
			this.listBox2.TabIndex = 6;
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(0, 248);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(64, 16);
			this.label1.TabIndex = 7;
			this.label1.Text = "Вложения";
			// 
			// label2
			// 
			this.label2.Location = new System.Drawing.Point(0, 280);
			this.label2.Name = "label2";
			this.label2.Size = new System.Drawing.Size(64, 16);
			this.label2.TabIndex = 8;
			this.label2.Text = "Письмо";
			// 
			// butQuit
			// 
			this.butQuit.Enabled = false;
			this.butQuit.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butQuit.Location = new System.Drawing.Point(398, 112);
			this.butQuit.Name = "butQuit";
			this.butQuit.Size = new System.Drawing.Size(168, 32);
			this.butQuit.TabIndex = 9;
			this.butQuit.Text = "Закрыть почту (обязательно при удалении писем)";
			this.butQuit.Click += new System.EventHandler(this.button3_Click);
			// 
			// butAllLoad
			// 
			this.butAllLoad.Enabled = false;
			this.butAllLoad.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butAllLoad.Location = new System.Drawing.Point(71, 112);
			this.butAllLoad.Name = "butAllLoad";
			this.butAllLoad.Size = new System.Drawing.Size(88, 32);
			this.butAllLoad.TabIndex = 10;
			this.butAllLoad.Text = "Получить все письма";
			this.butAllLoad.Click += new System.EventHandler(this.butAllLoad_Click);
			// 
			// butDel
			// 
			this.butDel.Enabled = false;
			this.butDel.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butDel.Location = new System.Drawing.Point(350, 112);
			this.butDel.Name = "butDel";
			this.butDel.Size = new System.Drawing.Size(56, 32);
			this.butDel.TabIndex = 11;
			this.butDel.Text = "Удалить письмо";
			this.butDel.Click += new System.EventHandler(this.butDel_Click);
			// 
			// textPop3
			// 
			this.textPop3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textPop3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textPop3.Location = new System.Drawing.Point(71, 0);
			this.textPop3.Name = "textPop3";
			this.textPop3.Size = new System.Drawing.Size(192, 20);
			this.textPop3.TabIndex = 12;
			this.textPop3.Text = "";
			// 
			// label3
			// 
			this.label3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label3.Location = new System.Drawing.Point(0, 0);
			this.label3.Name = "label3";
			this.label3.Size = new System.Drawing.Size(72, 20);
			this.label3.TabIndex = 13;
			this.label3.Text = "Pop3";
			this.label3.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// label4
			// 
			this.label4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label4.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label4.Location = new System.Drawing.Point(0, 19);
			this.label4.Name = "label4";
			this.label4.Size = new System.Drawing.Size(72, 20);
			this.label4.TabIndex = 15;
			this.label4.Text = "Login";
			this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textLogin
			// 
			this.textLogin.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textLogin.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textLogin.Location = new System.Drawing.Point(71, 19);
			this.textLogin.Name = "textLogin";
			this.textLogin.Size = new System.Drawing.Size(192, 20);
			this.textLogin.TabIndex = 14;
			this.textLogin.Text = "";
			// 
			// label5
			// 
			this.label5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label5.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label5.Location = new System.Drawing.Point(0, 38);
			this.label5.Name = "label5";
			this.label5.Size = new System.Drawing.Size(72, 20);
			this.label5.TabIndex = 17;
			this.label5.Text = "Password";
			this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textPassword
			// 
			this.textPassword.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textPassword.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textPassword.Location = new System.Drawing.Point(71, 38);
			this.textPassword.Name = "textPassword";
			this.textPassword.Size = new System.Drawing.Size(192, 20);
			this.textPassword.TabIndex = 16;
			this.textPassword.Text = "";
			// 
			// listPop3
			// 
			this.listPop3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.listPop3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.listPop3.Location = new System.Drawing.Point(304, 0);
			this.listPop3.Name = "listPop3";
			this.listPop3.Size = new System.Drawing.Size(352, 106);
			this.listPop3.TabIndex = 20;
			// 
			// butAdd
			// 
			this.butAdd.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butAdd.Location = new System.Drawing.Point(269, 0);
			this.butAdd.Name = "butAdd";
			this.butAdd.Size = new System.Drawing.Size(28, 23);
			this.butAdd.TabIndex = 21;
			this.butAdd.Text = ">";
			this.butAdd.Click += new System.EventHandler(this.butAdd_Click);
			// 
			// butDelete
			// 
			this.butDelete.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butDelete.Location = new System.Drawing.Point(269, 31);
			this.butDelete.Name = "butDelete";
			this.butDelete.Size = new System.Drawing.Size(28, 23);
			this.butDelete.TabIndex = 22;
			this.butDelete.Text = "<";
			this.butDelete.Click += new System.EventHandler(this.butDelete_Click);
			// 
			// butOpen
			// 
			this.butOpen.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butOpen.Location = new System.Drawing.Point(221, 112);
			this.butOpen.Name = "butOpen";
			this.butOpen.Size = new System.Drawing.Size(64, 32);
			this.butOpen.TabIndex = 23;
			this.butOpen.Text = "Открыть письмо";
			this.butOpen.Click += new System.EventHandler(this.butOpen_Click);
			// 
			// butSave
			// 
			this.butSave.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butSave.Location = new System.Drawing.Point(269, 62);
			this.butSave.Name = "butSave";
			this.butSave.Size = new System.Drawing.Size(28, 23);
			this.butSave.TabIndex = 24;
			this.butSave.Text = "!";
			this.butSave.Click += new System.EventHandler(this.butSave_Click);
			// 
			// label6
			// 
			this.label6.Location = new System.Drawing.Point(0, 72);
			this.label6.Name = "label6";
			this.label6.TabIndex = 25;
			this.label6.Text = "Инфо";
			// 
			// textBody
			// 
			this.textBody.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
				| System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.textBody.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textBody.Location = new System.Drawing.Point(0, 296);
			this.textBody.Multiline = true;
			this.textBody.Name = "textBody";
			this.textBody.Size = new System.Drawing.Size(656, 56);
			this.textBody.TabIndex = 26;
			this.textBody.Text = "";
			// 
			// butReadToText
			// 
			this.butReadToText.FlatStyle = System.Windows.Forms.FlatStyle.Popup;
			this.butReadToText.Location = new System.Drawing.Point(284, 112);
			this.butReadToText.Name = "butReadToText";
			this.butReadToText.Size = new System.Drawing.Size(68, 32);
			this.butReadToText.TabIndex = 27;
			this.butReadToText.Text = "Прочитать в тексте";
			this.butReadToText.Click += new System.EventHandler(this.butReadToText_Click);
			// 
			// label7
			// 
			this.label7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.label7.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.label7.Location = new System.Drawing.Point(0, 56);
			this.label7.Name = "label7";
			this.label7.Size = new System.Drawing.Size(72, 20);
			this.label7.TabIndex = 29;
			this.label7.Text = "Примечание";
			this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
			// 
			// textMemo
			// 
			this.textMemo.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
			this.textMemo.Font = new System.Drawing.Font("Microsoft Sans Serif", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((System.Byte)(204)));
			this.textMemo.Location = new System.Drawing.Point(71, 56);
			this.textMemo.Name = "textMemo";
			this.textMemo.Size = new System.Drawing.Size(192, 20);
			this.textMemo.TabIndex = 28;
			this.textMemo.Text = "";
			// 
			// Form1
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(656, 349);
			this.Controls.Add(this.label7);
			this.Controls.Add(this.textMemo);
			this.Controls.Add(this.textBody);
			this.Controls.Add(this.textPassword);
			this.Controls.Add(this.textLogin);
			this.Controls.Add(this.textPop3);
			this.Controls.Add(this.textStat);
			this.Controls.Add(this.butReadToText);
			this.Controls.Add(this.butSave);
			this.Controls.Add(this.butOpen);
			this.Controls.Add(this.butDelete);
			this.Controls.Add(this.butAdd);
			this.Controls.Add(this.listPop3);
			this.Controls.Add(this.label5);
			this.Controls.Add(this.label4);
			this.Controls.Add(this.label3);
			this.Controls.Add(this.butDel);
			this.Controls.Add(this.butAllLoad);
			this.Controls.Add(this.butQuit);
			this.Controls.Add(this.label2);
			this.Controls.Add(this.label1);
			this.Controls.Add(this.listBox2);
			this.Controls.Add(this.butMsgLoad);
			this.Controls.Add(this.butLoad);
			this.Controls.Add(this.listView1);
			this.Controls.Add(this.label6);
			this.Name = "Form1";
			this.Text = "Demo";
			this.Closing += new System.ComponentModel.CancelEventHandler(this.Form1_Closing);
			this.ResumeLayout(false);

		}
		#endregion

		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main() 
		{
			Application.Run(new Form1());
		}


		private void SetINI()
		{
			StreamWriter sw = new StreamWriter(Application.StartupPath + @"\bvv_pop3.ini",false, System.Text.Encoding.Default);
			for(int i = 0; i < listPop3.Items.Count; i++)
			{
				sw.WriteLine("POP3§" + listPop3.Items[i].ToString());
			}
			sw.Close();
		}

		private void GetINI()
		{
			string[] s;
			//получение настроек из файла
			try
			{
				StreamReader sr = new StreamReader(Application.StartupPath + @"\bvv_pop3.ini",System.Text.Encoding.Default);
				string str = sr.ReadLine();
				while (str != null)
				{
					s = str.Split('§');
					switch(s[0])
					{
						case "POP3":
							listPop3.Items.Add(s[1]);;
							break;
					}
					str = sr.ReadLine();
				}
				sr.Close();

			}
			catch 
			{
//				MessageBox.Show("Ошибка чтения настроек");
			}
		}

		private void butLoad_Click(object sender, System.EventArgs e)
		{
			if(listPop3.SelectedIndex != -1)
			{
				butLoad.Enabled = false;
				listView1.Items.Clear();

				string[] s = listPop3.SelectedItem.ToString().Split('|');
				p = new Pop3(s[0],110,s[1],s[2]);
				if(!p.Connect()) 
				{
					butLoad.Enabled = true;
					textStat.Text = "Ошибка подключения " + p.error;
					return;
				}

				//STAT
				int[] c = p.GetMsgCountSize();
				textStat.Text = "STAT " + c[0].ToString() + " Size " + c[1].ToString();

				p.GetListMsgSize();
				for(int i = 1; i <= p.msgCount; i++)
				{
					p.GetTop(i);
					Pop3Msg m = p.GetMsg(i);
					ListViewItem item;
					item = new ListViewItem(m.n.ToString());
					item.SubItems.Add(m.date);
					item.SubItems.Add(m.size.ToString());
					item.SubItems.Add(m.subject);
					item.SubItems.Add(m.from);
					item.SubItems.Add(m.to);
					listView1.Items.Add(item);
				}


				butDel.Enabled = true;
				butAllLoad.Enabled = true;
				butQuit.Enabled = true;
				butMsgLoad.Enabled = true;
				butLoad.Enabled = true;
			}
			else 
				MessageBox.Show("Выберите сервер для подключения");
		}

		private void button2_Click(object sender, System.EventArgs e)
		{
			if(listView1.SelectedItems.Count == 0) return;
			int n = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
			Pop3Msg m = p.GetMessage(n);

			listBox2.Items.Clear();
			if(m != null)
			{
				
				for(int i = 0; i < m.attachments.Count; i++)
				{
					Pop3Att a = (Pop3Att)m.attachments[i];
					try
					{
						a.SaveAs(m.id + @"\" + a.filename);
					}
					catch
					{
					}
					listBox2.Items.Add(a.filename);
				}
			}
			else MessageBox.Show("Error");

			
		}

		private void button3_Click(object sender, System.EventArgs e)
		{
			if(p.Disconnect())
			{

				butDel.Enabled = false;
				butAllLoad.Enabled = false;
				butQuit.Enabled = false;
				butMsgLoad.Enabled = false;
				butLoad.Enabled = true;
			}
			else MessageBox.Show("Error");

		}

		private void butAllLoad_Click(object sender, System.EventArgs e)
		{
			p.GetAllMessage();
		}

		private void butDel_Click(object sender, System.EventArgs e)
		{
			int n = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
			p.MsgDelete(n);		
		}

		private void butSave_Click(object sender, System.EventArgs e)
		{
			SetINI();
		}

		private void Form1_Closing(object sender, System.ComponentModel.CancelEventArgs e)
		{
			SetINI();
		}

		private void butAdd_Click(object sender, System.EventArgs e)
		{
			listPop3.Items.Add(textPop3.Text + "|" + textLogin.Text + "|" + textPassword.Text + "|" + textMemo.Text);
		}

		private void butDelete_Click(object sender, System.EventArgs e)
		{
			if(listPop3.SelectedIndex != -1)
			{
				string[] s = listPop3.SelectedItem.ToString().Split('|');
				textPop3.Text = s[0];
				textLogin.Text = s[1];
				textPassword.Text = s[2];
				textMemo.Text = s[3];
				listPop3.Items.RemoveAt(listPop3.SelectedIndex);
			}
		}

		private void butOpen_Click(object sender, System.EventArgs e)
		{
			if(listView1.SelectedItems.Count == 0) return;
			int n = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
			Pop3Msg m = p.GetMsg(n);
			if(m != null)
			{
				p.MsgOpen(n);
			}
			else MessageBox.Show("Письмо не загружено");

		}

		private void butReadToText_Click(object sender, System.EventArgs e)
		{
			if(listView1.SelectedItems.Count == 0) return;
			int n = Convert.ToInt32(listView1.SelectedItems[0].SubItems[0].Text);
			Pop3Msg m = p.GetMsg(n);
			if(m != null)
			{
				textBody.Text = p.MsgGetText(n);
			}
			else MessageBox.Show("Письмо не загружено");		
		}
	}
}
