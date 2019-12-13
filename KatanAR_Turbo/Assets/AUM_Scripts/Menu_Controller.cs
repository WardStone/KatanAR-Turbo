using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Controller : MonoBehaviour
{

    private void OnEnable()
    {
        GameController.ActivateGameObject(GameController.scanningController, false);
        GameController.ActivateGameObject(GameController.observationController, false);
        GameController.ActivateGameObject(GameController.combatController, false);
        GameController.ActivateGameObject(GameController.resultController, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
