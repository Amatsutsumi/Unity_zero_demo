using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("看向物体的高度")] private float lookDistance;
    [SerializeField, Header("看向物体")] private Transform lookObj;
    [SerializeField, Header("相机偏移距离")] private float scrollistance;
    [SerializeField, Header("滚轮偏移限制")] private Vector2 scrollClamp;
    [SerializeField, Header("上下限制角度")] private Vector2 upClamp;
    [SerializeField, Header("相机旋转灵敏度")] private float sensitivity;
    [SerializeField, Header("滚轮灵敏度")] private float Scrollsensitivity;

    //累计旋转
    private float x = 0f;
    private float y = 0f;
    //给外部用
    [HideInInspector]
    public Quaternion _rotation;

    private void LateUpdate()
    {
        CameraInput();
        Scroll();
        CameraPosition();
    }

    private void CameraInput()
    {
        x -= GameInputManager.Instance().Camera.y;
        y += GameInputManager.Instance().Camera.x;
        x = Mathf.Clamp(x, upClamp.x, upClamp.y);
        this.transform.rotation = Quaternion.Euler(new Vector3(x, y, 0));
        _rotation = transform.rotation;
    }
    private void Scroll()
    {
        float scroll = Input.GetAxis("Mouse ScrollWheel");
        scrollistance -= scroll * Scrollsensitivity * Time.deltaTime * 30f;
        //夹紧滚轮距离
        scrollistance = Mathf.Clamp(scrollistance, scrollClamp.x, scrollClamp.y);
    }

    private void CameraPosition()
    {
        this.transform.position = lookObj.position + Vector3.up * lookDistance - this.transform.rotation * Vector3.forward * scrollistance;
    }
}