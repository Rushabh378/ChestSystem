using System.Collections;
using UnityEngine;

namespace ChestSystem.Currancy
{
    public class Currancy : GenericSingleton<Currancy>
    {
        [SerializeReference]public ICurrancy coins = new Coins();
        [SerializeReference]public ICurrancy gems = new Gems();
    }
}