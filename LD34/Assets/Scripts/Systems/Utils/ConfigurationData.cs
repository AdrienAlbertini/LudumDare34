// ***********************************************************************
// Assembly         : Assembly-CSharp
// Author           : Adrien Albertini
// Created          : 03-10-2014
//
// Last Modified By : Adrien Albertini
// Last Modified On : 03-12-2014
// ***********************************************************************
// <copyright file="GlobalDatasModel.cs" company="">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ************************************************************************
using UnityEngine;
using System;

/// <summary>
/// Class ConfigurationData.
/// </summary>
public class ConfigurationData : Singleton<ConfigurationData>
{
    public static string actionsAssetPath = "actionsAsset";

    #region "Enumerations"

    #endregion

    public Custom.Inputs.Actions.ActionHolder actionHolder = null;

    /// <summary>
    /// Initializes this instance.
    /// </summary>
    public void Initialize()
    {
        if (this.actionHolder == null)
        {
            Custom.Inputs.InputsManager.Instance.LoadActions(actionsAssetPath);

            if ((this.actionHolder = Custom.Inputs.InputsManager.Instance.InputsActionHolder) == null)
            {
                Debug.LogError("ActionHolder not found");
            }
        }
    }
}
