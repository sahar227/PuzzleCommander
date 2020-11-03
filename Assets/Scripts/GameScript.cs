using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameScript : MonoBehaviour
{
    [SerializeField] private GameObject Boards;
    [SerializeField] private GameObject Puzzles;

    private GameObject Board;
    private GameObject Puzzle;

    Dictionary<char, Piece> boardDictionary;
    Dictionary<char, Vector3> boardPositions;

    private Dictionary<string, Piece> UnusedPieces;



    // Start is called before the first frame update
    void OnEnable()
    {
        boardDictionary = new Dictionary<char, Piece>();
        boardPositions = new Dictionary<char, Vector3>();
        UnusedPieces = new Dictionary<string, Piece>();

        Board = Boards.GetComponentsInChildren<RectTransform>(true)
            .First(e => e.gameObject.name == PuzzleParams.PuzzleDifficulty).gameObject;

        Puzzle = Puzzles.GetComponentsInChildren<RectTransform>(true)
            .First(e => e.gameObject.name == PuzzleParams.PuzzleDifficulty)
            .GetComponentsInChildren<RectTransform>(true)
            .First(e => e.gameObject.name == PuzzleParams.PuzzleName).gameObject;

        char spot = 'A';
        foreach(var piece in Puzzle.GetComponentsInChildren<Image>(true))
        {
            var PieceObject = piece.gameObject.AddComponent<Piece>();
            PieceObject.correctSpot = spot;
            PieceObject.PieceId = piece.GetComponentInChildren<TextMeshProUGUI>().text;
            PieceObject.Speed = 100;

            boardDictionary.Add(spot, null);
            UnusedPieces.Add(PieceObject.PieceId, PieceObject);

            spot++;

        }

        int skip = 0;
        foreach (var b in Board.GetComponentsInChildren<Image>(true))
        {
            if(skip % 2 == 0)
                boardPositions.Add(b.gameObject.name[0], b.transform.position);
            skip++;
        }

        Board.SetActive(true);
        Puzzle.SetActive(true);

    }

    private bool IsEmptySpotOnBoard(char boardIndex)
    {
        return boardDictionary.Any((s) => s.Key == boardIndex && s.Value == null);
    }

    public bool CheckVictory()
    {
        foreach (var piece in boardDictionary)
        {
            if (piece.Value == null)
                return false;
            if (piece.Value.correctSpot != piece.Key)
                return false;
        }
        return true;
    }


    public bool MovePieceToBoard(string pieceId, char boardIndex)
    {
        try
        {
            if (IsEmptySpotOnBoard(boardIndex))
            {
                boardDictionary[boardIndex] = TakePieceById(pieceId);
                //board[boardIndex].transform.position = boardPositions[boardIndex];
                boardDictionary[boardIndex].MoveStart(boardPositions[boardIndex]);

                return true;
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    public bool ReovePieceFromBoard(string pieceId)
    {
        try
        {
            foreach (var piece in boardDictionary)
            {
                if (piece.Value == null)
                    continue;
                if (piece.Value.PieceId == pieceId)
                {
                    UnusedPieces.Add(piece.Value.PieceId, piece.Value);
                    piece.Value.MoveStart(piece.Value.originalPos);
                    boardDictionary[piece.Key] = null;
                    return true;
                }
            }
            return false;
        }
        catch
        {
            return false;
        }
    }

    private Piece TakePieceById(string pieceId)
    {
        var result = UnusedPieces[pieceId];
        UnusedPieces.Remove(pieceId);
        return result;
    }
}
