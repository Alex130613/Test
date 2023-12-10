using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject Setting;
    public GameObject Award;
    private int Tickets;
    private bool Music;
    private bool Sounds;
    public GameObject OffS;
    public GameObject OffM;
    private GameObject Camera;
    private DateTime Time;
    private int Day;
    public Slider KolDays;
    public Text TicketText;
    public GameObject[] ButtonAward;
    public Text SliderText;
    public int level=1;
    public GameObject NewIm;
    public GameObject AwardCanvas;
    public Text DayText;
    public Text KolAw;
    void Start()
    {
        LoadGame();
        Camera=GameObject.FindWithTag("MainCamera");
        Camera.GetComponent<AudioSource>().mute=!Music;
         GetComponent<AudioSource>().mute=!Sounds;
       TicketText.text=Tickets.ToString(); 

        double DaySpan=1;
        if (Time!=null) {
            DateTime daynow=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
        var timeSpan=daynow-Time;
            DaySpan= timeSpan.TotalDays;
        if(DaySpan>1||DaySpan<0){Day=0;
                DaySpan=1;
        }
        }
        NewIm.active=(DaySpan==1);
    }
    public bool SetTickets(int t) {
        if (Tickets+t<0)return false;
                else { 
        Tickets+=t;
        TicketText.text=Tickets.ToString();
        return true;}
    }


    public void OpenSetting() { 
    Setting.active=true;
        OffM.active=!Music;
        OffS.active=!Sounds;
    }
    public void SetMusic() { 
        OffM.active=Music;
        Camera.GetComponent<AudioSource>().mute=Music;
    Music=!Music;
    }
    public void SetSound() {     
        OffS.active=Sounds;
        GetComponent<AudioSource>().mute=Sounds;
        Sounds=!Sounds;
    }
    public void closeSetting()
{Setting.active=false;
    }
    
    
    
    public void OpenAward() { 
    Award.active=true;
    UpdateAward();    
    }
    public void UpdateAward() { 
        
        double DaySpan=1;
        if (Time!=null) {
            DateTime daynow=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
        var timeSpan=daynow-Time;
            DaySpan= timeSpan.TotalDays;
        Debug.Log("Day:"+daynow.ToString()+"Day1:"+Time.ToString());
        if(DaySpan>1||DaySpan<0){Day=0;
                DaySpan=1;
        }
        }
        if (Day + DaySpan == 8) Day = 0;
    for (int i = 0; i < ButtonAward.Length; ++i) {
       if(i==Day&&(DaySpan == 1||Day==0))ButtonAward[i].GetComponent<Button>().interactable=true; 
       else ButtonAward[i].GetComponent<Button>().interactable=false;
        } 
    KolDays.value=Day+(int)DaySpan;
        SliderText.text=(Day+DaySpan).ToString()+"/7";
        NewIm.active=(DaySpan==1);
        if (Day == 6&&DaySpan==1)
        {
            AwardController AC=new AwardController();
            AC.Ticket = 20;
            AC.day = 7;
            TakeAward(AC);
        }
    }

    public void TakeAward(AwardController AC) {
        AwardCanvas.active = true;

        DayText.text ="DAY"+ (Day+1).ToString();
        KolAw.text ="x"+ AC.Ticket.ToString();
        Tickets+=AC.Ticket;
        TicketText.text=Tickets.ToString();
        Time=new DateTime(DateTime.Now.Year,DateTime.Now.Month,DateTime.Now.Day);
        Day++;
        UpdateAward();
        Award.active = false;
    }
    
    
    
    public void SaveGame()
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Create(Application.persistentDataPath + "/MainDate.dat");
            SaveData data = new SaveData();
            data.Tickets = Tickets;
            data.Music = Music;
            data.Sound = Sounds;
        data.Time=Time;
        data.Day=Day;
        data.level=level;
        bf.Serialize(file, data);
            file.Close();
        }
    public void LoadGame()
        {
        if (File.Exists(Application.persistentDataPath + "/MainDate.dat"))
        {
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/MainDate.dat", FileMode.Open);
            SaveData data = (SaveData)bf.Deserialize(file);
            file.Close();
            Tickets = data.Tickets;
            Music = data.Music;
            Sounds = data.Sound;
            Time = data.Time;
            Day = data.Day;
            level = data.level;
        }
        else
        {
            level=1;
            Tickets = 0;
            Day=0;
            Music=Sounds=true;
        }
    }
    void OnDestroy() { 
    SaveGame();}
}
 [Serializable]
    class SaveData
    {
        public int Tickets;
        public bool Sound;
        public bool Music;
    public  DateTime Time;
    public int Day;
    public int level;
    }