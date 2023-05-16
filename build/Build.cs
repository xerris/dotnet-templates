using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.ProjectModel;
using Nuke.Common.Tooling;
using Nuke.Common.Tools.DotNet;
using Xerris.Nuke.Components;
using static Nuke.Common.ControlFlow;
using static Nuke.Common.Tools.DotNet.DotNetTasks;

// ReSharper disable RedundantExtendsListEntry
// ReSharper disable InconsistentNaming

partial class Build : NukeBuild,
    IHasGitRepository,
    IHasVersioning,
    IClean,
    IRestore,
    IFormat,
    ICompile,
    IPack,
    IPush
{
    /// Support plugins are available for:
    ///   - JetBrains ReSharper        https://nuke.build/resharper
    ///   - JetBrains Rider            https://nuke.build/rider
    ///   - Microsoft VisualStudio     https://nuke.build/visualstudio
    ///   - Microsoft VSCode           https://nuke.build/vscode

    public static int Main() => Execute<Build>(x => ((ICompile) x).Compile);

    [Solution]
    readonly Solution Solution;
    Solution IHasSolution.Solution => Solution;

    public IEnumerable<string> ExcludedFormatPaths => Enumerable.Empty<string>();

    public bool RunFormatAnalyzers => true;

    Target ICompile.Compile => _ => _
        .Inherit<ICompile>()
        .DependsOn<IFormat>(x => x.VerifyFormat);

    Configure<DotNetPublishSettings> ICompile.PublishSettings => _ => _
        .When(!ScheduledTargets.Contains(((IPush) this).Push), _ => _
            .ClearProperties());

    Configure<DotNetPackSettings> IPack.PackSettings => _ => _
        .SetProject(Solution.GetAllProjects("Xerris.Templates").Single());

    Target IPush.Push => _ => _
        .Inherit<IPush>()
        .Consumes(this.FromComponent<IPack>().Pack)
        .Requires(() => this.FromComponent<IHasGitRepository>().GitRepository.Tags.Any())
        .WhenSkipped(DependencyBehavior.Execute);

    Target Install => _ => _
        .DependsOn<IPack>()
        .Executes(() =>
        {
            var packageName = Solution.GetProject("Xerris.Templates")!.Name;
            var version = this.FromComponent<IHasVersioning>().Versioning.NuGetVersionV2;
            var packagePath = this.FromComponent<IPack>().PackagesDirectory / $"{packageName}.{version}.nupkg";

            SuppressErrors(() => DotNet($"new uninstall {packageName}"));
            DotNet($"new install {packagePath}");
        });
}
