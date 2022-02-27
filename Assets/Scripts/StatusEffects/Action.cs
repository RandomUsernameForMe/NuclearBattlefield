using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action
{
    public ID id;
    public Dictionary<Ind,double> prms;
    public List<string> descs = new List<string>();
    public Dictionary<Ind,Builder> effects = new Dictionary<Ind, Builder>();

    public Action(ID ID)
    {
        this.id = ID;
        prms = new Dictionary<Ind, double>();
    }

    public void Add(Ind str, double val)
    {
        if (prms.ContainsKey(str) )
        {
            prms[str] += val;
        }
        else
        {
            prms.Add(str, val);
        }        
    }

    public void Add(string str)
    {
        if (!descs.Contains(str))
        {
            descs.Add(str);
        }
    }

    public void Add(Ind ind,Builder eff)
    {
        if (!effects.ContainsKey(ind))
        {
            effects.Add(ind,eff);
        }
    }

}
