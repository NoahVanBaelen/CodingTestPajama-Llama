using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreKeeper : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private TextMeshProUGUI _scoreText;
    [SerializeField] private int _maxScore;
    private int _currentScore = 0;
    void Start()
    {
        _scoreText.text = _currentScore.ToString();
    }

    public void Increment()
    {
        _currentScore++;
        _scoreText.text = _currentScore.ToString();

        if (_currentScore == _maxScore)
        {
            _scoreText.color = Color.yellow;
        }
    }
}
