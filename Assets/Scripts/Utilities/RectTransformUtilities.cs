using UnityEngine;

namespace ManyTools.UnityExtended.Utilities
{
    /// <summary>
    ///     A class containing various utility functions for RectTransforms
    /// </summary>
    public static class RectTransformUtilities
    {
        /// <summary>
        ///     Converts screen position to local position in a rect
        /// </summary>
        /// <param name="rect">The rect to get the position from</param>
        /// <param name="screenPosition">The screen position to convert</param>
        /// <param name="currentCamera">The camera to convert with</param>
        /// <param name="localPosition">The output local rect position</param>
        /// <returns>Whether the point could be converted</returns>
        public static bool ScreenToLocalRectPosition(RectTransform rect, Vector2 screenPosition, Camera currentCamera,
            out Vector2 localPosition)
        {
            localPosition = Vector2.zero;

            if (!ScreenToWorldRectPosition(rect: rect, screenPosition: screenPosition,
                currentCamera: currentCamera, worldPosition: out Vector3 worldPosition))
            {
                return false;
            }

            localPosition = rect.InverseTransformPoint(position: worldPosition);
            return true;
        }

        /// <summary>
        ///     Converts screen position to world position in a rect
        /// </summary>
        /// <param name="rect">The rect to get the world position from</param>
        /// <param name="screenPosition">The screen position to convert</param>
        /// <param name="currentCamera">The camera to convert with</param>
        /// <param name="worldPosition">The output world position</param>
        /// <returns>Whether the point could be converted</returns>
        public static bool ScreenToWorldRectPosition(RectTransform rect, Vector3 screenPosition, Camera currentCamera,
            out Vector3 worldPosition)
        {
            worldPosition = Vector2.zero;
            Ray ray = ScreenPositionToRay(currentCamera: currentCamera, screenPosition: screenPosition);

            if (!DoesRayHitRect(rect: rect, ray: ray, hitDistance: out float hitDistance))
            {
                return false;
            }

            worldPosition = ray.GetPoint(distance: hitDistance);
            return true;
        }

        /// <summary>
        ///     Checks whether a ray hits a rect plane
        /// </summary>
        /// <param name="rect"></param>
        /// <param name="ray"></param>
        /// <param name="hitDistance"></param>
        /// <returns></returns>
        private static bool DoesRayHitRect(RectTransform rect, Ray ray, out float hitDistance)
        {
            return new Plane(inNormal: rect.rotation * Vector3.back, inPoint: rect.position).Raycast(ray: ray,
                enter: out hitDistance);
        }

        /// <summary>
        ///     Gets a ray from a given screen position
        /// </summary>
        /// <param name="currentCamera">The camera to raycast with</param>
        /// <param name="screenPosition">The position to raycast from</param>
        /// <returns>The cast ray</returns>
        public static Ray ScreenPositionToRay(Camera currentCamera, Vector2 screenPosition)
        {
            return currentCamera != null
                ? currentCamera.ScreenPointToRay(pos: screenPosition)
                : MakeScreenRayWithOffset(screenPosition: screenPosition, zOffset: -100);
        }

        /// <summary>
        ///     Makes a ray from a screen position with a Z offset
        /// </summary>
        /// <param name="screenPosition">The position from which the ray should begin</param>
        /// <param name="zOffset">The Z offset for the Ray's starting position</param>
        /// <returns>A ray cast from the given position with the given Z offset</returns>
        private static Ray MakeScreenRayWithOffset(Vector2 screenPosition, float zOffset)
        {
            Vector3 origin = screenPosition;
            origin.z += zOffset;
            return new Ray(origin: origin, direction: Vector3.forward);
        }
    }
}