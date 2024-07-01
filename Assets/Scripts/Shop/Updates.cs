using System.Collections;
using System.Collections.Generic;
using System.Text.RegularExpressions;
using UnityEngine;
using UnityEngine.UI;

public class Updates : MonoBehaviour
{
    // Start is called before the first frame update
    public Clicker clicker;
    [Header("Магазин")]
    public Text[] upgradesTxt;
    public Button[] buttons;
    public float[] prices = {50, 50, 100};

    private void Start() {
        for (int i = 0; i < buttons.Length; i++) {
            upgradesTxt[i].text += $"{prices[i]} $";
        }
    }
    public void startInteractable() {
        interactableUpgrades();
    }

    private void Update() {
        interactableUpgrades();
    }

    private void interactableUpgrades() {
        for (int i = 0; i < buttons.Length; i++) {
            if (clicker.takeTotalScore() <= prices[i]) {
                buttons[i].interactable = false;
            }
            else {
                buttons[i].interactable = true;
            }
        }
    }
    public void Upgrade(int index) {
        switch(index) {
        /// Удосконалення кліку
            case 0:
                makeUpgrade(index, 2f, "damage");
            break;
        /// Удосконалення додаткових грошей
            case 1:
                makeUpgrade(index, 2f, "dopScore");
            break;
        /// Удосконалення кріт. шансу.
            case 2:
                makeUpgrade(index, 2f, "crit");
            break;
        }
    }
    private void makeUpgrade(int index, float upPrice, string chose){
        clicker.changeScore(prices[index], false);
        prices[index] *= upPrice;
        interactableUpgrades();
        upgradesTxt[index].text += $" {prices[index]}";
        switch (chose) {
            case "crit":
                clicker.upgradeCritChance();
            break;
            case "damage":
                clicker.upgradeDamage();
            break;
            case "dopScore":
                clicker.upgradeDopScore();
            break;
        }
    }
}
