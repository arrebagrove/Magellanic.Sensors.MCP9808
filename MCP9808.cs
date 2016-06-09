using Magellanic.I2C;
using System;

namespace Magellanic.Sensors.MCP9808
{
    public class MCP9808 : AbstractI2CDevice
    {
        private const byte I2C_ADDRESS = 0x18;

        private byte[] AmbientTemperatureAddress = new byte[] { 0x05 };

        private byte[] ManufacturerIdAddress = new byte[] { 0x06 };

        private byte[] DeviceIdAddress = new byte[] { 0x07 };

        public MCP9808()
        {
            this.Initialize(I2C_ADDRESS);
        }

        public int GetManufacturerId()
        {
            byte[] readBuffer = new byte[2];

            this.Slave.WriteRead(ManufacturerIdAddress, readBuffer);

            return BitConverter.ToUInt16(readBuffer, 0);
        }

        public int GetDeviceId()
        {
            byte[] readBuffer = new byte[2];

            this.Slave.WriteRead(DeviceIdAddress, readBuffer);

            return BitConverter.ToUInt16(readBuffer, 0);
        }

        public float GetTemperature()
        {
            byte[] readBuffer = new byte[2];

            this.Slave.WriteRead(AmbientTemperatureAddress, readBuffer);

            byte upperByte = readBuffer[0];
            byte lowerByte = readBuffer[1];

            // we need to mask out the upper three boundary bits
            upperByte = Convert.ToByte(upperByte & 0x1F);

            var processedUpperByte = (float)upperByte;
            var processedLowerByte = (float)lowerByte;

            if (Convert.ToByte(upperByte & 0x10) == 0x10)
            {
                upperByte = Convert.ToByte(upperByte & 0x0F);
                return 256 - (processedUpperByte * 16f + processedLowerByte / 16f);
            }
            else
            {
                // now need to multiply the upper byte by 16 and divide the lower byte by 16, and add the two together
                return processedUpperByte * 16f + processedLowerByte / 16f;
            }
        }
    }
}
