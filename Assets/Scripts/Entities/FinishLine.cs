using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FinishLine : MonoBehaviour
{
    public static event Action playerCrossedFinishLine;

    private void OnTriggerEnter2D(Collider2D col){
        playerCrossedFinishLine?.Invoke();
        
    }
}
