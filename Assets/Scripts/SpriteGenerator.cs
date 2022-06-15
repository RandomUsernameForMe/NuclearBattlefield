using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpriteGenerator : MonoBehaviour
{
    public List<Material> sprites;
    public List<Color> colors;

    public void AssignSpritesToEnemyParty()
    {
        var party = GameObject.Find("BattleManager").GetComponent<BattleManager>().enemyParty;
        AssignRandomSpritesToParty(party);
    }

    public void AssignRandomSpritesToParty(Party party)
    {
        var usedNums = new List<int>();
        foreach (var item in party.party)
        {
            var spriteNum = UnityEngine.Random.Range(0, sprites.Count);
            while (usedNums.Contains(spriteNum))
            {
                spriteNum = UnityEngine.Random.Range(0, sprites.Count);
            }
            usedNums.Add(spriteNum);
            var color = UnityEngine.Random.ColorHSV();
            color.a = 1;
            if (color.r + color.b + color.g < 1.5)
            {
                color.r = 0.3f;
                color.g = 0.3f;
                color.b = 0.3f;
            }
            var creature = item.GetComponentInChildren<Creature>();
            var sprite = sprites[spriteNum];
            creature.GetComponentInChildren<MeshRenderer>().material = sprite;
            creature.GetComponentInChildren<MeshRenderer>().material.color = color;
        }
    }
}
