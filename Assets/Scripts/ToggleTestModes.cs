using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class ToggleTestModes : MonoBehaviour
{
    [Tooltip("Set participant number for loading stimulus files (1-80)")]
    public int participantNumber = 1;
    public GameObject player;

    void Start()
    {
        GameObject.FindObjectOfType<FileLogger>().printProgress("Participant number: " + participantNumber);
        ShowTestMode("Phase1MemoryTest");
    }

    void Update()
    {
        // Check if key for going back to the beginning for Phase 2 Emotional ratings in pressed     
        if (Input.GetButtonDown("Phase2EmotionRating")) // Space
            ShowTestMode("Phase2EmotionRating");       
        
        else if (Input.GetKeyDown(KeyCode.Alpha1))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: -4");
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: -3");
        else if (Input.GetKeyDown(KeyCode.Alpha3))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: -2");
        else if (Input.GetKeyDown(KeyCode.Alpha4))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: -1");
        else if (Input.GetKeyDown(KeyCode.Alpha5))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: 0");
        else if (Input.GetKeyDown(KeyCode.Alpha6))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: 1");
        else if (Input.GetKeyDown(KeyCode.Alpha7))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: 2");
        else if (Input.GetKeyDown(KeyCode.Alpha8))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: 3");
        else if (Input.GetKeyDown(KeyCode.Alpha9))
            GameObject.FindObjectOfType<FileLogger>().printProgress("Participant response: 4");
        
        else if (Input.GetButtonDown("FixResponse")) // F
            GameObject.FindObjectOfType<FileLogger>().printProgress("FIXED PARTICIPANT RESPONSE");
    }
    
    void ShowTestMode(string testMode)
    {
        if (testMode == "Phase1MemoryTest")
        {
            GameObject.FindObjectOfType<FileLogger>().printProgress("Phase 1 Memory Test started");
        }
        else if (testMode == "Phase2EmotionRating")
        {
            GameObject.FindObjectOfType<FileLogger>().printProgress("Phase 2 Emotion Rating started");
            GameObject.FindObjectOfType<ObjectPlacement>().PlaceInBoxes(2);

            // Place player to the beginning of corridor
            player.transform.position = new Vector3(34, 0, -2.9f);
            player.transform.rotation = Quaternion.Euler(0, -90, 0);

        }
    }
}
