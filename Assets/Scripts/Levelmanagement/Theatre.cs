using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


public class Theatre : MonoBehaviour, IPointerClickHandler
{
    List<Slide> list = new List<Slide>();
    Queue<Texture2D> toBePlayed = new Queue<Texture2D>();
    public Texture2D start;
    public Texture2D first1;
    public Texture2D first2;
    public Texture2D first3;
    public Texture2D second1;
    public Texture2D second2;
    public Texture2D third1;
    public Texture2D third2;
    public Texture2D special1;
    public Texture2D special2;
    public Texture2D special3;
    public Texture2D special4;

    public Texture2D special5;
    public Texture2D destroy1;
    public Texture2D destroy2;
    public Texture2D destroy3;
    public Texture2D destroy4;

    public void Start()
    {
        list.Add(new Slide(start,0));

        // Regular texts
        list.Add(new Slide(first1, 1));
        list.Add(new Slide(first2, 1));
        list.Add(new Slide(first3, 1));
        list.Add(new Slide(second1, 2));
        list.Add(new Slide(second2, 2));
        list.Add(new Slide(third1, 3));

        // death related
        list.Add(new Slide(special1, Ind.Dead,1,"Sonnie"));
        list.Add(new Slide(special2, Ind.Dead, 1, "Will"));
        list.Add(new Slide(special3, Ind.Dead, 1, "Moon"));

        // Destroyer related
        list.Add(new Slide(special4, typeof(Destroyer),"Moon"));
        list.Add(new Slide(special5, typeof(Destroyer), "Moon"));
        list.Add(new Slide(destroy1, Ind.DestroyerUsed, 1,"Moon"));
        list.Add(new Slide(destroy2, Ind.DestroyerUsed, 1, "Moon"));
        list.Add(new Slide(destroy3, Ind.DestroyerUsed, 2, "Moon"));
        list.Add(new Slide(destroy4, Ind.DestroyerUsed, 2, "Moon"));
        PlayAllPossibleImages();
    }

    private void OnEnable()
    {
        LevelManager.OnBattleLoaded += Prep;
    }

    public void Prep()
    {
        MakeTiny(false);
        gameObject.SetActive(true);
        PlayAllPossibleImages();
    }

    void Update()
    {
    
    }


    public void OnPointerClick(PointerEventData eventData)
    {
        if (toBePlayed.Count == 0)
        {
            MakeTiny(true);
            GetComponent<Image>().sprite = null;
        }
        else
        {
            var t = toBePlayed.Dequeue();
            GetComponent<Image>().sprite = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    private void MakeTiny(bool v)
    {
        var sc = gameObject.transform.parent.GetComponent<Canvas>();
        if (v)
        {
            gameObject.SetActive(false);

        }
        else
        {
            gameObject.SetActive(true);
        }
    }

    public void PlayAllPossibleImages()
    {
        gameObject.SetActive(true);
        foreach (var item in list)
        {
            if (item.Possible())
            {
                toBePlayed.Enqueue(item.source);
                item.played = true;
            }
        }
        if (toBePlayed.Count != 0)
        {
            var t = toBePlayed.Dequeue();
            GetComponent<Image>().sprite = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
        
    }
}
public class Slide
{
    public Condition cond;
    public bool played;
    public Texture2D source;

    public Slide(Texture2D v, Type t, string name)
    {
        cond = new HasStatus(t, name);
        source = v;
        played = false;
    }

    public Slide(Texture2D v, Ind i, double val, string name)
    {
        cond = new HasValue(i, val, name);
        source = v;
    }

    public Slide(Texture2D v, int level)
    {
        cond = new IsLevel( level);
        source = v;
    }

    public bool Possible()
    {
        return (!played && cond.Passed());
    }
}

public abstract class Condition
{
    public string name;
    public abstract bool Passed(); 
}

public class HasStatus : Condition
{
    Type t;

    public HasStatus(Type t, string name)
    {
        this.name = name;
        this.t = t;
    }
    override public bool Passed()
    {
        var c = GameObject.Find(name);
        if (c!= null)
        {
            var effects = c.GetComponentsInChildren<Module>();
            return Upgrader.Contains(effects, t);
        }
        else return false;
        
    }
}

public class HasValue : Condition
{
    public Ind i;
    public double val;

    public HasValue(Ind i, double val, string name)
    {
        this.i = i;
        this.val = val;
        this.name = name;
    }

    public override bool Passed()
    {
        var c = GameObject.Find(name);


        if (c != null)
        {
            var g = c.GetComponent<Creature>();
            var a = new Action(ID.Query);
            a.Add(i, 0);
            a = g.ProcessAction(a);
            return (a.prms[i] == val);
        }
        else return false;
    
    }
}

public class IsLevel : Condition
{
    int level;
    public IsLevel(int level)
    {
        this.level = level;
    }

    public override bool Passed()
    {
        var i = GameObject.Find("LevelInfo").GetComponent<LevelInfo>();
        return (i.currLevel == level && i.campfire == false);
    }
}


