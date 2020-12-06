# FileSystemPosix
A small helper for the POSIX file system for C# / .NET.

The main purpose is to extend the .NET file system API with
access to

* owner user name of a file or directory
* owner group name of a file or directory
* owner user id of a file or directory
* owner group id of a file or directory
* permissions of the file or directory.

## Under development

This library is currently under development and considered as unstable.

## Versioning

This library uses semver 2.0 for versioning. It will be considered as
stable after the release 1.0.0.

Using the development version, i.e. version 0.x, might result in API changes and unknown runtime errors.

## Development

Development takes place in three branches:

* **development**: Current unstable branch.
* **staging**: Current branch with new features, but not with an unknown maturity. Ready for pre-releases.
* **main**: Stable and mature branch. Used for releases.
