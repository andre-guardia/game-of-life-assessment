using GameOfLife.Domain.Entities;

namespace GameOfLife.UnitTests.UseCases
{
    public class BoardTests
    {
        [Fact]
        public void CreateBoardSuccess()
        {
            //arrange
            var width = 10;
            var height = 10;

            //act
            var board = Board.Create(width, height, null, true);

            //assert
            Assert.NotNull(board);
            Assert.True(board.Cells.Count == (width * height));
        }
    }
}