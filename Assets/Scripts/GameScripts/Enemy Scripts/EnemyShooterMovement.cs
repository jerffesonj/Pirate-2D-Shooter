using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class EnemyShooterMovement : EnemyBaseMovement
{
    [SerializeField] private float radius;
    [SerializeField] private float minDistance;
    
    private EnemyShooterAttack attack;
    private Tilemap obstacleGrid;
    private Collider2D[] colliders = new Collider2D[5];
    private Vector3Int randomPosV3;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        player = null;
        attack = GetComponent<EnemyShooterAttack>();
        obstacleGrid = GameObject.FindGameObjectWithTag("Obstacles").GetComponent<Tilemap>();

        randomPosV3 = new Vector3Int((int)Random.Range(-30, 30), (int)Random.Range(-15, 15), 0);
    }

    // Update is called once per frame
    void Update()
    {
        ShooterMovement();
    }

    private void ShooterMovement()
    {
        if (player == null)
        {
            CheckPlayerInRadius();

            CalculateAngle(randomPosV3);

            if (Vector3.Distance(randomPosV3, this.transform.position) < minDistance)
            {
                RandomNewPoint();

                CalculateAngle(randomPosV3);
                RotateEnemy();
            }
            else
            {
                CheckAngleToMove();
            }
        }
        else
        {
            CalculateAngle(player.transform.position);

            if (Vector2.Distance(player.transform.position, this.transform.position) < minDistance)
            {
                CheckAngleToShoot();
            }
            else
            {
                CheckAngleToMove();
            }
        }
    }

    private void CheckPlayerInRadius()
    {
        Physics2D.OverlapCircleNonAlloc(this.transform.position, radius, colliders, LayerMask.GetMask("Player"));
        for (int i = 0; i < colliders.Length; i++)
        {
            if (colliders[i] == null)
                continue;

            if (colliders[i].gameObject.CompareTag("Player"))
            {
                player = colliders[i].gameObject.transform;
                break;
            }
        }
    }

    private void RandomNewPoint()
    {
        if (obstacleGrid.GetTile(randomPosV3) != null)
        {
            randomPosV3 = new Vector3Int((int)Random.Range(-30, 30), (int)Random.Range(-15, 15), 0);
        }
    }

    private void CheckAngleToShoot()
    {
        if (Mathf.Abs(rotZ - angle) < 10)
        {
            attack.FrontAttack();
        }
        else if (rotZ - angle > 25 && rotZ - angle > 165)
        {
            RotateEnemy();
            attack.SideAttack();
        }
        else
        {
            RotateEnemy();
        }
    }

    private void CheckAngleToMove()
    {
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

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(this.transform.position, radius);

    }
}
