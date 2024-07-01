using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bonus : MonoBehaviour
{
    public GameObject bonusPanel;
    public Text bonusBtnText;
    public Text doubleBonusBtnText;
    public Text dblActivateBtnText;
    public Text[] bonusesTxt;
    public Button bonusBtn;
    public Button[] bonusesBtn;
    public Clicker clicker;
    [SerializeField]
    private float doublePrice;
    public float[] prices = {100};
    private bool activeBonus = false;
    private int numOfDblBonus = 0;
    private int[] numOfBonuses = {0};
    private string[] txtForButtons = {"Бонус X2 за клік\nЦіна: "};
    public void Update() {
        intractableDoubleClick();
    }
    ///<summary>
    ///Функция - відкриває та закриває панель бонусів.
    ///</summary>
    public void openBonusPanel() {
        if (bonusPanel.activeSelf) {
            bonusBtnText.text = "Бонуси";
            bonusPanel.SetActive(false);
        }
        else {
            bonusBtnText.text = "Закрити";
            bonusPanel.SetActive(true);
        }
    }
    ///<summary>
    ///Функция - перевіряє чи користувач може придбати бонус.
    ///</summary>
    private void intractableDoubleClick() {
        if (clicker.takeTotalScore() > prices[0]) {
            bonusBtn.interactable = true;
        }
        else {
            bonusBtn.interactable = false;
        }
    }
    public void choseBonus(int id) {
        switch(id) {
            case 0:
                Debug.Log("Click: " + prices[0]);
                bonusShop(id, 3, 2);
            break;
        }
    }
    private void bonusShop(int index, float upPrice, int idDopTxt = 0) {
        numOfBonuses[index] ++;
        bonusesTxt[idDopTxt].text = $"{numOfBonuses[index]}";
        Debug.Log("Buy bonus !");
        clicker.changeScore(prices[index], false);
        prices[index] *= upPrice;
        bonusesTxt[index].text = txtForButtons[index] + $"{prices[index]} $";
    }
    ///<summary>
    ///Функция - удваює кількість очків за клік.
    ///</summary>
    public void doubleBonus() {
        numOfDblBonus ++;
        dblActivateBtnText.text = $"{numOfDblBonus}";
        Debug.Log("Buy bonus !");
        clicker.changeScore(doublePrice, false);
        doublePrice *= 3;
        doubleBonusBtnText.text = $"Бонус X2 за клік\nЦіна {doublePrice} $";
    }
    ///<summary>
    ///Функция - активує бонус і провіряє чи не закінчився попередній
    ///<para>і чи у гравця взагалі вони є.</para>
    ///</summary>
    public void activateDblBonus() {
        if (activeBonus == false && numOfDblBonus != 0) {
            Debug.Log("Activate bonus !");
            clicker.activateBonus(true);
            activeBonus = true;
            numOfDblBonus --;
            dblActivateBtnText.text = $"{numOfDblBonus}";
        }
    }
    ///<summary>
    ///Функция - активує сам бонус в класі Bonus.
    ///</summary>
    public void activateBonus(bool active) {
        activeBonus = active;
        if (!activeBonus) {
            clicker.activateBonus(false);
        }
    }
}
