using UnityEngine;
using System.Collections;

public class MoveSmooth : MonoBehaviour
{

    public GameObject CameraPlayer;
    private Vector3 _cameraOffset;

    public float MoveSpeed = 5f;
    private float RotateSpeed = 5f;
    private float CameraRotateSpeed = 2f;
    private Vector3 direction;

    // Use this for initialization
    void Start()
    {
        _cameraOffset = this.transform.position - CameraPlayer.transform.position;
    }

    // Update is called once per frame
    void Update()
    {



        if (Input.GetKey(KeyCode.W))
        {
            direction = transform.forward;
            MoveSpeed += Time.deltaTime * 10f;
            MoveSpeed = Mathf.Clamp(MoveSpeed, 10, 1000);
        }
        else
        {
            MoveSpeed -= Time.deltaTime;
        }
        if (Input.GetKey(KeyCode.A))
        {
            this.transform.Rotate(Vector3.down * Time.deltaTime * Mathf.Pow(RotateSpeed, 3));

            _cameraOffset = Quaternion.AngleAxis(((Time.deltaTime * Mathf.Pow(RotateSpeed, 3)) * -1), Vector3.up) * _cameraOffset;

            direction = transform.forward;
        }
        if (Input.GetKey(KeyCode.S))
        {
            MoveSpeed -= Time.deltaTime * 10;
        }
        if (Input.GetKey(KeyCode.D))
        {
            this.transform.Rotate(Vector3.up * Time.deltaTime * Mathf.Pow(RotateSpeed, 3));

            _cameraOffset = Quaternion.AngleAxis(Time.deltaTime * Mathf.Pow(RotateSpeed, 3), Vector3.up) * _cameraOffset;

            direction = transform.forward;
        }

        Debug.DrawLine(this.transform.position + direction.normalized * 10f, this.transform.localPosition, Color.red);
        this.transform.position = Vector3.Lerp(this.transform.position, this.transform.position + direction * MoveSpeed, Time.deltaTime * (MoveSpeed));

        CameraPlayer.transform.position = Vector3.Lerp(CameraPlayer.transform.position, this.transform.position - _cameraOffset, Time.deltaTime * RotateSpeed / 1.5f);


        CameraPlayer.transform.LookAt(this.transform);

    }
}
