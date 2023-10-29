using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ActivateHelpBlocks : MonoBehaviour
{
    [SerializeField] private List<GameObject> _otherActivationBlocks;
    [SerializeField] private List<GameObject> _hulpBlocks;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            foreach (GameObject block in _hulpBlocks)
            {
                block.SetActive(true);
            }

            foreach (GameObject block in _otherActivationBlocks)
            {
                block.SetActive(false);
            }

            gameObject.SetActive(false);
        }
    }
}
