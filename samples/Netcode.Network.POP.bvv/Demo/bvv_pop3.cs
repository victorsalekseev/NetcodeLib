//bvv
using System;
using System.Net; 
using System.Net.Sockets; 
using System.IO; 
using System.Collections;
using System.Text;
using System.Drawing;
using System.Windows.Forms;
using System.Threading;
using System.Diagnostics;

//pop3+attachment+индикатор загрузки письма
namespace Bvv.Pop3
{
	//Pop3
	#region
	public class Pop3 : System.Windows.Forms.Form
	{
		//var
		#region
		int bLen = 1024;
		ArrayList messages = new ArrayList();
		public int msgCount = 0;
		public int topLen = 10;

		private NetworkStream ns;
		private StreamReader sr;
		private TcpClient pop3Clnt = null;

		private Byte[] outbytes;
		private string input;

		private string pop3server;
		private int port;
		private string login;
		private string passwd;

		public string error = "";
		#endregion

		private bool pbNext = true;

		private System.Windows.Forms.ProgressBar progressBar1;
		private System.Windows.Forms.Button butCancel;
		private System.ComponentModel.IContainer components;

		public Pop3(string pop3server, int port, string login, string passwd)
		{
			InitializeComponent();

			this.pop3server = pop3server;
			this.port = port;
			this.login = login;
			this.passwd = passwd;
		}
		
		/// <summary>
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
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
			this.progressBar1 = new System.Windows.Forms.ProgressBar();
			this.butCancel = new System.Windows.Forms.Button();
			this.SuspendLayout();
			// 
			// progressBar1
			// 
			this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right)));
			this.progressBar1.Location = new System.Drawing.Point(0, 0);
			this.progressBar1.Name = "progressBar1";
			this.progressBar1.Size = new System.Drawing.Size(336, 8);
			this.progressBar1.TabIndex = 0;
			// 
			// butCancel
			// 
			this.butCancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
			this.butCancel.Location = new System.Drawing.Point(112, 12);
			this.butCancel.Name = "butCancel";
			this.butCancel.Size = new System.Drawing.Size(104, 24);
			this.butCancel.TabIndex = 1;
			this.butCancel.Text = "Отмена";
			this.butCancel.Click += new System.EventHandler(this.butCancel_Click);
			// 
			// Pop3Form
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(336, 37);
			this.ControlBox = false;
			this.Controls.Add(this.butCancel);
			this.Controls.Add(this.progressBar1);
			this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
			this.Name = "Pop3Form";
			this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "ProgressBarForm";
			this.TopMost = true;
			this.ResumeLayout(false);

		}


		//ProgressBar
		#region
		public void SetProgressBar(int min, int max)
		{
			this.Show();
			this.Text = "Прием почты";
			progressBar1.Minimum = min;
			progressBar1.Maximum = max;//(int)(max / 1000);
			progressBar1.Value = min;
		}
		public void SetProgressBarValue(int val)
		{
			if(val >= progressBar1.Minimum && val <= progressBar1.Maximum)
			{
				progressBar1.Value = val;
//				Graphics g = this.CreateGraphics();
//				g.DrawLine(new Pen(Color.Red, 100), 10, 10, 100, 100);
			}
		}
		#endregion
		#endregion

		//Connect подключиться USER PASS
		#region
		public bool Connect()
		{
			error = "";
			string str;
			try
			{
				bLen = 32768;//8192;16384
//				pop3Clnt.SendBufferSize = 10;
//				pop3Clnt.ReceiveBufferSize = bLen;

				pop3Clnt = new TcpClient(pop3server,port);
				ns = pop3Clnt.GetStream();
				sr = new StreamReader(ns);
				str = sr.ReadLine();
				if(str == null) {error = "ошибка подключения"; return false;}
				if(!str.StartsWith("+OK")) {error = str; return false;}
			}
			catch(Exception e)
			{
				error = e.Message;
				return false;
			}

			input = "USER " + login + "\r\n";
			outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
			ns.Write(outbytes,0,outbytes.Length) ;
			str = sr.ReadLine();
			if(!str.StartsWith("+OK")) {error = str; return false;}

			input = "PASS " + passwd + "\r\n";
			outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
			ns.Write(outbytes,0,outbytes.Length);
			str = sr.ReadLine();
			if(!str.StartsWith("+OK")) {error = str; return false;}
			return true;
		}
		#endregion

		//Disconnect отключиться QUIT
		#region 
		public bool Disconnect()
		{
			error = "";
			try
			{
				string input = "QUIT" + "\r\n";
				Byte[] outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
				ns.Write(outbytes,0,outbytes.Length);
				if(sr.ReadLine().Split(' ')[0] == "+OK")
				{
					ns.Close();
					return true;
				}
				return false;
			}
			catch(Exception e)
			{
				error = e.Message;
				return false;
			}
		}
		#endregion

		//GetMsgCountSize получить количество писем и размер STAT
		#region
		public int[] GetMsgCountSize()
		{
			error = "";
			int[] c = new int[2]{-1, -1};
			try
			{
				input = "STAT" + "\r\n";
				outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
				ns.Write(outbytes,0,outbytes.Length);
				string[] s = sr.ReadLine().Split(' ');
				if(s[0] == "+OK")
				{
					c[0] = Convert.ToInt32(s[1]);
					msgCount = c[0];
					c[1] = Convert.ToInt32(s[2]);
				}
			}
			catch(Exception e)
			{
				error = e.Message;
			}
			return c;
		}
		#endregion

		//Noop проверка связи NOOP 
		#region
		public bool Noop()
		{
			try
			{
				input = "NOOP" + "\r\n";
				outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
				ns.Write(outbytes,0,outbytes.Length);
				string s = sr.ReadLine();
				return s.StartsWith("+OK");
			}
			catch
			{
				return false;
			}
		}
		#endregion

		//GetListMsgSize список писем с размерами LIST 
		#region
		public bool GetListMsgSize()
		{
			error = "";
			try
			{
				input = "LIST \r\n";
				outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
				ns.Write(outbytes,0,outbytes.Length);

				string line = sr.ReadLine();
				string[] s = line.Split(' ');
				if(s[0] == "+OK")
				{
					//уколичество писем s[1]
					do
					{
						line = sr.ReadLine();
						string[] line1 = line.Split(' ');
						if(line1.Length > 1)
						{
							int[] c = new int[2];
							c[0] = Convert.ToInt32(line1[0]);
							c[1] = Convert.ToInt32(line1[1]);

							Pop3Msg m = GetMsg(c[0]);
							if(m == null)
							{
								m = new Pop3Msg();
								m.n = c[0];
							}
							m.size = c[1];
							SetMsg(m);
						}
						//i++;
					} while (line != ".");
				} 
				else return false;
			}
			catch(Exception e)
			{
				error = e.Message;
				return false;
			}
			return true;
		}
		#endregion

		//GetTop прочитать заголовок письма TOP 
		#region
		public bool GetTop(int n)
		{
			error = "";
			ArrayList al = new ArrayList();
			int i = 0;
			//			int topLen = 10;
			//читаем целое письмо (с вложением) строки
			try
			{
				input = "TOP " + n.ToString() + " " + topLen.ToString() + "\r\n";
				outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
				ns.Write(outbytes,0,outbytes.Length);

				string line = sr.ReadLine();
				string[] s = line.Split(' ');
				if(s[0] == "+OK")
				{
					Pop3Msg m = GetMsg(n);
					if(m == null)
					{
						m = new Pop3Msg();
						m.n = n;
					}
					do
					{
						line = sr.ReadLine();
						if(line.ToLower().StartsWith("subject:")) m.subject = Decode(line.Substring(9));
						if(line.ToLower().StartsWith("from:")) m.from = Decode(line.Substring(6));
						if(line.ToLower().StartsWith("to:")) m.to = Decode(line.Substring(4));
						if(line.ToLower().StartsWith("message-id:")) m.id = line.Substring(12+1).Replace(">","");
						if(line.ToLower().StartsWith("date:")) m.date = line.Substring(6).Trim();

						SetMsg(m);
						al.Add(line);
						i++;
					} while (line != "."); // (i <= topLen);
				}
				else return false;
			}
			catch(Exception e)
			{
				error = e.Message;
				return false;
			}
			return true;;
		}
		//декодер
		private string Decode(string s)
		{
			StringBuilder retString=new StringBuilder();
			int old=0,start=0,stop;
			for(;;)
			{
				start=s.IndexOf("=?",start);
				if (start==-1)
				{
					retString.Append(s,old,s.Length-old);
					return retString.ToString();
				}
				stop=s.IndexOf("?=",start+2);
				if (stop==-1) 
					return s;
				retString.Append(s,old,start-old);
				retString.Append(DecodeOne(s.Substring(start,stop-start+2)));
				start=stop+2;
				old=stop+2;
			}
		}
		private string DecodeOne(string s)
		{
			char[] separator={'?'};
			string[] sArray=s.Split(separator);
			if (sArray[0].Equals("=")==false)
				return s;
			
			byte[] bArray;
			if (sArray[2]=="Q") //querystring
				bArray=GetByteArray(sArray[3]);
			else if (sArray[2]=="B")//base64
				bArray=Convert.FromBase64String(sArray[3]);
			else
				return s;
			Encoding encoding=Encoding.GetEncoding(sArray[1]); 
			return encoding.GetString(bArray);
		}
		private byte[] GetByteArray(string s)
		{
			byte[] buffer=new byte[s.Length];

			int bufferPosition=0;
			for(int i=0;i<s.Length;i++)
			{
				if (s[i]=='=' && s.Length != 1)
				{
					if (s[i+1]=='\r' && s[i+2]=='\n')
						bufferPosition--;
					else
						buffer[bufferPosition]=System.Convert.ToByte(s.Substring(i+1,2),16);
					i+=2;
				}
				else if (s[i]=='_')
					buffer[bufferPosition]=32;
				else
					buffer[bufferPosition]=(byte)s[i];
				bufferPosition++;
			}
			byte[] newArray=new byte[bufferPosition];
			Array.Copy(buffer,newArray,bufferPosition);
			return newArray;
		}

		#endregion

		//дальнейшие функции необходимог использовать только после 
		//выполнения функций GetListMsgSize, GetTop

		//GetAllMessage получить все письма сразу
		#region
		public void GetAllMessage()
		{
			for(int i = 1; i <= msgCount; i++) 
			{
				GetMessage(i);
			}
		}
		#endregion

		//GetMessage Получить письмо
		#region
		public Pop3Msg GetMessage(int n)
		{
			error = "";
			pbNext = true;
			int msgSize;
			Pop3Msg msgNew = GetMsg(n);//текущее письмо
			try
			{
				if (msgNew == null)
				{
					if (msgNew == null) return msgNew;
				}

				msgSize = msgNew.size;
				SetProgressBar(0, msgSize);

				msgNew.attachments = new ArrayList();
			
				//строки константы
				string CMP_c_t_e = "content-transfer-encoding:";
				string CMP_c_t = "content-type:";

				//читаем целое письмо (с вложением)
				input = "RETR " + n.ToString() + "\r\n";
				outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
				ns.Write(outbytes,0,outbytes.Length);

				string tmp = "";
				string t_e = "", charset = "";
				string filename = "";
				int startMime = 0;
				int startMime1 = 0;
				ArrayList al = new ArrayList();
			
				if(true)
				{
					//делаем полную закачку письма
					#region
					char[] bufferTmp = new char[bLen];
					Directory.CreateDirectory(msgNew.id);
					using(StreamWriter sw = new StreamWriter(msgNew.id + @"\mail.eml", false, System.Text.Encoding.Default))
					{
						int maxLen = 0;
						bool exit = false;
						do
						{
							if(bLen >= msgSize - maxLen)
							{
								bLen = msgSize - maxLen;
								exit = true;
								string hh;
								do
								{
									hh = sr.ReadLine();
									sw.Write(hh + "\r\n");
									maxLen += hh.Length + 2;
									SetProgressBarValue(maxLen);//sb.Length);
									this.Text = "Письмо " + n.ToString() + " из " + msgCount.ToString() + " получено " + maxLen.ToString() + " из " + msgSize.ToString() + " (" + Convert.ToString(Math.Round((decimal)((decimal)maxLen / (decimal)msgSize * 100))) + "%)";

								} while(hh != ".");
								break;
							}

							sr.ReadBlock(bufferTmp, 0, bLen);
							sw.Write(bufferTmp,0,bLen);
							maxLen += bLen;

							SetProgressBarValue(maxLen);//sb.Length);
							this.Text = "Письмо " + n.ToString() + " из " + msgCount.ToString() + " получено " + maxLen.ToString() + " из " + msgSize.ToString() + " (" + Convert.ToString(Math.Round((decimal)((decimal)maxLen / (decimal)msgSize * 100))) + "%)";
							if(!pbNext) 
							{
								this.Hide();
								return null;
							}
							Application.DoEvents();
						} while (!exit);
					}
					#endregion

					//письмо получено разбор его
					#region
					using(StreamReader srMsg = new StreamReader(msgNew.id + @"\mail.eml", Encoding.Default))
					{
						StringBuilder sb = new StringBuilder();
						do
						{
							try
							{
								tmp = srMsg.ReadLine();
								if(tmp.Length == 0)
								{
									startMime = startMime1;
									startMime1 = sb.Length;//line.Length;
								}
								else
								{
									string names = tmp.Substring(1);
									int k = names.IndexOf("name=");
									if(k != -1)
									{
										filename = Decode(names.Substring(k + 6, names.Length - 7 - k));
									}
									if(tmp.ToLower().StartsWith(CMP_c_t_e))	t_e = tmp.Substring(CMP_c_t_e.Length);
									if(tmp.ToLower().StartsWith(CMP_c_t))
									{
										if (tmp.IndexOf("text/plain")!=-1 || tmp.IndexOf("text/html")!=-1)
										{
											int ct = tmp.IndexOf("charset=");
											if (ct != -1) charset=tmp.Substring(ct + 8);
										}
									}
								}

								if(tmp.StartsWith("------"))
								{
									if(t_e.Length > 0 && filename.Length > 0 && startMime1 > 0)
									{
										Pop3Att b = new Pop3Att();
										if(startMime > 0) b.start = startMime; else b.start = startMime1;
										b.end = sb.Length - b.start; //line.Length - b.start;//line.Length + tmp.Length - b.start;
										b.t_e = t_e;
										b.filename = filename;
										b.charset = charset;
										al.Add(b);
									}
									t_e = "";
									filename = ""; 
									startMime = 0;
									startMime1 = 0;
								}
							
								sb.Append(tmp);
							}
							catch(Exception e)
							{
								string g = e.Message;
							}
						} while (tmp != ".");

						//список аттачментов
						for(int i = 0; i < al.Count; i++)
						{
							Pop3Att b = (Pop3Att)al[i];
							if(b.end > 0)
							{
								string str = sb.ToString().Substring(b.start, b.end);//line.Substring(b.start, b.end);
								b.mime = str;
								msgNew.attachments.Add(b);
							}
						}
					}
					#endregion

					messages.Add(msgNew);
					this.Hide();
					return msgNew;
				}
			}
			catch(Exception e)
			{
				error = e.Message;
				return null;
			}
		}
		#endregion

		//GetMsg получить объект письмо
		#region
		public Pop3Msg GetMsg(int n)
		{
			Pop3Msg m;
			for(int i = 0; i < messages.Count; i++)
			{
				m = (Pop3Msg)messages[i];
				if(m.n == n) return m;
			}
			return null;
		}
		#endregion

		//SetMsg записать объект письмо
		#region
		private void SetMsg(Pop3Msg m)
		{
			for(int i = 0; i < messages.Count; i++)
			{
				Pop3Msg m1 = (Pop3Msg)messages[i];
				if(m1.n == m.n) 
				{
					messages[i] = m;
					return;
				}
			}
			messages.Add(m);
		}
		#endregion

		//MsgOpen открыть письмо 
		#region
		public bool MsgOpen(int n)
		{
			error = "";
			Pop3Msg m = GetMsg(n);
			if(m != null)
			{
				try
				{
					ProcessStartInfo pInfo = new ProcessStartInfo(m.id + @"\mail.eml");
					Process.Start(pInfo);
					return true;
				}
				catch(Exception e)
				{
					error = e.Message;
					return false;
				}
			}
			return false;
		}
		#endregion

		//MsgGetText получить письмо в тексте
		#region
		public string MsgGetText(int n)
		{
			Pop3Msg m = GetMsg(n);
			if(m != null)
			{
				using(StreamReader srMsg = new StreamReader(m.id + @"\mail.eml", Encoding.Default))
				{
					StringBuilder sb = new StringBuilder();
					return srMsg.ReadToEnd();

				}
			}
			return "";
		}
		#endregion

		//MsgDelete удалить письмо DELE
		//полное удаление происходит только после метода Disconnect
		#region
		public bool MsgDelete(int n)
		{
			error = "";
			try
			{
				input = "DELE " + n.ToString() + "\r\n";
				outbytes = Encoding.ASCII.GetBytes(input.ToCharArray());
				ns.Write(outbytes,0,outbytes.Length);
				//string line = sr.ReadLine();
				string s = sr.ReadLine();
				return s.StartsWith("+OK");
			}
			catch(Exception e)
			{
				error = e.Message;
				return false;
			}
		}
		#endregion

		//GetMsgStrings получить письмо в строковый масив RETR
		#region
		public ArrayList GetMsgStrings(int n)
		{
			ArrayList al = new ArrayList();
			int i = 0;
			//читаем целое письмо (с вложением) масив строк
			input = "RETR " + n.ToString() + "\r\n";
			outbytes = System.Text.Encoding.ASCII.GetBytes(input.ToCharArray());
			ns.Write(outbytes,0,outbytes.Length);

			string line = sr.ReadLine();
			string[] s = line.Split(' ');
			if(s[0] == "+OK")
			{
				//размер письма s[1]
				do
				{
					line = sr.ReadLine();
					al.Add(line);
					i++;
				} while (line != ".");
			}
			return al;
		}
		#endregion


		private void butCancel_Click(object sender, System.EventArgs e)
		{
			pbNext = false;
		}

	}

	#endregion

	//Pop3Msg
	#region
	public class Pop3Msg
	{
		public int n;
		public int size;
		public string top;
		public string from;
		public string to;
		public string subject;		
		public string id;
		public string date;
		public ArrayList attachments;
	}
	#endregion

	//Pop3Att
	#region
	public class Pop3Att
	{
		public string name;
		public int start;
		public int end;
		public string t_e;
		public string filename;
		public string mime;
		public string charset;
		
		public void SaveAs(string fileName)
		{
			bool flag = false;
			byte[] data = null;
			if (t_e.IndexOf("base64")!=-1)
			{
				data=Convert.FromBase64String(mime);
				flag = true;
			}
			else
				if (t_e.IndexOf("quoted-printable")!=-1)
			{
				data=GetByteArray(mime);
				flag = true;
			}
			else
			{
				if (t_e.IndexOf("8bit")!=-1)
				{
					data=Encoding.Default.GetBytes(Change(mime,charset));
					flag = true;
				}
				else
				{
					data=Encoding.Default.GetBytes(mime);
					flag = true;
				}
			}
			if(flag)
			{
				FileStream fs;
				try
				{
					fs = new FileStream(fileName,FileMode.OpenOrCreate);
				}
				catch
				{
					fs = new FileStream("русскоеимя",FileMode.OpenOrCreate);
				}
				BinaryWriter BW = new BinaryWriter(fs);

				BW.Write(data);
				BW.Close();
			}
		}
		private byte[] GetByteArray(string s)
		{
			byte[] buffer=new byte[s.Length];

			int bufferPosition=0;
			for(int i=0;i<s.Length;i++)
			{
				if (s[i]=='=' && s.Length != 1)
				{
					if (s[i+1]=='\r' && s[i+2]=='\n')
						bufferPosition--;
					else
						buffer[bufferPosition]=System.Convert.ToByte(s.Substring(i+1,2),16);
					i+=2;
				}
				else if (s[i]=='_')
					buffer[bufferPosition]=32;
				else
					buffer[bufferPosition]=(byte)s[i];
				bufferPosition++;
			}
			byte[] newArray=new byte[bufferPosition];
			Array.Copy(buffer,newArray,bufferPosition);
			return newArray;
		}
		private string Change(string text,string charset)
		{
			if (charset==null || charset=="")
				return text;
			byte[] b=Encoding.Default.GetBytes(text);
			return new string(Encoding.GetEncoding(charset).GetChars(b));
		}
	}
	#endregion
}
