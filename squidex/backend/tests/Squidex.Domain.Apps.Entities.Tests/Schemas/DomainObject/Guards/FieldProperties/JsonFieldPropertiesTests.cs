﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Domain.Apps.Core.Schemas;
using Squidex.Domain.Apps.Core.TestHelpers;

namespace Squidex.Domain.Apps.Entities.Schemas.DomainObject.Guards.FieldProperties;

public class JsonFieldPropertiesTests : IClassFixture<TranslationsFixture>
{
    [Fact]
    public void Should_not_add_error()
    {
        var sut = new JsonFieldProperties();

        var errors = FieldPropertiesValidator.Validate(sut).ToList();

        Assert.Empty(errors);
    }
}
