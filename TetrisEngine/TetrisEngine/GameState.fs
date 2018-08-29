namespace TetrisEngine

open GameConstants

type GameState(piece : Piece, nextPiece : Piece, position : int list, orientation : Orientations, board : Colors list) = 
   member val Piece = piece
   member val NextPiece = nextPiece
   member val PiecePosition = position
   member val PieceOrientation = orientation
   member val Board = board
//   member val GameOver = 
//      let stuff = 
//         [ for x in 0..GameConstants.WellCount - 1 ->
//              match x with
//              | x when board.Item(x) <> Colors.Empty -> x
//              | _ -> -1 ]
//      Set.intersect (Set.ofList position) (Set.ofList stuff) |> Set.isEmpty = false
