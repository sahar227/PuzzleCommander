using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MockInput : MonoBehaviour
{

    private GameManager gm;
    // Start is called before the first frame update
    void Start()
    {
        gm = GameObject.FindObjectOfType<GameManager>();
        Invoke("DoSomething", 2);
    }

    void DoSomething()
    {
        gm.MovePieceToBoard(0, 'A');
       // gm.MovePieceToBoard(1, 'B');
       // gm.MovePieceToBoard(2, 'C');
       // gm.MovePieceToBoard(3, 'D');

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
