namespace TetrisEngine

module Position = 
   open GameConstants
   
   let getRelativePosition (shape : Shapes, pieceOrientation : Orientations) : int list = 
      let positions = (snd (Tetriminos.Item(int shape)))
      (snd (Tetriminos.Item(int shape))).Item(int pieceOrientation) |> Array.toList
   
   let convertToAbsolutePosition (newRelativePosition : int) : int =
      let xDiff = newRelativePosition % 4
      let yDiff = if newRelativePosition > 0 then (int (newRelativePosition / 4)) * GameConstants.WellWidth else 0
      xDiff + yDiff
   
   let getRelativeZeroPoint(pieceAbsPosition : int list, oldOrientation : Orientations, newRelativePosition : int list, newOrientation : Orientations, shape : Shapes) : int = 
      let oldLowestAbsolutePosition = pieceAbsPosition |> List.min
      let oldRelativePosition = getRelativePosition(shape, oldOrientation) |> List.min
      let v = convertToAbsolutePosition(oldRelativePosition)
      oldLowestAbsolutePosition - v

   let convertToWellCoordinateViaWellZero(wellZero : int, newRelativePosition : int list) : int list = 
      newRelativePosition |> List.map (fun a -> wellZero + convertToAbsolutePosition(a))

   let convertToWellCoordinate(piecePosition : int list, oldOrientation : Orientations, newRelativePosition : int list, newPieceOrientation : Orientations, shape : Shapes) : int list = 
      let wellZero = getRelativeZeroPoint(piecePosition, oldOrientation, newRelativePosition, newPieceOrientation, shape)      
      convertToWellCoordinateViaWellZero(wellZero, newRelativePosition);
   
   let getDestination(piecePosition : int, direction : MoveDirections) : int = 
      match direction with
      | MoveDirections.LEFT -> piecePosition - 1
      | MoveDirections.RIGHT -> piecePosition + 1
      | MoveDirections.DOWN -> piecePosition - GameConstants.WellWidth
      | MoveDirections.UP -> piecePosition + GameConstants.WellWidth
      | _ -> piecePosition

   let getStopReason(piecePosition : int, pieceDestination : int, direction : MoveDirections, board : Colors list) : StopMoveReason = 
      let pieceModulus = piecePosition % GameConstants.WellWidth
      if pieceModulus = 0 && direction = MoveDirections.LEFT then 
         StopMoveReason.EDGE
      else if pieceModulus = GameConstants.WellWidth - 1 && direction = MoveDirections.RIGHT then 
         StopMoveReason.EDGE
      else 
         if pieceDestination < 0 then StopMoveReason.LAND
         else if board.Item(pieceDestination) = Colors.Empty then StopMoveReason.NONE
         else if direction = MoveDirections.DOWN then StopMoveReason.LAND
         else StopMoveReason.BUMP

   let getStopReason_ForFlip(piecePosition : int, pieceDestination : int, board : Colors list) : StopMoveReason = 
      if pieceDestination > GameConstants.WellCount then 
         StopMoveReason.EDGE
      else
         let pieceModulus = piecePosition % GameConstants.WellWidth
         if pieceModulus = 0 && (pieceDestination < piecePosition) then 
            StopMoveReason.EDGE
         else 
            if pieceDestination < 0 then StopMoveReason.LAND
            else if board.Item(pieceDestination) = Colors.Empty then StopMoveReason.NONE
            else StopMoveReason.BUMP
   
   let getPosition (piecePosition : int list, direction : MoveDirections, board : Colors list) : int list = 
      piecePosition |> List.map (fun p -> getDestination(p, direction))
