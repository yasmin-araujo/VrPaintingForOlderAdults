using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingController : MonoBehaviour
{
    [SerializeField] GameObject board;
    [SerializeField] GameObject boardTracker;

    public void BoardTrackerFound()
    {
        print("Using board tracker.");
        board.GetComponent<BoardController>().SetBoardPosition(boardTracker);
    }

    public void BoardTrackerLost()
    {
        print("Not using board tracker.");
    }
}
