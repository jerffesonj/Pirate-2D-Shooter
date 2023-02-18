using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class responsible to move all the boats in game
public class Movement : MonoBehaviour
{
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected float rotateSpeed;
    protected Rigidbody2D rigidBody;

    [SerializeField] protected TrailRenderer trail;

    protected void Start()
    {
        rigidBody = GetComponent<Rigidbody2D>();
        trail = GetComponentInChildren<TrailRenderer>();
    }
    protected void Move(float movementDirection)
    {
        rigidBody.velocity = movementDirection * moveSpeed * Time.deltaTime * transform.right;
    }

    protected void Rotate(float rotationDirection)
    {
        rigidBody.rotation -= rotateSpeed * rotationDirection * Time.deltaTime;
    }

    #region Methods to avoid trail teleport when respawn
    private void OnEnable()
    {
        StartCoroutine(ResetTrailDist());
    }
    private IEnumerator ResetTrailDist()
    {
        yield return new WaitForSeconds(.1f);
        trail.time = 0.3f;
    }
    private void OnDisable()
    {
        trail.time = 0;
    }
    #endregion
}
