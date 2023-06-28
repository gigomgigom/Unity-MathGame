using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    static GameController _gameController = null;
    public static GameController gameController { get => _gameController; }

    [SerializeField] GameObject[] gamePhaseObjs;
    [SerializeField] GameClearController gameClearController;
    int currentGamePhase, gamePhaseCount;

    void Start()
    {
        gamePhaseCount = gamePhaseObjs.Length;
        _gameController = this;

        gameClearController.Init(gamePhaseCount);

        foreach (GameObject go in gamePhaseObjs)
            go.SetActive(go == gamePhaseObjs[0] ? true : false);
    }

    public void PhaseClear()
    {
        ++currentGamePhase;
        if (currentGamePhase < gamePhaseCount)
        {
            gamePhaseObjs[currentGamePhase - 1].SetActive(false);
            gamePhaseObjs[currentGamePhase].SetActive(true);
        }
        else
            GameClear();
    }

    void GameClear()
    {
        gameClearController.GameClear();
        //해당 게임의 기록 저장 및 게임 클리어 표시
        Debug.Log("GameClear");
    }
}
