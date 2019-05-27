using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Emgu.CV;
using Emgu.CV.Structure;
using System.IO;
namespace Callibot
{
    class Strokes
    {
        private double L1 = 88, L2 = 88, L3 = 119.3, L4 = 89.5, L5 = 115.0;

        public Matrix<double> Rk(double Kx, double Ky, double Kz, double Theta)
        {
            double[,] a = { { Kx * Kx * (1 - Math.Cos(Theta)) + Math.Cos(Theta), Kx * Ky * (1 - Math.Cos(Theta)) - Kz * Math.Sin(Theta), Kx * Kz * (1 - Math.Cos(Theta)) + Ky * Math.Sin(Theta) }, { Kx * Ky * (1 - Math.Cos(Theta)) + Kz * Math.Sin(Theta), Ky * Ky * (1 - Math.Cos(Theta)) + Math.Cos(Theta), Ky * Kz * (1 - Math.Cos(Theta)) - Kx * Math.Sin(Theta) }, { Kx * Kz * (1 - Math.Cos(Theta)) - Ky * Math.Sin(Theta), Ky * Kz * (1 - Math.Cos(Theta)) + Kx * Math.Sin(Theta), Kz * Kz * (1 - Math.Cos(Theta)) + Math.Cos(Theta) } };
            Matrix<double> rk = new Matrix<double>(a);
            return rk;
        }

        public double[] Start(double End_x, double End_y, double Offset_x, double Offset_y, double Offset_z)
        {
            double[] motion = new double[18];
            double x, y;
            for (int i = 0; i < 2; i++)
            {
                x = (i-1) * End_x / 2;
                y = (i-1) * End_y / 2;
                motion[9 * i] = x + Offset_x;
                motion[9 * i + 1] = y + Offset_y;
                motion[9 * i + 2] = -i * Math.Sqrt(End_x * End_x + End_y * End_y) / 2 + Offset_z;
                motion[9 * i + 3] = x / L5;
                motion[9 * i + 4] = y / L5;
                motion[9 * i + 5] = Math.Sqrt(1 - (x * x + y * y) / (L5 * L5));
                if (motion[9 * i + 5] == 1)
                {
                    motion[9 * i + 8] = 0;
                    motion[9 * i + 7] = motion[9 * i - 2];
                    motion[9 * i + 6] = Math.Sqrt(1 - motion[9 * i + 7] * motion[9 * i + 7]);
                }
                else
                {
                    if (motion[9 * i + 3] + motion[9 * i + 4] >= 0)
                        motion[9 * i + 8] = -Math.Sqrt((motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 3] + motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 4] * motion[9 * i + 4]) / (motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 3] + motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 4] * motion[9 * i + 4] + motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 5] * motion[9 * i + 5]));
                    else
                        motion[9 * i + 8] = Math.Sqrt((motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 3] + motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 4] * motion[9 * i + 4]) / (motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 3] + motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 4] * motion[9 * i + 4] + motion[9 * i + 3] * motion[9 * i + 3] * motion[9 * i + 5] * motion[9 * i + 5]));
                    motion[9 * i + 7] = -motion[9 * i + 4] * motion[9 * i + 5] * motion[9 * i + 8] / (motion[9 * i + 3] * motion[9 * i + 3] + motion[9 * i + 4] * motion[9 * i + 4]);
                    motion[9 * i + 6] = -(motion[9 * i + 7] * motion[9 * i + 4] + motion[9 * i + 8] * motion[9 * i + 5]) / motion[9 * i + 3];
                }
            }
            return motion;
        }

        public double[] Turn(double Start_x, double Start_y, double End_x, double End_y, double Offset_x, double Offset_y, double Offset_z,double Kx,double Ky, double Kz,double X,double Y,double Z)
        {
            StreamWriter sw = File.AppendText("teaching data2.txt");
            double[] motion = new double[99];
            Matrix<double> np = new Matrix<double>(3,1);
            double[] a = { X, Y, Z };
            Matrix<double> n = new Matrix<double>(a);
            double x, y;
            double theta = Math.Atan2(End_y - Start_y / 2, End_x - Start_x / 2) - Math.Atan2(Start_y, Start_x);
            
            if (theta >= 2 * Math.PI)
                theta = theta - 2 * Math.PI;
            for (int i = 0; i <= 10; i++)
            {
                x = (20 - i) * Start_x / 20;
                y = Start_y + i * (Math.Sqrt(Start_x * Start_x + Start_y * Start_y) - Start_y / 2) / 10;
                motion[9 * i] = x + Offset_x;
                motion[9 * i + 1] = y + Offset_y;
                motion[9 * i + 2] = -Math.Sqrt(Start_x * Start_x + Start_y * Start_y) + Offset_z;
                motion[9 * i + 3] = Kx;
                motion[9 * i + 4] = Ky;
                motion[9 * i + 5] = Kz;
                np = Rk(Kx,Ky,Kz,i*theta/10).Mul(n);
                motion[9 * i + 6] = np[0,0];
                motion[9 * i + 7] = np[1,0];
                motion[9 * i + 8] = np[2,0];
                sw.WriteLine(motion[9 * i + 6]);
                sw.WriteLine(motion[9 * i + 7]);
                sw.WriteLine(motion[9 * i + 8]);
            }
            sw.WriteLine(X);
            sw.WriteLine(Y);
            sw.WriteLine(Z);
            sw.WriteLine(Kx);
            sw.WriteLine(Ky);
            sw.WriteLine(Kz);
            sw.Close();
            return motion;
        }
    }
}
