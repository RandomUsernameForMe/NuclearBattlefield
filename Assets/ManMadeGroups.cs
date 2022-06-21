using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManMadeGroups : MonoBehaviour
{
    public List<Party> list;

    public Party GetParty(int i)
    {
        return list[i];
    }
}
