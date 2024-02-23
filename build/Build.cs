using System.Collections.Generic;
using System.Linq;
using Nuke.Common;
using Nuke.Common.IO;
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

    public IEnumerable<AbsolutePath> ExcludedFormatPaths => Enumerable.Empty<AbsolutePath>();

    public bool RunFormatAnalyzers => IsServerBuild;

    Target ICompile.Compile => d => d
        .Inherit<ICompile>()
        .DependsOn<IFormat>(x => x.VerifyFormat);

    Configure<DotNetPublishSettings> ICompile.PublishSettings => s => s
        .When(!ScheduledTargets.Contains(((IPush) this).Push), x => x
            .ClearProperties());

    Project TemplatesProject => Solution.GetAllProjects("Xerris.Templates").Single();

    Configure<DotNetPackSettings> IPack.PackSettings => s => s
        .SetProject(TemplatesProject);

    Target IPush.Push => t => t
        .Inherit<IPush>()
        .Consumes(this.FromComponent<IPack>().Pack)
        .Requires(() => this.FromComponent<IHasGitRepository>().GitRepository.Tags.Any())
        .WhenSkipped(DependencyBehavior.Execute);

    Target Install => t => t
        .Description("Tests template package installation by building and re-installing the package locally.")
        .DependsOn<IPack>()
        .Executes(() =>
        {
            var packageName = TemplatesProject.Name;
            var version = this.FromComponent<IHasVersioning>().Versioning.NuGetVersionV2;
            var packagePath = this.FromComponent<IPack>().PackagesDirectory / $"{packageName}.{version}.nupkg";

            SuppressErrors(() => DotNet($"new --uninstall {packageName}"));
            DotNet($"new --install {packagePath}");
        });
}
