using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ARD_TargetHeight : MonoBehaviour
{
    Transform me = default;

    [SerializeField]
    private float minHeight = 1f, maxHeight = 1.5f;

    void Start()
    {
        valuesIntialisation();

        RandomHeight(minHeight, maxHeight);

    }

    void valuesIntialisation()
    {
        me = this.transform;
    }

    void RandomHeight(float minHeight, float maxHeight)
    {
        float height = Random.Range(minHeight, maxHeight);
        me.position += new Vector3(0, height, 0);
    }
}
