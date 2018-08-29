module PieceFactory

open TetrisEngine

   let getNewPiece(rnd : System.Random) = 
      let p = GameObjects.Pieces.Item(rnd.Next(0, GameObjects.NumberOfPieces))
      let absolutePosition = Position.convertToWellCoordinateViaWellZero(GameConstants.WellStartZero, p.StartingPosition)
      new Piece(p.Shape, p.Color, p.Hex, absolutePosition)