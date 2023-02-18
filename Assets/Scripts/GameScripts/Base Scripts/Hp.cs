using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Base class to create the health points
public class Hp : MonoBehaviour
{
    [SerializeField] protected int currentHp;
    [SerializeField] protected int maxHp;

    //Delegate to call methods when the health changes
    public delegate void OnHealthChange(float percentage);
    public OnHealthChange onRemoveHp;

    protected BoatSounds boatSounds;
    // Start is called before the first frame update
    protected void Start()
    {
        boatSounds = GetComponent<BoatSounds>();
        ResetHP();
    }

    public virtual void RemoveHp(int damage)
    {
        currentHp -= damage;

        float hpOnPercentage = (float)currentHp / (float)maxHp;

        onRemoveHp?.Invoke(hpOnPercentage);
        
        if (currentHp <= 0)
        {
            currentHp = 0;
            boatSounds.PlayBoatDeathSound();
        }
    }

    public void ResetHP()
    {
        currentHp = maxHp;
        onRemoveHp?.Invoke(currentHp / maxHp);
    }

    private void OnDisable()
    {
        ResetHP();
    }
}
