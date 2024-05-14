using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gift : PowerUp
{
    public float duration;

    public override IEnumerator PowerUpCoroutine(PlayerMovement target)
    {
        this.target = target;
        FindOppositeTarget();
        float prevRate = this.target.GetComponent<EmakRadar>().GetIncreaseRate();
        this.target.GetComponent<EmakRadar>().SetIncreaseRate(prevRate * 1.5f);
        yield return new WaitForSeconds(duration);
        this.target.GetComponent<EmakRadar>().SetIncreaseRate(prevRate);

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        PickedUp(collision);
    }
}