using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelController : MonoBehaviour
{
    public int number;
    public Text TextNumber;
    public GameObject Image;
    public MenuController MC;
    public RectTransform Content;
    public ScrollRect scrollRect;
    private bool Action=true;
    // Start is called before the first frame update
    void Start()
    {
        if(MC.level>=number){Image.active=false;
        TextNumber.text=number.ToString();
        }
        else {TextNumber.text="";
            Image.active=true;
            GetComponent<Button>().interactable=false;
        }
    }
    void Update() {
        if(MC.level==number){
        Image.active=false;
        TextNumber.text=number.ToString();
            GetComponent<Button>().interactable=true;
           if (Action)
        {
        Vector2 Pos= (Vector2)scrollRect.transform.InverseTransformPoint(0,Content.position.y,0)-(Vector2)scrollRect.transform.InverseTransformPoint(0,GetComponent<RectTransform>().position.y,0);
        Vector2 Sped=(Content.anchoredPosition-Pos)/10;
                if(Sped.y<1)Sped.y=1;
                if(Content.anchoredPosition.y>Pos.y) Content.anchoredPosition-=Sped;
                else Action=false;
        }
    } 
    }    
    public void PlayLevel() { 
        if (MC.level==number) MC.level++;
    } 
}
