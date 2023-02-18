using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible for the chase movement
public class EnemyChaserMovement : EnemyBaseMovement
{
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        CalculateAngle(player.position);
        RotateEnemy();

        if (Mathf.Abs(angle - rotZ) < 25)
        {
            Move(1);
        }
        else
        {
            Move(0);
        }
    }
}
