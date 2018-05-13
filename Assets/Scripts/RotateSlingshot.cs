using UnityEngine;

public class RotateSlingshot : MonoBehaviour
{
   private float _rotationZ;
   private float _sensitivityZ = 2;

   private void Start()
   {
      transform.rotation = Quaternion.identity;
   }

   private void Update()
   {
      UpdateLockedRotation();
   }

   private void UpdateLockedRotation()
   {
      _rotationZ += Input.GetAxis("Mouse ScrollWheel") * _sensitivityZ * 10;
      _rotationZ = Mathf.Clamp(_rotationZ, -30.0f, 30.0f);

      transform.localEulerAngles = new Vector3(transform.localEulerAngles.x, transform.localEulerAngles.y, -_rotationZ);
   }
}