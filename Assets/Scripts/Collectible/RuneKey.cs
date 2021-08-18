using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class RuneKey : MonoBehaviour
{
    public class RuneCollectedEvent : UnityEvent<KeyColor> { }

    public static RuneCollectedEvent OnRuneCollected = new RuneCollectedEvent();

    public KeyColor runeColor = KeyColor.Blue;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag != "Player")
            return;

        OnRuneCollected.Invoke(runeColor);

        //add to inventory?

        FindObjectOfType<AudioManager>().Play("Rune");
        Destroy(gameObject);

    }
}
