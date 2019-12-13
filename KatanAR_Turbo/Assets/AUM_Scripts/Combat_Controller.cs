using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Combat_Controller : MonoBehaviour
{
    GameObject roomController;

    float levelTimer;

    GameObject loseCanvas;

    GameObject combatCanvas;

    GameObject endCombatCanvas;

    Text textTimer;

    int numberOfTargetToHit;

    int oldMask;

    private void OnEnable()
    {
        if(GameController.startPassed)
        {
            GameController.ActivateGameObject(GameController.observationController, false);
            GameController.ActivateGameObject(GameController.resultController, false);
            roomController = GameObject.FindGameObjectWithTag("RepereCentre");

            levelTimer = roomController.GetComponent<Room_Controller>().levelTime;

            numberOfTargetToHit = roomController.GetComponent<Room_Controller>().levelNumberOfTarget;

            combatCanvas.SetActive(true);
            endCombatCanvas.SetActive(false);
            loseCanvas.SetActive(false);

            GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

            foreach(GameObject target in targets)
            {
                target.GetComponent<MeshRenderer>().enabled = false;
                target.GetComponentInChildren<ParticleSystem>().enableEmission = false;
            }
        }

    }

    private void OnDisable()
    {
        combatCanvas.SetActive(false);
    }

    private void Awake()
    {
        combatCanvas = GameObject.Find("CombatCanvas");
        endCombatCanvas = GameObject.Find("EndCombat");
        loseCanvas = GameObject.Find("Lose");
        textTimer = GameObject.Find("TextTimer").GetComponent<Text>();
    }

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

        foreach(GameObject target in targets)
        {
            if(target.GetComponent<Cible_Info>() != null)
            if (target.GetComponent<Cible_Info>().hit)
            numberOfTargetToHit -= 1;
        }

        if(numberOfTargetToHit == 0)
        {
            EndCombat();
        }
        else
        {
            numberOfTargetToHit = roomController.GetComponent<Room_Controller>().levelNumberOfTarget;
        }

        if(levelTimer > 0)
        {
            levelTimer -= Time.deltaTime;
            textTimer.text = Mathf.Round(levelTimer).ToString();
        }
        else
        {
            EndCombat();
            Lose();
        }

    }

    public IEnumerator WriteMessage(string message)
    {
        GameController.messageBar.SetActive(true);
        GameController.messageBar.GetComponentInChildren<Text>().text = message;

        yield return new WaitForSeconds(3f);

        GameController.messageBar.SetActive(false);
    }

    void EndCombat()
    {
        endCombatCanvas.SetActive(true);

        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

        foreach (GameObject target in targets)
        {
            target.GetComponent<MeshRenderer>().enabled = true;
            target.GetComponentInChildren<ParticleSystem>().enableEmission = true;
        }

        GameObject.Find("First Person Camera").GetComponent<Camera>().cullingMask |= 1 << LayerMask.NameToLayer("ResultTrailRenderer"); ;
    }

    void Lose()
    {
        loseCanvas.SetActive(true);
        
    }

    public void StartObservation()
    {
        GameController.ActivateGameObject(GameController.observationController, true);

        GameObject[] targets = GameObject.FindGameObjectsWithTag("target");

        foreach (GameObject target in targets)
        {
            target.GetComponent<Cible_Info>().hit = false;
        }

        GameObject.Find("First Person Camera").GetComponent<Camera>().cullingMask &= ~(1 << LayerMask.NameToLayer("ResultTrailRenderer"));

    }

}
