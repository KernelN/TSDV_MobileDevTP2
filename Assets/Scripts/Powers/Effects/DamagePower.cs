using System.Collections.Generic;
using UnityEngine;

namespace TheWasteland.Gameplay.Powers
{
    public class DamagePower : PowerDecorator
    {
        //[Header("Set Values")]
        //[Header("Runtime Values")]
        DamageData data;
        List<IHittable> targets;
        List<int> targetHits;
        float timer;
        bool hasLoops;

        //Unity Events

        //Methods
        public DamagePower(PowerComponent component) : base(component) { }
        public override void Set(PowerData powerData)
        {
            data = powerData as DamageData;
            hasLoops = data.dmgLoops > 0;
            
            if (hasLoops)
            {
                targets = new List<IHittable>();
                targetHits = new List<int>();
            }
        }
        public override void Update(float dt)
        {
            if(!hasLoops) return;
            if (timer > 0)
            {
                timer -= dt;
                return;
            }
            
            for (int i = 0; i < targets.Count; i++)
            {
                do
                {
                    //If target died, remove it
                    if (targets[i] == null)
                    {
                        targets.RemoveAt(i);
                        targetHits.RemoveAt(i);
                        continue;
                    }

                    //Hit target and increase counter by 1
                    targets[i].GetHitted(data.dmg);
                    targetHits[i]++;   
                    
                    //If target has been hit enough times (with dmg over time), remove it
                    if (targetHits[i] >= data.dmgLoops)
                    {
                        targets.RemoveAt(i);
                        targetHits.RemoveAt(i);
                    }
                } while (targets.Count > i && targetHits[i] >= data.dmgLoops);
            }
        }
        public override void Cast(Transform target)
        {
            if (!target.TryGetComponent(out IHittable hit)) return;
            
            hit.GetHitted(data.dmg);

            if (!hasLoops) return;
            timer = data.castCooldown;

            //If hitted again, reset counter, else, add to list
            if (targets.Contains(hit))
            {
                int index = targets.IndexOf(hit);
                targetHits[index] = 1;
            }
            else
            {
                targets.Add(hit);
                targetHits.Add(1);
            }
        }
    }
}