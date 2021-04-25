using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class PowerUpUI : MonoBehaviour
{
    public PowerUpEvent OnPowerupEvent = new PowerUpEvent();
    
    public Image[] powerUpCards = new Image[3];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void ChangeImage(Sprite card, int index) {
        powerUpCards[index].sprite = card;
    }
}

public class PowerUpEvent : UnityEvent<Sprite, int> {}
