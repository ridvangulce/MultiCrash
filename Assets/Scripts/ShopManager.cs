using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ShopManager : MonoBehaviour
{
    public GameObject[] carModels;
    public int currentCarIndex = 0;
    public CarBlueprint[] cars;
    public Button priceButton, playButton;

    private void Update()
    {
        UpdateUI();
    }

    void Start()
    {
        foreach (CarBlueprint car in cars)
        {
            if (car.price == 0)
            {
                car.isUnlocked = true;
            }
            else
            {
                car.isUnlocked = PlayerPrefs.GetInt(car.name, 0) == 0 ? false : true;
            }
        }


        currentCarIndex = PlayerPrefs.GetInt("SelectedCar", 0);
        foreach (GameObject car in carModels)
        {
            car.SetActive(false);
        }

        carModels[currentCarIndex].SetActive(true);
    }

    public void ChangeNext()
    {
        carModels[currentCarIndex].SetActive(false);
        currentCarIndex++;
        if (currentCarIndex == carModels.Length)
        {
            currentCarIndex = 0;
        }

        carModels[currentCarIndex].SetActive(true);
        CarBlueprint c = cars[currentCarIndex];
        if (!c.isUnlocked)
        {
            return;
        }

        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }

    public void ChangePrevious()
    {
        carModels[currentCarIndex].SetActive(false);
        currentCarIndex--;
        if (currentCarIndex == -1)
        {
            currentCarIndex = carModels.Length - 1;
        }

        carModels[currentCarIndex].SetActive(true);
        CarBlueprint c = cars[currentCarIndex];
        if (!c.isUnlocked)
        {
            return;
        }
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
    }

    public void UnlockCar()
    {
        CarBlueprint c = cars[currentCarIndex];
        PlayerPrefs.SetInt(c.name, 1);
        PlayerPrefs.SetInt("SelectedCar", currentCarIndex);
        c.isUnlocked = true;
        PlayerPrefs.SetInt("NumberOfCoins", PlayerPrefs.GetInt("NumberOfCoins", 0) - c.price);
    }

    void UpdateUI()
    {
        CarBlueprint c = cars[currentCarIndex];
        if (c.isUnlocked)
        {
            playButton.gameObject.SetActive(true);
            priceButton.gameObject.SetActive(false);
        }
        else
        {
            playButton.gameObject.SetActive(false);
            priceButton.gameObject.SetActive(true);
            priceButton.GetComponentInChildren<TextMeshProUGUI>().text = c.price.ToString();
            if (c.price < PlayerPrefs.GetInt("NumberOfCoins", 0))
            {
                priceButton.interactable = true;
            }
            else
            {
                priceButton.interactable = false;
            }
        }
    }
}