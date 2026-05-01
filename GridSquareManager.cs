using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.EventSystems;

public class GridSquareManager : MonoBehaviour,IPointerClickHandler
{
    [SerializeField] private TextMeshProUGUI oText;
    [SerializeField] private TextMeshProUGUI xText;
    private GridSquareState currentState = GridSquareState.empty;
    private int SquareId;

    public void OnPointerClick(PointerEventData eventData)
    {
        GameManager.instance.GridSquareClicked(SquareId);
    }

    public void setSquare(GridSquareState newState, Color newColor)
    {
        if (newState == GridSquareState.empty)
        {
            oText.enabled = false;
            xText.enabled = false;
        }

        else if (newState == GridSquareState.x)
        {
            oText.enabled = false;
            xText.enabled = true;
        }

        else if (newState == GridSquareState.o)
        {
            oText.enabled = true;
            xText.enabled = false;
        }
        currentState = newState;
        oText.color = newColor;
        xText.color = newColor;
    }

    public void setSquareID(int Id)
    { 
        SquareId = Id;  
    }

    public GridSquareState getSquareState()
    {
        return currentState;    
    }
}
 public enum GridSquareState { empty, x, o };
