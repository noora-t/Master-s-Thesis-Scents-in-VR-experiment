using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Valve.VR;
using Valve.VR.InteractionSystem;

public class HandOwn : MonoBehaviour
{
    public SteamVR_Action_Boolean m_GrabAction = null;

    private SteamVR_Behaviour_Pose m_Pose = null;
    private FixedJoint m_Joint = null;

    public InteractableOwn m_CurrentInteractable = null;
    //private Interactable m_OtherInteractable = null;
    public List<InteractableOwn> m_ContactInteractables = new List<InteractableOwn>();
    private GameObject animatedObject = null;

    private void Awake()
    {
        m_Pose = GetComponent<SteamVR_Behaviour_Pose>();
        m_Joint = GetComponent<FixedJoint>();
    }

    private void Update()
    {
        // Down
        if (m_GrabAction.GetStateDown(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger down");
            Pickup();
        }

        // Up
        if (m_GrabAction.GetStateUp(m_Pose.inputSource))
        {
            print(m_Pose.inputSource + " Trigger Up");
            Drop();
        }

        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ContactInteractables.Add(other.gameObject.GetComponent<InteractableOwn>());
    }


    private void OnTriggerExit(Collider other)
    {
        if (!other.gameObject.CompareTag("Interactable"))
            return;

        m_ContactInteractables.Remove(other.gameObject.GetComponent<InteractableOwn>());
    }

    public void Pickup()
    {
        // Get nearest
        m_CurrentInteractable = GetNearestInteractable();

        // Null check
        if (!m_CurrentInteractable)
            return;

        // Already held, check
        if (m_CurrentInteractable.m_ActiveHand)
            m_CurrentInteractable.m_ActiveHand.Drop();

        // Position (uncomment to adjust to center of object)
        //m_CurrentInteractable.transform.position = transform.position;

        // Attach
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        m_Joint.connectedBody = targetBody;

        // Set active hand
        m_CurrentInteractable.m_ActiveHand = this;
     
        GameObject.FindObjectOfType<FileLogger>().printProgress("Picked up: " + m_CurrentInteractable.name);

        animatedObject = m_CurrentInteractable.gameObject;
    }

    public void Drop()
    {
        // Null check
        if (!m_CurrentInteractable)
            return;

        // Apply velocity
        Rigidbody targetBody = m_CurrentInteractable.GetComponent<Rigidbody>();
        targetBody.velocity = m_Pose.GetVelocity();
        targetBody.angularVelocity = m_Pose.GetAngularVelocity();

        GameObject.FindObjectOfType<FileLogger>().printProgress("Dropped: " + m_CurrentInteractable.name);

        // Stop cap animation
        animatedObject = m_CurrentInteractable.gameObject;

        // Detach
        m_Joint.connectedBody = null;

        // Clear
        m_CurrentInteractable.m_ActiveHand = null;
        m_CurrentInteractable = null;
    }

    private InteractableOwn GetNearestInteractable()
    {
        InteractableOwn nearest = null;
        float minDistance = float.MaxValue;
        float distance = 0.0f;

        foreach (InteractableOwn interactable in m_ContactInteractables)
        {
            distance = (interactable.transform.position - transform.position).sqrMagnitude;

            if (distance < minDistance && interactable.gameObject.activeSelf == true)
            {
                minDistance = distance;
                nearest = interactable;
            }
        }
        return nearest;
    }

    
}
