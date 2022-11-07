using UnityEngine;
using Cinemachine;
 

[ExecuteInEditMode] [SaveDuringPlay] [AddComponentMenu("")]
public class LockYAxis: CinemachineExtension
{
    [Tooltip("Lock the camera's Z position to this value")]
    private float yPosition = 5.75f;
 
    protected override void PostPipelineStageCallback(
        CinemachineVirtualCameraBase vcam,
        CinemachineCore.Stage stage, ref CameraState state, float deltaTime)
    {
        if (stage == CinemachineCore.Stage.Body)
        {
            var pos = state.RawPosition;
            pos.y = yPosition;
            state.RawPosition = pos;
        }
    }
}