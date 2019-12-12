using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Scanning_Controller : MonoBehaviour
{
    

    private void OnEnable()
    {
        StartCoroutine(WriteMessage("KatanARMan! Scan une surface puis mets-y un point"));
        GameController.ActivateGameObject(GameController.observationController, false);
        GameController.ActivateGameObject(GameController.combatController, false);
        GameController.ActivateGameObject(GameController.resultController, false);
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
