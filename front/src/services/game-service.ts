import axios from "axios";
import { Board } from "../models/board";
import { Direction } from "../models/direction";

export class GameService {
    private apiAddress: string = 'https://localhost:7067/api/board';

    public createBoard = async () => {
            return await axios.post<Board>(this.apiAddress, {
                width: 10,
                height: 10,
                randomCells: true
        });
    };

    public getBoardById = async (boardId: string) => {
        return await axios.get<Board>(`${this.apiAddress}/${boardId}`);
    };

    public moveState = async (boardId: string, stateCount: number, finalState: boolean, direction: Direction) => {
        return await axios.put<Board>(`${this.apiAddress}/move-state/${boardId}`, {
            boardId: boardId,
            lenght: stateCount,
            finalState: finalState,
            direction
        });
    };

    public restartBoard = async (boardId: string) => {
        return await axios.put<Board>(`${this.apiAddress}/restart/${boardId}`);
    };

    public updateBoard = async (board: Board) => {
        return await axios.put<Board>(`${this.apiAddress}/update-board/${board.id}`, board);
    };
}