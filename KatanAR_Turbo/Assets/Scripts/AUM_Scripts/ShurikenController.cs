using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShurikenController : MonoBehaviour
{
    [HideInInspector] public Rigidbody rigibody;
    Vector3 instantiateDirection;

    float launchSpeed  = 1;

    void Awake()
    {
        rigibody = GetComponentInChildren<Rigidbody>();

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void StopMoving()
    {
        
    }

}
