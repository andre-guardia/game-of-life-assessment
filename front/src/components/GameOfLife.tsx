import { useEffect, useState } from 'react';
import { Board } from '../models/board';
import axios from 'axios';

const GameOfLife = () => {
    const [currentBoard, setCurrentBoard] = useState<Board>();
    const [boardId, setBoardId] = useState<string>('');
    const [stateCount, setStateCount] = useState<number>(1);
    const apiAddress = 'https://localhost:7067/api/board';
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
            const response = await axios.post<Board>(apiAddress, {
                width: 10,
                height: 10,
                randomCells: true
            });
            if (response.status == 200) {
                setCurrentBoard(response.data);
                setBoardId(response.data.id);
            }
        } catch (err) {
            console.log(err);
            handleError(err);
        }
    };

    const getBoardById = async () => {
        if (boardId) {
            try {
                const response = await axios.get<Board>(`${apiAddress}/${boardId}`);
                if (response.status == 200) {
                    setCurrentBoard(response.data);
                }
            } catch (err) {
                console.log(err);
                handleError(err);
            }
        }
    };

    const moveNext = async (finalState: boolean) => {
        try {
            const response = await axios.put<Board>(`${apiAddress}/move-state/${boardId}`, {
                boardId,
                lenght: stateCount,
                finalState: finalState
            });
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
            const response = await axios.put<Board>(`${apiAddress}/restart/${boardId}`);
            if (response.status == 200) {
                setCurrentBoard(response.data);
            }
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

    const contents = currentBoard === undefined
        ? <p></p>
        : <>
            <table className="table table-striped" aria-labelledby="tableLabel" border={1}>
                <tbody>
                    {lines && lines.map((line: any, idx: number) =>
                        <tr key={idx}>
                            {line && line.map((cell: any, idxCell: number) => <td style={{ width: '20px', height: '20px' }} key={ idxCell } > { cell.alive ? "✅" : "" }</td>)}
                        </tr>
                    )}
                </tbody>
            </table>
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
            <>
                <input type="number" placeholder="State Count" value={stateCount} onChange={e => setStateCount(+e.target.value)}></input>
                <button onClick={e => moveNext(false)}>Next</button>
                <button onClick={e => moveNext(true)}>Final State</button>
                <button onClick={restartBoard}>Restart</button>
            </>
        )}
        
        <button onClick={createBoard}>New Game</button>
        {contents}
      </div>
    );
  };
  
  export default GameOfLife;