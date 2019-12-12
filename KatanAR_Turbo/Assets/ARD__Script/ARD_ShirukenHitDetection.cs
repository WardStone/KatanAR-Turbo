using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARD_ShirukenHitDetection : MonoBehaviour
{
    Transform shuriken = default;
    BoxCollider shurikenCollider = default;
    Rigidbody shurikenBody = default;

    public GameObject trail = default;
    bool haveCollide = false;

    public float rotationSpeed = 10f;

    void Start()
    {
        Initialisation();
    }

    void FixedUpdate()
    {
        if (!haveCollide)
        {
            Quaternion deltaRotation = Quaternion.Euler(new Vector3(0, rotationSpeed, 0) * Time.deltaTime);
            shurikenBody.MoveRotation(shurikenBody.rotation * deltaRotation);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("target"))
        {
            TheWorld(shurikenBody);
            trail.SetActive(false);
            haveCollide = true;
        }
        else
        if (collision.gameObject.CompareTag("LD limit"))
        {
            Destroy(this.gameObject);
        }
    }

    void Initialisation()
    {
        shuriken = this.transform;
        shurikenCollider = this.GetComponent<BoxCollider>();
        shurikenBody = GetComponentInChildren<Rigidbody>();
    }

    void TheWorld(Rigidbody rb)
    {
        rb.velocity = Vector3.zero;
        rb.constraints = RigidbodyConstraints.FreezeAll;
    }

}
