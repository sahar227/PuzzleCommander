using Assets.Scripts;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class PieceManager : MonoBehaviour
{
    private List<Piece> UnusedPieces;
    private GameObject PieceSource;
    [SerializeField] private float SpaceBetweenPieces = 1f;
    [SerializeField] private float PiecesScale = 1f;

    public List<Piece> getUnusedPieces()
    {
        return UnusedPieces;
    } 

    private void InitPieces()
    {
        int i = 0;
        foreach (var piece in UnusedPieces)
        {
            piece.transform.SetPosition(transform.position
                + new Vector3((
                i * piece.GetComponent<SpriteRenderer>().size.x * PieceSource.transform.localScale.x
                + SpaceBetweenPieces * i),0,0));
            i++; 
        }
    }

    private void UpdatePieces()
    {
        int i = 0;
        foreach (var piece in UnusedPieces)
        {
            piece.MoveStart(transform.position
                + new Vector3((
                i * piece.GetComponent<SpriteRenderer>().size.x * PieceSource.transform.localScale.x
                + SpaceBetweenPieces * i), 0, 0));
            i++;
        }
    }


    public Piece TakePieceById(int id)
    {
        var piece = (from p in UnusedPieces where p.PieceId == "" select p).FirstOrDefault();
        UnusedPieces.Remove(piece);
        UpdatePieces();
        return piece;
    }

    public void InsertPieceBack(Piece piece)
    {
        UnusedPieces.Add(piece);
        InitPieces();
    }
    void OnEnable()
    {
        // Get reference to all pieces
        UnusedPieces = new List<Piece>();
        PieceSource = GameObject.FindGameObjectWithTag("Puzzle Source");
        PieceSource.GetComponentsInChildren<Piece>(UnusedPieces);

        // Take care of pieces presentation
        PieceSource.SetActive(true);
        PieceSource.transform.localScale = new Vector3(PiecesScale, PiecesScale, 0);
        InitPieces();
    }


    // Update is called once per frame
    void Update()
    {
        
    }
}
