using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHp : Hp
{
    //Delegate calling methods on the player dies
    public delegate void OnPlayerDeath();
    public event OnPlayerDeath onPlayerDeath;
    
    void Start()
    {
        base.Start();
        
    }

    public override void RemoveHp(int damage)
    {
        currentHp -= damage;

        float hpOnPercentage = (float)currentHp / (float)maxHp;

        onRemoveHp?.Invoke(hpOnPercentage);

        if (currentHp <= 0)
        {
            currentHp = 0;
            boatSounds.PlayBoatDeathSound();

            onPlayerDeath?.Invoke();
        }
    }
}
