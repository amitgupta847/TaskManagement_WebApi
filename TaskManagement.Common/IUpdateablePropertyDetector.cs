﻿using System.Collections.Generic;

namespace TaskManagement.Common
{
    /// <summary>
    ///     Detects updateable properties.
    /// </summary>
    public interface IUpdateablePropertyDetector
    {
        /// <summary>
        ///     Detects which properties on the target may be updated based on the supplied data.
        /// </summary>
        /// <remarks>
        ///     Editable properties on <typeparamref name="TTargetType"/> that have corresponding data in
        ///     <paramref name="objectContainingUpdatedData" /> are included in the response.
        /// </remarks>
        IEnumerable<string> GetNamesOfPropertiesToUpdate<TTargetType>(object objectContainingUpdatedData);
    }
}