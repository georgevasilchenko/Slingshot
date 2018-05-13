using UnityEngine;

public class SlingShot : MonoBehaviour
{
   public LineRenderer[] HandlesLineRenderers;
   public Transform[] HandleAnchorTrnsforms;
   public DragHandle DragHandle;
   public Transform ReleasePointTransform;
   public Transform ProjectileSpawnTransform;
   public Transform AimerTransform;
   public GameObject ProjectilePrefab;
   public float StartPower = 0;

   private float[] LineLengths;

   public float GetVelocity()
   {
      return Vector3.Distance(DragHandle.transform.position, ReleasePointTransform.transform.position) * 2.5f;
   }

   public float GetDistance(float Vinit)
   {
      var g = Physics.gravity.y;
      var Vvert = Vinit * (Mathf.Sin(GetAngle() * Mathf.Deg2Rad));
      var Vhor = Vinit * (Mathf.Cos(GetAngle() * Mathf.Deg2Rad));
      var Tvert = (0 - Vvert) / g;
      var Thor = 2 * Tvert;
      var distance = Vhor * Thor;
      return distance;
   }

   public float GetHeight(float Vinit, int amountPoints, int pointIndex)
   {
      var g = Physics.gravity.y;
      var Vvert = Vinit * (Mathf.Sin(GetAngle() * Mathf.Deg2Rad));
      var Vhor = Vinit * (Mathf.Cos(GetAngle() * Mathf.Deg2Rad));
      var Tvert = (0 - Vvert) / g;
      var Thor = 2 * Tvert;
      var Dtot = Vhor * Thor;
      var Dp = (Dtot / (amountPoints)) * pointIndex;
      var T2 = Dp / Vhor;
      var height = ((Vvert * Dp) / Vhor) + 0.5f * g * Mathf.Pow(T2, 2);
      return height;
   }

   public void MakeShot()
   {
      var _projectile = Instantiate(ProjectilePrefab, ProjectileSpawnTransform.position, Quaternion.identity) as GameObject;
      _projectile.GetComponent<Rigidbody>().AddForce(GetShotDirection() * StartPower * 2.5f, ForceMode.Impulse);

      Destroy(_projectile, 4.0f);
   }

   public float GetAngle()
   {
      var angle = Vector3.Angle((ReleasePointTransform.transform.position - DragHandle.transform.position).normalized, Vector3.right);

      if (DragHandle.transform.position.y > AimerTransform.position.y)
      {
         angle = angle * -1;
      }

      return angle;
   }

   private void Start()
   {
      LineLengths = new float[2];
      AimerTransform.position = new Vector3(1, 1, 0);

      for (var i = 0; i < HandlesLineRenderers.Length; i++)
      {
         HandlesLineRenderers[i].SetPosition(0, HandleAnchorTrnsforms[i].position);
         HandlesLineRenderers[i].SetPosition(1, DragHandle.transform.position);
         HandlesLineRenderers[i].startWidth = 0.15f;
         HandlesLineRenderers[i].endWidth = 0.05f;
      }
   }

   private void OnEnable()
   {
      DragHandle.OnDragHandleReleaseEvent += DragHandle_OnDragHandleReleaseEvent;
   }

   private void OnDisable()
   {
      DragHandle.OnDragHandleReleaseEvent -= DragHandle_OnDragHandleReleaseEvent;
   }

   private void OnDestroy()
   {
      DragHandle.OnDragHandleReleaseEvent -= DragHandle_OnDragHandleReleaseEvent;
   }

   private void DragHandle_OnDragHandleReleaseEvent()
   {
      MakeShot();
   }

   private void Update()
   {
      UpdateLines();
      UpdateAim();
      GetHeight(GetVelocity(), 3, 1);
      StartPower = Vector3.Distance(DragHandle.transform.position, ReleasePointTransform.transform.position);
   }

   private void UpdateLines()
   {
      for (var i = 0; i < HandlesLineRenderers.Length; i++)
      {
         HandlesLineRenderers[i].SetPosition(1, DragHandle.transform.position);
         HandlesLineRenderers[i].SetPosition(0, HandleAnchorTrnsforms[i].position);

         HandlesLineRenderers[i].GetComponent<LineRenderer>().startWidth = 0.15f / LineLengths[i];
         HandlesLineRenderers[i].GetComponent<LineRenderer>().endWidth = 0.05f / LineLengths[i];

         LineLengths[i] = Vector3.Distance(DragHandle.transform.position, HandleAnchorTrnsforms[i].position);

         if (LineLengths[i] <= 0.65f)
         {
            LineLengths[i] = 0.65f;
         }
      }
   }

   private void UpdateAim()
   {
      var pullDirection = ReleasePointTransform.position - (DragHandle.transform.position - ReleasePointTransform.position).normalized;
      AimerTransform.position = pullDirection;
   }

   private Vector3 GetShotDirection()
   {
      return (AimerTransform.position - ReleasePointTransform.transform.position).normalized;
   }
}