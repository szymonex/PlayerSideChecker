using UnityEngine;

public class SideChecker : MonoBehaviour
{
    public Transform playerTransform;
    Vector3 normalDistance;
    Vector3 forwardVec;

    private void Update()
    {      
        CheckSide();
    }

    private void CheckSide()
    {
        var angle = Theta();
        Vector3 cross = CrossVectors(normalDistance, forwardVec);

        if (cross.y < 0)
        {
            angle = -angle;
        }

        Debug.Log($"Player is on the {(angle < 0 ? Sides.left : Sides.right)} side of the object " +
                  $"and {(Mathf.Abs(angle) < 90 ? Sides.inFrontOf : Sides.behind)} it.");
    }

    private float Theta()
    {
        float theta = Mathf.Acos(DotProduct());
        float thetaInDeg = theta * Mathf.Rad2Deg;

        return thetaInDeg;
    }

    private float DotProduct()
    {
        normalDistance = NormalizeVector3(CheckDistanceToPlayer());
        forwardVec = transform.forward;

        return (normalDistance.x * forwardVec.x) + (normalDistance.y * forwardVec.y) + (normalDistance.z * forwardVec.z);
    }

    private Vector3 NormalizeVector3(Vector3 vectorToNormalize)
    {
        float lengthVectorToNormalize = Mathf.Sqrt(Mathf.Pow(vectorToNormalize.x, 2f) + Mathf.Pow(vectorToNormalize.y, 2f) + Mathf.Pow(vectorToNormalize.z, 2f));
        return new Vector3(vectorToNormalize.x / lengthVectorToNormalize, vectorToNormalize.y / lengthVectorToNormalize, vectorToNormalize.z / lengthVectorToNormalize);
    }

    private Vector3 CheckDistanceToPlayer()
    {
        return transform.position - playerTransform.position;
    }

    private Vector3 CrossVectors(Vector3 a, Vector3 b)
    {
        Vector3 result;
        result.x = a.y * b.z - a.z * b.y;
        result.y = a.z * b.x - a.x * b.z;
        result.z = a.x * b.y - a.y * b.x;
        return result;
    }

    private enum Sides
    {
        left,
        right,
        inFrontOf,
        behind
    }
}
