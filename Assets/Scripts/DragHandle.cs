using UnityEngine;

public class DragHandle : MonoBehaviour
{
   private Vector3 _offset;

   private Vector2 _defaulPos;

   private Vector2 _currentPosition;

   public event OnDragHandleReleaseDelegate OnDragHandleReleaseEvent;

   private void Start()
   {
      _defaulPos = new Vector2(-6.13f, -1.87f);
      transform.position = _defaulPos;
   }

   private void OnMouseDown()
   {
      _offset = gameObject.transform.position - Camera.main.ScreenToWorldPoint
      (
         new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0)
      );
      Cursor.visible = false;
   }

   private void OnMouseDrag()
   {
      var currentScreenPoint = new Vector3(Input.mousePosition.x, Input.mousePosition.y, 0);
      _currentPosition = Camera.main.ScreenToWorldPoint(currentScreenPoint) + _offset;

      transform.position = _currentPosition;
   }

   private void OnMouseUp()
   {
      Cursor.visible = true;

      if (OnDragHandleReleaseEvent != null)
      {
         OnDragHandleReleaseEvent.Invoke();
      }

      transform.position = _defaulPos;
   }
}