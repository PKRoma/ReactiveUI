﻿// Copyright (c) 2025 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

namespace ReactiveUI;

/// <summary>
/// A collection of helpers to aid working with observable properties.
/// </summary>
#if NET6_0_OR_GREATER
[RequiresDynamicCode("The method uses reflection and will not work in AOT environments.")]
[RequiresUnreferencedCode("The method uses reflection and will not work in AOT environments.")]
#endif
public static class OAPHCreationHelperMixin
{
    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// An Expression representing the property (i.e. <c>x => x.SomeProperty</c>).
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should normally
    /// be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing field
    /// for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        Expression<Func<TObj, TRet>> property,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        source.ArgumentNullExceptionThrowIfNull(nameof(source));
        property.ArgumentNullExceptionThrowIfNull(nameof(property));

        return source.ObservableToProperty(target, property, deferSubscription, scheduler);
    }

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// An Expression representing the property (i.e. <c>x => x.SomeProperty</c>).
    /// </param>
    /// <param name="initialValue">
    /// The initial value of the property.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should normally
    /// be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing field
    /// for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        Expression<Func<TObj, TRet>> property,
        TRet initialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
        => ToProperty(target, source, property, () => initialValue, deferSubscription, scheduler);

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// An Expression representing the property (i.e. <c>x => x.SomeProperty</c>).
    /// </param>
    /// <param name="getInitialValue">
    /// The function used to retrieve the initial value of the property.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should normally
    /// be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing field
    /// for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        Expression<Func<TObj, TRet>> property,
        Func<TRet> getInitialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
    {
        property.ArgumentNullExceptionThrowIfNull(nameof(property));
        return source.ObservableToProperty(target, property, getInitialValue, deferSubscription, scheduler);
    }

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// An Expression representing the property (i.e. <c>x => x.SomeProperty</c>).
    /// </param>
    /// <param name="result">
    /// An out param matching the return value, provided for convenience.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should
    /// normally be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing
    /// field for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        Expression<Func<TObj, TRet>> property,
        out ObservableAsPropertyHelper<TRet> result,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        source.ArgumentNullExceptionThrowIfNull(nameof(source));
        property.ArgumentNullExceptionThrowIfNull(nameof(property));

        var ret = source.ObservableToProperty(target, property, deferSubscription, scheduler);

        result = ret;

        return ret;
    }

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// An Expression representing the property (i.e. <c>x => x.SomeProperty</c>).
    /// </param>
    /// <param name="result">
    /// An out param matching the return value, provided for convenience.
    /// </param>
    /// <param name="initialValue">
    /// The initial value of the property.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should
    /// normally be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing
    /// field for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        Expression<Func<TObj, TRet>> property,
        out ObservableAsPropertyHelper<TRet> result,
        TRet initialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
        => ToProperty(target, source, property, out result, () => initialValue, deferSubscription, scheduler);

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// An Expression representing the property (i.e. <c>x => x.SomeProperty</c>).
    /// </param>
    /// <param name="result">
    /// An out param matching the return value, provided for convenience.
    /// </param>
    /// <param name="getInitialValue">
    /// The function used to retrieve the initial value of the property.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should
    /// normally be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing
    /// field for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        Expression<Func<TObj, TRet>> property,
        out ObservableAsPropertyHelper<TRet> result,
        Func<TRet> getInitialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
    {
        property.ArgumentNullExceptionThrowIfNull(nameof(property));
        var ret = source.ObservableToProperty(target, property, getInitialValue, deferSubscription, scheduler);

        result = ret;
        return ret;
    }

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// The name of the property that has changed. Recommended for use with nameof() or a FODY.
    /// or a FODY.
    /// </param>
    /// <param name="initialValue">
    /// The initial value of the property.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should normally
    /// be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing field
    /// for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        string property,
        TRet initialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
        => ToProperty(target, source, property, () => initialValue, deferSubscription, scheduler);

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// The name of the property that has changed. Recommended for use with nameof() or a FODY.
    /// or a FODY.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should normally
    /// be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing field
    /// for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        string property,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        source.ArgumentNullExceptionThrowIfNull(nameof(source));

        if (string.IsNullOrWhiteSpace(property))
        {
            throw new ArgumentException($"'{nameof(property)}' cannot be null or whitespace", nameof(property));
        }

        return source.ObservableToProperty(target, property, deferSubscription, scheduler);
    }

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// The name of the property that has changed. Recommended for use with nameof() or a FODY.
    /// or a FODY.
    /// </param>
    /// <param name="getInitialValue">
    /// The function used to retrieve the initial value of the property.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should normally
    /// be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing field
    /// for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        string property,
        Func<TRet> getInitialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        source.ArgumentNullExceptionThrowIfNull(nameof(source));

        if (string.IsNullOrWhiteSpace(property))
        {
            throw new ArgumentException($"'{nameof(property)}' cannot be null or whitespace", nameof(property));
        }

        return source.ObservableToProperty(target, property, getInitialValue, deferSubscription, scheduler);
    }

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// The name of the property that has changed. Recommended for use with nameof() or a FODY.
    /// </param>
    /// <param name="result">
    /// An out param matching the return value, provided for convenience.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should
    /// normally be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing
    /// field for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        string property,
        out ObservableAsPropertyHelper<TRet> result,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        source.ArgumentNullExceptionThrowIfNull(nameof(source));

        if (string.IsNullOrWhiteSpace(property))
        {
            throw new ArgumentException($"'{nameof(property)}' cannot be null or whitespace", nameof(property));
        }

        result = source.ObservableToProperty(target, property, deferSubscription, scheduler);

        return result;
    }

    /// <summary>
    /// Converts an Observable to an ObservableAsPropertyHelper and
    /// automatically provides the onChanged method to raise the property
    /// changed notification.
    /// </summary>
    /// <typeparam name="TObj">The object type.</typeparam>
    /// <typeparam name="TRet">The result type.</typeparam>
    /// <param name="target">
    /// The observable to convert to an ObservableAsPropertyHelper.
    /// </param>
    /// <param name="source">
    /// The ReactiveObject that has the property.
    /// </param>
    /// <param name="property">
    /// The name of the property that has changed. Recommended for use with nameof() or a FODY.
    /// </param>
    /// <param name="result">
    /// An out param matching the return value, provided for convenience.
    /// </param>
    /// <param name="getInitialValue">
    /// The function used to retrieve the initial value of the property.
    /// </param>
    /// <param name="deferSubscription">
    /// A value indicating whether the <see cref="ObservableAsPropertyHelper{T}"/>
    /// should defer the subscription to the <paramref name="target"/> source
    /// until the first call to <see cref="ObservableAsPropertyHelper{T}.Value"/>,
    /// or if it should immediately subscribe to the <paramref name="target"/> source.
    /// </param>
    /// <param name="scheduler">
    /// The scheduler that the notifications will be provided on - this should
    /// normally be a Dispatcher-based scheduler.
    /// </param>
    /// <returns>
    /// An initialized ObservableAsPropertyHelper; use this as the backing
    /// field for your property.
    /// </returns>
    public static ObservableAsPropertyHelper<TRet> ToProperty<TObj, TRet>(
        this IObservable<TRet> target,
        TObj source,
        string property,
        out ObservableAsPropertyHelper<TRet> result,
        Func<TRet> getInitialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null) // TODO: Create Test
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        source.ArgumentNullExceptionThrowIfNull(nameof(source));

        if (string.IsNullOrWhiteSpace(property))
        {
            throw new ArgumentException($"'{nameof(property)}' cannot be null or whitespace", nameof(property));
        }

        result = source.ObservableToProperty(target, property, getInitialValue, deferSubscription, scheduler);

        return result;
    }

    private static ObservableAsPropertyHelper<TRet> ObservableToProperty<TObj, TRet>(
        this TObj target,
        IObservable<TRet?> observable,
        Expression<Func<TObj, TRet>> property,
        Func<TRet> getInitialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null)
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        observable.ArgumentNullExceptionThrowIfNull(nameof(observable));
        property.ArgumentNullExceptionThrowIfNull(nameof(property));

        var expression = Reflection.Rewrite(property.Body);

        var parent = expression.GetParent() ?? throw new ArgumentException("The property expression does not have a valid parent.", nameof(property));
        if (parent.NodeType != ExpressionType.Parameter)
        {
            throw new ArgumentException("Property expression must be of the form 'x => x.SomeProperty'");
        }

        var memberInfo = expression.GetMemberInfo() ?? throw new ArgumentException("The property expression does not point towards a valid member.", nameof(property));
        var name = memberInfo.Name;
        if (expression is IndexExpression)
        {
            name += "[]";
        }

        return new ObservableAsPropertyHelper<TRet>(
                                                    observable,
                                                    _ => target.RaisingPropertyChanged(name),
                                                    _ => target.RaisingPropertyChanging(name),
                                                    getInitialValue,
                                                    deferSubscription,
                                                    scheduler);
    }

    private static ObservableAsPropertyHelper<TRet> ObservableToProperty<TObj, TRet>(
        this TObj target,
        IObservable<TRet?> observable,
        Expression<Func<TObj, TRet>> property,
        bool deferSubscription = false,
        IScheduler? scheduler = null)
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        observable.ArgumentNullExceptionThrowIfNull(nameof(observable));
        property.ArgumentNullExceptionThrowIfNull(nameof(property));

        var expression = Reflection.Rewrite(property.Body);

        var parent = expression.GetParent() ?? throw new ArgumentException("The property expression does not have a valid parent.", nameof(property));
        if (parent.NodeType != ExpressionType.Parameter)
        {
            throw new ArgumentException("Property expression must be of the form 'x => x.SomeProperty'");
        }

        var memberInfo = expression.GetMemberInfo() ?? throw new ArgumentException("The property expression does not point towards a valid member.", nameof(property));
        var name = memberInfo.Name;
        if (expression is IndexExpression)
        {
            name += "[]";
        }

        return new ObservableAsPropertyHelper<TRet>(
                                                    observable,
                                                    _ => target.RaisingPropertyChanged(name),
                                                    _ => target.RaisingPropertyChanging(name),
                                                    () => default,
                                                    deferSubscription,
                                                    scheduler);
    }

    private static ObservableAsPropertyHelper<TRet> ObservableToProperty<TObj, TRet>(
        this TObj target,
        IObservable<TRet?> observable,
        string property,
        Func<TRet> getInitialValue,
        bool deferSubscription = false,
        IScheduler? scheduler = null)
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        observable.ArgumentNullExceptionThrowIfNull(nameof(observable));
        property.ArgumentNullExceptionThrowIfNull(nameof(property));

        return new ObservableAsPropertyHelper<TRet>(
                                                    observable,
                                                    _ => target.RaisingPropertyChanged(property),
                                                    _ => target.RaisingPropertyChanging(property),
                                                    getInitialValue,
                                                    deferSubscription,
                                                    scheduler);
    }

    private static ObservableAsPropertyHelper<TRet> ObservableToProperty<TObj, TRet>(
        this TObj target,
        IObservable<TRet?> observable,
        string property,
        bool deferSubscription = false,
        IScheduler? scheduler = null)
        where TObj : class, IReactiveObject
    {
        target.ArgumentNullExceptionThrowIfNull(nameof(target));
        observable.ArgumentNullExceptionThrowIfNull(nameof(observable));
        property.ArgumentNullExceptionThrowIfNull(nameof(property));

        return new ObservableAsPropertyHelper<TRet>(
                                                    observable,
                                                    _ => target.RaisingPropertyChanged(property),
                                                    _ => target.RaisingPropertyChanging(property),
                                                    () => default,
                                                    deferSubscription,
                                                    scheduler);
    }
}
