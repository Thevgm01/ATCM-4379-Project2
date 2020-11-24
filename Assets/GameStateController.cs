using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public CardPlayer_Human human;
    public CardPlayer_Bot bot;

    public enum State
    {
        PlayerTurn,
        BotTurn
    }

    private State state;

    float botTimer;
    float botDelay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        state = State.PlayerTurn;
    }

    // Update is called once per frame
    void Update()
    {
        if(state == State.BotTurn)
        {
            botTimer += Time.deltaTime;
            if(botTimer > botDelay)
            {
                botTimer = 0;
                bool result = bot.Evaluate();
                if (!result)
                {
                    state = State.PlayerTurn;
                    human.BeginTurn();
                }
            }
        }
    }

    public void EndPlayerTurn()
    {
        if (state == State.PlayerTurn)
        {
            state = State.BotTurn;
            botTimer = 0;
            bot.BeginTurn();
        }
    }
}
