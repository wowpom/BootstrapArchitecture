using System;
using UnityEngine;

namespace CodeBase.Data
{
    [Serializable]
    public class PlayerProgress
    {
         public WorldData WorldData;

         public PlayerProgress(string initialScene)
         {
             WorldData = new WorldData(initialScene);
         }
    }
}
