using System;
using System.Collections.Generic;
using System.Linq;//for 集合查询
using System.Text;//包含表示ASCII、Unicode、UTF-7和UTF-8字符编码的类，在字符块和字节块之间进行相互转换的抽象基类，以及一个不需要创建String中间实例就能操作String对象并对其进行格式化的辅助类StringBuilder。

namespace Callibot
{
    class Motor
    {
        Robot robot = new Robot();

        public byte checksum(byte[] Command)
        {
            int check = 0;
            for(int i = 2;i < Command.Length - 1;i++)
                check = check + Command[i];
            check = 0xFFFF - check;
            byte[] checksum = BitConverter.GetBytes(check);
            return checksum[0];
        }

        public byte[] SetID(byte ID)
        {
            byte[] packet = new byte[8];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = 0xFE;
            packet[3] = 0x04;
            packet[4] = 0x03;
            packet[5] = 0x03;
            packet[6] = ID;
            packet[7] = checksum(packet);
            return packet;
        }

        public byte[] SetCCWLimit(byte ID, byte Low, byte High)
        {
            byte[] packet=new byte[9];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = ID;
            packet[3] = 0x05;
            packet[4] = 0x03;
            packet[5] = 0x08;
            packet[6] = Low;
            packet[7] = High;
            packet[8] = checksum(packet);
            return packet;
        }

        public byte[] SetCWLimit(byte ID, byte Low, byte High)
        {
            byte[] packet = new byte[9];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = ID;
            packet[3] = 0x05;
            packet[4] = 0x03;
            packet[5] = 0x06;
            packet[6] = Low;
            packet[7] = High;
            packet[8] = checksum(packet);
            return packet;
        }

        public byte[] GetPosition(byte ID)// 将id处理成机器人可以识别的形式，就是packet
        {
            byte[] packet = new byte[8];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = ID;
            packet[3] = 0x04;
            packet[4] = 0x02;
            packet[5] = 0x24;
            packet[6] = 0x02;
            packet[7] = checksum(packet);
            return packet;
        }

        public byte[] GetSpeed(byte ID)
        {
            byte[] packet = new byte[8];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = ID;
            packet[3] = 0x04;
            packet[4] = 0x02;
            packet[5] = 0x26;
            packet[6] = 0x02;
            packet[7] = checksum(packet);
            return packet;
        }

        public byte[] GetPositionAndSpeed(byte ID)
        {
            byte[] packet = new byte[8];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = ID;
            packet[3] = 0x04;
            packet[4] = 0x02;
            packet[5] = 0x24;
            packet[6] = 0x04;
            packet[7] = checksum(packet);
            return packet;
        }

        public byte[] Enable()
        {
            byte[] packet = new byte[20];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = 0xFE;
            packet[3] = 0x10;
            packet[4] = 0x83;
            packet[5] = 0x18;
            packet[6] = 0x01;
            packet[7] = 0x01;
            packet[8] = 0x01;
            packet[9] = 0x02;
            packet[10] = 0x01;
            packet[11] = 0x03;
            packet[12] = 0x01;
            packet[13] = 0x04;
            packet[14] = 0x01;
            packet[15] = 0x05;
            packet[16] = 0x01;
            packet[17] = 0x06;
            packet[18] = 0x01;
            packet[19] = checksum(packet);
            return packet;
        }

        public byte[] Disable()
        {
            byte[] packet = new byte[20];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = 0xFE;
            packet[3] = 0x10;
            packet[4] = 0x83;
            packet[5] = 0x18;
            packet[6] = 0x01;
            packet[7] = 0x01;
            packet[8] = 0x00;
            packet[9] = 0x02;
            packet[10] = 0x00;
            packet[11] = 0x03;
            packet[12] = 0x00;
            packet[13] = 0x04;
            packet[14] = 0x00;
            packet[15] = 0x05;
            packet[16] = 0x00;
            packet[17] = 0x06;
            packet[18] = 0x00;
            packet[19] = checksum(packet);
            return packet;
        }

        public byte[] MoveTo_MX(byte ID, double Target, double RPM)
        {
            byte[] position = BitConverter.GetBytes((int)Math.Round(4096 * Target / 360));
            byte[] speed = BitConverter.GetBytes((int)Math.Round(RPM / 0.11444));
            byte[] packet = new byte[11];
            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = ID;
            packet[3] = 0x07;
            packet[4] = 0x03;
            packet[5] = 0x1E;
            packet[6] = position[0];
            packet[7] = position[1];
            packet[8] = speed[0];
            packet[9] = speed[1];
            packet[10] = checksum(packet);
            return packet;
        }

        public byte[] SyncMove_MX(double[] Target, double[] RPM)
        {
            byte[] packet = new byte[38];
            byte[] position;
            byte[] speed;
            for (int i = 0; i < 6; i++)
            {
                position = BitConverter.GetBytes((int)Math.Round(4096 * Target[i] / 360));
                speed = BitConverter.GetBytes((int)Math.Round(RPM[i] / 0.11444));
                packet[8 + 5 * i] = position[0];
                packet[9 + 5 * i] = position[1];
                packet[10 + 5 * i] = speed[0];
                packet[11 + 5 * i] = speed[1];
            }

            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = 0xFE;
            packet[3] = 0x22;
            packet[4] = 0x83;
            packet[5] = 0x1E;
            packet[6] = 0x04;
            packet[7] = 0x01;
            packet[12] = 0x02;
            packet[17] = 0x03;
            packet[22] = 0x04;
            packet[27] = 0x05;
            packet[32] = 0x06;
            packet[37] = checksum(packet);
            return packet;
        }

        public byte[] SyncMove(byte[] Target, double[] RPM)
        {
            byte[] packet = new byte[38];
            byte[] speed;
            for (int i = 0; i < 6; i++)
            {
                speed = BitConverter.GetBytes((int)Math.Round(RPM[i] / 0.11444));
                packet[8 + 5 * i] = Target[2 * i];
                packet[9 + 5 * i] = Target[2 * i + 1];
                packet[10 + 5 * i] = speed[0];
                packet[11 + 5 * i] = speed[1];
            }

            packet[0] = 0xFF;
            packet[1] = 0xFF;
            packet[2] = 0xFE;
            packet[3] = 0x22;
            packet[4] = 0x83;
            packet[5] = 0x1E;
            packet[6] = 0x04;
            packet[7] = 0x01;
            packet[12] = 0x02;
            packet[17] = 0x03;
            packet[22] = 0x04;
            packet[27] = 0x05;
            packet[32] = 0x06;
            packet[37] = checksum(packet);
            return packet;
        }
    }
}
