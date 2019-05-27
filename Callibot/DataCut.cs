using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Emgu.CV;
using Emgu.CV.Structure;
using Emgu.CV.UI;

namespace Callibot
{
    //error的处理,txt的格式问题
    class DataCut
    {//v2.0 
     //input txt file should not contain error data. incorrect Format and lack of enough data was also not allow.
     // This file was used for data washing. Using these functions, we could eliminate uneccesary data, which was good for modeling.
     // For writing, one should start from a high place and end at a high place.

        //string strLine;
        Robot r = new Robot();
        static string filename;
        string[,] storage;
        StreamWriter wfile = File.CreateText("litest.txt");
        StreamWriter washed; 
        //FileStream rfile = new FileStream("zhi1.txt", FileMode.OpenOrCreate, FileAccess.Read);
        StreamReader read;
        double[] zList;
        double opimalz = -70;//found by function FindOptimalZ


        public DataCut(string FileName)
        {
            filename = FileName;
        }

        public void ReadFile()
        {
            int count = 0;
            int Row_num = 0;
            string temp;
            read = File.OpenText(filename+".txt");
            while (true)
            {
                temp = read.ReadLine();
                if (temp.Contains("Frame")) { count++; }    //here count refer to the total number of rows.
                Row_num++;
                if (read.Peek() == -1) { break; }
            }
            read.Close();

            storage = new string[count,12];
            zList = new double[count];

            read = File.OpenText(filename+".txt");
            count = -1;
            int col = 0;
            Boolean indicator = true;
            while (read.Peek() != -1)
            {
                temp = read.ReadLine();
                if (temp.Contains("Frame"))
                {
                    count++;
                    col = 0;
                    indicator = true;
                }else if (temp.Contains("error")|| (!indicator))
                {
                    col++;
                    continue;
                }
                else
                {
                    if (col == 12)
                    {
                        indicator = false;
                        storage[count, 11] = null;
                    }
                    else
                    {
                        storage[count, col] = temp;
                        col++;
                    }
                }
                    
                
            }
            read.Close();
        }

        public void CreateZAxisText()
        {
            byte[] position = new byte[12];
            double[,] Pen = { { 0.0 }, { 0.0 }, { 0.0 }, { 1.0 } }; //笔尖位置就在原点
            Matrix<double> VofPen = new Matrix<double>(Pen);
            double z = 0.0;
            Matrix<double> TransformMatrix;
            for (int i = 0; i < storage.GetLength(0); i++)
            {
                for (int j = 0; j < 6; j++)
                {
                    position[j * 2] = BitConverter.GetBytes(Convert.ToInt32(storage[i, j * 2]))[0];
                }
                for (int j = 0; j < 6; j++)
                {
                    position[j * 2 + 1] = BitConverter.GetBytes(Convert.ToInt32(storage[i, j * 2 + 1]))[0];
                }
                //byte[] position = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
                TransformMatrix = r.ForwardKinematics(position);
                z = TransformMatrix.Mul(VofPen)[2, 0];//接下来要算这个缩进量，通过inverse kinametic
                //wfile.WriteLine(z.ToString());
                zList[i] = z;
            }
            wfile.Close();   
        }

        public byte[] FindOptimalZ(double d)
         //using initial position to tried to find the optimal z-aix value of ground.
         //d is the distance of placing down the pen from the original initial position.
        {
            byte[] iniposition = { 0xAC, 0x09, 0xE8, 0x0A, 0x7B, 0x06, 0x00, 0x08, 0x82, 0x09, 0x00, 0x08 };
            Matrix<double> TransformMatrix = r.ForwardKinematics(iniposition);
            Matrix<double> Oripos = TransformMatrix.RemoveCols(0, 3).RemoveRows(3, 4);
            double[] oripos = new double[3];
            for (int i = 0; i < 3; i++)
            {
                oripos[i] = Oripos[i, 0];
                wfile.WriteLine(Convert.ToString(oripos[i]));
            }

            //double[,] zaxi = { {0.0 }, { 0.0 }, { 1.0 }, { 1.0 } };
            //Matrix<double> Zaxi = new Matrix<double>(zaxi);
            //Matrix<double> Ore1 = TransformMatrix.Mul(Zaxi).RemoveRows(3, 3)-Oripos;
            double[] ore1 = new double[3];
            for (int i = 0; i < 3; i++)
            {
                ore1[i] = TransformMatrix[i, 2];
            }

            //double[,] xaxi = { { 1.0 }, { 0.0 }, { 0.0 }, { 1.0 } };
            //Matrix<double> Xaxi = new Matrix<double>(xaxi);
            //Matrix<double> Ore2 = TransformMatrix.Mul(Zaxi).RemoveRows(3, 3) - Oripos;
            double[] ore2 = new double[3];
            for (int i = 0; i < 3; i++)
            {
                ore2[i] = TransformMatrix[i, 0];
            }

            double[] neworipos = new double[3];
            neworipos[0] = oripos[0];
            neworipos[1] = oripos[1];
            neworipos[2] = oripos[2]-d;
            byte[] newPosition = new byte[12];
            r.InverseKinematics(neworipos, ore1, ore2, ref newPosition);
            for (int i = 0; i<3; i++)
            {
                wfile.WriteLine(Convert.ToString(neworipos[i]));
            }
            wfile.Close();
            return newPosition;
        }//optimal origin position to base frame seems to be (242.369384180322, -53.8656548720669, -70.9159721161099), what we need was the z value.

        public void DataWash()
        {
            StreamWriter washed = File.CreateText(filename + "Washed.txt");
            int StartIndex = 0;
            int EndIndex = zList.Length;
            for (int i = 0; i < zList.Length; i++)
            {
                if (zList[i] <= opimalz)
                {
                    StartIndex = i;
                    break;
                }
            }
            for (int i = zList.Length-1; i>=0; i--)
            {
                if (zList[i] <= opimalz)
                {

                    EndIndex = i;
                    break;
                }
            }
            for  (int i = StartIndex; i<=EndIndex; i++)
            {
                washed.WriteLine("Frame "+Convert.ToString(i));
                for (int j = 0; j < 12; j++)
                {
                    washed.WriteLine(storage[i, j]);
                }
            }
            washed.Close();
        }

        public void ErrorCorrect()
        {
            StreamWriter cofile = File.CreateText(filename+"Correct.txt");
            Boolean indicator = true;
            for (int i = 0; i < zList.Length; i++)
            {
                for (int j = 11; j>=0; j--)
                {
                    if(storage[i, j] == null) { indicator = false; break; }
                    else { indicator = true; }
                }
                if (indicator)
                {
                    cofile.WriteLine("Frame " + Convert.ToString(i));
                    for (int j = 0; j < 12; j++)
                    {
                        cofile.WriteLine(storage[i, j]);
                    }

                }
            }
            cofile.Close();
        }

        public void Washmode()
        {
            ReadFile();
            CreateZAxisText();
            DataWash();
        }
        public void Correctmode()
        {
            ReadFile();
            CreateZAxisText();
            ErrorCorrect();
        }
    }
}
