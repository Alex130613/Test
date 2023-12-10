using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ShopControllerTicket : MonoBehaviour
{
    public float Money;
    public Text MoneyText;
    public int Tickets;
    public Text TextTickets;
    public MenuController MC;
    // Start is called before the first frame update
    void Start()
    {
        MoneyText.text=Money.ToString()+"$";
        TextTickets.text="x"+Tickets.ToString();
    }

    public void Buy() { 
        MC.SetTickets(Tickets);
    }
}
