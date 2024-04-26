using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class FPSCounter : MonoBehaviour
{
    [HideInInspector] public bool active;
    [HideInInspector] public Transform statsTransform;
    [Space]
    [HideInInspector] public TMP_Text framerateText;
    [HideInInspector] public TMP_Text avgFramerateText;
    [HideInInspector] public TMP_Text maxFramerateText;
    [HideInInspector] public TMP_Text minFrameRateText;
    [Space]
  
    [Space]
    [HideInInspector] public float updateInterval = 10;
    [HideInInspector] public bool useStaticColors;
    [HideInInspector] public Color highFPSColor = Color.green;
    [HideInInspector] public Color mediumFPSColor = Color.yellow;
    [HideInInspector] public Color lowFPSColor = Color.red;
    [HideInInspector] public Color staticInfoColor = Color.white;

    private float framerate;
    private int averageFramerate;
    private float maxFramerate;
    private float minFramerate;
    private Color framerateColor;
    private Color avgFramerateColor;
    private Color maxFramerateColor;
    private Color minFramerateColor;


    const float FPSMeasurePeriod = 0.5f;
    private int FPSAccumulator = 0;
    private float FPSPeriod = 0;
    private float nextUpdate;

    private List<float> FPSHistory = new List<float>();

    public void ActivateCounter(bool setActive)
    {
        switch (setActive)
        {
            case true:
            statsTransform.gameObject.SetActive(true);
            FPSHistory.Add(60);
            FPSPeriod = Time.realtimeSinceStartup + FPSMeasurePeriod;
            active = true;
            break;

            case false:
            statsTransform.gameObject.SetActive(false);
            FPSHistory.Clear();
            FPSPeriod = 0;
            active = false;

            framerateText.text = $"FPS: ---";
            avgFramerateText.text = $"Avg: ---";
            maxFramerateText.text = $"Max: ---";
            minFrameRateText.text = $"Min: ---";     

            break;
        }
    }

    private void Update()
    {
        if (active)
        {
            FPSAccumulator++;
            if (Time.realtimeSinceStartup > FPSPeriod)
            {
                framerate = (int)(FPSAccumulator / FPSMeasurePeriod);
                FPSAccumulator = 0;
                FPSPeriod += FPSMeasurePeriod;
            }

            if(Time.time >= nextUpdate)
            {
                nextUpdate = Time.time + 1 / updateInterval;
                UpdateUI();
            }


            float average = Time.frameCount / Time.unscaledTime;
            averageFramerate = (int)average;

            if(framerate > 0)
            FPSHistory.Add(framerate);

            if (FPSHistory.ToArray().Length > 1000) FPSHistory.RemoveRange(1, 999);

            CalculateMinAndMaxFPS();

        }

    }

    private void CalculateMinAndMaxFPS()
    {
        float max = FPSHistory[0];
        float min = FPSHistory[0];

        int index = 0;

        for (int i = 1; i < FPSHistory.Count; i++)
        {
            if (FPSHistory[i] < min)
            {
                min = FPSHistory[i];

                index = i;
            }
        }

        for (int i = 1; i < FPSHistory.Count; i++)
        {
            if (FPSHistory[i] > max)
            {
                max = FPSHistory[i];

                index = i;
            }
        }

        maxFramerate = max;
        minFramerate = min;
    }

    void UpdateUI()
    {

        if (useStaticColors)
        {
            framerateColor = staticInfoColor;
            avgFramerateColor = staticInfoColor;
            maxFramerateColor = staticInfoColor;
            minFramerateColor = staticInfoColor;
            
        }else{

            if (framerate >= 60) framerateColor = highFPSColor;
            if (framerate < 60 && framerate > 30) framerateColor = mediumFPSColor;
            if (framerate < 30) framerateColor = lowFPSColor;

            if (averageFramerate >= 60) avgFramerateColor = highFPSColor;
            if (averageFramerate < 60 && averageFramerate > 30) avgFramerateColor = mediumFPSColor;
            if (averageFramerate < 30) avgFramerateColor = lowFPSColor;

            if (maxFramerate >= 60) maxFramerateColor = highFPSColor;
            if (maxFramerate < 60 && maxFramerate > 30) maxFramerateColor = mediumFPSColor;
            if (maxFramerate < 30) maxFramerateColor = lowFPSColor;

            if (minFramerate >= 60) minFramerateColor = highFPSColor;
            if (minFramerate < 60 && minFramerate > 30) minFramerateColor = mediumFPSColor;
            if (minFramerate < 30) minFramerateColor = lowFPSColor;
        }

        framerateText.text = $"FPS: {framerate}";
        avgFramerateText.text = $"Avg: {averageFramerate}";
        maxFramerateText.text = $"Max: {maxFramerate}";
        minFrameRateText.text = $"Min: {minFramerate}";

        framerateText.color = framerateColor;
        avgFramerateText.color = avgFramerateColor;
        maxFramerateText.color = maxFramerateColor;
        minFrameRateText.color = minFramerateColor;
          
    }
    
}    