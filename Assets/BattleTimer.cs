using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleTimer : MonoBehaviour
{

    public float minimalTimeBetweenMoves;
    float lastMove;
    bool ready = true;
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
        if (ready && Time.time -lastMove >minimalTimeBetweenMoves)
        {
            ready = false;
            lastMove = Time.time;
            manager.RunOneTurn();
        }
    }

    void GetReady()
    {
        ready= true;
    }
}
