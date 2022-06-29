using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManMadeGroups : MonoBehaviour
{
    public List<Party> list;

    public Party GetPartyFromPrefab(int i)
    {
        return Instantiate(list[i]);
    }
}
