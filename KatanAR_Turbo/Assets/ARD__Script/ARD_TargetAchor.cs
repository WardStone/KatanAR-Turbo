using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
[RequireComponent(typeof(Rigidbody))]
public class ARD_TargetAchor : MonoBehaviour
{
    public string AvertissementTag = "il faut donner le tag anchor à l'objet";
    public string AvertissementRb  = "il faut que le rigidbody soit en Kinématique";

#region Variables

    [SerializeField]
    GameObject mySelf;

    [SerializeField]
    SphereCollider myCollider;

    [SerializeField]
    bool haveCheckAround = false;

#endregion

    void Start()
    {
        valuesIntialisation();

    }

    void OnTriggerStay(Collider other)
    {
        if (!haveCheckAround)
        {
            DectectOtherAnchor(other);
        }
    }

    void valuesIntialisation()
    {
        mySelf = this.gameObject;
        myCollider = this.GetComponent<SphereCollider>();
    }

    void DectectOtherAnchor(Collider other)
    {
        if (other.CompareTag("anchor"))
        {
            Destroy(mySelf);
        }
        else
        {
            haveCheckAround = true;
        }
    }
}
