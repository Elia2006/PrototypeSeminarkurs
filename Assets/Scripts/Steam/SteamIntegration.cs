using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SteamIntegration : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        try
        {
            Steamworks.SteamClient.Init(3719970);
            PrintYourName();
            ClearAchievementStatus("ACH_JUMP_ONCE");
            IsThisAchievementUnlocked("ACH_JUMP_ONCE");
            UnlockAchievements("ACH_JUMP_ONCE");
        }
        catch (System.Exception e)
        {
            Debug.Log(e);
        }

    }

    private void PrintYourName()
    {
        Debug.Log(Steamworks.SteamClient.Name);
    }

    private void PrintFriends()
    {
        foreach (var friend in Steamworks.SteamFriends.GetFriends())
        {
            Debug.Log(friend.Name);
        }
    }

    // Update is called once per frame
    void Update()
    {
        Steamworks.SteamClient.RunCallbacks();
    }

    private void OnApplicationQuit()
    {
        Steamworks.SteamClient.Shutdown();
    }

    #region Achievements

    public bool IsThisAchievementUnlocked(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);

        Debug.Log("Achievement " + id + " status: "+ ach.State);
        return ach.State;
    }

    public void UnlockAchievements(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Trigger();
        Debug.Log("Achievement " + id + " unlocked");
    }

    public void ClearAchievementStatus(string id)
    {
        var ach = new Steamworks.Data.Achievement(id);
        ach.Clear();

        Debug.Log("Achievement " + id + " cleared");
    }

    #endregion
}
