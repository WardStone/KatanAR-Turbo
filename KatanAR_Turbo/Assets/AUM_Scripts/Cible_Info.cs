using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cible_Info : MonoBehaviour
{
    public bool hit = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        hit = true;
    }

    private void OnTriggerEnter(Collider collision)
    {
        hit = true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
