using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Clicker : MonoBehaviour
{
    private float score = 0;
    public int damage = 1;
    public Text scoreText;
    public Text shopButtonText;
    public GameObject shopPanel;
    public Enemy enemy;
    public Bonus bonus;
    public Camera mainCamera;
    private float dopScore = 0, critDamage = 1;
    private float timeLeft = 1f;
    private float timeLeftBonusDbl = 20f;
    private bool activeBonus;
    private int clickNum, critChance = 20;
    public GameObject clickParent, clickPrefab;
    private ClickAnimation[] clickTextPool = new ClickAnimation[15];

    private void Start() {
        for (int i = 0; i < clickTextPool.Length; i++) {
            clickTextPool[i] = Instantiate(clickPrefab, clickParent.transform).GetComponent<ClickAnimation>();
        }
    }
    public void Update()
    {
        timeLeft -= Time.deltaTime;
        if ( timeLeft < 0 )
        {
            score += dopScore;
            ChangeScoreText(score);
            timeLeft += 1f;
            enemy.takeDamage(dopScore);
            //Debug.Log(score);
        }
        if (activeBonus) {
            timeLeftBonusDbl -= Time.deltaTime;
            if ( timeLeftBonusDbl < 0 )
            {
                Debug.Log("Bonus end");
                bonus.activateBonus(false);
                timeLeftBonusDbl += 20f;
            }
        }
    }
    ///<summary>
    ///Функция - при нажатии, добовляет очки игроку.
    ///</summary>
    public void onClick() {
        critDamage = Random.Range(0, critChance) == 1 ? 3:1;
        if (!activeBonus) {
            score += (float)damage * critDamage;
            enemy.takeDamage((float)damage * critDamage);
            clickTextPool[clickNum].StartMotion(damage * critDamage);
        }
        else {
            score += ((float)damage * 2)* critDamage;
            enemy.takeDamage(((float)damage * 2)* critDamage);
            clickTextPool[clickNum].StartMotion(damage * 2);
        }
        enemy.GetComponent<Animation>().Play();
        Debug.Log("Score: " + score);
        clickNum = clickNum == clickTextPool.Length - 1 ? 0 : clickNum + 1;
        ChangeScoreText(score);
    }

    public void ChangeScoreText(float score) {
        if(score >= 100 && score < 1000){
            scoreText.text = (System.Int32)(score / 10) + "h" + " $";
        }
        else if(score >= 1000){
            scoreText.text = (System.Int32)(score / 100) + "th" + " $";
        }
        else{
            scoreText.text = score + " $";
        }
    }
    public void openShopPanel() {
        // Включение или выключение панели
        if (shopPanel.activeSelf) {
            shopPanel.SetActive(false);
            shopButtonText.text = "Магазин";
        }
        else {
            shopPanel.SetActive(true);
            shopButtonText.text = "Закрити";
        }
    }
    public float takeTotalScore() {
        return score;
    }
    ///<summary>
    ///Меняет значения очков игрока.
    ///</summary>
    public void changeScore(float num, bool type) {
        if(type) {
            score += num;
        }
        else {
            score -= num;
        }
    }
    public void upgradeDopScore() {
        dopScore += 1;
    }
    public void upgradeDamage() {
        damage += 1;
    }
    public void upgradeCritChance (){
        critChance -= 2;
        Debug.Log($"Crit chance: {critChance}");
    }
    public void activateBonus(bool active) {
        activeBonus = active;
    }
}
