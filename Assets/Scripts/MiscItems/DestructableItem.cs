using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructableItem : MonoBehaviour, IDamageReciever
{
    public void OnRecievedDamage()
    {
        Debug.Log("Is Recieving Damage");
    }
}
