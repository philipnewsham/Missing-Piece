using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShowMoves : MonoBehaviour
{
    private ChessController chessController;

    void Start()
    {
        chessController = FindObjectOfType<ChessController>();
    }

	public List<Vector2> ShowPossibleMoves(Vector2 position, bool isWhite, PieceTitle.Piece piece, bool firstMove)
    {
        List<Vector2> possibleMoves = new List<Vector2>();
        switch (piece)
        {
            case PieceTitle.Piece.QUEEN:
                possibleMoves = BishopMoves(position, isWhite);
                possibleMoves.AddRange(RookMoves(position, isWhite));
                break;
            case PieceTitle.Piece.BISHOP:
                possibleMoves = BishopMoves(position, isWhite);
                break;
            case PieceTitle.Piece.KNIGHT:
                possibleMoves = KnightMoves(position, isWhite);
                break;
            case PieceTitle.Piece.ROOK:
                possibleMoves = RookMoves(position, isWhite);
                break;
            case PieceTitle.Piece.PAWN:
                possibleMoves = PawnMoves(position, isWhite, firstMove);
                break;
        }
        return possibleMoves;
    }
    
    List<Vector2> BishopMoves(Vector2 position, bool isWhite)
    {
        List<Vector2> possibleMoves = new List<Vector2>();
        Vector2 checkSpace = new Vector2();
        PieceController checkInfo = null;
        Vector2 gridCoordinate = position;

        //checking diagonal UR
        if (gridCoordinate.x < 7 && gridCoordinate.y < 7)
        {
            bool isFinished = false;
            for (int i = 1; i < 8; i++)
            {
                if (!isFinished)
                {
                    checkSpace = new Vector2(gridCoordinate.x + i, gridCoordinate.y + i);
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                    if (gridCoordinate.x + i > 7 || gridCoordinate.y + i > 7 || (checkInfo != null && checkInfo.isWhite == isWhite))
                        isFinished = true;
                    else if (checkInfo == null)
                        possibleMoves.Add(checkSpace);
                    else if (checkInfo.isWhite == !isWhite)
                    {
                        possibleMoves.Add(checkSpace);
                        isFinished = true;
                    }
                }
            }
        }

        //checking diagonal UL
        if (gridCoordinate.x > 0 && gridCoordinate.y < 7)
        {
            bool isFinished = false;
            for (int i = 1; i < 8; i++)
            {
                if (!isFinished)
                {
                    checkSpace = new Vector2(gridCoordinate.x - i, gridCoordinate.y + i);
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                    if (gridCoordinate.x - i < 0 || gridCoordinate.y + i > 7 || (checkInfo != null && checkInfo.isWhite == isWhite))
                        isFinished = true;
                    else if (checkInfo == null)
                        possibleMoves.Add(checkSpace);
                    else if (checkInfo.isWhite == !isWhite)
                    { 
                        possibleMoves.Add(checkSpace);
                        isFinished = true;
                    }
                }
            }
        }

        //checking diagonal DR
        if (gridCoordinate.x < 7 && gridCoordinate.y > 0)
        {
            bool isFinished = false;
            for (int i = 1; i < 8; i++)
            {
                if (!isFinished)
                {
                    checkSpace = new Vector2(gridCoordinate.x + i, gridCoordinate.y - i);
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                    if (gridCoordinate.x + i > 7 || gridCoordinate.y - i < 0 || (checkInfo != null && checkInfo.isWhite == isWhite))
                        isFinished = true;
                    else if (checkInfo == null)
                        possibleMoves.Add(checkSpace);
                    else if (checkInfo.isWhite == !isWhite)
                    {
                        possibleMoves.Add(checkSpace);
                        isFinished = true;
                    }
                }
            }
        }

        //checking diagonal DL
        if (gridCoordinate.x > 0 && gridCoordinate.y > 0)
        {
            bool isFinished = false;
            for (int i = 1; i < 8; i++)
            {
                if (!isFinished)
                {
                    checkSpace = new Vector2(gridCoordinate.x - i, gridCoordinate.y - i);
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                    if (gridCoordinate.x - i < 0 || gridCoordinate.y - i < 0 || (checkInfo != null && checkInfo.isWhite == isWhite))
                        isFinished = true;
                    else if (checkInfo == null)
                        possibleMoves.Add(checkSpace);
                    else if (checkInfo.isWhite == !isWhite)
                    {
                        possibleMoves.Add(checkSpace);
                        isFinished = true;
                    }
                }
            }
        }
        return possibleMoves;
    }

    List<Vector2> KnightMoves(Vector2 position, bool isWhite)
    {
        List<Vector2> possibleMoves = new List<Vector2>();
        Vector2 checkSpace = new Vector2();
        PieceController checkInfo = null;
        Vector2 gridCoordinate = position;

        //checking UL
        if (gridCoordinate.x - 1 >= 0 && gridCoordinate.y + 2 < 8)
        {
            checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y + 2);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking UR
        if (gridCoordinate.x + 1 < 8 && gridCoordinate.y + 2 < 8)
        {
            checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y + 2);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking UL
        if (gridCoordinate.x - 1 >= 0 && gridCoordinate.y + 2 < 8)
        {
            checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y + 2);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking RU
        if (gridCoordinate.x + 2 < 8 && gridCoordinate.y + 1 < 8)
        {
            checkSpace = new Vector2(gridCoordinate.x + 2, gridCoordinate.y + 1);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking RD
        if (gridCoordinate.x + 2 < 8 && gridCoordinate.y - 1 >= 0)
        {
            checkSpace = new Vector2(gridCoordinate.x + 2, gridCoordinate.y - 1);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking RU
        if (gridCoordinate.x + 2 < 8 && gridCoordinate.y + 1 < 8)
        {
            checkSpace = new Vector2(gridCoordinate.x + 2, gridCoordinate.y + 1);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking DR
        if (gridCoordinate.x + 1 < 8 && gridCoordinate.y - 2 >= 0)
        {
            checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y - 2);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking DR
        if (gridCoordinate.x + 1 < 8 && gridCoordinate.y - 2 >= 0)
        {
            checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y - 2);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking DL
        if (gridCoordinate.x - 1 >= 0 && gridCoordinate.y - 2 >= 0)
        {
            checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y - 2);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking LD
        if (gridCoordinate.x - 2 >= 0 && gridCoordinate.y - 1 >= 0)
        {
            checkSpace = new Vector2(gridCoordinate.x - 2, gridCoordinate.y - 1);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        //checking RU
        if (gridCoordinate.x - 2 >= 0 && gridCoordinate.y + 1 < 8)
        {
            checkSpace = new Vector2(gridCoordinate.x - 2, gridCoordinate.y + 1);
            checkInfo = chessController.CheckPieceOnSquare(checkSpace);
            if (checkInfo == null || checkInfo.isWhite == !isWhite)
                possibleMoves.Add(checkSpace);
        }

        return possibleMoves;
    }

    List<Vector2> RookMoves(Vector2 position, bool isWhite)
    {
        Vector2 gridCoordinate = position;
        List<Vector2> possibleMoves = new List<Vector2>();
        Vector2 checkSpace = new Vector2();
        PieceController checkInfo = null;

        //checking down line
        if (gridCoordinate.y > 0)
        {
            bool isFinished = false;
            for (int i = 1; i < 8; i++)
            {
                if (!isFinished)
                {
                    checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y - i);
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                    if (gridCoordinate.y - i < 0)//else space is not on board
                        isFinished = true;
                    else if (checkInfo == null) //if space is empty
                        possibleMoves.Add(checkSpace);
                    else if (checkInfo.isWhite == !isWhite) //else if space has an enemy
                    {
                        possibleMoves.Add(checkSpace);
                        isFinished = true;
                    }
                    else if (checkInfo.isWhite == isWhite)//else space has a friendly
                        isFinished = true;
                }
            }
        }

        //checking up line
        if (gridCoordinate.y < 7)
        {
            bool isFinished = false;
            for (int i = 1; i < 8; i++)
            {
                if (!isFinished)
                {
                    checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y + i);
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                    if (gridCoordinate.y + i > 7)//else space is not on board
                        isFinished = true;
                    else if (checkInfo == null) //if space is empty
                        possibleMoves.Add(checkSpace);
                    else if (checkInfo.isWhite == !isWhite) //else if space has an enemy
                    {
                        possibleMoves.Add(checkSpace);
                        isFinished = true;
                    }
                    else if (checkInfo.isWhite == isWhite)//else space has a friendly
                        isFinished = true;
                }
            }
        }

        //checking right line
        if (gridCoordinate.x > 0)
        {
            bool isFinished = false;
            for (int i = 1; i < 8; i++)
            {
                if (!isFinished)
                {
                    checkSpace = new Vector2(gridCoordinate.x - i, gridCoordinate.y);
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                    if (gridCoordinate.x - i < 0)//else space is not on board
                        isFinished = true;
                    else if (checkInfo == null) //if space is empty
                        possibleMoves.Add(checkSpace);
                    else if (checkInfo.isWhite == !isWhite) //else if space has an enemy
                    {
                        possibleMoves.Add(checkSpace);
                        isFinished = true;
                    }
                    else if (checkInfo.isWhite == isWhite)//else space has a friendly
                        isFinished = true;
                }
            }
        }
        //checking right line
        if (gridCoordinate.x < 7)
        {
            bool isFinished = false;
            for (int i = 1; i < 8; i++)
            {
                if (!isFinished)
                {
                    checkSpace = new Vector2(gridCoordinate.x + i, gridCoordinate.y);
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace);

                    if (gridCoordinate.x + i > 7)//else space is not on board
                        isFinished = true;
                    else if (checkInfo == null) //if space is empty
                        possibleMoves.Add(checkSpace);
                    else if (checkInfo.isWhite == !isWhite) //else if space has an enemy
                    {
                        possibleMoves.Add(checkSpace);
                        isFinished = true;
                    }
                    else if (checkInfo.isWhite == isWhite)//else space has a friendly
                        isFinished = true;
                }
            }
        }
        return possibleMoves;
    }

    List<Vector2> PawnMoves(Vector2 position, bool isWhite, bool firstMove)
    {
        Vector2 gridCoordinate = position;
        List<Vector2> possibleMoves = new List<Vector2>();
        Vector2 checkSpace = new Vector2();
        PieceController checkInfo = null;

        if (isWhite)
        {
            //check space in front is empty
            if (gridCoordinate.y + 1 < 8) // checking if space is on the board
            {
                checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y + 1); //checking space directly infront
                if (chessController.CheckPieceOnSquare(checkSpace) == null)
                    possibleMoves.Add(checkSpace);
            }

            //check if first move
            if (firstMove && gridCoordinate.y + 2 < 8) //if first move can move two spaces
            {
                if (possibleMoves.Count > 0)//if piece can't move in front then it wont be able to move two in front
                {
                    checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y + 2); //checking space two infront
                    if (chessController.CheckPieceOnSquare(checkSpace) == null) //if piece doesn't exist
                        possibleMoves.Add(checkSpace); //add coordinate for button
                }
            }

            //check diagonal left
            if (gridCoordinate.x - 1 >= 0 && gridCoordinate.y + 1 < 8) //check if space is on board
            {
                checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y + 1); //checking space up and to the left
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo != null && checkInfo.isWhite == !isWhite) //checking if piece exists AND is piece is opposite colour
                {
                    if (!checkInfo.isKing)//checking if piece is not a king
                        possibleMoves.Add(checkSpace);
                }
            }
            //check diagonal right
            if (gridCoordinate.x + 1 < 8 && gridCoordinate.y + 1 < 8) //check if space is on board
            {
                checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y + 1); //checking space up and to the right
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo != null && checkInfo.isWhite == !isWhite) //checking if piece exists AND is piece is opposite colour
                {
                    if (!checkInfo.isKing)//checking if piece is not a king
                        possibleMoves.Add(checkSpace);
                }
            }
        }
        else
        {
            if (gridCoordinate.y - 1 >= 0) // checking if space is on the board
            {
                checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y - 1); //checking space directly infront
                checkInfo = chessController.CheckPieceOnSquare(checkSpace); //getting info on piece (if piece is there) on square
                if (checkInfo == null) //if piece doesn't exist
                    possibleMoves.Add(checkSpace); //add coordinate for button
            }
            //check if first move
            if (firstMove && gridCoordinate.y - 2 >= 0) //if first move can move two spaces
            {
                if (possibleMoves.Count > 0)//if piece can't move in front then it wont be able to move two in front
                {
                    checkSpace = new Vector2(gridCoordinate.x, gridCoordinate.y - 2); //checking space two infront
                    checkInfo = chessController.CheckPieceOnSquare(checkSpace); //getting info on piece (if piece is there) on square
                    if (checkInfo == null) //if piece doesn't exist
                        possibleMoves.Add(checkSpace); //add coordinate for button
                }
            }
            //check diagonal left
            if (gridCoordinate.x - 1 >= 0 && gridCoordinate.y - 1 >= 0) //check if space is on board
            {
                checkSpace = new Vector2(gridCoordinate.x - 1, gridCoordinate.y - 1); //checking space up and to the left
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo != null && checkInfo.isWhite == !isWhite) //checking if piece exists AND is piece is opposite colour
                {
                    if (!checkInfo.isKing)//checking if piece is not a king
                        possibleMoves.Add(checkSpace);
                }
            }
            //check diagonal right
            if (gridCoordinate.x + 1 < 8 && gridCoordinate.y - 1 >= 0) //check if space is on board
            {
                checkSpace = new Vector2(gridCoordinate.x + 1, gridCoordinate.y - 1); //checking space up and to the right
                checkInfo = chessController.CheckPieceOnSquare(checkSpace);
                if (checkInfo != null && checkInfo.isWhite == !isWhite) //checking if piece exists AND is piece is opposite colour
                {
                    if (!checkInfo.isKing)//checking if piece is not a king
                        possibleMoves.Add(checkSpace);
                }
            }
        }
        return possibleMoves;
    }
}
