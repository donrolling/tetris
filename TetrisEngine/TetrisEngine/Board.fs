namespace TetrisEngine

module Board = 
   let private addPieceToBoard(newPosition : int list, gameState : GameState, board : Colors list) : Colors list = 
      [ for x in 0..GameConstants.WellCount - 1 do
           let c = 
              match x with
              | x when List.exists(fun z -> z = x) gameState.PiecePosition -> gameState.Piece.Color
              | _ -> board.Item(x)
           yield c ]
      
   let getGameState_NewBoard(gameState : GameState, board : Colors list) : GameState = 
      new GameState(gameState.Piece, gameState.NextPiece, gameState.PiecePosition, gameState.PieceOrientation, board)

   let getGameState_ForMovements(newPosition : int list, gameState : GameState, board : Colors list) : GameState = 
      new GameState(gameState.Piece, gameState.NextPiece, newPosition, gameState.PieceOrientation, board)

   let getGameState_ForFlip(newPosition : int list, newOrientation : Orientations, gameState : GameState) : GameState = 
      new GameState(gameState.Piece, gameState.NextPiece, newPosition, newOrientation, gameState.Board)

   let getGameState_ForLandings(rnd, newPosition : int list, gameState : GameState, board : Colors list) : GameState = 
      let newPiece = PieceFactory.getNewPiece(rnd)
      let newBoard = addPieceToBoard(newPosition, gameState, board)      
      new GameState(gameState.NextPiece, newPiece, gameState.NextPiece.StartingPosition, Orientations.ONE, newBoard)

   let getClearRows(board : Colors list) : int list = 
      let list = [ 0..10..GameConstants.WellCount - 1 ]
      List.choose(fun y -> 
         match y with
         | y when List.exists(fun x -> 
            board.Item(x) = Colors.Empty)
            [ y..(y + (GameConstants.WellWidth - 1)) ] -> None
         | _ -> Some y) list
   
   let clearRow(board : Colors list, row : int) : Colors list = 
      let topRow = (GameConstants.WellHeight - 1) * GameConstants.WellWidth
      let list = [ 0..GameConstants.WellCount - 1 ]
      let newBoard =
         [ for x in list -> 
              match x with
              | x when x >= topRow -> Colors.Empty
              | x when x < row -> board.Item(x)
              | _ -> board.Item(x + GameConstants.WellWidth) ]
      newBoard
   
   let rec getClearBoard(board : Colors list, clearRows : int list, index : int) : Colors list = 
      let newClearRows = if index = 0 then clearRows else clearRows |> List.map (fun x -> x - index)
      match newClearRows with
      | [] -> board
      | x :: xs -> getClearBoard(clearRow(board, x), xs, index + GameConstants.WellWidth)
   
   let getClearedBoard(board : Colors list) : Colors list * int list = 
      let clearRows = getClearRows(board)
      if clearRows = [] then
         board, clearRows
      else
         let newBoard = getClearBoard(board, clearRows, 0)
         newBoard, clearRows

   let go(rnd, direction : MoveDirections, gameState : GameState) = 
      let newPosition = Position.getPosition(gameState.PiecePosition, direction, gameState.Board)
      let stopReasons = [0..3] |> List.map(fun x -> Position.getStopReason(gameState.PiecePosition.Item(x), newPosition.Item(x), direction, gameState.Board))
      let hasLanded = List.exists(fun x -> x = StopMoveReason.LAND) stopReasons
      let hitEdge = List.exists(fun x -> x = StopMoveReason.EDGE || x = StopMoveReason.BUMP) stopReasons
      let clearedBoard = getClearedBoard(gameState.Board)
      if hasLanded then getGameState_ForLandings(rnd, gameState.PiecePosition, gameState, fst clearedBoard)
      else if hitEdge then getGameState_NewBoard(gameState, fst clearedBoard)
      else getGameState_ForMovements(newPosition, gameState, fst clearedBoard)
   
   let flip(rnd, direction : FlipDirections, gameState : GameState) = 
      let newPieceOrientation = Orientation.getOrientation(gameState.PieceOrientation, direction)
      let newRelativePosition = Position.getRelativePosition(gameState.Piece.Shape, newPieceOrientation)
      let newPosition = Position.convertToWellCoordinate(gameState.PiecePosition, gameState.PieceOrientation, newRelativePosition, newPieceOrientation, gameState.Piece.Shape)
      let stopReasons = [0..3] |> List.map(fun x -> 
         Position.getStopReason_ForFlip(gameState.PiecePosition.Item(x), newPosition.Item(x), gameState.Board))
      let hasLanded = List.exists(fun x -> x = StopMoveReason.LAND) stopReasons
      let hitEdge = List.exists(fun x -> x = StopMoveReason.EDGE || x = StopMoveReason.BUMP) stopReasons
      if hitEdge || hasLanded then 
         gameState
      else getGameState_ForFlip(newPosition, newPieceOrientation, gameState)
