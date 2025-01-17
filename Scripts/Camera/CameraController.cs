using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField, Header("��������ĸ߶�")] private float lookDistance;
    [SerializeField, Header("��������")] private Transform lookObj;
    [SerializeField, Header("���ƫ�ƾ���")] private float scrollistance;
    [SerializeField, Header("����ƫ������")] private Vector2 scrollClamp;
    [SerializeField, Header("�������ƽǶ�")] private Vector2 upClamp;
    [SerializeField, Header("�����ת������")] private float sensitivity;
    [SerializeField, Header("����������")] private float Scrollsensitivity;

    //�ۼ���ת
    private float x = 0f;
    private float y = 0f;
    //���ⲿ��
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
        //�н����־���
        scrollistance = Mathf.Clamp(scrollistance, scrollClamp.x, scrollClamp.y);
    }

    private void CameraPosition()
    {
        this.transform.position = lookObj.position + Vector3.up * lookDistance - this.transform.rotation * Vector3.forward * scrollistance;
    }
}