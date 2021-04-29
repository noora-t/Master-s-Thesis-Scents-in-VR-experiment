using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class InteractableOwn : MonoBehaviour
{
    [HideInInspector]
    public HandOwn m_ActiveHand = null;

    private void Awake()
    {

    }

    private void Update()
    {

    }

    private void OnTriggerEnter(Collider other)
    {

    }

    private void OnTriggerExit(Collider other)
    {

    }
}
