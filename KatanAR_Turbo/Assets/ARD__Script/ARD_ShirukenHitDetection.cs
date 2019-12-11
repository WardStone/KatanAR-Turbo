using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARD_ShirukenHitDetection : MonoBehaviour
{
    Transform shuriken = default;
    MeshCollider shurikenCollider = default;
    Rigidbody shurikenBody = default;

    void Start()
    {
        Initialisation();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("target"))
        {
            TheWorld(shurikenBody);
        }
    }

    void Initialisation()
    {
        shuriken = this.transform;
        shurikenCollider = this.GetComponent<MeshCollider>();
        shurikenBody = GetComponentInChildren<Rigidbody>();
    }

    void TheWorld(Rigidbody rb)
    {
        //je pourrais faire un FreezeAll mais ce ne serait pas marrant
        rb.constraints = RigidbodyConstraints.FreezePositionX | RigidbodyConstraints.FreezeRotationX;
        rb.constraints = RigidbodyConstraints.FreezePositionY | RigidbodyConstraints.FreezeRotationY;
        rb.constraints = RigidbodyConstraints.FreezePositionZ | RigidbodyConstraints.FreezeRotationZ;
    }

}
