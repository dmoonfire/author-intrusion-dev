// <copyright file="INamedSlugged.cs" company="Moonfire Games">
//     Copyright (c) Moonfire Games. Some Rights Reserved.
// </copyright>
// MIT Licensed (http://opensource.org/licenses/MIT)
namespace AuthorIntrusion
{
    /// <summary>
    /// Indicates that the item has both a name and a slug, used for some extension methods
    /// and selectors.
    /// </summary>
    public interface INamedSlugged : INamed, 
        ISlugged
    {
    }
}