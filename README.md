# Magellanic.Sensors.MCP8908
This is a C# implementation of code whick integrates the MCP8908 temperature sensor with Windows 10 IoT Core on the Raspberry Pi 3.

## Getting started
To build this project, you'll need the Magellanic.I2C project also.

You should reference the Magellanic.Sensors.MCP8908 and Magellanic.I2C projects in your Visual Studio solution. The MCP8908 can be used with the following sample code:

```C#
var temperatureSensor = new MCP9808();


while (true)
{  
    Debug.WriteLine(temperatureSensor.GetTemperature());  
    Task.Delay(1000).Wait();  
}   
```
