using UnityEngine;

public class bl_ControllerExample : MonoBehaviour {

    /// <summary>
    /// Step #1
    /// We need a simple reference of joystick in the script
    /// that we need add it.
    /// </summary>
	[SerializeField]private bl_Joystick Joystick;//Joystick reference for assign in inspector

    [SerializeField]private float Speed = 5;
    private Vector3 Direction;
    void Update()
    {
        //Step #2
        //Change Input.GetAxis (or the input that you using) to Joystick.Vertical or Joystick.Horizontal
        float v = Joystick.Vertical; //get the vertical value of joystick
        float h = Joystick.Horizontal;//get the horizontal value of joystick

        //in case you using keys instead of axis (due keys are bool and not float) you can do this:
        //bool isKeyPressed = (Joystick.Horizontal > 0) ? true : false;

        //ready!, you not need more.
        Vector3 translate = (new Vector3(h, 0, v) * Time.deltaTime) * Speed;
        transform.Translate(translate);
        
        
        Plane plane = new Plane(Vector3.up,transform.position);
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            
        float distance;
 
        if(plane.Raycast(ray, out distance))
            Direction = ray.GetPoint(distance);

        Joystick.GetComponent<RectTransform>().anchoredPosition = ScreenPointToAnchoredPosition(Direction);
    }
    
    
    private Vector2 ScreenPointToAnchoredPosition(Vector2 screenPosition)
    {
        Vector2 localPoint = Vector2.zero;
        if (RectTransformUtility.ScreenPointToLocalPointInRectangle(Joystick.GetComponent<RectTransform>(), screenPosition, Camera.main, out localPoint))
        {
            Vector2 pivotOffset = Joystick.GetComponent<RectTransform>().pivot * Joystick.GetComponent<RectTransform>().sizeDelta;
            return localPoint - Joystick.GetComponent<RectTransform>().anchorMax * Joystick.GetComponent<RectTransform>().sizeDelta + pivotOffset;
        }
        return Vector2.zero;
    }
}