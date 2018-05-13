using System.Collections.Generic;
using UnityEngine;

public class TargetManager : MonoBehaviour
{
   public GameObject TargetPrefab;
   public Target CurrentTarget;
   public List<Transform> Locations;

   private int _previousLocationIndex = 0;

   public void SpawnTarget()
   {
      CurrentTarget = Instantiate(TargetPrefab, Locations[_previousLocationIndex++].position, Quaternion.identity)
         .GetComponent<Target>();

      CurrentTarget.OnTargetHitEvent += CurrentTarget_OnTargetHitEvent;

      if (_previousLocationIndex == Locations.Count - 1)
      {
         _previousLocationIndex = 0;
      }
   }

   private void CurrentTarget_OnTargetHitEvent()
   {
      CurrentTarget.OnTargetHitEvent -= CurrentTarget_OnTargetHitEvent;
      Destroy(CurrentTarget.gameObject);
      SpawnTarget();
   }

   private void Start()
   {
      SpawnTarget();
   }
}