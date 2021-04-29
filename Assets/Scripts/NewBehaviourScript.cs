using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    string objectName;
    string distance;
    string scent;
    bool isStatic;

   // public Hand m_ActiveHand = null;
    public COMPort myPort = null;
    public int valveOpen = 0;
    public string scentPort = "no";   

    private void OnTriggerEnter(Collider other)
    {
        objectName = other.transform.parent.name;
        distance = other.gameObject.name;
        scent = other.transform.parent.gameObject.GetComponent<ScentInformation>().m_scent;
        isStatic = other.transform.parent.gameObject.GetComponent<ScentInformation>().m_isStatic;

        GameObject.FindObjectOfType<FileLogger>().printProgress("Name: " + objectName + ", scent: " + scent + ", distance: " + distance + ", is static: " + isStatic);


        if (isStatic && scent.Equals("vanilla") || !isStatic && scent.Equals("vanilla") && distance.Equals("Close"))
            scentPort = "c";
        else if (!isStatic && scent.Equals("vanilla") && distance.Equals("Middle"))
            scentPort = "b";
        else if (!isStatic && scent.Equals("vanilla") && distance.Equals("Far"))
            scentPort = "a";
        else if (isStatic && scent.Equals("lemon") || !isStatic && scent.Equals("lemon") && distance.Equals("Close"))
            scentPort = "f";
        else if (!isStatic && scent.Equals("lemon") && distance.Equals("Middle"))
            scentPort = "e";
        else if (!isStatic && scent.Equals("lemon") && distance.Equals("Far"))
            scentPort = "d";

        Debug.Log("Scentport " + scentPort);
        /*
        if (m_ActiveHand != null)
        {
            if (m_ActiveHand.grabbed == 1 && valveOpen == 0)
            {
                valveOpen = 1;
                myPort = FindObjectOfType(typeof(COMPort)) as COMPort;
                if (myPort != null && scent != null && myPort.triggerOnPickup == false)
                    myPort.WriteToArduino(scentPort);
            }
        }
        */
    }

    private void OnTriggerExit(Collider other)
    {
        objectName = other.transform.parent.name;
        distance = other.gameObject.name;
        scent = other.transform.parent.gameObject.GetComponent<ScentInformation>().m_scent;
        isStatic = other.transform.parent.gameObject.GetComponent<ScentInformation>().m_isStatic;

        GameObject.FindObjectOfType<FileLogger>().printProgress("Name: " + objectName + ", scent: " + scent + ", distance: " + distance + ", is static: " + isStatic);

        if (isStatic && scent.Equals("vanilla") || !isStatic && scent.Equals("vanilla") && distance.Equals("Close"))
            scentPort = "b";
        else if (!isStatic && scent.Equals("vanilla") && distance.Equals("Middle"))
            scentPort = "a";
        else if (!isStatic && scent.Equals("vanilla") && distance.Equals("Far"))
            scentPort = "g";
        else if (isStatic && scent.Equals("lemon") || !isStatic && scent.Equals("lemon") && distance.Equals("Close"))
            scentPort = "e";
        else if (!isStatic && scent.Equals("lemon") && distance.Equals("Middle"))
            scentPort = "d";
        else if (!isStatic && scent.Equals("lemon") && distance.Equals("Far"))
            scentPort = "g";

        Debug.Log("Scentport " + scentPort);

        /*
        if (m_ActiveHand != null) { 
            if (m_ActiveHand.grabbed == 1 && valveOpen == 1)
            {
                valveOpen = 0;
                scentPort = "g";
                myPort = FindObjectOfType(typeof(COMPort)) as COMPort;
                if (myPort != null && scent != null && myPort.triggerOnPickup == false)
                    myPort.WriteToArduino(scentPort);
            }
        }
        */
    }
}



/*
    a = vanilla far
    b = vanilla middle
    c = vanilla close
    d = lemon far
    e = lemon middle
    f = lemon close
    g = clean air
*/
