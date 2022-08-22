using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrameRateLimit : MonoBehaviour
{
    void Start()
    {
        QualitySettings.vSyncCount = 1; //or 2 or 3 or 4   

        Application.targetFrameRate = -1;
        
    }
}
