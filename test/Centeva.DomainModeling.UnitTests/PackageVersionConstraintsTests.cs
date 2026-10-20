using System.Xml.Linq;

namespace Centeva.DomainModeling.UnitTests;

public class PackageVersionConstraintsTests
{
    [Fact]
    public void MediatR_ShouldNotExceedVersion12()
    {
        // Arrange & Act
        var mediatRRange = GetPackageVersionRange("MediatR");

        // Assert
        if (mediatRRange is null)
        {
            Assert.Skip("MediatR is not installed in this solution - consider removing this test");
        }

        var (min, max, maxInclusive) = mediatRRange.Value;
        var version13 = new Version(13, 0, 0);

        min.Should().NotBeNull("MediatR must specify a minimum version");
        min!.Major.Should().Be(12,
            $"MediatR minimum version must be 12.x (found {min}). " +
            "Version 13+ has a commercial license.");

        max.Should().NotBeNull("MediatR must specify an upper bound to prevent upgrading to version 13+");
        if (maxInclusive)
        {
            max.Should().BeLessThan(version13,
                $"MediatR version must be 12.x (found upper bound {max}). " +
                "Version 13+ has a commercial license.");
        }
        else
        {
            max.Should().BeLessThanOrEqualTo(version13,
                $"MediatR version must be 12.x (found upper bound {max}). " +
                "Version 13+ has a commercial license.");
        }
    }

    /// <summary>
    /// Reads a package's version from Directory.Packages.props and parses it as a NuGet version range.
    /// A bare version (e.g. "12.4.1") is treated as pinned to exactly that version.
    /// </summary>
    private static (Version? Min, Version? Max, bool MaxInclusive)? GetPackageVersionRange(string packageName)
    {
        var packagesPropsPath = Path.Combine(
            GetSolutionRoot(),
            "Directory.Packages.props");

        var document = XDocument.Load(packagesPropsPath);
        var versionString = document
            .Descendants("PackageVersion")
            .FirstOrDefault(x => x.Attribute("Include")?.Value == packageName)
            ?.Attribute("Version")?.Value
            ?.Trim();

        if (versionString is null)
        {
            return null;
        }

        if (!versionString.StartsWith('[') && !versionString.StartsWith('('))
        {
            var version = ParseVersion(versionString);
            return (version, version, true);
        }

        var maxInclusive = versionString.EndsWith(']');
        var bounds = versionString[1..^1].Split(',');

        if (bounds.Length == 1)
        {
            // Exact match, e.g. "[12.4.1]"
            var version = ParseVersion(bounds[0]);
            return (version, version, true);
        }

        var min = string.IsNullOrWhiteSpace(bounds[0]) ? null : ParseVersion(bounds[0]);
        var max = string.IsNullOrWhiteSpace(bounds[1]) ? null : ParseVersion(bounds[1]);
        return (min, max, maxInclusive);
    }

    private static Version ParseVersion(string value)
    {
        // Normalize so that "13.0" and "13.0.0" compare as equal
        var version = Version.Parse(value.Trim());
        return new Version(version.Major, version.Minor, Math.Max(version.Build, 0));
    }

    private static string GetSolutionRoot()
    {
        var directory = Directory.GetCurrentDirectory();
        while (directory != null)
        {
            if (File.Exists(Path.Combine(directory, "Directory.Packages.props")) ||
                Directory.GetFiles(directory, "*.slnx").Length != 0)
            {
                return directory;
            }
            directory = Directory.GetParent(directory)?.FullName;
        }
        throw new InvalidOperationException("Could not find solution root directory");
    }
}
