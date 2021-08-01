using System.Collections.Generic;
using UnityEngine;

namespace Ninito.UsualSuspects
{
    /// <summary>
    ///     A script that controls basic functions relating to a window-based menu
    /// </summary>
    public class UIPageManager : MonoBehaviour
    {
        #region Private Fields

        [Header(header: "UI Page Manager Settings")]
        [SerializeField]
        private string defaultPageKey = string.Empty;

        [SerializeField]
        private SerializedDictionary<string, UIPage> uiPages = new SerializedDictionary<string, UIPage>();

        private UIPage _activePage;
        private readonly List<UIPage> _overlayedPages = new List<UIPage>();

        #endregion

        #region Unity Callback

        private void Start()
        {
            SwitchToPage(pageKey: defaultPageKey);
        }

        #endregion

        #region Public Methods

        /// <summary>
        ///     Switches to a given page
        /// </summary>
        /// <param name="pageKey">The desired page's key</param>
        public void SwitchToPage(string pageKey)
        {
            if (_activePage != null)
            {
                _activePage.Exit();
            }

            _activePage = uiPages[key: pageKey];
            _activePage.Enter();
        }

        /// <summary>
        ///     Enters a given page
        /// </summary>
        /// <param name="pageKey">The page to be entered</param>
        public void EnterPage(string pageKey)
        {
            _overlayedPages.Add(item: uiPages[key: pageKey]);
            uiPages[key: pageKey].Enter();
        }

        /// <summary>
        ///     Exits a page
        /// </summary>
        /// <param name="pageKey">The page to be exited</param>
        public void ExitPage(string pageKey)
        {
            _overlayedPages.Remove(item: uiPages[key: pageKey]);
            uiPages[key: pageKey].Exit();
        }

        /// <summary>
        ///     Exits all pages
        /// </summary>
        public void ExitAllPages()
        {
            _overlayedPages.Clear();
            _activePage = null;

            foreach (UIPage page in uiPages.Values)
            {
                page.Exit();
            }
        }

        #endregion
    }
}