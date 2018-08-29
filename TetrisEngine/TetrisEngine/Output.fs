namespace TetrisEngine

module Output = 
   open GameConstants
   open TetrisEngine
   open System
   
   let getOutput (i : int, x : Colors, piece : Piece, pieceOrientation : Orientations, piecePosition : int list) : string = 
      if List.exists (fun a -> a = i) piecePosition then 
         piece.Color.ToString().Substring(0, 1)
      else x.ToString().Substring(0, 1)
   
   let getPrint (i : int, x : Colors, piece : Piece, pieceOrientation : Orientations, piecePosition : int list) : string = 
      let output = getOutput (i, x, piece, pieceOrientation, piecePosition)
      match i with
      | i when i % GameConstants.WellWidth = 9 -> output + "\r\n"
      | _ -> output
   
   let getPrintHelper (i : int, gameState : GameState) : string = 
      getPrint (i, gameState.Board.Item(i), gameState.Piece, gameState.PieceOrientation, gameState.PiecePosition)
   
   let printGameState (gameState : GameState) = 
      let output = 
         [ for y in 1..GameConstants.WellHeight do
              for x in 0..GameConstants.WellWidth - 1 do
                 let i = ((GameConstants.WellHeight - y) * GameConstants.WellWidth) + x
                 yield getPrintHelper (i, gameState) ]
         |> String.concat ""
      printfn "\r\n%s\r\n" output
      let x = List.map (fun a -> a.ToString()) gameState.PiecePosition |> String.concat " "
      printfn "--------------------------\r\n%s : %i" x gameState.Board.Length