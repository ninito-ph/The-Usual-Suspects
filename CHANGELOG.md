# Changelog
All notable changes to this project will be documented in this file.

The format is based on [Keep a Changelog](https://keepachangelog.com/en/1.0.0/),
and this project adheres to [Semantic Versioning](https://semver.org/spec/v2.0.0.html).

## [Unreleased]

## [v2.0.1-minimal] - Quiet Quarterstaff Release - 2022-01-14
### Removed
- All samples

## [v2.0.1] - Happy Halberd Release - 2022-01-14
### Changed
- Make FlexibleLayout's OnValidate Editor-only
- Seal FlexibleLayout class
- Add editor-time exit playmode to GameManager QuitApplication
- Move GameManager to new namespace
- Make GameManager members virtual

## [v2.0.0] - Sentient Sword Release - 2022-01-13
### Added
- Volume slider script for quick volume slider functionality

### Changed
- Restored UI Page Manager to the old Window Manager, as the Window Manager is simpler, generally more useful and less complicated
- Removed methods from the Window Manager that violated the SRP

## [v1.0.0] - Hungry Polearm Release - 2021-09-08
### Added
- Initial release