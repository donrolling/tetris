namespace TetrisEngine

module GameStateFactory = 
   let private getNewBoardByLevel(level : int) = //todo: make level do something
      [ for i in 0..GameConstants.WellCount - 1 -> Colors.Empty ]

   let GetGameState_Placeholder() : GameState =
      let emptyList = [ 0; 0; 0; 0 ] 
      let piece = new Piece(Shapes.Invalid, Colors.Empty, "", emptyList);
      let board = []//empty board will signify to the engine to create the proper board
      let position = Position.convertToWellCoordinateViaWellZero(GameConstants.WellStartZero, piece.StartingPosition)
      new GameState(piece, piece, position, Orientations.ONE, board)

   let GetGameState_GivenBoard(rnd : System.Random, board : Colors list) : GameState =
      let piece = PieceFactory.getNewPiece(rnd)
      let nextPiece = PieceFactory.getNewPiece(rnd)
      new GameState(piece, nextPiece, piece.StartingPosition, Orientations.ONE, board)
         
   let GetGameState_GivenLevel(level : int, rnd : System.Random) = 
      let piece = PieceFactory.getNewPiece(rnd)
      let nextPiece = PieceFactory.getNewPiece(rnd)
      let newBoard = getNewBoardByLevel(level)
      new GameState(piece, nextPiece, piece.StartingPosition, Orientations.ONE, newBoard)

   let GetGameState_GivenLevelAndPiece(level : int, rnd : System.Random, initialPiece : Piece) = 
      let piece = if initialPiece.Shape = Shapes.Invalid then PieceFactory.getNewPiece(rnd) else initialPiece
      let nextPiece = PieceFactory.getNewPiece(rnd)
      let newBoard = getNewBoardByLevel(level)
      new GameState(piece, nextPiece, piece.StartingPosition, Orientations.ONE, newBoard)

   let GetGameState_GivenStartingPiece(level : int, rnd : System.Random, piece : Piece) : GameState = 
      let newBoard = getNewBoardByLevel(0)
      let nextPiece = PieceFactory.getNewPiece(rnd)
      let position = Position.convertToWellCoordinateViaWellZero(GameConstants.WellStartZero, piece.StartingPosition)
      new GameState(piece, nextPiece, position, Orientations.ONE, newBoard)

   let GetGameState_GivenStartingPieceType(level : int, shape : Shapes) : GameState = 
      let piece = GameObjects.Pieces.Item(int shape)
      let newBoard = getNewBoardByLevel(0)
      let position = Position.convertToWellCoordinateViaWellZero(GameConstants.WellStartZero, piece.StartingPosition)
      new GameState(piece, piece, position, Orientations.ONE, newBoard)
      
   let GetGameState_GivenLevelAndPieceAndBoard(level : int, rnd : System.Random, initialPiece : Piece, board : Colors list) = 
      let piece = if initialPiece.Shape = Shapes.Invalid then PieceFactory.getNewPiece(rnd) else initialPiece
      let nextPiece = PieceFactory.getNewPiece(rnd)
      let newBoard = if board = [] then getNewBoardByLevel(level) else board
      let position = Position.convertToWellCoordinateViaWellZero(GameConstants.WellStartZero, piece.StartingPosition)
      new GameState(piece, nextPiece, position, Orientations.ONE, newBoard)   
