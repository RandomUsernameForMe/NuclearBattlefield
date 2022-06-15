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
        var generator = GetComponent<SpriteGenerator>();
        generator.AssignRandomSpritesToParty(nextEnemyGroup);

        obj.GetComponent<PartyHolder>().party = nextEnemyGroup;
        obj.GetComponent<LevelManager>().LoadLevel(1,1);
    }
}
