using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Enemy : MonoBehaviour
{
    public Text healthPointTxt;
    private float fixedHealthPoint =50f;
    private float healthPoint = 50f;
    private float dropManyEnemy = 50f;
    public Clicker clicker;
    public Button enemy;

    private void Update() {
        checkDeath();
    }

    public void takeDamage(float dmg) {
        healthPoint -= dmg;
        Debug.Log(healthPoint);
        healthPointTxt.text = $"{healthPoint}";
    }

    public void checkDeath() {
        if (healthPoint < 0 + clicker.damage) {
            clicker.changeScore(dropManyEnemy, true);
            healthPoint = fixedHealthPoint * 2;
            fixedHealthPoint *= 2;
            dropManyEnemy *= 3;
            enemy.GetComponent<Image>().color = Random.ColorHSV();
        }
    }
}
