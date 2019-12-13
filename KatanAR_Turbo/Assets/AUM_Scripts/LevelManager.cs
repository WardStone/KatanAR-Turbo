using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{

    public int level = 0;
    [SerializeField] int actualLevel = 0;

    public Vector3 groundAnchor = default;
    public GameObject actualRoom = default;
    public GameObject[] GameAreaRoom = default;

    // Start is called before the first frame update
    void Start()
    {
        actualLevel = level;
    }

    // Update is called once per frame
    void Update()
    {


        if (actualLevel != level)
        { 

            //Je prend le parent(point d'accroche)
            Transform parent = actualRoom.transform.parent.transform;
            //Je supprime le level actuel
            Destroy(actualRoom.gameObject);
            //Le nouveau niveau
            actualRoom = Instantiate(GameAreaRoom[level], groundAnchor, Quaternion.identity, actualRoom.transform.parent.transform);
            //J'inque qu'on est passé au prochain level
            actualLevel = level;
        }
    }
}
