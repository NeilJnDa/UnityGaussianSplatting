using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

[System.Serializable]
public class CollisionTriggerEvent : UnityEvent<Collider>
{
}
public class PortalCollider : MonoBehaviour
{
    public Collider GetCollider {
        get {
            if (m_collider == null)
                m_collider = GetComponent<Collider>();
            return m_collider;
        } }
    private Collider m_collider;
    public CollisionTriggerEvent MainCameraTriggerFromFront = new CollisionTriggerEvent();
    public CollisionTriggerEvent MainCameraLeave = new CollisionTriggerEvent();

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform.tag == "MainCamera")
        {
            Vector3 relativeDirection = other.transform.position - this.transform.position;

            float dot = Vector3.Dot(this.transform.forward, relativeDirection);
            if(dot > 0)
            {
                //Trigger from front
                MainCameraTriggerFromFront.Invoke(other);
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.transform.tag == "MainCamera")
        {
            //Leave
            MainCameraLeave.Invoke(other);
        }
    }
}
