using UnityEngine;
using TMPro;

public class HighScore : MonoBehaviour
{
    public TMP_Text highScore;
    void Update()
    {
        highScore.text = PlayerPrefs.GetInt("HighScore", 0).ToString();
    }
}
