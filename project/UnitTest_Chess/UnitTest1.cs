using System;
using System.Collections.Generic;
using Chess;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest_Chess
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void StandardChessBoardSetup()
        {
            ChessBoard cb = new ChessBoard();
            cb.SetInitialPlacement(false);

            bool pass = (
                cb.Grid[0][0].piece == Piece.ROOK   && cb.Grid[7][0].piece == Piece.ROOK &&
                cb.Grid[0][1].piece == Piece.KNIGHT && cb.Grid[7][1].piece == Piece.KNIGHT &&
                cb.Grid[0][2].piece == Piece.BISHOP && cb.Grid[7][2].piece == Piece.BISHOP &&
                cb.Grid[0][3].piece == Piece.QUEEN  && cb.Grid[7][3].piece == Piece.QUEEN &&
                cb.Grid[0][4].piece == Piece.KING   && cb.Grid[7][4].piece == Piece.KING &&
                cb.Grid[0][5].piece == Piece.BISHOP && cb.Grid[7][5].piece == Piece.BISHOP &&
                cb.Grid[0][6].piece == Piece.KNIGHT && cb.Grid[7][6].piece == Piece.KNIGHT &&
                cb.Grid[0][7].piece == Piece.ROOK   && cb.Grid[7][7].piece == Piece.ROOK
                );

            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void Chess960HasAllPieces()
        {
            ChessBoard cb = new ChessBoard();
            cb.SetInitialPlacement(true);

            bool pass = false;
            int[] pieceCount = new int[] { 0, 0, 0, 0, 0 };
            for (int f = 0; f <= 7; f++)
            {
                pieceCount[(int)cb.Grid[0][f].piece - 1]++;
            }

            pass = (pieceCount[0] == 2 && pieceCount[1] == 2 && pieceCount[2] == 2 && pieceCount[3] == 1 && pieceCount[4] == 1);

            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void Chess960KingBetweenRooks()
        {
            ChessBoard cb = new ChessBoard();
            cb.SetInitialPlacement(true);

            bool pass = false;
            List<int> rookPoss = new List<int>();
            int kingPos = -1;
            for (int f = 0; f <= 7; f++)
            {
                if (cb.Grid[0][f].piece == Piece.ROOK) rookPoss.Add(f);
                else if (cb.Grid[0][f].piece == Piece.KING) kingPos = f;
            }

            pass = (kingPos > rookPoss[0] && kingPos < rookPoss[1]);

            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void Chess960BishopOnAlternetColors()
        {
            ChessBoard cb = new ChessBoard();
            cb.SetInitialPlacement(true);

            bool pass = false;
            int b1 = -1;
            int b2 = -1;
            for (int f = 0; f <= 7; f++)
            {
                if (cb.Grid[0][f].piece == Piece.BISHOP)
                {
                    if (b1 == -1)
                    {
                        b1 = f;
                        continue;
                    }
                    else if (b2 == -1)
                    {
                        b2 = f;
                        break;
                    }
                }
            }

            pass = (b1 % 2 != b2 % 2);

            Assert.IsTrue(pass);
        }

        [TestMethod]
        public void Chess960BlackIsMirrorOfWhite()
        {
            ChessBoard cb = new ChessBoard();
            cb.SetInitialPlacement(true);

            bool pass = (
                cb.Grid[0][0].piece == cb.Grid[7][0].piece &&
                cb.Grid[0][1].piece == cb.Grid[7][1].piece &&
                cb.Grid[0][2].piece == cb.Grid[7][2].piece &&
                cb.Grid[0][3].piece == cb.Grid[7][3].piece &&
                cb.Grid[0][4].piece == cb.Grid[7][4].piece &&
                cb.Grid[0][5].piece == cb.Grid[7][5].piece &&
                cb.Grid[0][6].piece == cb.Grid[7][6].piece &&
                cb.Grid[0][7].piece == cb.Grid[7][7].piece
                );

            Assert.IsTrue(pass);
        }
    }
}
