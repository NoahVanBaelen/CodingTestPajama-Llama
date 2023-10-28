using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPoint : MonoBehaviour
{
    [SerializeField] private Sprite _checkSprite;
    private bool _hasBecomeCheckpoint = false;
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !_hasBecomeCheckpoint)
        {
            //Set New SpawnLocation
            PlatformerModel model = Simulation.GetModel<PlatformerModel>();
            model.spawnPoint = gameObject.transform;

            //Set New Sprite
            gameObject.transform.GetChild(0).GetComponent<SpriteRenderer>().sprite = _checkSprite;
            _hasBecomeCheckpoint = true;
        }
    }
}
