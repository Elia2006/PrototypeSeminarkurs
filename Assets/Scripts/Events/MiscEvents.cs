using System;

public class MiscEvents
{
    public event Action onCoinCollected;
    public void CoinCollected() 
    {
        if (onCoinCollected != null) 
        {
            onCoinCollected();
        }
    }

    public event Action onItemPickup;

    public void ItemPickedUp()
    {
        if (onItemPickup != null) { 
            onItemPickup();
        }
    }

    public event Action onDoorOpen;

    public void DoorOpened()
    {
        if (onDoorOpen != null)
        {
            onDoorOpen();
        }
    }

    public event Action onBossDeath;
    
    public void BossDeath()
    {
        if (onBossDeath != null)
        {
            onBossDeath();
        }
    }

    public event Action onGemCollected;
    public void GemCollected() 
    {
        if (onGemCollected != null) 
        {
            onGemCollected();
        }
    }
}
