using System;
using System.Collections.Generic;
using System.Linq;
using TetrisEngine;
using UnityEngine;

public class BlockController : MonoBehaviour {
	public Engine Engine { get; private set; }

	private int _blockSize = 20;
	public int BlockSize {
		get { return _blockSize; }
		set { _blockSize = value; }
	}

	private int _initialXPosition = 25;
	public int InitialXPosition {
		get { return _initialXPosition; }
		set { _initialXPosition = value; }
	}

	private int _initialYPosition = -400;
	public int InitialYPosition {
		get { return _initialYPosition; }
		set { _initialYPosition = value; }
	}

	private Color _stageColor = Color.black;
	public Color StageColor {
		get { return _stageColor; }
		set { _stageColor = value; }
	}

	private int _tickSpeed = 300;
	public int TickSpeed {
		get { return _tickSpeed; }
		set { _tickSpeed = value; }
	}

	void Start() {
		var gameState = GameStateFactory.GetGameState_GivenLevel(0, new System.Random());
		this.Engine = new Engine(this.TickSpeed, gameState);
	}

	void Update() {
		if (Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S))
			this.Engine.Move(MoveDirections.DOWN);
		if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A))
			this.Engine.Move(MoveDirections.LEFT);
		if (Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D))
			this.Engine.Move(MoveDirections.RIGHT);
		if (Input.GetKey(KeyCode.K))
			this.Engine.Flip(FlipDirections.LEFT);
		if (Input.GetKey(KeyCode.L))
			this.Engine.Flip(FlipDirections.RIGHT);
	}

	void OnGUI() {
		var gameState = this.Engine.ViewGameState();
		this.drawTheWell(gameState);
	}

	private void drawTheWell(GameState gameState) {
		for (int i = 0; i < GameConstants.WellCount; i++) {
			drawIt(i, gameState.Board[i], gameState.Piece, gameState.PieceOrientation, gameState.PiecePosition.ToList());
		}
	}

	private void drawIt(int i, Colors color, Piece piece, Orientations pieceOrientation, List<int> piecePosition) {
		var position = getPosition(i);
		var areWeDrawingThePiece = piecePosition.Contains(i);
		if (!areWeDrawingThePiece && color == Colors.Empty) {
			return;
		}
		var hcolor = areWeDrawingThePiece ? hexToColor(piece.Hex) : getColor(color);
		var rectangle = new Rect(position.Item1, position.Item2, this.BlockSize, this.BlockSize);
		this.drawRectangle(rectangle, hcolor);
	}

	private Color getColor(Colors color) {
		switch (color) {
			case Colors.RED:
				return Color.red;
			case Colors.MAGENTA:
				return Color.magenta;
			case Colors.YELLOW:
				return Color.yellow;
			case Colors.CYAN:
				return Color.cyan;
			case Colors.BLUE:
				return Color.blue;
			case Colors.GREY:
				return Color.grey;
			case Colors.LIME:
				return Color.green;
			case Colors.Empty:
				return Color.white;
			default:
				break;
		}
		return Color.white;
	}

	private string colorToHex(Color32 color) {
		string hex = color.r.ToString("X2") + color.g.ToString("X2") + color.b.ToString("X2");
		return hex;
	}

	private Color hexToColor(string hex) {
		byte r = byte.Parse(hex.Substring(0, 2), System.Globalization.NumberStyles.HexNumber);
		byte g = byte.Parse(hex.Substring(2, 2), System.Globalization.NumberStyles.HexNumber);
		byte b = byte.Parse(hex.Substring(4, 2), System.Globalization.NumberStyles.HexNumber);
		return new Color32(r, g, b, 255);
	}

	private Tuple<int, int> getPosition(int i) {
		var x = this.InitialXPosition + ((i % GameConstants.WellWidth) * this.BlockSize);
		var y = this.InitialYPosition + ((i / GameConstants.WellWidth) * this.BlockSize);
		return Tuple.Create<int, int>(x, (y * -1));
	}

	private void drawRectangle(Rect position, Color color) {
		var texture = new Texture2D(BlockSize, BlockSize);
		for (int i = 0; i < BlockSize; i++) {
			for (int j = 0; j < BlockSize; j++) {
				texture.SetPixel(i, j, color);
			}
		}
		texture.Apply();
		GUI.DrawTexture(position, texture);
	}
}
