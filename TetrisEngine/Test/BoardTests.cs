using Microsoft.FSharp.Collections;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;
using TetrisEngine;

namespace Test {
	[TestClass]
	public class BoardTests {
		//proves that the math for clearing the Well is accurate
		[TestMethod]
		public void ClearBoardTest() {
			var colors = new List<Colors>();
			for (int i = 0; i < GameConstants.WellCount; i++) {
				if (i < 10) {
					colors.Add(Colors.BLUE);
				} else if (i >= 30 && i < 40) {
					colors.Add(Colors.BLUE);
				} else {
					colors.Add(Colors.Empty);
				}
			}
			var fList = ListModule.OfSeq(colors);
			var clearRows = Board.getClearRows(fList);
			Assert.AreEqual(0, clearRows[0]);
			Assert.AreEqual(30, clearRows[1]);
			var newBoard = Board.getClearBoard(fList, clearRows, 0).ToList();
			Assert.IsFalse(newBoard.Any(a => a != Colors.Empty));
		}

		//proves that the application logic for clearing the Well is accurate
		[TestMethod]
		public void ClearBoardTest2() {
			var colors = new List<Colors>();
			for (int i = 0; i < GameConstants.WellCount; i++) {
				if (i < 10) {
					colors.Add(Colors.BLUE);
				} else if (i >= 30 && i < 40) {
					colors.Add(Colors.BLUE);
				} else {
					colors.Add(Colors.Empty);
				}
			}
			var fList = ListModule.OfSeq(colors);
			var gameState = GameStateFactory.GetGameState_GivenBoard(new System.Random(), fList);
			var engine = new Engine(0, gameState);
			engine.tick();
			var gs = engine.ViewGameState();
			var clearRows = Board.getClearRows(gs.Board);
			Assert.IsTrue(!clearRows.Any());
		}

		[TestMethod]
		public void FlipTest_I() {
			var gameState = GameStateFactory.GetGameState_GivenStartingPieceType(0, Shapes.I);
			var engine = new Engine(0, gameState);
			flipPiece(engine);
		}

		[TestMethod]
		public void FlipTest_J() {
			var gameState = GameStateFactory.GetGameState_GivenStartingPieceType(0, Shapes.J);
			var engine = new Engine(0, gameState);
			flipPiece(engine);
		}

		[TestMethod]
		public void FlipTest_L() {
			var gameState = GameStateFactory.GetGameState_GivenStartingPieceType(0, Shapes.L);
			var engine = new Engine(0, gameState);
			flipPiece(engine);
		}

		[TestMethod]
		public void FlipTest_O() {
			var gameState = GameStateFactory.GetGameState_GivenStartingPieceType(0, Shapes.O);
			var engine = new Engine(0, gameState);
			flipPiece(engine);
		}

		[TestMethod]
		public void FlipTest_S() {
			var gameState = GameStateFactory.GetGameState_GivenStartingPieceType(0, Shapes.S);
			var engine = new Engine(0, gameState);
			flipPiece(engine);
		}

		[TestMethod]
		public void FlipTest_T() {
			var gameState = GameStateFactory.GetGameState_GivenStartingPieceType(0, Shapes.T);
			var engine = new Engine(0, gameState);
			flipPiece(engine);
		}

		[TestMethod]
		public void FlipTest_Z() {
			var gameState = GameStateFactory.GetGameState_GivenStartingPieceType(0, Shapes.Z);
			var engine = new Engine(0, gameState);
			flipPiece(engine);
		}

		private void flipPiece(Engine engine) {
			Assert.AreEqual(GameConstants.WellCount, engine.ViewGameState().Board.Length);
			var firstPosition = engine.ViewGameState().PiecePosition.ToArray().ToList();
			assertPositionsEquality(engine.ViewGameState(), firstPosition, Orientations.ONE);

			engine.Flip(FlipDirections.RIGHT);
			var secondPosition = engine.ViewGameState().PiecePosition.ToArray().ToList();
			assertPositionsEquality(engine.ViewGameState(), secondPosition, Orientations.TWO);

			engine.Flip(FlipDirections.RIGHT);
			var thirdPosition = engine.ViewGameState().PiecePosition.ToArray().ToList();
			assertPositionsEquality(engine.ViewGameState(), thirdPosition, Orientations.THREE);

			engine.Flip(FlipDirections.RIGHT);
			var fourthPosition = engine.ViewGameState().PiecePosition.ToArray().ToList();
			assertPositionsEquality(engine.ViewGameState(), fourthPosition, Orientations.FOUR);

			engine.Flip(FlipDirections.RIGHT);
			var fifthPosition = engine.ViewGameState().PiecePosition.ToArray().ToList();
			assertListSameness(firstPosition, fifthPosition);//should be back to original state
		}

		private void assertListSameness(List<int> list1, List<int> list2) {
			Assert.IsTrue(list2.Count() == list1.Count);
			var diff = list1.ToList().Intersect(list2);
			Assert.IsTrue(diff.Count() == list1.Count);
		}

		private void assertPositionsEquality(GameState gameState, List<int> position, Orientations orientations) {
			var orientation = (int)orientations;
			var shape = (int)gameState.Piece.Shape;
			var expectedPosition = GameConstants.Tetriminos[shape].Item2[orientation].ToList();
			var expectedAbsolutePosition = Position.convertToWellCoordinateViaWellZero(GameConstants.WellStartZero, ListModule.OfSeq(expectedPosition));
			assertListSameness(position, expectedAbsolutePosition.ToList());
		}
	}
}
