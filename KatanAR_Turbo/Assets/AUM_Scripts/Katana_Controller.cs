using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana_Controller : MonoBehaviour
{
    GameObject katanas;

    GameObject katanaPrefab;

    GameObject katanaButton;

    GameObject shurikenButton;

    private void OnEnable()
    {
        GameController.ActivateGameObject(GameController.shurikenController, false);

        GameController.ActivateGameObject(katanas, true);

        GameController.ActivateGameObject(katanaButton, false);

        GameController.ActivateGameObject(shurikenButton, true);

    }

    private void OnDisable()
    {
        GameController.ActivateGameObject(katanas, false);
    }

    // Start is called before the first frame update
    void Start()
    {
        katanas = GameObject.Find("Katanas");
        katanaButton = GameObject.Find("KatanaButton");
        shurikenButton = GameObject.Find("ShurikenButton");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
