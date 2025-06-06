// Copyright (c) 2025 .NET Foundation and Contributors. All rights reserved.
// Licensed to the .NET Foundation under one or more agreements.
// The .NET Foundation licenses this file to you under the MIT license.
// See the LICENSE file in the project root for full license information.

using Foundation;

using UIKit;

using NSAction = System.Action;

namespace ReactiveUI;

/// <summary>
/// <para>
/// AutoSuspend-based App Delegate. To use AutoSuspend with iOS, change your
/// AppDelegate to inherit from this class, then call:
/// </para>
/// <para>Locator.Current.GetService{ISuspensionHost}().SetupDefaultSuspendResume();.</para>
/// <para>This will get your suspension host.</para>
/// </summary>
public class AutoSuspendHelper : IEnableLogger, IDisposable
{
    private readonly Subject<UIApplication> _finishedLaunching = new();
    private readonly Subject<UIApplication> _activated = new();
    private readonly Subject<UIApplication> _backgrounded = new();

    private bool _isDisposed;

    /// <summary>
    /// Initializes a new instance of the <see cref="AutoSuspendHelper"/> class.
    /// </summary>
    /// <param name="appDelegate">The uiappdelegate.</param>
#if NET6_0_OR_GREATER
    [RequiresDynamicCode("The method uses reflection and will not work in AOT environments.")]
    [RequiresUnreferencedCode("The method uses reflection and will not work in AOT environments.")]
#endif
    public AutoSuspendHelper(UIApplicationDelegate appDelegate)
    {
        Reflection.ThrowIfMethodsNotOverloaded(
                                               nameof(AutoSuspendHelper),
                                               appDelegate,
                                               nameof(FinishedLaunching),
                                               nameof(OnActivated),
                                               nameof(DidEnterBackground));

        RxApp.SuspensionHost.IsLaunchingNew = Observable<Unit>.Never;
        RxApp.SuspensionHost.IsResuming = _finishedLaunching.Select(_ => Unit.Default);
        RxApp.SuspensionHost.IsUnpausing = _activated.Select(_ => Unit.Default);

        var untimelyDeath = new Subject<Unit>();
        AppDomain.CurrentDomain.UnhandledException += (o, e) => untimelyDeath.OnNext(Unit.Default);

        RxApp.SuspensionHost.ShouldInvalidateState = untimelyDeath;

        RxApp.SuspensionHost.ShouldPersistState = _backgrounded.SelectMany(app =>
        {
            var taskId = app.BeginBackgroundTask(new NSAction(() => untimelyDeath.OnNext(Unit.Default)));

            // NB: We're being force-killed, signal invalidate instead
            if (taskId == UIApplication.BackgroundTaskInvalid)
            {
                untimelyDeath.OnNext(Unit.Default);
                return Observable<IDisposable>.Empty;
            }

            return Observable.Return(Disposable.Create(() => app.EndBackgroundTask(taskId)));
        });
    }

    /// <summary>
    /// Gets the launch options.
    /// </summary>
    /// <value>
    /// The launch options.
    /// </value>
    public IDictionary<string, string>? LaunchOptions { get; private set; }

    /// <summary>
    /// Advances the finished launching observable.
    /// Finisheds the launching.
    /// </summary>
    /// <param name="application">The application.</param>
    /// <param name="launchOptions">The launch options.</param>
    public void FinishedLaunching(UIApplication application, NSDictionary launchOptions)
    {
        LaunchOptions = launchOptions is not null
                            ? launchOptions.Keys.ToDictionary(k => k.ToString(), v => launchOptions[v].ToString())
                            : [];

        // NB: This is run in-context (i.e. not scheduled), so by the time this
        // statement returns, UIWindow should be created already
        _finishedLaunching.OnNext(application);
    }

    /// <summary>
    /// Advances the on activated observable.
    /// </summary>
    /// <param name="application">The application.</param>
    public void OnActivated(UIApplication application) => _activated.OnNext(application);

    /// <summary>
    /// Advances the enter background observable.
    /// </summary>
    /// <param name="application">The application.</param>
    public void DidEnterBackground(UIApplication application) => _backgrounded.OnNext(application);

    /// <inheritdoc />
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }

    /// <summary>
    /// Disposes of any disposable entities within the class.
    /// </summary>
    /// <param name="isDisposing">If we are going to call Dispose methods on field items.</param>
    protected virtual void Dispose(bool isDisposing)
    {
        if (_isDisposed)
        {
            return;
        }

        if (isDisposing)
        {
            _activated?.Dispose();
            _backgrounded?.Dispose();
            _finishedLaunching?.Dispose();
        }

        _isDisposed = true;
    }
}
