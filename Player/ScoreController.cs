
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreController : MonoBehaviour{
    //all the score tracking variables
    public static int coinCount;
    public static int currentSection; 
    private int coinScore;
    public static int tunnelCount;
    public int totalScore;
    public TextMeshProUGUI ScoreTextDisplay;

    //when the player dies "Game Over!" along with their final score will be displayed
    public TextMeshProUGUI GameOverDisplay;
    public TextMeshProUGUI endScoreDisplay;

    //At the start of the game set the game over and endscore to false
    private void Start(){
        coinCount = 0;
        currentSection = 1; 
        coinScore = 0;
        tunnelCount = 0;
        totalScore = 0;
        if (endScoreDisplay != null)
        endScoreDisplay.gameObject.SetActive(false);

        if (GameOverDisplay != null)
            GameOverDisplay.gameObject.SetActive(false);
}
    public void TunnelCount(){
        //the score of each tunnel in a section
        //so section 1 a tunnel is worth 3 points
        //section 2  a tunnel is worth 5 points
        //section 3 a tunnel is worth 7 points
        //and so on
        int scoreToAdd = 1 + (currentSection * 2);
        tunnelCount += scoreToAdd;
        UpdateTotalScore();
}

    public void CollectCoin(){
        //so if you're in section 1 collectables are worth 1 point
        //section 2 collectables are worth 2
        //section 3 collectables are worth 3
        //and so on
        coinScore = currentSection;
        coinCount += coinScore;
        UpdateTotalScore();
    }

    //update and display the total score 
   private void UpdateTotalScore(){
    if (ScoreTextDisplay != null)
    {
        totalScore = coinCount + tunnelCount; // Calculate total score
        ScoreTextDisplay.text = "Score: " + totalScore.ToString();
    }
}
    public static void ChangeSection(int section){
        currentSection = section;
}
}

