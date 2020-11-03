using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class textOrderInLayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        var mesh = gameObject.GetComponent<MeshRenderer>();
        mesh.sortingLayerName = "Game Layer";
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
