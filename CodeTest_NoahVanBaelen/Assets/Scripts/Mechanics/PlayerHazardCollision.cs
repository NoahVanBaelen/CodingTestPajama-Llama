using Platformer.Core;
using Platformer.Mechanics;
using Platformer.Model;
using UnityEngine;
using static Platformer.Core.Simulation;

namespace Platformer.Gameplay
{

    /// <summary>
    /// Fired when a Player collides with an Enemy.
    /// </summary>
    /// <typeparam name="PlayerHazardCollision"></typeparam>
    public class PlayerHazardCollision : Simulation.Event<PlayerHazardCollision>
    {

        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            Schedule<PlayerGotHit>();
        }
    }
}
