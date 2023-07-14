# :gem: Fast Workspace :gem: 

## TL;DR
Launch a group of jobs in just one click, or configure it to run at startup. A job can be various things like launching an application, opening a terminal with a command, opening a folder, ... Separate that by workspace so that you can easily switch between projects without the pain of switching everything manually.

## Goal and motivation :fire:
A small project that I hope to make available quickly as a personal side project. It's supposed to allow you to manage multiple workspaces, a workspace would be a list of job that you can execute at will or setup to launch at startup. A job could be multiple thing but mostly launching applications or software, opening window terminal tabs, executing PowerShell script, opening Visual Studio Code folders or launching Visual Studio solutions. You can have multiple workspace so that if you switch project during the day you can set up everything on your machine in just one click. The application will be available on Windows as a desktop application and will be open source and free.

The idea behind it is to make it easier to switch between project on your computer. As a software engineer, we can sometime switch between totally different code base and need to reopen lots of different applications and tools depending on the project. This tool will be made to help you achieve that with hopefully less pain.

## Technology
The application would be developed using the [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui) Blazor hybrid platform and would be available only on Windows.

## Project
This is a small project to try a first app using [.NET MAUI](https://learn.microsoft.com/en-us/dotnet/maui/what-is-maui) and is not intended to be really complex nor long term maintained. The project is open source and free to use, feel free to download and use it or to fork it and develop your own features. Every feedback or suggestions of change or feature would be appreciated, feel free to contact me by mail [abouaban2@gmail.com](mailto:abouaban2@gmail.com).

## Concepts
### Workspaces
A list of jobs to be executed in a logical context (project, ...).

### Jobs
A job is a task to execute, like opening a folder or an application etc.

### Behind the scene
To allow more flexibility, every workspace will compile to a PowerShell script that will be responsible for executing the jobs, it will be invoked when the user executes a workspace job or at startup. This way, a user can use this app only as a startup script generator and fully customized them at will.
Workspaces will be saved as JSON files and will be loaded when launching the application. Making it also available for customization if needed.

## Todo

### Setup
- [x] Setup project with MAUI and Blazor
- [x] Add Mud Blazor to the project

### Minimum Viable Product
- [ ] Start a hard-coded workspace and make it work
- [ ] Persist and load the workspaces
- [ ] Allow the user to create and edit workspaces in the UI
- [ ] Allow the user to set up the workspace jobs execution at startup
- [ ] Improve the setup of workspace with helpers in the UX

### Nice to have
- [ ] Publish the application to the public
- [ ] Build the application on new main commit and publish it to GitHub to make it available for download