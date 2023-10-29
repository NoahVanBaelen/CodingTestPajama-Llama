using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStandsOnMovingPlatform : MonoBehaviour
{
    private PlayerController _playerController;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController = collision.gameObject.GetComponent<PlayerController>();
            _playerController.SetMovingPlatform(gameObject);
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            _playerController.SetMovingPlatform();
        }
    }
}
