using UnityEngine;

public class Target : MonoBehaviour
{
   public event OnTargetHitDelegate OnTargetHitEvent;

   private void OnCollisionEnter(Collision col)
   {
      if (col.gameObject.tag == "Projectile")
      {
         GameManager.Instance.AddScore(1);

         if (OnTargetHitEvent != null)
         {
            OnTargetHitEvent.Invoke();
         }
      }
   }
}