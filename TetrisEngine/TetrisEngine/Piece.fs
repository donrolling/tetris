namespace TetrisEngine

open GameConstants

type Piece(shape : Shapes, color : Colors, hex : string, startingPosition : int list) = 
   member val Shape = shape
   member val Color = color
   member val Hex = hex
   member val StartingPosition = startingPosition
