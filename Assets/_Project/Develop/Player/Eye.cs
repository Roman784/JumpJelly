using UnityEngine;

public class Eye : MonoBehaviour
{
    [field: SerializeField] public Transform Sclera { get; private set; }
    [field: SerializeField] public Transform Pupil {  get; private set; }
    [field: SerializeField] public float PupilDistance { get; private set; } // Distance from the center of the eye to the pupil.
}
