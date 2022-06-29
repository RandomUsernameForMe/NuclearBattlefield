using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UseGeneratedEnemiesButton : MonoBehaviour
{
    public Party nextEnemyGroup;

    private void Start()
    {
        gameObject.GetComponent<Button>().interactable = false;
    }
    public void LoadNextLevel()
    {
        var obj = GameObject.Find("LevelInfo");
        var levelInfo = obj.GetComponent<LevelInfo>();
        if (levelInfo.controlGroup)
        {
            var partyholder = obj.GetComponent<PartyHolder>();
            partyholder.party = obj.GetComponent<ManMadeGroups>().GetPartyFromPrefab(levelInfo.currLevel-1);
            partyholder.party.transform.position = new Vector3(5, -5, 0);
            DontDestroyOnLoad(partyholder.party);
            Destroy(nextEnemyGroup);
        }
        else
        {
            obj.GetComponent<PartyHolder>().party = nextEnemyGroup;            
        }
        obj.GetComponent<LevelManager>().LoadNextLevel();


    }
}
