using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class PowerUp : MonoBehaviour, IPowerUp
{
    public virtual void ActivatePowerUp(GameObject player)
    {
        Destroy(gameObject);
    }
}

public interface IPowerUp
{
    public abstract void ActivatePowerUp(GameObject player);
}
