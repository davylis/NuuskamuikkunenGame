using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameTimer : MonoBehaviour
{
   [SerializeField] TMP_Text _timerText; 

    float _elapsedTime;

    void Update()
    {
        _elapsedTime += Time.deltaTime;

        int minutes = Mathf.FloorToInt(_elapsedTime / 60f);
        int seconds = Mathf.FloorToInt(_elapsedTime % 60f);

        _timerText.text = $"{minutes:00}:{seconds:00}";
    }
}