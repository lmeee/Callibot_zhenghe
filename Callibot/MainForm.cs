using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.IO.Ports;
using System.Threading;
using System.Timers;
using System.Diagnostics;
using System.Collections;
using System.Runtime.InteropServices;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace Callibot
{
    public partial class MainForm : Form
    {
        Motor Arm = new Motor();
        Robot robot = new Robot();
        Strokes strokes = new Strokes();
        SerialPort sp = new SerialPort();
        SerialPort sp1 = new SerialPort();
        System.Timers.Timer t = new System.Timers.Timer(300);
        StreamWriter sw;
        Thread thread;
        byte[] currentposition = new byte[2];
        byte[] currentspeed = new byte[2];
        int count = 0;
        int image_width = 1280;
        int image_height = 960;
        bool comSwitch = true;
        bool survoSwitch = true;
        bool teachingSwitch = true;

        //为流程化教学准备的变量
        int successNum;
        int failNum;
        int error_num = 0;
        double horizontalInterval = 1480.00;
        public MainForm()
        {
            InitializeComponent();
            OpenCOM.Text = "COM ON";
            OpenCOM.BackColor = Color.Red;
            sp.PortName = "COM4";
            sp.BaudRate = 57600;
            sp.Parity = System.IO.Ports.Parity.None;
            sp.StopBits = System.IO.Ports.StopBits.One;
            sp.DataBits = 8;
            sp1.PortName = "COM5";
            sp1.BaudRate = 57600;
            sp1.Parity = System.IO.Ports.Parity.None;
            sp1.StopBits = System.IO.Ports.StopBits.One;
            sp1.DataBits = 8;
            sp.Open();
            sp1.Open();
            comSwitch = false;
        }

        private void OpenCOM_Click(object sender, EventArgs e)
        {
            if (comSwitch)
            {
                OpenCOM.Text = "COM ON";
                OpenCOM.BackColor = Color.Red;
                sp.PortName = "COM4";
                sp.BaudRate = 57600;
                sp.Parity = System.IO.Ports.Parity.None;
                sp.StopBits = System.IO.Ports.StopBits.One;
                sp.DataBits = 8;
                sp1.PortName = "COM5";
                sp1.BaudRate = 57600;
                sp1.Parity = System.IO.Ports.Parity.None;
                sp1.StopBits = System.IO.Ports.StopBits.One;
                sp1.DataBits = 8;
                sp.Open();
                sp1.Open();
                comSwitch = false;
            }
            else
            {
                OpenCOM.Text = "COM OFF";
                OpenCOM.BackColor = SystemColors.ButtonFace;
                comSwitch = true;
                sp.Close();
                sp1.Close();
            }
        }

        private void SetID_Click(object sender, EventArgs e)
        {
            if (MotorID.Text == "")
            {
                MessageBox.Show("No input!");
            }
            else
            {
                int temp = Convert.ToInt32(MotorID.Text);
                byte[] ID = BitConverter.GetBytes(temp);
                if (ID[0] >= 1 && ID[0] <= 6)
                {
                    byte[] command = Arm.SetID(ID[0]);
                    sp.Write(command, 0, command.Length);
                }
                else
                {
                    MessageBox.Show("Input exceeds limits!");
                }
            }
            Thread.Sleep(20);
            sp.ReadExisting();
        }

        private void CurrentPosition_Click(object sender, EventArgs e)
        {
            if (MotorID.Text == "")
            {
                MessageBox.Show("No input!");
            }
            else
            {
                int temp = Convert.ToInt32(MotorID.Text);
                byte[] ID = BitConverter.GetBytes(temp);
                byte[] buffer = new byte[8];
                if (ID[0] >= 1 && ID[0] <= 6)
                {
                    byte[] command = Arm.GetPosition(ID[0]);
                    sp.Write(command, 0, command.Length);
                    Thread.Sleep(100);
                    sp.Read(buffer, 0, 8);
                    currentposition[0] = buffer[5];
                    currentposition[1] = buffer[6];
                    textBox2.Text = buffer[5].ToString("X");
                    textBox1.Text = buffer[6].ToString("X");
                }
                else
                {
                    MessageBox.Show("Input exceeds limits!");
                }
            }
        }

        private void ServoOn_Click(object sender, EventArgs e)
        {
            if (survoSwitch)
            {
                survoSwitch = false;
                ServoOn.Text = "Servo ON";
                ServoOn.BackColor = Color.Red;
                byte[] command = Arm.Enable();
                sp.Write(command, 0, command.Length);
                Thread.Sleep(20);
                sp.ReadExisting();
            }
            else
            {
                survoSwitch = true;
                ServoOn.Text = "Servo OFF";
                ServoOn.BackColor = SystemColors.ButtonFace;
                byte[] command = Arm.Disable();
                sp.Write(command, 0, command.Length);
                Thread.Sleep(20);
                sp.ReadExisting();
            }

        }

        private void Move_Click(object sender, EventArgs e)
        {
            if (Joint1.Checked)
            {
                if (Joint1Target.Text == "" || Joint1Speed.Text == "")
                {
                    MessageBox.Show("No input!");
                }
                else
                {
                    double target = Convert.ToDouble(Joint1Target.Text);
                    double speed = Convert.ToDouble(Joint1Speed.Text);
                    byte[] command = Arm.MoveTo_MX(0x01, target, speed);
                    sp.Write(command, 0, command.Length);
                    Thread.Sleep(50);
                }
            }

            if (Joint2.Checked)
            {
                if (Joint2Target.Text == "" || Joint2Speed.Text == "")
                {
                    MessageBox.Show("No input!");
                }
                else
                {
                    double target = Convert.ToDouble(Joint2Target.Text);
                    double speed = Convert.ToDouble(Joint2Speed.Text);
                    byte[] command = Arm.MoveTo_MX(0x02, target, speed);
                    sp.Write(command, 0, command.Length);
                    Thread.Sleep(50);
                }
            }

            if (Joint3.Checked)
            {
                if (Joint3Target.Text == "" || Joint3Speed.Text == "")
                {
                    MessageBox.Show("No input!");
                }
                else
                {
                    double target = Convert.ToDouble(Joint3Target.Text);
                    double speed = Convert.ToDouble(Joint3Speed.Text);
                    byte[] command = Arm.MoveTo_MX(0x03, target, speed);
                    sp.Write(command, 0, command.Length);
                    Thread.Sleep(50);
                }
            }

            if (Joint4.Checked)
            {
                if (Joint4Target.Text == "" || Joint4Speed.Text == "")
                {
                    MessageBox.Show("No input!");
                }
                else
                {
                    double target = Convert.ToDouble(Joint4Target.Text);
                    double speed = Convert.ToDouble(Joint4Speed.Text);
                    byte[] command = Arm.MoveTo_MX(0x04, target, speed);
                    sp.Write(command, 0, command.Length);
                    Thread.Sleep(50);
                }
            }

            if (Joint5.Checked)
            {
                if (Joint5Target.Text == "" || Joint5Speed.Text == "")
                {
                    MessageBox.Show("No input!");
                }
                else
                {
                    double target = Convert.ToDouble(Joint5Target.Text);
                    double speed = Convert.ToDouble(Joint5Speed.Text);
                    byte[] command = Arm.MoveTo_MX(0x05, target, speed);
                    sp.Write(command, 0, command.Length);
                    Thread.Sleep(50);
                }
            }

            if (Joint6.Checked)
            {
                if (Joint6Target.Text == "" || Joint6Speed.Text == "")
                {
                    MessageBox.Show("No input!");
                }
                else
                {
                    double target = Convert.ToDouble(Joint6Target.Text);
                    double speed = Convert.ToDouble(Joint6Speed.Text);
                    byte[] command = Arm.MoveTo_MX(0x06, target, speed);
                    //byte[] command = Arm.MoveTo_RX(0x06, target, speed);
                    sp.Write(command, 0, command.Length);
                    Thread.Sleep(50);
                }
            }

            if (!Joint1.Checked && !Joint2.Checked && !Joint3.Checked && !Joint4.Checked && !Joint5.Checked && !Joint6.Checked)
            {
                MessageBox.Show("Please choose joints!");
            }
            Thread.Sleep(20);
            sp.ReadExisting();
        }

        private void SyncMove_Click(object sender, EventArgs e)
        {
            double[] target = { Convert.ToDouble(Joint1Target.Text), Convert.ToDouble(Joint2Target.Text), Convert.ToDouble(Joint3Target.Text), Convert.ToDouble(Joint4Target.Text), Convert.ToDouble(Joint5Target.Text), Convert.ToDouble(Joint6Target.Text) };
            double[] speed = { Convert.ToDouble(Joint1Speed.Text), Convert.ToDouble(Joint2Speed.Text), Convert.ToDouble(Joint3Speed.Text), Convert.ToDouble(Joint4Speed.Text), Convert.ToDouble(Joint5Speed.Text), Convert.ToDouble(Joint6Speed.Text) };
            byte[] command = Arm.SyncMove_MX(target, speed);
            sp.Write(command, 0, command.Length);
            Thread.Sleep(20);
            sp.ReadExisting();
        }

        private void Teaching_Click(object sender, EventArgs e)
        {
            if (teachingSwitch)
            {
                teachingSwitch = false;
                Teaching.Text = "Teaching...";
                Teaching.BackColor = Color.Red;
                sw = File.CreateText("t.txt");
                //newFile = System.IO.Directory.CreateDirectory("halo")    later append it into the directory and seporate txt files
                t.Elapsed += new System.Timers.ElapsedEventHandler(StartTeaching);
                t.Start();
            }
            else
            {
                Teaching.Text = "Teach";
                Teaching.BackColor = SystemColors.ButtonFace;
                teachingSwitch = true;
                t.Stop();
                count = 0;
                Thread.Sleep(1000);
                sw.Close();

                //DataCut w = new DataCut("t");
                //w.Correctmode();
                //DataCut c = new DataCut("tCorrect");
                //c.Washmode();

                MessageBox.Show("File saved");
            }

        }

        private void StartTeaching(object source, ElapsedEventArgs e)
        {
            byte[] command;
            byte[] buffer = new byte[8];    //缓存机器人传来的数据
            byte[] ID;
            string temp;

            count++;
            temp = "Frame " + Convert.ToString(count);
            sw.WriteLine(temp);
            for (int i = 1; i < 7; i++)
            {
                ID = BitConverter.GetBytes(i);  // WHY???
                command = Arm.GetPosition(ID[0]);
                sp.Write(command, 0, command.Length);
                Thread.Sleep(10);
                sp.Read(buffer, 0, 8);
                //sw.WriteLine("num" + Convert.ToString(i));
                if (buffer[0] == 0xFF && buffer[1] == 0xFF && buffer[2] == ID[0] && buffer[4] == 0x00)
                {//数据缺失不在这里
                    //sw.WriteLine("num" + Convert.ToString(i));
                    sw.WriteLine(buffer[5]);
                    sw.WriteLine(buffer[6]);
                }
                else
                {
                    sw.WriteLine("error");
                    break;
                }
                Thread.Sleep(10);

            }
            sp.ReadExisting();
        }

        private void Play_Text(string text_name, double speed)
        {
            
            StreamReader sr = File.OpenText(text_name);
            //my code
            sr.ReadLine();
            byte[] position = new byte[12];
            //byte[] last_position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            byte[] last_position = new byte[12];
            for (int i = 0; i < 12; i++)
            {
                last_position[i] = BitConverter.GetBytes(Convert.ToInt32(sr.ReadLine()))[0];
            }
            Initial_Position(last_position);
            sr.Close();
            Thread.Sleep(1000);
            sr = File.OpenText(text_name);





            // original code
            //byte[] position = new byte[12];
            //byte[] last_position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            double[] RPM = new double[6];
            double[] last_angle = new double[6];
            double[] angle = new double[6];
            last_angle = robot.ByteToAngle(last_position);
            byte[] command;
            string s;
            int flag = 0;
            byte[] buffer = new byte[10];
            int angle3, angle5;
            int last_angle3, last_angle5;
            int delta;
            double temp1 = 0;
            byte[] temp = new byte[4];
            temp[0] = last_position[4];
            temp[1] = last_position[5];
            temp[2] = 0x00;
            temp[3] = 0x00;
            last_angle3 = BitConverter.ToInt32(temp, 0);
            temp[0] = last_position[8];
            temp[1] = last_position[9];
            last_angle5 = BitConverter.ToInt32(temp, 0);
            int count1 = 0;
            while (true)
            {
                count1++;
                if ((s = sr.ReadLine()) != null)    //for that frame string
                {
                    if (s == "error")
                        flag = 2;
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if ((s = sr.ReadLine()) != null)
                            {
                                if (s == "error")
                                {
                                    flag = 2;
                                    break;
                                }
                                position[2 * i] = BitConverter.GetBytes(Convert.ToInt32(s))[0];
                            }
                            else
                            {
                                flag = 1;
                                break;
                            }
                            if ((s = sr.ReadLine()) != null)
                            {
                                if (s == "error")
                                {
                                    flag = 2;
                                    break;
                                }
                                position[2 * i + 1] = BitConverter.GetBytes(Convert.ToInt32(s))[0];
                            }
                            else
                            {
                                flag = 1;
                                break;
                            }
                        }
                    }
                }
                else
                    break;
                if (flag == 1)
                    break;
                else if (flag == 2)
                {
                    flag = 0;
                    continue;
                }
                else if (flag == 0)
                {
                    temp[0] = position[4];
                    temp[1] = position[5];
                    angle3 = BitConverter.ToInt32(temp, 0);
                    if (angle3 >= last_angle3)
                        temp1 = 12.124 * Math.Cos(Math.PI - angle3 * Math.PI / 2048) + 0.902;
                    else
                        temp1 = 21.834 * Math.Cos(Math.PI - angle3 * Math.PI / 2048) + 2.6965;
                    last_angle3 = angle3;
                    delta = (int)Math.Round(temp1);
                    angle3 = angle3 - delta;
                    position[4] = BitConverter.GetBytes(angle3)[0];
                    position[5] = BitConverter.GetBytes(angle3)[1];

                    temp[0] = position[8];
                    temp[1] = position[9];
                    angle5 = BitConverter.ToInt32(temp, 0);
                    if (angle5 > last_angle5)
                        temp1 = 2.7944 * Math.Cos(Math.PI - angle5 * Math.PI / 2048) + 2.1146;
                    else if (angle5 < last_angle5)
                        temp1 = 3.0771 * Math.Cos(Math.PI - angle5 * Math.PI / 2048) + 3.88;
                    last_angle5 = angle5;
                    delta = (int)Math.Round(temp1);
                    angle5 = angle5 - delta;
                    position[8] = BitConverter.GetBytes(angle5)[0];
                    position[9] = BitConverter.GetBytes(angle5)[1];

                    angle = robot.ByteToAngle(position);
                    for (int j = 0; j < 6; j++)
                    {
                        RPM[j] = speed * Math.Abs(last_angle[j] - angle[j]);
                        if (RPM[j] == 0)
                            RPM[j] = RPM[j] + 1;
                    }
                    command = Arm.SyncMove(position, RPM);
                    sp.Write(command, 0, command.Length);
                    //Thread.Sleep(200);
                    Thread.Sleep((int)(160 / speed));
                    sp.ReadExisting();
                    last_angle = angle;
                }
            }
            sr.Close();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (sp.IsOpen)
            {
                sp.ReadExisting();
                sp.Close();
            }

            //thread.Abort();
            //capture.Stop();

            if (sp1.IsOpen)
            {
                sp1.ReadExisting();
                sp1.Close();
            }
            //if (!survoSwitch)
            //{
            //    survoSwitch = true;
            //    ServoOn.Text = "Servo OFF";
            //    ServoOn.BackColor = SystemColors.ButtonFace;
            //    byte[] command = Arm.Disable();
            //    sp.Write(command, 0, command.Length);
            //    Thread.Sleep(20);
            //    sp.ReadExisting();
            //}
            if (!comSwitch)
            {
                OpenCOM.Text = "COM OFF";
                OpenCOM.BackColor = SystemColors.ButtonFace;
                comSwitch = true;
                sp.Close();
                sp1.Close();
            }

        }

        private void Initial_Position(byte[] position)
        {
            byte[] ID, command;
            byte[] buffer = new byte[8];
            byte[] last_position = new byte[12];
            int flag = 0;
            double[] RPM = new double[6];
            double[] angle = new double[6];
            double[] last_angle = new double[6];
            byte[] new_position = new byte[12];
            for (int i = 0; i < 12; i++)
                new_position[i] = position[i];
            int angle3, angle5;
            int last_angle3, last_angle5;
            int delta;
            double temp1 = 0;
            byte[] temp = new byte[4];
            temp[2] = 0x00;
            temp[3] = 0x00;
            for (int j = 1; j < 7; j++)
            {
                ID = BitConverter.GetBytes(j);
                command = Arm.GetPosition(ID[0]);
                sp.Write(command, 0, command.Length);
                Thread.Sleep(20);
                sp.Read(buffer, 0, 8);
                if (buffer[0] == 0xFF && buffer[1] == 0xFF && buffer[4] == 0x00)
                {
                    last_position[2 * j - 2] = buffer[5];
                    last_position[2 * j - 1] = buffer[6];
                }
                else
                {
                    flag = 1;
                    break;
                }
            }
            if (flag == 0)
            {
                angle = robot.ByteToAngle(position);
                last_angle = robot.ByteToAngle(last_position);
                for (int j = 0; j < 6; j++)
                {
                    RPM[j] = Math.Abs(last_angle[j] - angle[j]) * 0.2; //改变initial的角速度，原来为240/360.
                    if (RPM[j] == 0)
                        RPM[j] = RPM[j] + 1;
                }
                temp[0] = last_position[4];
                temp[1] = last_position[5];
                last_angle3 = BitConverter.ToInt32(temp, 0);
                temp[0] = position[4];
                temp[1] = position[5];
                angle3 = BitConverter.ToInt32(temp, 0);
                if (angle3 > last_angle3)
                    temp1 = 12.124 * Math.Cos(Math.PI - angle3 * Math.PI / 2048) + 0.902;
                else if (angle3 < last_angle3)
                    temp1 = 21.834 * Math.Cos(Math.PI - angle3 * Math.PI / 2048) + 2.6965;
                delta = (int)Math.Round(temp1);
                angle3 = angle3 - delta;
                new_position[4] = BitConverter.GetBytes(angle3)[0];
                new_position[5] = BitConverter.GetBytes(angle3)[1];

                temp[0] = last_position[8];
                temp[1] = last_position[9];
                last_angle5 = BitConverter.ToInt32(temp, 0);
                temp[0] = position[8];
                temp[1] = position[9];
                angle5 = BitConverter.ToInt32(temp, 0);
                if (angle5 > last_angle5)
                    temp1 = 2.7944 * Math.Cos(Math.PI - angle5 * Math.PI / 2048) + 2.1146;
                else if (angle5 < last_angle5)
                    temp1 = 3.0771 * Math.Cos(Math.PI - angle5 * Math.PI / 2048) + 3.88;
                delta = (int)Math.Round(temp1);
                angle5 = angle5 - delta;
                new_position[8] = BitConverter.GetBytes(angle5)[0];
                new_position[9] = BitConverter.GetBytes(angle5)[1];

                command = Arm.SyncMove(new_position, RPM);
                sp.Write(command, 0, command.Length);
                Thread.Sleep(20);
                sp.ReadExisting();
            }
        }

        private void Initial_Click(object sender, EventArgs e)
        {
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            //DataCut wash = new DataCut("rotate.txt");
            //byte[] position = wash.FindOptimalZ(20);
            Initial_Position(position);
        }

        private void ForwardKinematics_Click(object sender, EventArgs e)
        {
            StreamReader sr = File.OpenText("initial.txt");
            sw = File.CreateText("initial_test.txt");
            //StreamWriter sw1 = File.CreateText("l6_original.txt");
            string s;
            double temp, temp1;
            byte[] position = new byte[12];
            double[] Pos = new double[3];
            double[] Ore1 = new double[3];
            double[] Ore2 = new double[3];
            int flag = 0;
            int count1 = 0;
            double[] angle = new double[6];
            while (true)
            {
                count1++;
                if ((s = sr.ReadLine()) != null)
                {
                    if (s == "error")
                        flag = 2;
                    else
                    {
                        for (int i = 0; i < 6; i++)
                        {
                            if ((s = sr.ReadLine()) != null)
                            {
                                if (s == "error")
                                {
                                    flag = 2;
                                    break;
                                }
                                position[2 * i] = BitConverter.GetBytes(Convert.ToInt32(s))[0];
                            }
                            else
                            {
                                flag = 1;
                                break;
                            }
                            if ((s = sr.ReadLine()) != null)
                            {
                                if (s == "error")
                                {
                                    flag = 2;
                                    break;
                                }
                                position[2 * i + 1] = BitConverter.GetBytes(Convert.ToInt32(s))[0];
                            }
                            else
                            {
                                flag = 1;
                                break;
                            }
                        }
                    }
                }
                else
                    break;
                if (flag == 1)
                    break;
                else if (flag == 2)
                {
                    flag = 0;
                    continue;
                }
                else if (flag == 0)
                {
                    //angle = robot.ByteToAngle(position);
                    //for (int i = 0; i < 6; i++)
                    //{
                    //    sw.Write(angle[i]);
                    //    sw.Write(" ");
                    //}
                    //sw.Write("\r\n");
                    if (count1 == 1)//11 || count1 == 18 || count1 == 35 || count1 == 41 || count1 == 49 || count1 == 54 || count1 == 62 || count1 == 73 || count1 == 94 || count1 == 105)// || count1 == 150)// || count1 == 122 || count1 == 136 || count1 == 143)
                    {
                        Matrix<double> test = robot.ForwardKinematics(position);
                        for (int i = 0; i < 3; i++)
                        {
                            sw.Write(test[i, 3]);
                            sw.Write(" ");
                            //sw1.Write(test[i, 3]);
                            //sw1.Write(" ");
                        }
                        //for (int i = 0; i < 3; i++)
                        //{
                        //    sw1.Write(test[i, 2]);
                        //    sw1.Write(" ");
                        //}
                        //for (int i = 0; i < 3; i++)
                        //{
                        //    sw1.Write(test[i, 0]);
                        //    sw1.Write(" ");
                        //}
                        //sw1.Write("\r\n ");
                        temp = Math.Atan2(-test[2, 0], Math.Sqrt(test[0, 0] * test[0, 0] + test[1, 0] * test[1, 0]));
                        sw.Write(temp);
                        sw.Write(" ");
                        temp1 = Math.Atan2(test[1, 0] / Math.Cos(temp), test[0, 0] / Math.Cos(temp));
                        //if (count1 == 18)
                        //    temp1 = temp1 + 2 * Math.PI;
                        sw.Write(temp1);
                        sw.Write(" ");
                        temp1 = Math.Atan2(test[2, 1] / Math.Cos(temp), test[2, 2] / Math.Cos(temp));
                        sw.Write(temp1);
                        sw.Write(" ");
                        sw.Write("\r\n");
                    }
                }
            }
            sw.Close();
            //sw1.Close();
        }

        private void InverseKinematics_Click(object sender, EventArgs e)
        {
            double[] Pos = new double[3];
            double[] Ore1 = new double[3];
            double[] Ore2 = new double[3];
            byte[] position = new byte[12];
            double[] angle = new double[6];
            int count1 = 0;
            StreamReader sr = File.OpenText("brushMotion2.txt");
            StreamWriter sw1 = File.CreateText("hh14_LfD_Sim.txt");
            sw = File.CreateText("hh14_LfD.txt");
            string s, temp;
            while ((s = sr.ReadLine()) != null)
            {
                for (int i = 0; i < 3; i++)
                {
                    s = sr.ReadLine();
                    Pos[i] = Convert.ToDouble(s);
                }
                for (int i = 0; i < 3; i++)
                {
                    s = sr.ReadLine();
                    Ore1[i] = Convert.ToDouble(s);
                }
                for (int i = 0; i < 3; i++)
                {
                    s = sr.ReadLine();
                    Ore2[i] = Convert.ToDouble(s);
                }
                robot.InverseKinematics(Pos, Ore1, Ore2, ref position);
                count1++;
                temp = "Frame " + Convert.ToString(count1);
                sw.WriteLine(temp);
                for (int i = 0; i < 12; i++)
                    sw.WriteLine(position[i]);
                angle = robot.ByteToAngle(position);
                for (int i = 0; i < 6; i++)
                {
                    sw1.Write(angle[i]);
                    sw1.Write(" ");
                }
                sw1.Write("\r\n");
            }
            sw.Close();
            sw1.Close();
            sr.Close();
        }

        public int Cartesian_Move(double[] Position, double[] Orient1, double[] Orient2, ref byte[] last_angle)
        {
            byte[] angle = new byte[12];
            byte[] via_angle = new byte[12];
            double[] lastjoint = new double[6];
            double[] joint = new double[6];
            double[] viajoint = new double[6];
            //Matrix<double> ja = new Matrix<double>(6, 6);
            //Matrix<double> invja = new Matrix<double>(6, 6);
            //Matrix<double> velocity = new Matrix<double>(6,1);
            //Matrix<double> temp_velocity = new Matrix<double>(6, 1);
            //Matrix<double> w = new Matrix<double>(6, 1);
            double[] RPM = new double[6];
            double[] position = new double[3];
            double[] orient1 = new double[3];
            double[] orient2 = new double[3];
            double[] via_position = new double[3];
            double[] via_orient1 = new double[3];
            double[] via_orient2 = new double[3];
            double[] temp_position = new double[3];
            byte[] command;
            byte[] buffer = new byte[10];
            int[] speed = new int[6];
            byte[] ID;
            int flag = 0;
            bool status = false;
            sw = File.AppendText("teaching data.txt");
            for (int i = 0; i < Position.Length / 3 - 1; i++)
            {
                via_position[0] = Position[3 * i];
                via_position[1] = Position[3 * i + 1];
                via_position[2] = Position[3 * i + 2];
                via_orient1[0] = Orient1[3 * i];
                via_orient1[1] = Orient1[3 * i + 1];
                via_orient1[2] = Orient1[3 * i + 2];
                via_orient2[0] = Orient2[3 * i];
                via_orient2[1] = Orient2[3 * i + 1];
                via_orient2[2] = Orient2[3 * i + 2];
                position[0] = Position[3 * i + 3];
                position[1] = Position[3 * i + 4];
                position[2] = Position[3 * i + 5];
                orient1[0] = Orient1[3 * i + 3];
                orient1[1] = Orient1[3 * i + 4];
                orient1[2] = Orient1[3 * i + 5];
                orient2[0] = Orient2[3 * i + 3];
                orient2[1] = Orient2[3 * i + 4];
                orient2[2] = Orient2[3 * i + 5];
                //velocity[0, 0] = Velocity[6 * i];
                //velocity[1, 0] = Velocity[6 * i + 1];
                //velocity[2, 0] = Velocity[6 * i + 2];
                //velocity[3, 0] = Velocity[6 * i + 3];
                //velocity[4, 0] = Velocity[6 * i + 4];
                //velocity[5, 0] = Velocity[6 * i + 5];
                sw.WriteLine(i);
                if (robot.InverseKinematics(position, orient1, orient2, ref angle) == -1)
                {
                    MessageBox.Show("Unreachable!");
                    sw.Close();
                    return -1;
                }
                lastjoint = robot.ByteToAngle(last_angle);
                joint = robot.ByteToAngle(angle);
                if (robot.InverseKinematics(via_position, via_orient1, via_orient2, ref via_angle) == -1)
                {
                    MessageBox.Show("Unreachable!");
                    return -1;
                }
                viajoint = robot.ByteToAngle(via_angle);
                for (int j = 0; j < 6; j++)
                {
                    RPM[j] = Math.Abs(lastjoint[j] - joint[j]) * 900 / 360;
                    if (RPM[j] == 0)
                        RPM[j] = RPM[j] + 0.01;
                }
                command = Arm.SyncMove(angle, RPM);
                sp.Write(command, 0, command.Length);
                Thread.Sleep(10);
                sp.ReadExisting();
                //if (i < Position.Length / 3 - 2)
                {
                    do
                    {
                        for (int j = 1; j < 7; j++)
                        {
                            ID = BitConverter.GetBytes(j);
                            command = Arm.GetPosition(ID[0]);
                            sp.Write(command, 0, command.Length);
                            Thread.Sleep(10);
                            sp.Read(buffer, 0, 8);
                            if (buffer[0] == 0xFF && buffer[1] == 0xFF && buffer[4] == 0x00)
                            {
                                last_angle[2 * j - 2] = buffer[5];
                                last_angle[2 * j - 1] = buffer[6];
                            }
                            else
                            {
                                flag = 1;
                                break;
                            }
                        }
                        if (flag == 1)
                        {
                            flag = 0;
                            continue;
                        }
                        lastjoint = robot.ByteToAngle(last_angle);
                        for (int j = 0; j < 6; j++)
                        {
                            status = ((lastjoint[j] <= joint[j] && lastjoint[j] >= viajoint[j]) || (lastjoint[j] >= joint[j] && lastjoint[j] <= viajoint[j])) || status;
                        }
                        status = !status;
                    } while (status);
                }
            }
            sw.Close();
            last_angle = angle;
            return 0;
        }

        private byte[] StepMoter_Move(byte ID, double position, int frequency)
        {
            byte[] packet = new byte[8];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = ID;
            if (position > 0)
                packet[3] = 0x00;
            else
            {
                packet[3] = 0x01;
                position = -position;
            }
            int numbers = (int)(2 * position);
            packet[4] = BitConverter.GetBytes(numbers)[0];
            packet[5] = BitConverter.GetBytes(numbers)[1];
            packet[6] = BitConverter.GetBytes(frequency)[0];
            packet[7] = BitConverter.GetBytes(frequency)[1];
            return packet;
        }

        private void TrailMove_Click(object sender, EventArgs e)
        {
            double position = Convert.ToDouble(textBox1.Text);
            byte[] command = StepMoter_Move(0x01, position, 1000);
            sp1.Write(command, 0, command.Length);
        }

        private void RollingPaper_Click(object sender, EventArgs e)
        {
            double position = Convert.ToDouble(textBox3.Text);
            byte[] command = StepMoter_Move(0x02, position, 1000);
            sp1.Write(command, 0, command.Length);
        }

        private void PlayExisting_Click(object sender, EventArgs e)
        {
            Play_Text(textBox1.Text + ".txt", Convert.ToDouble(textBox3.Text));
        }

        private void PlayAK_Click(object sender, EventArgs e)
        {
            byte[] command;
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            //for (int i = 0; i < 3; i++)
            {
                Play_Text("zhanbi.txt", 2);
                command = StepMoter_Move(0x01, -1000, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(500);
                Play_Text("an2.txt", 1.5);
                //Initial_Position(position);
                //Thread.Sleep(300);
                //Play_Text("zhanbi.txt", 2.5);
                command = StepMoter_Move(0x01, -2000, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1000);
                Play_Text("kang8.txt", 3);
                command = StepMoter_Move(0x01, 3000, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(2000);
                command = StepMoter_Move(0x02, -2800, 1000);
                sp1.Write(command, 0, command.Length);
            }
        }

        private void PlaySA_Click(object sender, EventArgs e)
        {
            byte[] command;
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            //for (int i = 0; i < 3; i++)
            {

                Play_Text("zhanbi.txt", 2);//原有程序
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("ke5.txt", 2);
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("zhanbi.txt", 2);
                command = StepMoter_Move(0x01, -2800, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1000);
                Play_Text("xue5.txt", 3);
                command = StepMoter_Move(0x01, 2800, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                command = StepMoter_Move(0x02, -2800, 1000);
                sp1.Write(command, 0, command.Length);
                Play_Text("zhanbi.txt", 2);
                command = StepMoter_Move(0x01, -1200, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1000);
                Play_Text("yu5.txt", 1.5);
                command = StepMoter_Move(0x01, 1200, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1000);
                command = StepMoter_Move(0x02, -2800, 1000);
                sp1.Write(command, 0, command.Length);
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(1000);
                Play_Text("yi5.txt", 1.5);
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("zhanbi.txt", 2);
                command = StepMoter_Move(0x01, -2700, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1000);
                Play_Text("shu5.txt", 3);
                command = StepMoter_Move(0x01, 2700, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);

            }
        }

        private void PlayJYLQ_Click(object sender, EventArgs e)
        {
            byte[] command;
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            //for (int i = 0; i < 3; i++)
            {
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("jing3.txt", 2.5);
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(300);
                command = StepMoter_Move(0x01, -2000, 2000);
                sp1.Write(command, 0, command.Length);
                Thread.Sleep(1000);
                Play_Text("ye1.txt", 3);
                command = StepMoter_Move(0x01, 2000, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1200);
                command = StepMoter_Move(0x02, -3000, 1000);
                sp1.Write(command, 0, command.Length);
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(1000);
                Play_Text("le3.txt", 3);
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(300);
                command = StepMoter_Move(0x01, -2700, 2000);
                sp1.Write(command, 0, command.Length);
                Thread.Sleep(1000);
                Play_Text("qun3.txt", 2);
                command = StepMoter_Move(0x01, 2700, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                command = StepMoter_Move(0x02, -3000, 1000);
                sp1.Write(command, 0, command.Length);
            }
        }

        private void PlayCWZL_Click(object sender, EventArgs e)
        {
            byte[] command;
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            //for (int i = 0; i < 3; i++)
            {
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("cang4.txt", 2.5);
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(300);
                command = StepMoter_Move(0x01, -2500, 2000);
                sp1.Write(command, 0, command.Length);
                Thread.Sleep(1200);
                Play_Text("wang2.txt", 2.5);
                Initial_Position(position);
                Thread.Sleep(300);
                command = StepMoter_Move(0x01, 2500, 2000);
                sp1.Write(command, 0, command.Length);
                Thread.Sleep(1500);
                command = StepMoter_Move(0x02, -3000, 1000);
                sp1.Write(command, 0, command.Length);
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(1000);
                Play_Text("zhi1.txt", 1);
                Initial_Position(position);
                Thread.Sleep(300);
                Play_Text("zhanbi.txt", 2);
                Initial_Position(position);
                Thread.Sleep(300);
                command = StepMoter_Move(0x01, -2800, 2000);
                sp1.Write(command, 0, command.Length);
                Thread.Sleep(1500);
                Play_Text("lai3.txt", 2.5);
                command = StepMoter_Move(0x01, 2800, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                command = StepMoter_Move(0x02, -3000, 1000);
                sp1.Write(command, 0, command.Length);
            }
        }

        private void Shi_Click(object sender, EventArgs e)
        {
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            Play_Text("shi1_LfD.txt", 1);
            Thread.Sleep(300);
            Play_Text("shi2_LfD.txt", 1);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox2_Click(object sender, EventArgs e)
        {

        }

        private void Play_DZYN_Click(object sender, EventArgs e)
        {
            byte[] command;
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            //for (int i = 0; i < 3; i++)
            {
                //Play_Text("zhanbi.txt", 2);
                command = StepMoter_Move(0x01, -2000, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                Play_Text("dong1.txt", 1);
                command = StepMoter_Move(0x01, 2000, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                command = StepMoter_Move(0x02, -3000, 1000);
                sp1.Write(command, 0, command.Length);
                Play_Text("zhanbi.txt", 2);
                command = StepMoter_Move(0x01, -2000, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                Play_Text("zi1.txt", 2.0);
                command = StepMoter_Move(0x01, 2000, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                command = StepMoter_Move(0x02, -3500, 1000);
                sp1.Write(command, 0, command.Length);
                Play_Text("zhanbi.txt", 2);
                command = StepMoter_Move(0x01, -1600, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                Play_Text("yan1.txt", 2);
                command = StepMoter_Move(0x01, 1600, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                command = StepMoter_Move(0x02, -3000, 1000);
                sp1.Write(command, 0, command.Length);
                Play_Text("zhanbi.txt", 2);
                command = StepMoter_Move(0x01, -1600, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(1500);
                Play_Text("nian.txt", 2);
                command = StepMoter_Move(0x01, 1600, 2000);
                sp1.Write(command, 0, command.Length);
                Initial_Position(position);
                Thread.Sleep(300);
            }
        }

        private void button1_Click_1(object sender, EventArgs e)
        {
            byte[] command;
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            //for (int i = 0; i < 3; i++)
            {
                Initial_Position(position);
                Thread.Sleep(500);
                //Play_Text("zhanbi.txt", 2);//原有程序
                //Initial_Position(position);
                //Thread.Sleep(300);
                //Play_Text("1.txt", 2);
                //Initial_Position(position);
                //Thread.Sleep(300);
                //Play_Text("zhanbi.txt", 2);
                //command = StepMoter_Move(0x01, -2800, 2000);
                //sp1.Write(command, 0, command.Length);
                //Initial_Position(position);
                //Thread.Sleep(1000);
                Play_Text("t.txt", 2.5);
                //command = StepMoter_Move(0x01, 2800, 2000);
                //sp1.Write(command, 0, command.Length);
                //Thread.Sleep(1000);
                //Initial_Position(position);
                //Thread.Sleep(500);

            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            {
                byte[] command;
                byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
                //for (int i = 0; i < 3; i++)
                {
                    Play_Text("zhanbi.txt", 1);
                    Initial_Position(position);
                    Thread.Sleep(300);
                    Play_Text("bo3.txt", 1);
                    Initial_Position(position);
                    Thread.Sleep(300);
                    Play_Text("zhanbi.txt", 1);
                    Initial_Position(position);
                    Thread.Sleep(300);
                    command = StepMoter_Move(0x01, -2500, 2000);
                    sp1.Write(command, 0, command.Length);
                    Thread.Sleep(1200);
                    Play_Text("wen-final.txt", 1);
                    Initial_Position(position);
                    Thread.Sleep(800);
                    command = StepMoter_Move(0x01, 2900, 2000);
                    sp1.Write(command, 0, command.Length);
                    Thread.Sleep(1500);
                    command = StepMoter_Move(0x02, -3000, 1000);
                    sp1.Write(command, 0, command.Length);
                    Play_Text("zhanbi.txt", 1);
                    Initial_Position(position);
                    Thread.Sleep(1000);
                    Play_Text("yue5.txt", 1);
                    Initial_Position(position);
                    Thread.Sleep(300);
                    Play_Text("zhanbi.txt", 1);
                    Initial_Position(position);
                    Thread.Sleep(300);
                    command = StepMoter_Move(0x01, -3200, 2000);
                    sp1.Write(command, 0, command.Length);
                    Thread.Sleep(1500);
                    command = StepMoter_Move(0x02, -300, 1000);
                    sp1.Write(command, 0, command.Length);
                    Thread.Sleep(1500);
                    Play_Text("li-final.txt", 2.5);
                    command = StepMoter_Move(0x01, 3000, 2000);
                    sp1.Write(command, 0, command.Length);
                    Initial_Position(position);
                    Thread.Sleep(1500);
                    //command = StepMoter_Move(0x02, -3000, 1000);
                    //sp1.Write(command, 0, command.Length);
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button4_Click(object sender, EventArgs e)
        {
            //向右移动1480个单位
            byte[] command;
            //byte[] position = { 151, 8, 162, 8, 251, 5, 224, 3, 38, 4, 230, 13 };
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };

            Initial_Position(position);
            Thread.Sleep(300);
            command = StepMoter_Move(0x01, -1480, 2000);
            sp1.Write(command, 0, command.Length);

        }

        private void button5_Click(object sender, EventArgs e)
        {
            //向左移动1480个单位
            byte[] command;
            //byte[] position = { 151, 8, 162, 8, 251, 5, 224, 3, 38, 4, 230, 13 };
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            Initial_Position(position);
            Thread.Sleep(300);
            command = StepMoter_Move(0x01, 1480, 2000);
            sp1.Write(command, 0, command.Length);

        }

        private void button6_Click(object sender, EventArgs e)
        {
            byte[] command;
            byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            Initial_Position(position);
            //Thread.Sleep(1200);
            command = StepMoter_Move(0x02, -3000, 1000);
            sp1.Write(command, 0, command.Length);
        }

        private void Liuchenghuajiaoxue_Click(object sender, EventArgs e)
        {
            //流程化教学和teach按键只能分别使用，不能一次按流程化教学一次按teach
            if (teachingSwitch)
            {

                StreamReader sr = File.OpenText("count.txt");
                string l = sr.ReadLine();
                successNum = int.Parse(l);
                l = sr.ReadLine();
                failNum = int.Parse(l);
                sr.Close();
                teachingSwitch = false;
                Teaching.Text = "Teaching...";
                Teaching.BackColor = Color.Red;
                sw = File.CreateText(string.Format("{0:G}.txt", successNum));
                t.Elapsed += new System.Timers.ElapsedEventHandler(StartTeaching);
                t.Start();
            }
            else
            {
                Teaching.Text = "Teach";
                Teaching.BackColor = SystemColors.ButtonFace;
                teachingSwitch = true;
                t.Stop();
                count = 0;
                Thread.Sleep(1000);
                sw.Close();

                //DataCut w = new DataCut("t");
                //w.Correctmode();
                //DataCut c = new DataCut("tCorrect");
                //c.Washmode();

                if ((successNum + failNum) % 3 == 0 || (successNum + failNum) % 3 == 1)
                {
                    byte[] command = StepMoter_Move(0x01, horizontalInterval, 1000);
                    sp1.Write(command, 0, command.Length);
                }
                else
                {
                    byte[] command = StepMoter_Move(0x01, -horizontalInterval * 2, 1000);
                    sp1.Write(command, 0, command.Length);
                }

                Thread.Sleep(100);

                if (error_num <= 3)
                {
                    MessageBox.Show(string.Format("File Saved: {0:G}.txt", successNum++));
                }
                else
                {
                    failNum++;
                    error_num = 0;
                    MessageBox.Show("Too many error, try again.");
                }
                sp.ReadExisting();
                StreamWriter sw2 = new StreamWriter("count.txt");
                sw2.WriteLine(successNum);
                sw2.WriteLine(failNum);
                sw2.Close();
                this.Close();
            }
        }

        private void Sanlianxie_Click(object sender, EventArgs e)
        {
            StreamReader srr = File.OpenText("readcount.txt");
            StreamWriter sww;
            byte[] command;
            byte[] Initpos = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            int filename = int.Parse(srr.ReadLine());
            srr.Close();

            //Play_Text();  //沾笔
            Initial_Position(Initpos);
            Thread.Sleep(300);
            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            Play_Text(filename.ToString() + ".txt", 0.5);
            Initial_Position(Initpos);
            Thread.Sleep(300);
            filename++;

            //沾笔
            Initial_Position(Initpos);
            Thread.Sleep(300);
            command = StepMoter_Move(0x01, 1480, 2000);
            sp1.Write(command, 0, command.Length);
            Thread.Sleep(1500);

            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            Play_Text(filename.ToString() + ".txt", 0.5);
            Initial_Position(Initpos);
            Thread.Sleep(300);
            filename++;

            command = StepMoter_Move(0x01, -1480, 2000);
            sp1.Write(command, 0, command.Length);
            Thread.Sleep(1500);
            //沾笔
            Initial_Position(Initpos);
            Thread.Sleep(300);

            command = StepMoter_Move(0x01, 2960, 2000);
            sp1.Write(command, 0, command.Length);
            Thread.Sleep(1500);

            sp.DiscardInBuffer();
            sp.DiscardOutBuffer();
            Play_Text(filename.ToString() + ".txt", 0.5);
            Initial_Position(Initpos);
            Thread.Sleep(300);
            filename++;
            command = StepMoter_Move(0x01, -2960, 2000);
            sp1.Write(command, 0, command.Length);
            Thread.Sleep(1500);

            sww = File.CreateText("readcount.txt");
            sww.WriteLine(filename.ToString());
            sww.Close();
        }
    }
}

