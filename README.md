# art-gallery-website

## Tech Stack

### Backend

- [ASP.NET Web Forms](https://www.tutorialspoint.com/asp.net/asp.net_first_example.htm)
- [MS SQL Server](https://www.microsoft.com/en-us/sql-server)
- [C#](https://docs.microsoft.com/en-us/dotnet/csharp/)

### Frontend

- [Vue 3](https://v3.vuejs.org/guide/introduction.html)
- [TailwindCSS](https://tailwindcss.com/)
- [TypeScript](https://www.typescriptlang.org/)

## How to run this project?

Currently, we did not find any way to run this project outside of Visual Studio since it depends on Windows's IIS Express Server. Therefore, `git clone` the project to any where locally then open it in Visual Studio and open `ArtGalleryWebsite.sln` and run the project in Visual Studio.

## How to start contributing?

### Pre-requisites

First of all, you'll have to be able to set up a local development environment. There are some things that you have to get set up. First, make sure your system has Node installed, else you can use [nvm](https://github.com/nvm-sh/nvm) or if you're on Windows [nvm-windows](https://github.com/coreybutler/nvm-windows).

For IDEs, you'll definitely need [Visual Studio](https://visualstudio.microsoft.com/) in this case, then we recommned you getting [Visual Studio Code](https://code.visualstudio.com/) in this case, it helps working with Vue and Typescript a lot easier. Grab the [Vetur](https://marketplace.visualstudio.com/items?itemName=octref.vetur) plugin for syntax highlighting and code-formatting for Vue.

### Running the development environment

1. Install yarn if you haven't already `npm install -g yarn`
2. Open a terminal instance at the project root and run `yarn`, this will install the required dependencies
3. Run `yarn dev`, this will start webpack and tailwind in watch mode
4. Open up Visual Studio and open `ArtGalleryWebsite.sln` and run the IIS Express server.

That's it! Visual Studio should open up your browser and you can now start contributing to the project!

## Project Structure

It is a modifed from Visual Studio's ASP.NET Web Forms template. Most of the things follow standard ASP.NET standards except that we have our frontend Vue/Typescript files located in `Scripts/src`.

### Vue / Typescript

- `Scripts/src/components` - All Vue components should be located here
- `Scripts/src/pages` - The page component for a specific page eg. Home.vue
- `Scripts/src/types` - Typescript types files are stored here
- `Scripts/src/utils` - Utilities or helpers functions are stored here
- `Scripts/dist` - This folder stores the bundled javascript files by Webpack

In `Scripts/src` you will see some javascript files eg. HomePage.js this is for mounting the page component onto ASP.NET's aspx file.
