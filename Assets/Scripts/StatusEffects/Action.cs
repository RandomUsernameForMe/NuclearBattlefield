using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Query
{
    public QueryType type;
    public Dictionary<StatusParameter,double> parameters;
    public List<string> descs = new List<string>();
    public Dictionary<StatusParameter,StatusBuilder> effects = new Dictionary<StatusParameter, StatusBuilder>();

    public Query(QueryType type)
    {
        this.type = type;
        parameters = new Dictionary<StatusParameter, double>();
    }

    public void Add(StatusParameter str, double val)
    {
        if (parameters.ContainsKey(str) )
        {
            parameters[str] += val;
        }
        else
        {
            parameters.Add(str, val);
        }        
    }

    public void Add(string str)
    {
        if (!descs.Contains(str))
        {
            descs.Add(str);
        }
    }

    public void Add(StatusParameter ind,StatusBuilder eff)
    {
        if (!effects.ContainsKey(ind))
        {
            effects.Add(ind,eff);
        }
    }

}
