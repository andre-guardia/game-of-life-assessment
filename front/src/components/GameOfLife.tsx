import { useState } from 'react';
import { Board } from '../models/board';
import { Cell } from '../models/cell';
import { GameService } from '../services/game-service';
import { Direction } from '../models/direction';

const GameOfLife = () => {
    const [boards, setBoards] = useState<Board[]>([]);
    const [currentBoard, setCurrentBoard] = useState<Board>();
    const [stateCount, setStateCount] = useState<number>(1);
    const gameService = new GameService();
    const lines: any = [];

    if (currentBoard) {
        lines.splice(0, lines.length);
        let current: number = 0;
        while (currentBoard.width > current) {
            lines.push(currentBoard.cells.slice(currentBoard.width * current, currentBoard.width * (current + 1)));
            ++current;
        }
    }

    const createBoard = async () => {
        try {
            const response = await gameService.createBoard();
            if (response.status == 200) {
                setCurrentBoard(response.data);
                boards?.push(response.data);
                setBoards(boards);
            }
        } catch (err) {
            console.log(err);
            handleError(err);
        }
    };

    const getBoardById = async (boardId: string) => {
        try {
            const response = await gameService.getBoardById(boardId);
            if (response.status == 200) {
                setCurrentBoard(response.data);
            }
        } catch (err) {
            console.log(err);
            handleError(err);
        }
    };

    const moveState = async (finalState: boolean, direction: Direction = Direction.Forward) => {
        try {
            const response = await gameService.moveState(currentBoard!.id, stateCount, finalState, direction);
            if (response.status == 200) {
                setCurrentBoard(response.data);
            }
        } catch (err) {
            console.log(err);
            handleError(err);
        }
    };

    const restartBoard = async () => {
        try {
            const response = await gameService.restartBoard(currentBoard!.id);
            if (response.status == 200) {
                setCurrentBoard(response.data);
            }
        } catch (err) {
            console.log(err);
            handleError(err);
        }
    };

    const updateBoard = async () => {
        try {
            await gameService.updateBoard(currentBoard!);
        } catch (err) {
            console.log(err);
            handleError(err);
        }
    };

    const handleError = (err: any) => {
        var messages = err.response.data.errorMessages;
        if (messages) {
            alert(messages[0]);
        }
    };
    
    const handleCellClick = async (activeCell: Cell) => {
        if (currentBoard) {
            const newBoard: Board = {...currentBoard,
                cells: currentBoard!.cells.map(item => {
                    if (item.x === activeCell.x && item.y === activeCell.y) {
                        item.alive = !item.alive;
                    }

                    return item;
                })
            };
            setCurrentBoard(newBoard);
            await updateBoard();
        }
    };

    const onChangeBoard = async (event: any) => {
        await getBoardById(event.target.value);
    };

    const contents = currentBoard === undefined
        ? <p></p>
        : <>
            <table className="table table-striped" aria-labelledby="tableLabel" border={1}>
                <tbody>
                    {lines && lines.map((line: any, idx: number) =>
                        <tr key={idx}>
                            {line && line.map((cell: Cell, idxCell: number) => 
                            <td key={ idxCell }
                                style={{ width: '20px', height: '20px' }}
                                onClick={() => handleCellClick(cell)}>
                                { cell.alive ? "✅" : "" }
                            </td>
                            )}
                        </tr>
                    )}
                </tbody>
            </table>
            <p>
                <span>GameId: {currentBoard.id}</span>
            </p>
            <p>
                <span>States: {currentBoard.stateCount}</span>
            </p>
            <p>
                <span>Crashed: {currentBoard.isCrashed ? 'Yes' : 'No'}</span>
            </p>
          </>;
    
    return (
      <div>
        <h1>Game Of Life</h1>
        {currentBoard && (
            <fieldset>
                <div>
                    <label>GameId: </label>
                    <select onChange={onChangeBoard} value={currentBoard.id}>
                        {boards && (
                            boards.map(b => (
                                <option key={b.id} value={b.id}>{b.id}</option>
                            ))
                        )}
                    </select>
                </div>
                <div>
                    <label>States: </label>
                    <input type="number" placeholder="State Count" value={stateCount} onChange={e => setStateCount(+e.target.value)}></input>
                    <button onClick={e => moveState(false)}>Next</button>
                    <button onClick={e => moveState(false, Direction.Back)}>Back</button>
                </div>
                <div>
                    <button onClick={e => moveState(true)}>Final State</button>
                    <button onClick={restartBoard}>Restart</button>
                </div>
            </fieldset>
        )}
        <button onClick={createBoard}>New Game</button>
        {contents}
      </div>
    );
  };
  
  export default GameOfLife;