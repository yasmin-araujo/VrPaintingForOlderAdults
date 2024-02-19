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
    [SerializeField] private bool saveNewUser;
    [SerializeField] private string username;

    private string filename;
    private TextWriter textWriter;
    private int duration = 0;
    private int misses = 0;

    void Start()
    {
        filename = Application.dataPath + "/Scripts/Data/metrics.csv";
        print(filename);
        StartCSV(true);
        username = metricsSO.username;
    }

    void WriteCSV()
    {
        textWriter.WriteLine(username + "," + metricsSO.currentDrawing + "," + settingsSO.UseBrush + "," + settingsSO.UseTracking + "," + duration + "," + misses);
        textWriter.Close();
    }

    private void StartCSV(bool resetFile)
    {
        if (resetFile)
        {
            textWriter = new StreamWriter(filename, false);
            textWriter.WriteLine("username, drawing, withBrush, withTracking, duration, misses");
            textWriter.Close();
            metricsSO.playerIndex = 0;
            metricsSO.username = "player0";
            username = metricsSO.username;
        }
        textWriter = new StreamWriter(filename, true);
    }

    private void OnValidate()
    {
        if (saveNewUser == true)
        {
            saveNewUser = false;
            metricsSO.playerIndex++;
            username = "player" + metricsSO.playerIndex;
            metricsSO.username = username;
        }
    }
}
