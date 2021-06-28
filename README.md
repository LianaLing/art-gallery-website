# art-gallery-website

## How to run this project?

Currently, did not find anyway to run this project outside of Visual Studio since it depends on Windows's IIS Express Server. Therefore, `git clone` the project to anywhere locally then open it in Visual Studio and open `ArtGalleryWebsite.sln` and run the project in Visual Studio.

## How to use TailwindCSS in this project

We're using TailwindCSS's CLI and JIT mode for dynamically generating the CSS file. Refer this video to grasp how it works [What's new in TailwindCSS 2.2](https://www.youtube.com/watch?v=DxcJbrs6rKk).

#### Pre-requisites
In order to run the TailwindCSS CLI, make sure you have NodeJS and NPM installed. Use this [nvm-windows](https://github.com/coreybutler/nvm-windows) to install Node on Windows.

First, cd into the project root and run this following command

```sh
npx tailwindcss -o Content/tailwind.css -w
```

This will watch the folder and rebuild `tailwind.css` whenever extra classes is added.

For a detailed docs of what the command is doing refer to [TailwindCSS Docs](https://tailwindcss.com/docs/installation#using-tailwind-cli)

