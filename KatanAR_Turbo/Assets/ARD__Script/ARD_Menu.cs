using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARD_Menu : MonoBehaviour
{
    Transform text = default;
    BoxCollider textCollider = default;

    void Start()
    {
        Initialisation();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("shuriken"))
        {

        }
    }

    void Initialisation()
    {
        text = this.transform;
        textCollider = this.GetComponent<BoxCollider>();
    }


}
