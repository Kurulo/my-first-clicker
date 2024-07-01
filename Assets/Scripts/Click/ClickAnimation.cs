using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ClickAnimation : MonoBehaviour
{
    private bool move;
    private Vector2 randomVector;

    private void Update() {
        if (!move) return;
        transform.Translate(randomVector * Time.deltaTime);
    }

    public void StartMotion(float damageClick) {
        transform.localPosition = Vector2.zero;
        GetComponent<Text>().text = "+" + damageClick;
        randomVector = new Vector2(Random.Range(-4, 4), Random.Range(-4, 4));
        move = true;
        GetComponent<Animation>().Play();
    }
}
