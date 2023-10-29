using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandsOnMovingPlatform : MonoBehaviour
{
    [SerializeField] private float _timeBeforeOffThePlatform = 0.1f;
    private PlayerController _playerController;
    private bool _isNotOnPlatform = false;
    private float _accumalatedTime;
    private void Update()
    {
        if (_isNotOnPlatform)
        {
            _accumalatedTime += Time.deltaTime;
            if (_accumalatedTime >= _timeBeforeOffThePlatform)
            {
                _playerController.SetMovingPlatform();
                _isNotOnPlatform=false;
            }
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController = collision.gameObject.GetComponent<PlayerController>();
            _playerController.SetMovingPlatform(gameObject);
            _isNotOnPlatform = false;
            _accumalatedTime = 0;
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _isNotOnPlatform = true;
        }
    }
}
