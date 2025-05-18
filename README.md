# Visual Studio Cyrillic Highlighter
Visual Studio 2019 and 2022 extension for highlighting Cyrillic symbols. It underlines all Cyrillic characters in the code and brightly highlights in red letters on a yellow background for mixed Cyrillic and Latin identifiers.
SSMS extension gets all mixed cyrillic and latin identifiers in current document by menu item click.
## Compilation
1. Check if appropriate Visual Studio version is installed
    - **Visual Studio 2019** for vs_cyrillic_underline_2019 
    - **Visual Studio 2022** for vs_cyrillic_underline_2022
    - **Visual Studio 2017** for ssms_cyryllic_underline
2. Check if the 'Visual Studio Extension Development' component is installed in Visual Studio. If not, install it via the Visual Studio Installer
3. Open the *.sln file in the appropriate Visual Studio version:
    - **vs_cyrillic_underline_2019** in Visual Studio 2019
    - **vs_cyrillic_underline_2022** in Visual Studio 2022
    - **ssms_cyryllic_underline** in Visual Studio 2017
4. Build solution
5. The bin/release directory will contain the installation package.
## Installation
### Visual Studio
1. Close all instances of Visual Studio, SSMS and Visual Studio Installer.
2. Run the *.vsix file
3. When the window appears, click Install.
### SSMS
1. Copy the installation package to <ssms.exe file location>\Extensions\ssms_cyrillic_underline
2. Restart SSMS