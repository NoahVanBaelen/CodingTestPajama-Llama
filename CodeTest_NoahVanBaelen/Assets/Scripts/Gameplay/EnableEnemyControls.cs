using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.Gameplay
{

    public class EnableEnemyControls : Simulation.Event<EnableEnemyControls>
    {
        public EnemyController enemy;

        public override void Execute()
        {
            enemy.control.enabled = true;
        }
    }
}
