using UnityEngine;
using Vuforia;

public class FocusCamera : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        VuforiaApplication.Instance.OnVuforiaStarted += StartVuforiaFocus;
    }

    public void StartVuforiaFocus()
    {
        VuforiaBehaviour.Instance.CameraDevice.SetFocusMode(FocusMode.FOCUS_MODE_CONTINUOUSAUTO);
    }
    // Update is called once per frame
    void Update()
    {

    }
}
