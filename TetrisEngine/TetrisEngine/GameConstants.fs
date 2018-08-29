namespace TetrisEngine

type Shapes = | I = 0 | O = 1 | T = 2 | J = 3 | L = 4 | S = 5 | Z = 6 | Invalid = -1
type Colors = | RED = 0 | MAGENTA = 1 | YELLOW = 2 | CYAN = 3 | BLUE = 4 | GREY = 5 | LIME = 6 | Empty = 7
type MoveDirections = | LEFT = 0 | RIGHT = 1 | DOWN = 2 | UP = 3 
type FlipDirections = | LEFT = 0 | RIGHT = 1 
type Orientations = | ONE = 0 | TWO = 1 | THREE = 2 | FOUR = 3 
type StopMoveReason = | NONE = -1 | EDGE = 0 | BUMP = 1 | LAND = 2

module GameConstants = 
   //the tetriminos live in an 8x8 cube the grid is 0-15 bottom left to top right just like the well
   let Tetriminos = 
      [ (Shapes.I, 
         [ [| 8; 9; 10; 11 |]
           [| 2; 6; 10; 14 |]
           [| 4; 5; 6; 7 |]
           [| 1; 5; 9; 13 |] ])
        (Shapes.O, 
         [ [| 5; 6; 9; 10 |]
           [| 5; 6; 9; 10 |]
           [| 5; 6; 9; 10 |]
           [| 5; 6; 9; 10 |] ])
        (Shapes.T, 
         [ [| 4; 5; 6; 9 |]
           [| 1; 5; 6; 9 |]
           [| 1; 4; 5; 6 |]
           [| 1; 4; 5; 9 |] ])
        (Shapes.J, 
         [ [| 4; 5; 6; 8 |]
           [| 1; 5; 9; 10 |]
           [| 2; 4; 5; 6 |]
           [| 0; 1; 5; 9 |] ])
        (Shapes.L, 
         [ [| 4; 5; 6; 10 |]
           [| 1; 2; 5; 9; |]
           [| 0; 4; 5; 6 |]
           [| 1; 5; 8; 9; |] ])
        (Shapes.S, 
         [ [| 4; 5; 9; 10 |]
           [| 2; 5; 6; 9 |]
           [| 0; 1; 5; 6 |]
           [| 1; 4; 5; 8 |] ])
        (Shapes.Z, 
         [ [| 5; 6; 8; 9 |]
           [| 1; 5; 6; 10 |]
           [| 1; 2; 4; 5 |]
           [| 0; 4; 5; 9; |] ]) ]
   
   let WellSize = (10, 22)
   let WellStartZero = 184
   let WellWidth = fst WellSize
   let WellHeight = snd WellSize
   let WellCount = fst WellSize * snd WellSize