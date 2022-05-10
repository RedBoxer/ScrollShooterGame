using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Submarine : Helicopter
{
    private bool submerged = false;
    private Renderer selfRenderer;

    private Color submergedColor;
    private Color standartColor;
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        bossName = "Submarine";
        selfRenderer = this.gameObject.GetComponent<Renderer>();
        standartColor = selfRenderer.material.color;
        submergedColor = new Color(standartColor.r, standartColor.g, standartColor.b, standartColor.a / 5);
    }
    public override void Update()
    {
        base.Update();

        if (step == 13 && subStep == 0)
        {
            Submerge();
        }
    }

    private void Submerge()
    {
        submerged = !submerged;

        if (submerged)
        {
            selfRenderer.material.color = submergedColor;
        }
        else
        {
            selfRenderer.material.color = standartColor;
        }
    }

    protected override void MakeShot()
    {
        if (submerged)
        {
            base.MakeShot();
        }
    }

    public override void OnTriggerEnter2D(Collider2D other)
    {
        if (!submerged)
        {
            base.OnTriggerEnter2D(other);
        }
    }
}
