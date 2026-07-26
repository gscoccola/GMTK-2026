using UnityEngine;

public class VFX : MonoBehaviour
{
    [SerializeField] public VFXType Type;

    private void OnEnable()
    {
        int index = 0;
        if (Type == VFXType.Explosion) index = 0;
        else if (Type == VFXType.Fire) index = Random.Range(0, 6);
        else if (Type == VFXType.Smoke) index = Random.Range(0, 2);
        else if (Type == VFXType.Spark) index = Random.Range(0, 3);
        else if (Type == VFXType.SparkDark) index = 0;

        GetComponent<Animator>().Play(Type.ToString() + index.ToString(), 0);
    }

}

public enum VFXType
{
    Explosion,
    Fire,
    Smoke,
    Spark,
    SparkDark,
}