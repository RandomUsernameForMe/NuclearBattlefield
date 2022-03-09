using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingSystem
{
    /// <summary>
    /// Returns available target positions for an ability. 
    /// </summary>
    /// <param name="pos">Grid coordinates plus a bool if ally or enemy side</param>
    /// <param name="is_enemy">If the one asking is enemy or frieds</param>
    /// <returns></returns>
    public static List<(int, int, bool)> PickViableTargets(List<StatusParameter> pos, bool is_enemy)
    {
        List<(int, int, bool)> list = new List<(int, int, bool)> { (0, 0, false), (1, 0, false), (0, 1, false), (1, 1, false), (0, 0, true), (1, 0, true), (0, 1, true), (1, 1, true) };
        foreach (var item in pos)
        {
            switch (item)
            {
                case StatusParameter.Close:
                    list.Remove((0, 0, false));
                    list.Remove((0, 1, false));
                    list.Remove((1, 0, true));
                    list.Remove((1, 1, true));
                    break;
                case StatusParameter.Far:
                    list.Remove((1, 0, false));
                    list.Remove((1, 1, false));
                    list.Remove((0, 0, true));
                    list.Remove((0, 1, true));
                    break;
                case StatusParameter.Ally:
                    list.Remove((0, 0, !is_enemy));
                    list.Remove((0, 1, !is_enemy));
                    list.Remove((1, 0, !is_enemy));
                    list.Remove((1, 1, !is_enemy));
                    break;
                case StatusParameter.Enemy:
                    list.Remove((0, 0, is_enemy));
                    list.Remove((0, 1, is_enemy));
                    list.Remove((1, 0, is_enemy));
                    list.Remove((1, 1, is_enemy));
                    break;
            }
        }
        return list;
    }
}
