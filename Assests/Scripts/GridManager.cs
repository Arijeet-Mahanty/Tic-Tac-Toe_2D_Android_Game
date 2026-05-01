using Unity.VisualScripting;
using UnityEngine;

public class GridManager : MonoBehaviour
{
   [SerializeField] private GridSquareManager[] grid;

    public void resetGrid()
    {
        //foreach(GridSquareManager square in grid)
        //{
        //    square.setSquare(GridSquareState.empty);
        //}
        for(int i=0; i < grid.Length; i++)
        {
            grid[i].setSquare(GridSquareState.empty,Color.white);
            grid[i].setSquareID(i);
        }
    }

    public GridSquareState SpecificSquareState(int squareId)
    {
        return grid[squareId].getSquareState();
    }

    public void setSpecificSquare(GridSquareState gridSquareState,int square,Color color)
    {
        grid[square].setSquare(gridSquareState, color);
    }

    public bool checkIfGridisFull()
    {
        foreach(GridSquareManager square in grid)
        {
            if(square.getSquareState() == GridSquareState.empty)
            {
                return false;
            }
        }
        return true;
    }

    public GridSquareState CheckForWin(int gridSquare1,int gridSquare2,int gridSquare3)
    {
        GridSquareState state1 = grid[gridSquare1].getSquareState();
        GridSquareState state2 = grid[gridSquare2].getSquareState();
        GridSquareState state3 = grid[gridSquare3].getSquareState();

        if (state1 != GridSquareState.empty)
        {
            if(state1 == state2 && state1 == state3)
            {
                return state1;
            }
            else
            {
                return GridSquareState.empty;
            }
        }
        return GridSquareState.empty;
    }
    
}
