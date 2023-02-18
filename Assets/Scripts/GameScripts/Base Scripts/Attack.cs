using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class to handle all attacks
public class Attack : MonoBehaviour
{
    [SerializeField] protected int damage;
    [SerializeField] protected float maxTimeToAttack = 0.5f;
    [SerializeField] protected Transform frontAttackLocation;
    [SerializeField] protected List<Transform> sideAttackLocations;

    protected ObjectPooling bulletPool;
    protected BoatSounds sound;
    protected float timeToAttack = 0;
    // Start is called before the first frame update
    protected void Start()
    {
        sound = GetComponent<BoatSounds>();

        timeToAttack = maxTimeToAttack;

        GetBulletPooling();
    }

    private void GetBulletPooling()
    {
        for (int i = 0; i < GameController.instance.poolings.Count; i++)
        {
            if (GameController.instance.poolings[i].GetPooledObj().GetComponent<BulletScript>())
            {
                bulletPool = GameController.instance.poolings[i];
                break;
            }
        }
    }

    protected void Update()
    {
        CalculateTime();
    }

    private void CalculateTime()
    {
        timeToAttack += Time.deltaTime;
        if (timeToAttack >= maxTimeToAttack)
        {
            timeToAttack = maxTimeToAttack;
        }
    }

    protected void SideAttack()
    {
        if (timeToAttack < maxTimeToAttack)
            return;
        timeToAttack = 0;

        for (int i = 0; i < sideAttackLocations.Count; i++)
        {
            FireBullet(i);
        }
    }

    protected void FrontAttack()
    {
        if (timeToAttack < maxTimeToAttack)
          return;
        timeToAttack = 0;

        FireBullet(frontAttackLocation.position, transform.right);
    }

    void FireBullet(Vector3 startPos, Vector3 direction)
    {
        GameObject bullet = bulletPool.GetPooledObj();
        BulletScript bulletScript = bullet.GetComponent<BulletScript>();

        StartCoroutine(ResetTrailDist(bullet));

        bulletScript.InitiateBullet(startPos, direction, damage);

        sound.PlayShotSound();
    }

    IEnumerator ResetTrailDist(GameObject bullet)
    {
        yield return new WaitForSeconds(.1f);
        bullet.GetComponentInChildren<TrailRenderer>().time = 0.3f;

    }
    void FireBullet(int indexLocation)
    {
        FireBullet(sideAttackLocations[indexLocation].position, transform.up);
    }

    protected void SelfDestruct()
    {
        sound.PlayChaserAttackSound();
        gameObject.SetActive(false);
    }
}
