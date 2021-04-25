using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScoreManager : MonoBehaviour
{

    public ScoreEvent OnScoreEvent = new ScoreEvent();
    public UnityEvent OnPlayerHitChain = new UnityEvent();

    public ScoreUI uiScore;

    public int currentScore = 0;
    public int hitCount = 0;
    public int blueTeamScore = 0;
    public int redTeamScore = 0;

    public void Init(ScoreUI scoreUI) {
        this.uiScore = scoreUI;

        OnPlayerHitChain.AddListener(UpdateHitCount);
        OnScoreEvent.AddListener(UpdateScoreCount);
    }
    
    public void UpdateScoreCount(TeamColor teamColor, int amount) {
        currentScore += amount;
        UpdateTeamScore(teamColor);
        UpdateScoreLabel();

    }

    void UpdateTeamScore(TeamColor teamColor) {
        if(teamColor == TeamColor.Red)
            blueTeamScore++;
        else
            redTeamScore++;
    }

    void UpdateScoreLabel() {
        uiScore.ChangeScoreText(currentScore);
        uiScore.ChangeBlueScore(blueTeamScore);
        uiScore.ChangeRedScore(redTeamScore);
    }

    public void UpdateHitCount() {
        hitCount++;
        UpdateHitLabel();
        CancelInvoke("ResetHitCount");
        Invoke("ResetHitCount", 4f);
    }
    
    void UpdateHitLabel() {
        uiScore.ChangeHitText(hitCount);
    }

    void ResetHitCount() {
        hitCount = 0;
        UpdateHitLabel();
    }
}

[System.Serializable]
public class ScoreEvent: UnityEvent<TeamColor, int> {}
