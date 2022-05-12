using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Query
{
    public QueryType type;
    public Dictionary<QueryParameter,double> parameters;
    public List<string> descs = new List<string>();
    public Dictionary<QueryParameter,ComponentBuilder> effects = new Dictionary<QueryParameter, ComponentBuilder>();

    public static Query question = new Query(QueryType.Question);

    public Query(QueryType type)
    {
        this.type = type;
        parameters = new Dictionary<QueryParameter, double>();
    }

    public Query(Query other)
    {
        this.type = other.type;
        parameters = new Dictionary<QueryParameter, double>(other.parameters);
        effects = new Dictionary<QueryParameter, ComponentBuilder>(other.effects);
        descs = new List<string>(other.descs);
    }

    public void Clear()
    {
        parameters.Clear();
        descs.Clear();
        effects.Clear();
    }

    public void Add(QueryParameter str, double val)
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

    public void Add(QueryParameter ind,ComponentBuilder eff)
    {
        if (!effects.ContainsKey(ind))
        {
            effects.Add(ind,eff);
        }
    }

}
