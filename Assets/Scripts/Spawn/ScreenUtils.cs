using UnityEngine;

namespace Spawn
{
    public static class ScreenUtils
    {       
        public static Vector3 GetRandomVisiblePosition(Camera camera, float objectSize = 0.5f, float zPosition = 0f)
        {          
            float height = camera.orthographicSize * 2f;
            float width = height * camera.aspect;

            float minX = -width / 2f + objectSize;
            float maxX = width / 2f - objectSize;
            float minY = -height / 2f + objectSize;
            float maxY = height / 2f - objectSize;

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);

            return new Vector3(camera.transform.position.x + randomX, camera.transform.position.y + randomY, zPosition);
        }
    }
}
  