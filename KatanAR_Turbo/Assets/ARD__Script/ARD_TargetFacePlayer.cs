using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARD_TargetFacePlayer : MonoBehaviour
{
     public Transform cible = null;

     public GameObject player = default;
     public Transform playerLocalisation = default;

    Quaternion targetRotation = default;
    Vector3 targetDirection = default;

    private void Start()
    {
        InitiateVariables();
    }

    void Update()
    {
        FaceIt(playerLocalisation);
    }

    void InitiateVariables()
    {
        cible = this.transform;
        player = GameObject.FindGameObjectWithTag("MainCamera");
        playerLocalisation = player.transform;
    }

    void FaceIt(Transform TheThingToFace)
    {
        targetDirection = TheThingToFace.position - cible.position;
        targetRotation = Quaternion.LookRotation(targetDirection, Vector3.forward);
        cible.rotation = Quaternion.Lerp(transform.rotation, targetRotation, 100 * Time.deltaTime);
    }

    private void OnDrawGizmos()
    {
        Debug.DrawRay(cible.position, targetDirection, Color.blue);
    }
}
