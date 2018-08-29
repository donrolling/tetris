namespace TetrisEngine

open GameConstants

module GameObjects = 
   let Pieces = 
      [ 
      new Piece(Shapes.I, Colors.RED, "FF0000", 
               (snd (Tetriminos.Item(int Shapes.I))).Item(int Orientations.ONE) |> Array.toList)
        
      new Piece(Shapes.O, Colors.MAGENTA, "FF00FF", 
               (snd (Tetriminos.Item(int Shapes.O))).Item(int Orientations.ONE) |> Array.toList)
        
      new Piece(Shapes.T, Colors.YELLOW, "FFFF00", 
               (snd (Tetriminos.Item(int Shapes.T))).Item(int Orientations.ONE) |> Array.toList)
        
      new Piece(Shapes.J, Colors.CYAN, "00FFFF", 
               (snd (Tetriminos.Item(int Shapes.J))).Item(int Orientations.ONE) |> Array.toList)
        
      new Piece(Shapes.L, Colors.BLUE, "0000FF", 
               (snd (Tetriminos.Item(int Shapes.L))).Item(int Orientations.ONE) |> Array.toList)
        
      new Piece(Shapes.S, Colors.GREY, "C0C0C0", 
               (snd (Tetriminos.Item(int Shapes.S))).Item(int Orientations.ONE) |> Array.toList)
        
      new Piece(Shapes.Z, Colors.LIME, "80FF00", 
               (snd (Tetriminos.Item(int Shapes.Z))).Item(int Orientations.ONE) |> Array.toList)

      new Piece(Shapes.Invalid, Colors.Empty, "000000", [ 0; 0; 0; 0 ])
      ]
   
   let NumberOfPieces = Tetriminos.Length