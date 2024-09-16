﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure.Collections;

namespace Squidex.Domain.Apps.Core.Apps;

public sealed record AppSettings
{
    public static readonly AppSettings Empty = new AppSettings();

    public ReadonlyList<Pattern> Patterns { get; init; } = [];

    public ReadonlyList<Editor> Editors { get; init; } = [];

    public bool HideScheduler { get; init; }

    public bool HideDateTimeModeButton { get; init; }
}
