using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStateController : MonoBehaviour
{
    public CardPlayer_Human human;
    public CardPlayer_Bot bot;

    public AudioClip nextTurnSound;

    public GameObject menuObj, loseObj, winObj;
    public AudioClip menuMusic, gameMusic;
    AudioSource musicPlayer;

    public enum State
    {
        Menu,
        PlayerTurn,
        BotTurn
    }

    private State state;

    float botTimer;
    float botDelay = 0.3f;

    // Start is called before the first frame update
    void Start()
    {
        state = State.Menu;

        musicPlayer = AudioHelper.PlayClip2D(menuMusic, 0.5f, 0.5f);
        musicPlayer.loop = true;
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
                    if (bot.fleet.Count < 0) winObj.SetActive(true);
                    else human.BeginTurn();
                }
            }
        }
    }

    public void EndPlayerTurn()
    {
        if (state == State.PlayerTurn)
        {
            AudioHelper.PlayClip2D(nextTurnSound, 1);
            state = State.BotTurn;
            botTimer = 0;
            if (human.fleet.Count < 0) loseObj.SetActive(true);
            else bot.BeginTurn();
        }
    }

    public void BeginGame()
    {
        if(state == State.Menu)
        {
            state = State.PlayerTurn;
            Destroy(musicPlayer.gameObject);
            musicPlayer = AudioHelper.PlayClip2D(gameMusic, 1);
            musicPlayer.loop = true;
            AudioHelper.PlayClip2D(nextTurnSound, 1);
            human.BeginTurn();
            menuObj.SetActive(false);
        }
    }
}
