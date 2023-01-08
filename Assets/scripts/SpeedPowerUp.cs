using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedPowerUp : PowerUp
{
    public override void ActivatePowerUp(GameObject player)
    {
        if(player.TryGetComponent(out PlayerController movement))
        {
            movement.speed *= 2;
        }
        base.ActivatePowerUp(player);
    }
}
