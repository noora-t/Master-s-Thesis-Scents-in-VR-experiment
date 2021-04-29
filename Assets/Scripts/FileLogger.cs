﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class FileLogger : MonoBehaviour
{
    [Tooltip("Logger file name prefix, do not add file extension")]
    [SerializeField] private string filePrefix = "logfile";
    [Tooltip("Select to insert timestamp into filename")]
    [SerializeField] private bool timestampInFilename = true;

    private StreamWriter m_outFile = null;
    private float m_TimeStamp; // timestamp to store activity start time

    // Use this for initialization
    void Start()
    {
        //openFile();
        m_TimeStamp = Time.unscaledTime;
    }

    // print out progress timers
    public void printProgress(string s, bool resetTimer = false)
    {
        float delta = Time.unscaledTime - m_TimeStamp;
        if (resetTimer)
            m_TimeStamp = Time.unscaledTime;
        if (m_outFile == null)
        {
            openFile();
            m_outFile.WriteLine(Time.unscaledTime.ToString("0.000") + "\t" + "Logfile created");
        }
        m_outFile.WriteLine(Time.unscaledTime.ToString("0.000") + "\t" + s);
        m_outFile.Flush();
        Debug.Log("Time:" + Time.unscaledTime.ToString("0.000") + " - " + s);
    }

    // open log file (append mode if file exits)
    private void openFile()
    {
        string m_fileName;
        if (!timestampInFilename)
            m_fileName = filePrefix + ".log";
        else
            m_fileName = filePrefix + DateTime.Now.ToString("_yyyy-MM-dd_hhmm") + ".log";
        if (File.Exists(m_fileName))
            m_outFile = File.AppendText(m_fileName);
        else
            m_outFile = File.CreateText(m_fileName);
    }
}
