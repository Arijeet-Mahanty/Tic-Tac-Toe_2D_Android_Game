using NUnit.Framework.Constraints;
using UnityEngine;

public class WinLineManager : MonoBehaviour
{
    [SerializeField] private GameObject verticalMiddleWin;
    [SerializeField] private GameObject horizontalMiddleWin;

    [SerializeField] private GameObject verticalLeftWin;
    [SerializeField] private GameObject horizontalTopWin;
    [SerializeField] private GameObject diagonalLeftWin;

    [SerializeField] private GameObject diagonalRightWin;
    [SerializeField] private GameObject verticalRightWin;
    [SerializeField] private GameObject horizontalBottomWin;

    public void setWinLine(WinLine winline)
    {
        switch (winline)
        {
            case WinLine.none: { SetWinLineActive(null); break; }
            case WinLine.verticalLeft: { SetWinLineActive(verticalLeftWin); break; }
            case WinLine.verticalRight: { SetWinLineActive(verticalRightWin); break; }
            case WinLine.verticalMid: { SetWinLineActive(verticalMiddleWin); break; }

            case WinLine.horizontalMid: {  SetWinLineActive(horizontalMiddleWin); break; }
            case WinLine.horizontalTop: {  SetWinLineActive(horizontalTopWin); break; }
            case WinLine.horizontalBottom: { SetWinLineActive(horizontalBottomWin); break; }

            case WinLine.diagonalLeft: {  SetWinLineActive(diagonalLeftWin); break; }
            case WinLine.diagonalRight: {  SetWinLineActive(diagonalRightWin); break; }

        }
    }

    public void SetWinLineActive(GameObject activeWinLine)
    {
        verticalMiddleWin.SetActive(verticalMiddleWin == activeWinLine);
        verticalRightWin.SetActive(verticalRightWin == activeWinLine);
        verticalLeftWin.SetActive(verticalLeftWin == activeWinLine);

        horizontalMiddleWin.SetActive(horizontalMiddleWin == activeWinLine);
        horizontalTopWin.SetActive(horizontalTopWin == activeWinLine);
        horizontalBottomWin.SetActive(horizontalBottomWin == activeWinLine);

        diagonalLeftWin.SetActive(diagonalLeftWin == activeWinLine);
        diagonalRightWin.SetActive(diagonalRightWin == activeWinLine);
    }
}
public enum WinLine { none,verticalMid,horizontalMid,verticalLeft,diagonalLeft,verticalRight,diagonalRight,horizontalTop,horizontalBottom}
