using UnityEngine;

namespace Spawn
{
    public static class ScreenUtils
<<<<<<< Updated upstream
    {       
        public static Vector3 GetRandomVisiblePosition(Camera camera, float objectSize = 0.5f, float zPosition = 0f)
        {          
=======
    {
        public static bool IsPointUnderUI(Vector3 worldPosition, Camera camera, RectTransform[] uiZones)
        {
            if (uiZones == null) return false;

            Vector2 screenPoint = camera.WorldToScreenPoint(worldPosition);

            foreach (var zone in uiZones)
            {
                if (zone == null) continue;

                if (RectTransformUtility.ScreenPointToLocalPointInRectangle(
                    zone,
                    screenPoint,
                    camera,
                    out Vector2 localPoint))
                {
                    if (zone.rect.Contains(localPoint))
                    {
                        return true; 
                    }
                }
            }
            return false; 
        }

        public static Vector3 GetRandomVisiblePosition(Camera camera, float objectSize = 0.5f, float zPosition = 0f)
        {
>>>>>>> Stashed changes
            float height = camera.orthographicSize * 2f;
            float width = height * camera.aspect;

            float minX = -width / 2f + objectSize;
            float maxX = width / 2f - objectSize;
            float minY = -height / 2f + objectSize;
            float maxY = height / 2f - objectSize;

            float randomX = Random.Range(minX, maxX);
            float randomY = Random.Range(minY, maxY);
<<<<<<< Updated upstream

            return new Vector3(camera.transform.position.x + randomX, camera.transform.position.y + randomY, zPosition);
        }
=======
 
            return new Vector3(camera.transform.position.x + randomX, camera.transform.position.y + randomY, zPosition);
        }

        public static Vector3 GetSafeRandomPosition(
            Camera camera,
            float objectRadius,
            RectTransform[] uiZones,
            int maxAttempts = 10)
        {

            for (int i = 0; i < maxAttempts; i++)
            {

                Vector3 candidatePos = GetRandomVisiblePosition(camera, objectRadius);

                if (!IsPointUnderUI(candidatePos, camera, uiZones))
                {
                    return candidatePos;
                }
            }

            Debug.LogWarning("Не удалось найти свободное место для спавна вне UI!");
            return GetRandomVisiblePosition(camera, objectRadius);
        }
>>>>>>> Stashed changes
    }
}
  