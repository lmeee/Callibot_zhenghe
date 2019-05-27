using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using Emgu.CV;// open cv 的一个net版。open cv都是用于图像处理的
using Emgu.CV.Structure;
using System.IO;
namespace Callibot
{
    class Robot
    {
        private double L1 = 71.8, L2 = 25.0, L3 = 113.3, L4 = 90.4, L5 = 115.0; 
        //Theta in radian
        public Matrix<double> T1(double Theta1)
        {
            double[,] a = { { Math.Cos(Theta1), -Math.Sin(Theta1), 0, 0 }, { Math.Sin(Theta1), Math.Cos(Theta1), 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            //上面是csharp的二维数组，下面那个是emgu.cv的内置的类型，用来画图的估计是？
            Matrix<double> t1 = new Matrix<double>(a);
            return t1;
        }

        public Matrix<double> T2(double Theta2)
        {
            double[,] a = { { Math.Cos(Theta2), -Math.Sin(Theta2), 0, L1 }, { Math.Sin(Theta2), Math.Cos(Theta2), 0, 0 }, { 0, 0, 1, 0 }, { 0, 0, 0, 1 } };
            Matrix<double> t2 = new Matrix<double>(a);
            return t2;
        }

        public Matrix<double> T3(double Theta3)
        {
            double[,] a = { { Math.Sin(Theta3), Math.Cos(Theta3), 0, L2 }, { 0, 0, 1, 0 }, { Math.Cos(Theta3), -Math.Sin(Theta3), 0, 0 }, { 0, 0, 0, 1 } };
            Matrix<double> t3 = new Matrix<double>(a);
            return t3;
        }

        public Matrix<double> T4(double Theta4)
        {
            double[,] a = { { Math.Cos(Theta4), -Math.Sin(Theta4), 0, 0 }, { 0, 0, 1, L3 }, { -Math.Sin(Theta4), -Math.Cos(Theta4), 0, 0 }, { 0, 0, 0, 1 } };
            Matrix<double> t4 = new Matrix<double>(a);
            return t4;
        }

        public Matrix<double> T5(double Theta5)
        {
            double[,] a = { { Math.Cos(Theta5), -Math.Sin(Theta5), 0, 0 }, { 0, 0, -1, 0 }, { Math.Sin(Theta5), Math.Cos(Theta5), 0, 0 }, { 0, 0, 0, 1 } };
            Matrix<double> t5 = new Matrix<double>(a);
            return t5;
        }

        public Matrix<double> T6(double Theta6)
        {
            double[,] a = { { Math.Cos(Theta6), -Math.Sin(Theta6), 0, 0 }, { 0, 0, 1, 0 }, { -Math.Sin(Theta6), -Math.Cos(Theta6), 0, 0 }, { 0, 0, 0, 1 } };
            Matrix<double> t6 = new Matrix<double>(a);
            return t6;
        }
        //以上函数搞了六个emgu的4*4 matrix

        //JointAngle in degree, offset
        public byte[] AngleToByte(double[] JointAngle)
        {
            byte[] position = new byte[12];
            byte[] temp;
            for (int i = 0; i < 6; i++)
            {
                if (i == 1)
                    JointAngle[i] = -JointAngle[i];
                temp = BitConverter.GetBytes((int)Math.Round(4096 * (JointAngle[i]+180) / 360));
                position[2 * i] = temp[0];
                position[2 * i + 1] = temp[1];
            }
            return position;
        }
        //return degree, offset
        public double[] ByteToAngle(byte[] Position)
        {
            double[] angle = new double[6];
            byte[] temp = new byte[4];
            temp[2] = 0x00;
            temp[3] = 0x00;
            for (int i = 0; i < 6; i++)
            {
                temp[0] = Position[2 * i];
                temp[1] = Position[2 * i + 1];
                angle[i] = BitConverter.ToInt32(temp, 0) * 360.0 / 4096 - 180;
                if (i == 1)
                    angle[i] = -angle[i];
            }
            return angle;
        }

        //Position in byte
        public Matrix<double> ForwardKinematics(byte[] Position)
        {
            double[,] a = { { 1, 0, 0, -L5 }, { 0, 1, 0, 0 }, { 0, 0, 1, L4 }, { 0, 0, 0, 1 } };
            Matrix<double> Tt = new Matrix<double>(a);
            double[] JointAngle = ByteToAngle(Position);
            for (int i = 0; i < 6; i++)
                JointAngle[i] = JointAngle[i] * Math.PI / 180;
            Matrix<double> T = T1(JointAngle[0]).Mul(T2(JointAngle[1])).Mul(T3(JointAngle[2])).Mul(T4(JointAngle[3])).Mul(T5(JointAngle[4])).Mul(T6(JointAngle[5])).Mul(Tt);
            return T;
        }

        //return position byte
        public int InverseKinematics(double[] Position, double[] Orient1, double[] Orient2, ref byte[] position)
        {
            //StreamWriter sw = File.AppendText("teaching data1.txt");
            Rectangle R = new Rectangle(0, 0, 3, 3);
            double[] JointAngle = new double[6];
            double x, y, z;
            x = Position[0] - L4 * Orient1[0] + L5 * Orient2[0];
            y = Position[1] - L4 * Orient1[1] + L5 * Orient2[1];
            z = Position[2] - L4 * Orient1[2] + L5 * Orient2[2];
            //sw.WriteLine(x);
            //sw.WriteLine(y);
            //sw.WriteLine(z);
            //sw.Close();
            if ((z > L3 || z < -L3) || (x == 0 && y == 0))
                return -1;
            Matrix<double> d1 = new Matrix<double>(Orient1);
            Matrix<double> d2 = new Matrix<double>(Orient2);
            JointAngle[2] = -Math.Asin(z / L3);
            if ((L1 * L1 + x * x + y * y - (L2 + L3 * Math.Cos(JointAngle[2])) * (L2 + L3 * Math.Cos(JointAngle[2]))) / (2 * L1 * Math.Sqrt(x * x + y * y)) > 1 || (L1 * L1 + x * x + y * y - (L2 + L3 * Math.Cos(JointAngle[2])) * (L2 + L3 * Math.Cos(JointAngle[2]))) / (2 * L1 * Math.Sqrt(x * x + y * y)) < -1)
                return -1;
            JointAngle[0] = Math.Atan2(y, x) + Math.Acos((L1 * L1 + x * x + y * y - (L2 + L3 * Math.Cos(JointAngle[2])) * (L2 + L3 * Math.Cos(JointAngle[2]))) / (2 * L1 * Math.Sqrt(x * x + y * y)));
            if ((L1 * L1 - x * x - y * y + (L2 + L3 * Math.Cos(JointAngle[2])) * (L2 + L3 * Math.Cos(JointAngle[2]))) / (2 * L1 * (L2 + L3 * Math.Cos(JointAngle[2]))) > 1 || (L1 * L1 - x * x - y * y + (L2 + L3 * Math.Cos(JointAngle[2])) * (L2 + L3 * Math.Cos(JointAngle[2]))) / (2 * L1 * (L2 + L3 * Math.Cos(JointAngle[2]))) < -1 || L2 + L3 * Math.Cos(JointAngle[2]) == 0)
                return -1;
            JointAngle[1] = -Math.PI + Math.Acos((L1 * L1 - x * x - y * y + (L2 + L3 * Math.Cos(JointAngle[2])) * (L2 + L3 * Math.Cos(JointAngle[2]))) / (2 * L1 * (L2 + L3 * Math.Cos(JointAngle[2]))));
            d1 = T1(JointAngle[0]).GetSubRect(R).Mul(T2(JointAngle[1]).GetSubRect(R).Mul(T3(JointAngle[2]).GetSubRect(R))).Transpose().Mul(d1);
            if (d1[0, 0] >= 0)
            {
                JointAngle[4] = -Math.Atan2(Math.Sqrt(d1[0, 0] * d1[0, 0] + d1[2, 0] * d1[2, 0]), d1[1, 0]);
                if (Math.Atan2(d1[2, 0], -d1[0, 0]) >= 0)
                    JointAngle[3] = Math.Atan2(d1[2, 0], -d1[0, 0]) - Math.PI;
                else
                    JointAngle[3] = Math.Atan2(d1[2, 0], -d1[0, 0]) + Math.PI;
            }
            else
            {
                JointAngle[4] = Math.Atan2(Math.Sqrt(d1[0, 0] * d1[0, 0] + d1[2, 0] * d1[2, 0]), d1[1, 0]);
                JointAngle[3] = Math.Atan2(d1[2, 0], -d1[0, 0]);
            }
            d2 = T1(JointAngle[0]).GetSubRect(R).Mul(T2(JointAngle[1]).GetSubRect(R).Mul(T3(JointAngle[2]).GetSubRect(R).Mul(T4(JointAngle[3]).GetSubRect(R).Mul(T5(JointAngle[4]).GetSubRect(R))))).Transpose().Mul(d2);
            JointAngle[5] = Math.Atan2(- d2[2, 0], d2[0, 0]);
            for (int i = 0; i < 6; i++)
                JointAngle[i] = JointAngle[i] * 180 / Math.PI;
            position = AngleToByte(JointAngle);
            return 0;
        }
        
        //JointAngle in radian
        public Matrix<double> Jacobian(byte[] Position)
        {
            double[] JointAngle = ByteToAngle(Position);
            for (int i = 0; i < 6; i++)
                JointAngle[i] = JointAngle[i] * Math.PI / 180;
            double x1 = JointAngle[0], x2 = JointAngle[1], x3 = JointAngle[2], x4 = JointAngle[3], x5 = JointAngle[4], x6 = JointAngle[5];
            double j11, j12, j13, j14, j15, j16, j21, j22, j23, j24, j25, j26, j31, j32, j33, j34, j35, j36, j41, j42, j43, j44, j45, j46, j51, j52, j53, j54, j55, j56, j61, j62, j63, j64, j65, j66;
            j11 = -L4 * (Math.Sin(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) + Math.Cos(x3) * Math.Cos(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - L5 * (Math.Sin(x6) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) + Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) + Math.Cos(x6) * (Math.Cos(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - Math.Cos(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)))) - L2 * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) - L1 * Math.Sin(x1) - L3 * Math.Cos(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1));
            j12 = -L4 * (Math.Sin(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) + Math.Cos(x3) * Math.Cos(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - L5 * (Math.Sin(x6) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) + Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) + Math.Cos(x6) * (Math.Cos(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - Math.Cos(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)))) - L2 * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) - L3 * Math.Cos(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1));
            j13 = L5 * (Math.Cos(x6) * (Math.Sin(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x3) * Math.Cos(x4) * Math.Cos(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + Math.Cos(x3) * Math.Sin(x4) * Math.Sin(x6) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) - L4 * (Math.Cos(x5) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) + Math.Cos(x3) * Math.Cos(x4) * Math.Sin(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) - L3 * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2));
            j14 = L5 * (Math.Sin(x6) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) - Math.Cos(x5) * Math.Cos(x6) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) - Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)))) - L4 * Math.Sin(x5) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) - Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)));
            j15 = L5 * Math.Cos(x6) * (Math.Sin(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) - Math.Cos(x3) * Math.Cos(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) - L4 * (Math.Cos(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + Math.Cos(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)));
            j16 = -L5 * (Math.Cos(x6) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) - Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) - Math.Sin(x6) * (Math.Cos(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + Math.Cos(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))));
            j21 = L2 * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - L5 * (Math.Sin(x6) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) - Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + Math.Cos(x6) * (Math.Cos(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + Math.Cos(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)))) - L4 * (Math.Sin(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) - Math.Cos(x3) * Math.Cos(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + L1 * Math.Cos(x1) + L3 * Math.Cos(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2));
            j22 = L2 * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - L5 * (Math.Sin(x6) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) - Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + Math.Cos(x6) * (Math.Cos(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + Math.Cos(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)))) - L4 * (Math.Sin(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) - Math.Cos(x3) * Math.Cos(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))) + L3 * Math.Cos(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2));
            j23 = L5 * (Math.Cos(x6) * (Math.Sin(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) - Math.Cos(x3) * Math.Cos(x4) * Math.Cos(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) + Math.Cos(x3) * Math.Sin(x4) * Math.Sin(x6) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - L4 * (Math.Cos(x5) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x3) * Math.Cos(x4) * Math.Sin(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - L3 * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1));
            j24 = L4 * Math.Sin(x5) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) + Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - L5 * (Math.Sin(x6) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - Math.Cos(x5) * Math.Cos(x6) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) + Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))));
            j25 = L4 * (Math.Cos(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - Math.Cos(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - L5 * Math.Cos(x6) * (Math.Sin(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) + Math.Cos(x3) * Math.Cos(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)));
            j26 = L5 * (Math.Cos(x6) * (Math.Cos(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) + Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - Math.Sin(x6) * (Math.Cos(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) - Math.Cos(x3) * Math.Sin(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))));
            j31 = 0;
            j32 = 0;
            j33 = L5 * (Math.Cos(x6) * (Math.Cos(x3) * Math.Sin(x5) + Math.Cos(x4) * Math.Cos(x5) * Math.Sin(x3)) - Math.Sin(x3) * Math.Sin(x4) * Math.Sin(x6)) - L3 * Math.Cos(x3) - L4 * (Math.Cos(x3) * Math.Cos(x5) - Math.Cos(x4) * Math.Sin(x3) * Math.Sin(x5));
            j34 = L5 * (Math.Cos(x3) * Math.Cos(x4) * Math.Sin(x6) + Math.Cos(x3) * Math.Cos(x5) * Math.Cos(x6) * Math.Sin(x4)) + L4 * Math.Cos(x3) * Math.Sin(x4) * Math.Sin(x5);
            j35 = L4 * (Math.Sin(x3) * Math.Sin(x5) - Math.Cos(x3) * Math.Cos(x4) * Math.Cos(x5)) + L5 * Math.Cos(x6) * (Math.Cos(x5) * Math.Sin(x3) + Math.Cos(x3) * Math.Cos(x4) * Math.Sin(x5));
            j36 = -L5 * (Math.Sin(x6) * (Math.Sin(x3) * Math.Sin(x5) - Math.Cos(x3) * Math.Cos(x4) * Math.Cos(x5)) - Math.Cos(x3) * Math.Cos(x6) * Math.Sin(x4));
            j41 = 0; j51 = 0; j61 = 1;
            j42 = 0; j52 = 0; j62 = 1;
            j43 = -Math.Cos(x1) * Math.Sin(x2) - Math.Cos(x2) * Math.Sin(x1); j53 = Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2); j63 = 0;
            j44 = Math.Cos(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)); j54 = Math.Cos(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)); j64 = -Math.Sin(x3);
            j45 = Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)); j55 = Math.Cos(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) + Math.Sin(x3) * Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)); j65 = Math.Cos(x3) * Math.Sin(x4);
            j46 = Math.Cos(x3) * Math.Cos(x5) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Sin(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)) + Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2))); j56 = Math.Sin(x5) * (Math.Sin(x4) * (Math.Cos(x1) * Math.Cos(x2) - Math.Sin(x1) * Math.Sin(x2)) - Math.Cos(x4) * Math.Sin(x3) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1))) + Math.Cos(x3) * Math.Cos(x5) * (Math.Cos(x1) * Math.Sin(x2) + Math.Cos(x2) * Math.Sin(x1)); j66 = -Math.Cos(x5) * Math.Sin(x3) - Math.Cos(x3) * Math.Cos(x4) * Math.Sin(x5);
            double[,] a = { { j11, j12, j13, j14, j15, j16 }, { j21, j22, j23, j24, j25, j26 }, { j31, j32, j33, j34, j35, j36 }, { j41, j42, j43, j44, j45, j46 }, { j51, j52, j53, j54, j55, j56 }, { j61, j62, j63, j64, j65, j66 } };
            Matrix<double> J = new Matrix<double>(a);
            return J;
        }
    }
}
