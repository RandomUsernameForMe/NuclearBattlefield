using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PartyHolder : MonoBehaviour
{
    public Party party;
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(this);
    }
}
