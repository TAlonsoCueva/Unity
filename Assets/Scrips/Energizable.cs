using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Energizable : MonoBehaviour
{
    public float maxEnergy;
    public float currentEnergy;
    public float energyRegenerationBase;
    public float energyMaxRegeneration;
    public float energyRegeneration;

    public virtual bool CanRegenerateEnergy() 
    {
        return currentEnergy < maxEnergy;
    }
    public virtual void Update()
    {
        if (CanRegenerateEnergy())
        {
            energyRegeneration += energyRegenerationBase * Time.deltaTime;
            if (energyRegeneration >= energyMaxRegeneration)
                energyRegeneration = energyMaxRegeneration;
            currentEnergy += energyRegeneration * Time.deltaTime;
            if (currentEnergy >= maxEnergy)
                currentEnergy = maxEnergy;
            OnUpdateEnergy();
        }
    }
    public virtual void OnUpdateEnergy() 
    {
        //override en otra clase
    }
}
