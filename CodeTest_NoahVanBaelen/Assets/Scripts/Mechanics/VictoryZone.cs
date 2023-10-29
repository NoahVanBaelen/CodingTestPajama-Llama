using Platformer.Gameplay;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Mechanics
{
    /// <summary>
    /// Marks a trigger as a VictoryZone, usually used to end the current game level.
    /// </summary>
    public class VictoryZone : MonoBehaviour
    {
        void OnTriggerEnter2D(Collider2D collider)
        {
            var p = collider.gameObject.GetComponent<PlayerController>();
            HouseKeys keys = collider.gameObject.GetComponent<HouseKeys>();
            if (p != null)
            {
                if (keys.HasFoundAllKeys())
                {
                    var ev = Schedule<PlayerEnteredVictoryZone>();
                    ev.victoryZone = this;
                }
            }
        }
    }
}