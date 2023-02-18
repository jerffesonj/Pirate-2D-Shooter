using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class handling the base movement of the enemies
public class EnemyBaseMovement : Movement
{
    protected Transform player;

    protected float angle = 0;
    protected float rotZ = 0;

    // Start is called before the first frame update
    protected void Start()
    {
        base.Start();
        player = GameObject.FindGameObjectWithTag("Player").transform;
    }

    //Method responsible to calculate the angle between this object and the position of the other object
    protected void CalculateAngle(Vector3 position)
    {
        Vector2 direction = position - this.transform.position;
        direction = direction.normalized;

        angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

        rotZ = transform.localEulerAngles.z;

        if (rotZ > 180)
            rotZ -= 360;
    }

    protected void RotateEnemy()
    {
        if (rotZ + 360 < angle + 360)
        {
            Rotate(-1);
        }
        else
        {
            Rotate(1);
        }
    }
}
