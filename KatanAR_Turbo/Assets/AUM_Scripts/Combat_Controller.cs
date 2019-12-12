using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Controller : MonoBehaviour
{



    private void OnEnable()
    {
        GameController.ActivateGameObject(GameController.observationController, false);
        GameController.ActivateGameObject(GameController.scanningController, false);
        GameController.ActivateGameObject(GameController.resultController, false);
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public IEnumerator WriteMessage(string message)
    {
        GameController.messageBar.SetActive(true);
        GameController.messageBar.GetComponentInChildren<Text>().text = message;

        yield return new WaitForSeconds(3f);

        GameController.messageBar.SetActive(false);
    }

}
