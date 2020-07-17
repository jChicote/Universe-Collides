using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreUI : MonoBehaviour
{
    public Text scoreText;
    public Text blueScoreText;
    public Text redScoreText;
    public Text hitCountText;

    public void Init() {

    }

    public void ChangeScoreText(int score) {
        scoreText.text = score.ToString();
    }

    public void ChangeBlueScore(int score) {
        blueScoreText.text = score.ToString();
    }

    public void ChangeRedScore(int score) {
        redScoreText.text = score.ToString();
    }

    public void ChangeHitText(int hitCount) {
        hitCountText.text = hitCount.ToString();
    }
}
