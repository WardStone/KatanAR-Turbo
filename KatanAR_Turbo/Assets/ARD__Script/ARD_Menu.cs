using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoogleARCore.Examples.HelloAR
{
    public class ARD_Menu : MonoBehaviour
    {
        Transform text = default;
        BoxCollider textCollider = default;
        ARD_ControllerTargetCreator levelManager = default;
        void Start()
        {
            Initialisation();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("shuriken"))
            {
                levelManager.level++;
            }
        }

        void Initialisation()
        {
            text = this.transform;
            textCollider = this.GetComponent<BoxCollider>();
            levelManager = GetComponent<ARD_ControllerTargetCreator>();
        }

    }
}
