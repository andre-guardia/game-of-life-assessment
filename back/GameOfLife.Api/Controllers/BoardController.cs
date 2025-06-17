using GameOfLife.Application.UseCases.CreateBoard;
using GameOfLife.Application.UseCases.GetBoard;
using GameOfLife.Application.UseCases.MoveBoardState;
using GameOfLife.Application.UseCases.RestartBoard;
using GameOfLife.Application.UseCases.Shared.Outputs;
using GameOfLife.Application.UseCases.UpdateBoard;
using GameOfLife.Core.Extensions;
using GameOfLife.Core.UseCases.Outputs;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GameOfLife.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class BoardController : ControllerBase
    {
        private readonly ILogger<BoardController> _logger;
        private readonly IMediator _mediator;

        public BoardController(ILogger<BoardController> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BoardOutput))]
        [ProducesResponseType(StatusCodes.Status204NoContent, Type = typeof(Output))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Output))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> GetBoard([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Starting GetBoard: {id}");
            var input = new GetBoardInput(id);
            var output = await _mediator.Send(input, cancellationToken);
            if (output.IsValid)
            {
                var result = output.GetResult<GetBoardOutput>();
                if (result == null)
                    return NoContent();

                return Ok(result.Data);
            }

            return BadRequest(output);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BoardOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Output))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> CreateBoard([FromBody] CreateBoardInput input, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Starting CreateBoard: {input.ToJson()}");
            var output = await _mediator.Send(input, cancellationToken);
            if (output.IsValid)
            {
                var result = output.GetResult<CreateBoardOutput>();
                if (result == null)
                    return NoContent();

                return Ok(result.Data);
            }

            return BadRequest(output);
        }

        [HttpPut("move-state/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BoardOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Output))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> MoveState([FromRoute] Guid id, [FromBody] MoveBoardStateInput input, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Starting MoveState: {input.ToJson()}");
            var output = await _mediator.Send(input, cancellationToken);
            if (output.IsValid)
            {
                var result = output.GetResult<MoveBoardStateOutput>();
                if (result == null)
                    return BadRequest();

                return Ok(result.Data);
            }

            return BadRequest(output);
        }

        [HttpPut("restart/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BoardOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Output))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> RestartBoard([FromRoute] Guid id, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Starting RestartBoard: {id}");
            var input = new RestartBoardInput(id);
            var output = await _mediator.Send(input, cancellationToken);
            if (output.IsValid)
            {
                var result = output.GetResult<RestartBoardOutput>();
                if (result == null)
                    return BadRequest();

                return Ok(result.Data);
            }

            return BadRequest(output);
        }

        [HttpPut("update-board/{id}")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(BoardOutput))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(Output))]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]

        public async Task<IActionResult> UpdateBoard([FromRoute] Guid id, [FromBody] UpdateBoardInput input, CancellationToken cancellationToken = default)
        {
            _logger.LogInformation($"Starting UpdateBoard: {input.ToJson()}");
            input.BoardId = id;
            var output = await _mediator.Send(input, cancellationToken);
            if (output.IsValid)
                return NoContent();

            return BadRequest(output);
        }
    }
}
