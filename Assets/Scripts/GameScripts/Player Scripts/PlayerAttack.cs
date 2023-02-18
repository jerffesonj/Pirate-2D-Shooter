using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttack : Attack
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        base.Update();
        if (Input.GetMouseButton(0))
        {
            FrontAttack();
        }
        if (Input.GetMouseButton(1))
        {
            SideAttack();
        }
    }
}

