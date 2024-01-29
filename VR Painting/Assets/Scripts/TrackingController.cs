using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackingController : MonoBehaviour
{
    [SerializeField] GameObject board;

    public void BoardTrackerFound()
    {
        print("Using board tracker.");
        board.GetComponent<BoardController>().SetBoardPosition(GetComponent<Transform>().gameObject);
    }

    public void BoardTrackerLost()
    {
        print("Not using board tracker.");
    }
}
