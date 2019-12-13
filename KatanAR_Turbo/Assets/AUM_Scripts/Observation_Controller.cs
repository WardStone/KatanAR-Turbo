using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Observation_Controller : MonoBehaviour
{
    GameObject cacheButton;

    GameObject firstPersonCamera;

    GameObject repereCentre;

    private void OnEnable()
    {
        if(GameController.startPassed)
        {
            GameController.ActivateGameObject(GameController.menuController, false);
            GameController.ActivateGameObject(GameController.combatController, false);
            GameController.ActivateGameObject(GameController.scanningController, false);
            GameController.ActivateGameObject(GameController.resultController, false);

            GameController.observationCanvas.SetActive(true);
            
            StartCoroutine(WriteMessage("Observe tes alentours, puis quand tu es prêts KatanARman, places-toi au milieu"));

            GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

            foreach (GameObject target in targets)
            {
                target.GetComponent<MeshRenderer>().enabled = true;
                target.GetComponentInChildren<ParticleSystem>().enableEmission = true;
            }

            GameObject[] shurikens = GameObject.FindGameObjectsWithTag("shuriken");

            foreach (GameObject shuriken in shurikens)
            {
                Destroy(shuriken);
            }

        }

    }



    // Start is called before the first frame update
    void Start()
    {
        cacheButton = GameObject.Find("CacheButton");

        firstPersonCamera = GameObject.Find("First Person Camera");
    }

    // Update is called once per frame
    void Update()
    {
        repereCentre = GameObject.FindGameObjectWithTag("RepereCentre");

        if (firstPersonCamera.transform.position.x - 1f < repereCentre.transform.position.x &&
            repereCentre.transform.position.x < firstPersonCamera.transform.position.x + 1 &&
            firstPersonCamera.transform.position.z - 1 < repereCentre.transform.position.z &&
            repereCentre.transform.position.z < firstPersonCamera.transform.position.z + 1)
        {
            cacheButton.SetActive(false); 
        }
        else
        {
            cacheButton.SetActive(false);
        }
    }

    public IEnumerator WriteMessage(string message)
    {
        GameController.messageBar.SetActive(true);
        GameController.messageBar.GetComponentInChildren<Text>().text = message;

        yield return new WaitForSeconds(5f);

        GameController.messageBar.SetActive(false);
    }

    public void StartCombat()
    {
        GameController.ActivateGameObject(GameController.combatController, true);
        GameController.observationCanvas.SetActive(false);
    }

}
