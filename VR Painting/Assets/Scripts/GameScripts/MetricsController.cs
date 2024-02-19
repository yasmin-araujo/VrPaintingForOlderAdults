using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class MetricsController : MonoBehaviour
{
    [Header("Scriptable Objects")]
    [SerializeField] private MetricsSO metricsSO;
    [SerializeField] private SettingsSO settingsSO;

    [Header("Player Data")]
    [Tooltip("Click once to default settings to save a new player")]
    [SerializeField] private bool generateNewUser;
    [SerializeField] private string username;

    private string filename;
    private TextWriter textWriter;
    private DateTime startTime;
    // Number of times the pixels were painted wrongly.
    private int missCount = 0;
    // Number of times the pixels were painted wrongly, but only considering when they change to a different color.
    private int missCountDifferentColor = 0;

    void Start()
    {
        filename = Application.dataPath + "/Scripts/Data/metrics.csv";
        StartCSV(false);
        username = metricsSO.username;
    }

    public void WriteCSV()
    {
        string duration = (DateTime.Now - startTime).TotalSeconds.ToString();
        textWriter = new StreamWriter(filename, true);
        textWriter.WriteLine(username + "," + metricsSO.currentDrawing + "," + settingsSO.UseBrush
                                + "," + settingsSO.UseAssistance + "," + settingsSO.thresholdSize
                                + "," + settingsSO.UseTracking + "," + duration + "," + missCount
                                + "," + missCountDifferentColor);
        textWriter.Close();
    }

    private void StartCSV(bool resetFile)
    {
        if (resetFile)
        {
            textWriter = new StreamWriter(filename, false);
            textWriter.WriteLine("username, drawing, withBrush, withAssistance, threshold, withTracking, duration, missCount, missCountDifferentColor");
            textWriter.Close();
            metricsSO.playerIndex = 0;
            metricsSO.username = "player0";
            username = metricsSO.username;
        }
    }

    private void OnValidate()
    {
        if (generateNewUser == true)
        {
            generateNewUser = false;
            metricsSO.playerIndex++;
            username = "player" + metricsSO.playerIndex;
            metricsSO.username = username;
        }
    }

    public void StartDrawing(string id)
    {
        startTime = DateTime.Now;
        metricsSO.currentDrawing = id;
        missCount = 0;
    }

    public void IncrementMissCountMetric(bool isSameColor)
    {
        if (!isSameColor)
            missCountDifferentColor++;
        missCount++;
    }
}
