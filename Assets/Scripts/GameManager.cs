using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    Dictionary<char, Piece> board;
    Dictionary<char, Vector3> boardPositions;
    private PieceManager pm;
    [SerializeField] SpriteRenderer boardElement;
    [SerializeField] int rows = 2;
    [SerializeField] int cols = 2;
    Sprite s;

    // Start is called before the first frame update
    void Start()
    {
        pm = GameObject.FindObjectOfType<PieceManager>();
        InitializeBoard();
    }

    private void InitializeBoard()
    {
        board = new Dictionary<char, Piece>();
        boardPositions = new Dictionary<char, Vector3>();
        char index = 'A';
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < cols; j++)
            {
                board.Add(index, null);
                boardPositions.Add(index, new Vector3(transform.position.x + j * boardElement.transform.localScale.x,
                    transform.position.y - i * boardElement.transform.localScale.y,
                    0));
                index++;
            }
        }
        DrawBoard();
    }

    private void DrawBoard()
    {
        char boardIndex = 'A';
        var piece = pm.getUnusedPieces()[0];
       // boardElement.size = piece.GetComponent<SpriteRenderer>().size;

        for (int i = 0; i < rows; i++)
        {
            for( int j = 0; j < cols; j++)
            {

                var be = Instantiate(boardElement,
                    boardPositions[boardIndex],
                    Quaternion.identity);
                be.GetComponentInChildren<TextMeshPro>().text = boardIndex.ToString();
                
                //piece.transform.localScale = new Vector3()
                boardIndex++;
            }
        }
    }

    private bool IsEmptySpotOnBoard(char boardIndex)
    {
        return board.Any((s) => s.Key == boardIndex && s.Value == null);
    }

    public bool MovePieceToBoard(int pieceId, char boardIndex)
    {
        if(IsEmptySpotOnBoard(boardIndex))
        {
            board[boardIndex] = pm.TakePieceById(pieceId);
            //board[boardIndex].transform.position = boardPositions[boardIndex];
            board[boardIndex].MoveStart(boardPositions[boardIndex]);

            if (CheckVictory())
                Debug.Log("Win!");
            return true;
        }
        return false;
    }

    bool CheckVictory()
    {
        foreach(var piece in board)
        {
            if (piece.Value == null)
                return false;
            if (piece.Value.correctSpot != piece.Key)
                return false;
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
