# Example Automation Frameworks

![Build](https://img.shields.io/github/actions/workflow/status/chasemieras/automation-framework-example/.github/workflows/dotnet.yml)
![License](https://img.shields.io/github/license/chasemieras/automation-framework-example)

## Table of Contents

- [Description](#description)
- [Framework Overviews](#framework-overviews)
  - [C# Selenium Framework](#c-selenium-framework)
    - [Overview](#overview)
    - [Tech Stack](#tech-stack)
      - [Framework NuGet Packages](#framework-nuget-packages)
      - [Unit Testing NuGet Packages](#unit-testing-nuget-packages)
- [Installation & Setup](#installation--setup)

## Description

This repository serves as the home of the example frameworks that I worked on as a personal project.

My goal is to have example frameworks in .Net, Java and TypeScript using Selenium and Playwright.
These will all have a webdriver automation tool (like Selenium) in a [wrapper](https://en.wikipedia.org/wiki/Wrapper_function), to allow easy swapping between tools.

---

## Framework Overviews

### C# Selenium Framework

#### Overview

This Framework uses Selenium and ExtentReports to run automation seamlessly. It is written in dotnet 8.

#### Tech Stack

##### Framework NuGet Packages

- [ExtentReports](https://extentreports.com/)
  - Used to generate HTML Reports during testing
- [Selenium](https://www.selenium.dev/)
  - The primary test tool to run automation against a website
  - Also uses [Selenium Manager](https://www.selenium.dev/documentation/selenium_manager/), which automatically handles what version the driver will run based on what is on the system

##### Unit Testing NuGet Packages

- [Moq](https://github.com/moq/moq4)
  - Enables mocking of code from the framework
- [xUnit](https://xunit.net/)
  - The selected testing framework
- [FluentAssertions](https://fluentassertions.com/)
  - A much better assertion package, reads like sentences!

### Installation & Setup

Prerequisites before pulling code:

- Have [VS Code](https://code.visualstudio.com/) installed (conversely, you can just install [Visual Studios](https://visualstudio.microsoft.com/) and ignore the extensions below. This will assume you are using VS Code however.)
  - Have the following extensions installed:
    - [.Net Install Tool](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.vscode-dotnet-runtime)
    - [C#](https://marketplace.visualstudio.com/items?itemName=ms-dotnettools.csharp)
    - [CD Dev Kit](https://marketplace.visualstudio.com/items?itemName=ms-vscode.vs-code-cd-devkit)
    - [Code Spell Checker](https://marketplace.visualstudio.com/items?itemName=streetsidesoftware.code-spell-checker)
    - [Git History](https://marketplace.visualstudio.com/items?itemName=donjayamanne.githistory)
    - [GitHub Actions](https://marketplace.visualstudio.com/items?itemName=cschleiden.vscode-github-actions)
    - [indent-rainbow](https://marketplace.visualstudio.com/items?itemName=oderwat.indent-rainbow)
    - [markdownlint](https://marketplace.visualstudio.com/items?itemName=DavidAnson.vscode-markdownlint)
    - [Test Adapter Converter](https://marketplace.visualstudio.com/items?itemName=hbenl.test-adapter-converter)
    - [Test Explorer UI](https://marketplace.visualstudio.com/items?itemName=hbenl.vscode-test-explorer)
- Install [.Net 8](https://dotnet.microsoft.com/en-us/download/dotnet/8.0)

Then, [clone the repo](https://github.com/chasemieras/automation-framework-example.git).

You can view all example and unit tests in the Test Explorer from [Test Explorer UI](https://marketplace.visualstudio.com/items?itemName=hbenl.vscode-test-explorer).
