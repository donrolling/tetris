namespace TetrisEngine

module Orientation = 
   open GameConstants
   
   let orientationsOffset (pieceOrientation : Orientations, offset : int) : Orientations = 
      let num = (int pieceOrientation) + offset
      
      let newNum = 
         match num with
         | num when num < int Orientations.ONE -> int Orientations.FOUR
         | num when num > int Orientations.FOUR -> int Orientations.ONE
         | _ -> num
      enum<Orientations> (newNum)
   
   let getOrientation (pieceOrientation : Orientations, direction : FlipDirections) : Orientations = 
      match direction with
      | FlipDirections.LEFT -> orientationsOffset (pieceOrientation, -1)
      | FlipDirections.RIGHT -> orientationsOffset (pieceOrientation, 1)
      | _ -> pieceOrientation
