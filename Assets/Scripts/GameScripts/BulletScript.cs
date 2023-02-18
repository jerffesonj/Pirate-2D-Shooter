using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class BulletScript : MonoBehaviour
{
    [SerializeField] float bulletSpeed;
    [SerializeField] float maxTimeAlive = 3;

    private ObjectPooling explosionFxPool;
    private Rigidbody2D rb;
    private float timeAlive;
    private int damage;
    
    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }
    
    void Start()
    {
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

    void Update()
    {
        CheckTimeAlive();
    }

    private void CheckTimeAlive()
    {
        timeAlive += Time.deltaTime;
        if (timeAlive >= maxTimeAlive)
        {
            timeAlive = 0;
            gameObject.SetActive(false);
        }
    }

    public void InitiateBullet(Vector3 startPos, Vector3 direction, int damage)
    {
        transform.position = startPos;
        gameObject.SetActive(true);

        SetBulletDamage(damage);
        FireBullet(direction);
    }

    public void SetBulletDamage(int damage)
    {
        this.damage = damage;
    }
    public void FireBullet(Vector3 parentVector)
    {
        rb.AddForce(parentVector * bulletSpeed);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Hp hp = collision.gameObject.GetComponent<Hp>();
        if (hp)
            hp.RemoveHp(damage);

        GameObject explosion = explosionFxPool.GetPooledObj();
        explosion.transform.position = this.transform.position;
        explosion.SetActive(true);
        gameObject.SetActive(false);
    }
    private void ResetPosition()
    {
        transform.position = Vector2.zero;
    }

    private void OnDisable()
    {
        ResetPosition();
        GetComponentInChildren<TrailRenderer>().time = 0;
        timeAlive = 0;
    }
}
