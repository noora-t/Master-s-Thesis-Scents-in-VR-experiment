using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO.Ports;

public class COMPort : MonoBehaviour
{
    public SerialPort stream = null;
    public bool triggerOnPickup = true;
    public string comPort = "COM6";
    public int comPortOpen = 0;
    private bool valveOpen = false;
    public bool cleanAir = false;

    void Start()
    {
        stream = new SerialPort(comPort, 9600);
        stream.ReadTimeout = 50;
        stream.Open();

        GameObject.FindObjectOfType<FileLogger>().printProgress("Trying to open COM port");
        GameObject.FindObjectOfType<FileLogger>().printProgress("COM port status: " + stream.IsOpen);
        comPortOpen = 1;

        WriteToArduino("g");
        cleanAir = true;
    }

    public void WriteToArduino(string message)
    {
        if (comPortOpen == 1)
        {
            if (!valveOpen)
                valveOpen = true;
            else
                valveOpen = false;
            
            stream.WriteLine(message);
        }
    }
}