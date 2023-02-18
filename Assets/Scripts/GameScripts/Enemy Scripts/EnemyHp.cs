using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class controlling the enemy hp
[RequireComponent(typeof(Points))]
public class EnemyHp : Hp
{
    Points enemyPoints;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        enemyPoints = GetComponent<Points>();
    }

    public override void RemoveHp(int damage)
    {
        currentHp -= damage;

        float hpOnPercentage = (float)currentHp / (float)maxHp;

        onRemoveHp?.Invoke(hpOnPercentage);

        if (currentHp <= 0)
        {
            currentHp = 0;
            GameController.instance.AddPoints(enemyPoints.DeathPoints);
            boatSounds.PlayBoatDeathSound();

            this.gameObject.SetActive(false);
        }
    }
}
