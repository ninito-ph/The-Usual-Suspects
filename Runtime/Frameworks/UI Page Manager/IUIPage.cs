using UnityEngine;

namespace Ninito.UsualSuspects
{
    /// <summary>
    ///     An interface that defines essential functions for an UI page
    /// </summary>
    public interface IUIPage
    {
        public GameObject PageGameObject { get; }

        /// <summary>
        ///     Enters the page
        /// </summary>
        public void Enter();

        /// <summary>
        ///     Exits the page
        /// </summary>
        public void Exit();
    }
}