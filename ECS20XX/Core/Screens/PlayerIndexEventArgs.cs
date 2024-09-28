#region File Description

//-----------------------------------------------------------------------------
// PlayerIndexEventArgs.cs
//
// XNA Community Game Platform
// Copyright (C) Microsoft Corporation. All rights reserved.
//-----------------------------------------------------------------------------

#endregion File Description

#region Using Statements

using System;
using Microsoft.Xna.Framework;

#endregion Using Statements

namespace ECS2022_23.Core.Screens;

/// <summary>
///     Custom event argument which includes the index of the player who
///     triggered the event. This is used by the MenuEntry.Selected event.
/// </summary>
internal class PlayerIndexEventArgs : EventArgs
{
    /// <summary>
    ///     Constructor.
    /// </summary>
    public PlayerIndexEventArgs(PlayerIndex playerIndex)
    {
        PlayerIndex = playerIndex;
    }

    /// <summary>
    ///     Gets the index of the player who triggered this event.
    /// </summary>
    public PlayerIndex PlayerIndex { get; }
}