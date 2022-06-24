using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTimer : MonoBehaviour
{

    public float minimalTimeBetweenMoves;
    float lastMove;
    bool ready = true;
    bool readying = false;
    BattleManager manager;

    private void Start()
    {
        manager = GetComponent<BattleManager>();
        lastMove = 0;
    }

    private void OnEnable()
    {
        BattleManager.OnCharacterFinishedTurn += GetReady;
    }

    private void OnDisable()
    {
        BattleManager.OnCharacterFinishedTurn -= GetReady;
    }

    void Update()
    {
        if (ready)
        {
            lastMove = Time.time;
            readying = true;
            ready = false;
        }
        if (readying && Time.time -lastMove >minimalTimeBetweenMoves)
        {
            readying = false;
            manager.RunOneTurn();
        }
    }

    void GetReady()
    {
        ready= true;
    }
}
