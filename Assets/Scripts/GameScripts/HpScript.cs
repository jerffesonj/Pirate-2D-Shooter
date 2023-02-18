using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//Class controlling the hp bar on the player and enemy
public class HpScript : MonoBehaviour
{
    [SerializeField] private Image hpImage;
    [SerializeField] private Hp hp;
    void Start()
    {
        hp.onRemoveHp += SetHpBar;
    }

    void SetHpBar(float value)
    {
        hpImage.fillAmount = value;
    }
}
