using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour{
    //Player speeds
    public float moveSpeed = 8f; 
    public float sidewaysMoveSpeed = 12f;

    //So the player can't fall off the edge
    //The spawning of the sections got a small bit messy hence the strange values,
    public float boundaryLeft = 8f;
    public float boundaryRight = 27f; 

    private ScoreController ScoreController; 

    //so we can change the color and tag of the player
    private Renderer playerRenderer;
    private string currentColorTag;

    //An array of the tunnel colours
   private Color[] colors = new Color[] {
        new Color(0f, 0.2196f, 1f),//blue 
        new Color(0f, 0.7373f, 0.3647f),//green
        new Color(1f, 0f, 0f), //red
        new Color(0.9922f, 1f, 0f)//yellow
    };


    //an array of the tunnel tags
    private string[] tags = new string[]{
        "BlueTunnel",
        "GreenTunnel",
        "RedTunnel",
        "YellowTunnel"
    };

    private void Start(){
        ScoreController = FindObjectOfType<ScoreController>();
        //assign the player a random colour at the start of the game
        playerRenderer = GetComponent<Renderer>();
        AssignRandomColor();
    }

    private void Update(){
        //forward movement
        transform.Translate(Vector3.forward * Time.deltaTime * moveSpeed, Space.World);

        //mve left when the left arrow key is pressed
        if (Input.GetKey(KeyCode.LeftArrow) && transform.position.x > boundaryLeft){
            transform.Translate(Vector3.left * sidewaysMoveSpeed * Time.deltaTime, Space.World);
        }

        //move right when the right arrow is pressed
        if (Input.GetKey(KeyCode.RightArrow) && transform.position.x < boundaryRight){
            transform.Translate(Vector3.right * sidewaysMoveSpeed * Time.deltaTime, Space.World);
        }
    }

    //method so we can increase the players speed dynamically
    public void IncreaseSpeed(float speedIncrement){
        moveSpeed += speedIncrement;
    }

    public void IncreaseSidewaysMoveSpeed(float increment){
        sidewaysMoveSpeed += increment;
    }

    //assign a random colour and corresponding tag to the player
    private void AssignRandomColor(){
        int randomIndex = Random.Range(0, colors.Length);
        playerRenderer.material.color = colors[randomIndex];
        currentColorTag = tags[randomIndex];
    }

    private void OnCollisionEnter(Collision collision){
        //The different "illegal" objects the player can collide with
        if (collision.gameObject.CompareTag("Block") || 
            collision.gameObject.CompareTag("FallingBlock") || 
            collision.gameObject.CompareTag("SidewaysBlock") ||
            collision.gameObject.CompareTag("Pillar")){
                GameOver();
        }
    }

    private void OnTriggerEnter(Collider other){
    //Check if the player has gone under the tunnel that matches the player tunnel
    if (other.gameObject.CompareTag(currentColorTag)) {
        //if true assign the player a new random colour and update the numebr of tunnels
        //the player has travelled under
        AssignRandomColor();
        ScoreController.TunnelCount();

    }
    else if (other.gameObject.CompareTag("Collectable")){
        //when the player collides with a coin update the coin score
        ScoreController.CollectCoin();
        return;
    }
    else{
        GameOver();
    }
}
    private void GameOver(){
        //Go back to main menu after 2 seconds
        StartCoroutine(LoadMainMenuAfterDelay());
        //This ends our game
        Time.timeScale = 0; 
        //hide the score text that was in the top left corner
        if (ScoreController.ScoreTextDisplay != null){
            ScoreController.ScoreTextDisplay.gameObject.SetActive(false);
        }
        //show game over and the players score text
        if (ScoreController.GameOverDisplay != null){
            ScoreController.GameOverDisplay.gameObject.SetActive(true);
        }
        if (ScoreController.endScoreDisplay != null){
            ScoreController.endScoreDisplay.text = "Your Score: " + ScoreController.totalScore.ToString();
            ScoreController.endScoreDisplay.gameObject.SetActive(true);
        }
    }
     private IEnumerator LoadMainMenuAfterDelay(){
        yield return new WaitForSecondsRealtime(2f);
        SceneManager.LoadScene("Menu");
    }
}
