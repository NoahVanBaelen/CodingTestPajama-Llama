using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RespawnLogic : MonoBehaviour
{
    [SerializeField] private List<GameObject> _activationHulpBlocks;
    [SerializeField] private int _timesNeededToDieForActivation;
    private int _deadCount = 0;

    public void CheckForHelp()
    {
        _deadCount++;
        if (_timesNeededToDieForActivation == _deadCount)
        {
            foreach (GameObject block in _activationHulpBlocks)
            {
                block.SetActive(true);
            }
        }
    }
}
