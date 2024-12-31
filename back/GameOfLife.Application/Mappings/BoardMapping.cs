using AutoMapper;
using GameOfLife.Application.UseCases.CreateBoard;
using GameOfLife.Application.UseCases.Shared.Inputs;
using GameOfLife.Application.UseCases.Shared.Outputs;
using GameOfLife.Domain.Entities;

namespace GameOfLife.Application.Mappings
{
    public class BoardMapping : Profile
    {
        public BoardMapping()
        {
            InputMappings();
            OutputMappings();
        }

        private void InputMappings()
        {
            CreateMap<CreateBoardInput, Board>();
            CreateMap<CellInput, Cell>();
        }

        private void OutputMappings()
        {
            CreateMap<Board, BoardOutput>();
            CreateMap<Cell, CellOutput>();
        }
    }
}
