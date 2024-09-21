using System.Collections;
using UnityEngine;
using TMPro;
public class GameCountDown : MonoBehaviour
{
    [SerializeField]
    private TextMeshProUGUI countdownText;
    private Color[] colors = new Color[] {
        new Color(0f, 0.2196f, 1f),//blue
        new Color(0f, 0.7373f, 0.3647f),//green
        new Color(1f, 0f, 0f),//red
        new Color(0.9922f, 1f, 0f),//yellow
        new Color(0.50196078f, 0f, 0.50196078f),//purple
        new Color(1f, 0.75294118f, 0.79607843f),//pink
        new Color(1f, 0.64705882f, 0f)//ornage
    };

    private void Start(){
        StartCoroutine(StartCountdown());
    }

    IEnumerator StartCountdown(){
        //countdown 3,2,1
        int count = 3;
        while (count > 0){
            // Set the text color to a random one from the colors array
            countdownText.color = colors[Random.Range(0, colors.Length)];
            countdownText.text = count.ToString();
            yield return new WaitForSeconds(1);
            count--;
        }

        countdownText.color = colors[Random.Range(0, colors.Length)];
        countdownText.text = "GO!";
        yield return new WaitForSeconds(1);
        //hide the countdown tezt
        countdownText.gameObject.SetActive(false); 
    }
}

