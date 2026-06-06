# Grime.Modding.Templates

Templates for modding Grime pre-configured with many quality of life features and automations.

## Usage

1. Install the .NET 10 SDK. This is required for all analyzers to work correctly.
1. Ensure your IDE is up to date; Visual Studio 2026 or Rider 2025.3 or higher are recommended. Visual Studio
   Code should also work but has not been verified.
1. In a terminal, navigate to the folder you want to create a mod, then run the following commands:

```
> dotnet new install Grime.Modding.Templates

> dotnet new grimeplugin --github-username YourGitHubUsername
```

Use `dotnet new grimeplugin --help` to see additional options. Visual Studio and Rider both also provide a GUI for project
creation once the template is installed.

The default version of BepInEx 5 will *not* work for GRIME. You need to use the custom built version for the game, which you can
get from Thunderstore or directly from https://github.com/BadMagic100/BepInExPack-Grime/releases/latest.

## What's included in the template?

The template creates the following items for you:

- A basic plugin class for your mod
- A Directory.Build.props file where you can set metadata shared between Thunderstore and the build system in a single place
- A GrimePath.props file making it easy to point the build system at your local files without committing local paths to Git
- A project file that automatically copies your mod to your game folder and creates a Thunderstore
  package when building your mod using [Thunderstore CLI](https://github.com/thunderstore-io/thunderstore-cli)
- Placeholder metadata for packaging your mod in Thunderstore, including a lovely default icon.
- A GitHub actions workflow to help automatically publish your mod to GitHub Releases and Thunderstore.
  Minor additional setup is required for publishing; see steps below.


## Items that might need to be modified

There are a few scenarios where the template makes a best guess. The following things may need to be changed
in some scenarios:

- `website_url` in thunderstore.toml and `RepositoryUrl` in the .csproj assume that you are using GitHub and that the
  repository name is the name of your project. If either is not the case, you will have to update these URLs.
- Most metadata contained in thunderstore.toml is placeholder. You will have to update it before publishing your mod.
- The Thunderstore icon should be replaced. The default icon is included as a reference for the appropriate size and
  file path only.
- The `id` field in the plugin class similarly assumes that you are using GitHub. This ID is the BepInEx plugin GUID
  which means it should be globally unique and should not be changed after your first release. As a best practice, it
  is recommended to prefix the ID with the reverse of a domain name you own. All GitHub users have `username.github.io`
  for free by default, so the default prefix is `io.github.username`.

## Publishing with GitHub Actions

The template contains a workflow to automatically publish your mod whenever you update the version in Directory.Build.props.
The workflow is defined in `.github/workflows/build-publish.yml`. By default, publishing is disabled so that you can iterate
on your mod freely before the initial release. When you are ready to make your first release, simply change the `allow-release`
output from `'false'` to `'true'` (make sure to configure Thunderstore and NuGet first if desired). 

Publishing to Thunderstore requires you to create an API key. Without setting the API key,
publishing is skipped (ie, the workflow will not fail). API keys are secret, and should be 
treated similarly to a password. You can follow [GitHub's documentation] for how to add a secret to a repository. Steps for
configuring the necessary API keys can be found in the next sections.

### Publishing to GitHub Releases

Publishing to GitHub releases is enabled by default. It's recommended to enable release immutability from the General
tab of your repository settings.

If you want to change the format of your release notes, you can do so by updating the `release-github` job.
Documentation for the release action can be found at https://github.com/softprops/action-gh-release.

### Publishing to Thunderstore

Thunderstore is the recommended place to publish most mods. In order to publish to Thunderstore, you will need to set
the `THUNDERSTORE_API_KEY` secret on your repository. To get a Thunderstore API key, do the following steps:

1. Go to [your teams](https://thunderstore.io/settings/teams/) on Thunderstore and create one if needed.
   It's common for most developers to have a single team which is the same as their username on Thunderstore.
2. Click the team that you will publish the package under, and click Service Accounts.
3. Add a new service account. It's recommended to do this once per repository so that if your token is accidentally leaked,
   you can revoke it without impacting your other mods' build workflows.
4. Copy the provided API key and add it to your repository as a secret named `THUNDERSTORE_API_KEY`.

Before attempting to publish, it's a good idea to check that your manifest is valid. Thunderstore provides a manifest validator 
[here](https://thunderstore.io/tools/manifest-v1-validator/). Your manifest is automatically generated during build,
and can be found in the zip file in the `thunderstore/dist` directory.

## Publishing manually

When using this template, automated publishing via GitHub Actions should be preferred in most cases. Some reasons that might
warrant manual publishing include:

- Publishing to GitHub releases succeeded, but Thunderstore failed, such as if you forgot to set API keys 
  before enabling your first release
- You aren't using GitHub to host your repository

There are 2 ways to manually publish; using a workflow dispatch or via a terminal.

### Manually publishing via workflow dispatch

The workflow comes pre-configured with a workflow dispatch trigger that can forcibly re-publish to Thunderstore.
To do this, navigate to your repository's Actions tab and open the page for the Build workflow. Then, click the
"Run workflow" dropdown and select to republish to Thunderstore. You will still need to configure API keys
(as described above) for publishing to succeed.

### Manually publishing via a terminal

The build workflow can serve as a reference for the steps you need to do. `dotnet build` will build your mod and
create a Thunderstore package in `thunderstore/dist`

Once your build artifacts are created, you can upload them either by using the Thunderstore website's upload functionality,
or you can use the same CLI commands used in the publish jobs.
