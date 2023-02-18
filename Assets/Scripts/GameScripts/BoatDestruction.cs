using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Class controlling the degradation of the boat
public class BoatDestruction : MonoBehaviour
{
    [SerializeField] private List<Sprite> boatSprites;
    [SerializeField] private SpriteRenderer boatImage;

    private Hp thisHP;

    void Awake()
    {
        thisHP = GetComponent<Hp>();
    }
    private void OnEnable()
    {
        thisHP.onRemoveHp += ChangeBoatImage;
    }

    private void OnDisable()
    {
        ChangeBoatImage(1);

        thisHP.onRemoveHp -= ChangeBoatImage;
    }

    public void ChangeBoatImage(float percentage)
    {
        percentage *= 100;
        if (percentage >= 66)
        {
            boatImage.sprite = boatSprites[0];
        }
        else if (percentage < 66 && percentage >= 33)
        {
            boatImage.sprite = boatSprites[1];
        }
        else if (percentage < 33 && percentage > 0)
        {
            boatImage.sprite = boatSprites[2];
        }
    }
}
