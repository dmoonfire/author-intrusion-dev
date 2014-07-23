﻿// <copyright file="DefaultTokenizerTests.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace MfGames.TextTokens.Tests.Tokens
{
    using System.Collections.Generic;
    using System.Linq;

    using MfGames.TextTokens.Tokens;

    using NUnit.Framework;

    /// <summary>
    /// Tests the functionality of the default tokenizer to ensure it produces the
    /// correct tokens.
    /// </summary>
    [TestFixture]
    public class DefaultTokenizerTests
    {
        #region Public Methods and Operators

        /// <summary>
        /// </summary>
        [Test]
        public void HandleBlankString()
        {
            // Arrange
            string input = string.Empty;
            var tokenizer = new DefaultTokenizer();

            // Act
            List<string> results = tokenizer.Tokenize(input).ToList();

            // Assert
            Assert.IsNotNull(results, "The results were null");
            Assert.AreEqual(
                0, results.Count, "The number of tokens is unexpected.");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void HandleNullString()
        {
            // Arrange
            string input = null;
            var tokenizer = new DefaultTokenizer();

            // Act
            List<string> results = tokenizer.Tokenize(input).ToList();

            // Assert
            Assert.IsNotNull(results, "The results were null");
            Assert.AreEqual(
                0, results.Count, "The number of tokens is unexpected.");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void HandleSingleWordString()
        {
            // Arrange
            string input = "one";
            var tokenizer = new DefaultTokenizer();

            // Act
            List<string> results = tokenizer.Tokenize(input).ToList();

            // Assert
            Assert.IsNotNull(results, "The results were null");
            Assert.AreEqual(
                1, results.Count, "The number of tokens is unexpected.");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void HandleSpaceString()
        {
            // Arrange
            string input = " ";
            var tokenizer = new DefaultTokenizer();

            // Act
            List<string> results = tokenizer.Tokenize(input).ToList();

            // Assert
            Assert.IsNotNull(results, "The results were null");
            Assert.AreEqual(
                1, results.Count, "The number of tokens is unexpected.");
        }

        /// <summary>
        /// </summary>
        [Test]
        public void HandleTwoSpacesString()
        {
            // Arrange
            string input = "  ";
            var tokenizer = new DefaultTokenizer();

            // Act
            List<string> results = tokenizer.Tokenize(input).ToList();

            // Assert
            Assert.IsNotNull(results, "The results were null");
            Assert.AreEqual(
                1, results.Count, "The number of tokens is unexpected.");
        }

        #endregion
    }
}