using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace GoogleARCore.Examples.HelloAR
{
    public class ARD_Menu : MonoBehaviour
    {
        Transform text = default;
        BoxCollider textCollider = default;
        public static LevelManager levelManager = default;
        void Start()
        {
            Initialisation();
        }

        private void OnCollisionEnter(Collision collision)
        {
            if (collision.gameObject.CompareTag("shuriken"))
            {
                levelManager.level++;
                FindObjectOfType<ARD_SoundManager>().Play("PlayHit");
                GameController.ActivateGameObject(GameController.observationController, true);
            }
        }

        void Initialisation()
        {
            text = this.transform;
            textCollider = this.GetComponent<BoxCollider>();
            levelManager = GetComponent<LevelManager>();
        }

    }
}
