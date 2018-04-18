# color-palette
A project designed to identify and return key colors in an image.

# install & run
1. Clone repo
2. Open and build ColorPalette.sln
3a. Open Web.config and change the connection string from "(localdb)\MSSQLLOCALDB" to your local db's location
3b. Run "update-database" in Package Manager Console
4. In a separate window (preferably VS Code) open the color-palette folder and run "npm install" to restore the packages
5. Run ColorPalette.sln in Visual Studio
6. In VS Code, run "ng serve --open"