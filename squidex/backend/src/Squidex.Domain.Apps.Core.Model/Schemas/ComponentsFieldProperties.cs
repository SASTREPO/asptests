﻿// ==========================================================================
//  Squidex Headless CMS
// ==========================================================================
//  Copyright (c) Squidex UG (haftungsbeschraenkt)
//  All rights reserved. Licensed under the MIT license.
// ==========================================================================

using Squidex.Infrastructure;
using Squidex.Infrastructure.Collections;

namespace Squidex.Domain.Apps.Core.Schemas;

public sealed record ComponentsFieldProperties : FieldProperties
{
    public int? MinItems { get; init; }

    public int? MaxItems { get; init; }

    public ReadonlyList<string>? UniqueFields { get; init; }

    public ArrayCalculatedDefaultValue CalculatedDefaultValue { get; init; }

    public DomainId SchemaId
    {
        init
        {
            SchemaIds = value != default ? ReadonlyList.Create(value) : null;
        }
        get
        {
            return SchemaIds?.FirstOrDefault() ?? default;
        }
    }

    public ReadonlyList<DomainId>? SchemaIds { get; init; }

    public override T Accept<T, TArgs>(IFieldPropertiesVisitor<T, TArgs> visitor, TArgs args)
    {
        return visitor.Visit(this, args);
    }

    public override T Accept<T, TArgs>(IFieldVisitor<T, TArgs> visitor, IField field, TArgs args)
    {
        return visitor.Visit((IField<ComponentsFieldProperties>)field, args);
    }

    public override RootField CreateRootField(long id, string name, Partitioning partitioning)
    {
        return Fields.Components(id, name, partitioning, this);
    }

    public override NestedField CreateNestedField(long id, string name)
    {
        return Fields.Components(id, name, this);
    }
}
