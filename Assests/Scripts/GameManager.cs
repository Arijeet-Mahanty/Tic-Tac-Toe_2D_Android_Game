using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public static GameManager instance;

    private Turn currentTurn;
    [SerializeField] private GridManager gridManager;
    private GridSquareState playerState;
    private GridSquareState enemyState;

    private GameResults currentGameState;

    [SerializeField]private TextMeshProUGUI playerCharText;
    [SerializeField]private TextMeshProUGUI enemyCharText;
    [SerializeField] private TextMeshProUGUI playerNumber;
    [SerializeField] private TextMeshProUGUI playerCharacter;

    [SerializeField] private GameObject gameResultUI;
    [SerializeField] private GameObject currentUI;

    [SerializeField] private TextMeshProUGUI gameResultText;

    [SerializeField] private Color playerColor;
    [SerializeField] private Color enemyColor;

    [SerializeField] private WinLineManager winLineManager;


    private bool waitingInput = false;
    private void Awake()
    {
        if(instance == null) { instance = this; }
        else { Debug.Log("More than one game managers exists in this scene"); }
            StartNewGame();
    }

    // Restart button will use this function.
    public void RestartButtonClicked() { StartNewGame(); }


    private void StartNewGame()
    {
        currentGameState = GameResults.ongoing;
        
        //Reset the grid while starting a new game.
        gridManager.resetGrid();

        int firstTurn = Random.Range(0, 2);

        //Converts number into turn.
        currentTurn = (Turn)firstTurn;

        // Assigning the X and O to player and enemy.
        if(firstTurn == 0)
        {
            playerState = GridSquareState.x;
            enemyState = GridSquareState.o;
        }
        else 
        {
            enemyState = GridSquareState.x;
            playerState = GridSquareState.o;
        }

        // Setting Character UI

        playerCharText.text = playerState.ToString();
        enemyCharText.text = enemyState.ToString();

        gameResultUI.SetActive(false);
        currentUI.SetActive(true);

        winLineManager.setWinLine(WinLine.none);

        currentTurnUI();
        waitingInput = true;

    }

    private void TurnProcess(Turn turn,int selectedSquare)
    {
        waitingInput = false; 
        GridSquareState state = GridSquareState.empty;
        Color turncolor = Color.white;
        if (turn == Turn.playerTurn) 
        { 
            state = playerState;
            turncolor = playerColor;
        }
        else 
        { 
            state = enemyState;
            turncolor = enemyColor;
        }

        gridManager.setSpecificSquare(state, selectedSquare,turncolor);

        bool gameFinished = CheckIfGameEnds();
        if (!gameFinished)
        {
            ChangeTurn();
            waitingInput = true;
        }
        else
        {
            gameResultUI.SetActive(true);
            currentUI.SetActive(false);
        }
    }

    public void ChangeTurn()
    {
        if(currentTurn == Turn.playerTurn)
        {
            currentTurn = Turn.enemyTurn;
        }
        else
        {
            currentTurn = Turn.playerTurn;   
        }
        currentTurnUI();
    }

    private void currentTurnUI()
    { 
        if(currentTurn == Turn.playerTurn)
        {
            playerNumber.text = "Player 1";
            playerCharacter.text = playerState.ToString();
        }
        else
        {
            playerNumber.text = "Player 2";
            playerCharacter.text = enemyState.ToString();
        }
    }


    public void GridSquareClicked(int clickedSquare)
    {
        if (waitingInput == false)
        {
            return;
        }
        if(gridManager.SpecificSquareState(clickedSquare)!=GridSquareState.empty)
        {
            return;
        }
      TurnProcess(currentTurn,clickedSquare);
    }

    private bool CheckIfGameEnds()
    {
        bool gridFull = gridManager.checkIfGridisFull();
        GridSquareState winner = CheckForWin();

        if(winner != GridSquareState.empty)
        {
            if(winner == playerState)
            {
                // Player wins
                currentGameState = GameResults.playerWin;
                gameResultText.text = playerState.ToString() + " WINS";
                gameResultText.color = playerColor;
                return true;
            }
            else
            {
                // Enemy Wins
                currentGameState = GameResults.enemyWin;
                gameResultText.text = enemyState.ToString() + " WINS";
                gameResultText.color = enemyColor;
                return true;
            }
        }
        else
        {
            if(gridFull)
            {
                // Game is Draw
                currentGameState= GameResults.draw;
                gameResultText.text = "DRAW";
                return true;
            }
            else
            {
                // Game is ongoing
                return false;
            }
        }
    }

    private GridSquareState CheckForWin()
    {
        GridSquareState winner = GridSquareState.empty;

        // HORIZONTAL WIN CONDITIONS ARE CHECKED HERE

        winner = gridManager.CheckForWin(0, 1, 2);
        if(winner != GridSquareState.empty)
        {
            winLineManager.setWinLine(WinLine.horizontalTop);
            return winner;
        }
        winner = gridManager.CheckForWin(3, 4, 5);
        if(winner !=GridSquareState.empty)
        {
            winLineManager.setWinLine(WinLine.horizontalMid);
            return winner;
        }
        winner = gridManager.CheckForWin(6,7,8);
        if(winner !=GridSquareState.empty)
        {
            winLineManager.setWinLine(WinLine.horizontalBottom);
            return winner;
        }

        // VERTICAL WIN CONDITIONS ARE CHECKED HERE

        winner = gridManager.CheckForWin(0, 3, 6);
        if (winner != GridSquareState.empty)
        {
            winLineManager.setWinLine(WinLine.verticalLeft);
            return winner;
        }
        winner = gridManager.CheckForWin(1, 4, 7);
        if (winner != GridSquareState.empty)
        {
            winLineManager.setWinLine(WinLine.verticalMid);
            return winner;
        }
        winner = gridManager.CheckForWin(2, 5, 8);
        if (winner != GridSquareState.empty)
        {
            winLineManager.setWinLine(WinLine.verticalRight);
            return winner;
        }

        // DIAGONALS WIN CONDITIONS ARE CHECKED HERE

        winner = gridManager.CheckForWin(0, 4, 8);
        if(winner!= GridSquareState.empty)
        {
            winLineManager.setWinLine(WinLine.diagonalLeft);
            return winner;
        }
        winner = gridManager.CheckForWin(2, 4, 6);
        if(winner != GridSquareState.empty)
        {
            winLineManager.setWinLine(WinLine.diagonalRight);
            return winner;
        }
        return winner;
    }
}

public enum Turn{playerTurn , enemyTurn }
public enum GameResults { ongoing , draw,playerWin,enemyWin}
