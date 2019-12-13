using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Katana_Controller : MonoBehaviour
{

    GameObject katanaPrefab;

    private void OnEnable()
    {
        GameController.ActivateGameObject(GameController.shurikenController, true);

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
