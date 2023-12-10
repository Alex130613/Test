using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class ShopCotrollerLS : MonoBehaviour
{
    public string Name;
    public Text NameText;
    public int Price;
    public Text PriceText;
    public int Level;
    public Text LevelText;
    public GameObject LS;
    public GameObject CloseIm;
    public bool IsBuy=false;
    public GameObject ImBuy;
    public MenuController MC;
    public GameObject PriceField;
    public Button ButtonBuy;
    // Start is called before the first frame update
    void Start()
    {
        LoadGame();
        NameText.text = Name;
        UpdateStat();
    }

    public void Buy() {
        if (MC.SetTickets(-Price))
        {
            IsBuy = true;
            UpdateStat();
            SaveGame();
        }
    }
    void Update() { if (MC.level - 1 >= Level)UpdateStat(); }
    private void UpdateStat() {
        if (MC.level - 1 < Level)
        {
            CloseIm.active = true;
            LS.active = false;
            LevelText.text = "LV. " + Level.ToString();
            ButtonBuy.interactable = false;
        }
        else
        {
            CloseIm.active = false;
            LS.active = true;
            LevelText.text = "";
            ButtonBuy.interactable = true;
        ButtonBuy.interactable = !IsBuy;
        }
        ImBuy.active = IsBuy;
        PriceText.text = Price.ToString();
        PriceField.active = !IsBuy;
    } 

    public void SaveGame()
    {
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/"+Name+".dat");
        SaveBuy data = new SaveBuy();
        data.IsBuy = IsBuy;
        bf.Serialize(file, data);
        file.Close();
    }
    public void LoadGame()
    {
        if (File.Exists(Application.persistentDataPath + "/" + Name + ".dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/" + Name + ".dat", FileMode.Open);
            SaveBuy data = (SaveBuy)bf.Deserialize(file);
            file.Close();
            IsBuy = data.IsBuy;
        }
    }

}
[Serializable]
class SaveBuy
{
    public bool IsBuy;
}
