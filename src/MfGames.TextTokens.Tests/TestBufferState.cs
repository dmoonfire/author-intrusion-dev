// <copyright file="TestBufferState.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests
{
    /// <summary>
    /// A testing class that listens to events of the Buffer like a text editor and
    /// keeps track of the current state. This is used to reflect what the user will
    /// see in the editor.
    /// </summary>
    public class TestBufferState
    {
        #region Methods

        /// <summary>
        /// </summary>
        /// <param name="sender">
        /// </param>
        /// <param name="e">
        /// </param>
        private void OnTokenReplaced(object sender, TokenReplacedEventArgs e)
        {
        }

        #endregion
    }
}