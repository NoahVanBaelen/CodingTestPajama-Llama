using Platformer.Core;
using Platformer.Gameplay;
using Platformer.Model;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Platformer.Gameplay
{
    /// <typeparam name="PlayerGotHit"></typeparam>

    public class PlayerGotHit : Simulation.Event<PlayerGotHit>
    {
        PlatformerModel model = Simulation.GetModel<PlatformerModel>();

        public override void Execute()
        {
            var player = model.player;
            if (player.health.IsAlive)
            {
                player.health.Hit();
                //model.virtualCamera.m_Follow = null;
                //model.virtualCamera.m_LookAt = null;
                // player.collider.enabled = false;
                //player.controlEnabled = false;

                if (player.audioSource && player.ouchAudio)
                    player.audioSource.PlayOneShot(player.ouchAudio);
                player.animator.SetTrigger("hurt");
            }
        }
    }
}
