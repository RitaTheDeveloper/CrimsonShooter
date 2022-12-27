using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    public virtual AbilityData MyAbilityData
    {
        get; set;
    }
    public virtual void ActivateAbility()
    {
        Debug.Log("Активирована способность");
    }
}
