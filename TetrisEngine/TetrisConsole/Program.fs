#light 
namespace TetrisConsole
open System
open TetrisEngine

module Main = 
   let gameState = GameStateFactory.GetGameState_Placeholder()
   let engine = new Engine(0.0, gameState)
   engine.Start()
   while (Console.ReadKey(true).Key <> ConsoleKey.Escape) do
      while (Console.KeyAvailable = false) do
         match Console.ReadKey(true).Key with
            | ConsoleKey.DownArrow -> engine.Move(MoveDirections.DOWN)
            | ConsoleKey.S -> engine.Move(MoveDirections.DOWN)
            | ConsoleKey.LeftArrow -> engine.Move(MoveDirections.LEFT)
            | ConsoleKey.A -> engine.Move(MoveDirections.LEFT)
            | ConsoleKey.RightArrow -> engine.Move(MoveDirections.RIGHT)
            | ConsoleKey.D -> engine.Move(MoveDirections.RIGHT)
            | ConsoleKey.K -> engine.Flip(FlipDirections.LEFT)
            | ConsoleKey.L -> engine.Flip(FlipDirections.RIGHT)
            | _ -> ()