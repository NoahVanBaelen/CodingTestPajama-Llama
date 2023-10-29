using Platformer.Core;
using Platformer.Mechanics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.Gameplay
{
    /// <summary>
    /// Fired when the health component on an enemy has a hitpoint value of  0.
    /// </summary>
    /// <typeparam name="EnemyRespawn"></typeparam>
    public class EnemyRespawn : Simulation.Event<EnemyRespawn>
    {
        public EnemyController enemy;

        public override void Execute()
        {
            enemy._collider.enabled = true;
            enemy.TeleportToStartLocation();
            var ev = Simulation.Schedule<EnableEnemyControls>(1f);
            ev.enemy = enemy;
        }
    }
}
