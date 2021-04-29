using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Valve.VR.InteractionSystem;

public class ScentInformation : MonoBehaviour
{
    public string m_scent = null;
    public bool m_isStatic = true;

    // public Hand m_ActiveHand = null;
    public COMPort myPort = null;
    public Hand myHand = null;
    public int valveOpen = 0;
    public string scentPort = "no";

    GameObject head;
    float distanceValue;
    string distance = null;
    string tempDistanceName = null;

    void Start()
    {
        head = GameObject.Find("VRCamera");
        myPort = FindObjectOfType(typeof(COMPort)) as COMPort;
        myHand = FindObjectOfType(typeof(Hand)) as Hand;
    }

    void Update()
    {
        distanceValue = Vector3.Distance(head.transform.position, transform.position);

        if (distanceValue <= 0.55f && distanceValue > 0.3f)
            distance = "far";
        else if (distanceValue <= 0.3f && distanceValue > 0.15f)
            distance = "middle";
        else if (distanceValue <= 0.15f)
            distance = "close";
        /* else if (distanceValue > 0.5)
            distance = "odorless"; */

        if (myHand.currentAttachedObject == gameObject)
        {
            // check if object clean air is on (odor has been dropped)
            if (myPort.cleanAir == true)
                tempDistanceName = null;

            if (distance != null && distance != tempDistanceName)
            {
                
                if (!m_isStatic && m_scent.Equals("vanilla") && distance.Equals("close"))
                    scentPort = "f";
                else if (m_isStatic && m_scent.Equals("vanilla") || !m_isStatic && m_scent.Equals("vanilla") && distance.Equals("middle"))
                    scentPort = "e";
                else if (!m_isStatic && m_scent.Equals("vanilla") && distance.Equals("far"))
                    scentPort = "d";
                else if (!m_isStatic && m_scent.Equals("lemon") && distance.Equals("close"))
                    scentPort = "c";
                else if (m_isStatic && m_scent.Equals("lemon") || !m_isStatic && m_scent.Equals("lemon") && distance.Equals("middle"))
                    scentPort = "b";
                else if (!m_isStatic && m_scent.Equals("lemon") && distance.Equals("far"))
                    scentPort = "a";
                else if (m_isStatic && m_scent.Equals("odorless") || !m_isStatic && m_scent.Equals("odorless") && distance.Equals("close"))
                    scentPort = "h";
                else if (!m_isStatic && m_scent.Equals("odorless") && distance.Equals("middle"))
                    scentPort = "g";
                else if (!m_isStatic && m_scent.Equals("odorless") && distance.Equals("far"))
                    scentPort = "h";

                // Debug.Log("Scentport " + scentPort);
                myPort.WriteToArduino(scentPort);
                myPort.cleanAir = false;               

                GameObject.FindObjectOfType<FileLogger>().printProgress("Name: " + gameObject.name + ", scent: " + m_scent + ", distance: " + distance + ", is static: " + m_isStatic);

                tempDistanceName = distance;
                distance = null;
            }
        }
        else if (myHand.currentAttachedObject == null && myPort.cleanAir == false)
        {
            scentPort = "g";

            // set cleanAir boolean true so that we don't try to set scentPort to "g" multiple times
            myPort.cleanAir = true;

            //Debug.Log("Clean air");
            myPort.WriteToArduino(scentPort);

            GameObject.FindObjectOfType<FileLogger>().printProgress("Object not in hand");

            tempDistanceName = distance;
            distance = null;
        }
    }
}

/*
    a = lemon far
    b = lemon middle
    c = lemon close
    d = vanilla far
    e = vanilla middle
    f = vanilla close
    g = clean air
    h = clean air
*/


               

