using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class ObjectPlacement : MonoBehaviour
{
    public GameObject practiceSphere;

    List<GameObject> objects = new List<GameObject>();
    GameObject[] boxes;
    List<Vector3> objectRotations = new List<Vector3>();
    GameObject tempObject;

    void Start()
    {
        boxes = GameObject.FindGameObjectsWithTag("Box");

        foreach (Transform child in transform)
        {
            objects.Add(child.gameObject);
        }          

        RandomizeOrder();
        PlaceInBoxes(1);
    }

    void Update()
    {
        if (Input.GetButtonDown("ObjectPlacement")) // down arrow
        {
            practiceSphere.transform.position = new Vector3(37.586f, 0.4461f, -4.6f);
            practiceSphere.transform.eulerAngles = new Vector3(0, 0, 0);

            PlaceInBoxes(2);
        }
    }

    void RandomizeOrder() 
    {
        for (int i = 0; i < objects.Count - 1; i++)
        {
            int rnd = UnityEngine.Random.Range(i, objects.Count);
            tempObject = objects[rnd];
            objects[rnd] = objects[i];
            objects[i] = tempObject;
        }

        GameObject.FindObjectOfType<FileLogger>().printProgress("Object order randomized.");
    }

    public void PlaceInBoxes(int phase)
    {       
        for (int i = 0; i < objects.Count; i++)
        {
            if (phase == 1)
                objectRotations.Add(objects[i].transform.eulerAngles);
            else if (phase == 2)
                 objects[i].transform.eulerAngles = objectRotations[i];

            objects[i].transform.position = boxes[i].transform.position - new Vector3(0, 0.1f, 0) + new Vector3(0.05f, 0, 0);
       
        }

        GameObject.FindObjectOfType<FileLogger>().printProgress("Objects placed in boxes.");
    }
}
