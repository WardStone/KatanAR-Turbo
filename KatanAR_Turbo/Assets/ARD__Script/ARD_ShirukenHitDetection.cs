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

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
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
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}
