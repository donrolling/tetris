namespace TetrisEngine

open System
open System.Threading
open System.Text
open GameConstants

type Engine(timerInterval : float, gameState : GameState) as self = 
   let rnd : System.Random = new System.Random(Guid.NewGuid().GetHashCode())
   let basicHandler _ = self.tick()
   let tickMoveMax : int = 3;
   let mutable tickMoveCount : int = 0;
   let Timer = 
      if timerInterval > 0.0 then 
         Utilities.SetupTimer(timerInterval, basicHandler) 
      else 
         Utilities.EmptyTimer()
   let mutable GameState = 
      if gameState.Piece.Shape = Shapes.Invalid then 
         if gameState.Board = [] then 
            GameStateFactory.GetGameState_GivenStartingPiece(0, rnd, gameState.Piece) 
         else
            GameStateFactory.GetGameState_GivenLevelAndPieceAndBoard(0, rnd, gameState.Piece, gameState.Board) 
      else 
         if gameState.Board = [] then 
            GameStateFactory.GetGameState_GivenStartingPiece(0, rnd, gameState.Piece) 
         else
            gameState
   
   member this.Start() = 
      self.tick()
         
   member this.Move(direction : MoveDirections) = 
      if tickMoveCount < tickMoveMax then
         GameState <- Board.go (rnd, direction, GameState)
      tickMoveCount <- tickMoveCount + 1
      
   member this.Flip(direction : FlipDirections) = 
      if tickMoveCount < tickMoveMax then
         GameState <- Board.flip (rnd, direction, GameState)
      tickMoveCount <- tickMoveCount + 1
      
   member this.tick() = 
//      if GameState.GameOver = false then 
      GameState <- Board.go (rnd, MoveDirections.DOWN, GameState)
      tickMoveCount <- 0
      
   member this.ViewGameState() = GameState