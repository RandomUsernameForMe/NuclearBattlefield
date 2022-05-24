using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;


/// <summary>
/// After every scene transition, there is a possibility of story screens showing character monologues.
/// Each screen has a specific condition ans appears only once if condition is met.
/// </summary>
public class StoryScreens : MonoBehaviour, IPointerClickHandler
{
    List<Screen> list = new List<Screen>();
    Queue<Texture2D> toBePlayed = new Queue<Texture2D>();

    // Im ashamed of this.
    // These could be automatically generated from text.
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
        list.Add(new Screen(start,0));

        // Regular texts
        list.Add(new Screen(first1, 1));
        list.Add(new Screen(first2, 1));
        list.Add(new Screen(first3, 1));
        list.Add(new Screen(second1, 2));
        list.Add(new Screen(second2, 2));
        list.Add(new Screen(third1, 3));

        // Death related
        list.Add(new Screen(special1, QueryParameter.Dead,1,"Sonnie"));
        list.Add(new Screen(special2, QueryParameter.Dead, 1, "Will"));
        list.Add(new Screen(special3, QueryParameter.Dead, 1, "Moon"));

        // Destroyer related
        list.Add(new Screen(special4, new ComponentCondition<Destroyer>("Moon"), "Moon"));
        list.Add(new Screen(special5, new ComponentCondition<Destroyer>("Moon"), "Moon"));
        list.Add(new Screen(destroy1, QueryParameter.DestroyerUsed, 1,"Moon"));
        list.Add(new Screen(destroy2, QueryParameter.DestroyerUsed, 1, "Moon"));
        list.Add(new Screen(destroy3, QueryParameter.DestroyerUsed, 2, "Moon"));
        list.Add(new Screen(destroy4, QueryParameter.DestroyerUsed, 2, "Moon"));
        PreparePossibleImages();
    }

    private void OnEnable()
    {
        LevelManager.OnBattleSceneLoaded += PrepareScreens;
    }

    public void PrepareScreens()
    {
        gameObject.SetActive(true);
        PreparePossibleImages();
    }

    /// <summary>
    /// On pointer click I want to either switch to a new image or hid the window in case there are not any left.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if (toBePlayed.Count == 0)
        {
            gameObject.SetActive(false);
            GetComponent<Image>().sprite = null;
        }
        else
        {
            var t = toBePlayed.Dequeue();
            GetComponent<Image>().sprite = Sprite.Create(t, new Rect(0.0f, 0.0f, t.width, t.height), new Vector2(0.5f, 0.5f), 100.0f);
        }
    }

    /// <summary>
    /// Each image has a corresponding condition and is prepared to show if its met.
    /// </summary>
    public void PreparePossibleImages()
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