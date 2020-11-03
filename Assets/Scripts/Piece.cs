using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Piece : MonoBehaviour
{
    public string PieceId;
    public char correctSpot;
    public float Speed = 1f;

    public Vector3 originalPos;
    [SerializeField] private Vector3 _MoveToPosition;
   // private bool shouldMove = false;
    public void MoveStart(Vector3 newPos)
    {
        _MoveToPosition = newPos;
        //shouldMove = true;
    }

   /* private void Move()
    {
        if (transform.position.x > _MoveToPosition.x)
            transform.Translate
    }*/
    // Start is called before the first frame update
    void OnEnable()
    {
        _MoveToPosition = transform.position;
        originalPos = transform.position;
        //GetComponentInChildren<TextMeshProUGUI>().text = PieceId.ToString();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.MoveTowards(transform.position, _MoveToPosition, 0.1f * Speed);
        //transform.Translate(_MoveToPosition);
        /*  if (shouldMove)
          {

          }*/
    }
}
