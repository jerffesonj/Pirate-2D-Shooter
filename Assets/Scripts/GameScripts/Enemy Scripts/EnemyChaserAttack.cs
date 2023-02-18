using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Responsible for the attack of the chaser
public class EnemyChaserAttack : Attack
{
    private ObjectPooling explosionFxPool;

    // Start is called before the first frame update
    void Start()
    {
        base.Start();

        GetExplosionPooling();
    }

    private void GetExplosionPooling()
    {
        for (int i = 0; i < GameController.instance.poolings.Count; i++)
        {
            if (GameController.instance.poolings[i].GetPooledObj().GetComponent<FxType>())
            {
                if (GameController.instance.poolings[i].GetPooledObj().GetComponent<FxType>().GetFxName() == FxType.FxName.Explosion)
                {
                    explosionFxPool = GameController.instance.poolings[i];
                    break;
                }
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Hp playerHp = collision.gameObject.GetComponent<Hp>();
            DamagePlayer(playerHp);
        }
    }

    private void DamagePlayer(Hp playerHp)
    {
        playerHp.RemoveHp(damage);

        GameObject explosion = explosionFxPool.GetPooledObj();
        explosion.transform.position = this.transform.position;
        explosion.SetActive(true);
        SelfDestruct();
    }
}
